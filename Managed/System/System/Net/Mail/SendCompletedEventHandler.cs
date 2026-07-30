using System;
using System.ComponentModel;

namespace System.Net.Mail
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> containing event data.</param>
	// Token: 0x02000582 RID: 1410
	// (Invoke) Token: 0x06002BE3 RID: 11235
	public delegate void SendCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
