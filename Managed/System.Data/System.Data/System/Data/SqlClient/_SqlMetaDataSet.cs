using System;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200021F RID: 543
	internal sealed class _SqlMetaDataSet
	{
		// Token: 0x06001882 RID: 6274 RVA: 0x0007D28C File Offset: 0x0007B48C
		internal _SqlMetaDataSet(int count)
		{
			this._metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0007D2CC File Offset: 0x0007B4CC
		private _SqlMetaDataSet(_SqlMetaDataSet original)
		{
			this.id = original.id;
			this.indexMap = original.indexMap;
			this.visibleColumns = original.visibleColumns;
			this.dbColumnSchema = original.dbColumnSchema;
			if (original._metaDataArray == null)
			{
				this._metaDataArray = null;
				return;
			}
			this._metaDataArray = new _SqlMetaData[original._metaDataArray.Length];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = (_SqlMetaData)original._metaDataArray[i].Clone();
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0007D35F File Offset: 0x0007B55F
		internal int Length
		{
			get
			{
				return this._metaDataArray.Length;
			}
		}

		// Token: 0x17000491 RID: 1169
		internal _SqlMetaData this[int index]
		{
			get
			{
				return this._metaDataArray[index];
			}
			set
			{
				this._metaDataArray[index] = value;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0007D37E File Offset: 0x0007B57E
		public object Clone()
		{
			return new _SqlMetaDataSet(this);
		}

		// Token: 0x0400118C RID: 4492
		internal ushort id;

		// Token: 0x0400118D RID: 4493
		internal int[] indexMap;

		// Token: 0x0400118E RID: 4494
		internal int visibleColumns;

		// Token: 0x0400118F RID: 4495
		internal DataTable schemaTable;

		// Token: 0x04001190 RID: 4496
		private readonly _SqlMetaData[] _metaDataArray;

		// Token: 0x04001191 RID: 4497
		internal ReadOnlyCollection<DbColumn> dbColumnSchema;
	}
}
