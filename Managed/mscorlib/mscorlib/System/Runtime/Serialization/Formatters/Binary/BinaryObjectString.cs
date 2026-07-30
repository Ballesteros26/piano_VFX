using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200070F RID: 1807
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x06004B8F RID: 19343 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryObjectString()
		{
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0010DD4A File Offset: 0x0010BF4A
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0010DD5A File Offset: 0x0010BF5A
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0010DD7B File Offset: 0x0010BF7B
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002776 RID: 10102
		internal int objectId;

		// Token: 0x04002777 RID: 10103
		internal string value;
	}
}
