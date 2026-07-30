using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event.</summary>
	// Token: 0x020002B4 RID: 692
	public class FormViewModeEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewModeEventArgs" /> class.</summary>
		/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</param>
		/// <param name="cancelingEdit">true to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x00045F3E File Offset: 0x0004413E
		public FormViewModeEventArgs(FormViewMode mode, bool cancelingEdit)
			: base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
		/// <returns>true to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, false.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x00045F55 File Offset: 0x00044155
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control is changing.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00045F5D File Offset: 0x0004415D
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00045F65 File Offset: 0x00044165
		public FormViewMode NewMode
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

		// Token: 0x040016DA RID: 5850
		private FormViewMode _mode;

		// Token: 0x040016DB RID: 5851
		private bool _cancelingEdit;
	}
}
