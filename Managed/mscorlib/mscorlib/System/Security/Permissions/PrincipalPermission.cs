using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Permissions
{
	/// <summary>Allows checks against the active principal (see <see cref="T:System.Security.Principal.IPrincipal" />) using the language constructs defined for both declarative and imperative security actions. This class cannot be inherited.</summary>
	// Token: 0x020005A4 RID: 1444
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x0600404D RID: 16461 RVA: 0x000E4F4C File Offset: 0x000E314C
		public PrincipalPermission(PermissionState state)
		{
			this.principals = new ArrayList();
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(null, null, true);
				this.principals.Add(principalInfo);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class for the specified <paramref name="name" /> and <paramref name="role" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user. </param>
		/// <param name="role">The role of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user (for example, Administrator). </param>
		// Token: 0x0600404E RID: 16462 RVA: 0x000E4F8A File Offset: 0x000E318A
		public PrincipalPermission(string name, string role)
			: this(name, role, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class for the specified <paramref name="name" />, <paramref name="role" />, and authentication status.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user. </param>
		/// <param name="role">The role of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user (for example, Administrator). </param>
		/// <param name="isAuthenticated">true to signify that the user is authenticated; otherwise, false. </param>
		// Token: 0x0600404F RID: 16463 RVA: 0x000E4F98 File Offset: 0x000E3198
		public PrincipalPermission(string name, string role, bool isAuthenticated)
		{
			this.principals = new ArrayList();
			PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(name, role, isAuthenticated);
			this.principals.Add(principalInfo);
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000E4FCC File Offset: 0x000E31CC
		internal PrincipalPermission(ArrayList principals)
		{
			this.principals = (ArrayList)principals.Clone();
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06004051 RID: 16465 RVA: 0x000E4FE5 File Offset: 0x000E31E5
		public IPermission Copy()
		{
			return new PrincipalPermission(this.principals);
		}

		/// <summary>Determines at run time whether the current principal matches the principal specified by the current permission.</summary>
		/// <exception cref="T:System.Security.SecurityException">The current principal does not pass the security check for the principal specified by the current permission.-or- The current <see cref="T:System.Security.Principal.IPrincipal" /> is null. </exception>
		// Token: 0x06004052 RID: 16466 RVA: 0x000E4FF4 File Offset: 0x000E31F4
		[SecuritySafeCritical]
		public void Demand()
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null)
			{
				throw new SecurityException("no Principal");
			}
			if (this.principals.Count > 0)
			{
				bool flag = false;
				foreach (object obj in this.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
					if ((principalInfo.Name == null || principalInfo.Name == currentPrincipal.Identity.Name) && (principalInfo.Role == null || currentPrincipal.IsInRole(principalInfo.Role)) && ((principalInfo.IsAuthenticated && currentPrincipal.Identity.IsAuthenticated) || !principalInfo.IsAuthenticated))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new SecurityException("Demand for principal refused.");
				}
			}
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter is not a valid permission element.-or- The <paramref name="elem" /> parameter's version number is not valid. </exception>
		// Token: 0x06004053 RID: 16467 RVA: 0x000E50D8 File Offset: 0x000E32D8
		public void FromXml(SecurityElement elem)
		{
			this.CheckSecurityElement(elem, "elem", 1, 1);
			this.principals.Clear();
			if (elem.Children != null)
			{
				foreach (object obj in elem.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (securityElement.Tag != "Identity")
					{
						throw new ArgumentException("not IPermission/Identity");
					}
					string text = securityElement.Attribute("ID");
					string text2 = securityElement.Attribute("Role");
					string text3 = securityElement.Attribute("Authenticated");
					bool flag = false;
					if (text3 != null)
					{
						try
						{
							flag = bool.Parse(text3);
						}
						catch
						{
						}
					}
					PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(text, text2, flag);
					this.principals.Add(principalInfo);
				}
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission will be null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission. </exception>
		// Token: 0x06004054 RID: 16468 RVA: 0x000E51CC File Offset: 0x000E33CC
		public IPermission Intersect(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.Copy();
			}
			if (principalPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(PermissionState.None);
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if (principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						string text = null;
						if (principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null)
						{
							text = principalInfo.Name;
						}
						else if (principalInfo.Name == null)
						{
							text = principalInfo2.Name;
						}
						string text2 = null;
						if (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null)
						{
							text2 = principalInfo.Role;
						}
						else if (principalInfo.Role == null)
						{
							text2 = principalInfo2.Role;
						}
						if (text != null || text2 != null)
						{
							PrincipalPermission.PrincipalInfo principalInfo3 = new PrincipalPermission.PrincipalInfo(text, text2, principalInfo.IsAuthenticated);
							principalPermission2.principals.Add(principalInfo3);
						}
					}
				}
			}
			if (principalPermission2.principals.Count <= 0)
			{
				return null;
			}
			return principalPermission2;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission. </exception>
		// Token: 0x06004055 RID: 16469 RVA: 0x000E5380 File Offset: 0x000E3580
		public bool IsSubsetOf(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.IsUnrestricted();
			}
			if (principalPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				bool flag = false;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06004056 RID: 16470 RVA: 0x000E54A8 File Offset: 0x000E36A8
		public bool IsUnrestricted()
		{
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				if (principalInfo.Name == null && principalInfo.Role == null && principalInfo.IsAuthenticated)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates and returns a string representing the current permission.</summary>
		/// <returns>A representation of the current permission.</returns>
		// Token: 0x06004057 RID: 16471 RVA: 0x000E551C File Offset: 0x000E371C
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004058 RID: 16472 RVA: 0x000E552C File Offset: 0x000E372C
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Permission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", 1.ToString());
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				SecurityElement securityElement2 = new SecurityElement("Identity");
				if (principalInfo.Name != null)
				{
					securityElement2.AddAttribute("ID", principalInfo.Name);
				}
				if (principalInfo.Role != null)
				{
					securityElement2.AddAttribute("Role", principalInfo.Role);
				}
				if (principalInfo.IsAuthenticated)
				{
					securityElement2.AddAttribute("Authenticated", "true");
				}
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is an object that is not of the same type as the current permission. </exception>
		// Token: 0x06004059 RID: 16473 RVA: 0x000E5640 File Offset: 0x000E3840
		public IPermission Union(IPermission other)
		{
			PrincipalPermission principalPermission = this.Cast(other);
			if (principalPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(this.principals);
			foreach (object obj in principalPermission.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				principalPermission2.principals.Add(principalInfo);
			}
			return principalPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.PrincipalPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.PrincipalPermission" /> is equal to the current <see cref="T:System.Security.Permissions.PrincipalPermission" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.PrincipalPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.PrincipalPermission" />. </param>
		// Token: 0x0600405A RID: 16474 RVA: 0x000E56D8 File Offset: 0x000E38D8
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PrincipalPermission principalPermission = obj as PrincipalPermission;
			if (principalPermission == null)
			{
				return false;
			}
			if (this.principals.Count != principalPermission.principals.Count)
			{
				return false;
			}
			foreach (object obj2 in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj2;
				bool flag = false;
				foreach (object obj3 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj3;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
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

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.PrincipalPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.PrincipalPermission" /> object.</returns>
		// Token: 0x0600405B RID: 16475 RVA: 0x0007E1C5 File Offset: 0x0007C3C5
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00058C99 File Offset: 0x00056E99
		int IBuiltInPermission.GetTokenIndex()
		{
			return 8;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x000E5804 File Offset: 0x000E3A04
		private PrincipalPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PrincipalPermission principalPermission = target as PrincipalPermission;
			if (principalPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PrincipalPermission));
			}
			return principalPermission;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000E5824 File Offset: 0x000E3A24
		private bool IsEmpty()
		{
			return this.principals.Count == 0;
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000E5834 File Offset: 0x000E3A34
		internal int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "Permission")
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}"), se.Tag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x040020A0 RID: 8352
		private const int version = 1;

		// Token: 0x040020A1 RID: 8353
		private ArrayList principals;

		// Token: 0x020005A5 RID: 1445
		internal class PrincipalInfo
		{
			// Token: 0x06004060 RID: 16480 RVA: 0x000E58F0 File Offset: 0x000E3AF0
			public PrincipalInfo(string name, string role, bool isAuthenticated)
			{
				this._name = name;
				this._role = role;
				this._isAuthenticated = isAuthenticated;
			}

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06004061 RID: 16481 RVA: 0x000E590D File Offset: 0x000E3B0D
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06004062 RID: 16482 RVA: 0x000E5915 File Offset: 0x000E3B15
			public string Role
			{
				get
				{
					return this._role;
				}
			}

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06004063 RID: 16483 RVA: 0x000E591D File Offset: 0x000E3B1D
			public bool IsAuthenticated
			{
				get
				{
					return this._isAuthenticated;
				}
			}

			// Token: 0x040020A2 RID: 8354
			private string _name;

			// Token: 0x040020A3 RID: 8355
			private string _role;

			// Token: 0x040020A4 RID: 8356
			private bool _isAuthenticated;
		}
	}
}
