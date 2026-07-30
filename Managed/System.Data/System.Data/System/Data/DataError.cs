using System;

namespace System.Data
{
	// Token: 0x02000067 RID: 103
	internal sealed class DataError
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x000140D3 File Offset: 0x000122D3
		internal DataError()
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000140E6 File Offset: 0x000122E6
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00014100 File Offset: 0x00012300
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00014108 File Offset: 0x00012308
		internal string Text
		{
			get
			{
				return this._rowError;
			}
			set
			{
				this.SetText(value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00014111 File Offset: 0x00012311
		internal bool HasErrors
		{
			get
			{
				return this._rowError.Length != 0 || this._count != 0;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001412C File Offset: 0x0001232C
		internal void SetColumnError(DataColumn column, string error)
		{
			if (error == null || error.Length == 0)
			{
				this.Clear(column);
				return;
			}
			if (this._errorList == null)
			{
				this._errorList = new DataError.ColumnError[1];
			}
			int num = this.IndexOf(column);
			this._errorList[num]._column = column;
			this._errorList[num]._error = error;
			column._errors++;
			if (num == this._count)
			{
				this._count++;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000141B4 File Offset: 0x000123B4
		internal string GetColumnError(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return this._errorList[i]._error;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00014200 File Offset: 0x00012400
		internal void Clear(DataColumn column)
		{
			if (this._count == 0)
			{
				return;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					Array.Copy(this._errorList, i + 1, this._errorList, i, this._count - i - 1);
					this._count--;
					column._errors--;
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014278 File Offset: 0x00012478
		internal void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._errorList[i]._column._errors--;
			}
			this._count = 0;
			this._rowError = string.Empty;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000142C8 File Offset: 0x000124C8
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this._count];
			for (int i = 0; i < this._count; i++)
			{
				array[i] = this._errorList[i]._column;
			}
			return array;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014307 File Offset: 0x00012507
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this._rowError = errorText;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001431C File Offset: 0x0001251C
		internal int IndexOf(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return i;
				}
			}
			if (this._count >= this._errorList.Length)
			{
				DataError.ColumnError[] array = new DataError.ColumnError[Math.Min(this._count * 2, column.Table.Columns.Count)];
				Array.Copy(this._errorList, 0, array, 0, this._count);
				this._errorList = array;
			}
			return this._count;
		}

		// Token: 0x04000550 RID: 1360
		private string _rowError = string.Empty;

		// Token: 0x04000551 RID: 1361
		private int _count;

		// Token: 0x04000552 RID: 1362
		private DataError.ColumnError[] _errorList;

		// Token: 0x04000553 RID: 1363
		internal const int initialCapacity = 1;

		// Token: 0x02000068 RID: 104
		internal struct ColumnError
		{
			// Token: 0x04000554 RID: 1364
			internal DataColumn _column;

			// Token: 0x04000555 RID: 1365
			internal string _error;
		}
	}
}
