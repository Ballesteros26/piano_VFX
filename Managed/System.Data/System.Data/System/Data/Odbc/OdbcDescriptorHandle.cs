using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002A2 RID: 674
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x06001CA7 RID: 7335 RVA: 0x0008DFAC File Offset: 0x0008C1AC
		internal OdbcDescriptorHandle(OdbcStatementHandle statementHandle, ODBC32.SQL_ATTR attribute)
			: base(statementHandle, attribute)
		{
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
		internal ODBC32.RetCode GetDescriptionField(int i, ODBC32.SQL_DESC attribute, CNativeBuffer buffer, out int numericAttribute)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDescFieldW(this, checked((short)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
			ODBC.TraceODBC(3, "SQLGetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
		internal ODBC32.RetCode SetDescriptionField1(short ordinal, ODBC32.SQL_DESC type, IntPtr value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0008E010 File Offset: 0x0008C210
		internal ODBC32.RetCode SetDescriptionField2(short ordinal, ODBC32.SQL_DESC type, HandleRef value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}
	}
}
