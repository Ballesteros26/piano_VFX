using System;

namespace System.Net.Mail
{
	// Token: 0x0200058B RID: 1419
	internal class CCredentialsByHost : ICredentialsByHost
	{
		// Token: 0x06002C3B RID: 11323 RVA: 0x000AEECD File Offset: 0x000AD0CD
		public CCredentialsByHost(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000AEEE3 File Offset: 0x000AD0E3
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return new NetworkCredential(this.userName, this.password);
		}

		// Token: 0x040024B5 RID: 9397
		private string userName;

		// Token: 0x040024B6 RID: 9398
		private string password;
	}
}
