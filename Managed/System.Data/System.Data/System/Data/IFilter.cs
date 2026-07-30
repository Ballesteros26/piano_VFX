using System;

namespace System.Data
{
	// Token: 0x020000BB RID: 187
	internal interface IFilter
	{
		// Token: 0x06000AE6 RID: 2790
		bool Invoke(DataRow row, DataRowVersion version);
	}
}
