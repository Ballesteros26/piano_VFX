using System;

namespace System.Xml
{
	// Token: 0x0200008B RID: 139
	internal class ByteStack
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x00016215 File Offset: 0x00014415
		public ByteStack(int growthRate)
		{
			this.growthRate = growthRate;
			this.top = 0;
			this.stack = new byte[growthRate];
			this.size = growthRate;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00016240 File Offset: 0x00014440
		public void Push(byte data)
		{
			if (this.size == this.top)
			{
				byte[] array = new byte[this.size + this.growthRate];
				if (this.top > 0)
				{
					Buffer.BlockCopy(this.stack, 0, array, 0, this.top);
				}
				this.stack = array;
				this.size += this.growthRate;
			}
			byte[] array2 = this.stack;
			int num = this.top;
			this.top = num + 1;
			array2[num] = data;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000162C0 File Offset: 0x000144C0
		public byte Pop()
		{
			if (this.top > 0)
			{
				byte[] array = this.stack;
				int num = this.top - 1;
				this.top = num;
				return array[num];
			}
			return 0;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000162F0 File Offset: 0x000144F0
		public byte Peek()
		{
			if (this.top > 0)
			{
				return this.stack[this.top - 1];
			}
			return 0;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001630C File Offset: 0x0001450C
		public int Length
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x04000300 RID: 768
		private byte[] stack;

		// Token: 0x04000301 RID: 769
		private int growthRate;

		// Token: 0x04000302 RID: 770
		private int top;

		// Token: 0x04000303 RID: 771
		private int size;
	}
}
