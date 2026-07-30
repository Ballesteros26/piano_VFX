using System;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic principal.</summary>
	// Token: 0x0200061F RID: 1567
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericPrincipal" /> class from a user identity and an array of role names to which the user represented by that identity belongs.</summary>
		/// <param name="identity">A basic implementation of <see cref="T:System.Security.Principal.IIdentity" /> that represents any user. </param>
		/// <param name="roles">An array of role names to which the user represented by the <paramref name="identity" /> parameter belongs. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identity" /> parameter is null. </exception>
		// Token: 0x06004435 RID: 17461 RVA: 0x000EFDBC File Offset: 0x000EDFBC
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x000EFE0E File Offset: 0x000EE00E
		internal string[] Roles
		{
			get
			{
				return this.m_roles;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the current <see cref="T:System.Security.Principal.GenericPrincipal" />.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.GenericIdentity" /> of the user represented by the <see cref="T:System.Security.Principal.GenericPrincipal" />.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x000EFE16 File Offset: 0x000EE016
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.Principal.GenericPrincipal" /> belongs to the specified role.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Principal.GenericPrincipal" /> is a member of the specified role; otherwise, false.</returns>
		/// <param name="role">The name of the role for which to check membership. </param>
		// Token: 0x06004438 RID: 17464 RVA: 0x000EFE20 File Offset: 0x000EE020
		public override bool IsInRole(string role)
		{
			if (this.m_roles == null)
			{
				return false;
			}
			int length = role.Length;
			foreach (string text in this.m_roles)
			{
				if (text != null && length == text.Length && string.Compare(role, 0, text, 0, length, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002267 RID: 8807
		private IIdentity m_identity;

		// Token: 0x04002268 RID: 8808
		private string[] m_roles;
	}
}
