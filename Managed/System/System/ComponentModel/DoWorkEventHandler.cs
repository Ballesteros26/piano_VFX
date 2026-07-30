using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.DoWorkEventArgs" />    that contains the event data.</param>
	// Token: 0x02000266 RID: 614
	// (Invoke) Token: 0x060013A4 RID: 5028
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
}
