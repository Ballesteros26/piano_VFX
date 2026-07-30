using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies additional information about a forest trust collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> property value is <see cref="F:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType.TopLevelName" />.</summary>
	// Token: 0x02000088 RID: 136
	[Flags]
	public enum TopLevelNameCollisionOptions
	{
		/// <summary>No action has occurred.</summary>
		// Token: 0x04000168 RID: 360
		None = 0,
		/// <summary>The forest trust account has been created and is disabled.</summary>
		// Token: 0x04000169 RID: 361
		NewlyCreated = 1,
		/// <summary>The forest trust account was disabled by administrative action.</summary>
		// Token: 0x0400016A RID: 362
		DisabledByAdmin = 2,
		/// <summary>The forest trust account was disabled due to a conflict with an existing forest trust account.</summary>
		// Token: 0x0400016B RID: 363
		DisabledByConflict = 4
	}
}
