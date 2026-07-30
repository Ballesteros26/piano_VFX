using System;
using System.Collections;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000035 RID: 53
	internal class ClientSessionCache
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public static void Add(string host, byte[] id)
		{
			object obj = ClientSessionCache.locker;
			lock (obj)
			{
				string text = BitConverter.ToString(id);
				ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[text];
				if (clientSessionInfo == null)
				{
					ClientSessionCache.cache.Add(text, new ClientSessionInfo(host, id));
				}
				else if (clientSessionInfo.HostName == host)
				{
					clientSessionInfo.KeepAlive();
				}
				else
				{
					clientSessionInfo.Dispose();
					ClientSessionCache.cache.Remove(text);
					ClientSessionCache.cache.Add(text, new ClientSessionInfo(host, id));
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000E648 File Offset: 0x0000C848
		public static byte[] FromHost(string host)
		{
			object obj = ClientSessionCache.locker;
			byte[] array;
			lock (obj)
			{
				foreach (object obj2 in ClientSessionCache.cache.Values)
				{
					ClientSessionInfo clientSessionInfo = (ClientSessionInfo)obj2;
					if (clientSessionInfo.HostName == host && clientSessionInfo.Valid)
					{
						clientSessionInfo.KeepAlive();
						return clientSessionInfo.Id;
					}
				}
				array = null;
			}
			return array;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private static ClientSessionInfo FromContext(Context context, bool checkValidity)
		{
			if (context == null)
			{
				return null;
			}
			byte[] sessionId = context.SessionId;
			if (sessionId == null || sessionId.Length == 0)
			{
				return null;
			}
			string text = BitConverter.ToString(sessionId);
			ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[text];
			if (clientSessionInfo == null)
			{
				return null;
			}
			if (context.ClientSettings.TargetHost != clientSessionInfo.HostName)
			{
				return null;
			}
			if (checkValidity && !clientSessionInfo.Valid)
			{
				clientSessionInfo.Dispose();
				ClientSessionCache.cache.Remove(text);
				return null;
			}
			return clientSessionInfo;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E76C File Offset: 0x0000C96C
		public static bool SetContextInCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool flag2;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, false);
				if (clientSessionInfo == null)
				{
					flag2 = false;
				}
				else
				{
					clientSessionInfo.GetContext(context);
					clientSessionInfo.KeepAlive();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		public static bool SetContextFromCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool flag2;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, true);
				if (clientSessionInfo == null)
				{
					flag2 = false;
				}
				else
				{
					clientSessionInfo.SetContext(context);
					clientSessionInfo.KeepAlive();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0400013C RID: 316
		private static Hashtable cache = new Hashtable();

		// Token: 0x0400013D RID: 317
		private static object locker = new object();
	}
}
