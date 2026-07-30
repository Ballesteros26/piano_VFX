using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCommand" /> event.</summary>
	// Token: 0x020002BD RID: 701
	public class GridViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified row, source of the command, and event arguments.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row containing the button.</param>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
		// Token: 0x06001B12 RID: 6930 RVA: 0x00045FA5 File Offset: 0x000441A5
		public GridViewCommandEventArgs(GridViewRow row, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._row = row;
			this._commandSource = commandSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified source of the command and event arguments.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
		// Token: 0x06001B13 RID: 6931 RVA: 0x00045FBC File Offset: 0x000441BC
		public GridViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>A instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00045FCC File Offset: 0x000441CC
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished; otherwise, false.</returns>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00045FD4 File Offset: 0x000441D4
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x00045FDC File Offset: 0x000441DC
		public bool Handled { get; set; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x00045FE5 File Offset: 0x000441E5
		internal GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x040016E3 RID: 5859
		private GridViewRow _row;

		// Token: 0x040016E4 RID: 5860
		private object _commandSource;
	}
}
