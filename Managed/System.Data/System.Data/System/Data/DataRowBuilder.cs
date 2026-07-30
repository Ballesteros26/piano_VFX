using System;
using Unity;

namespace System.Data
{
	/// <summary>The DataRowBuilder type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007C RID: 124
	public sealed class DataRowBuilder
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00019840 File Offset: 0x00017A40
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataRowBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400058A RID: 1418
		internal readonly DataTable _table;

		// Token: 0x0400058B RID: 1419
		internal int _record;
	}
}
