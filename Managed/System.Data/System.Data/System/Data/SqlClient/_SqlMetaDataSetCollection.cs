using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000220 RID: 544
	internal sealed class _SqlMetaDataSetCollection
	{
		// Token: 0x06001888 RID: 6280 RVA: 0x0007D386 File Offset: 0x0007B586
		internal _SqlMetaDataSetCollection()
		{
			this._altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007D39C File Offset: 0x0007B59C
		internal void SetAltMetaData(_SqlMetaDataSet altMetaDataSet)
		{
			int id = (int)altMetaDataSet.id;
			for (int i = 0; i < this._altMetaDataSetArray.Count; i++)
			{
				if ((int)this._altMetaDataSetArray[i].id == id)
				{
					this._altMetaDataSetArray[i] = altMetaDataSet;
					return;
				}
			}
			this._altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007D3F4 File Offset: 0x0007B5F4
		internal _SqlMetaDataSet GetAltMetaData(int id)
		{
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				if ((int)sqlMetaDataSet.id == id)
				{
					return sqlMetaDataSet;
				}
			}
			return null;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007D450 File Offset: 0x0007B650
		public object Clone()
		{
			_SqlMetaDataSetCollection sqlMetaDataSetCollection = new _SqlMetaDataSetCollection();
			sqlMetaDataSetCollection.metaDataSet = ((this.metaDataSet == null) ? null : ((_SqlMetaDataSet)this.metaDataSet.Clone()));
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				sqlMetaDataSetCollection._altMetaDataSetArray.Add((_SqlMetaDataSet)sqlMetaDataSet.Clone());
			}
			return sqlMetaDataSetCollection;
		}

		// Token: 0x04001192 RID: 4498
		private readonly List<_SqlMetaDataSet> _altMetaDataSetArray;

		// Token: 0x04001193 RID: 4499
		internal _SqlMetaDataSet metaDataSet;
	}
}
