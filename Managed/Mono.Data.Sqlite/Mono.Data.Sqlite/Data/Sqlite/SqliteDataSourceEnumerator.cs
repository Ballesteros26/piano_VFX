using System;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000034 RID: 52
	public class SqliteDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000E368 File Offset: 0x0000C568
		public override DataTable GetDataSources()
		{
			DataTable dataTable = new DataTable();
			DataColumn dataColumn = new DataColumn("ServerName", typeof(string));
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn("InstanceName", typeof(string));
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn("IsClustered", typeof(bool));
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn("Version", typeof(string));
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn("FactoryName", typeof(string));
			dataTable.Columns.Add(dataColumn);
			DataRow dataRow = dataTable.NewRow();
			dataRow[0] = "Sqlite Embedded Database";
			dataRow[1] = "Sqlite Default Instance";
			dataRow[2] = false;
			dataRow[3] = "?";
			dataRow[4] = "Mono.Data.Sqlite.SqliteConnectionFactory";
			dataTable.Rows.Add(dataRow);
			return dataTable;
		}
	}
}
