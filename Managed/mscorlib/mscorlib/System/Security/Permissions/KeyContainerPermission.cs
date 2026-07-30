using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access key containers. This class cannot be inherited.</summary>
	// Token: 0x0200059C RID: 1436
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with either restricted or unrestricted permission.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x06003FFC RID: 16380 RVA: 0x000E4626 File Offset: 0x000E2826
		public KeyContainerPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified access.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </exception>
		// Token: 0x06003FFD RID: 16381 RVA: 0x000E4643 File Offset: 0x000E2843
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			this.SetFlags(flags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified global access and specific key container access rights.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <param name="accessList">An array of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects identifying specific key container access rights. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessList" /> is null. </exception>
		// Token: 0x06003FFE RID: 16382 RVA: 0x000E4654 File Offset: 0x000E2854
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			this.SetFlags(flags);
			if (accessList != null)
			{
				this._accessEntries = new KeyContainerPermissionAccessEntryCollection();
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in accessList)
				{
					this._accessEntries.Add(keyContainerPermissionAccessEntry);
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects associated with the current permission.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" /> containing the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects for this <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x000E469D File Offset: 0x000E289D
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this._accessEntries;
			}
		}

		/// <summary>Gets the key container permission flags that apply to all key containers associated with the permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x000E46A5 File Offset: 0x000E28A5
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06004001 RID: 16385 RVA: 0x000E46B0 File Offset: 0x000E28B0
		public override IPermission Copy()
		{
			if (this._accessEntries.Count == 0)
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[this._accessEntries.Count];
			this._accessEntries.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags, array);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.-or- The version number of <paramref name="securityElement" /> is not supported. </exception>
		// Token: 0x06004002 RID: 16386 RVA: 0x000E4700 File Offset: 0x000E2900
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
				return;
			}
			this._flags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), securityElement.Attribute("Flags"));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06004003 RID: 16387 RVA: 0x0000A42E File Offset: 0x0000862E
		[MonoTODO("(2.0)")]
		public override IPermission Intersect(IPermission target)
		{
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06004004 RID: 16388 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("(2.0)")]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		/// <summary>Determines whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06004005 RID: 16389 RVA: 0x000E4754 File Offset: 0x000E2954
		public bool IsUnrestricted()
		{
			return this._flags == KeyContainerPermissionFlags.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including state information.</returns>
		// Token: 0x06004006 RID: 16390 RVA: 0x000E4764 File Offset: 0x000E2964
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06004007 RID: 16391 RVA: 0x000E4794 File Offset: 0x000E2994
		public override IPermission Union(IPermission target)
		{
			KeyContainerPermission keyContainerPermission = this.Cast(target);
			if (keyContainerPermission == null)
			{
				return this.Copy();
			}
			KeyContainerPermissionAccessEntryCollection keyContainerPermissionAccessEntryCollection = new KeyContainerPermissionAccessEntryCollection();
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this._accessEntries)
			{
				keyContainerPermissionAccessEntryCollection.Add(keyContainerPermissionAccessEntry);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission._accessEntries)
			{
				if (this._accessEntries.IndexOf(keyContainerPermissionAccessEntry2) == -1)
				{
					keyContainerPermissionAccessEntryCollection.Add(keyContainerPermissionAccessEntry2);
				}
			}
			if (keyContainerPermissionAccessEntryCollection.Count == 0)
			{
				return new KeyContainerPermission(this._flags | keyContainerPermission._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[keyContainerPermissionAccessEntryCollection.Count];
			keyContainerPermissionAccessEntryCollection.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags | keyContainerPermission._flags, array);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000393E2 File Offset: 0x000375E2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000E4859 File Offset: 0x000E2A59
		private void SetFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & KeyContainerPermissionFlags.AllFlags) == KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), flags), "KeyContainerPermissionFlags");
			}
			this._flags = flags;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000E488B File Offset: 0x000E2A8B
		private KeyContainerPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = target as KeyContainerPermission;
			if (keyContainerPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(KeyContainerPermission));
			}
			return keyContainerPermission;
		}

		// Token: 0x0400207B RID: 8315
		private KeyContainerPermissionAccessEntryCollection _accessEntries;

		// Token: 0x0400207C RID: 8316
		private KeyContainerPermissionFlags _flags;

		// Token: 0x0400207D RID: 8317
		private const int version = 1;
	}
}
