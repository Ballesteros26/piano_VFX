using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemCommand" /> event.</summary>
	// Token: 0x0200029F RID: 671
	public class DetailsViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
		// Token: 0x06001AB2 RID: 6834 RVA: 0x00045EC5 File Offset: 0x000440C5
		public DetailsViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>An instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00045ED5 File Offset: 0x000440D5
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished running; otherwise, false.</returns>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00045EDD File Offset: 0x000440DD
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00045EE5 File Offset: 0x000440E5
		public bool Handled { get; set; }

		// Token: 0x040016B8 RID: 5816
		private object _commandSource;
	}
}
