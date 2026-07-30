using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003B0 RID: 944
	public class WebBrowserNavigatingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> class.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating. </param>
		/// <param name="targetFrameName">The name of the Web page frame in which the new document will be loaded. </param>
		// Token: 0x06004525 RID: 17701 RVA: 0x0010D8D4 File Offset: 0x0010BAD4
		public WebBrowserNavigatingEventArgs(Uri url, string targetFrameName)
		{
			this.url = url;
			this.target_frame_name = targetFrameName;
		}

		/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0010D8EC File Offset: 0x0010BAEC
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the name of the Web page frame in which the new document will be loaded.</summary>
		/// <returns>The name of the frame in which the new document will be loaded.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x0010D8F4 File Offset: 0x0010BAF4
		public string TargetFrameName
		{
			get
			{
				return this.target_frame_name;
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private Uri url;

		// Token: 0x04001CC1 RID: 7361
		private string target_frame_name;
	}
}
