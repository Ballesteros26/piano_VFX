using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000029 RID: 41
	internal class TextGenerationSettings
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00006F54 File Offset: 0x00005154
		protected bool Equals(TextGenerationSettings other)
		{
			return string.Equals(this.text, other.text) && this.screenRect.Equals(other.screenRect) && this.margins.Equals(other.margins) && this.scale.Equals(other.scale) && object.Equals(this.fontAsset, other.fontAsset) && object.Equals(this.material, other.material) && object.Equals(this.spriteAsset, other.spriteAsset) && this.fontStyle == other.fontStyle && this.textAlignment == other.textAlignment && this.overflowMode == other.overflowMode && this.wordWrap == other.wordWrap && this.wordWrappingRatio.Equals(other.wordWrappingRatio) && this.color.Equals(other.color) && object.Equals(this.fontColorGradient, other.fontColorGradient) && this.tintSprites == other.tintSprites && this.overrideRichTextColors == other.overrideRichTextColors && this.fontSize.Equals(other.fontSize) && this.autoSize == other.autoSize && this.fontSizeMin.Equals(other.fontSizeMin) && this.fontSizeMax.Equals(other.fontSizeMax) && this.enableKerning == other.enableKerning && this.richText == other.richText && this.isRightToLeft == other.isRightToLeft && this.extraPadding == other.extraPadding && this.parseControlCharacters == other.parseControlCharacters && this.characterSpacing.Equals(other.characterSpacing) && this.wordSpacing.Equals(other.wordSpacing) && this.lineSpacing.Equals(other.lineSpacing) && this.paragraphSpacing.Equals(other.paragraphSpacing) && this.lineSpacingMax.Equals(other.lineSpacingMax) && this.maxVisibleCharacters == other.maxVisibleCharacters && this.maxVisibleWords == other.maxVisibleWords && this.maxVisibleLines == other.maxVisibleLines && this.firstVisibleCharacter == other.firstVisibleCharacter && this.useMaxVisibleDescender == other.useMaxVisibleDescender && this.fontWeight == other.fontWeight && this.pageToDisplay == other.pageToDisplay && this.horizontalMapping == other.horizontalMapping && this.verticalMapping == other.verticalMapping && this.uvLineOffset.Equals(other.uvLineOffset) && this.geometrySortingOrder == other.geometrySortingOrder && this.inverseYAxis == other.inverseYAxis && this.charWidthMaxAdj.Equals(other.charWidthMaxAdj);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000728C File Offset: 0x0000548C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == obj;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = obj.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((TextGenerationSettings)obj);
				}
			}
			return flag2;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000072DC File Offset: 0x000054DC
		public override int GetHashCode()
		{
			int num = ((this.text != null) ? this.text.GetHashCode() : 0);
			num = (num * 397) ^ this.screenRect.GetHashCode();
			num = (num * 397) ^ this.margins.GetHashCode();
			num = (num * 397) ^ this.scale.GetHashCode();
			num = (num * 397) ^ ((this.fontAsset != null) ? this.fontAsset.GetHashCode() : 0);
			num = (num * 397) ^ ((this.material != null) ? this.material.GetHashCode() : 0);
			num = (num * 397) ^ ((this.spriteAsset != null) ? this.spriteAsset.GetHashCode() : 0);
			num = (num * 397) ^ (int)this.fontStyle;
			num = (num * 397) ^ (int)this.textAlignment;
			num = (num * 397) ^ (int)this.overflowMode;
			num = (num * 397) ^ this.wordWrap.GetHashCode();
			num = (num * 397) ^ this.wordWrappingRatio.GetHashCode();
			num = (num * 397) ^ this.color.GetHashCode();
			num = (num * 397) ^ ((this.fontColorGradient != null) ? this.fontColorGradient.GetHashCode() : 0);
			num = (num * 397) ^ this.tintSprites.GetHashCode();
			num = (num * 397) ^ this.overrideRichTextColors.GetHashCode();
			num = (num * 397) ^ this.fontSize.GetHashCode();
			num = (num * 397) ^ this.autoSize.GetHashCode();
			num = (num * 397) ^ this.fontSizeMin.GetHashCode();
			num = (num * 397) ^ this.fontSizeMax.GetHashCode();
			num = (num * 397) ^ this.enableKerning.GetHashCode();
			num = (num * 397) ^ this.richText.GetHashCode();
			num = (num * 397) ^ this.isRightToLeft.GetHashCode();
			num = (num * 397) ^ this.extraPadding.GetHashCode();
			num = (num * 397) ^ this.parseControlCharacters.GetHashCode();
			num = (num * 397) ^ this.characterSpacing.GetHashCode();
			num = (num * 397) ^ this.wordSpacing.GetHashCode();
			num = (num * 397) ^ this.lineSpacing.GetHashCode();
			num = (num * 397) ^ this.paragraphSpacing.GetHashCode();
			num = (num * 397) ^ this.lineSpacingMax.GetHashCode();
			num = (num * 397) ^ this.maxVisibleCharacters;
			num = (num * 397) ^ this.maxVisibleWords;
			num = (num * 397) ^ this.maxVisibleLines;
			num = (num * 397) ^ this.firstVisibleCharacter;
			num = (num * 397) ^ this.useMaxVisibleDescender.GetHashCode();
			num = (num * 397) ^ (int)this.fontWeight;
			num = (num * 397) ^ this.pageToDisplay;
			num = (num * 397) ^ (int)this.horizontalMapping;
			num = (num * 397) ^ (int)this.verticalMapping;
			num = (num * 397) ^ this.uvLineOffset.GetHashCode();
			num = (num * 397) ^ (int)this.geometrySortingOrder;
			num = (num * 397) ^ this.inverseYAxis.GetHashCode();
			return (num * 397) ^ this.charWidthMaxAdj.GetHashCode();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000766C File Offset: 0x0000586C
		public static bool operator ==(TextGenerationSettings left, TextGenerationSettings right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007688 File Offset: 0x00005888
		public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000076A4 File Offset: 0x000058A4
		public void Copy(TextGenerationSettings other)
		{
			bool flag = other == null;
			if (!flag)
			{
				this.text = other.text;
				this.screenRect = other.screenRect;
				this.margins = other.margins;
				this.scale = other.scale;
				this.fontAsset = other.fontAsset;
				this.material = other.material;
				this.spriteAsset = other.spriteAsset;
				this.fontStyle = other.fontStyle;
				this.textAlignment = other.textAlignment;
				this.overflowMode = other.overflowMode;
				this.wordWrap = other.wordWrap;
				this.wordWrappingRatio = other.wordWrappingRatio;
				this.color = other.color;
				this.fontColorGradient = other.fontColorGradient;
				this.tintSprites = other.tintSprites;
				this.overrideRichTextColors = other.overrideRichTextColors;
				this.fontSize = other.fontSize;
				this.autoSize = other.autoSize;
				this.fontSizeMin = other.fontSizeMin;
				this.fontSizeMax = other.fontSizeMax;
				this.enableKerning = other.enableKerning;
				this.richText = other.richText;
				this.isRightToLeft = other.isRightToLeft;
				this.extraPadding = other.extraPadding;
				this.parseControlCharacters = other.parseControlCharacters;
				this.characterSpacing = other.characterSpacing;
				this.wordSpacing = other.wordSpacing;
				this.lineSpacing = other.lineSpacing;
				this.paragraphSpacing = other.paragraphSpacing;
				this.lineSpacingMax = other.lineSpacingMax;
				this.maxVisibleCharacters = other.maxVisibleCharacters;
				this.maxVisibleWords = other.maxVisibleWords;
				this.maxVisibleLines = other.maxVisibleLines;
				this.firstVisibleCharacter = other.firstVisibleCharacter;
				this.useMaxVisibleDescender = other.useMaxVisibleDescender;
				this.fontWeight = other.fontWeight;
				this.pageToDisplay = other.pageToDisplay;
				this.horizontalMapping = other.horizontalMapping;
				this.verticalMapping = other.verticalMapping;
				this.uvLineOffset = other.uvLineOffset;
				this.geometrySortingOrder = other.geometrySortingOrder;
				this.inverseYAxis = other.inverseYAxis;
				this.charWidthMaxAdj = other.charWidthMaxAdj;
			}
		}

		// Token: 0x040001B7 RID: 439
		public string text = null;

		// Token: 0x040001B8 RID: 440
		public Rect screenRect;

		// Token: 0x040001B9 RID: 441
		public Vector4 margins;

		// Token: 0x040001BA RID: 442
		public float scale = 1f;

		// Token: 0x040001BB RID: 443
		public FontAsset fontAsset;

		// Token: 0x040001BC RID: 444
		public Material material;

		// Token: 0x040001BD RID: 445
		public TextSpriteAsset spriteAsset;

		// Token: 0x040001BE RID: 446
		public FontStyles fontStyle = FontStyles.Normal;

		// Token: 0x040001BF RID: 447
		public TextAlignment textAlignment = TextAlignment.TopLeft;

		// Token: 0x040001C0 RID: 448
		public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

		// Token: 0x040001C1 RID: 449
		public bool wordWrap = false;

		// Token: 0x040001C2 RID: 450
		public float wordWrappingRatio;

		// Token: 0x040001C3 RID: 451
		public Color color = Color.white;

		// Token: 0x040001C4 RID: 452
		public TextGradientPreset fontColorGradient;

		// Token: 0x040001C5 RID: 453
		public bool tintSprites;

		// Token: 0x040001C6 RID: 454
		public bool overrideRichTextColors;

		// Token: 0x040001C7 RID: 455
		public float fontSize = 18f;

		// Token: 0x040001C8 RID: 456
		public bool autoSize;

		// Token: 0x040001C9 RID: 457
		public float fontSizeMin;

		// Token: 0x040001CA RID: 458
		public float fontSizeMax;

		// Token: 0x040001CB RID: 459
		public bool enableKerning = true;

		// Token: 0x040001CC RID: 460
		public bool richText;

		// Token: 0x040001CD RID: 461
		public bool isRightToLeft;

		// Token: 0x040001CE RID: 462
		public bool extraPadding;

		// Token: 0x040001CF RID: 463
		public bool parseControlCharacters = true;

		// Token: 0x040001D0 RID: 464
		public float characterSpacing;

		// Token: 0x040001D1 RID: 465
		public float wordSpacing;

		// Token: 0x040001D2 RID: 466
		public float lineSpacing;

		// Token: 0x040001D3 RID: 467
		public float paragraphSpacing;

		// Token: 0x040001D4 RID: 468
		public float lineSpacingMax;

		// Token: 0x040001D5 RID: 469
		public int maxVisibleCharacters = 99999;

		// Token: 0x040001D6 RID: 470
		public int maxVisibleWords = 99999;

		// Token: 0x040001D7 RID: 471
		public int maxVisibleLines = 99999;

		// Token: 0x040001D8 RID: 472
		public int firstVisibleCharacter = 0;

		// Token: 0x040001D9 RID: 473
		public bool useMaxVisibleDescender;

		// Token: 0x040001DA RID: 474
		public FontWeight fontWeight = FontWeight.Regular;

		// Token: 0x040001DB RID: 475
		public int pageToDisplay = 1;

		// Token: 0x040001DC RID: 476
		public TextureMapping horizontalMapping = TextureMapping.Character;

		// Token: 0x040001DD RID: 477
		public TextureMapping verticalMapping = TextureMapping.Character;

		// Token: 0x040001DE RID: 478
		public float uvLineOffset;

		// Token: 0x040001DF RID: 479
		public VertexSortingOrder geometrySortingOrder = VertexSortingOrder.Normal;

		// Token: 0x040001E0 RID: 480
		public bool inverseYAxis;

		// Token: 0x040001E1 RID: 481
		public float charWidthMaxAdj;
	}
}
