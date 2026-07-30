using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines where and how to import the private key of an X.509 certificate.</summary>
	// Token: 0x020006A8 RID: 1704
	[Flags]
	public enum X509KeyStorageFlags
	{
		/// <summary>The default key set is used.  The user key set is usually the default. </summary>
		// Token: 0x0400261F RID: 9759
		DefaultKeySet = 0,
		/// <summary>Private keys are stored in the current user store rather than the local computer store. This occurs even if the certificate specifies that the keys should go in the local computer store. </summary>
		// Token: 0x04002620 RID: 9760
		UserKeySet = 1,
		/// <summary>Private keys are stored in the local computer store rather than the current user store. </summary>
		// Token: 0x04002621 RID: 9761
		MachineKeySet = 2,
		/// <summary>Imported keys are marked as exportable.  </summary>
		// Token: 0x04002622 RID: 9762
		Exportable = 4,
		/// <summary>Notify the user through a dialog box or other method that the key is accessed.  The Cryptographic Service Provider (CSP) in use defines the precise behavior.</summary>
		// Token: 0x04002623 RID: 9763
		UserProtected = 8,
		/// <summary>The key associated with a PFX file is persisted when importing a certificate.</summary>
		// Token: 0x04002624 RID: 9764
		PersistKeySet = 16,
		// Token: 0x04002625 RID: 9765
		EphemeralKeySet = 32
	}
}
