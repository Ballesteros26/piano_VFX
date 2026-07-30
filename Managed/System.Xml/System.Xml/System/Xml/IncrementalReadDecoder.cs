using System;

namespace System.Xml
{
	// Token: 0x020000A3 RID: 163
	internal abstract class IncrementalReadDecoder
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000572 RID: 1394
		internal abstract int DecodedCount { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000573 RID: 1395
		internal abstract bool IsFull { get; }

		// Token: 0x06000574 RID: 1396
		internal abstract void SetNextOutputBuffer(Array array, int offset, int len);

		// Token: 0x06000575 RID: 1397
		internal abstract int Decode(char[] chars, int startPos, int len);

		// Token: 0x06000576 RID: 1398
		internal abstract int Decode(string str, int startPos, int len);

		// Token: 0x06000577 RID: 1399
		internal abstract void Reset();
	}
}
