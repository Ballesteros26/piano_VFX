using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000712 RID: 1810
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06004B9F RID: 19359 RVA: 0x00002111 File Offset: 0x00000311
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0010DDF5 File Offset: 0x0010BFF5
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0010DE05 File Offset: 0x0010C005
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0010DE2D File Offset: 0x0010C02D
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400277B RID: 10107
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x0400277C RID: 10108
		internal object value;
	}
}
