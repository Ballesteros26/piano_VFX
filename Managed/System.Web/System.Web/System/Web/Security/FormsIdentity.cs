using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using Unity;

namespace System.Web.Security
{
	/// <summary>Represents a user identity authenticated using forms authentication. This class cannot be inherited.</summary>
	// Token: 0x020004C1 RID: 1217
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public class FormsIdentity : IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsIdentity" /> class.</summary>
		/// <param name="ticket">The authentication ticket upon which this identity is based. </param>
		// Token: 0x060036CA RID: 14026 RVA: 0x0008FA7C File Offset: 0x0008DC7C
		public FormsIdentity(FormsAuthenticationTicket ticket)
		{
			this.ticket = ticket;
		}

		/// <summary>Gets the type of authenticated identity.</summary>
		/// <returns>The type of authenticated identity. This property always returns "Forms".</returns>
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x0008FA8B File Offset: 0x0008DC8B
		public string AuthenticationType
		{
			get
			{
				return "Forms";
			}
		}

		/// <summary>Gets a value that indicates whether authentication took place.</summary>
		/// <returns>This property always returns true.</returns>
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the user name of the forms identity.</summary>
		/// <returns>The user name of the forms identity.</returns>
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x0008FA92 File Offset: 0x0008DC92
		public string Name
		{
			get
			{
				return this.ticket.Name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> for the forms-authentication user identity.</summary>
		/// <returns>The <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> supplied to the <see cref="M:System.Web.Security.FormsIdentity.#ctor(System.Web.Security.FormsAuthenticationTicket)" /> constructor for the current object.</returns>
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x0008FA9F File Offset: 0x0008DC9F
		public FormsAuthenticationTicket Ticket
		{
			get
			{
				return this.ticket;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsIdentity" /> class based on the specified identity.</summary>
		/// <param name="identity">The identity upon which this identity is based.</param>
		// Token: 0x060036CF RID: 14031 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected FormsIdentity(FormsIdentity identity)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the collection of claims that are associated with this identity.</summary>
		/// <returns>The collection of claims.</returns>
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a copy of the current <see cref="T:System.Web.Security.FormsIdentity" /> instance.</summary>
		/// <returns>A copy of the current <see cref="T:System.Web.Security.FormsIdentity" /> instance.</returns>
		// Token: 0x060036D1 RID: 14033 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ClaimsIdentity Clone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001DDB RID: 7643
		private FormsAuthenticationTicket ticket;
	}
}
