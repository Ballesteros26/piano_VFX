using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for files originating in the global assembly cache. This class cannot be inherited.</summary>
	// Token: 0x0200058F RID: 1423
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class.</summary>
		// Token: 0x06003FB2 RID: 16306 RVA: 0x000E30F6 File Offset: 0x000E12F6
		public GacIdentityPermission()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class with fully restricted <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000E3C80 File Offset: 0x000E1E80
		public GacIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000DED8E File Offset: 0x000DCF8E
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. The new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000E3C90 File Offset: 0x000E1E90
		public override IPermission Intersect(IPermission target)
		{
			if (this.Cast(target) == null)
			{
				return null;
			}
			return this.Copy();
		}

		/// <summary>Indicates whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission object to test for the subset relationship. The permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000E3CA3 File Offset: 0x000E1EA3
		public override bool IsSubsetOf(IPermission target)
		{
			return this.Cast(target) != null;
		}

		/// <summary>Creates and returns a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003FB7 RID: 16311 RVA: 0x000E3CAF File Offset: 0x000E1EAF
		public override IPermission Union(IPermission target)
		{
			this.Cast(target);
			return this.Copy();
		}

		/// <summary>Creates a permission from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" />  that contains the XML encoding to use to create the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" />is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element. -or- The version number of <paramref name="securityElement" /> is not valid. </exception>
		// Token: 0x06003FB8 RID: 16312 RVA: 0x000E3CBF File Offset: 0x000E1EBF
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that represents the XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003FB9 RID: 16313 RVA: 0x000E3CCF File Offset: 0x000E1ECF
		public override SecurityElement ToXml()
		{
			return base.Element(1);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0003B2DF File Offset: 0x000394DF
		int IBuiltInPermission.GetTokenIndex()
		{
			return 15;
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000E3CD8 File Offset: 0x000E1ED8
		private GacIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			GacIdentityPermission gacIdentityPermission = target as GacIdentityPermission;
			if (gacIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(GacIdentityPermission));
			}
			return gacIdentityPermission;
		}

		// Token: 0x04002044 RID: 8260
		private const int version = 1;
	}
}
