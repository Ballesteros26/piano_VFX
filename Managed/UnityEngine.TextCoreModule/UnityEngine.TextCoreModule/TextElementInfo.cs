using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000026 RID: 38
	internal struct TextElementInfo
	{
		// Token: 0x04000187 RID: 391
		public char character;

		// Token: 0x04000188 RID: 392
		public int index;

		// Token: 0x04000189 RID: 393
		public TextElementType elementType;

		// Token: 0x0400018A RID: 394
		public TextElement textElement;

		// Token: 0x0400018B RID: 395
		public FontAsset fontAsset;

		// Token: 0x0400018C RID: 396
		public TextSpriteAsset spriteAsset;

		// Token: 0x0400018D RID: 397
		public int spriteIndex;

		// Token: 0x0400018E RID: 398
		public Material material;

		// Token: 0x0400018F RID: 399
		public int materialReferenceIndex;

		// Token: 0x04000190 RID: 400
		public bool isUsingAlternateTypeface;

		// Token: 0x04000191 RID: 401
		public float pointSize;

		// Token: 0x04000192 RID: 402
		public int lineNumber;

		// Token: 0x04000193 RID: 403
		public int pageNumber;

		// Token: 0x04000194 RID: 404
		public int vertexIndex;

		// Token: 0x04000195 RID: 405
		public TextVertex vertexTopLeft;

		// Token: 0x04000196 RID: 406
		public TextVertex vertexBottomLeft;

		// Token: 0x04000197 RID: 407
		public TextVertex vertexTopRight;

		// Token: 0x04000198 RID: 408
		public TextVertex vertexBottomRight;

		// Token: 0x04000199 RID: 409
		public Vector3 topLeft;

		// Token: 0x0400019A RID: 410
		public Vector3 bottomLeft;

		// Token: 0x0400019B RID: 411
		public Vector3 topRight;

		// Token: 0x0400019C RID: 412
		public Vector3 bottomRight;

		// Token: 0x0400019D RID: 413
		public float origin;

		// Token: 0x0400019E RID: 414
		public float ascender;

		// Token: 0x0400019F RID: 415
		public float baseLine;

		// Token: 0x040001A0 RID: 416
		public float descender;

		// Token: 0x040001A1 RID: 417
		public float xAdvance;

		// Token: 0x040001A2 RID: 418
		public float aspectRatio;

		// Token: 0x040001A3 RID: 419
		public float scale;

		// Token: 0x040001A4 RID: 420
		public Color32 color;

		// Token: 0x040001A5 RID: 421
		public Color32 underlineColor;

		// Token: 0x040001A6 RID: 422
		public Color32 strikethroughColor;

		// Token: 0x040001A7 RID: 423
		public Color32 highlightColor;

		// Token: 0x040001A8 RID: 424
		public FontStyles style;

		// Token: 0x040001A9 RID: 425
		public bool isVisible;
	}
}
