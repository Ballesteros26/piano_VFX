using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
using Unity;

namespace System.Security.Principal
{
	/// <summary>Enables code to check the Windows group membership of a Windows user.</summary>
	// Token: 0x02000630 RID: 1584
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsPrincipal" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="ntIdentity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsPrincipal" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ntIdentity" /> is null. </exception>
		// Token: 0x060044C9 RID: 17609 RVA: 0x000F1C6F File Offset: 0x000EFE6F
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.WindowsIdentity" /> object of the current principal.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x000F1C8C File Offset: 0x000EFE8C
		public override IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified relative identifier (RID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group, that is, in a particular role; otherwise, false.</returns>
		/// <param name="rid">The RID of the Windows user group in which to check for the principal’s membership status. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060044CB RID: 17611 RVA: 0x000F1C94 File Offset: 0x000EFE94
		public virtual bool IsInRole(int rid)
		{
			if (Environment.IsUnix)
			{
				return WindowsPrincipal.IsMemberOfGroupId(this.Token, (IntPtr)rid);
			}
			string text;
			switch (rid)
			{
			case 544:
				text = "BUILTIN\\Administrators";
				break;
			case 545:
				text = "BUILTIN\\Users";
				break;
			case 546:
				text = "BUILTIN\\Guests";
				break;
			case 547:
				text = "BUILTIN\\Power Users";
				break;
			case 548:
				text = "BUILTIN\\Account Operators";
				break;
			case 549:
				text = "BUILTIN\\System Operators";
				break;
			case 550:
				text = "BUILTIN\\Print Operators";
				break;
			case 551:
				text = "BUILTIN\\Backup Operators";
				break;
			case 552:
				text = "BUILTIN\\Replicator";
				break;
			default:
				return false;
			}
			return this.IsInRole(text);
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified name.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">The name of the Windows user group for which to check membership. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060044CC RID: 17612 RVA: 0x000F1D40 File Offset: 0x000EFF40
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public override bool IsInRole(string role)
		{
			if (role == null)
			{
				return false;
			}
			if (Environment.IsUnix)
			{
				return WindowsPrincipal.IsMemberOfGroupName(this.Token, role);
			}
			if (this.m_roles == null)
			{
				this.m_roles = WindowsIdentity._GetRoles(this.Token);
			}
			role = role.ToUpperInvariant();
			foreach (string text in this.m_roles)
			{
				if (text != null && role == text.ToUpperInvariant())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">One of the <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> value.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060044CD RID: 17613 RVA: 0x000F1DB4 File Offset: 0x000EFFB4
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (!Environment.IsUnix)
			{
				return this.IsInRole((int)role);
			}
			if (role == WindowsBuiltInRole.Administrator)
			{
				string text = "root";
				return this.IsInRole(text);
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified security identifier (SID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="sid">A <see cref="T:System.Security.Principal.SecurityIdentifier" />  that uniquely identifies a Windows user group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sid" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">Windows returned a Win32 error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060044CE RID: 17614 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x000F1DEB File Offset: 0x000EFFEB
		private IntPtr Token
		{
			get
			{
				return this._identity.Token;
			}
		}

		// Token: 0x060044D0 RID: 17616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupId(IntPtr user, IntPtr group);

		// Token: 0x060044D1 RID: 17617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupName(IntPtr user, string group);

		/// <summary>Gets all Windows device claims from this principal.</summary>
		/// <returns>A collection of all Windows device claims from this principal.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x000F1BD7 File Offset: 0x000EFDD7
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets all Windows user claims from this principal.</summary>
		/// <returns>A collection of all Windows user claims from this principal.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x000F1BD7 File Offset: 0x000EFDD7
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x04002308 RID: 8968
		private WindowsIdentity _identity;

		// Token: 0x04002309 RID: 8969
		private string[] m_roles;
	}
}
