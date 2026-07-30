using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> containing event data.</param>
	// Token: 0x020004E2 RID: 1250
	// (Invoke) Token: 0x060025A0 RID: 9632
	public delegate void DownloadDataCompletedEventHandler(object sender, DownloadDataCompletedEventArgs e);
}
