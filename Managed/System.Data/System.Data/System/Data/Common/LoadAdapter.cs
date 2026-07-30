using System;

namespace System.Data.Common
{
	// Token: 0x02000330 RID: 816
	internal sealed class LoadAdapter : DataAdapter
	{
		// Token: 0x0600257D RID: 9597 RVA: 0x000AAFEA File Offset: 0x000A91EA
		internal LoadAdapter()
		{
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000AAFF2 File Offset: 0x000A91F2
		internal int FillFromReader(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.Fill(dataTables, dataReader, startRecord, maxRecords);
		}
	}
}
