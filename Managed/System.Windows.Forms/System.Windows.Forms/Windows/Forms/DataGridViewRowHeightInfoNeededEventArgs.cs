using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	// Token: 0x0200012C RID: 300
	public class DataGridViewRowHeightInfoNeededEventArgs : EventArgs
	{
		// Token: 0x0600155B RID: 5467 RVA: 0x000506FC File Offset: 0x0004E8FC
		internal DataGridViewRowHeightInfoNeededEventArgs(int rowIndex, int height, int minimumHeight)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		/// <summary>Gets or sets the height of the row the event occurred for.</summary>
		/// <returns>The row height. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65,536. </exception>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0005071C File Offset: 0x0004E91C
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x00050724 File Offset: 0x0004E924
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>Gets or sets the minimum height of the row the event occurred for. </summary>
		/// <returns>The minimum row height.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00050730 File Offset: 0x0004E930
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00050738 File Offset: 0x0004E938
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
			set
			{
				this.minimumHeight = value;
			}
		}

		/// <summary>Gets the index of the row associated with this <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs" />.</summary>
		/// <returns>The zero-based index of the row the event occurred for.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00050744 File Offset: 0x0004E944
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private int height;

		// Token: 0x04000C04 RID: 3076
		private int minimumHeight;

		// Token: 0x04000C05 RID: 3077
		private int rowIndex;
	}
}
