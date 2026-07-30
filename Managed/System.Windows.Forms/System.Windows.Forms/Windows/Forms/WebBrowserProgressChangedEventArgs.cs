using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003B1 RID: 945
	public class WebBrowserProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> class.</summary>
		/// <param name="currentProgress">The number of bytes that are loaded already. </param>
		/// <param name="maximumProgress">The total number of bytes to be loaded. </param>
		// Token: 0x06004528 RID: 17704 RVA: 0x0010D8FC File Offset: 0x0010BAFC
		public WebBrowserProgressChangedEventArgs(long currentProgress, long maximumProgress)
		{
			this.current_progress = currentProgress;
			this.maximum_progress = maximumProgress;
		}

		/// <summary>Gets the number of bytes that have been downloaded.</summary>
		/// <returns>The number of bytes that have been loaded or -1 to indicate that the download has completed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x0010D914 File Offset: 0x0010BB14
		public long CurrentProgress
		{
			get
			{
				return this.current_progress;
			}
		}

		/// <summary>Gets the total number of bytes in the document being loaded.</summary>
		/// <returns>The total number of bytes to be loaded.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x0010D91C File Offset: 0x0010BB1C
		public long MaximumProgress
		{
			get
			{
				return this.maximum_progress;
			}
		}

		// Token: 0x04001CC2 RID: 7362
		private long current_progress;

		// Token: 0x04001CC3 RID: 7363
		private long maximum_progress;
	}
}
