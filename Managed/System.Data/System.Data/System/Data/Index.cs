using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000EE RID: 238
	internal sealed class Index
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0003A53F File Offset: 0x0003873F
		public Index(DataTable table, IndexField[] indexFields, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, indexFields, null, recordStates, rowFilter)
		{
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003A54D File Offset: 0x0003874D
		public Index(DataTable table, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, Index.GetAllFields(table.Columns), comparison, recordStates, rowFilter)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003A568 File Offset: 0x00038768
		private static IndexField[] GetAllFields(DataColumnCollection columns)
		{
			IndexField[] array = new IndexField[columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new IndexField(columns[i], false);
			}
			return array;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003A5A4 File Offset: 0x000387A4
		private Index(DataTable table, IndexField[] indexFields, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState>("<ds.Index.Index|API> {0}, table={1}, recordStates={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, recordStates);
			if ((recordStates & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			this._table = table;
			this._listeners = new Listeners<DataViewListener>(this.ObjectID, (DataViewListener listener) => listener != null);
			this._indexFields = indexFields;
			this._recordStates = recordStates;
			this._comparison = comparison;
			DataColumnCollection columns = table.Columns;
			this._isSharable = rowFilter == null && comparison == null;
			if (rowFilter != null)
			{
				this._rowFilter = new WeakReference(rowFilter);
				DataExpression dataExpression = rowFilter as DataExpression;
				if (dataExpression != null)
				{
					this._hasRemoteAggregate = dataExpression.HasRemoteAggregate();
				}
			}
			this.InitRecords(rowFilter);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003A68C File Offset: 0x0003888C
		public bool Equal(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			if (!this._isSharable || this._indexFields.Length != indexDesc.Length || this._recordStates != recordStates || rowFilter != null)
			{
				return false;
			}
			for (int i = 0; i < this._indexFields.Length; i++)
			{
				if (this._indexFields[i].Column != indexDesc[i].Column || this._indexFields[i].IsDescending != indexDesc[i].IsDescending)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0003A710 File Offset: 0x00038910
		internal bool HasRemoteAggregate
		{
			get
			{
				return this._hasRemoteAggregate;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0003A718 File Offset: 0x00038918
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0003A720 File Offset: 0x00038920
		public DataViewRowState RecordStates
		{
			get
			{
				return this._recordStates;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0003A728 File Offset: 0x00038928
		public IFilter RowFilter
		{
			get
			{
				return (IFilter)((this._rowFilter != null) ? this._rowFilter.Target : null);
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003A745 File Offset: 0x00038945
		public int GetRecord(int recordIndex)
		{
			return this._records[recordIndex];
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0003A753 File Offset: 0x00038953
		public bool HasDuplicates
		{
			get
			{
				return this._records.HasDuplicates;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0003A760 File Offset: 0x00038960
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0003A768 File Offset: 0x00038968
		public bool IsSharable
		{
			get
			{
				return this._isSharable;
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003A770 File Offset: 0x00038970
		private bool AcceptRecord(int record)
		{
			return this.AcceptRecord(record, this.RowFilter);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003A780 File Offset: 0x00038980
		private bool AcceptRecord(int record, IFilter filter)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.AcceptRecord|API> {0}, record={1}", this.ObjectID, record);
			if (filter == null)
			{
				return true;
			}
			DataRow dataRow = this._table._recordManager[record];
			if (dataRow == null)
			{
				return true;
			}
			DataRowVersion dataRowVersion = DataRowVersion.Default;
			if (dataRow._oldRecord == record)
			{
				dataRowVersion = DataRowVersion.Original;
			}
			else if (dataRow._newRecord == record)
			{
				dataRowVersion = DataRowVersion.Current;
			}
			else if (dataRow._tempRecord == record)
			{
				dataRowVersion = DataRowVersion.Proposed;
			}
			return filter.Invoke(dataRow, dataRowVersion);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003A7FE File Offset: 0x000389FE
		internal void ListChangedAdd(DataViewListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003A80C File Offset: 0x00038A0C
		internal void ListChangedRemove(DataViewListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003A81A File Offset: 0x00038A1A
		public int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003A824 File Offset: 0x00038A24
		public void AddRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.AddRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			try
			{
				if (this._refCount == 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Add(this);
				}
				this._refCount++;
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003A8AC File Offset: 0x00038AAC
		public int RemoveRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.RemoveRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			int num2;
			try
			{
				int num = this._refCount - 1;
				this._refCount = num;
				num2 = num;
				if (this._refCount <= 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Remove(this);
				}
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
			return num2;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003A93C File Offset: 0x00038B3C
		private void ApplyChangeAction(int record, int action, int changeRecord)
		{
			if (action != 0)
			{
				if (action > 0)
				{
					if (this.AcceptRecord(record))
					{
						this.InsertRecord(record, true);
						return;
					}
				}
				else
				{
					if (this._comparison != null && -1 != record)
					{
						this.DeleteRecord(this.GetIndex(record, changeRecord));
						return;
					}
					this.DeleteRecord(this.GetIndex(record));
				}
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003A98B File Offset: 0x00038B8B
		public bool CheckUnique()
		{
			return !this.HasDuplicates;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003A998 File Offset: 0x00038B98
		private int CompareRecords(int record1, int record2)
		{
			if (this._comparison != null)
			{
				return this.CompareDataRows(record1, record2);
			}
			if (this._indexFields.Length != 0)
			{
				int i = 0;
				while (i < this._indexFields.Length)
				{
					int num = this._indexFields[i].Column.Compare(record1, record2);
					if (num != 0)
					{
						if (!this._indexFields[i].IsDescending)
						{
							return num;
						}
						return -num;
					}
					else
					{
						i++;
					}
				}
				return 0;
			}
			return this._table.Rows.IndexOf(this._table._recordManager[record1]).CompareTo(this._table.Rows.IndexOf(this._table._recordManager[record2]));
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003AA52 File Offset: 0x00038C52
		private int CompareDataRows(int record1, int record2)
		{
			return this._comparison(this._table._recordManager[record1], this._table._recordManager[record2]);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003AA84 File Offset: 0x00038C84
		private int CompareDuplicateRecords(int record1, int record2)
		{
			if (this._table._recordManager[record1] == null)
			{
				if (this._table._recordManager[record2] != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this._table._recordManager[record2] == null)
				{
					return 1;
				}
				int num = this._table._recordManager[record1].rowID.CompareTo(this._table._recordManager[record2].rowID);
				if (num == 0 && record1 != record2)
				{
					num = ((int)this._table._recordManager[record1].GetRecordState(record1)).CompareTo((int)this._table._recordManager[record2].GetRecordState(record2));
				}
				return num;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003AB44 File Offset: 0x00038D44
		private int CompareRecordToKey(int record1, object[] vals)
		{
			int i = 0;
			while (i < this._indexFields.Length)
			{
				int num = this._indexFields[i].Column.CompareValueTo(record1, vals[i]);
				if (num != 0)
				{
					if (!this._indexFields[i].IsDescending)
					{
						return num;
					}
					return -num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003AB9B File Offset: 0x00038D9B
		public void DeleteRecordFromIndex(int recordIndex)
		{
			this.DeleteRecord(recordIndex, false);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003ABA5 File Offset: 0x00038DA5
		private void DeleteRecord(int recordIndex)
		{
			this.DeleteRecord(recordIndex, true);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		private void DeleteRecord(int recordIndex, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.DeleteRecord|INFO> {0}, recordIndex={1}, fireEvent={2}", this.ObjectID, recordIndex, fireEvent);
			if (recordIndex >= 0)
			{
				this._recordCount--;
				int num = this._records.DeleteByIndex(recordIndex);
				this.MaintainDataView(ListChangedType.ItemDeleted, num, !fireEvent);
				if (fireEvent)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, recordIndex);
				}
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003AC0A File Offset: 0x00038E0A
		public RBTree<int>.RBTreeEnumerator GetEnumerator(int startIndex)
		{
			return new RBTree<int>.RBTreeEnumerator(this._records, startIndex);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003AC18 File Offset: 0x00038E18
		public int GetIndex(int record)
		{
			return this._records.GetIndexByKey(record);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003AC28 File Offset: 0x00038E28
		private int GetIndex(int record, int changeRecord)
		{
			DataRow dataRow = this._table._recordManager[record];
			int newRecord = dataRow._newRecord;
			int oldRecord = dataRow._oldRecord;
			int indexByKey;
			try
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = record;
					}
				}
				else
				{
					dataRow._newRecord = record;
				}
				indexByKey = this._records.GetIndexByKey(record);
			}
			finally
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = oldRecord;
					}
				}
				else
				{
					dataRow._newRecord = newRecord;
				}
			}
			return indexByKey;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003ACAC File Offset: 0x00038EAC
		public object[] GetUniqueKeyValues()
		{
			if (this._indexFields == null || this._indexFields.Length == 0)
			{
				return Array.Empty<object>();
			}
			List<object[]> list = new List<object[]>();
			this.GetUniqueKeyValues(list, this._records.root);
			return list.ToArray();
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		public int FindRecord(int record)
		{
			int num = this._records.Search(record);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003AD1C File Offset: 0x00038F1C
		public int FindRecordByKey(object key)
		{
			int num = this.FindNodeByKey(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0003AD44 File Offset: 0x00038F44
		public int FindRecordByKey(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003AD6C File Offset: 0x00038F6C
		private int FindNodeByKey(object originalKey)
		{
			if (this._indexFields.Length != 1)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, 1);
			}
			int num = this._records.root;
			if (num != 0)
			{
				DataColumn column = this._indexFields[0].Column;
				object obj = column.ConvertValue(originalKey);
				num = this._records.root;
				if (this._indexFields[0].IsDescending)
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 < 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
				else
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 > 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003AE58 File Offset: 0x00039058
		private int FindNodeByKeys(object[] originalKey)
		{
			int num = ((originalKey != null) ? originalKey.Length : 0);
			if (num == 0 || this._indexFields.Length != num)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, num);
			}
			int num2 = this._records.root;
			if (num2 != 0)
			{
				object[] array = new object[originalKey.Length];
				for (int i = 0; i < originalKey.Length; i++)
				{
					array[i] = this._indexFields[i].Column.ConvertValue(originalKey[i]);
				}
				num2 = this._records.root;
				while (num2 != 0)
				{
					num = this.CompareRecordToKey(this._records.Key(num2), array);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						num2 = this._records.Left(num2);
					}
					else
					{
						num2 = this._records.Right(num2);
					}
				}
			}
			return num2;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003AF18 File Offset: 0x00039118
		private int FindNodeByKeyRecord(int record)
		{
			int num = this._records.root;
			if (num != 0)
			{
				num = this._records.root;
				while (num != 0)
				{
					int num2 = this.CompareRecords(this._records.Key(num), record);
					if (num2 == 0)
					{
						break;
					}
					if (num2 > 0)
					{
						num = this._records.Left(num);
					}
					else
					{
						num = this._records.Right(num);
					}
				}
			}
			return num;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003AF80 File Offset: 0x00039180
		private Range GetRangeFromNode(int nodeId)
		{
			if (nodeId == 0)
			{
				return default(Range);
			}
			int indexByNode = this._records.GetIndexByNode(nodeId);
			if (this._records.Next(nodeId) == 0)
			{
				return new Range(indexByNode, indexByNode);
			}
			int num = this._records.SubTreeSize(this._records.Next(nodeId));
			return new Range(indexByNode, indexByNode + num - 1);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003AFE0 File Offset: 0x000391E0
		public Range FindRecords(object key)
		{
			int num = this.FindNodeByKey(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003AFFC File Offset: 0x000391FC
		public Range FindRecords(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003B018 File Offset: 0x00039218
		internal void FireResetEvent()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.FireResetEvent|API> {0}", this.ObjectID);
			if (this.DoListChanged)
			{
				this.OnListChanged(DataView.s_resetEventArgs);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003B044 File Offset: 0x00039244
		private int GetChangeAction(DataViewRowState oldState, DataViewRowState newState)
		{
			int num = (((this._recordStates & oldState) == DataViewRowState.None) ? 0 : 1);
			return (((this._recordStates & newState) == DataViewRowState.None) ? 0 : 1) - num;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003B070 File Offset: 0x00039270
		private static int GetReplaceAction(DataViewRowState oldState)
		{
			if ((DataViewRowState.CurrentRows & oldState) != DataViewRowState.None)
			{
				return 1;
			}
			if ((DataViewRowState.OriginalRows & oldState) == DataViewRowState.None)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003B083 File Offset: 0x00039283
		public DataRow GetRow(int i)
		{
			return this._table._recordManager[this.GetRecord(i)];
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003B09C File Offset: 0x0003929C
		public DataRow[] GetRows(object[] values)
		{
			return this.GetRows(this.FindRecords(values));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003B0AC File Offset: 0x000392AC
		public DataRow[] GetRows(Range range)
		{
			DataRow[] array = this._table.NewRowArray(range.Count);
			if (array.Length != 0)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.GetEnumerator(range.Min);
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = this._table._recordManager[enumerator.Current];
					num++;
				}
			}
			return array;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003B110 File Offset: 0x00039310
		private void InitRecords(IFilter filter)
		{
			DataViewRowState recordStates = this._recordStates;
			bool flag = this._indexFields.Length == 0;
			this._records = new Index.IndexTree(this);
			this._recordCount = 0;
			foreach (object obj in this._table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = -1;
				if (dataRow._oldRecord == dataRow._newRecord)
				{
					if ((recordStates & DataViewRowState.Unchanged) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if (dataRow._oldRecord == -1)
				{
					if ((recordStates & DataViewRowState.Added) != DataViewRowState.None)
					{
						num = dataRow._newRecord;
					}
				}
				else if (dataRow._newRecord == -1)
				{
					if ((recordStates & DataViewRowState.Deleted) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if ((recordStates & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					num = dataRow._newRecord;
				}
				else if ((recordStates & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
				{
					num = dataRow._oldRecord;
				}
				if (num != -1 && this.AcceptRecord(num, filter))
				{
					this._records.InsertAt(-1, num, flag);
					this._recordCount++;
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0003B234 File Offset: 0x00039434
		public int InsertRecordToIndex(int record)
		{
			int num = -1;
			if (this.AcceptRecord(record))
			{
				num = this.InsertRecord(record, false);
			}
			return num;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003B258 File Offset: 0x00039458
		private int InsertRecord(int record, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.InsertRecord|INFO> {0}, record={1}, fireEvent={2}", this.ObjectID, record, fireEvent);
			bool flag = false;
			if (this._indexFields.Length == 0 && this._table != null)
			{
				DataRow dataRow = this._table._recordManager[record];
				flag = this._table.Rows.IndexOf(dataRow) + 1 == this._table.Rows.Count;
			}
			int num = this._records.InsertAt(-1, record, flag);
			this._recordCount++;
			this.MaintainDataView(ListChangedType.ItemAdded, record, !fireEvent);
			if (fireEvent)
			{
				if (this.DoListChanged)
				{
					this.OnListChanged(ListChangedType.ItemAdded, this._records.GetIndexByNode(num));
				}
				return 0;
			}
			return this._records.GetIndexByNode(num);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003B31C File Offset: 0x0003951C
		public bool IsKeyInIndex(object key)
		{
			int num = this.FindNodeByKey(key);
			return num != 0;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003B338 File Offset: 0x00039538
		public bool IsKeyInIndex(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return num != 0;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0003B354 File Offset: 0x00039554
		public bool IsKeyRecordInIndex(int record)
		{
			int num = this.FindNodeByKeyRecord(record);
			return num != 0;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0003B36D File Offset: 0x0003956D
		private bool DoListChanged
		{
			get
			{
				return !this._suspendEvents && this._listeners.HasListeners && !this._table.AreIndexEventsSuspended;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003B394 File Offset: 0x00039594
		private void OnListChanged(ListChangedType changedType, int newIndex, int oldIndex)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, newIndex, oldIndex));
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0003B3AC File Offset: 0x000395AC
		private void OnListChanged(ListChangedType changedType, int index)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, index));
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003B3C4 File Offset: 0x000395C4
		private void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.OnListChanged|INFO> {0}", this.ObjectID);
			this._listeners.Notify<ListChangedEventArgs, bool, bool>(e, false, false, delegate(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			});
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003B414 File Offset: 0x00039614
		private void MaintainDataView(ListChangedType changedType, int record, bool trackAddRemove)
		{
			this._listeners.Notify<ListChangedType, DataRow, bool>(changedType, (0 <= record) ? this._table._recordManager[record] : null, trackAddRemove, delegate(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(changedType, row, track);
			});
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003B464 File Offset: 0x00039664
		public void Reset()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.Reset|API> {0}", this.ObjectID);
			this.InitRecords(this.RowFilter);
			this.MaintainDataView(ListChangedType.Reset, -1, false);
			this.FireResetEvent();
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003B498 File Offset: 0x00039698
		public void RecordChanged(int record)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.RecordChanged|API> {0}, record={1}", this.ObjectID, record);
			if (this.DoListChanged)
			{
				int index = this.GetIndex(record);
				if (index >= 0)
				{
					this.OnListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003B4D8 File Offset: 0x000396D8
		public void RecordChanged(int oldIndex, int newIndex)
		{
			DataCommonEventSource.Log.Trace<int, int, int>("<ds.Index.RecordChanged|API> {0}, oldIndex={1}, newIndex={2}", this.ObjectID, oldIndex, newIndex);
			if (oldIndex > -1 || newIndex > -1)
			{
				if (oldIndex == newIndex)
				{
					this.OnListChanged(ListChangedType.ItemChanged, newIndex, oldIndex);
					return;
				}
				if (oldIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemAdded, newIndex, oldIndex);
					return;
				}
				if (newIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, oldIndex);
					return;
				}
				this.OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0003B538 File Offset: 0x00039738
		public void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, record={1}, oldState={2}, newState={3}", this.ObjectID, record, oldState, newState);
			int changeAction = this.GetChangeAction(oldState, newState);
			this.ApplyChangeAction(record, changeAction, Index.GetReplaceAction(oldState));
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003B574 File Offset: 0x00039774
		public void RecordStateChanged(int oldRecord, DataViewRowState oldOldState, DataViewRowState oldNewState, int newRecord, DataViewRowState newOldState, DataViewRowState newNewState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, oldRecord={1}, oldOldState={2}, oldNewState={3}, newRecord={4}, newOldState={5}, newNewState={6}", this.ObjectID, oldRecord, oldOldState, oldNewState, newRecord, newOldState, newNewState);
			int changeAction = this.GetChangeAction(oldOldState, oldNewState);
			int changeAction2 = this.GetChangeAction(newOldState, newNewState);
			if (changeAction != -1 || changeAction2 != 1 || !this.AcceptRecord(newRecord))
			{
				this.ApplyChangeAction(oldRecord, changeAction, Index.GetReplaceAction(oldOldState));
				this.ApplyChangeAction(newRecord, changeAction2, Index.GetReplaceAction(newOldState));
				return;
			}
			int num;
			if (this._comparison != null && changeAction < 0)
			{
				num = this.GetIndex(oldRecord, Index.GetReplaceAction(oldOldState));
			}
			else
			{
				num = this.GetIndex(oldRecord);
			}
			if (this._comparison == null && num != -1 && this.CompareRecords(oldRecord, newRecord) == 0)
			{
				this._records.UpdateNodeKey(oldRecord, newRecord);
				int index = this.GetIndex(newRecord);
				this.OnListChanged(ListChangedType.ItemChanged, index, index);
				return;
			}
			this._suspendEvents = true;
			if (num != -1)
			{
				this._records.DeleteByIndex(num);
				this._recordCount--;
			}
			this._records.Insert(newRecord);
			this._recordCount++;
			this._suspendEvents = false;
			int index2 = this.GetIndex(newRecord);
			if (num == index2)
			{
				this.OnListChanged(ListChangedType.ItemChanged, index2, num);
				return;
			}
			if (num == -1)
			{
				this.MaintainDataView(ListChangedType.ItemAdded, newRecord, false);
				this.OnListChanged(ListChangedType.ItemAdded, this.GetIndex(newRecord));
				return;
			}
			this.OnListChanged(ListChangedType.ItemMoved, index2, num);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003B6D4 File Offset: 0x000398D4
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0003B6DC File Offset: 0x000398DC
		private void GetUniqueKeyValues(List<object[]> list, int curNodeId)
		{
			if (curNodeId != 0)
			{
				this.GetUniqueKeyValues(list, this._records.Left(curNodeId));
				int num = this._records.Key(curNodeId);
				object[] array = new object[this._indexFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._indexFields[i].Column[num];
				}
				list.Add(array);
				this.GetUniqueKeyValues(list, this._records.Right(curNodeId));
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0003B75C File Offset: 0x0003995C
		internal static int IndexOfReference<T>(List<T> list, T item) where T : class
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == item)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0003B794 File Offset: 0x00039994
		internal static bool ContainsReference<T>(List<T> list, T item) where T : class
		{
			return 0 <= Index.IndexOfReference<T>(list, item);
		}

		// Token: 0x04000856 RID: 2134
		private const int DoNotReplaceCompareRecord = 0;

		// Token: 0x04000857 RID: 2135
		private const int ReplaceNewRecordForCompare = 1;

		// Token: 0x04000858 RID: 2136
		private const int ReplaceOldRecordForCompare = 2;

		// Token: 0x04000859 RID: 2137
		private readonly DataTable _table;

		// Token: 0x0400085A RID: 2138
		internal readonly IndexField[] _indexFields;

		// Token: 0x0400085B RID: 2139
		private readonly Comparison<DataRow> _comparison;

		// Token: 0x0400085C RID: 2140
		private readonly DataViewRowState _recordStates;

		// Token: 0x0400085D RID: 2141
		private WeakReference _rowFilter;

		// Token: 0x0400085E RID: 2142
		private Index.IndexTree _records;

		// Token: 0x0400085F RID: 2143
		private int _recordCount;

		// Token: 0x04000860 RID: 2144
		private int _refCount;

		// Token: 0x04000861 RID: 2145
		private Listeners<DataViewListener> _listeners;

		// Token: 0x04000862 RID: 2146
		private bool _suspendEvents;

		// Token: 0x04000863 RID: 2147
		private readonly bool _isSharable;

		// Token: 0x04000864 RID: 2148
		private readonly bool _hasRemoteAggregate;

		// Token: 0x04000865 RID: 2149
		internal const int MaskBits = 2147483647;

		// Token: 0x04000866 RID: 2150
		private static int s_objectTypeCount;

		// Token: 0x04000867 RID: 2151
		private readonly int _objectID = Interlocked.Increment(ref Index.s_objectTypeCount);

		// Token: 0x020000EF RID: 239
		private sealed class IndexTree : RBTree<int>
		{
			// Token: 0x06000CBD RID: 3261 RVA: 0x0003B7A3 File Offset: 0x000399A3
			internal IndexTree(Index index)
				: base(TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				this._index = index;
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x0003B7B3 File Offset: 0x000399B3
			protected override int CompareNode(int record1, int record2)
			{
				return this._index.CompareRecords(record1, record2);
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x0003B7C2 File Offset: 0x000399C2
			protected override int CompareSateliteTreeNode(int record1, int record2)
			{
				return this._index.CompareDuplicateRecords(record1, record2);
			}

			// Token: 0x04000868 RID: 2152
			private readonly Index _index;
		}
	}
}
