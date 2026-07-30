using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted use of the <see cref="N:System.Reflection" /> and <see cref="N:System.Reflection.Emit" /> namespaces.</summary>
	// Token: 0x020005AB RID: 1451
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ReflectionPermissionFlag
	{
		/// <summary>Enumeration of types and members is allowed. Invocation operations are allowed on visible types and members.</summary>
		// Token: 0x040020B4 RID: 8372
		NoFlags = 0,
		/// <summary>This flag is obsolete. No flags are necessary to enumerate types and members and to examine their metadata. Use <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.NoFlags" /> instead.</summary>
		// Token: 0x040020B5 RID: 8373
		[Obsolete("not used anymore")]
		TypeInformation = 1,
		/// <summary>Invocation operations on all members are allowed, regardless of grant set. If this flag is not set, invocation operations are allowed only on visible members.</summary>
		// Token: 0x040020B6 RID: 8374
		MemberAccess = 2,
		/// <summary>Emitting debug symbols is allowed. Beginning with the .NET Framework 2.0 Service Pack 1, this flag is no longer required to emit code.</summary>
		// Token: 0x040020B7 RID: 8375
		[Obsolete]
		ReflectionEmit = 4,
		/// <summary>TypeInformation, MemberAccess, and ReflectionEmit are set. <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.AllFlags" /> does not include <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.RestrictedMemberAccess" />.</summary>
		// Token: 0x040020B8 RID: 8376
		[Obsolete]
		AllFlags = 7,
		/// <summary>Restricted member access is provided for partially trusted code. Partially trusted code can access nonpublic types and members, but only if the grant set of the partially trusted code includes all permissions in the grant set of the assembly that contains the nonpublic types and members being accessed. This flag is new in the .NET Framework 2.0 SP1.</summary>
		// Token: 0x040020B9 RID: 8377
		[ComVisible(false)]
		RestrictedMemberAccess = 8
	}
}
