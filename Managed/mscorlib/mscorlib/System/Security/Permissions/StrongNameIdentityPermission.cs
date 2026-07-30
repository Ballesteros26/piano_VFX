using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for strong names. This class cannot be inherited.</summary>
	// Token: 0x020005B6 RID: 1462
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06004107 RID: 16647 RVA: 0x000E7542 File Offset: 0x000E5742
		public StrongNameIdentityPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this._list = new ArrayList();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class for the specified strong name identity.</summary>
		/// <param name="blob">The public key defining the strong name identity namespace. </param>
		/// <param name="name">The simple name part of the strong name identity. This corresponds to the name of the assembly. </param>
		/// <param name="version">The version number of the identity. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06004108 RID: 16648 RVA: 0x000E7578 File Offset: 0x000E5778
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000E75DC File Offset: 0x000E57DC
		internal StrongNameIdentityPermission(StrongNameIdentityPermission snip)
		{
			this._state = snip._state;
			this._list = new ArrayList(snip._list.Count);
			foreach (object obj in snip._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				this._list.Add(new StrongNameIdentityPermission.SNIP(snip2.PublicKey, snip2.Name, snip2.AssemblyVersion));
			}
		}

		/// <summary>Gets or sets the simple name portion of the strong name identity.</summary>
		/// <returns>The simple name of the identity.</returns>
		/// <exception cref="T:System.ArgumentException">The value is an empty string ("").</exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x000E7680 File Offset: 0x000E5880
		// (set) Token: 0x0600410B RID: 16651 RVA: 0x000E76AC File Offset: 0x000E58AC
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException("name");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.Name = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the public key blob that defines the strong name identity namespace.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> that contains the public key of the identity, or null if there is no key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is set to null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x000E770F File Offset: 0x000E590F
		// (set) Token: 0x0600410D RID: 16653 RVA: 0x000E773C File Offset: 0x000E593C
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.PublicKey = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the version number of the identity.</summary>
		/// <returns>The version of the identity.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x000E7797 File Offset: 0x000E5997
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x000E77C4 File Offset: 0x000E59C4
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
			set
			{
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.AssemblyVersion = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000E7811 File Offset: 0x000E5A11
		internal void ResetToDefault()
		{
			this._list.Clear();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06004111 RID: 16657 RVA: 0x000E7834 File Offset: 0x000E5A34
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			return new StrongNameIdentityPermission(this);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid permission element.-or- The <paramref name="e" /> parameter's version number is not valid. </exception>
		// Token: 0x06004112 RID: 16658 RVA: 0x000E784C File Offset: 0x000E5A4C
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				using (IEnumerator enumerator = e.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement = (SecurityElement)obj;
						this._list.Add(this.FromSecurityElement(securityElement));
					}
					return;
				}
			}
			this._list.Add(this.FromSecurityElement(e));
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000E7900 File Offset: 0x000E5B00
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string text = se.Attribute("Name");
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text2 = se.Attribute("AssemblyVersion");
			Version version = ((text2 == null) ? null : new Version(text2));
			return new StrongNameIdentityPermission.SNIP(strongNamePublicKeyBlob, text, version);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission, or null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004114 RID: 16660 RVA: 0x000E794C File Offset: 0x000E5B4C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong permission type."));
			}
			if (this.IsEmpty() || strongNameIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (!this.Match(strongNameIdentityPermission.Name))
			{
				return null;
			}
			string text = ((this.Name.Length < strongNameIdentityPermission.Name.Length) ? this.Name : strongNameIdentityPermission.Name);
			if (!this.Version.Equals(strongNameIdentityPermission.Version))
			{
				return null;
			}
			if (!this.PublicKey.Equals(strongNameIdentityPermission.PublicKey))
			{
				return null;
			}
			return new StrongNameIdentityPermission(this.PublicKey, text, this.Version);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004115 RID: 16661 RVA: 0x000E7A00 File Offset: 0x000E5C00
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(snip2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004116 RID: 16662 RVA: 0x000E7AE4 File Offset: 0x000E5CE4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				using (IEnumerator enumerator = this._list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
						SecurityElement securityElement2 = new SecurityElement("StrongName");
						this.ToSecurityElement(securityElement2, snip);
						securityElement.AddChild(securityElement2);
					}
					return securityElement;
				}
			}
			if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000E7BA0 File Offset: 0x000E5DA0
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal and one is a subset of the other.</exception>
		// Token: 0x06004118 RID: 16664 RVA: 0x000E7C08 File Offset: 0x000E5E08
		public override IPermission Union(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null || strongNameIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return strongNameIdentityPermission.Copy();
			}
			StrongNameIdentityPermission strongNameIdentityPermission2 = (StrongNameIdentityPermission)this.Copy();
			foreach (object obj in strongNameIdentityPermission._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				if (!this.IsEmpty(snip) && !this.Contains(snip))
				{
					strongNameIdentityPermission2._list.Add(snip);
				}
			}
			return strongNameIdentityPermission2;
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x00059020 File Offset: 0x00057220
		int IBuiltInPermission.GetTokenIndex()
		{
			return 12;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000E7CB8 File Offset: 0x000E5EB8
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000E7CC4 File Offset: 0x000E5EC4
		private bool Contains(StrongNameIdentityPermission.SNIP snip)
		{
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				bool flag = (snip2.PublicKey == null && snip.PublicKey == null) || (snip2.PublicKey != null && snip2.PublicKey.Equals(snip.PublicKey));
				bool flag2 = snip2.IsNameSubsetOf(snip.Name);
				bool flag3 = (snip2.AssemblyVersion == null && snip.AssemblyVersion == null) || (snip2.AssemblyVersion != null && snip2.AssemblyVersion.Equals(snip.AssemblyVersion));
				if (flag && flag2 && flag3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000E7DB0 File Offset: 0x000E5FB0
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000E7E00 File Offset: 0x000E6000
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x000E7E67 File Offset: 0x000E6067
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000E7E88 File Offset: 0x000E6088
		private bool Match(string target)
		{
			if (this.Name == null || target == null)
			{
				return false;
			}
			int num = this.Name.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int num3;
			if (num == -1 && num2 == -1)
			{
				num3 = Math.Max(this.Name.Length, target.Length);
			}
			else if (num == -1)
			{
				num3 = num2;
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = Math.Min(num, num2);
			}
			return string.Compare(this.Name, 0, target, 0, num3, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x040020EE RID: 8430
		private const int version = 1;

		// Token: 0x040020EF RID: 8431
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x040020F0 RID: 8432
		private PermissionState _state;

		// Token: 0x040020F1 RID: 8433
		private ArrayList _list;

		// Token: 0x020005B7 RID: 1463
		private struct SNIP
		{
			// Token: 0x06004121 RID: 16673 RVA: 0x000E7F1E File Offset: 0x000E611E
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x06004122 RID: 16674 RVA: 0x000E7F35 File Offset: 0x000E6135
			internal static StrongNameIdentityPermission.SNIP CreateDefault()
			{
				return new StrongNameIdentityPermission.SNIP(null, string.Empty, (Version)StrongNameIdentityPermission.defaultVersion.Clone());
			}

			// Token: 0x06004123 RID: 16675 RVA: 0x000E7F54 File Offset: 0x000E6154
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06004124 RID: 16676 RVA: 0x000E7FB0 File Offset: 0x000E61B0
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x040020F2 RID: 8434
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x040020F3 RID: 8435
			public string Name;

			// Token: 0x040020F4 RID: 8436
			public Version AssemblyVersion;
		}
	}
}
