using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000015 RID: 21
	internal struct LineInfo
	{
		// Token: 0x04000075 RID: 117
		internal int controlCharacterCount;

		// Token: 0x04000076 RID: 118
		public int characterCount;

		// Token: 0x04000077 RID: 119
		public int visibleCharacterCount;

		// Token: 0x04000078 RID: 120
		public int spaceCount;

		// Token: 0x04000079 RID: 121
		public int wordCount;

		// Token: 0x0400007A RID: 122
		public int firstCharacterIndex;

		// Token: 0x0400007B RID: 123
		public int firstVisibleCharacterIndex;

		// Token: 0x0400007C RID: 124
		public int lastCharacterIndex;

		// Token: 0x0400007D RID: 125
		public int lastVisibleCharacterIndex;

		// Token: 0x0400007E RID: 126
		public float length;

		// Token: 0x0400007F RID: 127
		public float lineHeight;

		// Token: 0x04000080 RID: 128
		public float ascender;

		// Token: 0x04000081 RID: 129
		public float baseline;

		// Token: 0x04000082 RID: 130
		public float descender;

		// Token: 0x04000083 RID: 131
		public float maxAdvance;

		// Token: 0x04000084 RID: 132
		public float width;

		// Token: 0x04000085 RID: 133
		public float marginLeft;

		// Token: 0x04000086 RID: 134
		public float marginRight;

		// Token: 0x04000087 RID: 135
		public TextAlignment alignment;

		// Token: 0x04000088 RID: 136
		public Extents lineExtents;
	}
}
