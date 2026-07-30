using System;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms
{
	/// <summary>Manages the list of documents and Web sites the user has visited within the current session.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BC RID: 444
	public sealed class HtmlHistory : IDisposable
	{
		// Token: 0x06001D1F RID: 7455 RVA: 0x0006ED00 File Offset: 0x0006CF00
		internal HtmlHistory(IWebBrowser webHost, IHistory history)
		{
			this.webHost = webHost;
			this.history = history;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0006ED18 File Offset: 0x0006CF18
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.HtmlHistory" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D21 RID: 7457 RVA: 0x0006ED2C File Offset: 0x0006CF2C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the size of the history stack.</summary>
		/// <returns>The current number of entries in the Uniform Resource Locator (URL) history. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0006ED3C File Offset: 0x0006CF3C
		public int Length
		{
			get
			{
				return this.webHost.Navigation.HistoryCount;
			}
		}

		/// <summary>Gets the unmanaged interface wrapped by this class. </summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast into an IOmHistory interface pointer.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0006ED50 File Offset: 0x0006CF50
		[MonoTODO("Not supported, will throw NotSupportedException")]
		public object DomHistory
		{
			get
			{
				throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
			}
		}

		/// <summary>Navigates backward in the navigation stack by the specified number of entries.</summary>
		/// <param name="numberBack"></param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D24 RID: 7460 RVA: 0x0006ED5C File Offset: 0x0006CF5C
		public void Back(int numberBack)
		{
			this.history.Back(numberBack);
		}

		/// <summary>Navigates forward in the navigation stack by the specified number of entries. </summary>
		/// <param name="numberForward"></param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D25 RID: 7461 RVA: 0x0006ED6C File Offset: 0x0006CF6C
		public void Forward(int numberForward)
		{
			this.history.Forward(numberForward);
		}

		/// <summary>Navigates to the specified relative position in the browser's history. </summary>
		/// <param name="relativePosition"></param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D26 RID: 7462 RVA: 0x0006ED7C File Offset: 0x0006CF7C
		public void Go(int relativePosition)
		{
			this.history.GoToIndex(relativePosition);
		}

		/// <summary>Navigates to the specified Uniform Resource Locator (URL). </summary>
		/// <param name="urlString"></param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D27 RID: 7463 RVA: 0x0006ED8C File Offset: 0x0006CF8C
		public void Go(string urlString)
		{
			this.history.GoToUrl(urlString);
		}

		/// <summary>Navigates to the specified Uniform Resource Locator (URL). </summary>
		/// <param name="url">The URL as a <see cref="T:System.Uri" /> object.</param>
		// Token: 0x06001D28 RID: 7464 RVA: 0x0006ED9C File Offset: 0x0006CF9C
		public void Go(Uri url)
		{
			this.history.GoToUrl(url.ToString());
		}

		// Token: 0x04000F84 RID: 3972
		private bool disposed;

		// Token: 0x04000F85 RID: 3973
		private IWebBrowser webHost;

		// Token: 0x04000F86 RID: 3974
		private IHistory history;
	}
}
