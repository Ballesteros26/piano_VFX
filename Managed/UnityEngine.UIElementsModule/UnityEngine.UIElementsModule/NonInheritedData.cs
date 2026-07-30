using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BD RID: 445
	internal struct NonInheritedData : IEquatable<NonInheritedData>
	{
		// Token: 0x06000E34 RID: 3636 RVA: 0x00034ECC File Offset: 0x000330CC
		public static bool operator ==(NonInheritedData lhs, NonInheritedData rhs)
		{
			return lhs.alignContent.value == rhs.alignContent.value && lhs.alignContent.keyword == rhs.alignContent.keyword && lhs.alignItems.value == rhs.alignItems.value && lhs.alignItems.keyword == rhs.alignItems.keyword && lhs.alignSelf.value == rhs.alignSelf.value && lhs.alignSelf.keyword == rhs.alignSelf.keyword && lhs.backgroundColor == rhs.backgroundColor && lhs.backgroundImage == rhs.backgroundImage && lhs.borderBottomColor == rhs.borderBottomColor && lhs.borderBottomLeftRadius == rhs.borderBottomLeftRadius && lhs.borderBottomRightRadius == rhs.borderBottomRightRadius && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftColor == rhs.borderLeftColor && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightColor == rhs.borderRightColor && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopColor == rhs.borderTopColor && lhs.borderTopLeftRadius == rhs.borderTopLeftRadius && lhs.borderTopRightRadius == rhs.borderTopRightRadius && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.cursor == rhs.cursor && lhs.display.value == rhs.display.value && lhs.display.keyword == rhs.display.keyword && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection.value == rhs.flexDirection.value && lhs.flexDirection.keyword == rhs.flexDirection.keyword && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap.value == rhs.flexWrap.value && lhs.flexWrap.keyword == rhs.flexWrap.keyword && lhs.height == rhs.height && lhs.justifyContent.value == rhs.justifyContent.value && lhs.justifyContent.keyword == rhs.justifyContent.keyword && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.opacity == rhs.opacity && lhs.overflow.value == rhs.overflow.value && lhs.overflow.keyword == rhs.overflow.keyword && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position.value == rhs.position.value && lhs.position.keyword == rhs.position.keyword && lhs.right == rhs.right && lhs.textOverflow.value == rhs.textOverflow.value && lhs.textOverflow.keyword == rhs.textOverflow.keyword && lhs.top == rhs.top && lhs.unityBackgroundImageTintColor == rhs.unityBackgroundImageTintColor && lhs.unityBackgroundScaleMode.value == rhs.unityBackgroundScaleMode.value && lhs.unityBackgroundScaleMode.keyword == rhs.unityBackgroundScaleMode.keyword && lhs.unityOverflowClipBox.value == rhs.unityOverflowClipBox.value && lhs.unityOverflowClipBox.keyword == rhs.unityOverflowClipBox.keyword && lhs.unitySliceBottom == rhs.unitySliceBottom && lhs.unitySliceLeft == rhs.unitySliceLeft && lhs.unitySliceRight == rhs.unitySliceRight && lhs.unitySliceTop == rhs.unitySliceTop && lhs.unityTextOverflowPosition.value == rhs.unityTextOverflowPosition.value && lhs.unityTextOverflowPosition.keyword == rhs.unityTextOverflowPosition.keyword && lhs.width == rhs.width;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003555C File Offset: 0x0003375C
		public static bool operator !=(NonInheritedData lhs, NonInheritedData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00035578 File Offset: 0x00033778
		public bool Equals(NonInheritedData other)
		{
			return other == this;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00035598 File Offset: 0x00033798
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NonInheritedData && this.Equals((NonInheritedData)obj);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000355D0 File Offset: 0x000337D0
		public override int GetHashCode()
		{
			int num = this.alignContent.GetHashCode();
			num = (num * 397) ^ this.alignItems.GetHashCode();
			num = (num * 397) ^ this.alignSelf.GetHashCode();
			num = (num * 397) ^ this.backgroundColor.GetHashCode();
			num = (num * 397) ^ this.backgroundImage.GetHashCode();
			num = (num * 397) ^ this.borderBottomColor.GetHashCode();
			num = (num * 397) ^ this.borderBottomLeftRadius.GetHashCode();
			num = (num * 397) ^ this.borderBottomRightRadius.GetHashCode();
			num = (num * 397) ^ this.borderBottomWidth.GetHashCode();
			num = (num * 397) ^ this.borderLeftColor.GetHashCode();
			num = (num * 397) ^ this.borderLeftWidth.GetHashCode();
			num = (num * 397) ^ this.borderRightColor.GetHashCode();
			num = (num * 397) ^ this.borderRightWidth.GetHashCode();
			num = (num * 397) ^ this.borderTopColor.GetHashCode();
			num = (num * 397) ^ this.borderTopLeftRadius.GetHashCode();
			num = (num * 397) ^ this.borderTopRightRadius.GetHashCode();
			num = (num * 397) ^ this.borderTopWidth.GetHashCode();
			num = (num * 397) ^ this.bottom.GetHashCode();
			num = (num * 397) ^ this.cursor.GetHashCode();
			num = (num * 397) ^ this.display.GetHashCode();
			num = (num * 397) ^ this.flexBasis.GetHashCode();
			num = (num * 397) ^ this.flexDirection.GetHashCode();
			num = (num * 397) ^ this.flexGrow.GetHashCode();
			num = (num * 397) ^ this.flexShrink.GetHashCode();
			num = (num * 397) ^ this.flexWrap.GetHashCode();
			num = (num * 397) ^ this.height.GetHashCode();
			num = (num * 397) ^ this.justifyContent.GetHashCode();
			num = (num * 397) ^ this.left.GetHashCode();
			num = (num * 397) ^ this.marginBottom.GetHashCode();
			num = (num * 397) ^ this.marginLeft.GetHashCode();
			num = (num * 397) ^ this.marginRight.GetHashCode();
			num = (num * 397) ^ this.marginTop.GetHashCode();
			num = (num * 397) ^ this.maxHeight.GetHashCode();
			num = (num * 397) ^ this.maxWidth.GetHashCode();
			num = (num * 397) ^ this.minHeight.GetHashCode();
			num = (num * 397) ^ this.minWidth.GetHashCode();
			num = (num * 397) ^ this.opacity.GetHashCode();
			num = (num * 397) ^ this.overflow.GetHashCode();
			num = (num * 397) ^ this.paddingBottom.GetHashCode();
			num = (num * 397) ^ this.paddingLeft.GetHashCode();
			num = (num * 397) ^ this.paddingRight.GetHashCode();
			num = (num * 397) ^ this.paddingTop.GetHashCode();
			num = (num * 397) ^ this.position.GetHashCode();
			num = (num * 397) ^ this.right.GetHashCode();
			num = (num * 397) ^ this.textOverflow.GetHashCode();
			num = (num * 397) ^ this.top.GetHashCode();
			num = (num * 397) ^ this.unityBackgroundImageTintColor.GetHashCode();
			num = (num * 397) ^ this.unityBackgroundScaleMode.GetHashCode();
			num = (num * 397) ^ this.unityOverflowClipBox.GetHashCode();
			num = (num * 397) ^ this.unitySliceBottom.GetHashCode();
			num = (num * 397) ^ this.unitySliceLeft.GetHashCode();
			num = (num * 397) ^ this.unitySliceRight.GetHashCode();
			num = (num * 397) ^ this.unitySliceTop.GetHashCode();
			num = (num * 397) ^ this.unityTextOverflowPosition.GetHashCode();
			return (num * 397) ^ this.width.GetHashCode();
		}

		// Token: 0x04000552 RID: 1362
		public StyleEnum<Align> alignContent;

		// Token: 0x04000553 RID: 1363
		public StyleEnum<Align> alignItems;

		// Token: 0x04000554 RID: 1364
		public StyleEnum<Align> alignSelf;

		// Token: 0x04000555 RID: 1365
		public StyleColor backgroundColor;

		// Token: 0x04000556 RID: 1366
		public StyleBackground backgroundImage;

		// Token: 0x04000557 RID: 1367
		public StyleColor borderBottomColor;

		// Token: 0x04000558 RID: 1368
		public StyleLength borderBottomLeftRadius;

		// Token: 0x04000559 RID: 1369
		public StyleLength borderBottomRightRadius;

		// Token: 0x0400055A RID: 1370
		public StyleFloat borderBottomWidth;

		// Token: 0x0400055B RID: 1371
		public StyleColor borderLeftColor;

		// Token: 0x0400055C RID: 1372
		public StyleFloat borderLeftWidth;

		// Token: 0x0400055D RID: 1373
		public StyleColor borderRightColor;

		// Token: 0x0400055E RID: 1374
		public StyleFloat borderRightWidth;

		// Token: 0x0400055F RID: 1375
		public StyleColor borderTopColor;

		// Token: 0x04000560 RID: 1376
		public StyleLength borderTopLeftRadius;

		// Token: 0x04000561 RID: 1377
		public StyleLength borderTopRightRadius;

		// Token: 0x04000562 RID: 1378
		public StyleFloat borderTopWidth;

		// Token: 0x04000563 RID: 1379
		public StyleLength bottom;

		// Token: 0x04000564 RID: 1380
		public StyleCursor cursor;

		// Token: 0x04000565 RID: 1381
		public StyleEnum<DisplayStyle> display;

		// Token: 0x04000566 RID: 1382
		public StyleLength flexBasis;

		// Token: 0x04000567 RID: 1383
		public StyleEnum<FlexDirection> flexDirection;

		// Token: 0x04000568 RID: 1384
		public StyleFloat flexGrow;

		// Token: 0x04000569 RID: 1385
		public StyleFloat flexShrink;

		// Token: 0x0400056A RID: 1386
		public StyleEnum<Wrap> flexWrap;

		// Token: 0x0400056B RID: 1387
		public StyleLength height;

		// Token: 0x0400056C RID: 1388
		public StyleEnum<Justify> justifyContent;

		// Token: 0x0400056D RID: 1389
		public StyleLength left;

		// Token: 0x0400056E RID: 1390
		public StyleLength marginBottom;

		// Token: 0x0400056F RID: 1391
		public StyleLength marginLeft;

		// Token: 0x04000570 RID: 1392
		public StyleLength marginRight;

		// Token: 0x04000571 RID: 1393
		public StyleLength marginTop;

		// Token: 0x04000572 RID: 1394
		public StyleLength maxHeight;

		// Token: 0x04000573 RID: 1395
		public StyleLength maxWidth;

		// Token: 0x04000574 RID: 1396
		public StyleLength minHeight;

		// Token: 0x04000575 RID: 1397
		public StyleLength minWidth;

		// Token: 0x04000576 RID: 1398
		public StyleFloat opacity;

		// Token: 0x04000577 RID: 1399
		public StyleEnum<OverflowInternal> overflow;

		// Token: 0x04000578 RID: 1400
		public StyleLength paddingBottom;

		// Token: 0x04000579 RID: 1401
		public StyleLength paddingLeft;

		// Token: 0x0400057A RID: 1402
		public StyleLength paddingRight;

		// Token: 0x0400057B RID: 1403
		public StyleLength paddingTop;

		// Token: 0x0400057C RID: 1404
		public StyleEnum<Position> position;

		// Token: 0x0400057D RID: 1405
		public StyleLength right;

		// Token: 0x0400057E RID: 1406
		public StyleEnum<TextOverflow> textOverflow;

		// Token: 0x0400057F RID: 1407
		public StyleLength top;

		// Token: 0x04000580 RID: 1408
		public StyleColor unityBackgroundImageTintColor;

		// Token: 0x04000581 RID: 1409
		public StyleEnum<ScaleMode> unityBackgroundScaleMode;

		// Token: 0x04000582 RID: 1410
		public StyleEnum<OverflowClipBox> unityOverflowClipBox;

		// Token: 0x04000583 RID: 1411
		public StyleInt unitySliceBottom;

		// Token: 0x04000584 RID: 1412
		public StyleInt unitySliceLeft;

		// Token: 0x04000585 RID: 1413
		public StyleInt unitySliceRight;

		// Token: 0x04000586 RID: 1414
		public StyleInt unitySliceTop;

		// Token: 0x04000587 RID: 1415
		public StyleEnum<TextOverflowPosition> unityTextOverflowPosition;

		// Token: 0x04000588 RID: 1416
		public StyleLength width;
	}
}
