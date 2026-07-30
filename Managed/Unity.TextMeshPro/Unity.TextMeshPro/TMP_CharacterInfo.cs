using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000E RID: 14
	public struct TMP_CharacterInfo
	{
		// Token: 0x0400002A RID: 42
		public char character;

		// Token: 0x0400002B RID: 43
		public int index;

		// Token: 0x0400002C RID: 44
		public int stringLength;

		// Token: 0x0400002D RID: 45
		public TMP_TextElementType elementType;

		// Token: 0x0400002E RID: 46
		public TMP_TextElement textElement;

		// Token: 0x0400002F RID: 47
		public TMP_FontAsset fontAsset;

		// Token: 0x04000030 RID: 48
		public TMP_SpriteAsset spriteAsset;

		// Token: 0x04000031 RID: 49
		public int spriteIndex;

		// Token: 0x04000032 RID: 50
		public Material material;

		// Token: 0x04000033 RID: 51
		public int materialReferenceIndex;

		// Token: 0x04000034 RID: 52
		public bool isUsingAlternateTypeface;

		// Token: 0x04000035 RID: 53
		public float pointSize;

		// Token: 0x04000036 RID: 54
		public int lineNumber;

		// Token: 0x04000037 RID: 55
		public int pageNumber;

		// Token: 0x04000038 RID: 56
		public int vertexIndex;

		// Token: 0x04000039 RID: 57
		public TMP_Vertex vertex_BL;

		// Token: 0x0400003A RID: 58
		public TMP_Vertex vertex_TL;

		// Token: 0x0400003B RID: 59
		public TMP_Vertex vertex_TR;

		// Token: 0x0400003C RID: 60
		public TMP_Vertex vertex_BR;

		// Token: 0x0400003D RID: 61
		public Vector3 topLeft;

		// Token: 0x0400003E RID: 62
		public Vector3 bottomLeft;

		// Token: 0x0400003F RID: 63
		public Vector3 topRight;

		// Token: 0x04000040 RID: 64
		public Vector3 bottomRight;

		// Token: 0x04000041 RID: 65
		public float origin;

		// Token: 0x04000042 RID: 66
		public float ascender;

		// Token: 0x04000043 RID: 67
		public float baseLine;

		// Token: 0x04000044 RID: 68
		public float descender;

		// Token: 0x04000045 RID: 69
		public float xAdvance;

		// Token: 0x04000046 RID: 70
		public float aspectRatio;

		// Token: 0x04000047 RID: 71
		public float scale;

		// Token: 0x04000048 RID: 72
		public Color32 color;

		// Token: 0x04000049 RID: 73
		public Color32 underlineColor;

		// Token: 0x0400004A RID: 74
		public int underlineVertexIndex;

		// Token: 0x0400004B RID: 75
		public Color32 strikethroughColor;

		// Token: 0x0400004C RID: 76
		public int strikethroughVertexIndex;

		// Token: 0x0400004D RID: 77
		public Color32 highlightColor;

		// Token: 0x0400004E RID: 78
		public HighlightState highlightState;

		// Token: 0x0400004F RID: 79
		public FontStyles style;

		// Token: 0x04000050 RID: 80
		public bool isVisible;
	}
}
