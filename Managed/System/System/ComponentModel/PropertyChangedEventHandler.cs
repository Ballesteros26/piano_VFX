using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event raised when a property is changed on a component.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
	// Token: 0x020002BE RID: 702
	// (Invoke) Token: 0x06001606 RID: 5638
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
