using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Mono.Http
{
	// Token: 0x020000A4 RID: 164
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			int num = text.ToLower().IndexOf("ntlm");
			if (num == -1)
			{
				return null;
			}
			num = text.IndexOfAny(new char[] { ' ', '\t' });
			if (num != -1)
			{
				text = text.Substring(num).Trim();
			}
			else
			{
				text = null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			ConditionalWeakTable<HttpWebRequest, NtlmSession> conditionalWeakTable = NtlmClient.cache;
			Authorization authorization;
			lock (conditionalWeakTable)
			{
				authorization = NtlmClient.cache.GetValue(httpWebRequest, (HttpWebRequest x) => new NtlmSession()).Authenticate(text, webRequest, credentials);
			}
			return authorization;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009E57 File Offset: 0x00008057
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000C174 File Offset: 0x0000A374
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00004240 File Offset: 0x00002440
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000915 RID: 2325
		private static readonly ConditionalWeakTable<HttpWebRequest, NtlmSession> cache = new ConditionalWeakTable<HttpWebRequest, NtlmSession>();
	}
}
