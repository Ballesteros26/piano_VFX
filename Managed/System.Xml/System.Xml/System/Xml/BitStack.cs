using System;

namespace System.Xml
{
	// Token: 0x02000089 RID: 137
	internal class BitStack
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001601E File Offset: 0x0001421E
		public BitStack()
		{
			this.curr = 1U;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001602D File Offset: 0x0001422D
		public void PushBit(bool bit)
		{
			if ((this.curr & 2147483648U) != 0U)
			{
				this.PushCurr();
			}
			this.curr = (this.curr << 1) | (bit ? 1U : 0U);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016059 File Offset: 0x00014259
		public bool PopBit()
		{
			bool flag = (this.curr & 1U) > 0U;
			this.curr >>= 1;
			if (this.curr == 1U)
			{
				this.PopCurr();
			}
			return flag;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00016083 File Offset: 0x00014283
		public bool PeekBit()
		{
			return (this.curr & 1U) > 0U;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00016090 File Offset: 0x00014290
		public bool IsEmpty
		{
			get
			{
				return this.curr == 1U;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001609C File Offset: 0x0001429C
		private void PushCurr()
		{
			if (this.bitStack == null)
			{
				this.bitStack = new uint[16];
			}
			uint[] array = this.bitStack;
			int num = this.stackPos;
			this.stackPos = num + 1;
			array[num] = this.curr;
			this.curr = 1U;
			int num2 = this.bitStack.Length;
			if (this.stackPos >= num2)
			{
				uint[] array2 = new uint[2 * num2];
				Array.Copy(this.bitStack, array2, num2);
				this.bitStack = array2;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00016114 File Offset: 0x00014314
		private void PopCurr()
		{
			if (this.stackPos > 0)
			{
				uint[] array = this.bitStack;
				int num = this.stackPos - 1;
				this.stackPos = num;
				this.curr = array[num];
			}
		}

		// Token: 0x040002F8 RID: 760
		private uint[] bitStack;

		// Token: 0x040002F9 RID: 761
		private int stackPos;

		// Token: 0x040002FA RID: 762
		private uint curr;
	}
}
