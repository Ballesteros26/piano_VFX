using System;

namespace System.Data.Odbc
{
	// Token: 0x020002B0 RID: 688
	internal struct SQLLEN
	{
		// Token: 0x06001D61 RID: 7521 RVA: 0x00091257 File Offset: 0x0008F457
		internal SQLLEN(int value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00091265 File Offset: 0x0008F465
		internal SQLLEN(long value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00091273 File Offset: 0x0008F473
		internal SQLLEN(IntPtr value)
		{
			this._value = value;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0009127C File Offset: 0x0008F47C
		public static implicit operator SQLLEN(int value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00091284 File Offset: 0x0008F484
		public static explicit operator SQLLEN(long value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0009128C File Offset: 0x0008F48C
		public static implicit operator int(SQLLEN value)
		{
			return checked((int)value._value.ToInt64());
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0009129B File Offset: 0x0008F49B
		public static explicit operator long(SQLLEN value)
		{
			return value._value.ToInt64();
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000912A9 File Offset: 0x0008F4A9
		public long ToInt64()
		{
			return this._value.ToInt64();
		}

		// Token: 0x04001585 RID: 5509
		private IntPtr _value;
	}
}
