using System;

namespace System.Xml
{
	// Token: 0x020000A5 RID: 165
	internal class IncrementalReadCharsDecoder : IncrementalReadDecoder
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0000DE25 File Offset: 0x0000C025
		internal IncrementalReadCharsDecoder()
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000182C8 File Offset: 0x000164C8
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000182D7 File Offset: 0x000164D7
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000182E8 File Offset: 0x000164E8
		internal override int Decode(char[] chars, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			Buffer.BlockCopy(chars, startPos * 2, this.buffer, this.curIndex * 2, num * 2);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00018334 File Offset: 0x00016534
		internal override int Decode(string str, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			str.CopyTo(startPos, this.buffer, this.curIndex, num);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void Reset()
		{
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00018378 File Offset: 0x00016578
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (char[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x04000336 RID: 822
		private char[] buffer;

		// Token: 0x04000337 RID: 823
		private int startIndex;

		// Token: 0x04000338 RID: 824
		private int curIndex;

		// Token: 0x04000339 RID: 825
		private int endIndex;
	}
}
