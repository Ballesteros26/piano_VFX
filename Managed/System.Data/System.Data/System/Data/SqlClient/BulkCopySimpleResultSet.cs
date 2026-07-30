using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000151 RID: 337
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00052931 File Offset: 0x00050B31
		internal BulkCopySimpleResultSet()
		{
			this._results = new List<Result>();
		}

		// Token: 0x170002F3 RID: 755
		internal Result this[int idx]
		{
			get
			{
				return this._results[idx];
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00052954 File Offset: 0x00050B54
		internal void SetMetaData(_SqlMetaDataSet metadata)
		{
			this._resultSet = new Result(metadata);
			this._results.Add(this._resultSet);
			this._indexmap = new int[this._resultSet.MetaData.Length];
			for (int i = 0; i < this._indexmap.Length; i++)
			{
				this._indexmap[i] = i;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000529B5 File Offset: 0x00050BB5
		internal int[] CreateIndexMap()
		{
			return this._indexmap;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000529C0 File Offset: 0x00050BC0
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this._resultSet.MetaData.Length);
			this._resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x04000AD9 RID: 2777
		private readonly List<Result> _results;

		// Token: 0x04000ADA RID: 2778
		private Result _resultSet;

		// Token: 0x04000ADB RID: 2779
		private int[] _indexmap;
	}
}
