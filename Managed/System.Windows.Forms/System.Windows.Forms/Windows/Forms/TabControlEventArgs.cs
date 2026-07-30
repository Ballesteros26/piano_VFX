using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TabControl.Selected" /> and <see cref="E:System.Windows.Forms.TabControl.Deselected" /> events of a <see cref="T:System.Windows.Forms.TabControl" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002FB RID: 763
	public class TabControlEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControlEventArgs" /> class. </summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</param>
		/// <param name="tabPageIndex">The zero-based index of <paramref name="tabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</param>
		// Token: 0x06003313 RID: 13075 RVA: 0x000C2238 File Offset: 0x000C0438
		public TabControlEventArgs(TabPage tabPage, int tabPageIndex, TabControlAction action)
		{
			this.tab_page = tabPage;
			this.tab_page_index = tabPageIndex;
			this.action = action;
		}

		/// <summary>Gets a value indicating which event is occurring. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000C2258 File Offset: 0x000C0458
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
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000C2260 File Offset: 0x000C0460
		public TabPage TabPage
		{
			get
			{
				return this.tab_page;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <returns>The zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000C2268 File Offset: 0x000C0468
		public int TabPageIndex
		{
			get
			{
				return this.tab_page_index;
			}
		}

		// Token: 0x0400184E RID: 6222
		private TabControlAction action;

		// Token: 0x0400184F RID: 6223
		private TabPage tab_page;

		// Token: 0x04001850 RID: 6224
		private int tab_page_index;
	}
}
