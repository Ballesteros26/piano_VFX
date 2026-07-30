using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the SendMailError event of controls such as the <see cref="T:System.Web.UI.WebControls.ChangePassword" /> control, the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control, and the <see cref="T:System.Web.UI.WebControls.PasswordRecovery" /> control. </summary>
	// Token: 0x02000406 RID: 1030
	public class SendMailErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> class. </summary>
		/// <param name="e">An <see cref="T:System.Exception" /> object containing the exception.</param>
		// Token: 0x06002DAF RID: 11695 RVA: 0x00078FD5 File Offset: 0x000771D5
		public SendMailErrorEventArgs(Exception e)
		{
			this.exception = e;
			this.exceptionHandled = true;
		}

		/// <summary>Returns the exception thrown by an SMTP mail service when an e-mail message cannot be sent.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object that contains the exception.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x00078FEB File Offset: 0x000771EB
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x00078FF3 File Offset: 0x000771F3
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		/// <summary>Indicates if the SMTP exception that is contained in the <see cref="P:System.Web.UI.WebControls.SendMailErrorEventArgs.Exception" /> property has been handled.</summary>
		/// <returns>If true, the exception is consumed and handled by the <see cref="T:System.Web.UI.WebControls.SendMailErrorEventHandler" /> delegate. If false, the exception is rethrown, including the original call stack and error message.The default is false.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00078FFC File Offset: 0x000771FC
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x00079004 File Offset: 0x00077204
		public bool Handled
		{
			get
			{
				return this.exceptionHandled;
			}
			set
			{
				this.exceptionHandled = value;
			}
		}

		// Token: 0x04001B83 RID: 7043
		private Exception exception;

		// Token: 0x04001B84 RID: 7044
		private bool exceptionHandled;
	}
}
