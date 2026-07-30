using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.Navigated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003AF RID: 943
	public class WebBrowserNavigatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserNavigatedEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated. </param>
		// Token: 0x06004523 RID: 17699 RVA: 0x0010D8BC File Offset: 0x0010BABC
		public WebBrowserNavigatedEventArgs(Uri url)
		{
			this.url = url;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0010D8CC File Offset: 0x0010BACC
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04001CBF RID: 7359
		private Uri url;
	}
}
