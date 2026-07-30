using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200070B RID: 1803
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x06004B75 RID: 19317 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x0010D132 File Offset: 0x0010B332
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x0010D154 File Offset: 0x0010B354
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002759 RID: 10073
		internal int assemId;

		// Token: 0x0400275A RID: 10074
		internal int assemblyIndex;
	}
}
