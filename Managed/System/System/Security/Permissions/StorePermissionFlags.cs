using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to X.509 certificate stores.</summary>
	// Token: 0x02000379 RID: 889
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		/// <summary>Permission is not given to perform any certificate or store operations.</summary>
		// Token: 0x0400189B RID: 6299
		NoFlags = 0,
		/// <summary>The ability to create a new store.</summary>
		// Token: 0x0400189C RID: 6300
		CreateStore = 1,
		/// <summary>The ability to delete a store.</summary>
		// Token: 0x0400189D RID: 6301
		DeleteStore = 2,
		/// <summary>The ability to enumerate the stores on a computer.</summary>
		// Token: 0x0400189E RID: 6302
		EnumerateStores = 4,
		/// <summary>The ability to open a store.</summary>
		// Token: 0x0400189F RID: 6303
		OpenStore = 16,
		/// <summary>The ability to add a certificate to a store.</summary>
		// Token: 0x040018A0 RID: 6304
		AddToStore = 32,
		/// <summary>The ability to remove a certificate from a store.</summary>
		// Token: 0x040018A1 RID: 6305
		RemoveFromStore = 64,
		/// <summary>The ability to enumerate the certificates in a store.</summary>
		// Token: 0x040018A2 RID: 6306
		EnumerateCertificates = 128,
		/// <summary>The ability to perform all certificate and store operations.</summary>
		// Token: 0x040018A3 RID: 6307
		AllFlags = 247
	}
}
