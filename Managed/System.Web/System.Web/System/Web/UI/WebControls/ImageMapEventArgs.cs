using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ImageMap.Click" /> event of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
	// Token: 0x020002DB RID: 731
	public class ImageMapEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageMapEventArgs" /> class.</summary>
		/// <param name="value">The <see cref="T:System.String" /> object assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked. </param>
		// Token: 0x06001B8C RID: 7052 RVA: 0x000460AB File Offset: 0x000442AB
		public ImageMapEventArgs(string value)
		{
			this._postBackValue = value;
		}

		/// <summary>Gets the <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</summary>
		/// <returns>The <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</returns>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x000460BA File Offset: 0x000442BA
		public string PostBackValue
		{
			get
			{
				return this._postBackValue;
			}
		}

		// Token: 0x04001702 RID: 5890
		private string _postBackValue;
	}
}
