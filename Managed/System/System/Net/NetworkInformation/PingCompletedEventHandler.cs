using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event of a <see cref="T:System.Net.NetworkInformation.Ping" /> object.</summary>
	/// <param name="sender">The source of the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</param>
	/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" />  object that contains the event data.</param>
	// Token: 0x0200066E RID: 1646
	// (Invoke) Token: 0x06003451 RID: 13393
	public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);
}
