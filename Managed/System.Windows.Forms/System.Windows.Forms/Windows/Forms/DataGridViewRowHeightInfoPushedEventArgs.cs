using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoPushed" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	// Token: 0x0200012D RID: 301
	public class DataGridViewRowHeightInfoPushedEventArgs : HandledEventArgs
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x0005074C File Offset: 0x0004E94C
		internal DataGridViewRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		/// <summary>Gets the height of the row the event occurred for.</summary>
		/// <returns>The row height, in pixels.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0005076C File Offset: 0x0004E96C
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		/// <summary>Gets the minimum height of the row the event occurred for.</summary>
		/// <returns>The minimum row height, in pixels.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00050774 File Offset: 0x0004E974
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
		}

		/// <summary>Gets the index of the row the event occurred for.</summary>
		/// <returns>The zero-based index of the row.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005077C File Offset: 0x0004E97C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000C06 RID: 3078
		private int height;

		// Token: 0x04000C07 RID: 3079
		private int minimumHeight;

		// Token: 0x04000C08 RID: 3080
		private int rowIndex;
	}
}
