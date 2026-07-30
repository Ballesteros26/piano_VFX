using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x02000292 RID: 658
	internal sealed class OdbcConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0008A160 File Offset: 0x00088360
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x0008A168 File Offset: 0x00088368
		internal string DriverName
		{
			get
			{
				return this._driverName;
			}
			set
			{
				this._driverName = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0008A171 File Offset: 0x00088371
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0008A179 File Offset: 0x00088379
		internal string DriverVersion
		{
			get
			{
				return this._driverVersion;
			}
			set
			{
				this._driverVersion = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0008A182 File Offset: 0x00088382
		internal bool HasQuoteChar
		{
			get
			{
				return this._hasQuoteChar;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0008A18A File Offset: 0x0008838A
		internal bool HasEscapeChar
		{
			get
			{
				return this._hasEscapeChar;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0008A192 File Offset: 0x00088392
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0008A19A File Offset: 0x0008839A
		internal string QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				this._quoteChar = value;
				this._hasQuoteChar = true;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0008A1AA File Offset: 0x000883AA
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0008A1B2 File Offset: 0x000883B2
		internal char EscapeChar
		{
			get
			{
				return this._escapeChar;
			}
			set
			{
				this._escapeChar = value;
				this._hasEscapeChar = true;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0008A1C2 File Offset: 0x000883C2
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0008A1CA File Offset: 0x000883CA
		internal bool IsV3Driver
		{
			get
			{
				return this._isV3Driver;
			}
			set
			{
				this._isV3Driver = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0008A1D3 File Offset: 0x000883D3
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0008A1DB File Offset: 0x000883DB
		internal int SupportedSQLTypes
		{
			get
			{
				return this._supportedSQLTypes;
			}
			set
			{
				this._supportedSQLTypes = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0008A1E4 File Offset: 0x000883E4
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0008A1EC File Offset: 0x000883EC
		internal int TestedSQLTypes
		{
			get
			{
				return this._testedSQLTypes;
			}
			set
			{
				this._testedSQLTypes = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0008A1F5 File Offset: 0x000883F5
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0008A1FD File Offset: 0x000883FD
		internal int RestrictedSQLBindTypes
		{
			get
			{
				return this._restrictedSQLBindTypes;
			}
			set
			{
				this._restrictedSQLBindTypes = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0008A206 File Offset: 0x00088406
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0008A20E File Offset: 0x0008840E
		internal bool NoCurrentCatalog
		{
			get
			{
				return this._noCurrentCatalog;
			}
			set
			{
				this._noCurrentCatalog = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0008A217 File Offset: 0x00088417
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0008A21F File Offset: 0x0008841F
		internal bool NoConnectionDead
		{
			get
			{
				return this._noConnectionDead;
			}
			set
			{
				this._noConnectionDead = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0008A228 File Offset: 0x00088428
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0008A230 File Offset: 0x00088430
		internal bool NoQueryTimeout
		{
			get
			{
				return this._noQueryTimeout;
			}
			set
			{
				this._noQueryTimeout = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0008A239 File Offset: 0x00088439
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0008A241 File Offset: 0x00088441
		internal bool NoSqlSoptSSNoBrowseTable
		{
			get
			{
				return this._noSqlSoptSSNoBrowseTable;
			}
			set
			{
				this._noSqlSoptSSNoBrowseTable = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0008A24A File Offset: 0x0008844A
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x0008A252 File Offset: 0x00088452
		internal bool NoSqlSoptSSHiddenColumns
		{
			get
			{
				return this._noSqlSoptSSHiddenColumns;
			}
			set
			{
				this._noSqlSoptSSHiddenColumns = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0008A25B File Offset: 0x0008845B
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x0008A263 File Offset: 0x00088463
		internal bool NoSqlCASSColumnKey
		{
			get
			{
				return this._noSqlCASSColumnKey;
			}
			set
			{
				this._noSqlCASSColumnKey = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0008A26C File Offset: 0x0008846C
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0008A274 File Offset: 0x00088474
		internal bool NoSqlPrimaryKeys
		{
			get
			{
				return this._noSqlPrimaryKeys;
			}
			set
			{
				this._noSqlPrimaryKeys = value;
			}
		}

		// Token: 0x040014EB RID: 5355
		private string _driverName;

		// Token: 0x040014EC RID: 5356
		private string _driverVersion;

		// Token: 0x040014ED RID: 5357
		private string _quoteChar;

		// Token: 0x040014EE RID: 5358
		private char _escapeChar;

		// Token: 0x040014EF RID: 5359
		private bool _hasQuoteChar;

		// Token: 0x040014F0 RID: 5360
		private bool _hasEscapeChar;

		// Token: 0x040014F1 RID: 5361
		private bool _isV3Driver;

		// Token: 0x040014F2 RID: 5362
		private int _supportedSQLTypes;

		// Token: 0x040014F3 RID: 5363
		private int _testedSQLTypes;

		// Token: 0x040014F4 RID: 5364
		private int _restrictedSQLBindTypes;

		// Token: 0x040014F5 RID: 5365
		private bool _noCurrentCatalog;

		// Token: 0x040014F6 RID: 5366
		private bool _noConnectionDead;

		// Token: 0x040014F7 RID: 5367
		private bool _noQueryTimeout;

		// Token: 0x040014F8 RID: 5368
		private bool _noSqlSoptSSNoBrowseTable;

		// Token: 0x040014F9 RID: 5369
		private bool _noSqlSoptSSHiddenColumns;

		// Token: 0x040014FA RID: 5370
		private bool _noSqlCASSColumnKey;

		// Token: 0x040014FB RID: 5371
		private bool _noSqlPrimaryKeys;
	}
}
