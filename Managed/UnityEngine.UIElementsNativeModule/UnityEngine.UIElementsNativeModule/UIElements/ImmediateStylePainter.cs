using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/UIElementsNative/ImmediateStylePainter.h")]
	[StructLayout(0)]
	internal class ImmediateStylePainter
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00003BA1 File Offset: 0x00001DA1
		internal static void DrawRect(Rect screenRect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			ImmediateStylePainter.DrawRect_Injected(ref screenRect, ref color, ref borderWidths, ref borderRadiuses);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003BB0 File Offset: 0x00001DB0
		internal static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, int leftBorder, int topBorder, int rightBorder, int bottomBorder, bool usePremultiplyAlpha)
		{
			ImmediateStylePainter.DrawTexture_Injected(ref screenRect, texture, ref sourceRect, ref color, ref borderWidths, ref borderRadiuses, leftBorder, topBorder, rightBorder, bottomBorder, usePremultiplyAlpha);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003BD8 File Offset: 0x00001DD8
		internal static void DrawText(Rect screenRect, string text, Font font, int fontSize, FontStyle fontStyle, Color fontColor, TextAnchor anchor, bool wordWrap, float wordWrapWidth, bool richText, TextClipping textClipping)
		{
			ImmediateStylePainter.DrawText_Injected(ref screenRect, text, font, fontSize, fontStyle, ref fontColor, anchor, wordWrap, wordWrapWidth, richText, textClipping);
		}

		// Token: 0x06000157 RID: 343
		[MethodImpl(4096)]
		private static extern void DrawRect_Injected(ref Rect screenRect, ref Color color, ref Vector4 borderWidths, ref Vector4 borderRadiuses);

		// Token: 0x06000158 RID: 344
		[MethodImpl(4096)]
		private static extern void DrawTexture_Injected(ref Rect screenRect, Texture texture, ref Rect sourceRect, ref Color color, ref Vector4 borderWidths, ref Vector4 borderRadiuses, int leftBorder, int topBorder, int rightBorder, int bottomBorder, bool usePremultiplyAlpha);

		// Token: 0x06000159 RID: 345
		[MethodImpl(4096)]
		private static extern void DrawText_Injected(ref Rect screenRect, string text, Font font, int fontSize, FontStyle fontStyle, ref Color fontColor, TextAnchor anchor, bool wordWrap, float wordWrapWidth, bool richText, TextClipping textClipping);
	}
}
