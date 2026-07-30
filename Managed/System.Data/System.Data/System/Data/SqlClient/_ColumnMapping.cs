using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200014E RID: 334
	internal sealed class _ColumnMapping
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x000528AA File Offset: 0x00050AAA
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x04000AD4 RID: 2772
		internal int _sourceColumnOrdinal;

		// Token: 0x04000AD5 RID: 2773
		internal _SqlMetaData _metadata;
	}
}
