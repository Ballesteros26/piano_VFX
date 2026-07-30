using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002A1 RID: 673
	internal abstract class OdbcHandle : SafeHandle
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		protected OdbcHandle(ODBC32.SQL_HANDLE handleType, OdbcHandle parentHandle)
			: base(IntPtr.Zero, true)
		{
			this._handleType = handleType;
			bool flag = false;
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (handleType != ODBC32.SQL_HANDLE.ENV)
				{
					if (handleType - ODBC32.SQL_HANDLE.DBC <= 1)
					{
						parentHandle.DangerousAddRef(ref flag);
						retCode = UnsafeNativeMethods.SQLAllocHandle(handleType, parentHandle, out this.handle);
					}
				}
				else
				{
					retCode = UnsafeNativeMethods.SQLAllocHandle(handleType, IntPtr.Zero, out this.handle);
				}
			}
			finally
			{
				if (flag && handleType - ODBC32.SQL_HANDLE.DBC <= 1)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle || retCode != ODBC32.RetCode.SUCCESS)
			{
				throw ODBC.CantAllocateEnvironmentHandle(retCode);
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0008DDF4 File Offset: 0x0008BFF4
		internal OdbcHandle(OdbcStatementHandle parentHandle, ODBC32.SQL_ATTR attribute)
			: base(IntPtr.Zero, true)
		{
			this._handleType = ODBC32.SQL_HANDLE.DESC;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode statementAttribute;
			try
			{
				parentHandle.DangerousAddRef(ref flag);
				int num;
				statementAttribute = parentHandle.GetStatementAttribute(attribute, out this.handle, out num);
			}
			finally
			{
				if (flag)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle)
			{
				throw ODBC.FailedToGetDescriptorHandle(statementAttribute);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0008DE84 File Offset: 0x0008C084
		internal ODBC32.SQL_HANDLE HandleType
		{
			get
			{
				return this._handleType;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0008DE8C File Offset: 0x0008C08C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0008DEA0 File Offset: 0x0008C0A0
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				ODBC32.SQL_HANDLE handleType = this.HandleType;
				if (handleType - ODBC32.SQL_HANDLE.ENV > 2)
				{
					if (handleType != ODBC32.SQL_HANDLE.DESC)
					{
					}
				}
				else
				{
					UnsafeNativeMethods.SQLFreeHandle(handleType, handle);
				}
			}
			OdbcHandle parentHandle = this._parentHandle;
			this._parentHandle = null;
			if (parentHandle != null)
			{
				parentHandle.DangerousRelease();
			}
			return true;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0008DF04 File Offset: 0x0008C104
		internal ODBC32.RetCode GetDiagnosticField(out string sqlState)
		{
			StringBuilder stringBuilder = new StringBuilder(6);
			short num;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDiagFieldW(this.HandleType, this, 1, 4, stringBuilder, checked((short)(2 * stringBuilder.Capacity)), out num);
			ODBC.TraceODBC(3, "SQLGetDiagFieldW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0008DF58 File Offset: 0x0008C158
		internal ODBC32.RetCode GetDiagnosticRecord(short record, out string sqlState, StringBuilder message, out int nativeError, out short cchActual)
		{
			StringBuilder stringBuilder = new StringBuilder(5);
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDiagRecW(this.HandleType, this, record, stringBuilder, out nativeError, message, checked((short)message.Capacity), out cchActual);
			ODBC.TraceODBC(3, "SQLGetDiagRecW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x04001543 RID: 5443
		private ODBC32.SQL_HANDLE _handleType;

		// Token: 0x04001544 RID: 5444
		private OdbcHandle _parentHandle;
	}
}
