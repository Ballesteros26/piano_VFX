using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002B1 RID: 689
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x000912B6 File Offset: 0x0008F4B6
		internal OdbcStatementHandle(OdbcConnectionHandle connectionHandle)
			: base(ODBC32.SQL_HANDLE.STMT, connectionHandle)
		{
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000912C0 File Offset: 0x0008F4C0
		internal ODBC32.RetCode BindColumn2(int columnNumber, ODBC32.SQL_C targetType, HandleRef buffer, IntPtr length, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, length, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000912EC File Offset: 0x0008F4EC
		internal ODBC32.RetCode BindColumn3(int columnNumber, ODBC32.SQL_C targetType, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, ADP.PtrZero, ADP.PtrZero, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0009131C File Offset: 0x0008F51C
		internal ODBC32.RetCode BindParameter(short ordinal, short parameterDirection, ODBC32.SQL_C sqlctype, ODBC32.SQL_TYPE sqltype, IntPtr cchSize, IntPtr scale, HandleRef buffer, IntPtr bufferLength, HandleRef intbuffer)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, cchSize, scale, buffer, bufferLength, intbuffer);
			ODBC.TraceODBC(3, "SQLBindParameter", retCode);
			return retCode;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00091350 File Offset: 0x0008F550
		internal ODBC32.RetCode Cancel()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCancel(this);
			ODBC.TraceODBC(3, "SQLCancel", retCode);
			return retCode;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00091374 File Offset: 0x0008F574
		internal ODBC32.RetCode CloseCursor()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCloseCursor(this);
			ODBC.TraceODBC(3, "SQLCloseCursor", retCode);
			return retCode;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00091398 File Offset: 0x0008F598
		internal ODBC32.RetCode ColumnAttribute(int columnNumber, short fieldIdentifier, CNativeBuffer characterAttribute, out short stringLength, out SQLLEN numericAttribute)
		{
			IntPtr intPtr;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColAttributeW(this, checked((short)columnNumber), fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out intPtr);
			numericAttribute = new SQLLEN(intPtr);
			ODBC.TraceODBC(3, "SQLColAttributeW", retCode);
			return retCode;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000913D4 File Offset: 0x0008F5D4
		internal ODBC32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColumnsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00091414 File Offset: 0x0008F614
		internal ODBC32.RetCode Execute()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecute(this);
			ODBC.TraceODBC(3, "SQLExecute", retCode);
			return retCode;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00091438 File Offset: 0x0008F638
		internal ODBC32.RetCode ExecuteDirect(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecDirectW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLExecDirectW", retCode);
			return retCode;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0009145C File Offset: 0x0008F65C
		internal ODBC32.RetCode Fetch()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFetch(this);
			ODBC.TraceODBC(3, "SQLFetch", retCode);
			return retCode;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00091480 File Offset: 0x0008F680
		internal ODBC32.RetCode FreeStatement(ODBC32.STMT stmt)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFreeStmt(this, stmt);
			ODBC.TraceODBC(3, "SQLFreeStmt", retCode);
			return retCode;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000914A4 File Offset: 0x0008F6A4
		internal ODBC32.RetCode GetData(int index, ODBC32.SQL_C sqlctype, CNativeBuffer buffer, int cb, out IntPtr cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(cb), out cbActual);
			ODBC.TraceODBC(3, "SQLGetData", retCode);
			return retCode;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000914D4 File Offset: 0x0008F6D4
		internal ODBC32.RetCode GetStatementAttribute(ODBC32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetStmtAttrW(this, attribute, out value, ADP.PtrSize, out stringLength);
			ODBC.TraceODBC(3, "SQLGetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00091500 File Offset: 0x0008F700
		internal ODBC32.RetCode GetTypeInfo(short fSqlType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetTypeInfo(this, fSqlType);
			ODBC.TraceODBC(3, "SQLGetTypeInfo", retCode);
			return retCode;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00091524 File Offset: 0x0008F724
		internal ODBC32.RetCode MoreResults()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLMoreResults(this);
			ODBC.TraceODBC(3, "SQLMoreResults", retCode);
			return retCode;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00091548 File Offset: 0x0008F748
		internal ODBC32.RetCode NumberOfResultColumns(out short columnsAffected)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLNumResultCols(this, out columnsAffected);
			ODBC.TraceODBC(3, "SQLNumResultCols", retCode);
			return retCode;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0009156C File Offset: 0x0008F76C
		internal ODBC32.RetCode Prepare(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrepareW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLPrepareW", retCode);
			return retCode;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00091590 File Offset: 0x0008F790
		internal ODBC32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrimaryKeysW(this, catalogName, ODBC.ShortStringLength(catalogName), schemaName, ODBC.ShortStringLength(schemaName), tableName, ODBC.ShortStringLength(tableName));
			ODBC.TraceODBC(3, "SQLPrimaryKeysW", retCode);
			return retCode;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000915C8 File Offset: 0x0008F7C8
		internal ODBC32.RetCode Procedures(string procedureCatalog, string procedureSchema, string procedureName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProceduresW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName));
			ODBC.TraceODBC(3, "SQLProceduresW", retCode);
			return retCode;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00091600 File Offset: 0x0008F800
		internal ODBC32.RetCode ProcedureColumns(string procedureCatalog, string procedureSchema, string procedureName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProcedureColumnsW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLProcedureColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00091640 File Offset: 0x0008F840
		internal ODBC32.RetCode RowCount(out SQLLEN rowCount)
		{
			IntPtr intPtr;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLRowCount(this, out intPtr);
			rowCount = new SQLLEN(intPtr);
			ODBC.TraceODBC(3, "SQLRowCount", retCode);
			return retCode;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00091670 File Offset: 0x0008F870
		internal ODBC32.RetCode SetStatementAttribute(ODBC32.SQL_ATTR attribute, IntPtr value, ODBC32.SQL_IS stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
			ODBC.TraceODBC(3, "SQLSetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00091694 File Offset: 0x0008F894
		internal ODBC32.RetCode SpecialColumns(string quotedTable)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSpecialColumnsW(this, ODBC32.SQL_SPECIALCOLS.ROWVER, null, 0, null, 0, quotedTable, ODBC.ShortStringLength(quotedTable), ODBC32.SQL_SCOPE.SESSION, ODBC32.SQL_NULLABILITY.NO_NULLS);
			ODBC.TraceODBC(3, "SQLSpecialColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000916C4 File Offset: 0x0008F8C4
		internal ODBC32.RetCode Statistics(string tableCatalog, string tableSchema, string tableName, short unique, short accuracy)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), unique, accuracy);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00091700 File Offset: 0x0008F900
		internal ODBC32.RetCode Statistics(string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, null, 0, null, 0, tableName, ODBC.ShortStringLength(tableName), 0, 1);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00091730 File Offset: 0x0008F930
		internal ODBC32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLTablesW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), tableType, ODBC.ShortStringLength(tableType));
			ODBC.TraceODBC(3, "SQLTablesW", retCode);
			return retCode;
		}
	}
}
