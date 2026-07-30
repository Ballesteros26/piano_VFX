using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000511 RID: 1297
	internal sealed class EndPointManager
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x000020EB File Offset: 0x000002EB
		private EndPointManager()
		{
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00095FD8 File Offset: 0x000941D8
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = EndPointManager.ip_to_endpoints;
				lock (hashtable)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj in arrayList)
				{
					EndPointManager.RemovePrefix((string)obj, listener);
				}
				throw;
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000960B8 File Offset: 0x000942B8
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000960F8 File Offset: 0x000942F8
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00096170 File Offset: 0x00094370
		private static EndPointListener GetEPListener(string host, int port, HttpListener listener, bool secure)
		{
			IPAddress ipaddress;
			if (host == "*")
			{
				ipaddress = IPAddress.Any;
			}
			else if (!IPAddress.TryParse(host, out ipaddress))
			{
				try
				{
					IPHostEntry hostByName = Dns.GetHostByName(host);
					if (hostByName != null)
					{
						ipaddress = hostByName.AddressList[0];
					}
					else
					{
						ipaddress = IPAddress.Any;
					}
				}
				catch
				{
					ipaddress = IPAddress.Any;
				}
			}
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(ipaddress))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ipaddress];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[ipaddress] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(listener, ipaddress, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00096244 File Offset: 0x00094444
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				Hashtable hashtable2 = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable2.Remove(ep.Port);
				if (hashtable2.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000962C0 File Offset: 0x000944C0
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				foreach (string text in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(text, listener);
				}
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00096334 File Offset: 0x00094534
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00096374 File Offset: 0x00094574
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				return;
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x04002127 RID: 8487
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}
