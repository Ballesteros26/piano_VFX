using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	/// <summary>Associates a content template in a <see cref="T:System.Web.UI.WebControls.LoginView" /> control with one or more roles defined for the Web site. This class cannot be inherited.</summary>
	// Token: 0x02000402 RID: 1026
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroup
	{
		/// <summary>Gets or sets the content template associated with this role group.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> associated with this role group. The default value is null.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00078BB8 File Offset: 0x00076DB8
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x00078BC0 File Offset: 0x00076DC0
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		public ITemplate ContentTemplate
		{
			get
			{
				return this.contentTemplate;
			}
			set
			{
				this.contentTemplate = value;
			}
		}

		/// <summary>Gets or sets the roles associated with this role group.</summary>
		/// <returns>A comma-separated list of roles associated with this role group. The default is null.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x00078BC9 File Offset: 0x00076DC9
		// (set) Token: 0x06002D8C RID: 11660 RVA: 0x00078BE5 File Offset: 0x00076DE5
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Roles
		{
			get
			{
				if (this.roles == null)
				{
					this.roles = new string[0];
				}
				return this.roles;
			}
			set
			{
				this.roles = value;
			}
		}

		/// <summary>Indicates whether the specified user is a member of any of the roles in the role group.</summary>
		/// <returns>true if the user is a member of one of the roles associated with this role group; otherwise, false.</returns>
		/// <param name="user">The user name to look for in the role group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="User" /> is null.</exception>
		// Token: 0x06002D8D RID: 11661 RVA: 0x00078BF0 File Offset: 0x00076DF0
		public bool ContainsUser(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (this.roles != null)
			{
				foreach (string text in this.roles)
				{
					if (user.IsInRole(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Returns a comma-separated list of the roles associated with this role group.</summary>
		/// <returns>A comma-separated list of the roles associated with this role group.</returns>
		// Token: 0x06002D8E RID: 11662 RVA: 0x00078C38 File Offset: 0x00076E38
		public override string ToString()
		{
			if (this.roles == null || this.roles.Length == 0)
			{
				return string.Empty;
			}
			if (this.roles.Length == 1)
			{
				return this.roles[0];
			}
			return string.Join(",", this.roles);
		}

		// Token: 0x04001B7F RID: 7039
		private ITemplate contentTemplate;

		// Token: 0x04001B80 RID: 7040
		private string[] roles;
	}
}
