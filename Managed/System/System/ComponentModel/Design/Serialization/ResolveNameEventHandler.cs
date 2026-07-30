using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event of a serialization manager.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" />  that contains the event data.</param>
	// Token: 0x02000359 RID: 857
	// (Invoke) Token: 0x06001A9C RID: 6812
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void ResolveNameEventHandler(object sender, ResolveNameEventArgs e);
}
