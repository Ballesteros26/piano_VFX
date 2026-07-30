using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemDataBound" /> events.</summary>
	// Token: 0x02000308 RID: 776
	public class SiteMapNodeItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object as the source of the event.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event. </param>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x000462C6 File Offset: 0x000444C6
		public SiteMapNodeItemEventArgs(SiteMapNodeItem item)
		{
			this._item = item;
		}

		/// <summary>Gets the node item that is the source of the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event.</returns>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x000462D5 File Offset: 0x000444D5
		public SiteMapNodeItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0400175B RID: 5979
		private SiteMapNodeItem _item;
	}
}
