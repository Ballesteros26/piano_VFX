using System;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B9 RID: 441
	internal static class StyleValueExtensions
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x00034AA4 File Offset: 0x00032CA4
		internal static StyleFloat ToStyleFloat(this StyleLength styleLength)
		{
			return new StyleFloat(styleLength.value.value, styleLength.keyword);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00034AD4 File Offset: 0x00032CD4
		internal static StyleEnum<T> ToStyleEnum<T>(this StyleInt styleInt, T value) where T : struct, IConvertible
		{
			return new StyleEnum<T>(value, styleInt.keyword);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00034AF4 File Offset: 0x00032CF4
		internal static StyleLength ToStyleLength(this StyleValue styleValue)
		{
			return new StyleLength(new Length(styleValue.number), styleValue.keyword);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00034B1C File Offset: 0x00032D1C
		internal static StyleFloat ToStyleFloat(this StyleValue styleValue)
		{
			return new StyleFloat(styleValue.number, styleValue.keyword);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00034B40 File Offset: 0x00032D40
		internal static string DebugString<T>(this IStyleValue<T> styleValue)
		{
			return (styleValue.keyword != StyleKeyword.Undefined) ? string.Format("{0}", styleValue.keyword) : string.Format("{0}", styleValue.value);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00034B88 File Offset: 0x00032D88
		internal static YogaValue ToYogaValue(this StyleLength styleValue)
		{
			bool flag = styleValue.keyword == StyleKeyword.Auto;
			YogaValue yogaValue;
			if (flag)
			{
				yogaValue = YogaValue.Auto();
			}
			else
			{
				bool flag2 = styleValue.keyword == StyleKeyword.None;
				if (flag2)
				{
					yogaValue = float.NaN;
				}
				else
				{
					Length value = styleValue.value;
					LengthUnit unit = value.unit;
					if (unit != LengthUnit.Pixel)
					{
						if (unit != LengthUnit.Percent)
						{
							Debug.LogAssertion(string.Format("Unexpected unit '{0}'", value.unit));
							yogaValue = float.NaN;
						}
						else
						{
							yogaValue = YogaValue.Percent(value.value);
						}
					}
					else
					{
						yogaValue = YogaValue.Point(value.value);
					}
				}
			}
			return yogaValue;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00034C30 File Offset: 0x00032E30
		internal static StyleKeyword ToStyleKeyword(this StyleValueKeyword styleValueKeyword)
		{
			StyleKeyword styleKeyword;
			if (styleValueKeyword != StyleValueKeyword.Initial)
			{
				if (styleValueKeyword != StyleValueKeyword.Auto)
				{
					if (styleValueKeyword != StyleValueKeyword.None)
					{
						styleKeyword = StyleKeyword.Undefined;
					}
					else
					{
						styleKeyword = StyleKeyword.None;
					}
				}
				else
				{
					styleKeyword = StyleKeyword.Auto;
				}
			}
			else
			{
				styleKeyword = StyleKeyword.Initial;
			}
			return styleKeyword;
		}
	}
}
