using System;

namespace System.Net
{
	// Token: 0x0200045B RID: 1115
	internal class SplitWritesState
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x0007F6EC File Offset: 0x0007D8EC
		internal SplitWritesState(BufferOffsetSize[] buffers)
		{
			this._UserBuffers = buffers;
			this._LastBufferConsumed = 0;
			this._Index = 0;
			this._RealBuffers = null;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0007F710 File Offset: 0x0007D910
		internal bool IsDone
		{
			get
			{
				if (this._LastBufferConsumed != 0)
				{
					return false;
				}
				for (int i = this._Index; i < this._UserBuffers.Length; i++)
				{
					if (this._UserBuffers[i].Size != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0007F754 File Offset: 0x0007D954
		internal BufferOffsetSize[] GetNextBuffers()
		{
			int i = this._Index;
			int num = 0;
			int num2 = 0;
			int num3 = this._LastBufferConsumed;
			while (this._Index < this._UserBuffers.Length)
			{
				num2 = this._UserBuffers[this._Index].Size - this._LastBufferConsumed;
				num += num2;
				if (num > 65536)
				{
					num2 -= num - 65536;
					num = 65536;
					break;
				}
				num2 = 0;
				this._LastBufferConsumed = 0;
				this._Index++;
			}
			if (num == 0)
			{
				return null;
			}
			if (num3 == 0 && i == 0 && this._Index == this._UserBuffers.Length)
			{
				return this._UserBuffers;
			}
			int num4 = ((num2 == 0) ? (this._Index - i) : (this._Index - i + 1));
			if (this._RealBuffers == null || this._RealBuffers.Length != num4)
			{
				this._RealBuffers = new BufferOffsetSize[num4];
			}
			int num5 = 0;
			while (i < this._Index)
			{
				this._RealBuffers[num5++] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + num3, this._UserBuffers[i].Size - num3, false);
				num3 = 0;
				i++;
			}
			if (num2 != 0)
			{
				this._RealBuffers[num5] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + this._LastBufferConsumed, num2, false);
				if ((this._LastBufferConsumed += num2) == this._UserBuffers[this._Index].Size)
				{
					this._Index++;
					this._LastBufferConsumed = 0;
				}
			}
			return this._RealBuffers;
		}

		// Token: 0x04001DE6 RID: 7654
		private const int c_SplitEncryptedBuffersSize = 65536;

		// Token: 0x04001DE7 RID: 7655
		private BufferOffsetSize[] _UserBuffers;

		// Token: 0x04001DE8 RID: 7656
		private int _Index;

		// Token: 0x04001DE9 RID: 7657
		private int _LastBufferConsumed;

		// Token: 0x04001DEA RID: 7658
		private BufferOffsetSize[] _RealBuffers;
	}
}
