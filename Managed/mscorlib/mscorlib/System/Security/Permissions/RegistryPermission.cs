using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access registry variables. This class cannot be inherited.</summary>
	// Token: 0x020005AC RID: 1452
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06004099 RID: 16537 RVA: 0x000E60E9 File Offset: 0x000E42E9
		public RegistryPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x0600409A RID: 16538 RVA: 0x000E611F File Offset: 0x000E431F
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this._state = PermissionState.None;
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.AddPathList(access, pathList);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables and the specified access rights to registry control information.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x0600409B RID: 16539 RVA: 0x000E6158 File Offset: 0x000E4358
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			if (!Enum.IsDefined(typeof(AccessControlActions), control))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), control), "AccessControlActions");
			}
			this._state = PermissionState.None;
			this.AddPathList(access, control, pathList);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x0600409C RID: 16540 RVA: 0x000E61B4 File Offset: 0x000E43B4
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.AddWithUnionKey(this.readList, pathList);
				return;
			case RegistryPermissionAccess.Write:
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			case RegistryPermissionAccess.Create:
				this.AddWithUnionKey(this.createList, pathList);
				return;
			case RegistryPermissionAccess.AllAccess:
				this.AddWithUnionKey(this.createList, pathList);
				this.AddWithUnionKey(this.readList, pathList);
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission, specifying registry permission access and access control actions.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="control">One of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values. </param>
		/// <param name="pathList">A list of registry variables (separated by semicolons).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x0600409D RID: 16541 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets paths for all registry variables with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</summary>
		/// <returns>A list of the registry variables (semicolon-separated) with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</returns>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values that represents a single type of registry variable access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- <paramref name="access" /> is <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />, which represents more than one type of registry variable access, or <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" />, which does not represent any type of registry variable access. </exception>
		// Token: 0x0600409E RID: 16542 RVA: 0x000E6254 File Offset: 0x000E4454
		public string GetPathList(RegistryPermissionAccess access)
		{
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
			case RegistryPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(access, true);
				goto IL_0061;
			case RegistryPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case RegistryPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			case RegistryPermissionAccess.Create:
				return this.GetPathList(this.createList);
			}
			this.ThrowInvalidFlag(access, false);
			IL_0061:
			return null;
		}

		/// <summary>Sets new access for the specified registry variable names to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x0600409F RID: 16543 RVA: 0x000E62C4 File Offset: 0x000E44C4
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.readList.Clear();
				foreach (string text in pathList.Split(new char[] { ';' }))
				{
					this.readList.Add(text);
				}
				return;
			case RegistryPermissionAccess.Write:
				this.writeList.Clear();
				foreach (string text2 in pathList.Split(new char[] { ';' }))
				{
					this.writeList.Add(text2);
				}
				return;
			case RegistryPermissionAccess.Create:
				this.createList.Clear();
				foreach (string text3 in pathList.Split(new char[] { ';' }))
				{
					this.createList.Add(text3);
				}
				return;
			case RegistryPermissionAccess.AllAccess:
				this.createList.Clear();
				this.readList.Clear();
				this.writeList.Clear();
				foreach (string text4 in pathList.Split(new char[] { ';' }))
				{
					this.createList.Add(text4);
					this.readList.Add(text4);
					this.writeList.Add(text4);
				}
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x060040A0 RID: 16544 RVA: 0x000E643C File Offset: 0x000E463C
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(this._state);
			string text = this.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x060040A1 RID: 16545 RVA: 0x000E6490 File Offset: 0x000E4690
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Create");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Create, text);
			}
			string text2 = esd.Attribute("Read");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Read, text2);
			}
			string text3 = esd.Attribute("Write");
			if (text3 != null && text3.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Write, text3);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040A2 RID: 16546 RVA: 0x000E6528 File Offset: 0x000E4728
		[SecuritySafeCritical]
		public override IPermission Intersect(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.Copy();
			}
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			RegistryPermission registryPermission2 = new RegistryPermission(PermissionState.None);
			this.IntersectKeys(this.createList, registryPermission.createList, registryPermission2.createList);
			this.IntersectKeys(this.readList, registryPermission.readList, registryPermission2.readList);
			this.IntersectKeys(this.writeList, registryPermission.writeList, registryPermission2.writeList);
			if (!registryPermission2.IsEmpty())
			{
				return registryPermission2;
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040A3 RID: 16547 RVA: 0x000E65BC File Offset: 0x000E47BC
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return false;
			}
			if (registryPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.IsUnrestricted();
			}
			return registryPermission.IsUnrestricted() || (this.KeyIsSubsetOf(this.createList, registryPermission.createList) && this.KeyIsSubsetOf(this.readList, registryPermission.readList) && this.KeyIsSubsetOf(this.writeList, registryPermission.writeList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x060040A4 RID: 16548 RVA: 0x000E6641 File Offset: 0x000E4841
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x060040A5 RID: 16549 RVA: 0x000E664C File Offset: 0x000E484C
		[SecuritySafeCritical]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string text = this.GetPathList(RegistryPermissionAccess.Create);
				if (text != null)
				{
					securityElement.AddAttribute("Create", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Read);
				if (text != null)
				{
					securityElement.AddAttribute("Read", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Write);
				if (text != null)
				{
					securityElement.AddAttribute("Write", text);
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040A6 RID: 16550 RVA: 0x000E66C4 File Offset: 0x000E48C4
		[SecuritySafeCritical]
		public override IPermission Union(IPermission other)
		{
			RegistryPermission registryPermission = this.Cast(other);
			if (registryPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && registryPermission.IsEmpty())
			{
				return null;
			}
			RegistryPermission registryPermission2 = (RegistryPermission)this.Copy();
			string text = registryPermission.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Create, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Read, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission2;
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x000293E5 File Offset: 0x000275E5
		int IBuiltInPermission.GetTokenIndex()
		{
			return 5;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000E6752 File Offset: 0x000E4952
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.createList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x000E6786 File Offset: 0x000E4986
		private RegistryPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(RegistryPermission));
			}
			return registryPermission;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000E67A8 File Offset: 0x000E49A8
		internal void ThrowInvalidFlag(RegistryPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000E67E8 File Offset: 0x000E49E8
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string text = (string)obj;
				stringBuilder.Append(text);
				stringBuilder.Append(";");
			}
			string text2 = stringBuilder.ToString();
			int length = text2.Length;
			if (length > 0)
			{
				return text2.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x000E6894 File Offset: 0x000E4A94
		internal bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text.StartsWith(text2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x000E6940 File Offset: 0x000E4B40
		internal void AddWithUnionKey(IList list, string pathList)
		{
			foreach (string text in pathList.Split(new char[] { ';' }))
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					for (int j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (text2.StartsWith(text))
						{
							list[j] = text;
						}
						else if (!text.StartsWith(text2))
						{
							list.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x000E69CC File Offset: 0x000E4BCC
		internal void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (text2.StartsWith(text))
						{
							result.Add(text2);
						}
					}
					else if (text.StartsWith(text2))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x040020BA RID: 8378
		private const int version = 1;

		// Token: 0x040020BB RID: 8379
		private PermissionState _state;

		// Token: 0x040020BC RID: 8380
		private ArrayList createList;

		// Token: 0x040020BD RID: 8381
		private ArrayList readList;

		// Token: 0x040020BE RID: 8382
		private ArrayList writeList;
	}
}
