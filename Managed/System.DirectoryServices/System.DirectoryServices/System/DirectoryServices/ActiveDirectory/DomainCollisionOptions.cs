using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies additional information about a forest trust collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> property value is <see cref="F:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType.Domain" />.</summary>
	// Token: 0x02000054 RID: 84
	[Flags]
	public enum DomainCollisionOptions
	{
		/// <summary>No action has occurred.</summary>
		// Token: 0x040000E7 RID: 231
		None = 0,
		/// <summary>The forest trust SID was disabled by administrative action.</summary>
		// Token: 0x040000E8 RID: 232
		SidDisabledByAdmin = 1,
		/// <summary>The forest trust SID was disabled due to a conflict with an existing SID.</summary>
		// Token: 0x040000E9 RID: 233
		SidDisabledByConflict = 2,
		/// <summary>The forest trust NetBIOS record was disabled by administrative action.</summary>
		// Token: 0x040000EA RID: 234
		NetBiosNameDisabledByAdmin = 4,
		/// <summary>The forest trust NetBIOS record was disabled due to a conflict with an existing NetBIOS record.</summary>
		// Token: 0x040000EB RID: 235
		NetBiosNameDisabledByConflict = 8
	}
}
