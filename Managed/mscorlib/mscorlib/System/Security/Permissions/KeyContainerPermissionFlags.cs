using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of key container access allowed.</summary>
	// Token: 0x020005A1 RID: 1441
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		/// <summary>No access to a key container.</summary>
		// Token: 0x0400208D RID: 8333
		NoFlags = 0,
		/// <summary>Create a key container.</summary>
		// Token: 0x0400208E RID: 8334
		Create = 1,
		/// <summary>Open a key container and use the public key.</summary>
		// Token: 0x0400208F RID: 8335
		Open = 2,
		/// <summary>Delete a key container.</summary>
		// Token: 0x04002090 RID: 8336
		Delete = 4,
		/// <summary>Import a key into a key container.</summary>
		// Token: 0x04002091 RID: 8337
		Import = 16,
		/// <summary>Export a key from a key container.</summary>
		// Token: 0x04002092 RID: 8338
		Export = 32,
		/// <summary>Sign a file using a key.</summary>
		// Token: 0x04002093 RID: 8339
		Sign = 256,
		/// <summary>Decrypt a key container.</summary>
		// Token: 0x04002094 RID: 8340
		Decrypt = 512,
		/// <summary>View the access control list (ACL) for a key container.</summary>
		// Token: 0x04002095 RID: 8341
		ViewAcl = 4096,
		/// <summary>Change the access control list (ACL) for a key container. </summary>
		// Token: 0x04002096 RID: 8342
		ChangeAcl = 8192,
		/// <summary>Create, decrypt, delete, and open a key container; export and import a key; sign files using a key; and view and change the access control list for a key container.</summary>
		// Token: 0x04002097 RID: 8343
		AllFlags = 13111
	}
}
