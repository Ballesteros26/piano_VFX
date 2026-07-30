using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events of a designer.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" />  that contains the event data. </param>
	// Token: 0x02000318 RID: 792
	// (Invoke) Token: 0x06001942 RID: 6466
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DesignerTransactionCloseEventHandler(object sender, DesignerTransactionCloseEventArgs e);
}
