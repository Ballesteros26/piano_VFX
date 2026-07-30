using System;

namespace TMPro
{
	// Token: 0x0200002D RID: 45
	public struct TMP_LineInfo
	{
		// Token: 0x04000163 RID: 355
		internal int controlCharacterCount;

		// Token: 0x04000164 RID: 356
		public int characterCount;

		// Token: 0x04000165 RID: 357
		public int visibleCharacterCount;

		// Token: 0x04000166 RID: 358
		public int spaceCount;

		// Token: 0x04000167 RID: 359
		public int wordCount;

		// Token: 0x04000168 RID: 360
		public int firstCharacterIndex;

		// Token: 0x04000169 RID: 361
		public int firstVisibleCharacterIndex;

		// Token: 0x0400016A RID: 362
		public int lastCharacterIndex;

		// Token: 0x0400016B RID: 363
		public int lastVisibleCharacterIndex;

		// Token: 0x0400016C RID: 364
		public float length;

		// Token: 0x0400016D RID: 365
		public float lineHeight;

		// Token: 0x0400016E RID: 366
		public float ascender;

		// Token: 0x0400016F RID: 367
		public float baseline;

		// Token: 0x04000170 RID: 368
		public float descender;

		// Token: 0x04000171 RID: 369
		public float maxAdvance;

		// Token: 0x04000172 RID: 370
		public float width;

		// Token: 0x04000173 RID: 371
		public float marginLeft;

		// Token: 0x04000174 RID: 372
		public float marginRight;

		// Token: 0x04000175 RID: 373
		public HorizontalAlignmentOptions alignment;

		// Token: 0x04000176 RID: 374
		public Extents lineExtents;
	}
}
