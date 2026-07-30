using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x0200053F RID: 1343
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x000A094E File Offset: 0x0009EB4E
		public NtlmClient()
		{
			this.authObject = new NtlmClient();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000A0961 File Offset: 0x0009EB61
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x00009E57 File Offset: 0x00008057
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x0000C174 File Offset: 0x0000A374
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x00004240 File Offset: 0x00002440
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400229E RID: 8862
		private IAuthenticationModule authObject;
	}
}
