using System;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019D RID: 413
	internal static class MeshGenerationContextUtils
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0002B4EB File Offset: 0x000296EB
		public static void Rectangle(this MeshGenerationContext mgc, MeshGenerationContextUtils.RectangleParams rectParams)
		{
			mgc.painter.DrawRectangle(rectParams);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002B4FB File Offset: 0x000296FB
		public static void Border(this MeshGenerationContext mgc, MeshGenerationContextUtils.BorderParams borderParams)
		{
			mgc.painter.DrawBorder(borderParams);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002B50C File Offset: 0x0002970C
		public static void Text(this MeshGenerationContext mgc, MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			bool flag = textParams.font != null;
			if (flag)
			{
				mgc.painter.DrawText(textParams, handle, pixelsPerPoint);
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002B53C File Offset: 0x0002973C
		private static Vector2 ConvertBorderRadiusPercentToPoints(Vector2 borderRectSize, Length length)
		{
			float num = length.value;
			float num2 = length.value;
			bool flag = length.unit == LengthUnit.Percent;
			if (flag)
			{
				num = borderRectSize.x * length.value / 100f;
				num2 = borderRectSize.y * length.value / 100f;
			}
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Max(num2, 0f);
			return new Vector2(num, num2);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002B5B8 File Offset: 0x000297B8
		public static void GetVisualElementRadii(VisualElement ve, out Vector2 topLeft, out Vector2 bottomLeft, out Vector2 topRight, out Vector2 bottomRight)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			Vector2 vector = new Vector2(resolvedStyle.width, resolvedStyle.height);
			ComputedStyle computedStyle = ve.computedStyle;
			topLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderTopLeftRadius.value);
			bottomLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderBottomLeftRadius.value);
			topRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderTopRightRadius.value);
			bottomRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderBottomRightRadius.value);
		}

		// Token: 0x0200019E RID: 414
		public struct BorderParams
		{
			// Token: 0x040004DD RID: 1245
			public Rect rect;

			// Token: 0x040004DE RID: 1246
			public Color playmodeTintColor;

			// Token: 0x040004DF RID: 1247
			public Color leftColor;

			// Token: 0x040004E0 RID: 1248
			public Color topColor;

			// Token: 0x040004E1 RID: 1249
			public Color rightColor;

			// Token: 0x040004E2 RID: 1250
			public Color bottomColor;

			// Token: 0x040004E3 RID: 1251
			public float leftWidth;

			// Token: 0x040004E4 RID: 1252
			public float topWidth;

			// Token: 0x040004E5 RID: 1253
			public float rightWidth;

			// Token: 0x040004E6 RID: 1254
			public float bottomWidth;

			// Token: 0x040004E7 RID: 1255
			public Vector2 topLeftRadius;

			// Token: 0x040004E8 RID: 1256
			public Vector2 topRightRadius;

			// Token: 0x040004E9 RID: 1257
			public Vector2 bottomRightRadius;

			// Token: 0x040004EA RID: 1258
			public Vector2 bottomLeftRadius;

			// Token: 0x040004EB RID: 1259
			public Material material;
		}

		// Token: 0x0200019F RID: 415
		public struct RectangleParams
		{
			// Token: 0x06000B9A RID: 2970 RVA: 0x0002B650 File Offset: 0x00029850
			public static MeshGenerationContextUtils.RectangleParams MakeSolid(Rect rect, Color color, ContextType panelContext)
			{
				Color color2 = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					color = color,
					uv = new Rect(0f, 0f, 1f, 1f),
					playmodeTintColor = color2
				};
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x0002B6B4 File Offset: 0x000298B4
			public static MeshGenerationContextUtils.RectangleParams MakeTextured(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				float num = (float)texture.width * uv.width / ((float)texture.height * uv.height);
				float num2 = rect.width / rect.height;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					break;
				case ScaleMode.ScaleAndCrop:
				{
					bool flag = num2 > num;
					if (flag)
					{
						float num3 = uv.height * (num / num2);
						float num4 = (uv.height - num3) * 0.5f;
						uv = new Rect(uv.x, uv.y + num4, uv.width, num3);
					}
					else
					{
						float num5 = uv.width * (num2 / num);
						float num6 = (uv.width - num5) * 0.5f;
						uv = new Rect(uv.x + num6, uv.y, num5, uv.height);
					}
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag2 = num2 > num;
					if (flag2)
					{
						float num7 = num / num2;
						rect = new Rect(rect.xMin + rect.width * (1f - num7) * 0.5f, rect.yMin, num7 * rect.width, rect.height);
					}
					else
					{
						float num8 = num2 / num;
						rect = new Rect(rect.xMin, rect.yMin + rect.height * (1f - num8) * 0.5f, rect.width, num8 * rect.height);
					}
					break;
				}
				default:
					throw new NotImplementedException();
				}
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					texture = texture,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x0002B89C File Offset: 0x00029A9C
			public static MeshGenerationContextUtils.RectangleParams MakeVectorTextured(Rect rect, Rect uv, VectorImage vectorImage, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					vectorImage = vectorImage,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0002B900 File Offset: 0x00029B00
			internal bool HasRadius(float epsilon)
			{
				return (this.topLeftRadius.x > epsilon && this.topLeftRadius.y > epsilon) || (this.topRightRadius.x > epsilon && this.topRightRadius.y > epsilon) || (this.bottomRightRadius.x > epsilon && this.bottomRightRadius.y > epsilon) || (this.bottomLeftRadius.x > epsilon && this.bottomLeftRadius.y > epsilon);
			}

			// Token: 0x040004EC RID: 1260
			public Rect rect;

			// Token: 0x040004ED RID: 1261
			public Rect uv;

			// Token: 0x040004EE RID: 1262
			public Color color;

			// Token: 0x040004EF RID: 1263
			public Texture texture;

			// Token: 0x040004F0 RID: 1264
			public VectorImage vectorImage;

			// Token: 0x040004F1 RID: 1265
			public Material material;

			// Token: 0x040004F2 RID: 1266
			public ScaleMode scaleMode;

			// Token: 0x040004F3 RID: 1267
			public Color playmodeTintColor;

			// Token: 0x040004F4 RID: 1268
			public Vector2 topLeftRadius;

			// Token: 0x040004F5 RID: 1269
			public Vector2 topRightRadius;

			// Token: 0x040004F6 RID: 1270
			public Vector2 bottomRightRadius;

			// Token: 0x040004F7 RID: 1271
			public Vector2 bottomLeftRadius;

			// Token: 0x040004F8 RID: 1272
			public int leftSlice;

			// Token: 0x040004F9 RID: 1273
			public int topSlice;

			// Token: 0x040004FA RID: 1274
			public int rightSlice;

			// Token: 0x040004FB RID: 1275
			public int bottomSlice;
		}

		// Token: 0x020001A0 RID: 416
		public struct TextParams
		{
			// Token: 0x06000B9E RID: 2974 RVA: 0x0002B988 File Offset: 0x00029B88
			public override int GetHashCode()
			{
				int num = this.rect.GetHashCode();
				num = (num * 397) ^ ((this.text != null) ? this.text.GetHashCode() : 0);
				num = (num * 397) ^ ((this.font != null) ? this.font.GetHashCode() : 0);
				num = (num * 397) ^ this.fontSize;
				num = (num * 397) ^ (int)this.fontStyle;
				num = (num * 397) ^ this.fontColor.GetHashCode();
				num = (num * 397) ^ (int)this.anchor;
				num = (num * 397) ^ this.wordWrap.GetHashCode();
				num = (num * 397) ^ this.wordWrapWidth.GetHashCode();
				num = (num * 397) ^ this.richText.GetHashCode();
				num = (num * 397) ^ ((this.material != null) ? this.material.GetHashCode() : 0);
				num = (num * 397) ^ this.playmodeTintColor.GetHashCode();
				num = (num * 397) ^ this.textOverflowMode.GetHashCode();
				return (num * 397) ^ this.textOverflowPosition.GetHashCode();
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x0002BAE8 File Offset: 0x00029CE8
			internal static MeshGenerationContextUtils.TextParams MakeStyleBased(VisualElement ve, string text)
			{
				ComputedStyle computedStyle = ve.computedStyle;
				MeshGenerationContextUtils.TextParams textParams = default(MeshGenerationContextUtils.TextParams);
				textParams.rect = ve.contentRect;
				textParams.text = text;
				textParams.font = computedStyle.unityFont.value;
				textParams.fontSize = (int)computedStyle.fontSize.value.value;
				textParams.fontStyle = computedStyle.unityFontStyleAndWeight.value;
				textParams.fontColor = computedStyle.color.value;
				textParams.anchor = computedStyle.unityTextAlign.value;
				textParams.wordWrap = computedStyle.whiteSpace.value == WhiteSpace.Normal;
				textParams.wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f);
				textParams.richText = false;
				IPanel panel = ve.panel;
				textParams.playmodeTintColor = ((panel != null && panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				textParams.textOverflowMode = MeshGenerationContextUtils.TextParams.GetTextOverflowMode(computedStyle);
				textParams.textOverflowPosition = computedStyle.unityTextOverflowPosition.value;
				return textParams;
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0002BC34 File Offset: 0x00029E34
			public static TextOverflowMode GetTextOverflowMode(ComputedStyle style)
			{
				bool flag = style.textOverflow.value == TextOverflow.Clip;
				TextOverflowMode textOverflowMode;
				if (flag)
				{
					textOverflowMode = TextOverflowMode.Masking;
				}
				else
				{
					bool flag2 = style.textOverflow.value != TextOverflow.Ellipsis;
					if (flag2)
					{
						textOverflowMode = TextOverflowMode.Overflow;
					}
					else
					{
						bool flag3 = style.whiteSpace.value == WhiteSpace.NoWrap && style.overflow == OverflowInternal.Hidden;
						if (flag3)
						{
							textOverflowMode = TextOverflowMode.Ellipsis;
						}
						else
						{
							textOverflowMode = TextOverflowMode.Overflow;
						}
					}
				}
				return textOverflowMode;
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0002BCAC File Offset: 0x00029EAC
			internal static TextNativeSettings GetTextNativeSettings(MeshGenerationContextUtils.TextParams textParams, float scaling)
			{
				return new TextNativeSettings
				{
					text = textParams.text,
					font = textParams.font,
					size = textParams.fontSize,
					scaling = scaling,
					style = textParams.fontStyle,
					color = textParams.fontColor,
					anchor = textParams.anchor,
					wordWrap = textParams.wordWrap,
					wordWrapWidth = textParams.wordWrapWidth,
					richText = textParams.richText
				};
			}

			// Token: 0x040004FC RID: 1276
			public Rect rect;

			// Token: 0x040004FD RID: 1277
			public string text;

			// Token: 0x040004FE RID: 1278
			public Font font;

			// Token: 0x040004FF RID: 1279
			public int fontSize;

			// Token: 0x04000500 RID: 1280
			public FontStyle fontStyle;

			// Token: 0x04000501 RID: 1281
			public Color fontColor;

			// Token: 0x04000502 RID: 1282
			public TextAnchor anchor;

			// Token: 0x04000503 RID: 1283
			public bool wordWrap;

			// Token: 0x04000504 RID: 1284
			public float wordWrapWidth;

			// Token: 0x04000505 RID: 1285
			public bool richText;

			// Token: 0x04000506 RID: 1286
			public Material material;

			// Token: 0x04000507 RID: 1287
			public Color playmodeTintColor;

			// Token: 0x04000508 RID: 1288
			public TextOverflowMode textOverflowMode;

			// Token: 0x04000509 RID: 1289
			public TextOverflowPosition textOverflowPosition;
		}
	}
}
