using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.Common.DataAdapter.FillError" /> event of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A4 RID: 164
	public class FillErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.FillErrorEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> being updated. </param>
		/// <param name="values">The values for the row being updated. </param>
		// Token: 0x06000A0A RID: 2570 RVA: 0x0002D127 File Offset: 0x0002B327
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this._dataTable = dataTable;
			this._values = values;
			if (this._values == null)
			{
				this._values = Array.Empty<object>();
			}
		}

		/// <summary>Gets or sets a value indicating whether to continue the fill operation despite the error.</summary>
		/// <returns>true if the fill operation should continue; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002D150 File Offset: 0x0002B350
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002D158 File Offset: 0x0002B358
		public bool Continue
		{
			get
			{
				return this._continueFlag;
			}
			set
			{
				this._continueFlag = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> being updated when the error occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002D161 File Offset: 0x0002B361
		public DataTable DataTable
		{
			get
			{
				return this._dataTable;
			}
		}

		/// <summary>Gets the errors being handled.</summary>
		/// <returns>The errors being handled.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002D169 File Offset: 0x0002B369
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002D171 File Offset: 0x0002B371
		public Exception Errors
		{
			get
			{
				return this._errors;
			}
			set
			{
				this._errors = value;
			}
		}

		/// <summary>Gets the values for the row being updated when the error occurred.</summary>
		/// <returns>The values for the row being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002D17C File Offset: 0x0002B37C
		public object[] Values
		{
			get
			{
				object[] array = new object[this._values.Length];
				for (int i = 0; i < this._values.Length; i++)
				{
					array[i] = this._values[i];
				}
				return array;
			}
		}

		// Token: 0x040006A4 RID: 1700
		private bool _continueFlag;

		// Token: 0x040006A5 RID: 1701
		private DataTable _dataTable;

		// Token: 0x040006A6 RID: 1702
		private Exception _errors;

		// Token: 0x040006A7 RID: 1703
		private object[] _values;
	}
}
