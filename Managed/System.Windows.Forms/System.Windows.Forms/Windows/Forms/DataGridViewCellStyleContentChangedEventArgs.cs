using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStyleContentChanged" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F2 RID: 242
	public class DataGridViewCellStyleContentChangedEventArgs : EventArgs
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x00048E74 File Offset: 0x00047074
		internal DataGridViewCellStyleContentChangedEventArgs(DataGridViewCellStyle cellStyle, DataGridViewCellStyleScopes cellStyleScope)
		{
			this.cellStyle = cellStyle;
			this.cellStyleScope = cellStyleScope;
		}

		/// <summary>Gets the changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>The changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00048E8C File Offset: 0x0004708C
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
		}

		/// <summary>Gets the scope that is affected by the changed cell style.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyleScopes" /> that indicates which <see cref="T:System.Windows.Forms.DataGridView" /> entity owns the cell style that changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00048E94 File Offset: 0x00047094
		public DataGridViewCellStyleScopes CellStyleScope
		{
			get
			{
				return this.cellStyleScope;
			}
		}

		// Token: 0x04000B24 RID: 2852
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000B25 RID: 2853
		private DataGridViewCellStyleScopes cellStyleScope;
	}
}
