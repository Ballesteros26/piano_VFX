using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the way to open the X.509 certificate store.</summary>
	// Token: 0x02000396 RID: 918
	[Flags]
	public enum OpenFlags
	{
		/// <summary>Open the X.509 certificate store for reading only.</summary>
		// Token: 0x040018FC RID: 6396
		ReadOnly = 0,
		/// <summary>Open the X.509 certificate store for both reading and writing.</summary>
		// Token: 0x040018FD RID: 6397
		ReadWrite = 1,
		/// <summary>Open the X.509 certificate store for the highest access allowed.</summary>
		// Token: 0x040018FE RID: 6398
		MaxAllowed = 2,
		/// <summary>Opens only existing stores; if no store exists, the <see cref="M:System.Security.Cryptography.X509Certificates.X509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags)" /> method will not create a new store.</summary>
		// Token: 0x040018FF RID: 6399
		OpenExistingOnly = 4,
		/// <summary>Open the X.509 certificate store and include archived certificates.</summary>
		// Token: 0x04001900 RID: 6400
		IncludeArchived = 8
	}
}
