using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000339 RID: 825
	public class ToolBarButtonClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> class.</summary>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked. </param>
		// Token: 0x060039BD RID: 14781 RVA: 0x000ED62C File Offset: 0x000EB82C
		public ToolBarButtonClickEventArgs(ToolBarButton button)
		{
			this.button = button;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000ED63C File Offset: 0x000EB83C
		// (set) Token: 0x060039BF RID: 14783 RVA: 0x000ED644 File Offset: 0x000EB844
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
			set
			{
				this.button = value;
			}
		}

		// Token: 0x04001A0B RID: 6667
		private ToolBarButton button;
	}
}
