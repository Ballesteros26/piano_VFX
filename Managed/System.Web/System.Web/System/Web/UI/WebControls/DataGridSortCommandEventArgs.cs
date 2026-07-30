using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.SortCommand" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000297 RID: 663
	public class DataGridSortCommandEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridSortCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="dce">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A9A RID: 6810 RVA: 0x00045E57 File Offset: 0x00044057
		public DataGridSortCommandEventArgs(object commandSource, DataGridCommandEventArgs dce)
		{
			this.commandSource = commandSource;
			this.sortExpression = (string)dce.CommandArgument;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00045E77 File Offset: 0x00044077
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		/// <summary>Gets the expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00045E7F File Offset: 0x0004407F
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x040016AD RID: 5805
		private string sortExpression;

		// Token: 0x040016AE RID: 5806
		private object commandSource;
	}
}
