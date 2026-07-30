using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000254 RID: 596
	internal class SNITCPHandle : SNIHandle
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x00085304 File Offset: 0x00083504
		public override void Dispose()
		{
			lock (this)
			{
				if (this._sslOverTdsStream != null)
				{
					this._sslOverTdsStream.Dispose();
					this._sslOverTdsStream = null;
				}
				if (this._sslStream != null)
				{
					this._sslStream.Dispose();
					this._sslStream = null;
				}
				if (this._tcpStream != null)
				{
					this._tcpStream.Dispose();
					this._tcpStream = null;
				}
				this._stream = null;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00085390 File Offset: 0x00083590
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00085398 File Offset: 0x00083598
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000853A0 File Offset: 0x000835A0
		public SNITCPHandle(string serverName, int port, long timerExpire, object callbackObject, bool parallel)
		{
			this._writeScheduler = new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler;
			this._writeTaskFactory = new TaskFactory(this._writeScheduler);
			this._callbackObject = callbackObject;
			this._targetServer = serverName;
			try
			{
				TimeSpan timeSpan = default(TimeSpan);
				bool flag = long.MaxValue == timerExpire;
				if (!flag)
				{
					timeSpan = DateTime.FromFileTime(timerExpire) - DateTime.Now;
					timeSpan = ((timeSpan.Ticks < 0L) ? TimeSpan.FromTicks(0L) : timeSpan);
				}
				Task<Socket> task;
				if (parallel)
				{
					Task<IPAddress[]> hostAddressesAsync = Dns.GetHostAddressesAsync(serverName);
					hostAddressesAsync.Wait(timeSpan);
					IPAddress[] result = hostAddressesAsync.Result;
					if (result.Length > 64)
					{
						this.ReportTcpSNIError(0U, 47U, string.Empty);
						return;
					}
					task = SNITCPHandle.ParallelConnectAsync(result, port);
				}
				else
				{
					task = SNITCPHandle.ConnectAsync(serverName, port);
				}
				if (!(flag ? task.Wait(-1) : task.Wait(timeSpan)))
				{
					this.ReportTcpSNIError(0U, 40U, string.Empty);
					return;
				}
				this._socket = task.Result;
				if (this._socket == null || !this._socket.Connected)
				{
					if (this._socket != null)
					{
						this._socket.Dispose();
						this._socket = null;
					}
					this.ReportTcpSNIError(0U, 40U, string.Empty);
					return;
				}
				this._socket.NoDelay = true;
				this._tcpStream = new NetworkStream(this._socket, true);
				this._sslOverTdsStream = new SslOverTdsStream(this._tcpStream);
				this._sslStream = new SslStream(this._sslOverTdsStream, true, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
			}
			catch (SocketException ex)
			{
				this.ReportTcpSNIError(ex);
				return;
			}
			catch (Exception ex2)
			{
				this.ReportTcpSNIError(ex2);
				return;
			}
			this._stream = this._tcpStream;
			this._status = 0U;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000855B4 File Offset: 0x000837B4
		private static async Task<Socket> ConnectAsync(string serverName, int port)
		{
			IPAddress[] array = await Dns.GetHostAddressesAsync(serverName).ConfigureAwait(false);
			IPAddress targetAddrV4 = Array.Find<IPAddress>(array, (IPAddress addr) => addr.AddressFamily == AddressFamily.InterNetwork);
			IPAddress targetAddrV5 = Array.Find<IPAddress>(array, (IPAddress addr) => addr.AddressFamily == AddressFamily.InterNetworkV6);
			Socket socket2;
			if (targetAddrV4 != null && targetAddrV5 != null)
			{
				socket2 = await SNITCPHandle.ParallelConnectAsync(new IPAddress[] { targetAddrV4, targetAddrV5 }, port).ConfigureAwait(false);
			}
			else
			{
				IPAddress ipaddress = ((targetAddrV4 != null) ? targetAddrV4 : targetAddrV5);
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					await socket.ConnectAsync(ipaddress, port).ConfigureAwait(false);
				}
				catch
				{
					socket.Dispose();
					throw;
				}
				socket2 = socket;
			}
			return socket2;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00085604 File Offset: 0x00083804
		private static Task<Socket> ParallelConnectAsync(IPAddress[] serverAddresses, int port)
		{
			if (serverAddresses == null)
			{
				throw new ArgumentNullException("serverAddresses");
			}
			if (serverAddresses.Length == 0)
			{
				throw new ArgumentOutOfRangeException("serverAddresses");
			}
			List<Socket> list = new List<Socket>(serverAddresses.Length);
			List<Task> list2 = new List<Task>(serverAddresses.Length);
			TaskCompletionSource<Socket> taskCompletionSource = new TaskCompletionSource<Socket>();
			StrongBox<Exception> strongBox = new StrongBox<Exception>();
			StrongBox<int> strongBox2 = new StrongBox<int>(serverAddresses.Length);
			foreach (IPAddress ipaddress in serverAddresses)
			{
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				list.Add(socket);
				try
				{
					list2.Add(socket.ConnectAsync(ipaddress, port));
				}
				catch (Exception ex)
				{
					list2.Add(Task.FromException(ex));
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				SNITCPHandle.ParallelConnectHelper(list[j], list2[j], taskCompletionSource, strongBox2, strongBox, list);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000856F4 File Offset: 0x000838F4
		private static async void ParallelConnectHelper(Socket socket, Task connectTask, TaskCompletionSource<Socket> tcs, StrongBox<int> pendingCompleteCount, StrongBox<Exception> lastError, List<Socket> sockets)
		{
			bool success = false;
			try
			{
				await connectTask.ConfigureAwait(false);
				success = tcs.TrySetResult(socket);
				if (success)
				{
					foreach (Socket socket2 in sockets)
					{
						if (socket2 != socket)
						{
							socket2.Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Interlocked.Exchange<Exception>(ref lastError.Value, ex);
			}
			finally
			{
				if (!success && Interlocked.Decrement(ref pendingCompleteCount.Value) == 0)
				{
					if (lastError.Value != null)
					{
						tcs.TrySetException(lastError.Value);
					}
					else
					{
						tcs.TrySetCanceled();
					}
					List<Socket>.Enumerator enumerator = sockets.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Socket socket3 = enumerator.Current;
							socket3.Dispose();
						}
					}
					finally
					{
						int num;
						if (num < 0)
						{
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00085758 File Offset: 0x00083958
		public override uint EnableSsl(uint options)
		{
			this._validateCert = (options & 1U) > 0U;
			try
			{
				this._sslStream.AuthenticateAsClientAsync(this._targetServer).GetAwaiter().GetResult();
				this._sslOverTdsStream.FinishHandshake();
			}
			catch (AuthenticationException ex)
			{
				return this.ReportTcpSNIError(ex);
			}
			catch (InvalidOperationException ex2)
			{
				return this.ReportTcpSNIError(ex2);
			}
			this._stream = this._sslStream;
			return 0U;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000857E0 File Offset: 0x000839E0
		public override void DisableSsl()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				this._sslStream.Dispose();
			}
			this._sslStream = null;
			this._sslOverTdsStream.Dispose();
			this._sslOverTdsStream = null;
			this._stream = this._tcpStream;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0008581F File Offset: 0x00083A1F
		private bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return !this._validateCert || SNICommon.ValidateSslServerCertificate(this._targetServer, sender, cert, chain, policyErrors);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0008583B File Offset: 0x00083A3B
		public override void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
			this._socket.SendBufferSize = bufferSize;
			this._socket.ReceiveBufferSize = bufferSize;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0008585C File Offset: 0x00083A5C
		public override uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				try
				{
					packet.WriteToStream(this._stream);
					num = 0U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportTcpSNIError(ex);
				}
				catch (SocketException ex2)
				{
					num = this.ReportTcpSNIError(ex2);
				}
				catch (IOException ex3)
				{
					num = this.ReportTcpSNIError(ex3);
				}
			}
			return num;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000858EC File Offset: 0x00083AEC
		public override uint Receive(out SNIPacket packet, int timeoutInMilliseconds)
		{
			uint num;
			lock (this)
			{
				packet = null;
				try
				{
					if (timeoutInMilliseconds > 0)
					{
						this._socket.ReceiveTimeout = timeoutInMilliseconds;
					}
					else
					{
						if (timeoutInMilliseconds != -1)
						{
							this.ReportTcpSNIError(0U, 11U, string.Empty);
							return 258U;
						}
						this._socket.ReceiveTimeout = 0;
					}
					packet = new SNIPacket(null);
					packet.Allocate(this._bufferSize);
					packet.ReadFromStream(this._stream);
					if (packet.Length == 0)
					{
						Win32Exception ex = new Win32Exception();
						num = this.ReportErrorAndReleasePacket(packet, (uint)ex.NativeErrorCode, 0U, ex.Message);
					}
					else
					{
						num = 0U;
					}
				}
				catch (ObjectDisposedException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
				catch (SocketException ex3)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex3);
				}
				catch (IOException ex4)
				{
					uint num2 = this.ReportErrorAndReleasePacket(packet, ex4);
					if (ex4.InnerException is SocketException && ((SocketException)ex4.InnerException).SocketErrorCode == SocketError.TimedOut)
					{
						num2 = 258U;
					}
					num = num2;
				}
				finally
				{
					this._socket.ReceiveTimeout = 0;
				}
			}
			return num;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00085A88 File Offset: 0x00083C88
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
			this._receiveCallback = receiveCallback;
			this._sendCallback = sendCallback;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00085A98 File Offset: 0x00083C98
		public override uint SendAsync(SNIPacket packet, SNIAsyncCallback callback = null)
		{
			SNIPacket packet2 = packet;
			this._writeTaskFactory.StartNew(delegate
			{
				try
				{
					SNITCPHandle <>4__this = this;
					lock (<>4__this)
					{
						packet.WriteToStream(this._stream);
					}
				}
				catch (Exception ex)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 35U, ex);
					if (callback != null)
					{
						callback(packet, 1U);
					}
					else
					{
						this._sendCallback(packet, 1U);
					}
					return;
				}
				if (callback != null)
				{
					callback(packet, 0U);
					return;
				}
				this._sendCallback(packet, 0U);
			});
			return 997U;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00085AE4 File Offset: 0x00083CE4
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				packet = new SNIPacket(null);
				packet.Allocate(this._bufferSize);
				try
				{
					packet.ReadFromStreamAsync(this._stream, this._receiveCallback);
					num = 997U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex);
				}
				catch (SocketException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
				catch (IOException ex3)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex3);
				}
			}
			return num;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00085B98 File Offset: 0x00083D98
		public override uint CheckConnection()
		{
			try
			{
				if (!this._socket.Connected || this._socket.Poll(0, SelectMode.SelectError))
				{
					return 1U;
				}
			}
			catch (SocketException ex)
			{
				return this.ReportTcpSNIError(ex);
			}
			catch (ObjectDisposedException ex2)
			{
				return this.ReportTcpSNIError(ex2);
			}
			return 0U;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00085BFC File Offset: 0x00083DFC
		private uint ReportTcpSNIError(Exception sniException)
		{
			this._status = 1U;
			return SNICommon.ReportSNIError(SNIProviders.TCP_PROV, 35U, sniException);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00085C0E File Offset: 0x00083E0E
		private uint ReportTcpSNIError(uint nativeError, uint sniError, string errorMessage)
		{
			this._status = 1U;
			return SNICommon.ReportSNIError(SNIProviders.TCP_PROV, nativeError, sniError, errorMessage);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00085C20 File Offset: 0x00083E20
		private uint ReportErrorAndReleasePacket(SNIPacket packet, Exception sniException)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return this.ReportTcpSNIError(sniException);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00085C32 File Offset: 0x00083E32
		private uint ReportErrorAndReleasePacket(SNIPacket packet, uint nativeError, uint sniError, string errorMessage)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return this.ReportTcpSNIError(nativeError, sniError, errorMessage);
		}

		// Token: 0x040012F0 RID: 4848
		private readonly string _targetServer;

		// Token: 0x040012F1 RID: 4849
		private readonly object _callbackObject;

		// Token: 0x040012F2 RID: 4850
		private readonly Socket _socket;

		// Token: 0x040012F3 RID: 4851
		private NetworkStream _tcpStream;

		// Token: 0x040012F4 RID: 4852
		private readonly TaskScheduler _writeScheduler;

		// Token: 0x040012F5 RID: 4853
		private readonly TaskFactory _writeTaskFactory;

		// Token: 0x040012F6 RID: 4854
		private Stream _stream;

		// Token: 0x040012F7 RID: 4855
		private SslStream _sslStream;

		// Token: 0x040012F8 RID: 4856
		private SslOverTdsStream _sslOverTdsStream;

		// Token: 0x040012F9 RID: 4857
		private SNIAsyncCallback _receiveCallback;

		// Token: 0x040012FA RID: 4858
		private SNIAsyncCallback _sendCallback;

		// Token: 0x040012FB RID: 4859
		private bool _validateCert = true;

		// Token: 0x040012FC RID: 4860
		private int _bufferSize = 4096;

		// Token: 0x040012FD RID: 4861
		private uint _status = uint.MaxValue;

		// Token: 0x040012FE RID: 4862
		private Guid _connectionId = Guid.NewGuid();

		// Token: 0x040012FF RID: 4863
		private const int MaxParallelIpAddresses = 64;
	}
}
