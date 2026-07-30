using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of name the X509 certificate contains.</summary>
	// Token: 0x0200039E RID: 926
	public enum X509NameType
	{
		/// <summary>The simple name of a subject or issuer of an X509 certificate.</summary>
		// Token: 0x04001954 RID: 6484
		SimpleName,
		/// <summary>The email address of the subject or issuer associated of an X509 certificate.</summary>
		// Token: 0x04001955 RID: 6485
		EmailName,
		/// <summary>The UPN name of the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04001956 RID: 6486
		UpnName,
		/// <summary>The DNS name associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04001957 RID: 6487
		DnsName,
		/// <summary>The DNS name associated with the alternative name of either the subject or the issuer of an X.509 certificate.  This value is equivalent to the <see cref="F:System.Security.Cryptography.X509Certificates.X509NameType.DnsName" /> value.</summary>
		// Token: 0x04001958 RID: 6488
		DnsFromAlternativeName,
		/// <summary>The URL address associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04001959 RID: 6489
		UrlName
	}
}
