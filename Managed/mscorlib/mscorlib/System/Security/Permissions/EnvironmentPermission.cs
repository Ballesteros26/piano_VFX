using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Controls access to system and user environment variables. This class cannot be inherited.</summary>
	// Token: 0x02000586 RID: 1414
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003F4B RID: 16203 RVA: 0x000E2592 File Offset: 0x000E0792
		public EnvironmentPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with the specified access to the specified environment variables.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated) to which access is granted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003F4C RID: 16204 RVA: 0x000E25BD File Offset: 0x000E07BD
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.SetPathList(flag, pathList);
		}

		/// <summary>Adds access for the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003F4D RID: 16205 RVA: 0x000E25E4 File Offset: 0x000E07E4
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
				foreach (string text in pathList.Split(new char[] { ';' }))
				{
					if (!this.readList.Contains(text))
					{
						this.readList.Add(text);
					}
				}
				return;
			case EnvironmentPermissionAccess.Write:
				foreach (string text2 in pathList.Split(new char[] { ';' }))
				{
					if (!this.writeList.Contains(text2))
					{
						this.writeList.Add(text2);
					}
				}
				return;
			case EnvironmentPermissionAccess.AllAccess:
				foreach (string text3 in pathList.Split(new char[] { ';' }))
				{
					if (!this.readList.Contains(text3))
					{
						this.readList.Add(text3);
					}
					if (!this.writeList.Contains(text3))
					{
						this.writeList.Add(text3);
					}
				}
				return;
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003F4E RID: 16206 RVA: 0x000E2700 File Offset: 0x000E0900
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(this._state);
			string text = this.GetPathList(EnvironmentPermissionAccess.Read);
			if (text != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			text = this.GetPathList(EnvironmentPermissionAccess.Write);
			if (text != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, text);
			}
			return environmentPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003F4F RID: 16207 RVA: 0x000E2740 File Offset: 0x000E0940
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Read");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			string text2 = esd.Attribute("Write");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Write, text2);
			}
		}

		/// <summary>Gets all environment variables with the specified <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</summary>
		/// <returns>A list of environment variables (semicolon-separated) for the selected flag.</returns>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values that represents a single type of environment variable access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.-or- <paramref name="flag" /> is <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.AllAccess" />, which represents more than one type of environment variable access, or <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.NoAccess" />, which does not represent any type of environment variable access. </exception>
		// Token: 0x06003F50 RID: 16208 RVA: 0x000E27AC File Offset: 0x000E09AC
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
			case EnvironmentPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(flag, true);
				break;
			case EnvironmentPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case EnvironmentPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
			return null;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F51 RID: 16209 RVA: 0x000E2800 File Offset: 0x000E0A00
		[SecuritySafeCritical]
		public override IPermission Intersect(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.Copy();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			int num = 0;
			EnvironmentPermission environmentPermission2 = new EnvironmentPermission(PermissionState.None);
			string pathList = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (pathList != null)
			{
				foreach (string text in pathList.Split(new char[] { ';' }))
				{
					if (this.readList.Contains(text))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, text);
						num++;
					}
				}
			}
			string pathList2 = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (pathList2 != null)
			{
				foreach (string text2 in pathList2.Split(new char[] { ';' }))
				{
					if (this.writeList.Contains(text2))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, text2);
						num++;
					}
				}
			}
			if (num <= 0)
			{
				return null;
			}
			return environmentPermission2;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F52 RID: 16210 RVA: 0x000E28F4 File Offset: 0x000E0AF4
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return false;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.IsUnrestricted();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.readList)
			{
				string text = (string)obj;
				if (!environmentPermission.readList.Contains(text))
				{
					return false;
				}
			}
			foreach (object obj2 in this.writeList)
			{
				string text2 = (string)obj2;
				if (!environmentPermission.writeList.Contains(text2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003F53 RID: 16211 RVA: 0x000E29DC File Offset: 0x000E0BDC
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		/// <summary>Sets the specified access to the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003F54 RID: 16212 RVA: 0x000E29E8 File Offset: 0x000E0BE8
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
				this.readList.Clear();
				foreach (string text in pathList.Split(new char[] { ';' }))
				{
					this.readList.Add(text);
				}
				return;
			case EnvironmentPermissionAccess.Write:
				this.writeList.Clear();
				foreach (string text2 in pathList.Split(new char[] { ';' }))
				{
					this.writeList.Add(text2);
				}
				return;
			case EnvironmentPermissionAccess.AllAccess:
				this.readList.Clear();
				this.writeList.Clear();
				foreach (string text3 in pathList.Split(new char[] { ';' }))
				{
					this.readList.Add(text3);
					this.writeList.Add(text3);
				}
				return;
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003F55 RID: 16213 RVA: 0x000E2AF8 File Offset: 0x000E0CF8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string text = this.GetPathList(EnvironmentPermissionAccess.Read);
				if (text != null)
				{
					securityElement.AddAttribute("Read", text);
				}
				text = this.GetPathList(EnvironmentPermissionAccess.Write);
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
		// Token: 0x06003F56 RID: 16214 RVA: 0x000E2B58 File Offset: 0x000E0D58
		[SecuritySafeCritical]
		public override IPermission Union(IPermission other)
		{
			EnvironmentPermission environmentPermission = this.Cast(other);
			if (environmentPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && environmentPermission.IsEmpty())
			{
				return null;
			}
			EnvironmentPermission environmentPermission2 = (EnvironmentPermission)this.Copy();
			string text = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (text != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, text);
			}
			text = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (text != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, text);
			}
			return environmentPermission2;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00015ED5 File Offset: 0x000140D5
		int IBuiltInPermission.GetTokenIndex()
		{
			return 0;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000E2BD3 File Offset: 0x000E0DD3
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000E2BFA File Offset: 0x000E0DFA
		private EnvironmentPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			EnvironmentPermission environmentPermission = target as EnvironmentPermission;
			if (environmentPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(EnvironmentPermission));
			}
			return environmentPermission;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000E2C1C File Offset: 0x000E0E1C
		internal void ThrowInvalidFlag(EnvironmentPermissionAccess flag, bool context)
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

		// Token: 0x06003F5B RID: 16219 RVA: 0x000E2C5C File Offset: 0x000E0E5C
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

		// Token: 0x04002017 RID: 8215
		private const int version = 1;

		// Token: 0x04002018 RID: 8216
		private PermissionState _state;

		// Token: 0x04002019 RID: 8217
		private ArrayList readList;

		// Token: 0x0400201A RID: 8218
		private ArrayList writeList;
	}
}
