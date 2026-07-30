using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200073E RID: 1854
	internal sealed class SerStack
	{
		// Token: 0x06004CFC RID: 19708 RVA: 0x00116102 File Offset: 0x00114302
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x00116128 File Offset: 0x00114328
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0011614C File Offset: 0x0011434C
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0011618C File Offset: 0x0011438C
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object obj = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return obj;
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x001161CC File Offset: 0x001143CC
		internal void IncreaseCapacity()
		{
			object[] array = new object[this.objects.Length * 2];
			Array.Copy(this.objects, 0, array, 0, this.objects.Length);
			this.objects = array;
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00116206 File Offset: 0x00114406
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x00116220 File Offset: 0x00114420
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0011623C File Offset: 0x0011443C
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00116246 File Offset: 0x00114446
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00116254 File Offset: 0x00114454
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
			}
		}

		// Token: 0x04002959 RID: 10585
		internal object[] objects = new object[5];

		// Token: 0x0400295A RID: 10586
		internal string stackId;

		// Token: 0x0400295B RID: 10587
		internal int top = -1;

		// Token: 0x0400295C RID: 10588
		internal int next;
	}
}
