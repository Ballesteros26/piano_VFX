using System;

namespace System.Web.Mail
{
	/// <summary>Specifies the priority level for the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F9 RID: 249
	[Obsolete("The recommended alternative is System.Net.Mail.MailPriority. http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum MailPriority
	{
		/// <summary>Specifies that the e-mail message has normal priority.</summary>
		// Token: 0x04001146 RID: 4422
		Normal,
		/// <summary>Specifies that the e-mail message has low priority.</summary>
		// Token: 0x04001147 RID: 4423
		Low,
		/// <summary>Specifies that the e-mail message has high priority.</summary>
		// Token: 0x04001148 RID: 4424
		High
	}
}
