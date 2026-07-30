using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the access permissions for encrypting data and memory.</summary>
	// Token: 0x02000011 RID: 17
	[Flags]
	[Serializable]
	public enum DataProtectionPermissionFlags
	{
		/// <summary>No protection abilities.</summary>
		// Token: 0x04000092 RID: 146
		NoFlags = 0,
		/// <summary>The ability to encrypt data.</summary>
		// Token: 0x04000093 RID: 147
		ProtectData = 1,
		/// <summary>The ability to unencrypt data.</summary>
		// Token: 0x04000094 RID: 148
		UnprotectData = 2,
		/// <summary>The ability to encrypt memory.</summary>
		// Token: 0x04000095 RID: 149
		ProtectMemory = 4,
		/// <summary>The ability to unencrypt memory.</summary>
		// Token: 0x04000096 RID: 150
		UnprotectMemory = 8,
		/// <summary>The ability to encrypt data, encrypt memory, unencrypt data, and unencrypt memory.</summary>
		// Token: 0x04000097 RID: 151
		AllFlags = 15
	}
}
