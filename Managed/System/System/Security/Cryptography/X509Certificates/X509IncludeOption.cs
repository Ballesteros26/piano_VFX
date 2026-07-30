using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies how much of the X.509 certificate chain should be included in the X.509 data.</summary>
	// Token: 0x0200039C RID: 924
	public enum X509IncludeOption
	{
		/// <summary>No X.509 chain information is included.</summary>
		// Token: 0x04001944 RID: 6468
		None,
		/// <summary>The entire X.509 chain is included except for the root certificate.</summary>
		// Token: 0x04001945 RID: 6469
		ExcludeRoot,
		/// <summary>Only the end certificate is included in the X.509 chain information.</summary>
		// Token: 0x04001946 RID: 6470
		EndCertOnly,
		/// <summary>The entire X.509 chain is included.</summary>
		// Token: 0x04001947 RID: 6471
		WholeChain
	}
}
