using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event. </summary>
	// Token: 0x020006CB RID: 1739
	public class WebPartDisplayModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> class. </summary>
		/// <param name="oldDisplayMode">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> applied to the Web Parts control before the display mode is changed.  </param>
		// Token: 0x06004A06 RID: 18950 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDisplayModeEventArgs(WebPartDisplayMode oldDisplayMode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the former display mode for a Web Part control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> applied to a Web Parts control before the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event.</returns>
		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A08 RID: 18952 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDisplayMode OldDisplayMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
