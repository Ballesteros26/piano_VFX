using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200070C RID: 1804
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x06004B7A RID: 19322 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryObject()
		{
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x0010D16E File Offset: 0x0010B36E
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x0010D17E File Offset: 0x0010B37E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x0010D19F File Offset: 0x0010B39F
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0010CF1D File Offset: 0x0010B11D
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400275B RID: 10075
		internal int objectId;

		// Token: 0x0400275C RID: 10076
		internal int mapId;
	}
}
