using System;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreateUserError" /> event.</summary>
	// Token: 0x0200028B RID: 651
	public class CreateUserErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> object that describes the result of a <see cref="Overload:System.Web.Security.Membership.CreateUser" /> attempt.</param>
		// Token: 0x06001A7F RID: 6783 RVA: 0x00045DD3 File Offset: 0x00043FD3
		public CreateUserErrorEventArgs(MembershipCreateStatus s)
		{
			this._error = s;
		}

		/// <summary>Gets or sets a value indicating the result of a <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatingUser" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration values.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00045DE2 File Offset: 0x00043FE2
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00045DEA File Offset: 0x00043FEA
		public MembershipCreateStatus CreateUserError
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}

		// Token: 0x04001692 RID: 5778
		private MembershipCreateStatus _error;
	}
}
