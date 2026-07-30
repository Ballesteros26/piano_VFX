using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001F4 RID: 500
	internal static class SQL
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x0007108F File Offset: 0x0006F28F
		internal static Exception CannotGetDTCAddress()
		{
			return ADP.InvalidOperation(SR.GetString("Unable to get the address of the distributed transaction coordinator for the server, from the server.  Is DTC enabled on the server?"));
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000710A0 File Offset: 0x0006F2A0
		internal static Exception InvalidInternalPacketSize(string str)
		{
			return ADP.ArgumentOutOfRange(str);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000710A8 File Offset: 0x0006F2A8
		internal static Exception InvalidPacketSize()
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Invalid Packet Size."));
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000710B9 File Offset: 0x0006F2B9
		internal static Exception InvalidPacketSizeValue()
		{
			return ADP.Argument(SR.GetString("Invalid 'Packet Size'.  The value must be an integer >= 512 and <= 32768."));
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000710CA File Offset: 0x0006F2CA
		internal static Exception InvalidSSPIPacketSize()
		{
			return ADP.Argument(SR.GetString("Invalid SSPI packet size."));
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000710DB File Offset: 0x0006F2DB
		internal static Exception NullEmptyTransactionName()
		{
			return ADP.Argument(SR.GetString("Invalid transaction or invalid name for a point at which to save within the transaction."));
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000710EC File Offset: 0x0006F2EC
		internal static Exception UserInstanceFailoverNotCompatible()
		{
			return ADP.Argument(SR.GetString("User Instance and Failover are not compatible options.  Please choose only one of the two in the connection string."));
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000710FD File Offset: 0x0006F2FD
		internal static Exception InvalidSQLServerVersionUnknown()
		{
			return ADP.DataAdapter(SR.GetString("Unsupported SQL Server version.  The .Net Framework SqlClient Data Provider can only be used with SQL Server versions 7.0 and later."));
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0007110E File Offset: 0x0006F30E
		internal static Exception SynchronousCallMayNotPend()
		{
			return new Exception(SR.GetString("Internal Error"));
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0007111F File Offset: 0x0006F31F
		internal static Exception ConnectionLockedForBcpEvent()
		{
			return ADP.InvalidOperation(SR.GetString("The connection cannot be used because there is an ongoing operation that must be finished."));
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00071130 File Offset: 0x0006F330
		internal static Exception InstanceFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Instance failure."));
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00071141 File Offset: 0x0006F341
		internal static Exception GlobalTransactionsNotEnabled()
		{
			return ADP.InvalidOperation(SR.GetString("Global Transactions are not enabled for this Azure SQL Database. Please contact Azure SQL Database support for assistance."));
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00071152 File Offset: 0x0006F352
		internal static Exception UnknownSysTxIsolationLevel(IsolationLevel isolationLevel)
		{
			return ADP.InvalidOperation(SR.GetString("Unrecognized System.Transactions.IsolationLevel enumeration value: {0}.", new object[] { isolationLevel.ToString() }));
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00071179 File Offset: 0x0006F379
		internal static Exception InvalidPartnerConfiguration(string server, string database)
		{
			return ADP.InvalidOperation(SR.GetString("Server {0}, database {1} is not configured for database mirroring.", new object[] { server, database }));
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00071198 File Offset: 0x0006F398
		internal static Exception MARSUnspportedOnConnection()
		{
			return ADP.InvalidOperation(SR.GetString("The connection does not support MultipleActiveResultSets."));
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000711A9 File Offset: 0x0006F3A9
		internal static Exception CannotModifyPropertyAsyncOperationInProgress([CallerMemberName] string property = "")
		{
			return ADP.InvalidOperation(SR.GetString("{0} cannot be changed while async operation is in progress.", new object[] { property }));
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000711C4 File Offset: 0x0006F3C4
		internal static Exception NonLocalSSEInstance()
		{
			return ADP.NotSupported(SR.GetString("SSE Instance re-direction is not supported for non-local user instances."));
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000711D5 File Offset: 0x0006F3D5
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework SqlClient Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0007120A File Offset: 0x0006F40A
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0007121C File Offset: 0x0006F41C
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0007122E File Offset: 0x0006F42E
		internal static Exception OperationCancelled()
		{
			return ADP.InvalidOperation(SR.GetString("Operation cancelled by user."));
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0007123F File Offset: 0x0006F43F
		internal static Exception PendingBeginXXXExists()
		{
			return ADP.InvalidOperation(SR.GetString("The command execution cannot proceed due to a pending asynchronous operation already in progress."));
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00071250 File Offset: 0x0006F450
		internal static ArgumentOutOfRangeException InvalidSqlDependencyTimeout(string param)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Timeout specified is invalid. Timeout cannot be < 0."), param);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00071262 File Offset: 0x0006F462
		internal static Exception NonXmlResult()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid command sent to ExecuteXmlReader.  The command must return an Xml result."));
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00071273 File Offset: 0x0006F473
		internal static Exception InvalidParameterTypeNameFormat()
		{
			return ADP.Argument(SR.GetString("Invalid 3 part name format for TypeName."));
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00071284 File Offset: 0x0006F484
		internal static Exception InvalidParameterNameLength(string value)
		{
			return ADP.Argument(SR.GetString("The length of the parameter '{0}' exceeds the limit of 128 characters.", new object[] { value }));
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0007129F File Offset: 0x0006F49F
		internal static Exception PrecisionValueOutOfRange(byte precision)
		{
			return ADP.Argument(SR.GetString("Precision value '{0}' is either less than 0 or greater than the maximum allowed precision of 38.", new object[] { precision.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000712C5 File Offset: 0x0006F4C5
		internal static Exception ScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 38.", new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000712EB File Offset: 0x0006F4EB
		internal static Exception TimeScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 7.", new object[] { scale.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00071311 File Offset: 0x0006F511
		internal static Exception InvalidSqlDbType(SqlDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SqlDbType), (int)value);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00071323 File Offset: 0x0006F523
		internal static Exception UnsupportedTVPOutputParameter(ParameterDirection direction, string paramName)
		{
			return ADP.NotSupported(SR.GetString("ParameterDirection '{0}' specified for parameter '{1}' is not supported. Table-valued parameters only support ParameterDirection.Input.", new object[]
			{
				direction.ToString(),
				paramName
			}));
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0007134E File Offset: 0x0006F54E
		internal static Exception DBNullNotSupportedForTVPValues(string paramName)
		{
			return ADP.NotSupported(SR.GetString("DBNull value for parameter '{0}' is not supported. Table-valued parameters cannot be DBNull.", new object[] { paramName }));
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00071369 File Offset: 0x0006F569
		internal static Exception UnexpectedTypeNameForNonStructParams(string paramName)
		{
			return ADP.NotSupported(SR.GetString("TypeName specified for parameter '{0}'.  TypeName must only be set for Structured parameters.", new object[] { paramName }));
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00071384 File Offset: 0x0006F584
		internal static Exception ParameterInvalidVariant(string paramName)
		{
			return ADP.InvalidOperation(SR.GetString("Parameter '{0}' exceeds the size limit for the sql_variant datatype.", new object[] { paramName }));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0007139F File Offset: 0x0006F59F
		internal static Exception MustSetTypeNameForParam(string paramType, string paramName)
		{
			return ADP.Argument(SR.GetString("The {0} type parameter '{1}' must have a valid type name.", new object[] { paramType, paramName }));
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000713BE File Offset: 0x0006F5BE
		internal static Exception NullSchemaTableDataTypeNotSupported(string columnName)
		{
			return ADP.Argument(SR.GetString("DateType column for field '{0}' in schema table is null.  DataType must be non-null.", new object[] { columnName }));
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000713D9 File Offset: 0x0006F5D9
		internal static Exception InvalidSchemaTableOrdinals()
		{
			return ADP.Argument(SR.GetString("Invalid column ordinals in schema table.  ColumnOrdinals, if present, must not have duplicates or gaps."));
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000713EA File Offset: 0x0006F5EA
		internal static Exception EnumeratedRecordMetaDataChanged(string fieldName, int recordNumber)
		{
			return ADP.Argument(SR.GetString("Metadata for field '{0}' of record '{1}' did not match the original record's metadata.", new object[] { fieldName, recordNumber }));
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0007140E File Offset: 0x0006F60E
		internal static Exception EnumeratedRecordFieldCountChanged(int recordNumber)
		{
			return ADP.Argument(SR.GetString("Number of fields in record '{0}' does not match the number in the original record.", new object[] { recordNumber }));
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0007142E File Offset: 0x0006F62E
		internal static Exception InvalidTDSVersion()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server instance returned an invalid or unsupported protocol version during login negotiation."));
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0007143F File Offset: 0x0006F63F
		internal static Exception ParsingError()
		{
			return ADP.InvalidOperation(SR.GetString("Internal connection fatal error."));
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00071450 File Offset: 0x0006F650
		internal static Exception MoneyOverflow(string moneyValue)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallMoney overflow.  Value '{0}' is out of range.  Must be between -214,748.3648 and 214,748.3647.", new object[] { moneyValue }));
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0007146B File Offset: 0x0006F66B
		internal static Exception SmallDateTimeOverflow(string datetime)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallDateTime overflow.  Value '{0}' is out of range.  Must be between 1/1/1900 12:00:00 AM and 6/6/2079 11:59:59 PM.", new object[] { datetime }));
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00071486 File Offset: 0x0006F686
		internal static Exception SNIPacketAllocationFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Memory allocation for internal connection failed."));
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00071497 File Offset: 0x0006F697
		internal static Exception TimeOverflow(string time)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.Time overflow.  Value '{0}' is out of range.  Must be between 00:00:00.0000000 and 23:59:59.9999999.", new object[] { time }));
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000714B2 File Offset: 0x0006F6B2
		internal static Exception InvalidRead()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read when no data is present."));
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000714C3 File Offset: 0x0006F6C3
		internal static Exception NonBlobColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetBytes on column '{0}'.  The GetBytes function can only be used on columns of type Text, NText, or Image.", new object[] { columnName }));
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000714DE File Offset: 0x0006F6DE
		internal static Exception NonCharColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetChars on column '{0}'.  The GetChars function can only be used on columns of type Text, NText, Xml, VarChar or NVarChar.", new object[] { columnName }));
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000714F9 File Offset: 0x0006F6F9
		internal static Exception StreamNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetStream on column '{0}'. The GetStream function can only be used on columns of type Binary, Image, Udt or VarBinary.", new object[] { columnName }));
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00071514 File Offset: 0x0006F714
		internal static Exception TextReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetTextReader on column '{0}'. The GetTextReader function can only be used on columns of type Char, NChar, NText, NVarChar, Text or VarChar.", new object[] { columnName }));
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0007152F File Offset: 0x0006F72F
		internal static Exception XmlReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetXmlReader on column '{0}'. The GetXmlReader function can only be used on columns of type Xml.", new object[] { columnName }));
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0007154A File Offset: 0x0006F74A
		internal static Exception SqlCommandHasExistingSqlNotificationRequest()
		{
			return ADP.InvalidOperation(SR.GetString("This SqlCommand object is already associated with another SqlDependency object."));
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0007155B File Offset: 0x0006F75B
		internal static Exception SqlDepDefaultOptionsButNoStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called prior to execution of a command added to the SqlDependency instance."));
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0007156C File Offset: 0x0006F76C
		internal static Exception SqlDependencyDatabaseBrokerDisabled()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server Service Broker for the current database is not enabled, and as a result query notifications are not supported.  Please enable the Service Broker for this database if you wish to use notifications."));
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0007157D File Offset: 0x0006F77D
		internal static Exception SqlDependencyEventNoDuplicate()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.OnChange does not support multiple event registrations for the same delegate."));
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0007158E File Offset: 0x0006F78E
		internal static Exception SqlDependencyDuplicateStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency does not support calling Start() with different connection strings having the same server, user, and database in the same app domain."));
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0007159F File Offset: 0x0006F79F
		internal static Exception SqlDependencyIdMismatch()
		{
			return ADP.InvalidOperation(SR.GetString("No SqlDependency exists for the key."));
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000715B0 File Offset: 0x0006F7B0
		internal static Exception SqlDependencyNoMatchingServerStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called for each server that is being executed against."));
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000715C1 File Offset: 0x0006F7C1
		internal static Exception SqlDependencyNoMatchingServerDatabaseStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.Start has been called for the server the command is executing against more than once, but there is no matching server/user/database Start() call for current command."));
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000715D2 File Offset: 0x0006F7D2
		internal static TransactionPromotionException PromotionFailed(Exception inner)
		{
			TransactionPromotionException ex = new TransactionPromotionException(SR.GetString("Failure while attempting to promote transaction."), inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000715EA File Offset: 0x0006F7EA
		internal static Exception InvalidSqlDbTypeForConstructor(SqlDbType type)
		{
			return ADP.Argument(SR.GetString("The dbType {0} is invalid for this constructor.", new object[] { type.ToString() }));
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00071611 File Offset: 0x0006F811
		internal static Exception NameTooLong(string parameterName)
		{
			return ADP.Argument(SR.GetString("The name is too long."), parameterName);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00071623 File Offset: 0x0006F823
		internal static Exception InvalidSortOrder(SortOrder order)
		{
			return ADP.InvalidEnumerationValue(typeof(SortOrder), (int)order);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00071635 File Offset: 0x0006F835
		internal static Exception MustSpecifyBothSortOrderAndOrdinal(SortOrder order, int ordinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort order and ordinal must either both be specified, or neither should be specified (SortOrder.Unspecified and -1).  The values given were: order = {0}, ordinal = {1}.", new object[]
			{
				order.ToString(),
				ordinal
			}));
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00071665 File Offset: 0x0006F865
		internal static Exception UnsupportedColumnTypeForSqlProvider(string columnName, string typeName)
		{
			return ADP.Argument(SR.GetString("The type of column '{0}' is not supported.  The type is '{1}'", new object[] { columnName, typeName }));
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00071684 File Offset: 0x0006F884
		internal static Exception InvalidColumnMaxLength(string columnName, long maxLength)
		{
			return ADP.Argument(SR.GetString("The size of column '{0}' is not supported. The size is {1}.", new object[] { columnName, maxLength }));
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000716A8 File Offset: 0x0006F8A8
		internal static Exception InvalidColumnPrecScale()
		{
			return ADP.Argument(SR.GetString("Invalid numeric precision/scale."));
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000716B9 File Offset: 0x0006F8B9
		internal static Exception NotEnoughColumnsInStructuredType()
		{
			return ADP.Argument(SR.GetString("There are not enough fields in the Structured type.  Structured types must have at least one field."));
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000716CA File Offset: 0x0006F8CA
		internal static Exception DuplicateSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was specified twice.", new object[] { sortOrdinal }));
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000716EA File Offset: 0x0006F8EA
		internal static Exception MissingSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was not specified.", new object[] { sortOrdinal }));
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0007170A File Offset: 0x0006F90A
		internal static Exception SortOrdinalGreaterThanFieldCount(int columnOrdinal, int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} on field {1} exceeds the total number of fields.", new object[] { sortOrdinal, columnOrdinal }));
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00071733 File Offset: 0x0006F933
		internal static Exception IEnumerableOfSqlDataRecordHasNoRows()
		{
			return ADP.Argument(SR.GetString("There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead."));
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00071744 File Offset: 0x0006F944
		internal static Exception BulkLoadMappingInaccessible()
		{
			return ADP.InvalidOperation(SR.GetString("The mapped collection is in use and cannot be accessed at this time;"));
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00071755 File Offset: 0x0006F955
		internal static Exception BulkLoadMappingsNamesOrOrdinalsOnly()
		{
			return ADP.InvalidOperation(SR.GetString("Mappings must be either all name or all ordinal based."));
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00071766 File Offset: 0x0006F966
		internal static Exception BulkLoadCannotConvertValue(Type sourcetype, MetaType metatype, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given value of type {0} from the data source cannot be converted to type {1} of the specified target column.", new object[] { sourcetype.Name, metatype.TypeName }), e);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00071790 File Offset: 0x0006F990
		internal static Exception BulkLoadNonMatchingColumnMapping()
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnMapping does not match up with any column in the source or destination."));
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000717A1 File Offset: 0x0006F9A1
		internal static Exception BulkLoadNonMatchingColumnName(string columnName)
		{
			return SQL.BulkLoadNonMatchingColumnName(columnName, null);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000717AA File Offset: 0x0006F9AA
		internal static Exception BulkLoadNonMatchingColumnName(string columnName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnName '{0}' does not match up with any column in data source.", new object[] { columnName }), e);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000717C6 File Offset: 0x0006F9C6
		internal static Exception BulkLoadStringTooLong()
		{
			return ADP.InvalidOperation(SR.GetString("String or binary data would be truncated."));
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000717D7 File Offset: 0x0006F9D7
		internal static Exception BulkLoadInvalidVariantValue()
		{
			return ADP.InvalidOperation(SR.GetString("Value cannot be converted to SqlVariant."));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000717E8 File Offset: 0x0006F9E8
		internal static Exception BulkLoadInvalidTimeout(int timeout)
		{
			return ADP.Argument(SR.GetString("Timeout Value '{0}' is less than 0.", new object[] { timeout.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0007180E File Offset: 0x0006FA0E
		internal static Exception BulkLoadExistingTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Unexpected existing transaction."));
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0007181F File Offset: 0x0006FA1F
		internal static Exception BulkLoadNoCollation()
		{
			return ADP.InvalidOperation(SR.GetString("Failed to obtain column collation information for the destination table. If the table is not in the current database the name must be qualified using the database name (e.g. [mydb]..[mytable](e.g. [mydb]..[mytable]); this also applies to temporary-tables (e.g. #mytable would be specified as tempdb..#mytable)."));
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00071830 File Offset: 0x0006FA30
		internal static Exception BulkLoadConflictingTransactionOption()
		{
			return ADP.Argument(SR.GetString("Must not specify SqlBulkCopyOption.UseInternalTransaction and pass an external Transaction at the same time."));
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00071841 File Offset: 0x0006FA41
		internal static Exception BulkLoadLcidMismatch(int sourceLcid, string sourceColumnName, int destinationLcid, string destinationColumnName)
		{
			return ADP.InvalidOperation(SR.GetString("The locale id '{0}' of the source column '{1}' and the locale id '{2}' of the destination column '{3}' do not match.", new object[] { sourceLcid, sourceColumnName, destinationLcid, destinationColumnName }));
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00071872 File Offset: 0x0006FA72
		internal static Exception InvalidOperationInsideEvent()
		{
			return ADP.InvalidOperation(SR.GetString("Function must not be called during event."));
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00071883 File Offset: 0x0006FA83
		internal static Exception BulkLoadMissingDestinationTable()
		{
			return ADP.InvalidOperation(SR.GetString("The DestinationTableName property must be set before calling this method."));
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00071894 File Offset: 0x0006FA94
		internal static Exception BulkLoadInvalidDestinationTable(string tableName, Exception inner)
		{
			return ADP.InvalidOperation(SR.GetString("Cannot access destination table '{0}'.", new object[] { tableName }), inner);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000718B0 File Offset: 0x0006FAB0
		internal static Exception BulkLoadBulkLoadNotAllowDBNull(string columnName)
		{
			return ADP.InvalidOperation(SR.GetString("Column '{0}' does not allow DBNull.Value.", new object[] { columnName }));
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000718CB File Offset: 0x0006FACB
		internal static Exception BulkLoadPendingOperation()
		{
			return ADP.InvalidOperation(SR.GetString("Attempt to invoke bulk copy on an object that has a pending operation."));
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000718DC File Offset: 0x0006FADC
		internal static Exception InvalidTableDerivedPrecisionForTvp(string columnName, byte precision)
		{
			return ADP.InvalidOperation(SR.GetString("Precision '{0}' required to send all values in column '{1}' exceeds the maximum supported precision '{2}'. The values must all fit in a single precision.", new object[]
			{
				precision,
				columnName,
				SqlDecimal.MaxPrecision
			}));
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0007190D File Offset: 0x0006FB0D
		internal static Exception ConnectionDoomed()
		{
			return ADP.InvalidOperation(SR.GetString("The requested operation cannot be completed because the connection has been broken."));
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007191E File Offset: 0x0006FB1E
		internal static Exception OpenResultCountExceeded()
		{
			return ADP.InvalidOperation(SR.GetString("Open result count exceeded."));
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0007192F File Offset: 0x0006FB2F
		internal static Exception UnsupportedSysTxForGlobalTransactions()
		{
			return ADP.InvalidOperation(SR.GetString("The currently loaded System.Transactions.dll does not support Global Transactions."));
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00071940 File Offset: 0x0006FB40
		internal static Exception MultiSubnetFailoverWithFailoverPartner(bool serverProvidedFailoverPartner, SqlInternalConnectionTds internalConnection)
		{
			string @string = SR.GetString("Connecting to a mirrored SQL Server instance using the MultiSubnetFailover connection option is not supported.");
			if (serverProvidedFailoverPartner)
			{
				SqlException ex = SqlException.CreateException(new SqlErrorCollection
				{
					new SqlError(0, 0, 20, null, @string, "", 0, null)
				}, null, internalConnection, null);
				ex._doNotReconnect = true;
				return ex;
			}
			return ADP.Argument(@string);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0007198E File Offset: 0x0006FB8E
		internal static Exception MultiSubnetFailoverWithMoreThan64IPs()
		{
			return ADP.InvalidOperation(SQL.GetSNIErrorMessage(47));
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0007199C File Offset: 0x0006FB9C
		internal static Exception MultiSubnetFailoverWithInstanceSpecified()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(48));
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000719AA File Offset: 0x0006FBAA
		internal static Exception MultiSubnetFailoverWithNonTcpProtocol()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(49));
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000719B8 File Offset: 0x0006FBB8
		internal static Exception ROR_FailoverNotSupportedConnString()
		{
			return ADP.Argument(SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."));
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000719CC File Offset: 0x0006FBCC
		internal static Exception ROR_FailoverNotSupportedServer(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00071A10 File Offset: 0x0006FC10
		internal static Exception ROR_RecursiveRoutingNotSupported(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Two or more redirections have occurred. Only one redirection per login is allowed."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00071A54 File Offset: 0x0006FC54
		internal static Exception ROR_UnexpectedRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Unexpected routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00071A98 File Offset: 0x0006FC98
		internal static Exception ROR_InvalidRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Invalid routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00071ADC File Offset: 0x0006FCDC
		internal static Exception ROR_TimeoutAfterRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Server provided routing information, but timeout already expired."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00071B20 File Offset: 0x0006FD20
		internal static SqlException CR_ReconnectTimeout()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, SQLMessage.Timeout(), "", 0, 258U, null)
			}, "");
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00071B60 File Offset: 0x0006FD60
		internal static SqlException CR_ReconnectionCancelled()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SQLMessage.OperationCancelled(), "", 0, null)
			}, "");
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00071B98 File Offset: 0x0006FD98
		internal static Exception CR_NextAttemptWillExceedQueryTimeout(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("Next reconnection attempt will exceed query timeout. Reconnection was terminated."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00071BD8 File Offset: 0x0006FDD8
		internal static Exception CR_EncryptionChanged(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve SSL encryption during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00071C18 File Offset: 0x0006FE18
		internal static SqlException CR_AllAttemptsFailed(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("The connection is broken and recovery is not possible.  The client driver attempted to recover the connection one or more times and all attempts failed.  Increase the value of ConnectRetryCount to increase the number of recovery attempts."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00071C58 File Offset: 0x0006FE58
		internal static SqlException CR_NoCRAckAtReconnection(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not acknowledge a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00071C98 File Offset: 0x0006FE98
		internal static SqlException CR_TDSVersionNotPreserved(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00071CD8 File Offset: 0x0006FED8
		internal static SqlException CR_UnrecoverableServer(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the server as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00071D18 File Offset: 0x0006FF18
		internal static SqlException CR_UnrecoverableClient(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the client driver as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00071D57 File Offset: 0x0006FF57
		internal static Exception StreamWriteNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support writing."));
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00071D68 File Offset: 0x0006FF68
		internal static Exception StreamReadNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support reading."));
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00071D79 File Offset: 0x0006FF79
		internal static Exception StreamSeekNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support seeking."));
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00071D8A File Offset: 0x0006FF8A
		internal static SqlNullValueException SqlNullValue()
		{
			return new SqlNullValueException();
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00071D91 File Offset: 0x0006FF91
		internal static Exception SubclassMustOverride()
		{
			return ADP.InvalidOperation(SR.GetString("Subclass did not override a required method."));
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00071DA2 File Offset: 0x0006FFA2
		internal static Exception UnsupportedKeyword(string keyword)
		{
			return ADP.NotSupported(SR.GetString("The keyword '{0}' is not supported on this platform.", new object[] { keyword }));
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00071DBD File Offset: 0x0006FFBD
		internal static Exception NetworkLibraryKeywordNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The keyword 'Network Library' is not supported on this platform, prefix the 'Data Source' with the protocol desired instead ('tcp:' for a TCP connection, or 'np:' for a Named Pipe connection)."));
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00071DD0 File Offset: 0x0006FFD0
		internal static Exception UnsupportedFeatureAndToken(SqlInternalConnectionTds internalConnection, string token)
		{
			NotSupportedException ex = ADP.NotSupported(SR.GetString("Received an unsupported token '{0}' while reading data from the server.", new object[] { token }));
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server is attempting to use a feature that is not supported on this platform."), "", 0, null)
			}, "", internalConnection, ex);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00071E29 File Offset: 0x00070029
		internal static Exception BatchedUpdatesNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Batching updates is not supported on the context connection."));
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00071E3A File Offset: 0x0007003A
		internal static string GetSNIErrorMessage(int sniError)
		{
			string text = string.Format(null, "SNI_ERROR_{0}", sniError);
			return SR.GetResourceString(text, text);
		}

		// Token: 0x04000F21 RID: 3873
		internal static readonly byte[] AttentionHeader = new byte[] { 6, 1, 0, 8, 0, 0, 0, 0 };

		// Token: 0x04000F22 RID: 3874
		internal const int SqlDependencyTimeoutDefault = 0;

		// Token: 0x04000F23 RID: 3875
		internal const int SqlDependencyServerTimeout = 432000;

		// Token: 0x04000F24 RID: 3876
		internal const string SqlNotificationServiceDefault = "SqlQueryNotificationService";

		// Token: 0x04000F25 RID: 3877
		internal const string SqlNotificationStoredProcedureDefault = "SqlQueryNotificationStoredProcedure";
	}
}
