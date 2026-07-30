using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies flags that modify the behavior of the cryptographic service providers (CSP).</summary>
	// Token: 0x02000650 RID: 1616
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CspProviderFlags
	{
		/// <summary>Do not specify any settings.</summary>
		// Token: 0x040023E9 RID: 9193
		NoFlags = 0,
		/// <summary>Use key information from the computer's key store.</summary>
		// Token: 0x040023EA RID: 9194
		UseMachineKeyStore = 1,
		/// <summary>Use key information from the default key container.</summary>
		// Token: 0x040023EB RID: 9195
		UseDefaultKeyContainer = 2,
		/// <summary>Use key information that cannot be exported.</summary>
		// Token: 0x040023EC RID: 9196
		UseNonExportableKey = 4,
		/// <summary>Use key information from the current key.</summary>
		// Token: 0x040023ED RID: 9197
		UseExistingKey = 8,
		/// <summary>Allow a key to be exported for archival or recovery.</summary>
		// Token: 0x040023EE RID: 9198
		UseArchivableKey = 16,
		/// <summary>Notify the user through a dialog box or another method when certain actions are attempting to use a key.  This flag is not compatible with the <see cref="F:System.Security.Cryptography.CspProviderFlags.NoPrompt" /> flag.</summary>
		// Token: 0x040023EF RID: 9199
		UseUserProtectedKey = 32,
		/// <summary>Prevent the CSP from displaying any user interface (UI) for this context.</summary>
		// Token: 0x040023F0 RID: 9200
		NoPrompt = 64,
		/// <summary>Create a temporary key that is released when the associated Rivest-Shamir-Adleman (RSA) object is closed. Do not use this flag if you want your key to be independent of the RSA object.</summary>
		// Token: 0x040023F1 RID: 9201
		CreateEphemeralKey = 128
	}
}
