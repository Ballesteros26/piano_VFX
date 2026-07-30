using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200014F RID: 335
	internal sealed class Row
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x000528C0 File Offset: 0x00050AC0
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000528D4 File Offset: 0x00050AD4
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x170002EF RID: 751
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private object[] _dataFields;
	}
}
