using System;

namespace System.Xml
{
	// Token: 0x020000A4 RID: 164
	internal class IncrementalReadDummyDecoder : IncrementalReadDecoder
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000182C2 File Offset: 0x000164C2
		internal override int DecodedCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000226C File Offset: 0x0000046C
		internal override bool IsFull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void SetNextOutputBuffer(Array array, int offset, int len)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000182C5 File Offset: 0x000164C5
		internal override int Decode(char[] chars, int startPos, int len)
		{
			return len;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000182C5 File Offset: 0x000164C5
		internal override int Decode(string str, int startPos, int len)
		{
			return len;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void Reset()
		{
		}
	}
}
