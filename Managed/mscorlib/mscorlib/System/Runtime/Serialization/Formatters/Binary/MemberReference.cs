using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000717 RID: 1815
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x06004BBD RID: 19389 RVA: 0x00002111 File Offset: 0x00000311
		internal MemberReference()
		{
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0010E66E File Offset: 0x0010C86E
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0010E677 File Offset: 0x0010C877
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0010E68D File Offset: 0x0010C88D
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002797 RID: 10135
		internal int idRef;
	}
}
