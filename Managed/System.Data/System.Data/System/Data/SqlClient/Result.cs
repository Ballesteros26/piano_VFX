using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000150 RID: 336
	internal sealed class Result
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x000528E6 File Offset: 0x00050AE6
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new List<Row>();
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00052900 File Offset: 0x00050B00
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0005290D File Offset: 0x00050B0D
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x170002F2 RID: 754
		internal Row this[int index]
		{
			get
			{
				return this._rowset[index];
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00052923 File Offset: 0x00050B23
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x04000AD7 RID: 2775
		private readonly _SqlMetaDataSet _metadata;

		// Token: 0x04000AD8 RID: 2776
		private readonly List<Row> _rowset;
	}
}
