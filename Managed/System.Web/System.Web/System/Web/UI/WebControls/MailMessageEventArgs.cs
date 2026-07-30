using System;
using System.Net.Mail;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for e-mail–related events.</summary>
	// Token: 0x020002E5 RID: 741
	public class MailMessageEventArgs : LoginCancelEventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> class.</summary>
		/// <param name="message">The <see cref="T:System.Net.Mail.MailMessage" /> containing the message.</param>
		// Token: 0x06001B9A RID: 7066 RVA: 0x000460EB File Offset: 0x000442EB
		public MailMessageEventArgs(MailMessage message)
		{
			this._message = message;
		}

		/// <summary>Gets the e-mail message contents.</summary>
		/// <returns>A <see cref="T:System.Web.Mail.MailMessage" /> containing the message contents.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x000460FA File Offset: 0x000442FA
		public MailMessage Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0400171E RID: 5918
		private MailMessage _message;
	}
}
