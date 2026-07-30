using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000506 RID: 1286
	internal class DigestClient : IAuthenticationModule
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x00094A50 File Offset: 0x00092C50
		private static Hashtable Cache
		{
			get
			{
				object syncRoot = DigestClient.cache.SyncRoot;
				lock (syncRoot)
				{
					DigestClient.CheckExpired(DigestClient.cache.Count);
				}
				return DigestClient.cache;
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00094AA4 File Offset: 0x00092CA4
		private static void CheckExpired(int count)
		{
			if (count < 10)
			{
				return;
			}
			DateTime dateTime = DateTime.MaxValue;
			DateTime utcNow = DateTime.UtcNow;
			ArrayList arrayList = null;
			foreach (object obj in DigestClient.cache.Keys)
			{
				int num = (int)obj;
				DigestSession digestSession = (DigestSession)DigestClient.cache[num];
				if (digestSession.LastUse < dateTime && (digestSession.LastUse - utcNow).Ticks > 6000000000L)
				{
					dateTime = digestSession.LastUse;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(num);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					int num2 = (int)obj2;
					DigestClient.cache.Remove(num2);
				}
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00094BD0 File Offset: 0x00092DD0
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("digest") == -1)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			DigestSession digestSession = new DigestSession();
			if (!digestSession.Parse(challenge))
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode() ^ digestSession.Nonce.GetHashCode();
			DigestSession digestSession2 = (DigestSession)DigestClient.Cache[num];
			bool flag = digestSession2 == null;
			if (flag)
			{
				digestSession2 = digestSession;
			}
			else if (!digestSession2.Parse(challenge))
			{
				return null;
			}
			if (flag)
			{
				DigestClient.Cache.Add(num, digestSession2);
			}
			return digestSession2.Authenticate(webRequest, credentials);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00094C88 File Offset: 0x00092E88
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			if (credentials == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			if (digestSession == null)
			{
				return null;
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x00094CDC File Offset: 0x00092EDC
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002111 RID: 8465
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
	}
}
