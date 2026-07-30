using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a method that can handle events which may or may not require further processing after the event handler has returned.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.HandledEventArgs" /> that contains the event data.</param>
	// Token: 0x02000275 RID: 629
	// (Invoke) Token: 0x06001423 RID: 5155
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void HandledEventHandler(object sender, HandledEventArgs e);
}
