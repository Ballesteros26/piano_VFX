using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000014 RID: 20
	public class UpdateEventArgs : EventArgs
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00008288 File Offset: 0x00006488
		internal UpdateEventArgs(string database, string table, UpdateEventType eventType, long rowid)
		{
			this.Database = database;
			this.Table = table;
			this.Event = eventType;
			this.RowId = rowid;
		}

		// Token: 0x0400006A RID: 106
		public readonly string Database;

		// Token: 0x0400006B RID: 107
		public readonly string Table;

		// Token: 0x0400006C RID: 108
		public readonly UpdateEventType Event;

		// Token: 0x0400006D RID: 109
		public readonly long RowId;
	}
}
