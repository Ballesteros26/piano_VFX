using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x02000260 RID: 608
	internal static class ODBC
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00086D33 File Offset: 0x00084F33
		internal static Exception ConnectionClosed()
		{
			return ADP.InvalidOperation(SR.GetString("The connection is closed."));
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00086D44 File Offset: 0x00084F44
		internal static Exception OpenConnectionNoOwner()
		{
			return ADP.InvalidOperation(SR.GetString("An internal connection does not have an owner."));
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00086D55 File Offset: 0x00084F55
		internal static Exception UnknownSQLType(ODBC32.SQL_TYPE sqltype)
		{
			return ADP.Argument(SR.GetString("Unknown SQL type - {0}.", new object[] { sqltype.ToString() }));
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00086D7C File Offset: 0x00084F7C
		internal static Exception ConnectionStringTooLong()
		{
			return ADP.Argument(SR.GetString("Connection string exceeds maximum allowed length of {0}.", new object[] { 1024 }));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00086DA0 File Offset: 0x00084FA0
		internal static ArgumentException GetSchemaRestrictionRequired()
		{
			return ADP.Argument(SR.GetString("The ODBC managed provider requires that the TABLE_NAME restriction be specified and non-null for the GetSchema indexes collection."));
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00086DB1 File Offset: 0x00084FB1
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework Odbc Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00086DE6 File Offset: 0x00084FE6
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00086DF8 File Offset: 0x00084FF8
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00086E0A File Offset: 0x0008500A
		internal static InvalidOperationException NoMappingForSqlTransactionLevel(int value)
		{
			return ADP.DataAdapter(SR.GetString("No valid mapping for a SQL_TRANSACTION '{0}' to a System.Data.IsolationLevel enumeration value.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00086E30 File Offset: 0x00085030
		internal static Exception NegativeArgument()
		{
			return ADP.Argument(SR.GetString("Invalid negative argument!"));
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00086E41 File Offset: 0x00085041
		internal static Exception CantSetPropertyOnOpenConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Can't set property on an open connection."));
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00086E52 File Offset: 0x00085052
		internal static Exception CantEnableConnectionpooling(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to enable connection pooling...", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00086E72 File Offset: 0x00085072
		internal static Exception CantAllocateEnvironmentHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to allocate an environment handle.", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00086E92 File Offset: 0x00085092
		internal static Exception FailedToGetDescriptorHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to get descriptor handle.", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00086EB2 File Offset: 0x000850B2
		internal static Exception NotInTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Not in a transaction"));
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00086EC3 File Offset: 0x000850C3
		internal static Exception UnknownOdbcType(OdbcType odbctype)
		{
			return ADP.InvalidEnumerationValue(typeof(OdbcType), (int)odbctype);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00005E03 File Offset: 0x00004003
		internal static void TraceODBC(int level, string method, ODBC32.RetCode retcode)
		{
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00086ED5 File Offset: 0x000850D5
		internal static short ShortStringLength(string inputString)
		{
			return checked((short)ADP.StringLength(inputString));
		}

		// Token: 0x04001337 RID: 4919
		internal const string Pwd = "pwd";
	}
}
