using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x020000A1 RID: 161
	internal sealed class SimpleResolver : IDisposable
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000B31C File Offset: 0x0000951C
		public SimpleResolver()
		{
			this.queries = new Dictionary<int, SimpleResolverEventArgs>();
			this.receive_cb = new AsyncCallback(this.OnReceive);
			this.timeout_cb = new TimerCallback(this.OnTimeout);
			this.InitFromSystem();
			this.InitSocket();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B36A File Offset: 0x0000956A
		void IDisposable.Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.client != null)
				{
					this.client.Close();
					this.client = null;
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B395 File Offset: 0x00009595
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private void GetLocalHost(SimpleResolverEventArgs args)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			iphostEntry.HostName = "localhost";
			iphostEntry.AddressList = new IPAddress[] { IPAddress.Loopback };
			iphostEntry.Aliases = SimpleResolver.EmptyStrings;
			args.ResolverError = ResolverError.NoError;
			args.HostEntry = iphostEntry;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B3EC File Offset: 0x000095EC
		public bool GetHostAddressesAsync(SimpleResolverEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.HostName == null)
			{
				throw new ArgumentNullException("args.HostName is null");
			}
			if (args.HostName.Length > 255)
			{
				throw new ArgumentException("args.HostName is too long");
			}
			args.Reset(ResolverAsyncOperation.GetHostAddresses);
			string hostName = args.HostName;
			if (hostName == "")
			{
				this.GetLocalHost(args);
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				args.HostEntry = new IPHostEntry
				{
					HostName = hostName,
					Aliases = SimpleResolver.EmptyStrings,
					AddressList = new IPAddress[] { ipaddress }
				};
				return false;
			}
			this.SendAQuery(args, true);
			return true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public bool GetHostEntryAsync(SimpleResolverEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.HostName == null)
			{
				throw new ArgumentNullException("args.HostName is null");
			}
			if (args.HostName.Length > 255)
			{
				throw new ArgumentException("args.HostName is too long");
			}
			args.Reset(ResolverAsyncOperation.GetHostEntry);
			string hostName = args.HostName;
			if (hostName == "")
			{
				this.GetLocalHost(args);
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				args.HostEntry = new IPHostEntry
				{
					HostName = hostName,
					Aliases = SimpleResolver.EmptyStrings,
					AddressList = new IPAddress[] { ipaddress }
				};
				args.PTRAddress = ipaddress;
				this.SendPTRQuery(args, true);
				return true;
			}
			this.SendAQuery(args, true);
			return true;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B564 File Offset: 0x00009764
		private bool AddQuery(DnsQuery query, SimpleResolverEventArgs args)
		{
			Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
			lock (dictionary)
			{
				if (this.queries.ContainsKey((int)query.Header.ID))
				{
					return false;
				}
				this.queries[(int)query.Header.ID] = args;
			}
			return true;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B5D4 File Offset: 0x000097D4
		private static DnsQuery GetQuery(string host, DnsQType q, DnsQClass c)
		{
			return new DnsQuery(host, q, c);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B5DE File Offset: 0x000097DE
		private void SendAQuery(SimpleResolverEventArgs args, bool add_it)
		{
			this.SendAQuery(args, args.HostName, add_it);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B5F0 File Offset: 0x000097F0
		private void SendAQuery(SimpleResolverEventArgs args, string host, bool add_it)
		{
			DnsQuery query = SimpleResolver.GetQuery(host, DnsQType.A, DnsQClass.Internet);
			this.SendQuery(args, query, add_it);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B610 File Offset: 0x00009810
		private static string GetPTRName(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder(28);
			for (int i = addressBytes.Length - 1; i >= 0; i--)
			{
				stringBuilder.AppendFormat("{0}.", addressBytes[i]);
			}
			stringBuilder.Append("in-addr.arpa");
			return stringBuilder.ToString();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B664 File Offset: 0x00009864
		private void SendPTRQuery(SimpleResolverEventArgs args, bool add_it)
		{
			DnsQuery query = SimpleResolver.GetQuery(SimpleResolver.GetPTRName(args.PTRAddress), DnsQType.PTR, DnsQClass.Internet);
			this.SendQuery(args, query, add_it);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B690 File Offset: 0x00009890
		private void SendQuery(SimpleResolverEventArgs args, DnsQuery query, bool add_it)
		{
			int num = 0;
			if (add_it)
			{
				for (;;)
				{
					query.Header.ID = (ushort)new Random().Next(1, 65534);
					if (num > 500)
					{
						break;
					}
					if (this.AddQuery(query, args))
					{
						goto Block_2;
					}
				}
				throw new InvalidOperationException("Too many pending queries (or really bad luck)");
				Block_2:
				args.QueryID = query.Header.ID;
			}
			else
			{
				query.Header.ID = args.QueryID;
			}
			if (args.Timer == null)
			{
				args.Timer = new Timer(this.timeout_cb, args, 5000, -1);
			}
			else
			{
				args.Timer.Change(5000, -1);
			}
			this.client.BeginSend(query.Packet, 0, query.Length, SocketFlags.None, null, null);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B74F File Offset: 0x0000994F
		private byte[] GetFreshBuffer()
		{
			return new byte[512];
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000027E8 File Offset: 0x000009E8
		private void FreeBuffer(byte[] buffer)
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B75C File Offset: 0x0000995C
		private void InitSocket()
		{
			this.client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.client.Blocking = true;
			this.client.Bind(new IPEndPoint(IPAddress.Any, 0));
			this.client.Connect(this.endpoints[0]);
			this.BeginReceive();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B7B4 File Offset: 0x000099B4
		private void BeginReceive()
		{
			byte[] freshBuffer = this.GetFreshBuffer();
			this.client.BeginReceive(freshBuffer, 0, freshBuffer.Length, SocketFlags.None, this.receive_cb, freshBuffer);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B7E4 File Offset: 0x000099E4
		private void OnTimeout(object obj)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = (SimpleResolverEventArgs)obj;
			Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
			lock (dictionary)
			{
				SimpleResolverEventArgs simpleResolverEventArgs2;
				if (this.queries.TryGetValue((int)simpleResolverEventArgs.QueryID, out simpleResolverEventArgs2))
				{
					if (simpleResolverEventArgs != simpleResolverEventArgs2)
					{
						throw new Exception("Should not happen: args != args2");
					}
					SimpleResolverEventArgs simpleResolverEventArgs3 = simpleResolverEventArgs;
					simpleResolverEventArgs3.Retries += 1;
					if (simpleResolverEventArgs.Retries > 1)
					{
						simpleResolverEventArgs.ResolverError = ResolverError.Timeout;
						simpleResolverEventArgs.OnCompleted(this);
					}
					else
					{
						this.SendAQuery(simpleResolverEventArgs, false);
					}
				}
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B87C File Offset: 0x00009A7C
		private void OnReceive(IAsyncResult ares)
		{
			if (this.disposed)
			{
				return;
			}
			int num = 0;
			EndPoint remoteEndPoint = this.client.RemoteEndPoint;
			try
			{
				num = this.client.EndReceive(ares);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex);
			}
			this.BeginReceive();
			byte[] array = (byte[])ares.AsyncState;
			if (num > 12)
			{
				DnsResponse dnsResponse = new DnsResponse(array, num);
				int id = (int)dnsResponse.Header.ID;
				SimpleResolverEventArgs simpleResolverEventArgs = null;
				Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
				lock (dictionary)
				{
					if (this.queries.TryGetValue(id, out simpleResolverEventArgs))
					{
						this.queries.Remove(id);
					}
				}
				if (simpleResolverEventArgs != null)
				{
					Timer timer = simpleResolverEventArgs.Timer;
					if (timer != null)
					{
						timer.Change(-1, -1);
					}
					try
					{
						this.ProcessResponse(simpleResolverEventArgs, dnsResponse, remoteEndPoint);
					}
					catch (Exception ex2)
					{
						simpleResolverEventArgs.ResolverError = (ResolverError)(-1);
						simpleResolverEventArgs.ErrorMessage = ex2.Message;
					}
					IPHostEntry hostEntry = simpleResolverEventArgs.HostEntry;
					if (simpleResolverEventArgs.ResolverError != ResolverError.NoError && simpleResolverEventArgs.PTRAddress != null && hostEntry != null && hostEntry.HostName != null)
					{
						simpleResolverEventArgs.PTRAddress = null;
						this.SendAQuery(simpleResolverEventArgs, hostEntry.HostName, true);
						simpleResolverEventArgs.Timer.Change(5000, -1);
					}
					else
					{
						simpleResolverEventArgs.OnCompleted(this);
					}
				}
			}
			this.FreeBuffer(array);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BA04 File Offset: 0x00009C04
		private void ProcessResponse(SimpleResolverEventArgs args, DnsResponse response, EndPoint server_ep)
		{
			DnsRCode rcode = response.Header.RCode;
			if (rcode != DnsRCode.NoError)
			{
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = (ResolverError)rcode;
				return;
			}
			else
			{
				if (((IPEndPoint)server_ep).Port != 53)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "Port";
					return;
				}
				DnsHeader header = response.Header;
				if (!header.IsQuery)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "IsQuery";
					return;
				}
				if (header.QuestionCount > 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount";
					return;
				}
				ReadOnlyCollection<DnsQuestion> questions = response.GetQuestions();
				if (questions.Count != 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount 2";
					return;
				}
				DnsQuestion dnsQuestion = questions[0];
				DnsQType type = dnsQuestion.Type;
				if (type != DnsQType.A && type != DnsQType.AAAA && type != DnsQType.PTR)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QType " + dnsQuestion.Type;
					return;
				}
				if (dnsQuestion.Class != DnsQClass.Internet)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QClass " + dnsQuestion.Class;
					return;
				}
				ReadOnlyCollection<DnsResourceRecord> answers = response.GetAnswers();
				if (answers.Count != 0)
				{
					List<string> list = null;
					List<IPAddress> list2 = null;
					foreach (DnsResourceRecord dnsResourceRecord in answers)
					{
						if (dnsResourceRecord.Class == DnsClass.Internet)
						{
							if (dnsResourceRecord.Type == DnsType.A || dnsResourceRecord.Type == DnsType.AAAA)
							{
								if (list2 == null)
								{
									list2 = new List<IPAddress>();
								}
								list2.Add(((DnsResourceRecordIPAddress)dnsResourceRecord).Address);
							}
							else if (dnsResourceRecord.Type == DnsType.CNAME)
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(((DnsResourceRecordCName)dnsResourceRecord).CName);
							}
							else if (dnsResourceRecord.Type == DnsType.PTR)
							{
								args.HostEntry.HostName = ((DnsResourceRecordPTR)dnsResourceRecord).DName;
								args.HostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
								args.HostEntry.AddressList = SimpleResolver.EmptyAddresses;
								return;
							}
						}
					}
					IPHostEntry iphostEntry = args.HostEntry ?? new IPHostEntry();
					if (iphostEntry.HostName == null && list != null && list.Count > 0)
					{
						iphostEntry.HostName = list[0];
						list.RemoveAt(0);
					}
					iphostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
					iphostEntry.AddressList = ((list2 == null) ? SimpleResolver.EmptyAddresses : list2.ToArray());
					args.HostEntry = iphostEntry;
					if ((dnsQuestion.Type == DnsQType.A || dnsQuestion.Type == DnsQType.AAAA) && iphostEntry.AddressList == SimpleResolver.EmptyAddresses)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No addresses in response";
						return;
					}
					if (dnsQuestion.Type == DnsQType.PTR && iphostEntry.HostName == null)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No PTR in response";
					}
					return;
				}
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = ResolverError.NameError;
				args.ErrorMessage = "NoAnswers";
				return;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BD2C File Offset: 0x00009F2C
		private void InitFromSystem()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (NetworkInterfaceType.Loopback != networkInterface.NetworkInterfaceType)
				{
					foreach (IPAddress ipaddress in networkInterface.GetIPProperties().DnsAddresses)
					{
						if (AddressFamily.InterNetworkV6 != ipaddress.AddressFamily)
						{
							IPEndPoint ipendPoint = new IPEndPoint(ipaddress, 53);
							if (!list.Contains(ipendPoint))
							{
								list.Add(ipendPoint);
							}
						}
					}
				}
			}
			this.endpoints = list.ToArray();
		}

		// Token: 0x04000901 RID: 2305
		private static string[] EmptyStrings = new string[0];

		// Token: 0x04000902 RID: 2306
		private static IPAddress[] EmptyAddresses = new IPAddress[0];

		// Token: 0x04000903 RID: 2307
		private IPEndPoint[] endpoints;

		// Token: 0x04000904 RID: 2308
		private Socket client;

		// Token: 0x04000905 RID: 2309
		private Dictionary<int, SimpleResolverEventArgs> queries;

		// Token: 0x04000906 RID: 2310
		private AsyncCallback receive_cb;

		// Token: 0x04000907 RID: 2311
		private TimerCallback timeout_cb;

		// Token: 0x04000908 RID: 2312
		private bool disposed;
	}
}
