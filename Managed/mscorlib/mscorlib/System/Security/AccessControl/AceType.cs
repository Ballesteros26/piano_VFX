using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the available access control entry (ACE) types.</summary>
	// Token: 0x020005CB RID: 1483
	public enum AceType : byte
	{
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002136 RID: 8502
		AccessAllowed,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002137 RID: 8503
		AccessDenied,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002138 RID: 8504
		SystemAudit,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002139 RID: 8505
		SystemAlarm,
		/// <summary>Defined but never used. Included here for completeness.</summary>
		// Token: 0x0400213A RID: 8506
		AccessAllowedCompound,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x0400213B RID: 8507
		AccessAllowedObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x0400213C RID: 8508
		AccessDeniedObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x0400213D RID: 8509
		SystemAuditObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400213E RID: 8510
		SystemAlarmObject,
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x0400213F RID: 8511
		AccessAllowedCallback,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04002140 RID: 8512
		AccessDeniedCallback,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type may contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04002141 RID: 8513
		AccessAllowedCallbackObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04002142 RID: 8514
		AccessDeniedCallbackObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04002143 RID: 8515
		SystemAuditCallback,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002144 RID: 8516
		SystemAlarmCallback,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager–specific BLOB that is not interpreted.</summary>
		// Token: 0x04002145 RID: 8517
		SystemAuditCallbackObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002146 RID: 8518
		SystemAlarmCallbackObject,
		/// <summary>Tracks the maximum defined ACE type in the enumeration.</summary>
		// Token: 0x04002147 RID: 8519
		MaxDefinedAceType = 16
	}
}
