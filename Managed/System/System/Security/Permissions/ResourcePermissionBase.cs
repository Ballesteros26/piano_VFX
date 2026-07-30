using System;
using System.Collections;

namespace System.Security.Permissions
{
	/// <summary>Allows control of code access security permissions.</summary>
	// Token: 0x02000375 RID: 885
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class.</summary>
		// Token: 0x06001B0D RID: 6925 RVA: 0x0006C4D4 File Offset: 0x0006A6D4
		protected ResourcePermissionBase()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class with the specified level of access to resources at creation.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001B0E RID: 6926 RVA: 0x0006C4E7 File Offset: 0x0006A6E7
		protected ResourcePermissionBase(PermissionState state)
			: this()
		{
			PermissionHelper.CheckPermissionState(state, true);
			this._unrestricted = state == PermissionState.Unrestricted;
		}

		/// <summary>Gets or sets an enumeration value that describes the types of access that you are giving the resource.</summary>
		/// <returns>An enumeration value that is derived from <see cref="T:System.Type" /> and describes the types of access that you are giving the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The property value is not an enumeration value. </exception>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0006C501 File Offset: 0x0006A701
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0006C509 File Offset: 0x0006A709
		protected Type PermissionAccessType
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PermissionAccessType");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException("!Enum", "PermissionAccessType");
				}
				this._type = value;
			}
		}

		/// <summary>Gets or sets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are trying to protect.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the array is 0. </exception>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0006C53E File Offset: 0x0006A73E
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0006C546 File Offset: 0x0006A746
		protected string[] TagNames
		{
			get
			{
				return this._tags;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("TagNames");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Length==0", "TagNames");
				}
				this._tags = value;
			}
		}

		/// <summary>Adds a permission entry to the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.-or- The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is already included in the permission. </exception>
		// Token: 0x06001B13 RID: 6931 RVA: 0x0006C571 File Offset: 0x0006A771
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			if (this.Exists(entry))
			{
				throw new InvalidOperationException(global::Locale.GetText("Entry already exists."));
			}
			this._list.Add(entry);
		}

		/// <summary>Clears the permission of the added permission entries.</summary>
		// Token: 0x06001B14 RID: 6932 RVA: 0x0006C5A0 File Offset: 0x0006A7A0
		protected void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001B15 RID: 6933 RVA: 0x0006C5B0 File Offset: 0x0006A7B0
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = ResourcePermissionBase.CreateFromType(base.GetType(), this._unrestricted);
			if (this._tags != null)
			{
				resourcePermissionBase._tags = (string[])this._tags.Clone();
			}
			resourcePermissionBase._type = this._type;
			resourcePermissionBase._list.AddRange(this._list);
			return resourcePermissionBase;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> parameter is not a valid permission element.-or- The version number of the <paramref name="securityElement" /> parameter is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is null.</exception>
		// Token: 0x06001B16 RID: 6934 RVA: 0x0006C60C File Offset: 0x0006A80C
		[MonoTODO("incomplete - need more test")]
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._list.Clear();
			this._unrestricted = PermissionHelper.IsUnrestricted(securityElement);
			if (securityElement.Children == null || securityElement.Children.Count < 1)
			{
				return;
			}
			string[] array = new string[1];
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				array[0] = securityElement2.Attribute("name");
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = new ResourcePermissionBaseEntry((int)Enum.Parse(this.PermissionAccessType, securityElement2.Attribute("access")), array);
				this.AddPermissionAccess(resourcePermissionBaseEntry);
			}
		}

		/// <summary>Returns an array of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects added to this permission.</summary>
		/// <returns>An array of <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects that were added to this permission.</returns>
		// Token: 0x06001B17 RID: 6935 RVA: 0x0006C6EC File Offset: 0x0006A8EC
		protected ResourcePermissionBaseEntry[] GetPermissionEntries()
		{
			ResourcePermissionBaseEntry[] array = new ResourcePermissionBaseEntry[this._list.Count];
			this._list.CopyTo(array, 0);
			return array;
		}

		/// <summary>Creates and returns a permission object that is the intersection of the current permission object and a target permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="target">A permission object of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The target permission object is not of the same type as the current permission object. </exception>
		// Token: 0x06001B18 RID: 6936 RVA: 0x0006C718 File Offset: 0x0006A918
		public override IPermission Intersect(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return null;
			}
			bool flag = this.IsUnrestricted();
			bool flag2 = resourcePermissionBase.IsUnrestricted();
			if (this.IsEmpty() && !flag2)
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty() && !flag)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag && flag2);
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (flag2 || resourcePermissionBase.Exists(resourcePermissionBaseEntry))
				{
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry);
				}
			}
			foreach (object obj2 in resourcePermissionBase._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry2 = (ResourcePermissionBaseEntry)obj2;
				if ((flag || this.Exists(resourcePermissionBaseEntry2)) && !resourcePermissionBase2.Exists(resourcePermissionBaseEntry2))
				{
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry2);
				}
			}
			return resourcePermissionBase2;
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object; otherwise, false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. </param>
		// Token: 0x06001B19 RID: 6937 RVA: 0x0006C834 File Offset: 0x0006AA34
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return true;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				return false;
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.IsUnrestricted();
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (!resourcePermissionBase.Exists(resourcePermissionBaseEntry))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>true if permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06001B1A RID: 6938 RVA: 0x0006C8C4 File Offset: 0x0006AAC4
		public bool IsUnrestricted()
		{
			return this._unrestricted;
		}

		/// <summary>Removes a permission entry from the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.-or- The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is not in the permission. </exception>
		// Token: 0x06001B1B RID: 6939 RVA: 0x0006C8CC File Offset: 0x0006AACC
		protected void RemovePermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			for (int i = 0; i < this._list.Count; i++)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)this._list[i];
				if (this.Equals(entry, resourcePermissionBaseEntry))
				{
					this._list.RemoveAt(i);
					return;
				}
			}
			throw new InvalidOperationException(global::Locale.GetText("Entry doesn't exists."));
		}

		/// <summary>Creates and returns an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001B1C RID: 6940 RVA: 0x0006C930 File Offset: 0x0006AB30
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(base.GetType(), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					SecurityElement securityElement2 = securityElement;
					string text = null;
					if (this.PermissionAccessType != null)
					{
						text = Enum.Format(this.PermissionAccessType, resourcePermissionBaseEntry.PermissionAccess, "g");
					}
					for (int i = 0; i < this._tags.Length; i++)
					{
						SecurityElement securityElement3 = new SecurityElement(this._tags[i]);
						securityElement3.AddAttribute("name", resourcePermissionBaseEntry.PermissionAccessPath[i]);
						if (text != null)
						{
							securityElement3.AddAttribute("access", text);
						}
						securityElement2.AddChild(securityElement3);
					}
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission object that combines the current permission object and the target permission object.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> permission object is not of the same type as the current permission object. </exception>
		// Token: 0x06001B1D RID: 6941 RVA: 0x0006CA40 File Offset: 0x0006AC40
		public override IPermission Union(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && resourcePermissionBase.IsEmpty())
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return resourcePermissionBase.Copy();
			}
			bool flag = this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted();
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag);
			if (!flag)
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry);
				}
				foreach (object obj2 in resourcePermissionBase._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry2 = (ResourcePermissionBaseEntry)obj2;
					if (!resourcePermissionBase2.Exists(resourcePermissionBaseEntry2))
					{
						resourcePermissionBase2.AddPermissionAccess(resourcePermissionBaseEntry2);
					}
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0006CB60 File Offset: 0x0006AD60
		private bool IsEmpty()
		{
			return !this._unrestricted && this._list.Count == 0;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0006CB7A File Offset: 0x0006AD7A
		private ResourcePermissionBase Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(ResourcePermissionBase));
			}
			return resourcePermissionBase;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0006CB9A File Offset: 0x0006AD9A
		internal void CheckEntry(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath == null || entry.PermissionAccessPath.Length != this._tags.Length)
			{
				throw new InvalidOperationException(global::Locale.GetText("Entry doesn't match TagNames"));
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0006CBD4 File Offset: 0x0006ADD4
		internal bool Equals(ResourcePermissionBaseEntry entry1, ResourcePermissionBaseEntry entry2)
		{
			if (entry1.PermissionAccess != entry2.PermissionAccess)
			{
				return false;
			}
			if (entry1.PermissionAccessPath.Length != entry2.PermissionAccessPath.Length)
			{
				return false;
			}
			for (int i = 0; i < entry1.PermissionAccessPath.Length; i++)
			{
				if (entry1.PermissionAccessPath[i] != entry2.PermissionAccessPath[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0006CC34 File Offset: 0x0006AE34
		internal bool Exists(ResourcePermissionBaseEntry entry)
		{
			if (this._list.Count == 0)
			{
				return false;
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
				if (this.Equals(resourcePermissionBaseEntry, entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0006CCA8 File Offset: 0x0006AEA8
		internal static void ValidateMachineName(string name)
		{
			if (name == null || name.Length == 0 || name.IndexOfAny(ResourcePermissionBase.invalidChars) != -1)
			{
				string text = global::Locale.GetText("Invalid machine name '{0}'.");
				if (name == null)
				{
					name = "(null)";
				}
				throw new ArgumentException(string.Format(text, name), "MachineName");
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0006CCE8 File Offset: 0x0006AEE8
		internal static ResourcePermissionBase CreateFromType(Type type, bool unrestricted)
		{
			return (ResourcePermissionBase)Activator.CreateInstance(type, new object[] { unrestricted ? PermissionState.Unrestricted : PermissionState.None });
		}

		// Token: 0x0400188D RID: 6285
		private const int version = 1;

		// Token: 0x0400188E RID: 6286
		private ArrayList _list;

		// Token: 0x0400188F RID: 6287
		private bool _unrestricted;

		// Token: 0x04001890 RID: 6288
		private Type _type;

		// Token: 0x04001891 RID: 6289
		private string[] _tags;

		/// <summary>Specifies the character to be used to represent the any wildcard character.</summary>
		// Token: 0x04001892 RID: 6290
		public const string Any = "*";

		/// <summary>Specifies the character to be used to represent a local reference.</summary>
		// Token: 0x04001893 RID: 6291
		public const string Local = ".";

		// Token: 0x04001894 RID: 6292
		private static char[] invalidChars = new char[] { '\t', '\n', '\v', '\f', '\r', ' ', '\\', 'Š' };
	}
}
