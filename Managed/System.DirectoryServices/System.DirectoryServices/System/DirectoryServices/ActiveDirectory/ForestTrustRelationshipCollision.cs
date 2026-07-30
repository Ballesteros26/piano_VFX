using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Represents a collision record resulting from a collision between forest trust records.</summary>
	// Token: 0x0200005F RID: 95
	public class ForestTrustRelationshipCollision
	{
		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" /> value for the forest trust collision.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" /> value indicating the collision type.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000208C File Offset: 0x0000028C
		public ForestTrustCollisionType CollisionType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollisionOptions" /> value for the forest trust collision.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollisionOptions" /> value that provides information about the collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> type is <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" />.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000208C File Offset: 0x0000028C
		public TopLevelNameCollisionOptions TopLevelNameCollisionOption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollisionOptions" /> value for the forest trust collision.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollisionOptions" /> value that provides information about the collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> type is <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" />.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainCollisionOptions DomainCollisionOption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the collision record from the underlying Active Directory Domain Services service.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the collision record resulting from a collision between forest trust records.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000208C File Offset: 0x0000028C
		public string CollisionRecord
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
