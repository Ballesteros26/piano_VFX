using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200070A RID: 1802
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x06004B6F RID: 19311 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryAssembly()
		{
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0010D0E6 File Offset: 0x0010B2E6
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0010D0F6 File Offset: 0x0010B2F6
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0010D118 File Offset: 0x0010B318
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002757 RID: 10071
		internal int assemId;

		// Token: 0x04002758 RID: 10072
		internal string assemblyString;
	}
}
