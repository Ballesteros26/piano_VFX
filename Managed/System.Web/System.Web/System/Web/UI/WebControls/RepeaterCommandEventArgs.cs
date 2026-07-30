using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event of a <see cref="T:System.Web.UI.WebControls.Repeater" />. This class cannot be inherited.</summary>
	// Token: 0x02000300 RID: 768
	public class RepeaterCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" />. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.Item" /> property is set to this value. </param>
		/// <param name="commandSource">The command source. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.CommandSource" /> property is set to this value. </param>
		/// <param name="originalArgs">The original event arguments. </param>
		// Token: 0x06001BDC RID: 7132 RVA: 0x00046259 File Offset: 0x00044459
		public RepeaterCommandEventArgs(RepeaterItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00046270 File Offset: 0x00044470
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The command source.</returns>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00046278 File Offset: 0x00044478
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x04001750 RID: 5968
		private RepeaterItem item;

		// Token: 0x04001751 RID: 5969
		private object commandSource;
	}
}
