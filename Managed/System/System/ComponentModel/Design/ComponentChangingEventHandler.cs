using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> event that contains the event data. </param>
	// Token: 0x0200030C RID: 780
	// (Invoke) Token: 0x060018E6 RID: 6374
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ComponentChangingEventHandler(object sender, ComponentChangingEventArgs e);
}
