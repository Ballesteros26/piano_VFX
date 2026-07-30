using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies the collision type of a <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision" /> object.</summary>
	// Token: 0x0200005B RID: 91
	public enum ForestTrustCollisionType
	{
		/// <summary>The collision is between top-level domains. This collision type indicates a collision with a namespace element of another forest.</summary>
		// Token: 0x04000100 RID: 256
		TopLevelName,
		/// <summary>The collision is between domain cross-references. This collision type indicates a collision with a domain in the same forest.</summary>
		// Token: 0x04000101 RID: 257
		Domain,
		/// <summary>The collision is not a collision between top-level domains or domain cross references.</summary>
		// Token: 0x04000102 RID: 258
		Other
	}
}
