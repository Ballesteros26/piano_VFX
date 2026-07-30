using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.Common
{
	// Token: 0x0200031A RID: 794
	internal static class ADP
	{
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000A3C06 File Offset: 0x000A1E06
		internal static Task<bool> TrueTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP._trueTask) == null)
				{
					task = (ADP._trueTask = Task.FromResult<bool>(true));
				}
				return task;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000A3C1D File Offset: 0x000A1E1D
		internal static Task<bool> FalseTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP._falseTask) == null)
				{
					task = (ADP._falseTask = Task.FromResult<bool>(false));
				}
				return task;
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00014682 File Offset: 0x00012882
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000A3C34 File Offset: 0x000A1E34
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000A3C41 File Offset: 0x000A1E41
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000A3C4E File Offset: 0x000A1E4E
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000A3C5C File Offset: 0x000A1E5C
		internal static ArgumentException Argument(string error, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000A3C6B File Offset: 0x000A1E6B
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A3C7A File Offset: 0x000A1E7A
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000A3C88 File Offset: 0x000A1E88
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000A3C97 File Offset: 0x000A1E97
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000A3CA5 File Offset: 0x000A1EA5
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000A3CB4 File Offset: 0x000A1EB4
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A3CC2 File Offset: 0x000A1EC2
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000A3CCB File Offset: 0x000A1ECB
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000A3CDA File Offset: 0x000A1EDA
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000A3CE8 File Offset: 0x000A1EE8
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000A3CF5 File Offset: 0x000A1EF5
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000A3D04 File Offset: 0x000A1F04
		internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
		{
			int num = ((quotePrefix != null) ? quotePrefix.Length : 0);
			int num2 = ((quoteSuffix != null) ? quoteSuffix.Length : 0);
			if (num2 + num == 0)
			{
				unquotedString = quotedString;
				return true;
			}
			if (quotedString == null)
			{
				unquotedString = quotedString;
				return false;
			}
			int length = quotedString.Length;
			if (length < num + num2)
			{
				unquotedString = quotedString;
				return false;
			}
			if (num > 0 && !quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
			{
				unquotedString = quotedString;
				return false;
			}
			if (num2 > 0)
			{
				if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
				{
					unquotedString = quotedString;
					return false;
				}
				unquotedString = quotedString.Substring(num, length - (num + num2)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
			}
			else
			{
				unquotedString = quotedString.Substring(num, length - num);
			}
			return true;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000A3D9F File Offset: 0x000A1F9F
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, string value, string method)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is not supported by the {2} method.", type.Name, value, method), type.Name);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000A3DBE File Offset: 0x000A1FBE
		internal static InvalidOperationException DataAdapter(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000A3DBE File Offset: 0x000A1FBE
		private static InvalidOperationException Provider(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000A3DC6 File Offset: 0x000A1FC6
		internal static ArgumentException InvalidMultipartName(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\".", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000A3DDF File Offset: 0x000A1FDF
		internal static ArgumentException InvalidMultipartNameIncorrectUsageOfQuotes(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", incorrect usage of quotes.", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000A3DF8 File Offset: 0x000A1FF8
		internal static ArgumentException InvalidMultipartNameToManyParts(string property, string value, int limit)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", the current limit of \"{2}\" is insufficient.", property, value, limit));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A3E17 File Offset: 0x000A2017
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull(parameterName);
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000A3E24 File Offset: 0x000A2024
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType && !ADP.s_securityType.IsAssignableFrom(type);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000A3E8C File Offset: 0x000A208C
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000A3EE1 File Offset: 0x000A20E1
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is invalid.", type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000A3F0A File Offset: 0x000A210A
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return ADP.Argument(SR.Format("Format of the initialization string does not conform to specification starting at index {0}.", index));
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000A3F21 File Offset: 0x000A2121
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return ADP.Argument(SR.Format("Keyword not supported: '{0}'.", keyword));
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000A3F33 File Offset: 0x000A2133
		internal static ArgumentException ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return ADP.Argument(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", fromType.FullName, toType.FullName), innerException);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000A3F51 File Offset: 0x000A2151
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return ADP.InvalidConnectionOptionValue(key, null);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000A3F5A File Offset: 0x000A215A
		internal static Exception InvalidConnectionOptionValue(string key, Exception inner)
		{
			return ADP.Argument(SR.Format("Invalid value for key '{0}'.", key), inner);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A3F6D File Offset: 0x000A216D
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(SR.Format("Attempted to remove an {0} that is not contained by this {1}.", itemType.Name, collection.GetType().Name));
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A3F8F File Offset: 0x000A218F
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, SR.Format("The {0} only accepts non-null {1} type objects.", collection.Name, itemType.Name));
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000A3FAD File Offset: 0x000A21AD
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(SR.Format("Invalid index {0} for this {1} with Count={2}.", index.ToString(CultureInfo.InvariantCulture), collection.Name, count.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000A3FDC File Offset: 0x000A21DC
		internal static IndexOutOfRangeException CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return ADP.IndexOutOfRange(SR.Format("An {0} with {1} '{2}' is not contained by this {3}.", new object[] { itemType.Name, propertyName, propertyValue, collection.Name }));
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000A400D File Offset: 0x000A220D
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(SR.Format("The {0} only accepts non-null {1} type objects, not {2} objects.", collection.Name, itemType.Name, invalidValue.GetType().Name));
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000A4038 File Offset: 0x000A2238
		private static string ConnectionStateMsg(ConnectionState state)
		{
			switch (state)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				return "The connection's current state is open.";
			case ConnectionState.Connecting:
				return "The connection's current state is connecting.";
			case ConnectionState.Open | ConnectionState.Connecting:
			case ConnectionState.Executing:
				goto IL_0046;
			case ConnectionState.Open | ConnectionState.Executing:
				return "The connection's current state is executing.";
			default:
				if (state == (ConnectionState.Open | ConnectionState.Fetching))
				{
					return "The connection's current state is fetching.";
				}
				if (state != (ConnectionState.Connecting | ConnectionState.Broken))
				{
					goto IL_0046;
				}
				break;
			}
			return "The connection's current state is closed.";
			IL_0046:
			return SR.Format("The connection's current state: {0}.", state.ToString());
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000A40A2 File Offset: 0x000A22A2
		internal static Exception StreamClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to {0} when stream is closed.", method));
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A40B4 File Offset: 0x000A22B4
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(quotePrefix))
			{
				stringBuilder.Append(quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
			{
				stringBuilder.Append(unQuotedString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000A410C File Offset: 0x000A230C
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000A410C File Offset: 0x000A230C
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A412E File Offset: 0x000A232E
		internal static Exception InternalError(ADP.InternalErrorCode internalError)
		{
			return ADP.InvalidOperation(SR.Format("Internal .Net Framework Data Provider error {0}.", (int)internalError));
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A4145 File Offset: 0x000A2345
		internal static Exception DataReaderClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to call {0} when reader is closed.", method));
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A4157 File Offset: 0x000A2357
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid source buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), srcOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000A4181 File Offset: 0x000A2381
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid destination buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), dstOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000A41AB File Offset: 0x000A23AB
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return ADP.IndexOutOfRange(SR.Format("Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.", numBytes.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000A41D4 File Offset: 0x000A23D4
		internal static Exception InvalidDataLength(long length)
		{
			return ADP.IndexOutOfRange(SR.Format("Data length '{0}' is less than 0.", length.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000A41F1 File Offset: 0x000A23F1
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000A4208 File Offset: 0x000A2408
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A421D File Offset: 0x000A241D
		internal static bool IsEmptyArray(string[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000A422C File Offset: 0x000A242C
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000A4258 File Offset: 0x000A2458
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange("Specified SeekOrigin value is invalid.", parameterName);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000A4265 File Offset: 0x000A2465
		internal static void SetCurrentTransaction(Transaction transaction)
		{
			Transaction.Current = transaction;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000A426D File Offset: 0x000A246D
		internal static Task<T> CreatedTaskWithCancellation<T>()
		{
			return Task.FromCanceled<T>(new CancellationToken(true));
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000A427A File Offset: 0x000A247A
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000A4287 File Offset: 0x000A2487
		internal static DataException Data(string message)
		{
			DataException ex = new DataException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A4295 File Offset: 0x000A2495
		internal static void CheckArgumentLength(string value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty string for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A42B7 File Offset: 0x000A24B7
		internal static void CheckArgumentLength(Array value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty array for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A42D9 File Offset: 0x000A24D9
		internal static ArgumentOutOfRangeException InvalidAcceptRejectRule(AcceptRejectRule value)
		{
			return ADP.InvalidEnumerationValue(typeof(AcceptRejectRule), (int)value);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A42EB File Offset: 0x000A24EB
		internal static ArgumentOutOfRangeException InvalidCatalogLocation(CatalogLocation value)
		{
			return ADP.InvalidEnumerationValue(typeof(CatalogLocation), (int)value);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000A42FD File Offset: 0x000A24FD
		internal static ArgumentOutOfRangeException InvalidConflictOptions(ConflictOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(ConflictOption), (int)value);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000A430F File Offset: 0x000A250F
		internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowState), (int)value);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000A4321 File Offset: 0x000A2521
		internal static ArgumentOutOfRangeException InvalidKeyRestrictionBehavior(KeyRestrictionBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(KeyRestrictionBehavior), (int)value);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000A4333 File Offset: 0x000A2533
		internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(LoadOption), (int)value);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000A4345 File Offset: 0x000A2545
		internal static ArgumentOutOfRangeException InvalidMissingMappingAction(MissingMappingAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingMappingAction), (int)value);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000A4357 File Offset: 0x000A2557
		internal static ArgumentOutOfRangeException InvalidMissingSchemaAction(MissingSchemaAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingSchemaAction), (int)value);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000A4369 File Offset: 0x000A2569
		internal static ArgumentOutOfRangeException InvalidRule(Rule value)
		{
			return ADP.InvalidEnumerationValue(typeof(Rule), (int)value);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000A437B File Offset: 0x000A257B
		internal static ArgumentOutOfRangeException InvalidSchemaType(SchemaType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SchemaType), (int)value);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000A438D File Offset: 0x000A258D
		internal static ArgumentOutOfRangeException InvalidStatementType(StatementType value)
		{
			return ADP.InvalidEnumerationValue(typeof(StatementType), (int)value);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000A439F File Offset: 0x000A259F
		internal static ArgumentOutOfRangeException InvalidUpdateStatus(UpdateStatus value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateStatus), (int)value);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000A43B1 File Offset: 0x000A25B1
		internal static ArgumentOutOfRangeException NotSupportedStatementType(StatementType value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(StatementType), value.ToString(), method);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000A43D0 File Offset: 0x000A25D0
		internal static ArgumentException InvalidKeyname(string parameterName)
		{
			return ADP.Argument("Invalid keyword, contain one or more of 'no characters', 'control characters', 'leading or trailing whitespace' or 'leading semicolons'.", parameterName);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A43DD File Offset: 0x000A25DD
		internal static ArgumentException InvalidValue(string parameterName)
		{
			return ADP.Argument("The value contains embedded nulls (\\\\u0000).", parameterName);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000A43EA File Offset: 0x000A25EA
		internal static Exception WrongType(Type got, Type expected)
		{
			return ADP.Argument(SR.Format("Expecting argument of type {1}, but received type {0}.", got.ToString(), expected.ToString()));
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000A4407 File Offset: 0x000A2607
		internal static Exception CollectionUniqueValue(Type itemType, string propertyName, string propertyValue)
		{
			return ADP.Argument(SR.Format("The {0}.{1} is required to be unique, '{2}' already exists in the collection.", itemType.Name, propertyName, propertyValue));
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000A4420 File Offset: 0x000A2620
		internal static InvalidOperationException MissingSelectCommand(string method)
		{
			return ADP.Provider(SR.Format("The SelectCommand property has not been initialized before calling '{0}'.", method));
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000A3DBE File Offset: 0x000A1FBE
		private static InvalidOperationException DataMapping(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000A4432 File Offset: 0x000A2632
		internal static InvalidOperationException ColumnSchemaExpression(string srcColumn, string cacheColumn)
		{
			return ADP.DataMapping(SR.Format("The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.", srcColumn, cacheColumn));
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000A4445 File Offset: 0x000A2645
		internal static InvalidOperationException ColumnSchemaMismatch(string srcColumn, Type srcType, DataColumn column)
		{
			return ADP.DataMapping(SR.Format("Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.", new object[]
			{
				srcColumn,
				srcType.Name,
				column.ColumnName,
				column.DataType.Name
			}));
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000A4480 File Offset: 0x000A2680
		internal static InvalidOperationException ColumnSchemaMissing(string cacheColumn, string tableName, string srcColumn)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				return ADP.InvalidOperation(SR.Format("Missing the DataColumn '{0}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
			}
			return ADP.DataMapping(SR.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000A44AF File Offset: 0x000A26AF
		internal static InvalidOperationException MissingColumnMapping(string srcColumn)
		{
			return ADP.DataMapping(SR.Format("Missing SourceColumn mapping for '{0}'.", srcColumn));
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000A44C1 File Offset: 0x000A26C1
		internal static InvalidOperationException MissingTableSchema(string cacheTable, string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing the '{0}' DataTable for the '{1}' SourceTable.", cacheTable, srcTable));
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000A44D4 File Offset: 0x000A26D4
		internal static InvalidOperationException MissingTableMapping(string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing SourceTable mapping: '{0}'", srcTable));
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000A44E6 File Offset: 0x000A26E6
		internal static InvalidOperationException MissingTableMappingDestination(string dstTable)
		{
			return ADP.DataMapping(SR.Format("Missing TableMapping when TableMapping.DataSetTable='{0}'.", dstTable));
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000A44F8 File Offset: 0x000A26F8
		internal static Exception InvalidSourceColumn(string parameter)
		{
			return ADP.Argument("SourceColumn is required to be a non-empty string.", parameter);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000A4505 File Offset: 0x000A2705
		internal static Exception ColumnsAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataColumnMappingCollection), typeof(DataColumnMapping));
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000A4521 File Offset: 0x000A2721
		internal static Exception ColumnsDataSetColumn(string cacheColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "DataSetColumn", cacheColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000A4542 File Offset: 0x000A2742
		internal static Exception ColumnsIndexInt32(int index, IColumnMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000A4556 File Offset: 0x000A2756
		internal static Exception ColumnsIndexSource(string srcColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "SourceColumn", srcColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000A4577 File Offset: 0x000A2777
		internal static Exception ColumnsIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000A4589 File Offset: 0x000A2789
		internal static Exception ColumnsIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000A459B File Offset: 0x000A279B
		internal static Exception ColumnsUniqueSourceColumn(string srcColumn)
		{
			return ADP.CollectionUniqueValue(typeof(DataColumnMapping), "SourceColumn", srcColumn);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000A45B2 File Offset: 0x000A27B2
		internal static Exception NotADataColumnMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataColumnMappingCollection), typeof(DataColumnMapping), value);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000A45CE File Offset: 0x000A27CE
		internal static Exception InvalidSourceTable(string parameter)
		{
			return ADP.Argument("SourceTable is required to be a non-empty string", parameter);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A45DB File Offset: 0x000A27DB
		internal static Exception TablesAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataTableMappingCollection), typeof(DataTableMapping));
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A45F7 File Offset: 0x000A27F7
		internal static Exception TablesDataSetTable(string cacheTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "DataSetTable", cacheTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000A4542 File Offset: 0x000A2742
		internal static Exception TablesIndexInt32(int index, ITableMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000A4618 File Offset: 0x000A2818
		internal static Exception TablesIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000A462A File Offset: 0x000A282A
		internal static Exception TablesIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000A463C File Offset: 0x000A283C
		internal static Exception TablesSourceIndex(string srcTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "SourceTable", srcTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000A465D File Offset: 0x000A285D
		internal static Exception TablesUniqueSourceTable(string srcTable)
		{
			return ADP.CollectionUniqueValue(typeof(DataTableMapping), "SourceTable", srcTable);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000A4674 File Offset: 0x000A2874
		internal static Exception NotADataTableMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataTableMappingCollection), typeof(DataTableMapping), value);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000A4690 File Offset: 0x000A2890
		internal static InvalidOperationException UpdateConnectionRequired(StatementType statementType, bool isRowUpdatingCommand)
		{
			string text;
			if (!isRowUpdatingCommand)
			{
				switch (statementType)
				{
				case StatementType.Insert:
					text = "Update requires the InsertCommand to have a connection object. The Connection property of the InsertCommand has not been initialized.";
					goto IL_004A;
				case StatementType.Update:
					text = "Update requires the UpdateCommand to have a connection object. The Connection property of the UpdateCommand has not been initialized.";
					goto IL_004A;
				case StatementType.Delete:
					text = "Update requires the DeleteCommand to have a connection object. The Connection property of the DeleteCommand has not been initialized.";
					goto IL_004A;
				}
				throw ADP.InvalidStatementType(statementType);
			}
			text = "Update requires the command clone to have a connection object. The Connection property of the command clone has not been initialized.";
			IL_004A:
			return ADP.InvalidOperation(text);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000A46ED File Offset: 0x000A28ED
		internal static InvalidOperationException ConnectionRequired_Res(string method)
		{
			return ADP.InvalidOperation("ADP_ConnectionRequired_" + method);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000A4700 File Offset: 0x000A2900
		internal static InvalidOperationException UpdateOpenConnectionRequired(StatementType statementType, bool isRowUpdatingCommand, ConnectionState state)
		{
			string text;
			if (isRowUpdatingCommand)
			{
				text = "Update requires the updating command to have an open connection object. {1}";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Insert:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Update:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Delete:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(SR.Format(text, ADP.ConnectionStateMsg(state)));
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000A475E File Offset: 0x000A295E
		internal static ArgumentException UnwantedStatementType(StatementType statementType)
		{
			return ADP.Argument(SR.Format("The StatementType {0} is not expected here.", statementType.ToString()));
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000A477C File Offset: 0x000A297C
		internal static Exception FillSchemaRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("FillSchema: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000A4789 File Offset: 0x000A2989
		internal static Exception InvalidMaxRecords(string parameter, int max)
		{
			return ADP.Argument(SR.Format("The MaxRecords value of {0} is invalid; the value must be >= 0.", max.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000A47A7 File Offset: 0x000A29A7
		internal static Exception InvalidStartRecord(string parameter, int start)
		{
			return ADP.Argument(SR.Format("The StartRecord value of {0} is invalid; the value must be >= 0.", start.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000A47C5 File Offset: 0x000A29C5
		internal static Exception FillRequires(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000A47CD File Offset: 0x000A29CD
		internal static Exception FillRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("Fill: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000A47DA File Offset: 0x000A29DA
		internal static Exception FillChapterAutoIncrement()
		{
			return ADP.InvalidOperation("Hierarchical chapter columns must map to an AutoIncrement DataColumn.");
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000A47E6 File Offset: 0x000A29E6
		internal static InvalidOperationException MissingDataReaderFieldType(int index)
		{
			return ADP.DataAdapter(SR.Format("DataReader.GetFieldType({0}) returned null.", index));
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000A47FD File Offset: 0x000A29FD
		internal static InvalidOperationException OnlyOneTableForStartRecordOrMaxRecords()
		{
			return ADP.DataAdapter("Only specify one item in the dataTables array when using non-zero values for startRecords or maxRecords.");
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000A47C5 File Offset: 0x000A29C5
		internal static ArgumentNullException UpdateRequiresNonNullDataSet(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000A4809 File Offset: 0x000A2A09
		internal static InvalidOperationException UpdateRequiresSourceTable(string defaultSrcTableName)
		{
			return ADP.InvalidOperation(SR.Format("Update unable to find TableMapping['{0}'] or DataTable '{0}'.", defaultSrcTableName));
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000A481B File Offset: 0x000A2A1B
		internal static InvalidOperationException UpdateRequiresSourceTableName(string srcTable)
		{
			return ADP.InvalidOperation(SR.Format("Update: expected a non-empty SourceTable name.", srcTable));
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000A47C5 File Offset: 0x000A29C5
		internal static ArgumentNullException UpdateRequiresDataTable(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A4830 File Offset: 0x000A2A30
		internal static Exception UpdateConcurrencyViolation(StatementType statementType, int affected, int expected, DataRow[] dataRows)
		{
			string text;
			switch (statementType)
			{
			case StatementType.Update:
				text = "Concurrency violation: the UpdateCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Delete:
				text = "Concurrency violation: the DeleteCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Batch:
				text = "Concurrency violation: the batched command affected {0} of the expected {1} records.";
				break;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
			DBConcurrencyException ex = new DBConcurrencyException(SR.Format(text, affected.ToString(CultureInfo.InvariantCulture), expected.ToString(CultureInfo.InvariantCulture)), null, dataRows);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000A48A0 File Offset: 0x000A2AA0
		internal static InvalidOperationException UpdateRequiresCommand(StatementType statementType, bool isRowUpdatingCommand)
		{
			string text;
			if (isRowUpdatingCommand)
			{
				text = "Update requires the command clone to be valid.";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Select:
					text = "Auto SQL generation during Update requires a valid SelectCommand.";
					break;
				case StatementType.Insert:
					text = "Update requires a valid InsertCommand when passed DataRow collection with new rows.";
					break;
				case StatementType.Update:
					text = "Update requires a valid UpdateCommand when passed DataRow collection with modified rows.";
					break;
				case StatementType.Delete:
					text = "Update requires a valid DeleteCommand when passed DataRow collection with deleted rows.";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(text);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000A48FD File Offset: 0x000A2AFD
		internal static ArgumentException UpdateMismatchRowTable(int i)
		{
			return ADP.Argument(SR.Format("DataRow[{0}] is from a different DataTable than DataRow[0].", i.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000A491A File Offset: 0x000A2B1A
		internal static DataException RowUpdatedErrors()
		{
			return ADP.Data("RowUpdatedEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000A4926 File Offset: 0x000A2B26
		internal static DataException RowUpdatingErrors()
		{
			return ADP.Data("RowUpdatingEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000A4932 File Offset: 0x000A2B32
		internal static InvalidOperationException ResultsNotAllowedDuringBatch()
		{
			return ADP.DataAdapter("When batching, the command's UpdatedRowSource property value of UpdateRowSource.FirstReturnedRecord or UpdateRowSource.Both is invalid.");
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000A493E File Offset: 0x000A2B3E
		internal static InvalidOperationException DynamicSQLJoinUnsupported()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against multiple base tables.");
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000A494A File Offset: 0x000A2B4A
		internal static InvalidOperationException DynamicSQLNoTableInfo()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.");
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000A4956 File Offset: 0x000A2B56
		internal static InvalidOperationException DynamicSQLNoKeyInfoDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000A4962 File Offset: 0x000A2B62
		internal static InvalidOperationException DynamicSQLNoKeyInfoUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000A496E File Offset: 0x000A2B6E
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000A497A File Offset: 0x000A2B7A
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000A4986 File Offset: 0x000A2B86
		internal static InvalidOperationException DynamicSQLNestedQuote(string name, string quote)
		{
			return ADP.InvalidOperation(SR.Format("Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.", name, quote));
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000A4999 File Offset: 0x000A2B99
		internal static InvalidOperationException NoQuoteChange()
		{
			return ADP.InvalidOperation("The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.");
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000A49A5 File Offset: 0x000A2BA5
		internal static InvalidOperationException MissingSourceCommand()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand property needs to be initialized.");
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000A49B1 File Offset: 0x000A2BB1
		internal static InvalidOperationException MissingSourceCommandConnection()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand.Connection property needs to be initialized;");
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000A49C0 File Offset: 0x000A2BC0
		internal static DataRow[] SelectAdapterRows(DataTable dataTable, bool sorted)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			DataRowCollection rows = dataTable.Rows;
			foreach (object obj in rows)
			{
				DataRowState dataRowState = ((DataRow)obj).RowState;
				if (dataRowState != DataRowState.Added)
				{
					if (dataRowState != DataRowState.Deleted)
					{
						if (dataRowState == DataRowState.Modified)
						{
							num3++;
						}
					}
					else
					{
						num2++;
					}
				}
				else
				{
					num++;
				}
			}
			DataRow[] array = new DataRow[num + num2 + num3];
			if (sorted)
			{
				num3 = num + num2;
				num2 = num;
				num = 0;
				using (IEnumerator enumerator = rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataRow dataRow = (DataRow)obj2;
						DataRowState dataRowState = dataRow.RowState;
						if (dataRowState != DataRowState.Added)
						{
							if (dataRowState != DataRowState.Deleted)
							{
								if (dataRowState == DataRowState.Modified)
								{
									array[num3++] = dataRow;
								}
							}
							else
							{
								array[num2++] = dataRow;
							}
						}
						else
						{
							array[num++] = dataRow;
						}
					}
					return array;
				}
			}
			int num4 = 0;
			foreach (object obj3 in rows)
			{
				DataRow dataRow2 = (DataRow)obj3;
				if ((dataRow2.RowState & (DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					array[num4++] = dataRow2;
					if (num4 == array.Length)
					{
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000A4B4C File Offset: 0x000A2D4C
		internal static void BuildSchemaTableInfoTableNames(string[] columnNameArray)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNameArray.Length);
			int num = columnNameArray.Length;
			int num2 = columnNameArray.Length - 1;
			while (0 <= num2)
			{
				string text = columnNameArray[num2];
				if (text != null && 0 < text.Length)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					int num3;
					if (dictionary.TryGetValue(text, out num3))
					{
						num = Math.Min(num, num3);
					}
					dictionary[text] = num2;
				}
				else
				{
					columnNameArray[num2] = string.Empty;
					num = num2;
				}
				num2--;
			}
			int num4 = 1;
			for (int i = num; i < columnNameArray.Length; i++)
			{
				string text2 = columnNameArray[i];
				if (text2.Length == 0)
				{
					columnNameArray[i] = "Column";
					num4 = ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, num4);
				}
				else
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					if (i != dictionary[text2])
					{
						ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, 1);
					}
				}
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000A4C30 File Offset: 0x000A2E30
		private static int GenerateUniqueName(Dictionary<string, int> hash, ref string columnName, int index, int uniqueIndex)
		{
			string text;
			for (;;)
			{
				text = columnName + uniqueIndex.ToString(CultureInfo.InvariantCulture);
				string text2 = text.ToLower(CultureInfo.InvariantCulture);
				if (hash.TryAdd(text2, index))
				{
					break;
				}
				uniqueIndex++;
			}
			columnName = text;
			return uniqueIndex;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000A4C74 File Offset: 0x000A2E74
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000A4C84 File Offset: 0x000A2E84
		internal static Exception ExceptionWithStackTrace(Exception e)
		{
			try
			{
				throw e;
			}
			catch (Exception ex)
			{
			}
			Exception ex;
			return ex;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000A4CA8 File Offset: 0x000A2EA8
		internal static IndexOutOfRangeException IndexOutOfRange(int value)
		{
			return new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000A4CBB File Offset: 0x000A2EBB
		internal static IndexOutOfRangeException IndexOutOfRange()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000A4CC2 File Offset: 0x000A2EC2
		internal static TimeoutException TimeoutException(string error)
		{
			return new TimeoutException(error);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000A4CCA File Offset: 0x000A2ECA
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			return new InvalidOperationException(error, inner);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000A4CD3 File Offset: 0x000A2ED3
		internal static OverflowException Overflow(string error)
		{
			return ADP.Overflow(error, null);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000A4CDC File Offset: 0x000A2EDC
		internal static OverflowException Overflow(string error, Exception inner)
		{
			return new OverflowException(error, inner);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000A4CE5 File Offset: 0x000A2EE5
		internal static PlatformNotSupportedException DbTypeNotSupported(string dbType)
		{
			return new PlatformNotSupportedException(SR.GetString("Type {0} is not supported on this platform.", new object[] { dbType }));
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000A4D00 File Offset: 0x000A2F00
		internal static InvalidCastException InvalidCast()
		{
			return new InvalidCastException();
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000A4D07 File Offset: 0x000A2F07
		internal static IOException IO(string error)
		{
			return new IOException(error);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000A4D0F File Offset: 0x000A2F0F
		internal static IOException IO(string error, Exception inner)
		{
			return new IOException(error, inner);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000A4D18 File Offset: 0x000A2F18
		internal static ObjectDisposedException ObjectDisposed(object instance)
		{
			return new ObjectDisposedException(instance.GetType().Name);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000A4D2A File Offset: 0x000A2F2A
		internal static Exception DataTableDoesNotExist(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection '{0}' is missing from the metadata XML.", new object[] { collectionName }));
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A4D45 File Offset: 0x000A2F45
		internal static InvalidOperationException MethodCalledTwice(string method)
		{
			return new InvalidOperationException(SR.GetString("The method '{0}' cannot be called more than once for the same execution.", new object[] { method }));
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000A4D60 File Offset: 0x000A2F60
		internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000A4D72 File Offset: 0x000A2F72
		internal static ArgumentOutOfRangeException InvalidIsolationLevel(IsolationLevel value)
		{
			return ADP.InvalidEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000A4D84 File Offset: 0x000A2F84
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return ADP.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000A4D96 File Offset: 0x000A2F96
		internal static Exception TooManyRestrictions(string collectionName)
		{
			return ADP.Argument(SR.GetString("More restrictions were provided than the requested schema ('{0}') supports.", new object[] { collectionName }));
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000A4DB1 File Offset: 0x000A2FB1
		internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000A4DC3 File Offset: 0x000A2FC3
		internal static ArgumentException InvalidMinMaxPoolSizeValues()
		{
			return ADP.Argument(SR.GetString("Invalid min or max pool size values, min pool size cannot be greater than the max pool size."));
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000A4DD4 File Offset: 0x000A2FD4
		internal static InvalidOperationException NoConnectionString()
		{
			return ADP.InvalidOperation(SR.GetString("The ConnectionString property has not been initialized."));
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000A4DE5 File Offset: 0x000A2FE5
		internal static Exception MethodNotImplemented([CallerMemberName] string methodName = "")
		{
			return NotImplemented.ByDesignWithMessage(methodName);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A4DED File Offset: 0x000A2FED
		internal static Exception QueryFailed(string collectionName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("Unable to build the '{0}' collection because execution of the SQL query failed. See the inner exception for details.", new object[] { collectionName }), e);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A4E09 File Offset: 0x000A3009
		internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
		{
			return ADP.Argument(SR.GetString("The value's length for key '{0}' exceeds it's limit of '{1}'.", new object[] { key, limit }));
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000A4E2D File Offset: 0x000A302D
		internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
		{
			return ADP.Argument(SR.GetString("Use of key '{0}' requires the key '{1}' to be present.", new object[] { key, requiredAdditionalKey }));
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000A4E4C File Offset: 0x000A304C
		internal static Exception PooledOpenTimeout()
		{
			return ADP.InvalidOperation(SR.GetString("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached."));
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000A4E5D File Offset: 0x000A305D
		internal static Exception NonPooledOpenTimeout()
		{
			return ADP.TimeoutException(SR.GetString("Timeout attempting to open the connection.  The time period elapsed prior to attempting to open the connection has been exceeded.  This may have occurred because of too many simultaneous non-pooled connection attempts."));
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000A4E6E File Offset: 0x000A306E
		internal static InvalidOperationException TransactionConnectionMismatch()
		{
			return ADP.Provider(SR.GetString("The transaction is either not associated with the current connection or has been completed."));
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A4E7F File Offset: 0x000A307F
		internal static InvalidOperationException TransactionRequired(string method)
		{
			return ADP.Provider(SR.GetString("{0} requires the command to have a transaction when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.", new object[] { method }));
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A4E9A File Offset: 0x000A309A
		internal static Exception CommandTextRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: CommandText property has not been initialized", new object[] { method }));
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000A4EB5 File Offset: 0x000A30B5
		internal static Exception NoColumns()
		{
			return ADP.Argument(SR.GetString("The schema table contains no columns."));
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000A4EC6 File Offset: 0x000A30C6
		internal static InvalidOperationException ConnectionRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: Connection property has not been initialized.", new object[] { method }));
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000A4EE1 File Offset: 0x000A30E1
		internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("{0} requires an open and available Connection. {1}", new object[]
			{
				method,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000A4F05 File Offset: 0x000A3105
		internal static Exception OpenReaderExists()
		{
			return ADP.OpenReaderExists(null);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000A4F0D File Offset: 0x000A310D
		internal static Exception OpenReaderExists(Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("There is already an open DataReader associated with this Command which must be closed first."), e);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000A4F1F File Offset: 0x000A311F
		internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid {2} attempt at dataIndex '{0}'.  With CommandBehavior.SequentialAccess, you may only read from dataIndex '{1}' or greater.", new object[]
			{
				badIndex.ToString(CultureInfo.InvariantCulture),
				currIndex.ToString(CultureInfo.InvariantCulture),
				method
			}));
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000A4F58 File Offset: 0x000A3158
		internal static Exception InvalidXml()
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid."));
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000A4F69 File Offset: 0x000A3169
		internal static Exception NegativeParameter(string parameterName)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid value for argument '{0}'. The value must be greater than or equal to 0.", new object[] { parameterName }));
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000A4F84 File Offset: 0x000A3184
		internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {0} collection must contain a {1} column and it must be a string column.", new object[] { collectionName, columnName }));
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000A4FA3 File Offset: 0x000A31A3
		internal static Exception InvalidMetaDataValue()
		{
			return ADP.Argument(SR.GetString("Invalid value for this metadata."));
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000A4FB4 File Offset: 0x000A31B4
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read from column ordinal '{0}'.  With CommandBehavior.SequentialAccess, you may only read from column ordinal '{1}' or greater.", new object[]
			{
				badCol.ToString(CultureInfo.InvariantCulture),
				currCol.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000A4FE9 File Offset: 0x000A31E9
		internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {1} column of the {0} collection must contain a non-empty string.", new object[] { collectionName, columnName }));
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000A5008 File Offset: 0x000A3208
		internal static Exception CollectionNameIsNotUnique(string collectionName)
		{
			return ADP.Argument(SR.GetString("There are multiple collections named '{0}'.", new object[] { collectionName }));
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000A5023 File Offset: 0x000A3223
		internal static Exception InvalidCommandTimeout(int value, [CallerMemberName] string property = "")
		{
			return ADP.Argument(SR.GetString("Invalid CommandTimeout value {0}; the value must be >= 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }), property);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000A504A File Offset: 0x000A324A
		internal static Exception UninitializedParameterSize(int index, Type dataType)
		{
			return ADP.InvalidOperation(SR.GetString("{1}[{0}]: the Size property has an invalid size of 0.", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dataType.Name
			}));
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000A5079 File Offset: 0x000A3279
		internal static Exception UnableToBuildCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("Unable to build schema collection '{0}';", new object[] { collectionName }));
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000A5094 File Offset: 0x000A3294
		internal static Exception PrepareParameterType(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all parameters to have an explicitly set type.", new object[] { cmd.GetType().Name }));
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000A50B9 File Offset: 0x000A32B9
		internal static Exception UndefinedCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("The requested collection ({0}) is not defined.", new object[] { collectionName }));
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000A50D4 File Offset: 0x000A32D4
		internal static Exception UnsupportedVersion(string collectionName)
		{
			return ADP.Argument(SR.GetString(" requested collection ({0}) is not supported by this version of the provider.", new object[] { collectionName }));
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000A50EF File Offset: 0x000A32EF
		internal static Exception AmbigousCollectionName(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection name '{0}' matches at least two collections with the same name but with different case, but does not match any of them exactly.", new object[] { collectionName }));
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000A510A File Offset: 0x000A330A
		internal static Exception PrepareParameterSize(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.", new object[] { cmd.GetType().Name }));
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000A512F File Offset: 0x000A332F
		internal static Exception PrepareParameterScale(DbCommand cmd, string type)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires parameters of type '{1}' have an explicitly set Precision and Scale.", new object[]
			{
				cmd.GetType().Name,
				type
			}));
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000A5158 File Offset: 0x000A3358
		internal static Exception MissingDataSourceInformationColumn()
		{
			return ADP.Argument(SR.GetString("One of the required DataSourceInformation tables columns is missing."));
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000A5169 File Offset: 0x000A3369
		internal static Exception IncorrectNumberOfDataSourceInformationRows()
		{
			return ADP.Argument(SR.GetString("The DataSourceInformation table must contain exactly one row."));
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000A517A File Offset: 0x000A337A
		internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
		{
			return ADP.InvalidOperation(SR.GetString("Mismatched end method call for asyncResult.  Expected call to {0} but {1} was called instead.", new object[] { expectedMethod, gotMethod }));
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000A5199 File Offset: 0x000A3399
		internal static Exception ClosedConnectionError()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid operation. The connection is closed."));
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000A51AA File Offset: 0x000A33AA
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("The connection was not closed. {0}", new object[] { ADP.ConnectionStateMsg(state) }));
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000A51CA File Offset: 0x000A33CA
		internal static Exception TransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Connection currently has transaction enlisted.  Finish current transaction and retry."));
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000A51DB File Offset: 0x000A33DB
		internal static Exception LocalTransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry."));
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000A51EC File Offset: 0x000A33EC
		internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("Not allowed to change the '{0}' property. {1}", new object[]
			{
				property,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000A5210 File Offset: 0x000A3410
		internal static Exception EmptyDatabaseName()
		{
			return ADP.Argument(SR.GetString("Database cannot be null, the empty string, or string of only whitespace."));
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000A5221 File Offset: 0x000A3421
		internal static Exception MissingRestrictionColumn()
		{
			return ADP.Argument(SR.GetString("One or more of the required columns of the restrictions collection is missing."));
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A5232 File Offset: 0x000A3432
		internal static Exception InternalConnectionError(ADP.ConnectionError internalError)
		{
			return ADP.InvalidOperation(SR.GetString("Internal DbConnection Error: {0}", new object[] { (int)internalError }));
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A5252 File Offset: 0x000A3452
		internal static Exception InvalidConnectRetryCountValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryCount value (should be 0-255)."));
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000A5263 File Offset: 0x000A3463
		internal static Exception MissingRestrictionRow()
		{
			return ADP.Argument(SR.GetString("A restriction exists for which there is no matching row in the restrictions collection."));
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000A5274 File Offset: 0x000A3474
		internal static Exception InvalidConnectRetryIntervalValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryInterval value (should be 1-60)."));
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000A5285 File Offset: 0x000A3485
		internal static InvalidOperationException AsyncOperationPending()
		{
			return ADP.InvalidOperation(SR.GetString("Can not start another operation while there is an asynchronous operation pending."));
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A5296 File Offset: 0x000A3496
		internal static IOException ErrorReadingFromStream(Exception internalException)
		{
			return ADP.IO(SR.GetString("An error occurred while reading."), internalException);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000A52A8 File Offset: 0x000A34A8
		internal static ArgumentException InvalidDataType(string typeName)
		{
			return ADP.Argument(SR.GetString("The parameter data type of {0} is invalid.", new object[] { typeName }));
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000A52C3 File Offset: 0x000A34C3
		internal static ArgumentException UnknownDataType(Type dataType)
		{
			return ADP.Argument(SR.GetString("No mapping exists from object type {0} to a known managed provider native type.", new object[] { dataType.FullName }));
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A52E3 File Offset: 0x000A34E3
		internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
		{
			return ADP.Argument(SR.GetString("No mapping exists from DbType {0} to a known {1}.", new object[]
			{
				type.ToString(),
				enumtype.Name
			}));
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000A5313 File Offset: 0x000A3513
		internal static ArgumentException InvalidOffsetValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Offset value '{0}'. The value must be greater than or equal to 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000A5339 File Offset: 0x000A3539
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000A535F File Offset: 0x000A355F
		internal static ArgumentException ParameterValueOutOfRange(decimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[] { value.ToString(null) }));
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000A5381 File Offset: 0x000A3581
		internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[] { value.ToString() }));
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000A53A8 File Offset: 0x000A35A8
		internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
		{
			return ADP.Argument(SR.GetString("The version of SQL Server in use does not support datatype '{0}'.", new object[] { typeName }));
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000A53C4 File Offset: 0x000A35C4
		internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
		{
			string @string = SR.GetString("Failed to convert parameter value from a {0} to a {1}.", new object[]
			{
				value.GetType().Name,
				destType.Name
			});
			Exception ex;
			if (inner is ArgumentException)
			{
				ex = new ArgumentException(@string, inner);
			}
			else if (inner is FormatException)
			{
				ex = new FormatException(@string, inner);
			}
			else if (inner is InvalidCastException)
			{
				ex = new InvalidCastException(@string, inner);
			}
			else if (inner is OverflowException)
			{
				ex = new OverflowException(@string, inner);
			}
			else
			{
				ex = inner;
			}
			return ex;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000A5444 File Offset: 0x000A3644
		internal static Exception ParametersMappingIndex(int index, DbParameterCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000A5458 File Offset: 0x000A3658
		internal static Exception ParametersSourceIndex(string parameterName, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionIndexString(parameterType, "ParameterName", parameterName, collection.GetType());
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000A546C File Offset: 0x000A366C
		internal static Exception ParameterNull(string parameter, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		internal static Exception UndefinedPopulationMechanism(string populationMechanism)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000A547B File Offset: 0x000A367B
		internal static Exception InvalidParameterType(DbParameterCollection collection, Type parameterType, object invalidValue)
		{
			return ADP.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000A548A File Offset: 0x000A368A
		internal static Exception ParallelTransactionsNotSupported(DbConnection obj)
		{
			return ADP.InvalidOperation(SR.GetString("{0} does not support parallel transactions.", new object[] { obj.GetType().Name }));
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000A54AF File Offset: 0x000A36AF
		internal static Exception TransactionZombied(DbTransaction obj)
		{
			return ADP.InvalidOperation(SR.GetString("This {0} has completed; it is no longer usable.", new object[] { obj.GetType().Name }));
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000A54D4 File Offset: 0x000A36D4
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				foreach (Delegate @delegate in mcd.GetInvocationList())
				{
					if (@delegate.Target is DbCommandBuilder)
					{
						return @delegate;
					}
				}
			}
			return null;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000A5510 File Offset: 0x000A3710
		internal static void TimerCurrent(out long ticks)
		{
			ticks = DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000A552C File Offset: 0x000A372C
		internal static long TimerCurrent()
		{
			return DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000A5546 File Offset: 0x000A3746
		internal static long TimerFromSeconds(int seconds)
		{
			checked
			{
				return unchecked((long)seconds) * 10000000L;
			}
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000A5551 File Offset: 0x000A3751
		internal static long TimerFromMilliseconds(long milliseconds)
		{
			return checked(milliseconds * 10000L);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000A555B File Offset: 0x000A375B
		internal static bool TimerHasExpired(long timerExpire)
		{
			return ADP.TimerCurrent() > timerExpire;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000A5568 File Offset: 0x000A3768
		internal static long TimerRemaining(long timerExpire)
		{
			long num = ADP.TimerCurrent();
			return checked(timerExpire - num);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000A557E File Offset: 0x000A377E
		internal static long TimerRemainingMilliseconds(long timerExpire)
		{
			return ADP.TimerToMilliseconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000A558B File Offset: 0x000A378B
		internal static long TimerRemainingSeconds(long timerExpire)
		{
			return ADP.TimerToSeconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000A5598 File Offset: 0x000A3798
		internal static long TimerToMilliseconds(long timerValue)
		{
			return timerValue / 10000L;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000A55A2 File Offset: 0x000A37A2
		private static long TimerToSeconds(long timerValue)
		{
			return timerValue / 10000000L;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000A55AC File Offset: 0x000A37AC
		internal static string MachineName()
		{
			return Environment.MachineName;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000A55B3 File Offset: 0x000A37B3
		internal static Transaction GetCurrentTransaction()
		{
			return Transaction.Current;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000A55BA File Offset: 0x000A37BA
		internal static bool IsDirection(DbParameter value, ParameterDirection condition)
		{
			return condition == (condition & value.Direction);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000A55C8 File Offset: 0x000A37C8
		internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
		{
			if (value == null || value == DBNull.Value)
			{
				isNull = true;
				isSqlType = false;
				return;
			}
			INullable nullable = value as INullable;
			if (nullable != null)
			{
				isNull = nullable.IsNull;
				isSqlType = value is SqlBinary || value is SqlBoolean || value is SqlByte || value is SqlBytes || value is SqlChars || value is SqlDateTime || value is SqlDecimal || value is SqlDouble || value is SqlGuid || value is SqlInt16 || value is SqlInt32 || value is SqlInt64 || value is SqlMoney || value is SqlSingle || value is SqlString;
				return;
			}
			isNull = false;
			isSqlType = false;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000A5681 File Offset: 0x000A3881
		internal static Version GetAssemblyVersion()
		{
			if (ADP.s_systemDataVersion == null)
			{
				ADP.s_systemDataVersion = new Version("4.6.57.0");
			}
			return ADP.s_systemDataVersion;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000A56A4 File Offset: 0x000A38A4
		internal static bool IsAzureSqlServerEndpoint(string dataSource)
		{
			int i = dataSource.LastIndexOf(',');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			i = dataSource.LastIndexOf('\\');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			dataSource = dataSource.Trim();
			for (i = 0; i < ADP.AzureSqlServerEndpoints.Length; i++)
			{
				if (dataSource.EndsWith(ADP.AzureSqlServerEndpoints[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000A570C File Offset: 0x000A390C
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000A571E File Offset: 0x000A391E
		internal static ArgumentException SingleValuedProperty(string propertyName, string value)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The only acceptable value for the property '{0}' is '{1}'.", new object[] { propertyName, value }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000A5743 File Offset: 0x000A3943
		internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The acceptable values for the property '{0}' are '{1}' or '{2}'.", new object[] { propertyName, value1, value2 }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000A576C File Offset: 0x000A396C
		internal static ArgumentException InvalidPrefixSuffix()
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified QuotePrefix and QuoteSuffix values do not match."));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000A5783 File Offset: 0x000A3983
		internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000A5795 File Offset: 0x000A3995
		internal static void ValidateCommandBehavior(CommandBehavior value)
		{
			if (value < CommandBehavior.Default || (CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection) < value)
			{
				throw ADP.InvalidCommandBehavior(value);
			}
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000A57A7 File Offset: 0x000A39A7
		internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000A57C6 File Offset: 0x000A39C6
		internal static ArgumentException BadParameterName(string parameterName)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified parameter name '{0}' is not valid.", new object[] { parameterName }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000A57E8 File Offset: 0x000A39E8
		internal static Exception DeriveParametersNotSupported(IDbCommand value)
		{
			return ADP.DataAdapter(SR.GetString("{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{1}.", new object[]
			{
				value.GetType().Name,
				value.CommandType.ToString()
			}));
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000A582F File Offset: 0x000A3A2F
		internal static Exception NoStoredProcedureExists(string sproc)
		{
			return ADP.InvalidOperation(SR.GetString("The stored procedure '{0}' doesn't exist.", new object[] { sproc }));
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A584A File Offset: 0x000A3A4A
		internal static InvalidOperationException TransactionCompletedButNotDisposed()
		{
			return ADP.Provider(SR.GetString("The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements."));
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000061D5 File Offset: 0x000043D5
		internal static bool NeedManualEnlistment()
		{
			return false;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000A585B File Offset: 0x000A3A5B
		internal static bool IsEmpty(string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000A5863 File Offset: 0x000A3A63
		internal static Exception DatabaseNameTooLong()
		{
			return ADP.Argument(SR.GetString("The argument is too long."));
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00080F96 File Offset: 0x0007F196
		internal static int StringLength(string inputString)
		{
			if (inputString == null)
			{
				return 0;
			}
			return inputString.Length;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A5874 File Offset: 0x000A3A74
		internal static Exception NumericToDecimalOverflow()
		{
			return ADP.InvalidCast(SR.GetString("The numerical value is too large to fit into a 96 bit decimal."));
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000A5885 File Offset: 0x000A3A85
		internal static Exception OdbcNoTypesFromProvider()
		{
			return ADP.InvalidOperation(SR.GetString("The ODBC provider did not return results from SQLGETTYPEINFO."));
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000A5896 File Offset: 0x000A3A96
		internal static ArgumentException InvalidRestrictionValue(string collectionName, string restrictionName, string restrictionValue)
		{
			return ADP.Argument(SR.GetString("'{2}' is not a valid value for the '{1}' restriction of the '{0}' schema collection.", new object[] { collectionName, restrictionName, restrictionValue }));
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000A58B9 File Offset: 0x000A3AB9
		internal static Exception DataReaderNoData()
		{
			return ADP.InvalidOperation(SR.GetString("No data exists for the row/column."));
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A58CA File Offset: 0x000A3ACA
		internal static Exception ConnectionIsDisabled(Exception InnerException)
		{
			return ADP.InvalidOperation(SR.GetString("The connection has been disabled."), InnerException);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A58DC File Offset: 0x000A3ADC
		internal static Exception OffsetOutOfRangeException()
		{
			return ADP.InvalidOperation(SR.GetString("Offset must refer to a location within the value."));
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000A58ED File Offset: 0x000A3AED
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return ADP.Argument(SR.GetString("The parameter data type of {0} is invalid.", new object[] { typecode.ToString() }));
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000A5914 File Offset: 0x000A3B14
		internal static InvalidOperationException QuotePrefixNotSet(string method)
		{
			return ADP.InvalidOperation(Res.GetString("{0} requires open connection when the quote prefix has not been set.", new object[] { method }));
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000A592F File Offset: 0x000A3B2F
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000A5937 File Offset: 0x000A3B37
		internal static InvalidOperationException InvalidDataDirectory()
		{
			return ADP.InvalidOperation(SR.GetString("The DataDirectory substitute is not a string."));
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000A5948 File Offset: 0x000A3B48
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string text = "Unable to handle an unknown TypeCode {0} returned by Type {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)typeCode;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = dataType.FullName;
			return ADP.Argument(SR.GetString(text, array));
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000A5984 File Offset: 0x000A3B84
		internal static void EscapeSpecialCharacters(string unescapedString, StringBuilder escapedString)
		{
			foreach (char c in unescapedString)
			{
				if (".$^{[(|)*+?\\]".IndexOf(c) >= 0)
				{
					escapedString.Append("\\");
				}
				escapedString.Append(c);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000A59CE File Offset: 0x000A3BCE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (4 == ADP.PtrSize)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000A59F6 File Offset: 0x000A3BF6
		internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Format value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(Format), value.ToString(), method);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000A5A15 File Offset: 0x000A3C15
		internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Format value)
		{
			return ADP.InvalidEnumerationValue(typeof(Format), (int)value);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000A5A27 File Offset: 0x000A3C27
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, value, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000A5A37 File Offset: 0x000A3C37
		internal static Exception InvalidXMLBadVersion()
		{
			return ADP.Argument(Res.GetString("Invalid Xml; can only parse elements of version one."));
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000A5A48 File Offset: 0x000A3C48
		internal static Exception NotAPermissionElement()
		{
			return ADP.Argument(Res.GetString("Given security element is not a permission element."));
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000A5A59 File Offset: 0x000A3C59
		internal static Exception PermissionTypeMismatch()
		{
			return ADP.Argument(Res.GetString("Type mismatch."));
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000A5A6A File Offset: 0x000A3C6A
		internal static ArgumentOutOfRangeException InvalidPermissionState(PermissionState value)
		{
			return ADP.InvalidEnumerationValue(typeof(PermissionState), (int)value);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000A5A7C File Offset: 0x000A3C7C
		internal static ConfigurationException Configuration(string message)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000A5A8A File Offset: 0x000A3C8A
		internal static ConfigurationException Configuration(string message, XmlNode node)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message, node);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000A5A99 File Offset: 0x000A3C99
		internal static ArgumentException ConfigProviderNotFound()
		{
			return ADP.Argument(Res.GetString("Unable to find the requested .Net Framework Data Provider.  It may not be installed."));
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000A5AAA File Offset: 0x000A3CAA
		internal static InvalidOperationException ConfigProviderInvalid()
		{
			return ADP.InvalidOperation(Res.GetString("The requested .Net Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type."));
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000A5ABB File Offset: 0x000A3CBB
		internal static ConfigurationException ConfigProviderNotInstalled()
		{
			return ADP.Configuration(Res.GetString("Failed to find or load the registered .Net Framework Data Provider."));
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000A5ACC File Offset: 0x000A3CCC
		internal static ConfigurationException ConfigProviderMissing()
		{
			return ADP.Configuration(Res.GetString("The missing .Net Framework Data Provider's assembly qualified name is required."));
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000A5ADD File Offset: 0x000A3CDD
		internal static ConfigurationException ConfigBaseNoChildNodes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Child nodes not allowed."), node);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000A5AEF File Offset: 0x000A3CEF
		internal static ConfigurationException ConfigBaseElementsOnly(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Only elements allowed."), node);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000A5B01 File Offset: 0x000A3D01
		internal static ConfigurationException ConfigUnrecognizedAttributes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized attribute '{0}'.", new object[] { node.Attributes[0].Name }), node);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000A5B2D File Offset: 0x000A3D2D
		internal static ConfigurationException ConfigUnrecognizedElement(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized element."), node);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000A5B3F File Offset: 0x000A3D3F
		internal static ConfigurationException ConfigSectionsUnique(string sectionName)
		{
			return ADP.Configuration(Res.GetString("The '{0}' section can only appear once per config file.", new object[] { sectionName }));
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000A5B5A File Offset: 0x000A3D5A
		internal static ConfigurationException ConfigRequiredAttributeMissing(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' not found.", new object[] { name }), node);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000A5B76 File Offset: 0x000A3D76
		internal static ConfigurationException ConfigRequiredAttributeEmpty(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' cannot be empty.", new object[] { name }), node);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000A5B92 File Offset: 0x000A3D92
		internal static Exception OleDb()
		{
			return new NotImplementedException("OleDb is not implemented.");
		}

		// Token: 0x04001757 RID: 5975
		private static Task<bool> _trueTask;

		// Token: 0x04001758 RID: 5976
		private static Task<bool> _falseTask;

		// Token: 0x04001759 RID: 5977
		internal const CompareOptions DefaultCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x0400175A RID: 5978
		internal const int DefaultConnectionTimeout = 15;

		// Token: 0x0400175B RID: 5979
		private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400175C RID: 5980
		private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400175D RID: 5981
		private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

		// Token: 0x0400175E RID: 5982
		private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

		// Token: 0x0400175F RID: 5983
		private static readonly Type s_accessViolationType = typeof(AccessViolationException);

		// Token: 0x04001760 RID: 5984
		private static readonly Type s_securityType = typeof(SecurityException);

		// Token: 0x04001761 RID: 5985
		internal static readonly bool IsWindowsNT = PlatformID.Win32NT == Environment.OSVersion.Platform;

		// Token: 0x04001762 RID: 5986
		internal static readonly bool IsPlatformNT5 = ADP.IsWindowsNT && Environment.OSVersion.Version.Major >= 5;

		// Token: 0x04001763 RID: 5987
		internal const string ConnectionString = "ConnectionString";

		// Token: 0x04001764 RID: 5988
		internal const string DataSetColumn = "DataSetColumn";

		// Token: 0x04001765 RID: 5989
		internal const string DataSetTable = "DataSetTable";

		// Token: 0x04001766 RID: 5990
		internal const string Fill = "Fill";

		// Token: 0x04001767 RID: 5991
		internal const string FillSchema = "FillSchema";

		// Token: 0x04001768 RID: 5992
		internal const string SourceColumn = "SourceColumn";

		// Token: 0x04001769 RID: 5993
		internal const string SourceTable = "SourceTable";

		// Token: 0x0400176A RID: 5994
		internal const string Parameter = "Parameter";

		// Token: 0x0400176B RID: 5995
		internal const string ParameterName = "ParameterName";

		// Token: 0x0400176C RID: 5996
		internal const string ParameterSetPosition = "set_Position";

		// Token: 0x0400176D RID: 5997
		internal const int DefaultCommandTimeout = 30;

		// Token: 0x0400176E RID: 5998
		internal const float FailoverTimeoutStep = 0.08f;

		// Token: 0x0400176F RID: 5999
		internal static readonly string StrEmpty = "";

		// Token: 0x04001770 RID: 6000
		internal const int CharSize = 2;

		// Token: 0x04001771 RID: 6001
		private static Version s_systemDataVersion;

		// Token: 0x04001772 RID: 6002
		internal static readonly string[] AzureSqlServerEndpoints = new string[]
		{
			SR.GetString(".database.windows.net"),
			SR.GetString(".database.cloudapi.de"),
			SR.GetString(".database.usgovcloudapi.net"),
			SR.GetString(".database.chinacloudapi.cn")
		};

		// Token: 0x04001773 RID: 6003
		internal const int DecimalMaxPrecision = 29;

		// Token: 0x04001774 RID: 6004
		internal const int DecimalMaxPrecision28 = 28;

		// Token: 0x04001775 RID: 6005
		internal static readonly IntPtr PtrZero = new IntPtr(0);

		// Token: 0x04001776 RID: 6006
		internal static readonly int PtrSize = IntPtr.Size;

		// Token: 0x04001777 RID: 6007
		internal const string BeginTransaction = "BeginTransaction";

		// Token: 0x04001778 RID: 6008
		internal const string ChangeDatabase = "ChangeDatabase";

		// Token: 0x04001779 RID: 6009
		internal const string CommitTransaction = "CommitTransaction";

		// Token: 0x0400177A RID: 6010
		internal const string CommandTimeout = "CommandTimeout";

		// Token: 0x0400177B RID: 6011
		internal const string DeriveParameters = "DeriveParameters";

		// Token: 0x0400177C RID: 6012
		internal const string ExecuteReader = "ExecuteReader";

		// Token: 0x0400177D RID: 6013
		internal const string ExecuteNonQuery = "ExecuteNonQuery";

		// Token: 0x0400177E RID: 6014
		internal const string ExecuteScalar = "ExecuteScalar";

		// Token: 0x0400177F RID: 6015
		internal const string GetSchema = "GetSchema";

		// Token: 0x04001780 RID: 6016
		internal const string GetSchemaTable = "GetSchemaTable";

		// Token: 0x04001781 RID: 6017
		internal const string Prepare = "Prepare";

		// Token: 0x04001782 RID: 6018
		internal const string RollbackTransaction = "RollbackTransaction";

		// Token: 0x04001783 RID: 6019
		internal const string QuoteIdentifier = "QuoteIdentifier";

		// Token: 0x04001784 RID: 6020
		internal const string UnquoteIdentifier = "UnquoteIdentifier";

		// Token: 0x0200031B RID: 795
		internal enum InternalErrorCode
		{
			// Token: 0x04001786 RID: 6022
			UnpooledObjectHasOwner,
			// Token: 0x04001787 RID: 6023
			UnpooledObjectHasWrongOwner,
			// Token: 0x04001788 RID: 6024
			PushingObjectSecondTime,
			// Token: 0x04001789 RID: 6025
			PooledObjectHasOwner,
			// Token: 0x0400178A RID: 6026
			PooledObjectInPoolMoreThanOnce,
			// Token: 0x0400178B RID: 6027
			CreateObjectReturnedNull,
			// Token: 0x0400178C RID: 6028
			NewObjectCannotBePooled,
			// Token: 0x0400178D RID: 6029
			NonPooledObjectUsedMoreThanOnce,
			// Token: 0x0400178E RID: 6030
			AttemptingToPoolOnRestrictedToken,
			// Token: 0x0400178F RID: 6031
			ConvertSidToStringSidWReturnedNull = 10,
			// Token: 0x04001790 RID: 6032
			AttemptingToConstructReferenceCollectionOnStaticObject = 12,
			// Token: 0x04001791 RID: 6033
			AttemptingToEnlistTwice,
			// Token: 0x04001792 RID: 6034
			CreateReferenceCollectionReturnedNull,
			// Token: 0x04001793 RID: 6035
			PooledObjectWithoutPool,
			// Token: 0x04001794 RID: 6036
			UnexpectedWaitAnyResult,
			// Token: 0x04001795 RID: 6037
			SynchronousConnectReturnedPending,
			// Token: 0x04001796 RID: 6038
			CompletedConnectReturnedPending,
			// Token: 0x04001797 RID: 6039
			NameValuePairNext = 20,
			// Token: 0x04001798 RID: 6040
			InvalidParserState1,
			// Token: 0x04001799 RID: 6041
			InvalidParserState2,
			// Token: 0x0400179A RID: 6042
			InvalidParserState3,
			// Token: 0x0400179B RID: 6043
			InvalidBuffer = 30,
			// Token: 0x0400179C RID: 6044
			UnimplementedSMIMethod = 40,
			// Token: 0x0400179D RID: 6045
			InvalidSmiCall,
			// Token: 0x0400179E RID: 6046
			SqlDependencyObtainProcessDispatcherFailureObjectHandle = 50,
			// Token: 0x0400179F RID: 6047
			SqlDependencyProcessDispatcherFailureCreateInstance,
			// Token: 0x040017A0 RID: 6048
			SqlDependencyProcessDispatcherFailureAppDomain,
			// Token: 0x040017A1 RID: 6049
			SqlDependencyCommandHashIsNotAssociatedWithNotification,
			// Token: 0x040017A2 RID: 6050
			UnknownTransactionFailure = 60
		}

		// Token: 0x0200031C RID: 796
		internal enum ConnectionError
		{
			// Token: 0x040017A4 RID: 6052
			BeginGetConnectionReturnsNull,
			// Token: 0x040017A5 RID: 6053
			GetConnectionReturnsNull,
			// Token: 0x040017A6 RID: 6054
			ConnectionOptionsMissing,
			// Token: 0x040017A7 RID: 6055
			CouldNotSwitchToClosedPreviouslyOpenedState
		}
	}
}
