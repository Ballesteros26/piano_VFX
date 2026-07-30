using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows an application to determine whether a remote computer is accessible over the network.</summary>
	// Token: 0x02000667 RID: 1639
	[MonoTODO("IPv6 support is missing")]
	public class Ping : Component, IDisposable
	{
		/// <summary>Occurs when an asynchronous operation to send an Internet Control Message Protocol (ICMP) echo message and receive the corresponding ICMP echo reply message completes or is canceled.</summary>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06003419 RID: 13337 RVA: 0x000C25F8 File Offset: 0x000C07F8
		// (remove) Token: 0x0600341A RID: 13338 RVA: 0x000C2630 File Offset: 0x000C0830
		public event PingCompletedEventHandler PingCompleted;

		// Token: 0x0600341B RID: 13339 RVA: 0x000C2668 File Offset: 0x000C0868
		static Ping()
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Ping.CheckLinuxCapabilities();
				if (!Ping.canSendPrivileged && WindowsIdentity.GetCurrent().Name == "root")
				{
					Ping.canSendPrivileged = true;
				}
				foreach (string text in Ping.PingBinPaths)
				{
					if (File.Exists(text))
					{
						Ping.PingBinPath = text;
						break;
					}
				}
			}
			else
			{
				Ping.canSendPrivileged = true;
			}
			if (Ping.PingBinPath == null)
			{
				Ping.PingBinPath = "/bin/ping";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.Ping" /> class.</summary>
		// Token: 0x0600341C RID: 13340 RVA: 0x000C271C File Offset: 0x000C091C
		public Ping()
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[2];
			randomNumberGenerator.GetBytes(array);
			this.identifier = (ushort)((int)array[0] + ((int)array[1] << 8));
		}

		// Token: 0x0600341D RID: 13341
		[DllImport("libc")]
		private static extern int capget(ref Ping.cap_user_header_t header, ref Ping.cap_user_data_t data);

		// Token: 0x0600341E RID: 13342 RVA: 0x000C2754 File Offset: 0x000C0954
		private static void CheckLinuxCapabilities()
		{
			try
			{
				Ping.cap_user_header_t cap_user_header_t = default(Ping.cap_user_header_t);
				Ping.cap_user_data_t cap_user_data_t = default(Ping.cap_user_data_t);
				cap_user_header_t.version = 429392688U;
				int num = -1;
				try
				{
					num = Ping.capget(ref cap_user_header_t, ref cap_user_data_t);
				}
				catch (Exception)
				{
				}
				if (num != -1)
				{
					Ping.canSendPrivileged = (cap_user_data_t.effective & 8192U) > 0U;
				}
			}
			catch
			{
				Ping.canSendPrivileged = false;
			}
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000027E8 File Offset: 0x000009E8
		void IDisposable.Dispose()
		{
		}

		/// <summary>Raises the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" />  object that contains event data.</param>
		// Token: 0x06003420 RID: 13344 RVA: 0x000C27D0 File Offset: 0x000C09D0
		protected void OnPingCompleted(PingCompletedEventArgs e)
		{
			this.user_async_state = null;
			this.worker = null;
			if (this.cts != null)
			{
				this.cts.Dispose();
				this.cts = null;
			}
			if (this.PingCompleted != null)
			{
				this.PingCompleted(this, e);
			}
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or describes the reason for the failure if no message was received.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003421 RID: 13345 RVA: 0x000C280F File Offset: 0x000C0A0F
		public PingReply Send(IPAddress address)
		{
			return this.Send(address, 4000);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation. </summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003422 RID: 13346 RVA: 0x000C281D File Offset: 0x000C0A1D
		public PingReply Send(IPAddress address, int timeout)
		{
			return this.Send(address, timeout, Ping.default_buffer);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003423 RID: 13347 RVA: 0x000C282C File Offset: 0x000C0A2C
		public PingReply Send(IPAddress address, int timeout, byte[] buffer)
		{
			return this.Send(address, timeout, buffer, new PingOptions());
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003424 RID: 13348 RVA: 0x000C283C File Offset: 0x000C0A3C
		public PingReply Send(string hostNameOrAddress)
		{
			return this.Send(hostNameOrAddress, 4000);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003425 RID: 13349 RVA: 0x000C284A File Offset: 0x000C0A4A
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, Ping.default_buffer);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003426 RID: 13350 RVA: 0x000C2859 File Offset: 0x000C0A59
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, new PingOptions());
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is a zero length string.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003427 RID: 13351 RVA: 0x000C286C File Offset: 0x000C0A6C
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostNameOrAddress);
			return this.Send(hostAddresses[0], timeout, buffer, options);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" /> and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003428 RID: 13352 RVA: 0x000C2890 File Offset: 0x000C0A90
		public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout must be non-negative integer");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException("buffer");
			}
			if (Ping.canSendPrivileged)
			{
				return this.SendPrivileged(address, timeout, buffer, options);
			}
			return this.SendUnprivileged(address, timeout, buffer, options);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000C2900 File Offset: 0x000C0B00
		private PingReply SendPrivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			IPEndPoint ipendPoint = new IPEndPoint(address, 0);
			PingReply pingReply;
			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp))
			{
				if (options != null)
				{
					socket.DontFragment = options.DontFragment;
					socket.Ttl = (short)options.Ttl;
				}
				socket.SendTimeout = timeout;
				socket.ReceiveTimeout = timeout;
				byte[] array = new Ping.IcmpMessage(8, 0, this.identifier, 0, buffer).GetBytes();
				socket.SendBufferSize = array.Length;
				socket.SendTo(array, array.Length, SocketFlags.None, ipendPoint);
				Stopwatch stopwatch = Stopwatch.StartNew();
				array = new byte[100];
				SocketError socketError;
				long elapsedMilliseconds;
				Ping.IcmpMessage icmpMessage;
				for (;;)
				{
					EndPoint endPoint = ipendPoint;
					socketError = SocketError.Success;
					int num = socket.ReceiveFrom(array, 0, 100, SocketFlags.None, ref endPoint, out socketError);
					if (socketError != SocketError.Success)
					{
						break;
					}
					elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
					int num2 = (int)(array[0] & 15) << 2;
					int num3 = num - num2;
					if (!((IPEndPoint)endPoint).Address.Equals(ipendPoint.Address))
					{
						long num4 = (long)timeout - elapsedMilliseconds;
						if (num4 <= 0L)
						{
							goto Block_7;
						}
						socket.ReceiveTimeout = (int)num4;
					}
					else
					{
						icmpMessage = new Ping.IcmpMessage(array, num2, num3);
						if (icmpMessage.Identifier == this.identifier && icmpMessage.Type != 8)
						{
							goto IL_0190;
						}
						long num5 = (long)timeout - elapsedMilliseconds;
						if (num5 <= 0L)
						{
							goto Block_9;
						}
						socket.ReceiveTimeout = (int)num5;
					}
				}
				if (socketError == SocketError.TimedOut)
				{
					return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				}
				throw new NotSupportedException(string.Format("Unexpected socket error during ping request: {0}", socketError));
				Block_7:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				Block_9:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				IL_0190:
				pingReply = new PingReply(address, icmpMessage.Data, options, elapsedMilliseconds, icmpMessage.IPStatus);
			}
			return pingReply;
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000C2AE4 File Offset: 0x000C0CE4
		private PingReply SendUnprivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Process process = new Process();
			string text = this.BuildPingArgs(address, timeout, options);
			long num = 0L;
			process.StartInfo.FileName = Ping.PingBinPath;
			process.StartInfo.Arguments = text;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			IPStatus ipstatus = IPStatus.Unknown;
			try
			{
				process.Start();
				process.StandardOutput.ReadToEnd();
				process.StandardError.ReadToEnd();
				num = stopwatch.ElapsedMilliseconds;
				if (!process.WaitForExit(timeout) || (process.HasExited && process.ExitCode == 2))
				{
					ipstatus = IPStatus.TimedOut;
				}
				else if (process.ExitCode == 0)
				{
					ipstatus = IPStatus.Success;
				}
				else if (process.ExitCode == 1)
				{
					ipstatus = IPStatus.TtlExpired;
				}
			}
			catch
			{
			}
			finally
			{
				if (!process.HasExited)
				{
					process.Kill();
				}
				process.Dispose();
			}
			return new PingReply(address, buffer, options, num, ipstatus);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x0600342B RID: 13355 RVA: 0x000C2C04 File Offset: 0x000C0E04
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(address, 4000, Ping.default_buffer, new PingOptions(), userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.Net.IPAddress,System.Int32,System.Byte[],System.Object)" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x0600342C RID: 13356 RVA: 0x000C2C1E File Offset: 0x000C0E1E
		public void SendAsync(IPAddress address, int timeout, object userToken)
		{
			this.SendAsync(address, 4000, Ping.default_buffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to the <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600342D RID: 13357 RVA: 0x000C2C32 File Offset: 0x000C0E32
		public void SendAsync(IPAddress address, object userToken)
		{
			this.SendAsync(address, 4000, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x0600342E RID: 13358 RVA: 0x000C2C41 File Offset: 0x000C0E41
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, buffer, new PingOptions(), userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="buffer">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x0600342F RID: 13359 RVA: 0x000C2C54 File Offset: 0x000C0E54
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			IPAddress ipaddress = Dns.GetHostEntry(hostNameOrAddress).AddressList[0];
			this.SendAsync(ipaddress, timeout, buffer, options, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003430 RID: 13360 RVA: 0x000C2C7C File Offset: 0x000C0E7C
		public void SendAsync(string hostNameOrAddress, int timeout, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, Ping.default_buffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.String,System.Object)" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003431 RID: 13361 RVA: 0x000C2C8C File Offset: 0x000C0E8C
		public void SendAsync(string hostNameOrAddress, object userToken)
		{
			this.SendAsync(hostNameOrAddress, 4000, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message. </param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06003432 RID: 13362 RVA: 0x000C2C9C File Offset: 0x000C0E9C
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (this.worker != null || this.cts != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new BackgroundWorker();
			this.worker.DoWork += delegate(object o, DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					ea.Result = this.Send(address, timeout, buffer, options);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, RunWorkerCompletedEventArgs ea)
			{
				this.OnPingCompleted(new PingCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state, ea.Result as PingReply));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Cancels all pending asynchronous requests to send an Internet Control Message Protocol (ICMP) echo message and receives a corresponding ICMP echo reply message.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003433 RID: 13363 RVA: 0x000C2D40 File Offset: 0x000C0F40
		public void SendAsyncCancel()
		{
			if (this.cts != null)
			{
				this.cts.Cancel();
				return;
			}
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000C2D74 File Offset: 0x000C0F74
		private string BuildPingArgs(IPAddress address, int timeout, PingOptions options)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StringBuilder stringBuilder = new StringBuilder();
			uint num = Convert.ToUInt32(Math.Floor((double)(timeout + 1000) / 1000.0));
			bool isMacOS = Platform.IsMacOS;
			if (!isMacOS)
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -w {1} -t {2} -M ", 1, num, options.Ttl);
			}
			else
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -t {1} -o -m {2} ", 1, num, options.Ttl);
			}
			if (!isMacOS)
			{
				stringBuilder.Append(options.DontFragment ? "do " : "dont ");
			}
			else if (options.DontFragment)
			{
				stringBuilder.Append("-D ");
			}
			stringBuilder.Append(address.ToString());
			return stringBuilder.ToString();
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation and a buffer to use for send and receive.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65,500 bytes.</exception>
		// Token: 0x06003435 RID: 13365 RVA: 0x000C2E42 File Offset: 0x000C1042
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer)
		{
			return this.SendPingAsync(address, 4000, Ping.default_buffer, new PingOptions());
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		// Token: 0x06003436 RID: 13366 RVA: 0x000C2E5A File Offset: 0x000C105A
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout)
		{
			return this.SendPingAsync(address, 4000, Ping.default_buffer);
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06003437 RID: 13367 RVA: 0x000C2E6D File Offset: 0x000C106D
		public Task<PingReply> SendPingAsync(IPAddress address)
		{
			return this.SendPingAsync(address, 4000);
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation and a buffer to use for send and receive.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		// Token: 0x06003438 RID: 13368 RVA: 0x000C2E7B File Offset: 0x000C107B
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.SendPingAsync(hostNameOrAddress, timeout, buffer, new PingOptions());
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation, a buffer to use for send and receive, and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		// Token: 0x06003439 RID: 13369 RVA: 0x000C2E8C File Offset: 0x000C108C
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			IPAddress ipaddress = Dns.GetHostEntry(hostNameOrAddress).AddressList[0];
			return this.SendPingAsync(ipaddress, timeout, buffer, options);
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		// Token: 0x0600343A RID: 13370 RVA: 0x000C2EB2 File Offset: 0x000C10B2
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout)
		{
			return this.SendPingAsync(hostNameOrAddress, timeout, Ping.default_buffer);
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		// Token: 0x0600343B RID: 13371 RVA: 0x000C2EC1 File Offset: 0x000C10C1
		public Task<PingReply> SendPingAsync(string hostNameOrAddress)
		{
			return this.SendPingAsync(hostNameOrAddress, 4000);
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation, a buffer to use for send and receive, and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65,500 bytes.</exception>
		// Token: 0x0600343C RID: 13372 RVA: 0x000C2ED0 File Offset: 0x000C10D0
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (this.worker != null || this.cts != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.cts = new CancellationTokenSource();
			Task<PingReply> task = Task<PingReply>.Factory.StartNew(() => this.Send(address, timeout, buffer, options), this.cts.Token);
			task.ContinueWith(delegate(Task<PingReply> t)
			{
				if (t.IsCanceled)
				{
					this.OnPingCompleted(new PingCompletedEventArgs(null, true, null, null));
					return;
				}
				if (t.IsFaulted)
				{
					this.OnPingCompleted(new PingCompletedEventArgs(t.Exception, false, null, null));
					return;
				}
				this.OnPingCompleted(new PingCompletedEventArgs(null, false, null, t.Result));
			});
			return task;
		}

		// Token: 0x04002953 RID: 10579
		private const int DefaultCount = 1;

		// Token: 0x04002954 RID: 10580
		private static readonly string[] PingBinPaths = new string[] { "/bin/ping", "/sbin/ping", "/usr/sbin/ping" };

		// Token: 0x04002955 RID: 10581
		private static readonly string PingBinPath;

		// Token: 0x04002956 RID: 10582
		private static bool canSendPrivileged;

		// Token: 0x04002957 RID: 10583
		private const int default_timeout = 4000;

		// Token: 0x04002958 RID: 10584
		private ushort identifier;

		// Token: 0x04002959 RID: 10585
		private const uint _LINUX_CAPABILITY_VERSION_1 = 429392688U;

		// Token: 0x0400295A RID: 10586
		private static readonly byte[] default_buffer = new byte[0];

		// Token: 0x0400295B RID: 10587
		private BackgroundWorker worker;

		// Token: 0x0400295C RID: 10588
		private object user_async_state;

		// Token: 0x0400295D RID: 10589
		private CancellationTokenSource cts;

		// Token: 0x02000668 RID: 1640
		private struct cap_user_header_t
		{
			// Token: 0x0400295F RID: 10591
			public uint version;

			// Token: 0x04002960 RID: 10592
			public int pid;
		}

		// Token: 0x02000669 RID: 1641
		private struct cap_user_data_t
		{
			// Token: 0x04002961 RID: 10593
			public uint effective;

			// Token: 0x04002962 RID: 10594
			public uint permitted;

			// Token: 0x04002963 RID: 10595
			public uint inheritable;
		}

		// Token: 0x0200066A RID: 1642
		private class IcmpMessage
		{
			// Token: 0x0600343D RID: 13373 RVA: 0x000C2F61 File Offset: 0x000C1161
			public IcmpMessage(byte[] bytes, int offset, int size)
			{
				this.bytes = new byte[size];
				Buffer.BlockCopy(bytes, offset, this.bytes, 0, size);
			}

			// Token: 0x0600343E RID: 13374 RVA: 0x000C2F84 File Offset: 0x000C1184
			public IcmpMessage(byte type, byte code, ushort identifier, ushort sequence, byte[] data)
			{
				this.bytes = new byte[data.Length + 8];
				this.bytes[0] = type;
				this.bytes[1] = code;
				this.bytes[4] = (byte)(identifier & 255);
				this.bytes[5] = (byte)(identifier >> 8);
				this.bytes[6] = (byte)(sequence & 255);
				this.bytes[7] = (byte)(sequence >> 8);
				Buffer.BlockCopy(data, 0, this.bytes, 8, data.Length);
				ushort num = Ping.IcmpMessage.ComputeChecksum(this.bytes);
				this.bytes[2] = (byte)(num & 255);
				this.bytes[3] = (byte)(num >> 8);
			}

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x0600343F RID: 13375 RVA: 0x000C302F File Offset: 0x000C122F
			public byte Type
			{
				get
				{
					return this.bytes[0];
				}
			}

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x06003440 RID: 13376 RVA: 0x000C3039 File Offset: 0x000C1239
			public byte Code
			{
				get
				{
					return this.bytes[1];
				}
			}

			// Token: 0x17000C42 RID: 3138
			// (get) Token: 0x06003441 RID: 13377 RVA: 0x000C3043 File Offset: 0x000C1243
			public ushort Identifier
			{
				get
				{
					return (ushort)((int)this.bytes[4] + ((int)this.bytes[5] << 8));
				}
			}

			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x06003442 RID: 13378 RVA: 0x000C3059 File Offset: 0x000C1259
			public ushort Sequence
			{
				get
				{
					return (ushort)((int)this.bytes[6] + ((int)this.bytes[7] << 8));
				}
			}

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x06003443 RID: 13379 RVA: 0x000C3070 File Offset: 0x000C1270
			public byte[] Data
			{
				get
				{
					byte[] array = new byte[this.bytes.Length - 8];
					Buffer.BlockCopy(this.bytes, 8, array, 0, array.Length);
					return array;
				}
			}

			// Token: 0x06003444 RID: 13380 RVA: 0x000C309F File Offset: 0x000C129F
			public byte[] GetBytes()
			{
				return this.bytes;
			}

			// Token: 0x06003445 RID: 13381 RVA: 0x000C30A8 File Offset: 0x000C12A8
			private static ushort ComputeChecksum(byte[] data)
			{
				uint num = 0U;
				for (int i = 0; i < data.Length; i += 2)
				{
					ushort num2 = (ushort)((i + 1 < data.Length) ? data[i + 1] : 0);
					num2 = (ushort)(num2 << 8);
					num2 += (ushort)data[i];
					num += (uint)num2;
				}
				num = (num >> 16) + (num & 65535U);
				return (ushort)(~(ushort)num);
			}

			// Token: 0x17000C45 RID: 3141
			// (get) Token: 0x06003446 RID: 13382 RVA: 0x000C30F8 File Offset: 0x000C12F8
			public IPStatus IPStatus
			{
				get
				{
					byte b = this.Type;
					switch (b)
					{
					case 0:
						return IPStatus.Success;
					case 1:
					case 2:
						break;
					case 3:
						switch (this.Code)
						{
						case 0:
							return IPStatus.DestinationNetworkUnreachable;
						case 1:
							return IPStatus.DestinationHostUnreachable;
						case 2:
							return IPStatus.DestinationProtocolUnreachable;
						case 3:
							return IPStatus.DestinationPortUnreachable;
						case 4:
							return IPStatus.BadOption;
						case 5:
							return IPStatus.BadRoute;
						}
						break;
					case 4:
						return IPStatus.SourceQuench;
					default:
						switch (b)
						{
						case 8:
							return IPStatus.Success;
						case 11:
							b = this.Code;
							if (b == 0)
							{
								return IPStatus.TimeExceeded;
							}
							if (b == 1)
							{
								return IPStatus.TtlReassemblyTimeExceeded;
							}
							break;
						case 12:
							return IPStatus.ParameterProblem;
						}
						break;
					}
					return IPStatus.Unknown;
				}
			}

			// Token: 0x04002964 RID: 10596
			private byte[] bytes;
		}
	}
}
