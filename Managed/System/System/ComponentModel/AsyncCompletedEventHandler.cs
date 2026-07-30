using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the MethodNameCompleted event of an asynchronous operation.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data. </param>
	// Token: 0x0200022B RID: 555
	// (Invoke) Token: 0x060011EB RID: 4587
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void AsyncCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
