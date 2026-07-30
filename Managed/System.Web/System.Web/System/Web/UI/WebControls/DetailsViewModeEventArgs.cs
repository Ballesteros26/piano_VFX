using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event.</summary>
	// Token: 0x020002A6 RID: 678
	public class DetailsViewModeEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewModeEventArgs" /> class.</summary>
		/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</param>
		/// <param name="cancelingEdit">true to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</param>
		// Token: 0x06001ACA RID: 6858 RVA: 0x00045EEE File Offset: 0x000440EE
		public DetailsViewModeEventArgs(DetailsViewMode mode, bool cancelingEdit)
			: base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
		/// <returns>true to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</returns>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00045F05 File Offset: 0x00044105
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is changing.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</returns>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00045F0D File Offset: 0x0004410D
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00045F15 File Offset: 0x00044115
		public DetailsViewMode NewMode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		// Token: 0x040016BE RID: 5822
		private DetailsViewMode _mode;

		// Token: 0x040016BF RID: 5823
		private bool _cancelingEdit;
	}
}
