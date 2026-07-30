using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200025E RID: 606
	internal static class StylePropertyUtil
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004F360 File Offset: 0x0004D560
		public static int GetEnumIntValue(StyleEnumType enumType, string value)
		{
			int num;
			switch (enumType)
			{
			case StyleEnumType.Align:
			{
				bool flag = string.Equals(value, "auto", 5);
				if (flag)
				{
					num = 0;
				}
				else
				{
					bool flag2 = string.Equals(value, "flex-start", 5);
					if (flag2)
					{
						num = 1;
					}
					else
					{
						bool flag3 = string.Equals(value, "center", 5);
						if (flag3)
						{
							num = 2;
						}
						else
						{
							bool flag4 = string.Equals(value, "flex-end", 5);
							if (flag4)
							{
								num = 3;
							}
							else
							{
								bool flag5 = string.Equals(value, "stretch", 5);
								if (flag5)
								{
									num = 4;
								}
								else
								{
									num = 0;
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.DisplayStyle:
			{
				bool flag6 = string.Equals(value, "flex", 5);
				if (flag6)
				{
					num = 0;
				}
				else
				{
					bool flag7 = string.Equals(value, "none", 5);
					if (flag7)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.FlexDirection:
			{
				bool flag8 = string.Equals(value, "column", 5);
				if (flag8)
				{
					num = 0;
				}
				else
				{
					bool flag9 = string.Equals(value, "column-reverse", 5);
					if (flag9)
					{
						num = 1;
					}
					else
					{
						bool flag10 = string.Equals(value, "row", 5);
						if (flag10)
						{
							num = 2;
						}
						else
						{
							bool flag11 = string.Equals(value, "row-reverse", 5);
							if (flag11)
							{
								num = 3;
							}
							else
							{
								num = 0;
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.FontStyle:
			{
				bool flag12 = string.Equals(value, "normal", 5);
				if (flag12)
				{
					num = 0;
				}
				else
				{
					bool flag13 = string.Equals(value, "bold", 5);
					if (flag13)
					{
						num = 1;
					}
					else
					{
						bool flag14 = string.Equals(value, "italic", 5);
						if (flag14)
						{
							num = 2;
						}
						else
						{
							bool flag15 = string.Equals(value, "bold-and-italic", 5);
							if (flag15)
							{
								num = 3;
							}
							else
							{
								num = 0;
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.Justify:
			{
				bool flag16 = string.Equals(value, "flex-start", 5);
				if (flag16)
				{
					num = 0;
				}
				else
				{
					bool flag17 = string.Equals(value, "center", 5);
					if (flag17)
					{
						num = 1;
					}
					else
					{
						bool flag18 = string.Equals(value, "flex-end", 5);
						if (flag18)
						{
							num = 2;
						}
						else
						{
							bool flag19 = string.Equals(value, "space-between", 5);
							if (flag19)
							{
								num = 3;
							}
							else
							{
								bool flag20 = string.Equals(value, "space-around", 5);
								if (flag20)
								{
									num = 4;
								}
								else
								{
									num = 0;
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.Overflow:
			{
				bool flag21 = string.Equals(value, "visible", 5);
				if (flag21)
				{
					num = 0;
				}
				else
				{
					bool flag22 = string.Equals(value, "hidden", 5);
					if (flag22)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.OverflowClipBox:
			{
				bool flag23 = string.Equals(value, "padding-box", 5);
				if (flag23)
				{
					num = 0;
				}
				else
				{
					bool flag24 = string.Equals(value, "content-box", 5);
					if (flag24)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.OverflowInternal:
			{
				bool flag25 = string.Equals(value, "visible", 5);
				if (flag25)
				{
					num = 0;
				}
				else
				{
					bool flag26 = string.Equals(value, "hidden", 5);
					if (flag26)
					{
						num = 1;
					}
					else
					{
						bool flag27 = string.Equals(value, "scroll", 5);
						if (flag27)
						{
							num = 2;
						}
						else
						{
							num = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.Position:
			{
				bool flag28 = string.Equals(value, "relative", 5);
				if (flag28)
				{
					num = 0;
				}
				else
				{
					bool flag29 = string.Equals(value, "absolute", 5);
					if (flag29)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.ScaleMode:
			{
				bool flag30 = string.Equals(value, "stretch-to-fill", 5);
				if (flag30)
				{
					num = 0;
				}
				else
				{
					bool flag31 = string.Equals(value, "scale-and-crop", 5);
					if (flag31)
					{
						num = 1;
					}
					else
					{
						bool flag32 = string.Equals(value, "scale-to-fit", 5);
						if (flag32)
						{
							num = 2;
						}
						else
						{
							num = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.TextAnchor:
			{
				bool flag33 = string.Equals(value, "upper-left", 5);
				if (flag33)
				{
					num = 0;
				}
				else
				{
					bool flag34 = string.Equals(value, "upper-center", 5);
					if (flag34)
					{
						num = 1;
					}
					else
					{
						bool flag35 = string.Equals(value, "upper-right", 5);
						if (flag35)
						{
							num = 2;
						}
						else
						{
							bool flag36 = string.Equals(value, "middle-left", 5);
							if (flag36)
							{
								num = 3;
							}
							else
							{
								bool flag37 = string.Equals(value, "middle-center", 5);
								if (flag37)
								{
									num = 4;
								}
								else
								{
									bool flag38 = string.Equals(value, "middle-right", 5);
									if (flag38)
									{
										num = 5;
									}
									else
									{
										bool flag39 = string.Equals(value, "lower-left", 5);
										if (flag39)
										{
											num = 6;
										}
										else
										{
											bool flag40 = string.Equals(value, "lower-center", 5);
											if (flag40)
											{
												num = 7;
											}
											else
											{
												bool flag41 = string.Equals(value, "lower-right", 5);
												if (flag41)
												{
													num = 8;
												}
												else
												{
													num = 0;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				break;
			}
			case StyleEnumType.TextOverflow:
			{
				bool flag42 = string.Equals(value, "clip", 5);
				if (flag42)
				{
					num = 0;
				}
				else
				{
					bool flag43 = string.Equals(value, "ellipsis", 5);
					if (flag43)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.TextOverflowPosition:
			{
				bool flag44 = string.Equals(value, "start", 5);
				if (flag44)
				{
					num = 1;
				}
				else
				{
					bool flag45 = string.Equals(value, "middle", 5);
					if (flag45)
					{
						num = 2;
					}
					else
					{
						bool flag46 = string.Equals(value, "end", 5);
						if (flag46)
						{
							num = 0;
						}
						else
						{
							num = 0;
						}
					}
				}
				break;
			}
			case StyleEnumType.Visibility:
			{
				bool flag47 = string.Equals(value, "visible", 5);
				if (flag47)
				{
					num = 0;
				}
				else
				{
					bool flag48 = string.Equals(value, "hidden", 5);
					if (flag48)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.WhiteSpace:
			{
				bool flag49 = string.Equals(value, "normal", 5);
				if (flag49)
				{
					num = 0;
				}
				else
				{
					bool flag50 = string.Equals(value, "nowrap", 5);
					if (flag50)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				break;
			}
			case StyleEnumType.Wrap:
			{
				bool flag51 = string.Equals(value, "nowrap", 5);
				if (flag51)
				{
					num = 0;
				}
				else
				{
					bool flag52 = string.Equals(value, "wrap", 5);
					if (flag52)
					{
						num = 1;
					}
					else
					{
						bool flag53 = string.Equals(value, "wrap-reverse", 5);
						if (flag53)
						{
							num = 2;
						}
						else
						{
							num = 0;
						}
					}
				}
				break;
			}
			default:
				num = 0;
				break;
			}
			return num;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004F954 File Offset: 0x0004DB54
		// Note: this type is marked as 'beforefieldinit'.
		static StylePropertyUtil()
		{
			Dictionary<string, StylePropertyId> dictionary = new Dictionary<string, StylePropertyId>();
			dictionary.Add("align-content", StylePropertyId.AlignContent);
			dictionary.Add("align-items", StylePropertyId.AlignItems);
			dictionary.Add("align-self", StylePropertyId.AlignSelf);
			dictionary.Add("background-color", StylePropertyId.BackgroundColor);
			dictionary.Add("background-image", StylePropertyId.BackgroundImage);
			dictionary.Add("border-bottom-color", StylePropertyId.BorderBottomColor);
			dictionary.Add("border-bottom-left-radius", StylePropertyId.BorderBottomLeftRadius);
			dictionary.Add("border-bottom-right-radius", StylePropertyId.BorderBottomRightRadius);
			dictionary.Add("border-bottom-width", StylePropertyId.BorderBottomWidth);
			dictionary.Add("border-color", StylePropertyId.BorderColor);
			dictionary.Add("border-left-color", StylePropertyId.BorderLeftColor);
			dictionary.Add("border-left-width", StylePropertyId.BorderLeftWidth);
			dictionary.Add("border-radius", StylePropertyId.BorderRadius);
			dictionary.Add("border-right-color", StylePropertyId.BorderRightColor);
			dictionary.Add("border-right-width", StylePropertyId.BorderRightWidth);
			dictionary.Add("border-top-color", StylePropertyId.BorderTopColor);
			dictionary.Add("border-top-left-radius", StylePropertyId.BorderTopLeftRadius);
			dictionary.Add("border-top-right-radius", StylePropertyId.BorderTopRightRadius);
			dictionary.Add("border-top-width", StylePropertyId.BorderTopWidth);
			dictionary.Add("border-width", StylePropertyId.BorderWidth);
			dictionary.Add("bottom", StylePropertyId.Bottom);
			dictionary.Add("color", StylePropertyId.Color);
			dictionary.Add("cursor", StylePropertyId.Cursor);
			dictionary.Add("display", StylePropertyId.Display);
			dictionary.Add("flex", StylePropertyId.Flex);
			dictionary.Add("flex-basis", StylePropertyId.FlexBasis);
			dictionary.Add("flex-direction", StylePropertyId.FlexDirection);
			dictionary.Add("flex-grow", StylePropertyId.FlexGrow);
			dictionary.Add("flex-shrink", StylePropertyId.FlexShrink);
			dictionary.Add("flex-wrap", StylePropertyId.FlexWrap);
			dictionary.Add("font-size", StylePropertyId.FontSize);
			dictionary.Add("height", StylePropertyId.Height);
			dictionary.Add("justify-content", StylePropertyId.JustifyContent);
			dictionary.Add("left", StylePropertyId.Left);
			dictionary.Add("margin", StylePropertyId.Margin);
			dictionary.Add("margin-bottom", StylePropertyId.MarginBottom);
			dictionary.Add("margin-left", StylePropertyId.MarginLeft);
			dictionary.Add("margin-right", StylePropertyId.MarginRight);
			dictionary.Add("margin-top", StylePropertyId.MarginTop);
			dictionary.Add("max-height", StylePropertyId.MaxHeight);
			dictionary.Add("max-width", StylePropertyId.MaxWidth);
			dictionary.Add("min-height", StylePropertyId.MinHeight);
			dictionary.Add("min-width", StylePropertyId.MinWidth);
			dictionary.Add("opacity", StylePropertyId.Opacity);
			dictionary.Add("overflow", StylePropertyId.Overflow);
			dictionary.Add("padding", StylePropertyId.Padding);
			dictionary.Add("padding-bottom", StylePropertyId.PaddingBottom);
			dictionary.Add("padding-left", StylePropertyId.PaddingLeft);
			dictionary.Add("padding-right", StylePropertyId.PaddingRight);
			dictionary.Add("padding-top", StylePropertyId.PaddingTop);
			dictionary.Add("position", StylePropertyId.Position);
			dictionary.Add("right", StylePropertyId.Right);
			dictionary.Add("text-overflow", StylePropertyId.TextOverflow);
			dictionary.Add("top", StylePropertyId.Top);
			dictionary.Add("-unity-background-image-tint-color", StylePropertyId.UnityBackgroundImageTintColor);
			dictionary.Add("-unity-background-scale-mode", StylePropertyId.UnityBackgroundScaleMode);
			dictionary.Add("-unity-font", StylePropertyId.UnityFont);
			dictionary.Add("-unity-font-style", StylePropertyId.UnityFontStyleAndWeight);
			dictionary.Add("-unity-overflow-clip-box", StylePropertyId.UnityOverflowClipBox);
			dictionary.Add("-unity-slice-bottom", StylePropertyId.UnitySliceBottom);
			dictionary.Add("-unity-slice-left", StylePropertyId.UnitySliceLeft);
			dictionary.Add("-unity-slice-right", StylePropertyId.UnitySliceRight);
			dictionary.Add("-unity-slice-top", StylePropertyId.UnitySliceTop);
			dictionary.Add("-unity-text-align", StylePropertyId.UnityTextAlign);
			dictionary.Add("-unity-text-overflow-position", StylePropertyId.UnityTextOverflowPosition);
			dictionary.Add("visibility", StylePropertyId.Visibility);
			dictionary.Add("white-space", StylePropertyId.WhiteSpace);
			dictionary.Add("width", StylePropertyId.Width);
			StylePropertyUtil.s_NameToId = dictionary;
		}

		// Token: 0x040008F4 RID: 2292
		public const int k_GroupOffset = 16;

		// Token: 0x040008F5 RID: 2293
		internal static readonly Dictionary<string, StylePropertyId> s_NameToId;
	}
}
