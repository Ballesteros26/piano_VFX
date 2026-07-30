using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000711 RID: 1809
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x06004B9A RID: 19354 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0010DDD1 File Offset: 0x0010BFD1
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0010DDE7 File Offset: 0x0010BFE7
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400277A RID: 10106
		internal int crossAppDomainArrayIndex;
	}
}
