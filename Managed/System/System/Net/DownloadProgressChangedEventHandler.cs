using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> containing event data.</param>
	// Token: 0x020004EC RID: 1260
	// (Invoke) Token: 0x060025C3 RID: 9667
	public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);
}
