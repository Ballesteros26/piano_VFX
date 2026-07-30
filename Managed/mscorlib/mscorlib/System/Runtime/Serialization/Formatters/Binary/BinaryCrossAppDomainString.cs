using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000710 RID: 1808
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x06004B95 RID: 19349 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0010DD95 File Offset: 0x0010BF95
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0010DDB7 File Offset: 0x0010BFB7
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002778 RID: 10104
		internal int objectId;

		// Token: 0x04002779 RID: 10105
		internal int value;
	}
}
