using System;

namespace System.Net.NetworkInformation
{
	/// <summary>References one or more methods to be called when the address of a network interface changes.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains data about the event. </param>
	// Token: 0x0200060A RID: 1546
	// (Invoke) Token: 0x0600318D RID: 12685
	public delegate void NetworkAddressChangedEventHandler(object sender, EventArgs e);
}
