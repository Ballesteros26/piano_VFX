using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> that contains event data.</param>
	// Token: 0x020004E0 RID: 1248
	// (Invoke) Token: 0x06002599 RID: 9625
	public delegate void DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs e);
}
