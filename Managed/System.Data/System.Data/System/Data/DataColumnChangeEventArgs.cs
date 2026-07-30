using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000063 RID: 99
	public class DataColumnChangeEventArgs : EventArgs
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00012D25 File Offset: 0x00010F25
		internal DataColumnChangeEventArgs(DataRow row)
		{
			this.Row = row;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumnChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> of the column with the changing value. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> with the changing value. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x060003B4 RID: 948 RVA: 0x00012D34 File Offset: 0x00010F34
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this.Row = row;
			this._column = column;
			this.ProposedValue = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00012D51 File Offset: 0x00010F51
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> of the column with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> of the column with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00012D59 File Offset: 0x00010F59
		public DataRow Row { get; }

		/// <summary>Gets or sets the proposed new value for the column.</summary>
		/// <returns>The proposed value, of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00012D61 File Offset: 0x00010F61
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00012D69 File Offset: 0x00010F69
		public object ProposedValue { get; set; }

		// Token: 0x060003B9 RID: 953 RVA: 0x00012D72 File Offset: 0x00010F72
		internal void InitializeColumnChangeEvent(DataColumn column, object value)
		{
			this._column = column;
			this.ProposedValue = value;
		}

		// Token: 0x04000540 RID: 1344
		private DataColumn _column;
	}
}
