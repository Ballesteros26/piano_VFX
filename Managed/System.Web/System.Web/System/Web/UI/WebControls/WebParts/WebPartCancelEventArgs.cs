using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" /> event, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event. </summary>
	// Token: 0x02000475 RID: 1141
	public class WebPartCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
		// Token: 0x0600342A RID: 13354 RVA: 0x0008A92E File Offset: 0x00088B2E
		public WebPartCancelEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		/// <summary>Gets the Web Parts control involved in the cancelable event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event.</returns>
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x0008A93D File Offset: 0x00088B3D
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x0008A945 File Offset: 0x00088B45
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
			set
			{
				this._webPart = value;
			}
		}

		// Token: 0x04001CF4 RID: 7412
		private WebPart _webPart;
	}
}
