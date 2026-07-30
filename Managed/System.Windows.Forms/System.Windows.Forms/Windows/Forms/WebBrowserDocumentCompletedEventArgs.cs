using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003AD RID: 941
	public class WebBrowserDocumentCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document that was loaded. </param>
		// Token: 0x06004521 RID: 17697 RVA: 0x0010D8A4 File Offset: 0x0010BAA4
		public WebBrowserDocumentCompletedEventArgs(Uri url)
		{
			this.url = url;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document that was loaded.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x0010D8B4 File Offset: 0x0010BAB4
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04001CB6 RID: 7350
		private Uri url;
	}
}
