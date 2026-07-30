using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000420 RID: 1056
	internal class CredentialKey
	{
		// Token: 0x06002017 RID: 8215 RVA: 0x0007D3F9 File Offset: 0x0007B5F9
		internal CredentialKey(Uri uriPrefix, string authenticationType)
		{
			this.UriPrefix = uriPrefix;
			this.UriPrefixLength = this.UriPrefix.ToString().Length;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0007D42C File Offset: 0x0007B62C
		internal bool Match(Uri uri, string authenticationType)
		{
			return !(uri == null) && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.IsPrefix(uri, this.UriPrefix);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0007D45C File Offset: 0x0007B65C
		internal bool IsPrefix(Uri uri, Uri prefixUri)
		{
			if (prefixUri.Scheme != uri.Scheme || prefixUri.Host != uri.Host || prefixUri.Port != uri.Port)
			{
				return false;
			}
			int num = prefixUri.AbsolutePath.LastIndexOf('/');
			return num <= uri.AbsolutePath.LastIndexOf('/') && string.Compare(uri.AbsolutePath, 0, prefixUri.AbsolutePath, 0, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0007D4D7 File Offset: 0x0007B6D7
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.UriPrefixLength + this.UriPrefix.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0007D518 File Offset: 0x0007B718
		public override bool Equals(object comparand)
		{
			CredentialKey credentialKey = comparand as CredentialKey;
			return comparand != null && string.Compare(this.AuthenticationType, credentialKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.UriPrefix.Equals(credentialKey.UriPrefix);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0007D558 File Offset: 0x0007B758
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.UriPrefixLength.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				ValidationHelper.ToString(this.UriPrefix),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001BD9 RID: 7129
		internal Uri UriPrefix;

		// Token: 0x04001BDA RID: 7130
		internal int UriPrefixLength = -1;

		// Token: 0x04001BDB RID: 7131
		internal string AuthenticationType;

		// Token: 0x04001BDC RID: 7132
		private int m_HashCode;

		// Token: 0x04001BDD RID: 7133
		private bool m_ComputedHashCode;
	}
}
