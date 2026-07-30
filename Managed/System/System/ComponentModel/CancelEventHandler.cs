using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that handles a cancelable event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	// Token: 0x0200023C RID: 572
	// (Invoke) Token: 0x06001291 RID: 4753
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void CancelEventHandler(object sender, CancelEventArgs e);
}
