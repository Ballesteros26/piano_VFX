using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000008 RID: 8
	internal sealed class Connection
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000025 RID: 37 RVA: 0x000024AC File Offset: 0x000006AC
		// (remove) Token: 0x06000026 RID: 38 RVA: 0x000024E4 File Offset: 0x000006E4
		public event CertificateValidationCallback OnCertificateValidation;

		// Token: 0x06000027 RID: 39 RVA: 0x0000251C File Offset: 0x0000071C
		private static string GetProblemMessage(Connection.CertificateProblem Problem)
		{
			string text = "";
			string text2 = Enum.GetName(typeof(Connection.CertificateProblem), Problem);
			if (text2 != null)
			{
				text += text2;
			}
			else
			{
				text = "Unknown Certificate Problem";
			}
			return text;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000255C File Offset: 0x0000075C
		private void InitBlock()
		{
			this.writeSemaphore = new object();
			this.encoder = new LBEREncoder();
			this.decoder = new LBERDecoder();
			this.stopReaderMessageID = -99;
			this.messages = new MessageVector(5, 5);
			this.unsolicitedListeners = new ArrayList(3);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025AB File Offset: 0x000007AB
		internal bool Cloned
		{
			get
			{
				return this.cloneCount > 0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025B6 File Offset: 0x000007B6
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025BE File Offset: 0x000007BE
		internal bool Ssl
		{
			get
			{
				return this.ssl;
			}
			set
			{
				this.ssl = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025C7 File Offset: 0x000007C7
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025CF File Offset: 0x000007CF
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025D7 File Offset: 0x000007D7
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000025DF File Offset: 0x000007DF
		internal int BindSemId
		{
			get
			{
				return this.bindSemaphoreId;
			}
			set
			{
				this.bindSemaphoreId = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025E8 File Offset: 0x000007E8
		internal bool BindSemIdClear
		{
			get
			{
				return this.bindSemaphoreId == 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025F5 File Offset: 0x000007F5
		internal bool Bound
		{
			get
			{
				return this.bindProperties != null && !this.bindProperties.Anonymous;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000260F File Offset: 0x0000080F
		internal bool Connected
		{
			get
			{
				return this.in_Renamed != null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000261A File Offset: 0x0000081A
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002622 File Offset: 0x00000822
		internal BindProperties BindProperties
		{
			get
			{
				return this.bindProperties;
			}
			set
			{
				this.bindProperties = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000262B File Offset: 0x0000082B
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002633 File Offset: 0x00000833
		internal ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
			set
			{
				this.activeReferral = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000263C File Offset: 0x0000083C
		internal string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002644 File Offset: 0x00000844
		internal Connection()
		{
			this.InitBlock();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002676 File Offset: 0x00000876
		internal object copy()
		{
			Connection connection = new Connection();
			connection.host = this.host;
			connection.port = this.port;
			Connection.protocol = Connection.protocol;
			return connection;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000269F File Offset: 0x0000089F
		internal int acquireWriteSemaphore()
		{
			return this.acquireWriteSemaphore(0);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026A8 File Offset: 0x000008A8
		internal int acquireWriteSemaphore(int msgId)
		{
			int num = msgId;
			object obj = this.writeSemaphore;
			lock (obj)
			{
				if (num == 0)
				{
					int num3;
					if (this.ephemeralId != -2147483648)
					{
						int num2 = this.ephemeralId - 1;
						this.ephemeralId = num2;
						num3 = num2;
					}
					else
					{
						num3 = (this.ephemeralId = -1);
					}
					this.ephemeralId = num3;
					num = this.ephemeralId;
				}
				while (this.writeSemaphoreOwner != 0)
				{
					if (this.writeSemaphoreOwner != num)
					{
						try
						{
							Monitor.Wait(this.writeSemaphore);
							continue;
						}
						catch (ThreadInterruptedException)
						{
							continue;
						}
					}
					IL_0079:
					this.writeSemaphoreCount++;
					return num;
				}
				this.writeSemaphoreOwner = num;
				goto IL_0079;
			}
			return num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002768 File Offset: 0x00000968
		internal void freeWriteSemaphore(int msgId)
		{
			object obj = this.writeSemaphore;
			lock (obj)
			{
				if (this.writeSemaphoreOwner == 0)
				{
					throw new SystemException("Connection.freeWriteSemaphore(" + msgId + "): semaphore not owned by any thread");
				}
				if (this.writeSemaphoreOwner != msgId)
				{
					throw new SystemException(string.Concat(new object[] { "Connection.freeWriteSemaphore(", msgId, "): thread does not own the semaphore, owned by ", this.writeSemaphoreOwner }));
				}
				int num = this.writeSemaphoreCount - 1;
				this.writeSemaphoreCount = num;
				if (num == 0)
				{
					this.writeSemaphoreOwner = 0;
					Monitor.Pulse(this.writeSemaphore);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000282C File Offset: 0x00000A2C
		private void waitForReader(Thread thread)
		{
			Thread thread2;
			if (this.reader != null)
			{
				thread2 = this.reader;
			}
			else
			{
				thread2 = null;
			}
			Thread thread3;
			if (thread != null)
			{
				thread3 = thread;
			}
			else
			{
				thread3 = null;
			}
			while (!object.Equals(thread2, thread3))
			{
				try
				{
					if (thread == this.deadReader)
					{
						if (thread == null)
						{
							return;
						}
						IOException ex = this.deadReaderException;
						this.deadReaderException = null;
						this.deadReader = null;
						throw new LdapException("CONNECTION_READER", 91, null, ex);
					}
					else
					{
						lock (this)
						{
							Monitor.Wait(this, TimeSpan.FromMilliseconds(5.0));
						}
					}
				}
				catch (ThreadInterruptedException)
				{
				}
				if (this.reader != null)
				{
					thread2 = this.reader;
				}
				else
				{
					thread2 = null;
				}
				if (thread != null)
				{
					thread3 = thread;
					continue;
				}
				thread3 = null;
				continue;
			}
			this.deadReaderException = null;
			this.deadReader = null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000291C File Offset: 0x00000B1C
		internal void connect(string host, int port)
		{
			this.connect(host, port, 0);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002927 File Offset: 0x00000B27
		public bool ServerCertificateValidation(X509Certificate certificate, int[] certificateErrors)
		{
			if (this.OnCertificateValidation != null)
			{
				return this.OnCertificateValidation(certificate, certificateErrors);
			}
			return this.DefaultCertificateValidationHandler(certificate, certificateErrors);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002948 File Offset: 0x00000B48
		public bool DefaultCertificateValidationHandler(X509Certificate certificate, int[] certificateErrors)
		{
			bool flag;
			if (certificateErrors != null && certificateErrors.Length != 0)
			{
				if (certificateErrors.Length == 1 && certificateErrors[0] == -2146762481)
				{
					flag = true;
				}
				else
				{
					Console.WriteLine("Detected errors in the Server Certificate:");
					for (int i = 0; i < certificateErrors.Length; i++)
					{
						this.handshakeProblemsEncountered.Add((Connection.CertificateProblem)((ulong)certificateErrors[i]));
						Console.WriteLine(certificateErrors[i]);
					}
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B0 File Offset: 0x00000BB0
		private void connect(string host, int port, int semaphoreId)
		{
			this.waitForReader(null);
			this.unsolSvrShutDnNotification = false;
			int num = this.acquireWriteSemaphore(semaphoreId);
			try
			{
				if (port == 0)
				{
					port = 389;
				}
				try
				{
					if (this.in_Renamed == null || this.out_Renamed == null)
					{
						if (this.Ssl)
						{
							this.host = host;
							this.port = port;
							this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
							IPEndPoint ipendPoint = new IPEndPoint(Dns.Resolve(host).AddressList[0], port);
							this.sock.Connect(ipendPoint);
							NetworkStream networkStream = new NetworkStream(this.sock, true);
							Assembly assembly;
							try
							{
								assembly = Assembly.LoadWithPartialName("Mono.Security");
							}
							catch (FileNotFoundException)
							{
								throw new LdapException("SSL_PROVIDER_MISSING", 114, null);
							}
							Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
							object[] array = new object[4];
							array[0] = networkStream;
							array[1] = host;
							array[2] = false;
							Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
							Enum @enum = (Enum)Activator.CreateInstance(type2);
							int num2 = (int)Enum.Parse(type2, "Ssl3");
							int num3 = (int)Enum.Parse(type2, "Tls");
							array[3] = Enum.ToObject(type2, num2 | num3);
							object obj = Activator.CreateInstance(type, array);
							PropertyInfo property = type.GetProperty("ServerCertValidationDelegate");
							property.SetValue(obj, Delegate.CreateDelegate(property.PropertyType, this, "ServerCertificateValidation"), null);
							this.in_Renamed = (Stream)obj;
							this.out_Renamed = (Stream)obj;
						}
						else
						{
							this.socket = new TcpClient(host, port);
							this.in_Renamed = this.socket.GetStream();
							this.out_Renamed = this.socket.GetStream();
						}
					}
					else
					{
						Console.WriteLine("connect input/out Stream specified");
					}
				}
				catch (SocketException ex)
				{
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[] { host, port }, 91, null, ex);
				}
				catch (IOException ex2)
				{
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[] { host, port }, 91, null, ex2);
				}
				this.host = host;
				this.port = port;
				this.startReader();
				this.clientActive = true;
			}
			finally
			{
				this.freeWriteSemaphore(num);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C54 File Offset: 0x00000E54
		internal void incrCloneCount()
		{
			lock (this)
			{
				this.cloneCount++;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C98 File Offset: 0x00000E98
		internal Connection destroyClone(bool apiCall)
		{
			Connection connection2;
			lock (this)
			{
				Connection connection = this;
				if (this.cloneCount > 0)
				{
					this.cloneCount--;
					if (apiCall)
					{
						connection = (Connection)this.copy();
					}
					else
					{
						connection = null;
					}
				}
				else if (this.in_Renamed != null)
				{
					InterThreadException ex = new InterThreadException(apiCall ? "CONNECTION_CLOSED" : "CONNECTION_FINALIZED", null, 91, null, null);
					this.shutdown("destroy clone", 0, ex);
				}
				connection2 = connection;
			}
			return connection2;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D30 File Offset: 0x00000F30
		internal void clearBindSemId()
		{
			this.bindSemaphoreId = 0;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D3C File Offset: 0x00000F3C
		internal void writeMessage(Message info)
		{
			object[][] contents = new ExceptionMessages().getContents();
			this.messages.Add(info);
			if (info.BindRequest && !this.Connected && this.host != null)
			{
				this.connect(this.host, this.port, info.MessageID);
			}
			if (this.Connected)
			{
				LdapMessage request = info.Request;
				this.writeMessage(request);
				return;
			}
			int num = 0;
			while (num < contents.Length && contents[num][0] != "CONNECTION_CLOSED")
			{
				num++;
			}
			throw new LdapException("CONNECTION_CLOSED", new object[] { this.host, this.port }, 91, (string)contents[num][1]);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DF8 File Offset: 0x00000FF8
		internal void writeMessage(LdapMessage msg)
		{
			int messageID;
			if (this.bindSemaphoreId == 0)
			{
				messageID = msg.MessageID;
			}
			else
			{
				messageID = this.bindSemaphoreId;
			}
			Stream stream = this.out_Renamed;
			this.acquireWriteSemaphore(messageID);
			try
			{
				if (stream == null)
				{
					throw new IOException("Output stream not initialized");
				}
				if (!stream.CanWrite)
				{
					return;
				}
				sbyte[] encoding = msg.Asn1Object.getEncoding(this.encoder);
				stream.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
				stream.Flush();
			}
			catch (IOException ex)
			{
				if (msg.Type == 0 && this.ssl)
				{
					string text = "Following problem(s) occurred while establishing SSL based Connection : ";
					if (this.handshakeProblemsEncountered.Count > 0)
					{
						text += Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[0]);
						for (int i = 1; i < this.handshakeProblemsEncountered.Count; i++)
						{
							text = text + ", " + Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[i]);
						}
					}
					else
					{
						text += "Unknown Certificate Problem";
					}
					throw new LdapException(text, new object[] { this.host, this.port }, 113, null, ex);
				}
				if (this.clientActive)
				{
					if (this.unsolSvrShutDnNotification)
					{
						throw new LdapException("SERVER_SHUTDOWN_REQ", new object[] { this.host, this.port }, 91, null, ex);
					}
					throw new LdapException("IO_EXCEPTION", new object[] { this.host, this.port }, 91, null, ex);
				}
			}
			finally
			{
				this.freeWriteSemaphore(messageID);
				this.handshakeProblemsEncountered.Clear();
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002FEC File Offset: 0x000011EC
		internal MessageAgent getMessageAgent(int msgId)
		{
			return this.messages.findMessageById(msgId).MessageAgent;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FFF File Offset: 0x000011FF
		internal void removeMessage(Message info)
		{
			SupportClass.VectorRemoveElement(this.messages, info);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003010 File Offset: 0x00001210
		~Connection()
		{
			this.shutdown("Finalize", 0, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003044 File Offset: 0x00001244
		private void shutdown(string reason, int semaphoreId, InterThreadException notifyUser)
		{
			Message message = null;
			if (!this.clientActive)
			{
				return;
			}
			this.clientActive = false;
			for (;;)
			{
				try
				{
					object obj = this.messages[0];
					this.messages.RemoveAt(0);
					message = (Message)obj;
				}
				catch (ArgumentOutOfRangeException)
				{
					break;
				}
				message.Abandon(null, notifyUser);
			}
			int num = this.acquireWriteSemaphore(semaphoreId);
			if (this.bindProperties != null && this.out_Renamed != null && this.out_Renamed.CanWrite && !this.bindProperties.Anonymous)
			{
				try
				{
					sbyte[] encoding = new LdapUnbindRequest(null).Asn1Object.getEncoding(this.encoder);
					this.out_Renamed.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
					this.out_Renamed.Flush();
					this.out_Renamed.Close();
				}
				catch (Exception)
				{
				}
			}
			this.bindProperties = null;
			if (this.socket != null || this.sock != null)
			{
				if (this.reader != null && reason != "reader: thread stopping")
				{
					this.reader.Abort();
				}
				try
				{
					if (this.Ssl)
					{
						try
						{
							this.sock.Shutdown(SocketShutdown.Both);
						}
						catch
						{
						}
						this.sock.Close();
					}
					else
					{
						if (this.in_Renamed != null)
						{
							this.in_Renamed.Close();
						}
						this.socket.Close();
					}
				}
				catch (Exception)
				{
				}
				this.socket = null;
				this.sock = null;
				this.in_Renamed = null;
				this.out_Renamed = null;
			}
			this.freeWriteSemaphore(num);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031E8 File Offset: 0x000013E8
		internal bool areMessagesComplete()
		{
			object[] objectArray = this.messages.ObjectArray;
			int num = objectArray.Length;
			if (this.bindSemaphoreId != 0)
			{
				return false;
			}
			if (num == 0)
			{
				return true;
			}
			for (int i = 0; i < num; i++)
			{
				if (!((Message)objectArray[i]).Complete)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003232 File Offset: 0x00001432
		internal void stopReaderOnReply(int messageID)
		{
			this.stopReaderMessageID = messageID;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000323C File Offset: 0x0000143C
		internal void startReader()
		{
			Thread thread = new Thread(new ThreadStart(new Connection.ReaderThread(this).Run));
			thread.IsBackground = true;
			thread.Start();
			this.waitForReader(thread);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003275 File Offset: 0x00001475
		internal bool TLS
		{
			get
			{
				return this.nonTLSBackup != null;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003280 File Offset: 0x00001480
		internal void startTLS()
		{
			try
			{
				this.waitForReader(null);
				this.nonTLSBackup = this.socket;
				Assembly assembly = null;
				try
				{
					assembly = Assembly.LoadFrom("Mono.Security.dll");
				}
				catch (FileNotFoundException)
				{
					throw new LdapException("SSL_PROVIDER_MISSING", 114, null);
				}
				Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
				object[] array = new object[4];
				array[0] = this.socket.GetStream();
				array[1] = this.host;
				array[2] = false;
				Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
				Enum @enum = (Enum)Activator.CreateInstance(type2);
				int num = (int)Enum.Parse(type2, "Ssl3");
				int num2 = (int)Enum.Parse(type2, "Tls");
				array[3] = Enum.ToObject(type2, num | num2);
				object obj = Activator.CreateInstance(type, array);
				EventInfo @event = type.GetEvent("ServerCertValidationDelegate");
				@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, "ServerCertificateValidation"));
				this.in_Renamed = (Stream)obj;
				this.out_Renamed = (Stream)obj;
			}
			catch (IOException ex)
			{
				this.nonTLSBackup = null;
				throw new LdapException("Could not negotiate a secure connection", 91, null, ex);
			}
			catch (Exception ex2)
			{
				this.nonTLSBackup = null;
				throw new LdapException("The host is unknown", 91, null, ex2);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000033E0 File Offset: 0x000015E0
		internal void stopTLS()
		{
			try
			{
				this.stopReaderMessageID = -98;
				this.out_Renamed.Close();
				this.in_Renamed.Close();
				this.waitForReader(null);
				this.socket = this.nonTLSBackup;
				this.in_Renamed = this.socket.GetStream();
				this.out_Renamed = this.socket.GetStream();
				this.stopReaderMessageID = -99;
			}
			catch (IOException ex)
			{
				throw new LdapException("STOPTLS_ERROR", 91, null, ex);
			}
			finally
			{
				this.nonTLSBackup = null;
				this.startReader();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003484 File Offset: 0x00001684
		internal Stream InputStream
		{
			get
			{
				return this.in_Renamed;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000348C File Offset: 0x0000168C
		internal Stream OutputStream
		{
			get
			{
				return this.out_Renamed;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003494 File Offset: 0x00001694
		internal void ReplaceStreams(Stream newIn, Stream newOut)
		{
			this.waitForReader(null);
			this.in_Renamed = newIn;
			this.out_Renamed = newOut;
			this.startReader();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034B1 File Offset: 0x000016B1
		internal void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			this.unsolicitedListeners.Add(listener);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034C0 File Offset: 0x000016C0
		internal void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			SupportClass.VectorRemoveElement(this.unsolicitedListeners, listener);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034D0 File Offset: 0x000016D0
		private void notifyAllUnsolicitedListeners(RfcLdapMessage message)
		{
			if (((LdapExtendedResponse)new LdapExtendedResponse(message)).ID.Equals("1.3.6.1.4.1.1466.20036"))
			{
				this.unsolSvrShutDnNotification = true;
			}
			int count = this.unsolicitedListeners.Count;
			for (int i = 0; i < count; i++)
			{
				LdapUnsolicitedNotificationListener ldapUnsolicitedNotificationListener = (LdapUnsolicitedNotificationListener)this.unsolicitedListeners[i];
				LdapExtendedResponse ldapExtendedResponse = new LdapExtendedResponse(message);
				new Connection.UnsolicitedListenerThread(this, ldapUnsolicitedNotificationListener, ldapExtendedResponse).Start();
			}
		}

		// Token: 0x04000038 RID: 56
		private ArrayList handshakeProblemsEncountered = new ArrayList();

		// Token: 0x04000039 RID: 57
		private object writeSemaphore;

		// Token: 0x0400003A RID: 58
		private int writeSemaphoreOwner;

		// Token: 0x0400003B RID: 59
		private int writeSemaphoreCount;

		// Token: 0x0400003C RID: 60
		private int ephemeralId = -1;

		// Token: 0x0400003D RID: 61
		private BindProperties bindProperties;

		// Token: 0x0400003E RID: 62
		private int bindSemaphoreId;

		// Token: 0x0400003F RID: 63
		private Thread reader;

		// Token: 0x04000040 RID: 64
		private Thread deadReader;

		// Token: 0x04000041 RID: 65
		private IOException deadReaderException;

		// Token: 0x04000042 RID: 66
		private LBEREncoder encoder;

		// Token: 0x04000043 RID: 67
		private LBERDecoder decoder;

		// Token: 0x04000044 RID: 68
		private Socket sock;

		// Token: 0x04000045 RID: 69
		private TcpClient socket;

		// Token: 0x04000046 RID: 70
		private TcpClient nonTLSBackup;

		// Token: 0x04000047 RID: 71
		private Stream in_Renamed;

		// Token: 0x04000048 RID: 72
		private Stream out_Renamed;

		// Token: 0x04000049 RID: 73
		private bool clientActive = true;

		// Token: 0x0400004A RID: 74
		private bool ssl;

		// Token: 0x0400004B RID: 75
		private bool unsolSvrShutDnNotification;

		// Token: 0x0400004C RID: 76
		private const int CONTINUE_READING = -99;

		// Token: 0x0400004D RID: 77
		private const int STOP_READING = -98;

		// Token: 0x0400004E RID: 78
		private int stopReaderMessageID;

		// Token: 0x0400004F RID: 79
		private MessageVector messages;

		// Token: 0x04000050 RID: 80
		private ReferralInfo activeReferral;

		// Token: 0x04000051 RID: 81
		private ArrayList unsolicitedListeners;

		// Token: 0x04000052 RID: 82
		private string host;

		// Token: 0x04000053 RID: 83
		private int port;

		// Token: 0x04000054 RID: 84
		private int cloneCount;

		// Token: 0x04000055 RID: 85
		private string name = "";

		// Token: 0x04000056 RID: 86
		private static object nameLock = new object();

		// Token: 0x04000057 RID: 87
		private static int connNum = 0;

		// Token: 0x04000058 RID: 88
		internal static string sdk = new StringBuilder("2.1.8").ToString();

		// Token: 0x04000059 RID: 89
		internal static int protocol = 3;

		// Token: 0x0400005A RID: 90
		internal static string security = "simple";

		// Token: 0x020000EF RID: 239
		public enum CertificateProblem : long
		{
			// Token: 0x040004CE RID: 1230
			CertEXPIRED = 2148204801L,
			// Token: 0x040004CF RID: 1231
			CertVALIDITYPERIODNESTING,
			// Token: 0x040004D0 RID: 1232
			CertROLE,
			// Token: 0x040004D1 RID: 1233
			CertPATHLENCONST,
			// Token: 0x040004D2 RID: 1234
			CertCRITICAL,
			// Token: 0x040004D3 RID: 1235
			CertPURPOSE,
			// Token: 0x040004D4 RID: 1236
			CertISSUERCHAINING,
			// Token: 0x040004D5 RID: 1237
			CertMALFORMED,
			// Token: 0x040004D6 RID: 1238
			CertUNTRUSTEDROOT,
			// Token: 0x040004D7 RID: 1239
			CertCHAINING,
			// Token: 0x040004D8 RID: 1240
			CertREVOKED = 2148204812L,
			// Token: 0x040004D9 RID: 1241
			CertUNTRUSTEDTESTROOT,
			// Token: 0x040004DA RID: 1242
			CertREVOCATION_FAILURE,
			// Token: 0x040004DB RID: 1243
			CertCN_NO_MATCH,
			// Token: 0x040004DC RID: 1244
			CertWRONG_USAGE,
			// Token: 0x040004DD RID: 1245
			CertUNTRUSTEDCA = 2148204818L
		}

		// Token: 0x020000F0 RID: 240
		public class ReaderThread
		{
			// Token: 0x06000617 RID: 1559 RVA: 0x000190D7 File Offset: 0x000172D7
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000618 RID: 1560 RVA: 0x000190E0 File Offset: 0x000172E0
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x000190E8 File Offset: 0x000172E8
			public ReaderThread(Connection enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x000190F8 File Offset: 0x000172F8
			public virtual void Run()
			{
				string text = "reader: thread stopping";
				InterThreadException ex = null;
				Message message = null;
				IOException ex2 = null;
				this.enclosingInstance.reader = Thread.CurrentThread;
				try
				{
					for (;;)
					{
						Stream in_Renamed = this.enclosingInstance.in_Renamed;
						if (in_Renamed == null)
						{
							goto IL_0113;
						}
						Asn1Identifier asn1Identifier = new Asn1Identifier(in_Renamed);
						int tag = asn1Identifier.Tag;
						if (asn1Identifier.Tag == 16)
						{
							Asn1Length asn1Length = new Asn1Length(in_Renamed);
							RfcLdapMessage rfcLdapMessage = new RfcLdapMessage(this.enclosingInstance.decoder, in_Renamed, asn1Length.Length);
							int messageID = rfcLdapMessage.MessageID;
							try
							{
								message = this.enclosingInstance.messages.findMessageById(messageID);
								message.putReply(rfcLdapMessage);
							}
							catch (FieldAccessException)
							{
								if (messageID == 0)
								{
									this.enclosingInstance.notifyAllUnsolicitedListeners(rfcLdapMessage);
									if (this.enclosingInstance.unsolSvrShutDnNotification)
									{
										ex = new InterThreadException("SERVER_SHUTDOWN_REQ", new object[]
										{
											this.enclosingInstance.host,
											this.enclosingInstance.port
										}, 91, null, null);
										break;
									}
								}
							}
							if (this.enclosingInstance.stopReaderMessageID == messageID || this.enclosingInstance.stopReaderMessageID == -98)
							{
								break;
							}
						}
					}
					return;
					IL_0113:;
				}
				catch (ThreadAbortException)
				{
					return;
				}
				catch (IOException ex3)
				{
					ex2 = ex3;
					if (this.enclosingInstance.stopReaderMessageID != -98 && this.enclosingInstance.clientActive)
					{
						ex = new InterThreadException("CONNECTION_WAIT", new object[]
						{
							this.enclosingInstance.host,
							this.enclosingInstance.port
						}, 91, ex3, message);
					}
					this.enclosingInstance.in_Renamed = null;
					this.enclosingInstance.out_Renamed = null;
				}
				finally
				{
					if (!this.enclosingInstance.clientActive || ex != null)
					{
						this.enclosingInstance.shutdown(text, 0, ex);
					}
					else
					{
						this.enclosingInstance.stopReaderMessageID = -99;
					}
				}
				this.enclosingInstance.deadReaderException = ex2;
				this.enclosingInstance.deadReader = this.enclosingInstance.reader;
				this.enclosingInstance.reader = null;
			}

			// Token: 0x040004DE RID: 1246
			private Connection enclosingInstance;
		}

		// Token: 0x020000F1 RID: 241
		private class UnsolicitedListenerThread : SupportClass.ThreadClass
		{
			// Token: 0x0600061B RID: 1563 RVA: 0x00019358 File Offset: 0x00017558
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x0600061C RID: 1564 RVA: 0x00019361 File Offset: 0x00017561
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x00019369 File Offset: 0x00017569
			internal UnsolicitedListenerThread(Connection enclosingInstance, LdapUnsolicitedNotificationListener l, LdapExtendedResponse m)
			{
				this.InitBlock(enclosingInstance);
				this.listenerObj = l;
				this.unsolicitedMsg = m;
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x00019386 File Offset: 0x00017586
			public override void Run()
			{
				this.listenerObj.messageReceived(this.unsolicitedMsg);
			}

			// Token: 0x040004DF RID: 1247
			private Connection enclosingInstance;

			// Token: 0x040004E0 RID: 1248
			private LdapUnsolicitedNotificationListener listenerObj;

			// Token: 0x040004E1 RID: 1249
			private LdapExtendedResponse unsolicitedMsg;
		}
	}
}
