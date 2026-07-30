using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the scope of memory protection to be applied by the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
	// Token: 0x02000017 RID: 23
	public enum MemoryProtectionScope
	{
		/// <summary>Only code running in the same process as the code that called the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method can unprotect memory.</summary>
		// Token: 0x040000A0 RID: 160
		SameProcess,
		/// <summary>All code in any process can unprotect memory that was protected using the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
		// Token: 0x040000A1 RID: 161
		CrossProcess,
		/// <summary>Only code running in the same user context as the code that called the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method can unprotect memory.</summary>
		// Token: 0x040000A2 RID: 162
		SameLogon
	}
}
