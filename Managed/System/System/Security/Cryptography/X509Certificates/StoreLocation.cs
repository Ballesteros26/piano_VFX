using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the location of the X.509 certificate store.</summary>
	// Token: 0x02000397 RID: 919
	public enum StoreLocation
	{
		/// <summary>The X.509 certificate store used by the current user.</summary>
		// Token: 0x04001902 RID: 6402
		CurrentUser = 1,
		/// <summary>The X.509 certificate store assigned to the local machine.</summary>
		// Token: 0x04001903 RID: 6403
		LocalMachine
	}
}
