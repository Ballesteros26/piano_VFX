using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200041F RID: 1055
	internal class CredentialHostKey
	{
		// Token: 0x06002012 RID: 8210 RVA: 0x0007D278 File Offset: 0x0007B478
		internal CredentialHostKey(string host, int port, string authenticationType)
		{
			this.Host = host;
			this.Port = port;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0007D295 File Offset: 0x0007B495
		internal bool Match(string host, int port, string authenticationType)
		{
			return host != null && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, host, StringComparison.OrdinalIgnoreCase) == 0 && port == this.Port;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0007D2D0 File Offset: 0x0007B4D0
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.Host.ToUpperInvariant().GetHashCode() + this.Port.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0007D328 File Offset: 0x0007B528
		public override bool Equals(object comparand)
		{
			CredentialHostKey credentialHostKey = comparand as CredentialHostKey;
			return comparand != null && (string.Compare(this.AuthenticationType, credentialHostKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, credentialHostKey.Host, StringComparison.OrdinalIgnoreCase) == 0) && this.Port == credentialHostKey.Port;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0007D37C File Offset: 0x0007B57C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Host.Length.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				this.Host,
				":",
				this.Port.ToString(NumberFormatInfo.InvariantInfo),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001BD4 RID: 7124
		internal string Host;

		// Token: 0x04001BD5 RID: 7125
		internal string AuthenticationType;

		// Token: 0x04001BD6 RID: 7126
		internal int Port;

		// Token: 0x04001BD7 RID: 7127
		private int m_HashCode;

		// Token: 0x04001BD8 RID: 7128
		private bool m_ComputedHashCode;
	}
}
