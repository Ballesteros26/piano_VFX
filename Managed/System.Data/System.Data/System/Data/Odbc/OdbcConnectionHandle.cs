using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000290 RID: 656
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x00089E00 File Offset: 0x00088000
		internal OdbcConnectionHandle(OdbcConnection connection, OdbcConnectionString constr, OdbcEnvironmentHandle environmentHandle)
			: base(ODBC32.SQL_HANDLE.DBC, environmentHandle)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			if (constr == null)
			{
				throw ADP.ArgumentNull("constr");
			}
			int connectionTimeout = connection.ConnectionTimeout;
			ODBC32.RetCode retCode = this.SetConnectionAttribute2(ODBC32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)connectionTimeout, -5);
			string text = constr.UsersConnectionString(false);
			retCode = this.Connect(text);
			connection.HandleError(this, retCode);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00089E64 File Offset: 0x00088064
		private ODBC32.RetCode AutoCommitOff()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_OFF, -5);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Transacted;
				}
			}
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00089EB4 File Offset: 0x000880B4
		internal ODBC32.RetCode BeginTransaction(ref IsolationLevel isolevel)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			if (IsolationLevel.Unspecified != isolevel)
			{
				IsolationLevel isolationLevel = isolevel;
				ODBC32.SQL_TRANSACTION sql_TRANSACTION;
				ODBC32.SQL_ATTR sql_ATTR;
				if (isolationLevel <= IsolationLevel.ReadCommitted)
				{
					if (isolationLevel == IsolationLevel.Chaos)
					{
						throw ODBC.NotSupportedIsolationLevel(isolevel);
					}
					if (isolationLevel == IsolationLevel.ReadUncommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.ReadCommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_COMMITTED;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
				}
				else
				{
					if (isolationLevel == IsolationLevel.RepeatableRead)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.REPEATABLE_READ;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.Serializable)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SERIALIZABLE;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.Snapshot)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SNAPSHOT;
						sql_ATTR = ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION;
						goto IL_007D;
					}
				}
				throw ADP.InvalidIsolationLevel(isolevel);
				IL_007D:
				retCode = this.SetConnectionAttribute2(sql_ATTR, (IntPtr)((int)sql_TRANSACTION), -6);
				if (ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					isolevel = IsolationLevel.Unspecified;
				}
			}
			if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				retCode = this.AutoCommitOff();
				this._handleState = OdbcConnectionHandle.HandleState.TransactionInProgress;
			}
			return retCode;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00089F68 File Offset: 0x00088168
		internal ODBC32.RetCode CompleteTransaction(short transactionOperation)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
				base.DangerousAddRef(ref flag);
				retCode = this.CompleteTransaction(transactionOperation, this.handle);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return retCode;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00089FB0 File Offset: 0x000881B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private ODBC32.RetCode CompleteTransaction(short transactionOperation, IntPtr handle)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
				{
					retCode = UnsafeNativeMethods.SQLEndTran(base.HandleType, handle, transactionOperation);
					if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
					{
						this._handleState = OdbcConnectionHandle.HandleState.Transacted;
					}
				}
				if (OdbcConnectionHandle.HandleState.Transacted == this._handleState)
				{
					retCode = UnsafeNativeMethods.SQLSetConnectAttrW(handle, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_ON, -5);
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			return retCode;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0008A020 File Offset: 0x00088220
		private ODBC32.RetCode Connect(string connectionString)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				short num;
				retCode = UnsafeNativeMethods.SQLDriverConnectW(this, ADP.PtrZero, connectionString, -3, ADP.PtrZero, 0, out num, 0);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			ODBC.TraceODBC(3, "SQLDriverConnectW", retCode);
			return retCode;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0008A078 File Offset: 0x00088278
		protected override bool ReleaseHandle()
		{
			this.CompleteTransaction(1, this.handle);
			if (OdbcConnectionHandle.HandleState.Connected == this._handleState || OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
			{
				UnsafeNativeMethods.SQLDisconnect(this.handle);
				this._handleState = OdbcConnectionHandle.HandleState.Allocated;
			}
			return base.ReleaseHandle();
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0008A0B3 File Offset: 0x000882B3
		internal ODBC32.RetCode GetConnectionAttribute(ODBC32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			return UnsafeNativeMethods.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0008A0C4 File Offset: 0x000882C4
		internal ODBC32.RetCode GetFunctions(ODBC32.SQL_API fFunction, out short fExists)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetFunctions(this, fFunction, out fExists);
			ODBC.TraceODBC(3, "SQLGetFunctions", retCode);
			return retCode;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0008A0E7 File Offset: 0x000882E7
		internal ODBC32.RetCode GetInfo2(ODBC32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			return UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), out cbActual);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0008A0F6 File Offset: 0x000882F6
		internal ODBC32.RetCode GetInfo1(ODBC32.SQL_INFO info, byte[] buffer)
		{
			return UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), ADP.PtrZero);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0008A10C File Offset: 0x0008830C
		internal ODBC32.RetCode SetConnectionAttribute2(ODBC32.SQL_ATTR attribute, IntPtr value, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, value, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0008A130 File Offset: 0x00088330
		internal ODBC32.RetCode SetConnectionAttribute3(ODBC32.SQL_ATTR attribute, string buffer, int length)
		{
			return UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, buffer, length);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0008A13C File Offset: 0x0008833C
		internal ODBC32.RetCode SetConnectionAttribute4(ODBC32.SQL_ATTR attribute, IDtcTransaction transaction, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, transaction, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x040014E5 RID: 5349
		private OdbcConnectionHandle.HandleState _handleState;

		// Token: 0x02000291 RID: 657
		private enum HandleState
		{
			// Token: 0x040014E7 RID: 5351
			Allocated,
			// Token: 0x040014E8 RID: 5352
			Connected,
			// Token: 0x040014E9 RID: 5353
			Transacted,
			// Token: 0x040014EA RID: 5354
			TransactionInProgress
		}
	}
}
