using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000258 RID: 600
	internal static class InitialStyle
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x0004DC60 File Offset: 0x0004BE60
		public static ComputedStyle Get()
		{
			return InitialStyle.s_InitialStyle;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004DC78 File Offset: 0x0004BE78
		static InitialStyle()
		{
			InitialStyle.s_InitialStyle.nonInheritedData.alignContent = Align.FlexStart;
			InitialStyle.s_InitialStyle.nonInheritedData.alignItems = Align.Stretch;
			InitialStyle.s_InitialStyle.nonInheritedData.alignSelf = Align.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.backgroundColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.backgroundImage = default(StyleBackground);
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomLeftRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomRightRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderBottomWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderLeftColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderLeftWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderRightColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderRightWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopColor = Color.clear;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopLeftRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopRightRadius = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.borderTopWidth = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.bottom = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.inheritedData.color = Color.black;
			InitialStyle.s_InitialStyle.nonInheritedData.cursor = default(StyleCursor);
			InitialStyle.s_InitialStyle.nonInheritedData.display = DisplayStyle.Flex;
			InitialStyle.s_InitialStyle.nonInheritedData.flexBasis = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.flexDirection = FlexDirection.Column;
			InitialStyle.s_InitialStyle.nonInheritedData.flexGrow = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.flexShrink = 1f;
			InitialStyle.s_InitialStyle.nonInheritedData.flexWrap = Wrap.NoWrap;
			InitialStyle.s_InitialStyle.inheritedData.fontSize = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.height = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.justifyContent = Justify.FlexStart;
			InitialStyle.s_InitialStyle.nonInheritedData.left = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.marginBottom = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginLeft = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginRight = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.marginTop = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.maxHeight = StyleKeyword.None;
			InitialStyle.s_InitialStyle.nonInheritedData.maxWidth = StyleKeyword.None;
			InitialStyle.s_InitialStyle.nonInheritedData.minHeight = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.minWidth = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.opacity = 1f;
			InitialStyle.s_InitialStyle.nonInheritedData.overflow = OverflowInternal.Visible;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingBottom = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingLeft = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingRight = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.paddingTop = 0f;
			InitialStyle.s_InitialStyle.nonInheritedData.position = Position.Relative;
			InitialStyle.s_InitialStyle.nonInheritedData.right = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.textOverflow = TextOverflow.Clip;
			InitialStyle.s_InitialStyle.nonInheritedData.top = StyleKeyword.Auto;
			InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundImageTintColor = Color.white;
			InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundScaleMode = ScaleMode.StretchToFill;
			InitialStyle.s_InitialStyle.inheritedData.unityFont = default(StyleFont);
			InitialStyle.s_InitialStyle.inheritedData.unityFontStyleAndWeight = FontStyle.Normal;
			InitialStyle.s_InitialStyle.nonInheritedData.unityOverflowClipBox = OverflowClipBox.PaddingBox;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceBottom = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceLeft = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceRight = 0;
			InitialStyle.s_InitialStyle.nonInheritedData.unitySliceTop = 0;
			InitialStyle.s_InitialStyle.inheritedData.unityTextAlign = TextAnchor.UpperLeft;
			InitialStyle.s_InitialStyle.nonInheritedData.unityTextOverflowPosition = TextOverflowPosition.End;
			InitialStyle.s_InitialStyle.inheritedData.visibility = Visibility.Visible;
			InitialStyle.s_InitialStyle.inheritedData.whiteSpace = WhiteSpace.Normal;
			InitialStyle.s_InitialStyle.nonInheritedData.width = StyleKeyword.Auto;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004E213 File Offset: 0x0004C413
		public static StyleEnum<Align> alignContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignContent;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004E224 File Offset: 0x0004C424
		public static StyleEnum<Align> alignItems
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignItems;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004E235 File Offset: 0x0004C435
		public static StyleEnum<Align> alignSelf
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.alignSelf;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004E246 File Offset: 0x0004C446
		public static StyleColor backgroundColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.backgroundColor;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004E257 File Offset: 0x0004C457
		public static StyleBackground backgroundImage
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.backgroundImage;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004E268 File Offset: 0x0004C468
		public static StyleColor borderBottomColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomColor;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004E279 File Offset: 0x0004C479
		public static StyleLength borderBottomLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomLeftRadius;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004E28A File Offset: 0x0004C48A
		public static StyleLength borderBottomRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomRightRadius;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004E29B File Offset: 0x0004C49B
		public static StyleFloat borderBottomWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderBottomWidth;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004E2AC File Offset: 0x0004C4AC
		public static StyleColor borderLeftColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderLeftColor;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004E2BD File Offset: 0x0004C4BD
		public static StyleFloat borderLeftWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderLeftWidth;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0004E2CE File Offset: 0x0004C4CE
		public static StyleColor borderRightColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderRightColor;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004E2DF File Offset: 0x0004C4DF
		public static StyleFloat borderRightWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderRightWidth;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
		public static StyleColor borderTopColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopColor;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004E301 File Offset: 0x0004C501
		public static StyleLength borderTopLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopLeftRadius;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004E312 File Offset: 0x0004C512
		public static StyleLength borderTopRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopRightRadius;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004E323 File Offset: 0x0004C523
		public static StyleFloat borderTopWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.borderTopWidth;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004E334 File Offset: 0x0004C534
		public static StyleLength bottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.bottom;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004E345 File Offset: 0x0004C545
		public static StyleColor color
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.color;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004E356 File Offset: 0x0004C556
		public static StyleCursor cursor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.cursor;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004E367 File Offset: 0x0004C567
		public static StyleEnum<DisplayStyle> display
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.display;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0004E378 File Offset: 0x0004C578
		public static StyleLength flexBasis
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexBasis;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004E389 File Offset: 0x0004C589
		public static StyleEnum<FlexDirection> flexDirection
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexDirection;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0004E39A File Offset: 0x0004C59A
		public static StyleFloat flexGrow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexGrow;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004E3AB File Offset: 0x0004C5AB
		public static StyleFloat flexShrink
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexShrink;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0004E3BC File Offset: 0x0004C5BC
		public static StyleEnum<Wrap> flexWrap
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.flexWrap;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004E3CD File Offset: 0x0004C5CD
		public static StyleLength fontSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.fontSize;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0004E3DE File Offset: 0x0004C5DE
		public static StyleLength height
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.height;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004E3EF File Offset: 0x0004C5EF
		public static StyleEnum<Justify> justifyContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.justifyContent;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004E400 File Offset: 0x0004C600
		public static StyleLength left
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.left;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004E411 File Offset: 0x0004C611
		public static StyleLength marginBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginBottom;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0004E422 File Offset: 0x0004C622
		public static StyleLength marginLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginLeft;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004E433 File Offset: 0x0004C633
		public static StyleLength marginRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginRight;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0004E444 File Offset: 0x0004C644
		public static StyleLength marginTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.marginTop;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004E455 File Offset: 0x0004C655
		public static StyleLength maxHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.maxHeight;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0004E466 File Offset: 0x0004C666
		public static StyleLength maxWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.maxWidth;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004E477 File Offset: 0x0004C677
		public static StyleLength minHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.minHeight;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004E488 File Offset: 0x0004C688
		public static StyleLength minWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.minWidth;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004E499 File Offset: 0x0004C699
		public static StyleFloat opacity
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.opacity;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004E4AA File Offset: 0x0004C6AA
		public static StyleEnum<OverflowInternal> overflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.overflow;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004E4BB File Offset: 0x0004C6BB
		public static StyleLength paddingBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingBottom;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0004E4CC File Offset: 0x0004C6CC
		public static StyleLength paddingLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingLeft;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004E4DD File Offset: 0x0004C6DD
		public static StyleLength paddingRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingRight;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0004E4EE File Offset: 0x0004C6EE
		public static StyleLength paddingTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.paddingTop;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004E4FF File Offset: 0x0004C6FF
		public static StyleEnum<Position> position
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.position;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004E510 File Offset: 0x0004C710
		public static StyleLength right
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.right;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004E521 File Offset: 0x0004C721
		public static StyleEnum<TextOverflow> textOverflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.textOverflow;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004E532 File Offset: 0x0004C732
		public static StyleLength top
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.top;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004E543 File Offset: 0x0004C743
		public static StyleColor unityBackgroundImageTintColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004E554 File Offset: 0x0004C754
		public static StyleEnum<ScaleMode> unityBackgroundScaleMode
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityBackgroundScaleMode;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004E565 File Offset: 0x0004C765
		public static StyleFont unityFont
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityFont;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0004E576 File Offset: 0x0004C776
		public static StyleEnum<FontStyle> unityFontStyleAndWeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityFontStyleAndWeight;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004E587 File Offset: 0x0004C787
		public static StyleEnum<OverflowClipBox> unityOverflowClipBox
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityOverflowClipBox;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004E598 File Offset: 0x0004C798
		public static StyleInt unitySliceBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceBottom;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004E5A9 File Offset: 0x0004C7A9
		public static StyleInt unitySliceLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceLeft;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0004E5BA File Offset: 0x0004C7BA
		public static StyleInt unitySliceRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceRight;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004E5CB File Offset: 0x0004C7CB
		public static StyleInt unitySliceTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unitySliceTop;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004E5DC File Offset: 0x0004C7DC
		public static StyleEnum<TextAnchor> unityTextAlign
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.unityTextAlign;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004E5ED File Offset: 0x0004C7ED
		public static StyleEnum<TextOverflowPosition> unityTextOverflowPosition
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.unityTextOverflowPosition;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004E5FE File Offset: 0x0004C7FE
		public static StyleEnum<Visibility> visibility
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.visibility;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004E60F File Offset: 0x0004C80F
		public static StyleEnum<WhiteSpace> whiteSpace
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.whiteSpace;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004E620 File Offset: 0x0004C820
		public static StyleLength width
		{
			get
			{
				return InitialStyle.s_InitialStyle.nonInheritedData.width;
			}
		}

		// Token: 0x04000896 RID: 2198
		private static ComputedStyle s_InitialStyle = ComputedStyle.CreateUninitialized(true);
	}
}
