using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the interfaces of an ActiveX site for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class.</summary>
	// Token: 0x020003B4 RID: 948
	[MonoTODO("Needs Implementation")]
	[ComVisible(true)]
	public class WebBrowserSiteBase : IDisposable
	{
		// Token: 0x0600452B RID: 17707 RVA: 0x0010D924 File Offset: 0x0010BB24
		internal WebBrowserSiteBase()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" />. </summary>
		// Token: 0x0600452C RID: 17708 RVA: 0x0010D92C File Offset: 0x0010BB2C
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600452D RID: 17709 RVA: 0x0010D930 File Offset: 0x0010BB30
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
