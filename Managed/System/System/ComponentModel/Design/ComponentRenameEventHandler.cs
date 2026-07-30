using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> that contains the event data. </param>
	// Token: 0x02000310 RID: 784
	// (Invoke) Token: 0x060018F4 RID: 6388
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ComponentRenameEventHandler(object sender, ComponentRenameEventArgs e);
}
