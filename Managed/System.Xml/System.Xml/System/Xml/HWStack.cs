using System;

namespace System.Xml
{
	// Token: 0x02000240 RID: 576
	internal class HWStack : ICloneable
	{
		// Token: 0x06001672 RID: 5746 RVA: 0x0007C092 File Offset: 0x0007A292
		internal HWStack(int GrowthRate)
			: this(GrowthRate, int.MaxValue)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		internal HWStack(int GrowthRate, int limit)
		{
			this.growthRate = GrowthRate;
			this.used = 0;
			this.stack = new object[GrowthRate];
			this.size = GrowthRate;
			this.limit = limit;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
		internal object Push()
		{
			if (this.used == this.size)
			{
				if (this.limit <= this.used)
				{
					throw new XmlException("Stack overflow.", string.Empty);
				}
				object[] array = new object[this.size + this.growthRate];
				if (this.used > 0)
				{
					Array.Copy(this.stack, 0, array, 0, this.used);
				}
				this.stack = array;
				this.size += this.growthRate;
			}
			object[] array2 = this.stack;
			int num = this.used;
			this.used = num + 1;
			return array2[num];
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0007C16B File Offset: 0x0007A36B
		internal object Pop()
		{
			if (0 < this.used)
			{
				this.used--;
				return this.stack[this.used];
			}
			return null;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0007C193 File Offset: 0x0007A393
		internal object Peek()
		{
			if (this.used <= 0)
			{
				return null;
			}
			return this.stack[this.used - 1];
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0007C1AF File Offset: 0x0007A3AF
		internal void AddToTop(object o)
		{
			if (this.used > 0)
			{
				this.stack[this.used - 1] = o;
			}
		}

		// Token: 0x17000482 RID: 1154
		internal object this[int index]
		{
			get
			{
				if (index >= 0 && index < this.used)
				{
					return this.stack[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this.used)
				{
					this.stack[index] = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0007C205 File Offset: 0x0007A405
		internal int Length
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0007C20D File Offset: 0x0007A40D
		private HWStack(object[] stack, int growthRate, int used, int size)
		{
			this.stack = stack;
			this.growthRate = growthRate;
			this.used = used;
			this.size = size;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0007C232 File Offset: 0x0007A432
		public object Clone()
		{
			return new HWStack((object[])this.stack.Clone(), this.growthRate, this.used, this.size);
		}

		// Token: 0x04000E2B RID: 3627
		private object[] stack;

		// Token: 0x04000E2C RID: 3628
		private int growthRate;

		// Token: 0x04000E2D RID: 3629
		private int used;

		// Token: 0x04000E2E RID: 3630
		private int size;

		// Token: 0x04000E2F RID: 3631
		private int limit;
	}
}
