using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000285 RID: 645
	internal static class Lerp
	{
		// Token: 0x06001326 RID: 4902 RVA: 0x000554B8 File Offset: 0x000536B8
		public static float Interpolate(float start, float end, float ratio)
		{
			return Mathf.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000554D4 File Offset: 0x000536D4
		public static int Interpolate(int start, int end, float ratio)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)start, (float)end, ratio));
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000554F8 File Offset: 0x000536F8
		public static Rect Interpolate(Rect r1, Rect r2, float ratio)
		{
			return new Rect(Mathf.LerpUnclamped(r1.x, r2.x, ratio), Mathf.LerpUnclamped(r1.y, r2.y, ratio), Mathf.LerpUnclamped(r1.width, r2.width, ratio), Mathf.LerpUnclamped(r1.height, r2.height, ratio));
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00055560 File Offset: 0x00053760
		public static Color Interpolate(Color start, Color end, float ratio)
		{
			return Color.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0005557C File Offset: 0x0005377C
		public static Vector2 Interpolate(Vector2 start, Vector2 end, float ratio)
		{
			return Vector2.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00055598 File Offset: 0x00053798
		public static Vector3 Interpolate(Vector3 start, Vector3 end, float ratio)
		{
			return Vector3.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000555B4 File Offset: 0x000537B4
		public static Quaternion Interpolate(Quaternion start, Quaternion end, float ratio)
		{
			return Quaternion.SlerpUnclamped(start, end, ratio);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000555D0 File Offset: 0x000537D0
		internal static StyleValues Interpolate(StyleValues start, StyleValues end, float ratio)
		{
			StyleValues styleValues = default(StyleValues);
			foreach (StyleValue styleValue in end.m_StyleValues.m_Values)
			{
				StyleValue styleValue2 = default(StyleValue);
				bool flag = !start.m_StyleValues.TryGetStyleValue(styleValue.id, ref styleValue2);
				if (flag)
				{
					throw new ArgumentException("Start StyleValues must contain the same values as end values. Missing property:" + styleValue.id);
				}
				StylePropertyId id = styleValue.id;
				if (id <= StylePropertyId.Width)
				{
					switch (id)
					{
					case StylePropertyId.Custom:
					case StylePropertyId.Unknown:
					case StylePropertyId.UnityFont:
					case StylePropertyId.UnityFontStyleAndWeight:
					case StylePropertyId.UnityTextAlign:
					case StylePropertyId.Visibility:
					case StylePropertyId.WhiteSpace:
						goto IL_01F8;
					case StylePropertyId.Color:
						goto IL_01D4;
					case StylePropertyId.FontSize:
						break;
					default:
						switch (id)
						{
						case StylePropertyId.AlignContent:
						case StylePropertyId.AlignItems:
						case StylePropertyId.AlignSelf:
						case StylePropertyId.BackgroundImage:
						case StylePropertyId.BorderBottomColor:
						case StylePropertyId.BorderLeftColor:
						case StylePropertyId.BorderRightColor:
						case StylePropertyId.BorderTopColor:
						case StylePropertyId.Cursor:
						case StylePropertyId.Display:
						case StylePropertyId.FlexDirection:
						case StylePropertyId.FlexWrap:
						case StylePropertyId.JustifyContent:
						case StylePropertyId.Overflow:
						case StylePropertyId.Position:
						case StylePropertyId.TextOverflow:
						case StylePropertyId.UnityBackgroundScaleMode:
						case StylePropertyId.UnityOverflowClipBox:
						case StylePropertyId.UnitySliceBottom:
						case StylePropertyId.UnitySliceLeft:
						case StylePropertyId.UnitySliceRight:
						case StylePropertyId.UnitySliceTop:
						case StylePropertyId.UnityTextOverflowPosition:
							goto IL_01F8;
						case StylePropertyId.BackgroundColor:
						case StylePropertyId.UnityBackgroundImageTintColor:
							goto IL_01D4;
						case StylePropertyId.BorderBottomLeftRadius:
						case StylePropertyId.BorderBottomRightRadius:
						case StylePropertyId.BorderBottomWidth:
						case StylePropertyId.BorderLeftWidth:
						case StylePropertyId.BorderRightWidth:
						case StylePropertyId.BorderTopLeftRadius:
						case StylePropertyId.BorderTopRightRadius:
						case StylePropertyId.BorderTopWidth:
						case StylePropertyId.Bottom:
						case StylePropertyId.FlexBasis:
						case StylePropertyId.FlexGrow:
						case StylePropertyId.FlexShrink:
						case StylePropertyId.Height:
						case StylePropertyId.Left:
						case StylePropertyId.MarginBottom:
						case StylePropertyId.MarginLeft:
						case StylePropertyId.MarginRight:
						case StylePropertyId.MarginTop:
						case StylePropertyId.MaxHeight:
						case StylePropertyId.MaxWidth:
						case StylePropertyId.MinHeight:
						case StylePropertyId.MinWidth:
						case StylePropertyId.Opacity:
						case StylePropertyId.PaddingBottom:
						case StylePropertyId.PaddingLeft:
						case StylePropertyId.PaddingRight:
						case StylePropertyId.PaddingTop:
						case StylePropertyId.Right:
						case StylePropertyId.Top:
						case StylePropertyId.Width:
							break;
						default:
							goto IL_01F8;
						}
						break;
					}
					styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.number, styleValue.number, ratio));
				}
				else
				{
					if (id == StylePropertyId.BorderColor)
					{
						goto IL_01D4;
					}
					if (id - StylePropertyId.BorderRadius > 4)
					{
						goto IL_01F8;
					}
					goto IL_01F8;
				}
				continue;
				IL_01D4:
				styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.color, styleValue.color, ratio));
				continue;
				IL_01F8:
				throw new ArgumentException("Style Value can't be animated");
			}
			return styleValues;
		}
	}
}
