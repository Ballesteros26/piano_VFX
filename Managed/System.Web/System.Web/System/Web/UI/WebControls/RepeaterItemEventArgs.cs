using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.Repeater.ItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.Repeater" />.</summary>
	// Token: 0x02000302 RID: 770
	public class RepeaterItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> class.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event. The <see cref="P:System.Web.UI.WebControls.RepeaterItemEventArgs.Item" /> property is set to this value. </param>
		// Token: 0x06001BE3 RID: 7139 RVA: 0x00046280 File Offset: 0x00044480
		public RepeaterItemEventArgs(RepeaterItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</returns>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0004628F File Offset: 0x0004448F
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001752 RID: 5970
		private RepeaterItem item;
	}
}
