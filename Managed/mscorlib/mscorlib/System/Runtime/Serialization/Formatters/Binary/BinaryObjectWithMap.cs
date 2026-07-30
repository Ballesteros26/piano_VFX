using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000713 RID: 1811
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x06004BA5 RID: 19365 RVA: 0x00002111 File Offset: 0x00000311
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0010DE4D File Offset: 0x0010C04D
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0010DE5C File Offset: 0x0010C05C
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0010DE98 File Offset: 0x0010C098
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0010DF0C File Offset: 0x0010C10C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0010DF84 File Offset: 0x0010C184
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x0400277D RID: 10109
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x0400277E RID: 10110
		internal int objectId;

		// Token: 0x0400277F RID: 10111
		internal string name;

		// Token: 0x04002780 RID: 10112
		internal int numMembers;

		// Token: 0x04002781 RID: 10113
		internal string[] memberNames;

		// Token: 0x04002782 RID: 10114
		internal int assemId;
	}
}
