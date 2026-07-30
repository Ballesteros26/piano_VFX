using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000060 RID: 96
	internal struct CursorPositionStylePainterParameters
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00008440 File Offset: 0x00006640
		public static CursorPositionStylePainterParameters GetDefault(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = ve.computedStyle;
			return new CursorPositionStylePainterParameters
			{
				rect = ve.contentRect,
				text = text,
				font = computedStyle.unityFont.value,
				fontSize = (int)computedStyle.fontSize.value.value,
				fontStyle = computedStyle.unityFontStyleAndWeight.value,
				anchor = computedStyle.unityTextAlign.value,
				wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = false,
				cursorIndex = 0
			};
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000851C File Offset: 0x0000671C
		internal TextNativeSettings GetTextNativeSettings(float scaling)
		{
			return new TextNativeSettings
			{
				text = this.text,
				font = this.font,
				size = this.fontSize,
				scaling = scaling,
				style = this.fontStyle,
				color = Color.white,
				anchor = this.anchor,
				wordWrap = true,
				wordWrapWidth = this.wordWrapWidth,
				richText = this.richText
			};
		}

		// Token: 0x0400011E RID: 286
		public Rect rect;

		// Token: 0x0400011F RID: 287
		public string text;

		// Token: 0x04000120 RID: 288
		public Font font;

		// Token: 0x04000121 RID: 289
		public int fontSize;

		// Token: 0x04000122 RID: 290
		public FontStyle fontStyle;

		// Token: 0x04000123 RID: 291
		public TextAnchor anchor;

		// Token: 0x04000124 RID: 292
		public float wordWrapWidth;

		// Token: 0x04000125 RID: 293
		public bool richText;

		// Token: 0x04000126 RID: 294
		public int cursorIndex;
	}
}
