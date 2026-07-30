using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.SortCompare" /> event.</summary>
	// Token: 0x02000137 RID: 311
	public class DataGridViewSortCompareEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs" /> class.</summary>
		/// <param name="dataGridViewColumn">The column to sort.</param>
		/// <param name="cellValue1">The value of the first cell to compare.</param>
		/// <param name="cellValue2">The value of the second cell to compare.</param>
		/// <param name="rowIndex1">The index of the row containing the first cell.</param>
		/// <param name="rowIndex2">The index of the row containing the second cell.</param>
		// Token: 0x060015D2 RID: 5586 RVA: 0x00051104 File Offset: 0x0004F304
		public DataGridViewSortCompareEventArgs(DataGridViewColumn dataGridViewColumn, object cellValue1, object cellValue2, int rowIndex1, int rowIndex2)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.cellValue1 = cellValue1;
			this.cellValue2 = cellValue2;
			this.rowIndex1 = rowIndex1;
			this.rowIndex2 = rowIndex2;
		}

		/// <summary>Gets the value of the first cell to compare.</summary>
		/// <returns>The value of the first cell.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00051134 File Offset: 0x0004F334
		public object CellValue1
		{
			get
			{
				return this.cellValue1;
			}
		}

		/// <summary>Gets the value of the second cell to compare.</summary>
		/// <returns>The value of the second cell.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0005113C File Offset: 0x0004F33C
		public object CellValue2
		{
			get
			{
				return this.cellValue2;
			}
		}

		/// <summary>Gets the column being sorted. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to sort.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00051144 File Offset: 0x0004F344
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the index of the row containing the first cell to compare.</summary>
		/// <returns>The index of the row containing the second cell.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0005114C File Offset: 0x0004F34C
		public int RowIndex1
		{
			get
			{
				return this.rowIndex1;
			}
		}

		/// <summary>Gets the index of the row containing the second cell to compare.</summary>
		/// <returns>The index of the row containing the second cell.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00051154 File Offset: 0x0004F354
		public int RowIndex2
		{
			get
			{
				return this.rowIndex2;
			}
		}

		/// <summary>Gets or sets a value indicating the order in which the compared cells will be sorted.</summary>
		/// <returns>Less than zero if the first cell will be sorted before the second cell; zero if the first cell and second cell have equivalent values; greater than zero if the second cell will be sorted before the first cell.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0005115C File Offset: 0x0004F35C
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00051164 File Offset: 0x0004F364
		public int SortResult
		{
			get
			{
				return this.sortResult;
			}
			set
			{
				this.sortResult = value;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000C2C RID: 3116
		private object cellValue1;

		// Token: 0x04000C2D RID: 3117
		private object cellValue2;

		// Token: 0x04000C2E RID: 3118
		private int rowIndex1;

		// Token: 0x04000C2F RID: 3119
		private int rowIndex2;

		// Token: 0x04000C30 RID: 3120
		private int sortResult;
	}
}
