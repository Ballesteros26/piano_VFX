using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x0200028C RID: 652
	internal sealed class CMDWrapper
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x0008861D File Offset: 0x0008681D
		internal CMDWrapper(OdbcConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0008862C File Offset: 0x0008682C
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x00088634 File Offset: 0x00086834
		internal bool Canceling
		{
			get
			{
				return this._canceling;
			}
			set
			{
				this._canceling = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0008863D File Offset: 0x0008683D
		internal OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x00088645 File Offset: 0x00086845
		internal bool HasBoundColumns
		{
			set
			{
				this._hasBoundColumns = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0008864E File Offset: 0x0008684E
		internal OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._stmt;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00088656 File Offset: 0x00086856
		internal OdbcStatementHandle KeyInfoStatement
		{
			get
			{
				return this._keyinfostmt;
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0008865E File Offset: 0x0008685E
		internal void CreateKeyInfoStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this._keyinfostmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00088677 File Offset: 0x00086877
		internal void CreateStatementHandle()
		{
			this.DisposeStatementHandle();
			this._stmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00088690 File Offset: 0x00086890
		internal void Dispose()
		{
			if (this._dataReaderBuf != null)
			{
				this._dataReaderBuf.Dispose();
				this._dataReaderBuf = null;
			}
			this.DisposeStatementHandle();
			CNativeBuffer nativeParameterBuffer = this._nativeParameterBuffer;
			this._nativeParameterBuffer = null;
			if (nativeParameterBuffer != null)
			{
				nativeParameterBuffer.Dispose();
			}
			this._ssKeyInfoModeOn = false;
			this._ssKeyInfoModeOff = false;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000886E4 File Offset: 0x000868E4
		private void DisposeDescriptorHandle()
		{
			OdbcDescriptorHandle hdesc = this._hdesc;
			if (hdesc != null)
			{
				this._hdesc = null;
				hdesc.Dispose();
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00088708 File Offset: 0x00086908
		internal void DisposeStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt = this._stmt;
			if (stmt != null)
			{
				this._stmt = null;
				stmt.Dispose();
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00088738 File Offset: 0x00086938
		internal void DisposeKeyInfoStatementHandle()
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				this._keyinfostmt = null;
				keyinfostmt.Dispose();
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0008875C File Offset: 0x0008695C
		internal void FreeStatementHandle(ODBC32.STMT stmt)
		{
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt2 = this._stmt;
			if (stmt2 != null)
			{
				try
				{
					ODBC32.RetCode retCode = stmt2.FreeStatement(stmt);
					this.StatementErrorHandler(retCode);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this._stmt = null;
						stmt2.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000887B0 File Offset: 0x000869B0
		internal void FreeKeyInfoStatementHandle(ODBC32.STMT stmt)
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				try
				{
					keyinfostmt.FreeStatement(stmt);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this._keyinfostmt = null;
						keyinfostmt.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000887F8 File Offset: 0x000869F8
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			OdbcDescriptorHandle odbcDescriptorHandle = this._hdesc;
			if (this._hdesc == null)
			{
				odbcDescriptorHandle = (this._hdesc = new OdbcDescriptorHandle(this._stmt, attribute));
			}
			return odbcDescriptorHandle;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0008882C File Offset: 0x00086A2C
		internal string GetDiagSqlState()
		{
			string text;
			this._stmt.GetDiagnosticField(out text);
			return text;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00088848 File Offset: 0x00086A48
		internal void StatementErrorHandler(ODBC32.RetCode retcode)
		{
			if (retcode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				this._connection.HandleErrorNoThrow(this._stmt, retcode);
				return;
			}
			throw this._connection.HandleErrorNoThrow(this._stmt, retcode);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00088874 File Offset: 0x00086A74
		internal void UnbindStmtColumns()
		{
			if (this._hasBoundColumns)
			{
				this.FreeStatementHandle(ODBC32.STMT.UNBIND);
				this._hasBoundColumns = false;
			}
		}

		// Token: 0x040014D0 RID: 5328
		private OdbcStatementHandle _stmt;

		// Token: 0x040014D1 RID: 5329
		private OdbcStatementHandle _keyinfostmt;

		// Token: 0x040014D2 RID: 5330
		internal OdbcDescriptorHandle _hdesc;

		// Token: 0x040014D3 RID: 5331
		internal CNativeBuffer _nativeParameterBuffer;

		// Token: 0x040014D4 RID: 5332
		internal CNativeBuffer _dataReaderBuf;

		// Token: 0x040014D5 RID: 5333
		private readonly OdbcConnection _connection;

		// Token: 0x040014D6 RID: 5334
		private bool _canceling;

		// Token: 0x040014D7 RID: 5335
		internal bool _hasBoundColumns;

		// Token: 0x040014D8 RID: 5336
		internal bool _ssKeyInfoModeOn;

		// Token: 0x040014D9 RID: 5337
		internal bool _ssKeyInfoModeOff;
	}
}
