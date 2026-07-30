using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> and <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> events of a <see cref="T:System.Windows.Forms.TabControl" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002FA RID: 762
	public class TabControlCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> class. </summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</param>
		/// <param name="tabPageIndex">The zero-based index of <paramref name="tabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</param>
		/// <param name="cancel">true to cancel the tab change by default; otherwise, false.</param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</param>
		// Token: 0x0600330F RID: 13071 RVA: 0x000C2200 File Offset: 0x000C0400
		public TabControlCancelEventArgs(TabPage tabPage, int tabPageIndex, bool cancel, TabControlAction action)
			: base(cancel)
		{
			this.tab_page = tabPage;
			this.tab_page_index = tabPageIndex;
			this.action = action;
		}

		/// <summary>Gets a value indicating which event is occurring. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000C2220 File Offset: 0x000C0420
		public TabControlAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000C2228 File Offset: 0x000C0428
		public TabPage TabPage
		{
			get
			{
				return this.tab_page;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="P:System.Windows.Forms.TabControlCancelEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <returns>The zero-based index of the <see cref="P:System.Windows.Forms.TabControlCancelEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000C2230 File Offset: 0x000C0430
		public int TabPageIndex
		{
			get
			{
				return this.tab_page_index;
			}
		}

		// Token: 0x0400184B RID: 6219
		private TabControlAction action;

		// Token: 0x0400184C RID: 6220
		private TabPage tab_page;

		// Token: 0x0400184D RID: 6221
		private int tab_page_index;
	}
}
