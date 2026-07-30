using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000168 RID: 360
	internal static class SqlClientDiagnosticListenerExtensions
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00056750 File Offset: 0x00054950
		public static Guid WriteCommandBefore(this DiagnosticListener @this, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandBefore"))
			{
				Guid guid = Guid.NewGuid();
				string text = "System.Data.SqlClient.WriteCommandBefore";
				Guid guid2 = guid;
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(text, new
				{
					OperationId = guid2,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000567AC File Offset: 0x000549AC
		public static void WriteCommandAfter(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandAfter"))
			{
				string text = "System.Data.SqlClient.WriteCommandAfter";
				SqlConnection connection = sqlCommand.Connection;
				Guid? guid = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null);
				SqlStatistics statistics = sqlCommand.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = guid,
					Command = sqlCommand,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00056810 File Offset: 0x00054A10
		public static void WriteCommandError(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandError"))
			{
				string text = "System.Data.SqlClient.WriteCommandError";
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00056864 File Offset: 0x00054A64
		public static Guid WriteConnectionOpenBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteConnectionOpenBefore", new
				{
					OperationId = guid,
					Operation = operation,
					Connection = sqlConnection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000568A3 File Offset: 0x00054AA3
		public static void WriteConnectionOpenAfter(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter"))
			{
				string text = "System.Data.SqlClient.WriteConnectionOpenAfter";
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000568E2 File Offset: 0x00054AE2
		public static void WriteConnectionOpenError(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenError"))
			{
				@this.Write("System.Data.SqlClient.WriteConnectionOpenError", new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = sqlConnection.ClientConnectionId,
					Connection = sqlConnection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00056914 File Offset: 0x00054B14
		public static Guid WriteConnectionCloseBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseBefore"))
			{
				Guid guid = Guid.NewGuid();
				string text = "System.Data.SqlClient.WriteConnectionCloseBefore";
				Guid guid2 = guid;
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = guid2,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0005696B File Offset: 0x00054B6B
		public static void WriteConnectionCloseAfter(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseAfter"))
			{
				string text = "System.Data.SqlClient.WriteConnectionCloseAfter";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000569A8 File Offset: 0x00054BA8
		public static void WriteConnectionCloseError(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseError"))
			{
				string text = "System.Data.SqlClient.WriteConnectionCloseError";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000569F0 File Offset: 0x00054BF0
		public static Guid WriteTransactionCommitBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionCommitBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00056A30 File Offset: 0x00054C30
		public static void WriteTransactionCommitAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00056A59 File Offset: 0x00054C59
		public static void WriteTransactionCommitError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00056A84 File Offset: 0x00054C84
		public static Guid WriteTransactionRollbackBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00056AC6 File Offset: 0x00054CC6
		public static void WriteTransactionRollbackAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00056AF4 File Offset: 0x00054CF4
		public static void WriteTransactionRollbackError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x04000B61 RID: 2913
		public const string DiagnosticListenerName = "SqlClientDiagnosticListener";

		// Token: 0x04000B62 RID: 2914
		private const string SqlClientPrefix = "System.Data.SqlClient.";

		// Token: 0x04000B63 RID: 2915
		public const string SqlBeforeExecuteCommand = "System.Data.SqlClient.WriteCommandBefore";

		// Token: 0x04000B64 RID: 2916
		public const string SqlAfterExecuteCommand = "System.Data.SqlClient.WriteCommandAfter";

		// Token: 0x04000B65 RID: 2917
		public const string SqlErrorExecuteCommand = "System.Data.SqlClient.WriteCommandError";

		// Token: 0x04000B66 RID: 2918
		public const string SqlBeforeOpenConnection = "System.Data.SqlClient.WriteConnectionOpenBefore";

		// Token: 0x04000B67 RID: 2919
		public const string SqlAfterOpenConnection = "System.Data.SqlClient.WriteConnectionOpenAfter";

		// Token: 0x04000B68 RID: 2920
		public const string SqlErrorOpenConnection = "System.Data.SqlClient.WriteConnectionOpenError";

		// Token: 0x04000B69 RID: 2921
		public const string SqlBeforeCloseConnection = "System.Data.SqlClient.WriteConnectionCloseBefore";

		// Token: 0x04000B6A RID: 2922
		public const string SqlAfterCloseConnection = "System.Data.SqlClient.WriteConnectionCloseAfter";

		// Token: 0x04000B6B RID: 2923
		public const string SqlErrorCloseConnection = "System.Data.SqlClient.WriteConnectionCloseError";

		// Token: 0x04000B6C RID: 2924
		public const string SqlBeforeCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitBefore";

		// Token: 0x04000B6D RID: 2925
		public const string SqlAfterCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitAfter";

		// Token: 0x04000B6E RID: 2926
		public const string SqlErrorCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitError";

		// Token: 0x04000B6F RID: 2927
		public const string SqlBeforeRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackBefore";

		// Token: 0x04000B70 RID: 2928
		public const string SqlAfterRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackAfter";

		// Token: 0x04000B71 RID: 2929
		public const string SqlErrorRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackError";
	}
}
