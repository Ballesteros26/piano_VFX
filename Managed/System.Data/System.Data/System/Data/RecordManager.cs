using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x020000E5 RID: 229
	internal sealed class RecordManager
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x00038498 File Offset: 0x00036698
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this._table = table;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000384C8 File Offset: 0x000366C8
		private void GrowRecordCapacity()
		{
			this.RecordCapacity = ((RecordManager.NewCapacity(this._recordCapacity) < this.NormalizedMinimumCapacity(this._minimumCapacity)) ? this.NormalizedMinimumCapacity(this._minimumCapacity) : RecordManager.NewCapacity(this._recordCapacity));
			DataRow[] array = this._table.NewRowArray(this._recordCapacity);
			if (this._rows != null)
			{
				Array.Copy(this._rows, 0, array, 0, Math.Min(this._lastFreeRecord, this._rows.Length));
			}
			this._rows = array;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0003854F File Offset: 0x0003674F
		internal int LastFreeRecord
		{
			get
			{
				return this._lastFreeRecord;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00038557 File Offset: 0x00036757
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0003855F File Offset: 0x0003675F
		internal int MinimumCapacity
		{
			get
			{
				return this._minimumCapacity;
			}
			set
			{
				if (this._minimumCapacity != value)
				{
					if (value < 0)
					{
						throw ExceptionBuilder.NegativeMinimumCapacity();
					}
					this._minimumCapacity = value;
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0003857B File Offset: 0x0003677B
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00038584 File Offset: 0x00036784
		internal int RecordCapacity
		{
			get
			{
				return this._recordCapacity;
			}
			set
			{
				if (this._recordCapacity != value)
				{
					for (int i = 0; i < this._table.Columns.Count; i++)
					{
						this._table.Columns[i].SetCapacity(value);
					}
					this._recordCapacity = value;
				}
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000385D3 File Offset: 0x000367D3
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000385E6 File Offset: 0x000367E6
		private int NormalizedMinimumCapacity(int capacity)
		{
			if (capacity >= 1014)
			{
				return (capacity + 10 >> 10) + 1 << 10;
			}
			if (capacity >= 246)
			{
				return 1024;
			}
			if (capacity < 54)
			{
				return 64;
			}
			return 256;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00038618 File Offset: 0x00036818
		internal int NewRecordBase()
		{
			int num;
			if (this._freeRecordList.Count != 0)
			{
				num = this._freeRecordList[this._freeRecordList.Count - 1];
				this._freeRecordList.RemoveAt(this._freeRecordList.Count - 1);
			}
			else
			{
				if (this._lastFreeRecord >= this._recordCapacity)
				{
					this.GrowRecordCapacity();
				}
				num = this._lastFreeRecord;
				this._lastFreeRecord++;
			}
			return num;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00038690 File Offset: 0x00036890
		internal void FreeRecord(ref int record)
		{
			if (-1 != record)
			{
				this[record] = null;
				int count = this._table._columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					this._table._columnCollection[i].FreeRecord(record);
				}
				if (this._lastFreeRecord == record + 1)
				{
					this._lastFreeRecord--;
				}
				else if (record < this._lastFreeRecord)
				{
					this._freeRecordList.Add(record);
				}
				record = -1;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00038718 File Offset: 0x00036918
		internal void Clear(bool clearAll)
		{
			if (clearAll)
			{
				for (int i = 0; i < this._recordCapacity; i++)
				{
					this._rows[i] = null;
				}
				int count = this._table._columnCollection.Count;
				for (int j = 0; j < count; j++)
				{
					DataColumn dataColumn = this._table._columnCollection[j];
					for (int k = 0; k < this._recordCapacity; k++)
					{
						dataColumn.FreeRecord(k);
					}
				}
				this._lastFreeRecord = 0;
				this._freeRecordList.Clear();
				return;
			}
			this._freeRecordList.Capacity = this._freeRecordList.Count + this._table.Rows.Count;
			for (int l = 0; l < this._recordCapacity; l++)
			{
				if (this._rows[l] != null && this._rows[l].rowID != -1L)
				{
					int num = l;
					this.FreeRecord(ref num);
				}
			}
		}

		// Token: 0x17000230 RID: 560
		internal DataRow this[int record]
		{
			get
			{
				return this._rows[record];
			}
			set
			{
				this._rows[record] = value;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003881C File Offset: 0x00036A1C
		internal void SetKeyValues(int record, DataKey key, object[] keyValues)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00038849 File Offset: 0x00036A49
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00038854 File Offset: 0x00036A54
		internal int CopyRecord(DataTable src, int record, int copy)
		{
			if (record == -1)
			{
				return copy;
			}
			int num = -1;
			try
			{
				num = ((copy == -1) ? this._table.NewUninitializedRecord() : copy);
				int count = this._table.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this._table.Columns[i];
					DataColumn dataColumn2 = src.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						object obj = dataColumn2[record];
						ICloneable cloneable = obj as ICloneable;
						if (cloneable != null)
						{
							dataColumn[num] = cloneable.Clone();
						}
						else
						{
							dataColumn[num] = obj;
						}
					}
					else if (-1 == copy)
					{
						dataColumn.Init(num);
					}
				}
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				if (-1 == copy)
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00038938 File Offset: 0x00036B38
		internal void SetRowCache(DataRow[] newRows)
		{
			this._rows = newRows;
			this._lastFreeRecord = this._rows.Length;
			this._recordCapacity = this._lastFreeRecord;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record)
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record, DataRow row)
		{
		}

		// Token: 0x0400082F RID: 2095
		private readonly DataTable _table;

		// Token: 0x04000830 RID: 2096
		private int _lastFreeRecord;

		// Token: 0x04000831 RID: 2097
		private int _minimumCapacity = 50;

		// Token: 0x04000832 RID: 2098
		private int _recordCapacity;

		// Token: 0x04000833 RID: 2099
		private readonly List<int> _freeRecordList = new List<int>();

		// Token: 0x04000834 RID: 2100
		private DataRow[] _rows;
	}
}
