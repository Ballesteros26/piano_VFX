using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event. </summary>
	// Token: 0x0200008D RID: 141
	public class ColumnReorderedEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> class. </summary>
		/// <param name="oldDisplayIndex">The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="newDisplayIndex">The new display position for the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</param>
		// Token: 0x0600065E RID: 1630 RVA: 0x0001D754 File Offset: 0x0001B954
		public ColumnReorderedEventArgs(int oldDisplayIndex, int newDisplayIndex, ColumnHeader header)
		{
			this.old_display_index = oldDisplayIndex;
			this.new_display_index = newDisplayIndex;
			this.header = header;
		}

		/// <summary>Gets the previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001D774 File Offset: 0x0001B974
		public int OldDisplayIndex
		{
			get
			{
				return this.old_display_index;
			}
		}

		/// <summary>Gets the new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></summary>
		/// <returns>The new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001D77C File Offset: 0x0001B97C
		public int NewDisplayIndex
		{
			get
			{
				return this.new_display_index;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001D784 File Offset: 0x0001B984
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x0400073A RID: 1850
		private ColumnHeader header;

		// Token: 0x0400073B RID: 1851
		private int new_display_index;

		// Token: 0x0400073C RID: 1852
		private int old_display_index;
	}
}
