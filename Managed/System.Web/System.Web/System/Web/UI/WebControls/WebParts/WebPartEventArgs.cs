using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> events. </summary>
	// Token: 0x02000477 RID: 1143
	public class WebPartEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
		// Token: 0x06003431 RID: 13361 RVA: 0x0008A94E File Offset: 0x00088B4E
		public WebPartEventArgs(WebPart webPart)
		{
			this._webPart = webPart;
		}

		/// <summary>Gets the Web Parts control involved in the event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event.</returns>
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x0008A95D File Offset: 0x00088B5D
		public WebPart WebPart
		{
			get
			{
				return this._webPart;
			}
		}

		// Token: 0x04001CF5 RID: 7413
		private WebPart _webPart;
	}
}
