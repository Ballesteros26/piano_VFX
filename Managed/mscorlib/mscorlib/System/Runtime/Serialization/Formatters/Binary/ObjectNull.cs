using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000718 RID: 1816
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x06004BC3 RID: 19395 RVA: 0x00002111 File Offset: 0x00000311
		internal ObjectNull()
		{
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0010E69B File Offset: 0x0010C89B
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0010E6A4 File Offset: 0x0010C8A4
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0010E6FA File Offset: 0x0010C8FA
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0010E708 File Offset: 0x0010C908
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0010E754 File Offset: 0x0010C954
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY") && this.nullCount != 1)
			{
				int num = this.nullCount;
			}
		}

		// Token: 0x04002798 RID: 10136
		internal int nullCount;
	}
}
