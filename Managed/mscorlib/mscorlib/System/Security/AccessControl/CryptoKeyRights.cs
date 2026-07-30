using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the cryptographic key operation for which an authorization rule controls access or auditing.</summary>
	// Token: 0x020005E0 RID: 1504
	[Flags]
	public enum CryptoKeyRights
	{
		/// <summary>Read the key data.</summary>
		// Token: 0x04002187 RID: 8583
		ReadData = 1,
		/// <summary>Write key data.</summary>
		// Token: 0x04002188 RID: 8584
		WriteData = 2,
		/// <summary>Read extended attributes of the key.</summary>
		// Token: 0x04002189 RID: 8585
		ReadExtendedAttributes = 8,
		/// <summary>Write extended attributes of the key.</summary>
		// Token: 0x0400218A RID: 8586
		WriteExtendedAttributes = 16,
		/// <summary>Read attributes of the key.</summary>
		// Token: 0x0400218B RID: 8587
		ReadAttributes = 128,
		/// <summary>Write attributes of the key.</summary>
		// Token: 0x0400218C RID: 8588
		WriteAttributes = 256,
		/// <summary>Delete the key.</summary>
		// Token: 0x0400218D RID: 8589
		Delete = 65536,
		/// <summary>Read permissions for the key.</summary>
		// Token: 0x0400218E RID: 8590
		ReadPermissions = 131072,
		/// <summary>Change permissions for the key.</summary>
		// Token: 0x0400218F RID: 8591
		ChangePermissions = 262144,
		/// <summary>Take ownership of the key.</summary>
		// Token: 0x04002190 RID: 8592
		TakeOwnership = 524288,
		/// <summary>Use the key for synchronization.</summary>
		// Token: 0x04002191 RID: 8593
		Synchronize = 1048576,
		/// <summary>Full control of the key.</summary>
		// Token: 0x04002192 RID: 8594
		FullControl = 2032027,
		/// <summary>A combination of <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericRead" /> and <see cref="F:System.Security.AccessControl.CryptoKeyRights.GenericWrite" />.</summary>
		// Token: 0x04002193 RID: 8595
		GenericAll = 268435456,
		/// <summary>Not used.</summary>
		// Token: 0x04002194 RID: 8596
		GenericExecute = 536870912,
		/// <summary>Write the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04002195 RID: 8597
		GenericWrite = 1073741824,
		/// <summary>Read the key data, extended attributes of the key, attributes of the key, and permissions for the key.</summary>
		// Token: 0x04002196 RID: 8598
		GenericRead = -2147483648
	}
}
