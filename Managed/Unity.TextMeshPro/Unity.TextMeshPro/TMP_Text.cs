using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000053 RID: 83
	public abstract class TMP_Text : MaskableGraphic
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000124B9 File Offset: 0x000106B9
		// (set) Token: 0x0600033D RID: 829 RVA: 0x000124C4 File Offset: 0x000106C4
		public virtual string text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				if (this.m_text != null && value != null && this.m_text.Length == value.Length && this.m_text == value)
				{
					return;
				}
				this.m_text = value;
				this.m_inputSource = TMP_Text.TextInputSources.String;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001252D File Offset: 0x0001072D
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00012535 File Offset: 0x00010735
		public bool isRightToLeftText
		{
			get
			{
				return this.m_isRightToLeft;
			}
			set
			{
				if (this.m_isRightToLeft == value)
				{
					return;
				}
				this.m_isRightToLeft = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00012569 File Offset: 0x00010769
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00012571 File Offset: 0x00010771
		public TMP_FontAsset font
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				if (this.m_fontAsset == value)
				{
					return;
				}
				this.m_fontAsset = value;
				this.LoadFontAsset();
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000125B0 File Offset: 0x000107B0
		// (set) Token: 0x06000343 RID: 835 RVA: 0x000125B8 File Offset: 0x000107B8
		public virtual Material fontSharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				if (this.m_sharedMaterial == value)
				{
					return;
				}
				this.SetSharedMaterial(value);
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000125EA File Offset: 0x000107EA
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000125F2 File Offset: 0x000107F2
		public virtual Material[] fontSharedMaterials
		{
			get
			{
				return this.GetSharedMaterials();
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00012615 File Offset: 0x00010815
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00012624 File Offset: 0x00010824
		public Material fontMaterial
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00012680 File Offset: 0x00010880
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000125F2 File Offset: 0x000107F2
		public virtual Material[] fontMaterials
		{
			get
			{
				return this.GetMaterials(this.m_fontSharedMaterials);
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001268E File Offset: 0x0001088E
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00012696 File Offset: 0x00010896
		public override Color color
		{
			get
			{
				return this.m_fontColor;
			}
			set
			{
				if (this.m_fontColor == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_fontColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000126BB File Offset: 0x000108BB
		// (set) Token: 0x0600034D RID: 845 RVA: 0x000126C8 File Offset: 0x000108C8
		public float alpha
		{
			get
			{
				return this.m_fontColor.a;
			}
			set
			{
				if (this.m_fontColor.a == value)
				{
					return;
				}
				this.m_fontColor.a = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000126F2 File Offset: 0x000108F2
		// (set) Token: 0x0600034F RID: 847 RVA: 0x000126FA File Offset: 0x000108FA
		public bool enableVertexGradient
		{
			get
			{
				return this.m_enableVertexGradient;
			}
			set
			{
				if (this.m_enableVertexGradient == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableVertexGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001271A File Offset: 0x0001091A
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00012722 File Offset: 0x00010922
		public VertexGradient colorGradient
		{
			get
			{
				return this.m_fontColorGradient;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012738 File Offset: 0x00010938
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00012740 File Offset: 0x00010940
		public TMP_ColorGradient colorGradientPreset
		{
			get
			{
				return this.m_fontColorGradientPreset;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradientPreset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00012756 File Offset: 0x00010956
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0001275E File Offset: 0x0001095E
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00012788 File Offset: 0x00010988
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00012790 File Offset: 0x00010990
		public bool tintAllSprites
		{
			get
			{
				return this.m_tintAllSprites;
			}
			set
			{
				if (this.m_tintAllSprites == value)
				{
					return;
				}
				this.m_tintAllSprites = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000127B0 File Offset: 0x000109B0
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000127B8 File Offset: 0x000109B8
		public TMP_StyleSheet styleSheet
		{
			get
			{
				return this.m_StyleSheet;
			}
			set
			{
				this.m_StyleSheet = value;
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000127E2 File Offset: 0x000109E2
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00012820 File Offset: 0x00010A20
		public TMP_Style textStyle
		{
			get
			{
				this.m_TextStyle = this.GetStyle(this.m_TextStyleHashCode);
				if (this.m_TextStyle == null)
				{
					this.m_TextStyle = TMP_Style.NormalStyle;
					this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				}
				return this.m_TextStyle;
			}
			set
			{
				this.m_TextStyle = value;
				this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001285B File Offset: 0x00010A5B
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00012863 File Offset: 0x00010A63
		public bool overrideColorTags
		{
			get
			{
				return this.m_overrideHtmlColors;
			}
			set
			{
				if (this.m_overrideHtmlColors == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_overrideHtmlColors = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00012883 File Offset: 0x00010A83
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000128BB File Offset: 0x00010ABB
		public Color32 faceColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_faceColor;
				}
				this.m_faceColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_FaceColor);
				return this.m_faceColor;
			}
			set
			{
				if (this.m_faceColor.Compare(value))
				{
					return;
				}
				this.SetFaceColor(value);
				this.m_havePropertiesChanged = true;
				this.m_faceColor = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000128ED File Offset: 0x00010AED
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00012925 File Offset: 0x00010B25
		public Color32 outlineColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineColor;
				}
				this.m_outlineColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
				return this.m_outlineColor;
			}
			set
			{
				if (this.m_outlineColor.Compare(value))
				{
					return;
				}
				this.SetOutlineColor(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00012951 File Offset: 0x00010B51
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00012984 File Offset: 0x00010B84
		public float outlineWidth
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineWidth;
				}
				this.m_outlineWidth = this.m_sharedMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
				return this.m_outlineWidth;
			}
			set
			{
				if (this.m_outlineWidth == value)
				{
					return;
				}
				this.SetOutlineThickness(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineWidth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000129AB File Offset: 0x00010BAB
		// (set) Token: 0x06000365 RID: 869 RVA: 0x000129B4 File Offset: 0x00010BB4
		public float fontSize
		{
			get
			{
				return this.m_fontSize;
			}
			set
			{
				if (this.m_fontSize == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_fontSize = value;
				if (!this.m_enableAutoSizing)
				{
					this.m_fontSizeBase = this.m_fontSize;
				}
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00012A00 File Offset: 0x00010C00
		public float fontScale
		{
			get
			{
				return this.m_fontScale;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00012A08 File Offset: 0x00010C08
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00012A10 File Offset: 0x00010C10
		public FontWeight fontWeight
		{
			get
			{
				return this.m_fontWeight;
			}
			set
			{
				if (this.m_fontWeight == value)
				{
					return;
				}
				this.m_fontWeight = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00012A44 File Offset: 0x00010C44
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font)
				{
					return canvas.scaleFactor;
				}
				if (this.m_currentFontAsset == null || this.m_currentFontAsset.faceInfo.pointSize <= 0 || this.m_fontSize <= 0f)
				{
					return 1f;
				}
				return this.m_fontSize / (float)this.m_currentFontAsset.faceInfo.pointSize;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00012ACC File Offset: 0x00010CCC
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00012AD4 File Offset: 0x00010CD4
		public bool enableAutoSizing
		{
			get
			{
				return this.m_enableAutoSizing;
			}
			set
			{
				if (this.m_enableAutoSizing == value)
				{
					return;
				}
				this.m_enableAutoSizing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00012AF3 File Offset: 0x00010CF3
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00012AFB File Offset: 0x00010CFB
		public float fontSizeMin
		{
			get
			{
				return this.m_fontSizeMin;
			}
			set
			{
				if (this.m_fontSizeMin == value)
				{
					return;
				}
				this.m_fontSizeMin = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00012B1A File Offset: 0x00010D1A
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00012B22 File Offset: 0x00010D22
		public float fontSizeMax
		{
			get
			{
				return this.m_fontSizeMax;
			}
			set
			{
				if (this.m_fontSizeMax == value)
				{
					return;
				}
				this.m_fontSizeMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00012B41 File Offset: 0x00010D41
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00012B49 File Offset: 0x00010D49
		public FontStyles fontStyle
		{
			get
			{
				return this.m_fontStyle;
			}
			set
			{
				if (this.m_fontStyle == value)
				{
					return;
				}
				this.m_fontStyle = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00012B7D File Offset: 0x00010D7D
		public bool isUsingBold
		{
			get
			{
				return this.m_isUsingBold;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00012B85 File Offset: 0x00010D85
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00012B8D File Offset: 0x00010D8D
		public HorizontalAlignmentOptions horizontalAlignment
		{
			get
			{
				return this.m_HorizontalAlignment;
			}
			set
			{
				if (this.m_HorizontalAlignment == value)
				{
					return;
				}
				this.m_HorizontalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00012BAD File Offset: 0x00010DAD
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00012BB5 File Offset: 0x00010DB5
		public VerticalAlignmentOptions verticalAlignment
		{
			get
			{
				return this.m_VerticalAlignment;
			}
			set
			{
				if (this.m_VerticalAlignment == value)
				{
					return;
				}
				this.m_VerticalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00012BD5 File Offset: 0x00010DD5
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public TextAlignmentOptions alignment
		{
			get
			{
				return (TextAlignmentOptions)(this.m_HorizontalAlignment | (HorizontalAlignmentOptions)this.m_VerticalAlignment);
			}
			set
			{
				HorizontalAlignmentOptions horizontalAlignmentOptions = (HorizontalAlignmentOptions)(value & (TextAlignmentOptions)255);
				VerticalAlignmentOptions verticalAlignmentOptions = (VerticalAlignmentOptions)(value & (TextAlignmentOptions)65280);
				if (this.m_HorizontalAlignment == horizontalAlignmentOptions && this.m_VerticalAlignment == verticalAlignmentOptions)
				{
					return;
				}
				this.m_HorizontalAlignment = horizontalAlignmentOptions;
				this.m_VerticalAlignment = verticalAlignmentOptions;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00012C2F File Offset: 0x00010E2F
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00012C37 File Offset: 0x00010E37
		public float characterSpacing
		{
			get
			{
				return this.m_characterSpacing;
			}
			set
			{
				if (this.m_characterSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_characterSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00012C64 File Offset: 0x00010E64
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00012C6C File Offset: 0x00010E6C
		public float wordSpacing
		{
			get
			{
				return this.m_wordSpacing;
			}
			set
			{
				if (this.m_wordSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_wordSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00012C99 File Offset: 0x00010E99
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00012CA1 File Offset: 0x00010EA1
		public float lineSpacing
		{
			get
			{
				return this.m_lineSpacing;
			}
			set
			{
				if (this.m_lineSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00012CCE File Offset: 0x00010ECE
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00012CD6 File Offset: 0x00010ED6
		public float lineSpacingAdjustment
		{
			get
			{
				return this.m_lineSpacingMax;
			}
			set
			{
				if (this.m_lineSpacingMax == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_lineSpacingMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00012D03 File Offset: 0x00010F03
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00012D0B File Offset: 0x00010F0B
		public float paragraphSpacing
		{
			get
			{
				return this.m_paragraphSpacing;
			}
			set
			{
				if (this.m_paragraphSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_paragraphSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00012D38 File Offset: 0x00010F38
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00012D40 File Offset: 0x00010F40
		public float characterWidthAdjustment
		{
			get
			{
				return this.m_charWidthMaxAdj;
			}
			set
			{
				if (this.m_charWidthMaxAdj == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_charWidthMaxAdj = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00012D6D File Offset: 0x00010F6D
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00012D75 File Offset: 0x00010F75
		public bool enableWordWrapping
		{
			get
			{
				return this.m_enableWordWrapping;
			}
			set
			{
				if (this.m_enableWordWrapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.m_isCalculateSizeRequired = true;
				this.m_enableWordWrapping = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00012DA9 File Offset: 0x00010FA9
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00012DB1 File Offset: 0x00010FB1
		public float wordWrappingRatios
		{
			get
			{
				return this.m_wordWrappingRatios;
			}
			set
			{
				if (this.m_wordWrappingRatios == value)
				{
					return;
				}
				this.m_wordWrappingRatios = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00012DDE File Offset: 0x00010FDE
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00012DE6 File Offset: 0x00010FE6
		public TextOverflowModes overflowMode
		{
			get
			{
				return this.m_overflowMode;
			}
			set
			{
				if (this.m_overflowMode == value)
				{
					return;
				}
				this.m_overflowMode = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00012E13 File Offset: 0x00011013
		public bool isTextOverflowing
		{
			get
			{
				return this.m_firstOverflowCharacterIndex != -1;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00012E21 File Offset: 0x00011021
		public int firstOverflowCharacterIndex
		{
			get
			{
				return this.m_firstOverflowCharacterIndex;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00012E29 File Offset: 0x00011029
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00012E34 File Offset: 0x00011034
		public TMP_Text linkedTextComponent
		{
			get
			{
				return this.m_linkedTextComponent;
			}
			set
			{
				if (value == null)
				{
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
				}
				else
				{
					if (this.IsSelfOrLinkedAncestor(value))
					{
						return;
					}
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
					this.m_linkedTextComponent.parentLinkedComponent = this;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00012EA2 File Offset: 0x000110A2
		public bool isTextTruncated
		{
			get
			{
				return this.m_isTextTruncated;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00012EAA File Offset: 0x000110AA
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00012EB2 File Offset: 0x000110B2
		public bool enableKerning
		{
			get
			{
				return this.m_enableKerning;
			}
			set
			{
				if (this.m_enableKerning == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_enableKerning = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00012EDF File Offset: 0x000110DF
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00012EE7 File Offset: 0x000110E7
		public bool extraPadding
		{
			get
			{
				return this.m_enableExtraPadding;
			}
			set
			{
				if (this.m_enableExtraPadding == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableExtraPadding = value;
				this.UpdateMeshPadding();
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00012F0D File Offset: 0x0001110D
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00012F15 File Offset: 0x00011115
		public bool richText
		{
			get
			{
				return this.m_isRichText;
			}
			set
			{
				if (this.m_isRichText == value)
				{
					return;
				}
				this.m_isRichText = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00012F49 File Offset: 0x00011149
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00012F51 File Offset: 0x00011151
		public bool parseCtrlCharacters
		{
			get
			{
				return this.m_parseCtrlCharacters;
			}
			set
			{
				if (this.m_parseCtrlCharacters == value)
				{
					return;
				}
				this.m_parseCtrlCharacters = value;
				this.m_havePropertiesChanged = true;
				this.m_isCalculateSizeRequired = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00012F85 File Offset: 0x00011185
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00012F8D File Offset: 0x0001118D
		public bool isOverlay
		{
			get
			{
				return this.m_isOverlay;
			}
			set
			{
				if (this.m_isOverlay == value)
				{
					return;
				}
				this.m_isOverlay = value;
				this.SetShaderDepth();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00012FB3 File Offset: 0x000111B3
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00012FBB File Offset: 0x000111BB
		public bool isOrthographic
		{
			get
			{
				return this.m_isOrthographic;
			}
			set
			{
				if (this.m_isOrthographic == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isOrthographic = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00012FDB File Offset: 0x000111DB
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00012FE3 File Offset: 0x000111E3
		public bool enableCulling
		{
			get
			{
				return this.m_isCullingEnabled;
			}
			set
			{
				if (this.m_isCullingEnabled == value)
				{
					return;
				}
				this.m_isCullingEnabled = value;
				this.SetCulling();
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00013003 File Offset: 0x00011203
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0001300B File Offset: 0x0001120B
		public bool ignoreVisibility
		{
			get
			{
				return this.m_ignoreCulling;
			}
			set
			{
				if (this.m_ignoreCulling == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_ignoreCulling = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00013025 File Offset: 0x00011225
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001302D File Offset: 0x0001122D
		public TextureMappingOptions horizontalMapping
		{
			get
			{
				return this.m_horizontalMapping;
			}
			set
			{
				if (this.m_horizontalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_horizontalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001304D File Offset: 0x0001124D
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00013055 File Offset: 0x00011255
		public TextureMappingOptions verticalMapping
		{
			get
			{
				return this.m_verticalMapping;
			}
			set
			{
				if (this.m_verticalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_verticalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00013075 File Offset: 0x00011275
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001307D File Offset: 0x0001127D
		public float mappingUvLineOffset
		{
			get
			{
				return this.m_uvLineOffset;
			}
			set
			{
				if (this.m_uvLineOffset == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_uvLineOffset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001309D File Offset: 0x0001129D
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x000130A5 File Offset: 0x000112A5
		public TextRenderFlags renderMode
		{
			get
			{
				return this.m_renderMode;
			}
			set
			{
				if (this.m_renderMode == value)
				{
					return;
				}
				this.m_renderMode = value;
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000130BF File Offset: 0x000112BF
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x000130C7 File Offset: 0x000112C7
		public VertexSortingOrder geometrySortingOrder
		{
			get
			{
				return this.m_geometrySortingOrder;
			}
			set
			{
				this.m_geometrySortingOrder = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000130DD File Offset: 0x000112DD
		// (set) Token: 0x060003AB RID: 939 RVA: 0x000130E5 File Offset: 0x000112E5
		public bool isTextObjectScaleStatic
		{
			get
			{
				return this.m_IsTextObjectScaleStatic;
			}
			set
			{
				this.m_IsTextObjectScaleStatic = value;
				if (this.m_IsTextObjectScaleStatic)
				{
					TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
					return;
				}
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00013103 File Offset: 0x00011303
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0001310B File Offset: 0x0001130B
		public bool vertexBufferAutoSizeReduction
		{
			get
			{
				return this.m_VertexBufferAutoSizeReduction;
			}
			set
			{
				this.m_VertexBufferAutoSizeReduction = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00013121 File Offset: 0x00011321
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00013129 File Offset: 0x00011329
		public int firstVisibleCharacter
		{
			get
			{
				return this.m_firstVisibleCharacter;
			}
			set
			{
				if (this.m_firstVisibleCharacter == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_firstVisibleCharacter = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00013149 File Offset: 0x00011349
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00013151 File Offset: 0x00011351
		public int maxVisibleCharacters
		{
			get
			{
				return this.m_maxVisibleCharacters;
			}
			set
			{
				if (this.m_maxVisibleCharacters == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleCharacters = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00013171 File Offset: 0x00011371
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00013179 File Offset: 0x00011379
		public int maxVisibleWords
		{
			get
			{
				return this.m_maxVisibleWords;
			}
			set
			{
				if (this.m_maxVisibleWords == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleWords = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00013199 File Offset: 0x00011399
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000131A1 File Offset: 0x000113A1
		public int maxVisibleLines
		{
			get
			{
				return this.m_maxVisibleLines;
			}
			set
			{
				if (this.m_maxVisibleLines == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.m_maxVisibleLines = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000131C8 File Offset: 0x000113C8
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000131D0 File Offset: 0x000113D0
		public bool useMaxVisibleDescender
		{
			get
			{
				return this.m_useMaxVisibleDescender;
			}
			set
			{
				if (this.m_useMaxVisibleDescender == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000131F0 File Offset: 0x000113F0
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000131F8 File Offset: 0x000113F8
		public int pageToDisplay
		{
			get
			{
				return this.m_pageToDisplay;
			}
			set
			{
				if (this.m_pageToDisplay == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_pageToDisplay = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00013218 File Offset: 0x00011418
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00013220 File Offset: 0x00011420
		public virtual Vector4 margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				if (this.m_margin == value)
				{
					return;
				}
				this.m_margin = value;
				this.ComputeMarginSize();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001324B File Offset: 0x0001144B
		public TMP_TextInfo textInfo
		{
			get
			{
				return this.m_textInfo;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00013253 File Offset: 0x00011453
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0001325B File Offset: 0x0001145B
		public bool havePropertiesChanged
		{
			get
			{
				return this.m_havePropertiesChanged;
			}
			set
			{
				if (this.m_havePropertiesChanged == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.m_isInputParsingRequired = true;
				this.SetAllDirty();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001327B File Offset: 0x0001147B
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00013283 File Offset: 0x00011483
		public bool isUsingLegacyAnimationComponent
		{
			get
			{
				return this.m_isUsingLegacyAnimationComponent;
			}
			set
			{
				this.m_isUsingLegacyAnimationComponent = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001328C File Offset: 0x0001148C
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000132AE File Offset: 0x000114AE
		public new RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000132D0 File Offset: 0x000114D0
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x000132D8 File Offset: 0x000114D8
		public virtual bool autoSizeTextContainer { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000132E1 File Offset: 0x000114E1
		public virtual Mesh mesh
		{
			get
			{
				return this.m_mesh;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000132E9 File Offset: 0x000114E9
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000132F1 File Offset: 0x000114F1
		public bool isVolumetricText
		{
			get
			{
				return this.m_isVolumetricText;
			}
			set
			{
				if (this.m_isVolumetricText == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.m_textInfo.ResetVertexLayout(value);
				this.m_isInputParsingRequired = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013324 File Offset: 0x00011524
		public Bounds bounds
		{
			get
			{
				if (this.m_mesh == null)
				{
					return default(Bounds);
				}
				return this.GetCompoundBounds();
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00013350 File Offset: 0x00011550
		public Bounds textBounds
		{
			get
			{
				if (this.m_textInfo == null)
				{
					return default(Bounds);
				}
				return this.GetTextBounds();
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003CA RID: 970 RVA: 0x00013378 File Offset: 0x00011578
		// (remove) Token: 0x060003CB RID: 971 RVA: 0x000133AC File Offset: 0x000115AC
		public static event Func<int, string, TMP_FontAsset> onFontAssetRequest;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003CC RID: 972 RVA: 0x000133E0 File Offset: 0x000115E0
		// (remove) Token: 0x060003CD RID: 973 RVA: 0x00013414 File Offset: 0x00011614
		public static event Func<int, string, TMP_SpriteAsset> onSpriteAssetRequest;

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00013448 File Offset: 0x00011648
		protected TMP_SpriteAnimator spriteAnimator
		{
			get
			{
				if (this.m_spriteAnimator == null)
				{
					this.m_spriteAnimator = base.GetComponent<TMP_SpriteAnimator>();
					if (this.m_spriteAnimator == null)
					{
						this.m_spriteAnimator = base.gameObject.AddComponent<TMP_SpriteAnimator>();
					}
				}
				return this.m_spriteAnimator;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00013494 File Offset: 0x00011694
		public float flexibleHeight
		{
			get
			{
				return this.m_flexibleHeight;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001349C File Offset: 0x0001169C
		public float flexibleWidth
		{
			get
			{
				return this.m_flexibleWidth;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000134A4 File Offset: 0x000116A4
		public float minWidth
		{
			get
			{
				return this.m_minWidth;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000134AC File Offset: 0x000116AC
		public float minHeight
		{
			get
			{
				return this.m_minHeight;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000134B4 File Offset: 0x000116B4
		public float maxWidth
		{
			get
			{
				return this.m_maxWidth;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000134BC File Offset: 0x000116BC
		public float maxHeight
		{
			get
			{
				return this.m_maxHeight;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000134C4 File Offset: 0x000116C4
		protected LayoutElement layoutElement
		{
			get
			{
				if (this.m_LayoutElement == null)
				{
					this.m_LayoutElement = base.GetComponent<LayoutElement>();
				}
				return this.m_LayoutElement;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000134E6 File Offset: 0x000116E6
		public virtual float preferredWidth
		{
			get
			{
				if (!this.m_isPreferredWidthDirty)
				{
					return this.m_preferredWidth;
				}
				this.m_preferredWidth = this.GetPreferredWidth();
				return this.m_preferredWidth;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00013509 File Offset: 0x00011709
		public virtual float preferredHeight
		{
			get
			{
				if (!this.m_isPreferredHeightDirty)
				{
					return this.m_preferredHeight;
				}
				this.m_preferredHeight = this.GetPreferredHeight();
				return this.m_preferredHeight;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001352C File Offset: 0x0001172C
		public virtual float renderedWidth
		{
			get
			{
				return this.GetRenderedWidth();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00013534 File Offset: 0x00011734
		public virtual float renderedHeight
		{
			get
			{
				return this.GetRenderedHeight();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001353C File Offset: 0x0001173C
		public int layoutPriority
		{
			get
			{
				return this.m_layoutPriority;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void LoadFontAsset()
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetSharedMaterial(Material mat)
		{
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013544 File Offset: 0x00011744
		protected virtual Material GetMaterial(Material mat)
		{
			return null;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetFontBaseMaterial(Material mat)
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00013544 File Offset: 0x00011744
		protected virtual Material[] GetSharedMaterials()
		{
			return null;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetSharedMaterials(Material[] materials)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013544 File Offset: 0x00011744
		protected virtual Material[] GetMaterials(Material[] mats)
		{
			return null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00011D3F File Offset: 0x0000FF3F
		protected virtual Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013548 File Offset: 0x00011748
		protected void SetVertexColorGradient(TMP_ColorGradient gradient)
		{
			if (gradient == null)
			{
				return;
			}
			this.m_fontColorGradient.bottomLeft = gradient.bottomLeft;
			this.m_fontColorGradient.bottomRight = gradient.bottomRight;
			this.m_fontColorGradient.topLeft = gradient.topLeft;
			this.m_fontColorGradient.topRight = gradient.topRight;
			this.SetVerticesDirty();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000027BA File Offset: 0x000009BA
		protected void SetTextSortingOrder(VertexSortingOrder order)
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000027BA File Offset: 0x000009BA
		protected void SetTextSortingOrder(int[] order)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetFaceColor(Color32 color)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetOutlineColor(Color32 color)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetOutlineThickness(float thickness)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetShaderDepth()
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetCulling()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000135AC File Offset: 0x000117AC
		protected virtual float GetPaddingForMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_sharedMaterial == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001361C File Offset: 0x0001181C
		protected virtual float GetPaddingForMaterial(Material mat)
		{
			if (mat == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(mat, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013544 File Offset: 0x00011744
		protected virtual Vector3[] GetTextContainerLocalCorners()
		{
			return null;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013678 File Offset: 0x00011878
		internal void SetTextInternal(string text)
		{
			this.m_text = text;
			this.m_renderMode = TextRenderFlags.DontRender;
			this.m_isInputParsingRequired = true;
			this.ForceMeshUpdate(false, false);
			this.m_renderMode = TextRenderFlags.Render;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void UpdateGeometry(Mesh mesh, int index)
		{
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void UpdateVertexData()
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void SetVertices(Vector3[] vertices)
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void UpdateMeshPadding()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000136A2 File Offset: 0x000118A2
		public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
			this.InternalCrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000136BA File Offset: 0x000118BA
		public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
			this.InternalCrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void InternalCrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void InternalCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000136D0 File Offset: 0x000118D0
		protected void ParseInputText()
		{
			this.m_isInputParsingRequired = false;
			switch (this.m_inputSource)
			{
			case TMP_Text.TextInputSources.Text:
			case TMP_Text.TextInputSources.String:
				this.StringToCharArray(this.m_text, ref this.m_TextParsingBuffer);
				break;
			case TMP_Text.TextInputSources.SetText:
				this.SetTextArrayToCharArray(this.m_input_CharArray, ref this.m_TextParsingBuffer);
				break;
			}
			this.SetArraySizes(this.m_TextParsingBuffer);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013736 File Offset: 0x00011936
		public void SetText(string text, bool syncTextInputBox = true)
		{
			this.text = text;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001373F File Offset: 0x0001193F
		public void SetText(string text, float arg0)
		{
			this.SetText(text, arg0, 255f, 255f);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013753 File Offset: 0x00011953
		public void SetText(string text, float arg0, float arg1)
		{
			this.SetText(text, arg0, arg1, 255f);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013764 File Offset: 0x00011964
		public void SetText(string text, float arg0, float arg1, float arg2)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c == '{')
				{
					if (text[i + 2] == ':')
					{
						num = (int)(text[i + 3] - '0');
					}
					switch (text[i + 1])
					{
					case '0':
						this.AddFloatToCharArray((double)arg0, ref num2, num);
						break;
					case '1':
						this.AddFloatToCharArray((double)arg1, ref num2, num);
						break;
					case '2':
						this.AddFloatToCharArray((double)arg2, ref num2, num);
						break;
					}
					if (text[i + 2] == ':')
					{
						i += 4;
					}
					else
					{
						i += 2;
					}
				}
				else
				{
					this.m_input_CharArray[num2] = c;
					num2++;
				}
			}
			this.m_input_CharArray[num2] = '\0';
			this.m_charArray_Length = num2;
			this.m_inputSource = TMP_Text.TextInputSources.SetText;
			this.m_isInputParsingRequired = true;
			this.m_havePropertiesChanged = true;
			this.m_isCalculateSizeRequired = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001385A File Offset: 0x00011A5A
		public void SetText(StringBuilder text)
		{
			this.m_inputSource = TMP_Text.TextInputSources.SetCharArray;
			this.StringBuilderToIntArray(text, ref this.m_TextParsingBuffer);
			this.m_isInputParsingRequired = true;
			this.m_havePropertiesChanged = true;
			this.m_isCalculateSizeRequired = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013894 File Offset: 0x00011A94
		public void SetCharArray(char[] sourceText)
		{
			if (this.m_TextParsingBuffer == null)
			{
				this.m_TextParsingBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref this.m_TextParsingBuffer, ref num);
			}
			int num2 = 0;
			while (sourceText != null && num2 < sourceText.Length)
			{
				if (sourceText[num2] != '\\' || num2 >= sourceText.Length - 1)
				{
					goto IL_018F;
				}
				int num3 = (int)sourceText[num2 + 1];
				if (num3 != 110)
				{
					switch (num3)
					{
					case 114:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 13;
						num2++;
						num++;
						break;
					case 115:
					case 117:
						goto IL_018F;
					case 116:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 9;
						num2++;
						num++;
						break;
					case 118:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 11;
						num2++;
						num++;
						break;
					default:
						goto IL_018F;
					}
				}
				else
				{
					if (num == this.m_TextParsingBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
					}
					this.m_TextParsingBuffer[num].unicode = 10;
					num2++;
					num++;
				}
				IL_02C7:
				num2++;
				continue;
				IL_018F:
				if (sourceText[num2] == '<')
				{
					if (this.IsTagName(ref sourceText, "<BR>", num2))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 10;
						num++;
						num2 += 3;
						goto IL_02C7;
					}
					if (this.IsTagName(ref sourceText, "<NBSP>", num2))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 160;
						num++;
						num2 += 5;
						goto IL_02C7;
					}
					if (this.IsTagName(ref sourceText, "<STYLE=", num2))
					{
						this.m_TextStyleStackDepth++;
						int num4;
						if (this.ReplaceOpeningStyleTag(ref sourceText, num2, out num4, ref this.m_TextParsingBuffer, ref num))
						{
							num2 = num4;
							goto IL_02C7;
						}
					}
					else if (this.IsTagName(ref sourceText, "</STYLE>", num2))
					{
						this.m_TextStyleStackDepth++;
						this.ReplaceClosingStyleTag(ref sourceText, num2, ref this.m_TextParsingBuffer, ref num);
						num2 += 7;
						goto IL_02C7;
					}
				}
				if (num == this.m_TextParsingBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
				}
				this.m_TextParsingBuffer[num].unicode = (int)sourceText[num2];
				num++;
				goto IL_02C7;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextParsingBuffer, ref num);
			}
			if (num == this.m_TextParsingBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
			}
			this.m_TextParsingBuffer[num].unicode = 0;
			this.m_inputSource = TMP_Text.TextInputSources.SetCharArray;
			this.m_isInputParsingRequired = true;
			this.m_havePropertiesChanged = true;
			this.m_isCalculateSizeRequired = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public void SetCharArray(char[] sourceText, int start, int length)
		{
			if (this.m_TextParsingBuffer == null)
			{
				this.m_TextParsingBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref this.m_TextParsingBuffer, ref num);
			}
			int j = start;
			int num2 = start + length;
			while (j < num2)
			{
				if (sourceText[j] != '\\' || j >= length - 1)
				{
					goto IL_0194;
				}
				int num3 = (int)sourceText[j + 1];
				if (num3 != 110)
				{
					switch (num3)
					{
					case 114:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 13;
						j++;
						num++;
						break;
					case 115:
					case 117:
						goto IL_0194;
					case 116:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 9;
						j++;
						num++;
						break;
					case 118:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 11;
						j++;
						num++;
						break;
					default:
						goto IL_0194;
					}
				}
				else
				{
					if (num == this.m_TextParsingBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
					}
					this.m_TextParsingBuffer[num].unicode = 10;
					j++;
					num++;
				}
				IL_02CC:
				j++;
				continue;
				IL_0194:
				if (sourceText[j] == '<')
				{
					if (this.IsTagName(ref sourceText, "<BR>", j))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 10;
						num++;
						j += 3;
						goto IL_02CC;
					}
					if (this.IsTagName(ref sourceText, "<NBSP>", j))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 160;
						num++;
						j += 5;
						goto IL_02CC;
					}
					if (this.IsTagName(ref sourceText, "<STYLE=", j))
					{
						this.m_TextStyleStackDepth++;
						int num4;
						if (this.ReplaceOpeningStyleTag(ref sourceText, j, out num4, ref this.m_TextParsingBuffer, ref num))
						{
							j = num4;
							goto IL_02CC;
						}
					}
					else if (this.IsTagName(ref sourceText, "</STYLE>", j))
					{
						this.m_TextStyleStackDepth++;
						this.ReplaceClosingStyleTag(ref sourceText, j, ref this.m_TextParsingBuffer, ref num);
						j += 7;
						goto IL_02CC;
					}
				}
				if (num == this.m_TextParsingBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
				}
				this.m_TextParsingBuffer[num].unicode = (int)sourceText[j];
				num++;
				goto IL_02CC;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextParsingBuffer, ref num);
			}
			if (num == this.m_TextParsingBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
			}
			this.m_TextParsingBuffer[num].unicode = 0;
			this.m_inputSource = TMP_Text.TextInputSources.SetCharArray;
			this.m_havePropertiesChanged = true;
			this.m_isInputParsingRequired = true;
			this.m_isCalculateSizeRequired = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013F54 File Offset: 0x00012154
		public void SetCharArray(int[] sourceText, int start, int length)
		{
			if (this.m_TextParsingBuffer == null)
			{
				this.m_TextParsingBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref this.m_TextParsingBuffer, ref num);
			}
			int num2 = start + length;
			int num3 = start;
			while (num3 < num2 && num3 < sourceText.Length)
			{
				if (sourceText[num3] != 92 || num3 >= length - 1)
				{
					goto IL_0194;
				}
				int num4 = sourceText[num3 + 1];
				if (num4 != 110)
				{
					switch (num4)
					{
					case 114:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 13;
						num3++;
						num++;
						break;
					case 115:
					case 117:
						goto IL_0194;
					case 116:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 9;
						num3++;
						num++;
						break;
					case 118:
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 11;
						num3++;
						num++;
						break;
					default:
						goto IL_0194;
					}
				}
				else
				{
					if (num == this.m_TextParsingBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
					}
					this.m_TextParsingBuffer[num].unicode = 10;
					num3++;
					num++;
				}
				IL_02CC:
				num3++;
				continue;
				IL_0194:
				if (sourceText[num3] == 60)
				{
					if (this.IsTagName(ref sourceText, "<BR>", num3))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 10;
						num++;
						num3 += 3;
						goto IL_02CC;
					}
					if (this.IsTagName(ref sourceText, "<NBSP>", num3))
					{
						if (num == this.m_TextParsingBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
						}
						this.m_TextParsingBuffer[num].unicode = 160;
						num++;
						num3 += 5;
						goto IL_02CC;
					}
					if (this.IsTagName(ref sourceText, "<STYLE=", num3))
					{
						this.m_TextStyleStackDepth++;
						int num5;
						if (this.ReplaceOpeningStyleTag(ref sourceText, num3, out num5, ref this.m_TextParsingBuffer, ref num))
						{
							num3 = num5;
							goto IL_02CC;
						}
					}
					else if (this.IsTagName(ref sourceText, "</STYLE>", num3))
					{
						this.m_TextStyleStackDepth++;
						this.ReplaceClosingStyleTag(ref sourceText, num3, ref this.m_TextParsingBuffer, ref num);
						num3 += 7;
						goto IL_02CC;
					}
				}
				if (num == this.m_TextParsingBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
				}
				this.m_TextParsingBuffer[num].unicode = sourceText[num3];
				num++;
				goto IL_02CC;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextParsingBuffer, ref num);
			}
			if (num == this.m_TextParsingBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref this.m_TextParsingBuffer);
			}
			this.m_TextParsingBuffer[num].unicode = 0;
			this.m_inputSource = TMP_Text.TextInputSources.SetCharArray;
			this.m_havePropertiesChanged = true;
			this.m_isInputParsingRequired = true;
			this.m_isCalculateSizeRequired = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000142B8 File Offset: 0x000124B8
		protected void SetTextArrayToCharArray(char[] sourceText, ref TMP_Text.UnicodeChar[] charBuffer)
		{
			if (sourceText == null || this.m_charArray_Length == 0)
			{
				return;
			}
			if (charBuffer == null)
			{
				charBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref this.m_TextParsingBuffer, ref num);
			}
			for (int j = 0; j < this.m_charArray_Length; j++)
			{
				if (char.IsHighSurrogate(sourceText[j]) && char.IsLowSurrogate(sourceText[j + 1]))
				{
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = char.ConvertToUtf32(sourceText[j], sourceText[j + 1]);
					j++;
					num++;
				}
				else
				{
					if (sourceText[j] == '<')
					{
						if (this.IsTagName(ref sourceText, "<BR>", j))
						{
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 10;
							num++;
							j += 3;
							goto IL_01C6;
						}
						if (this.IsTagName(ref sourceText, "<NBSP>", j))
						{
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 160;
							num++;
							j += 5;
							goto IL_01C6;
						}
						if (this.IsTagName(ref sourceText, "<STYLE=", j))
						{
							this.m_TextStyleStackDepth++;
							int num2;
							if (this.ReplaceOpeningStyleTag(ref sourceText, j, out num2, ref charBuffer, ref num))
							{
								j = num2;
								goto IL_01C6;
							}
						}
						else if (this.IsTagName(ref sourceText, "</STYLE>", j))
						{
							this.m_TextStyleStackDepth++;
							this.ReplaceClosingStyleTag(ref sourceText, j, ref charBuffer, ref num);
							j += 7;
							goto IL_01C6;
						}
					}
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = (int)sourceText[j];
					num++;
				}
				IL_01C6:;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextParsingBuffer, ref num);
			}
			if (num == charBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
			}
			charBuffer[num].unicode = 0;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000144E0 File Offset: 0x000126E0
		protected void StringToCharArray(string sourceText, ref TMP_Text.UnicodeChar[] charBuffer)
		{
			if (sourceText == null)
			{
				charBuffer[0].unicode = 0;
				return;
			}
			if (charBuffer == null)
			{
				charBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref charBuffer, ref num);
			}
			int j = 0;
			while (j < sourceText.Length)
			{
				if (this.m_inputSource != TMP_Text.TextInputSources.Text || sourceText[j] != '\\' || sourceText.Length <= j + 1)
				{
					goto IL_0346;
				}
				int num2 = (int)sourceText[j + 1];
				if (num2 != 85)
				{
					if (num2 != 92)
					{
						switch (num2)
						{
						case 110:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0346;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 10;
							charBuffer[num].stringIndex = j;
							charBuffer[num].length = 1;
							j++;
							num++;
							break;
						case 111:
						case 112:
						case 113:
						case 115:
							goto IL_0346;
						case 114:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0346;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 13;
							charBuffer[num].stringIndex = j;
							charBuffer[num].length = 1;
							j++;
							num++;
							break;
						case 116:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0346;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 9;
							charBuffer[num].stringIndex = j;
							charBuffer[num].length = 1;
							j++;
							num++;
							break;
						case 117:
							if (sourceText.Length <= j + 5)
							{
								goto IL_0346;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = this.GetUTF16(sourceText, j + 2);
							charBuffer[num].stringIndex = j;
							charBuffer[num].length = 6;
							j += 5;
							num++;
							break;
						case 118:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0346;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 11;
							charBuffer[num].stringIndex = j;
							charBuffer[num].length = 1;
							j++;
							num++;
							break;
						default:
							goto IL_0346;
						}
					}
					else
					{
						if (!this.m_parseCtrlCharacters || sourceText.Length <= j + 2)
						{
							goto IL_0346;
						}
						if (num + 2 > charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = (int)sourceText[j + 1];
						charBuffer[num + 1].unicode = (int)sourceText[j + 2];
						j += 2;
						num += 2;
					}
				}
				else
				{
					if (sourceText.Length <= j + 9)
					{
						goto IL_0346;
					}
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = this.GetUTF32(sourceText, j + 2);
					charBuffer[num].stringIndex = j;
					charBuffer[num].length = 10;
					j += 9;
					num++;
				}
				IL_052B:
				j++;
				continue;
				IL_0346:
				if (char.IsHighSurrogate(sourceText[j]) && char.IsLowSurrogate(sourceText[j + 1]))
				{
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = char.ConvertToUtf32(sourceText[j], sourceText[j + 1]);
					charBuffer[num].stringIndex = j;
					charBuffer[num].length = 2;
					j++;
					num++;
					goto IL_052B;
				}
				if (sourceText[j] == '<' && this.m_isRichText)
				{
					if (this.IsTagName(ref sourceText, "<BR>", j))
					{
						if (num == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = 10;
						charBuffer[num].stringIndex = j;
						charBuffer[num].length = 1;
						num++;
						j += 3;
						goto IL_052B;
					}
					if (this.IsTagName(ref sourceText, "<NBSP>", j))
					{
						if (num == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = 160;
						charBuffer[num].stringIndex = j;
						charBuffer[num].length = 1;
						num++;
						j += 5;
						goto IL_052B;
					}
					if (this.IsTagName(ref sourceText, "<STYLE=", j))
					{
						this.m_TextStyleStackDepth++;
						int num3;
						if (this.ReplaceOpeningStyleTag(ref sourceText, j, out num3, ref charBuffer, ref num))
						{
							j = num3;
							goto IL_052B;
						}
					}
					else if (this.IsTagName(ref sourceText, "</STYLE>", j))
					{
						this.m_TextStyleStackDepth++;
						this.ReplaceClosingStyleTag(ref sourceText, j, ref charBuffer, ref num);
						j += 7;
						goto IL_052B;
					}
				}
				if (num == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[num].unicode = (int)sourceText[j];
				charBuffer[num].stringIndex = num;
				charBuffer[num].length = 1;
				num++;
				goto IL_052B;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref charBuffer, ref num);
			}
			if (num == charBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
			}
			charBuffer[num].unicode = 0;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014A68 File Offset: 0x00012C68
		protected void StringBuilderToIntArray(StringBuilder sourceText, ref TMP_Text.UnicodeChar[] charBuffer)
		{
			if (sourceText == null)
			{
				charBuffer[0].unicode = 0;
				return;
			}
			if (charBuffer == null)
			{
				charBuffer = new TMP_Text.UnicodeChar[8];
			}
			for (int i = 0; i < this.m_TextStyleStacks.Length; i++)
			{
				this.m_TextStyleStacks[i].SetDefault(0);
			}
			this.m_TextStyleStackDepth = 0;
			int num = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, 0, ref charBuffer, ref num);
			}
			int j = 0;
			while (j < sourceText.Length)
			{
				if (!this.m_parseCtrlCharacters || sourceText[j] != '\\' || sourceText.Length <= j + 1)
				{
					goto IL_0263;
				}
				int num2 = (int)sourceText[j + 1];
				if (num2 != 85)
				{
					if (num2 != 92)
					{
						switch (num2)
						{
						case 110:
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 10;
							j++;
							num++;
							break;
						case 111:
						case 112:
						case 113:
						case 115:
							goto IL_0263;
						case 114:
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 13;
							j++;
							num++;
							break;
						case 116:
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 9;
							j++;
							num++;
							break;
						case 117:
							if (sourceText.Length <= j + 5)
							{
								goto IL_0263;
							}
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = this.GetUTF16(sourceText, j + 2);
							j += 5;
							num++;
							break;
						case 118:
							if (num == charBuffer.Length)
							{
								this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
							}
							charBuffer[num].unicode = 11;
							j++;
							num++;
							break;
						default:
							goto IL_0263;
						}
					}
					else
					{
						if (sourceText.Length <= j + 2)
						{
							goto IL_0263;
						}
						if (num + 2 > charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = (int)sourceText[j + 1];
						charBuffer[num + 1].unicode = (int)sourceText[j + 2];
						j += 2;
						num += 2;
					}
				}
				else
				{
					if (sourceText.Length <= j + 9)
					{
						goto IL_0263;
					}
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = this.GetUTF32(sourceText, j + 2);
					j += 9;
					num++;
				}
				IL_03CD:
				j++;
				continue;
				IL_0263:
				if (char.IsHighSurrogate(sourceText[j]) && char.IsLowSurrogate(sourceText[j + 1]))
				{
					if (num == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[num].unicode = char.ConvertToUtf32(sourceText[j], sourceText[j + 1]);
					j++;
					num++;
					goto IL_03CD;
				}
				if (sourceText[j] == '<')
				{
					if (this.IsTagName(ref sourceText, "<BR>", j))
					{
						if (num == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = 10;
						num++;
						j += 3;
						goto IL_03CD;
					}
					if (this.IsTagName(ref sourceText, "<NBSP>", j))
					{
						if (num == charBuffer.Length)
						{
							this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
						}
						charBuffer[num].unicode = 160;
						num++;
						j += 5;
						goto IL_03CD;
					}
					if (this.IsTagName(ref sourceText, "<STYLE=", j))
					{
						this.m_TextStyleStackDepth++;
						int num3;
						if (this.ReplaceOpeningStyleTag(ref sourceText, j, out num3, ref charBuffer, ref num))
						{
							j = num3;
							goto IL_03CD;
						}
					}
					else if (this.IsTagName(ref sourceText, "</STYLE>", j))
					{
						this.m_TextStyleStackDepth++;
						this.ReplaceClosingStyleTag(ref sourceText, j, ref charBuffer, ref num);
						j += 7;
						goto IL_03CD;
					}
				}
				if (num == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[num].unicode = (int)sourceText[j];
				num++;
				goto IL_03CD;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref charBuffer, ref num);
			}
			if (num == charBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
			}
			charBuffer[num].unicode = 0;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00014E94 File Offset: 0x00013094
		private bool ReplaceOpeningStyleTag(ref string sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int tagHashCode = this.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(tagHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_01FD;
				}
				if (this.IsTagName(ref styleOpeningTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num4;
					if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
					{
						goto IL_01FD;
					}
					i = num4;
				}
				else
				{
					if (!this.IsTagName(ref styleOpeningTagArray, "</STYLE>", i))
					{
						goto IL_01FD;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0229:
				i++;
				continue;
				IL_01FD:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_0229;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000150E8 File Offset: 0x000132E8
		private bool ReplaceOpeningStyleTag(ref int[] sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int tagHashCode = this.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(tagHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_01FD;
				}
				if (this.IsTagName(ref styleOpeningTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num4;
					if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
					{
						goto IL_01FD;
					}
					i = num4;
				}
				else
				{
					if (!this.IsTagName(ref styleOpeningTagArray, "</STYLE>", i))
					{
						goto IL_01FD;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0229:
				i++;
				continue;
				IL_01FD:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_0229;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001533C File Offset: 0x0001353C
		private bool ReplaceOpeningStyleTag(ref char[] sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int tagHashCode = this.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(tagHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_01FD;
				}
				if (this.IsTagName(ref styleOpeningTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num4;
					if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
					{
						goto IL_01FD;
					}
					i = num4;
				}
				else
				{
					if (!this.IsTagName(ref styleOpeningTagArray, "</STYLE>", i))
					{
						goto IL_01FD;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0229:
				i++;
				continue;
				IL_01FD:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_0229;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00015590 File Offset: 0x00013790
		private bool ReplaceOpeningStyleTag(ref StringBuilder sourceText, int srcIndex, out int srcOffset, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int tagHashCode = this.GetTagHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(tagHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_01FD;
				}
				if (this.IsTagName(ref styleOpeningTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num4;
					if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
					{
						goto IL_01FD;
					}
					i = num4;
				}
				else
				{
					if (!this.IsTagName(ref styleOpeningTagArray, "</STYLE>", i))
					{
						goto IL_01FD;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0229:
				i++;
				continue;
				IL_01FD:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_0229;
			}
			return true;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000157D8 File Offset: 0x000139D8
		private bool ReplaceClosingStyleTag(ref string sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int num = this.m_TextStyleStacks[this.m_TextStyleStackDepth].Pop();
			TMP_Style style = this.GetStyle(num);
			if (style == null)
			{
				return false;
			}
			int num2 = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num2)
			{
				int num3 = styleClosingTagArray[i];
				if (num3 == 92 && i + 1 < num2)
				{
					int num4 = styleClosingTagArray[i + 1];
					if (num4 <= 92)
					{
						if (num4 != 85)
						{
							if (num4 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num2)
						{
							num3 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num4 != 110)
					{
						switch (num4)
						{
						case 117:
							if (i + 5 < num2)
							{
								num3 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num3 = 10;
						i++;
					}
				}
				if (num3 != 60)
				{
					goto IL_01E0;
				}
				if (this.IsTagName(ref styleClosingTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num5;
					if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num5, ref charBuffer, ref writeIndex))
					{
						goto IL_01E0;
					}
					i = num5;
				}
				else
				{
					if (!this.IsTagName(ref styleClosingTagArray, "</STYLE>", i))
					{
						goto IL_01E0;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0209:
				i++;
				continue;
				IL_01E0:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num3;
				writeIndex++;
				goto IL_0209;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015A0C File Offset: 0x00013C0C
		private bool ReplaceClosingStyleTag(ref int[] sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int num = this.m_TextStyleStacks[this.m_TextStyleStackDepth].Pop();
			TMP_Style style = this.GetStyle(num);
			if (style == null)
			{
				return false;
			}
			int num2 = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num2)
			{
				int num3 = styleClosingTagArray[i];
				if (num3 == 92 && i + 1 < num2)
				{
					int num4 = styleClosingTagArray[i + 1];
					if (num4 <= 92)
					{
						if (num4 != 85)
						{
							if (num4 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num2)
						{
							num3 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num4 != 110)
					{
						switch (num4)
						{
						case 117:
							if (i + 5 < num2)
							{
								num3 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num3 = 10;
						i++;
					}
				}
				if (num3 != 60)
				{
					goto IL_01E0;
				}
				if (this.IsTagName(ref styleClosingTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num5;
					if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num5, ref charBuffer, ref writeIndex))
					{
						goto IL_01E0;
					}
					i = num5;
				}
				else
				{
					if (!this.IsTagName(ref styleClosingTagArray, "</STYLE>", i))
					{
						goto IL_01E0;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0209:
				i++;
				continue;
				IL_01E0:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num3;
				writeIndex++;
				goto IL_0209;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00015C40 File Offset: 0x00013E40
		private bool ReplaceClosingStyleTag(ref char[] sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int num = this.m_TextStyleStacks[this.m_TextStyleStackDepth].Pop();
			TMP_Style style = this.GetStyle(num);
			if (style == null)
			{
				return false;
			}
			int num2 = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num2)
			{
				int num3 = styleClosingTagArray[i];
				if (num3 == 92 && i + 1 < num2)
				{
					int num4 = styleClosingTagArray[i + 1];
					if (num4 <= 92)
					{
						if (num4 != 85)
						{
							if (num4 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num2)
						{
							num3 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num4 != 110)
					{
						switch (num4)
						{
						case 117:
							if (i + 5 < num2)
							{
								num3 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num3 = 10;
						i++;
					}
				}
				if (num3 != 60)
				{
					goto IL_01E0;
				}
				if (this.IsTagName(ref styleClosingTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num5;
					if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num5, ref charBuffer, ref writeIndex))
					{
						goto IL_01E0;
					}
					i = num5;
				}
				else
				{
					if (!this.IsTagName(ref styleClosingTagArray, "</STYLE>", i))
					{
						goto IL_01E0;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0209:
				i++;
				continue;
				IL_01E0:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num3;
				writeIndex++;
				goto IL_0209;
			}
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00015E74 File Offset: 0x00014074
		private bool ReplaceClosingStyleTag(ref StringBuilder sourceText, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int num = this.m_TextStyleStacks[this.m_TextStyleStackDepth].Pop();
			TMP_Style style = this.GetStyle(num);
			if (style == null)
			{
				return false;
			}
			int num2 = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num2)
			{
				int num3 = styleClosingTagArray[i];
				if (num3 == 92 && i + 1 < num2)
				{
					int num4 = styleClosingTagArray[i + 1];
					if (num4 <= 92)
					{
						if (num4 != 85)
						{
							if (num4 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num2)
						{
							num3 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num4 != 110)
					{
						switch (num4)
						{
						case 117:
							if (i + 5 < num2)
							{
								num3 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num3 = 10;
						i++;
					}
				}
				if (num3 != 60)
				{
					goto IL_01E0;
				}
				if (this.IsTagName(ref styleClosingTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num5;
					if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num5, ref charBuffer, ref writeIndex))
					{
						goto IL_01E0;
					}
					i = num5;
				}
				else
				{
					if (!this.IsTagName(ref styleClosingTagArray, "</STYLE>", i))
					{
						goto IL_01E0;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_0209:
				i++;
				continue;
				IL_01E0:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num3;
				writeIndex++;
				goto IL_0209;
			}
			return true;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001609C File Offset: 0x0001429C
		private TMP_Style GetStyle(int hashCode)
		{
			TMP_Style tmp_Style = null;
			if (this.m_StyleSheet != null)
			{
				tmp_Style = this.m_StyleSheet.GetStyle(hashCode);
				if (tmp_Style != null)
				{
					return tmp_Style;
				}
			}
			if (TMP_Settings.defaultStyleSheet != null)
			{
				tmp_Style = TMP_Settings.defaultStyleSheet.GetStyle(hashCode);
			}
			return tmp_Style;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000160E8 File Offset: 0x000142E8
		private bool InsertOpeningStyleTag(TMP_Style style, int srcIndex, ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			if (style == null)
			{
				return false;
			}
			this.m_TextStyleStacks[0].Push(style.hashCode);
			int num = style.styleOpeningTagArray.Length;
			int[] styleOpeningTagArray = style.styleOpeningTagArray;
			int i = 0;
			while (i < num)
			{
				int num2 = styleOpeningTagArray[i];
				if (num2 == 92 && i + 1 < num)
				{
					int num3 = styleOpeningTagArray[i + 1];
					if (num3 <= 92)
					{
						if (num3 != 85)
						{
							if (num3 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num)
						{
							num2 = this.GetUTF32(styleOpeningTagArray, i + 2);
							i += 9;
						}
					}
					else if (num3 != 110)
					{
						switch (num3)
						{
						case 117:
							if (i + 5 < num)
							{
								num2 = this.GetUTF16(styleOpeningTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num2 = 10;
						i++;
					}
				}
				if (num2 != 60)
				{
					goto IL_01B2;
				}
				if (this.IsTagName(ref styleOpeningTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleOpeningTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num4;
					if (!this.ReplaceOpeningStyleTag(ref styleOpeningTagArray, i, out num4, ref charBuffer, ref writeIndex))
					{
						goto IL_01B2;
					}
					i = num4;
				}
				else
				{
					if (!this.IsTagName(ref styleOpeningTagArray, "</STYLE>", i))
					{
						goto IL_01B2;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleOpeningTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_01DA:
				i++;
				continue;
				IL_01B2:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num2;
				writeIndex++;
				goto IL_01DA;
			}
			this.m_TextStyleStackDepth = 0;
			return true;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000162E4 File Offset: 0x000144E4
		private bool InsertClosingStyleTag(ref TMP_Text.UnicodeChar[] charBuffer, ref int writeIndex)
		{
			int num = this.m_TextStyleStacks[0].Pop();
			TMP_Style style = this.GetStyle(num);
			int num2 = style.styleClosingTagArray.Length;
			int[] styleClosingTagArray = style.styleClosingTagArray;
			int i = 0;
			while (i < num2)
			{
				int num3 = styleClosingTagArray[i];
				if (num3 == 92 && i + 1 < num2)
				{
					int num4 = styleClosingTagArray[i + 1];
					if (num4 <= 92)
					{
						if (num4 != 85)
						{
							if (num4 == 92)
							{
								i++;
							}
						}
						else if (i + 9 < num2)
						{
							num3 = this.GetUTF32(styleClosingTagArray, i + 2);
							i += 9;
						}
					}
					else if (num4 != 110)
					{
						switch (num4)
						{
						case 117:
							if (i + 5 < num2)
							{
								num3 = this.GetUTF16(styleClosingTagArray, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						num3 = 10;
						i++;
					}
				}
				if (num3 != 60)
				{
					goto IL_01AA;
				}
				if (this.IsTagName(ref styleClosingTagArray, "<BR>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 10;
					writeIndex++;
					i += 3;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<NBSP>", i))
				{
					if (writeIndex == charBuffer.Length)
					{
						this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
					}
					charBuffer[writeIndex].unicode = 160;
					writeIndex++;
					i += 5;
				}
				else if (this.IsTagName(ref styleClosingTagArray, "<STYLE=", i))
				{
					this.m_TextStyleStackDepth++;
					int num5;
					if (!this.ReplaceOpeningStyleTag(ref styleClosingTagArray, i, out num5, ref charBuffer, ref writeIndex))
					{
						goto IL_01AA;
					}
					i = num5;
				}
				else
				{
					if (!this.IsTagName(ref styleClosingTagArray, "</STYLE>", i))
					{
						goto IL_01AA;
					}
					this.m_TextStyleStackDepth++;
					this.ReplaceClosingStyleTag(ref styleClosingTagArray, i, ref charBuffer, ref writeIndex);
					i += 7;
				}
				IL_01CF:
				i++;
				continue;
				IL_01AA:
				if (writeIndex == charBuffer.Length)
				{
					this.ResizeInternalArray<TMP_Text.UnicodeChar>(ref charBuffer);
				}
				charBuffer[writeIndex].unicode = num3;
				writeIndex++;
				goto IL_01CF;
			}
			this.m_TextStyleStackDepth = 0;
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000164D4 File Offset: 0x000146D4
		private bool IsTagName(ref string text, string tag, int index)
		{
			if (text.Length < index + tag.Length)
			{
				return false;
			}
			for (int i = 0; i < tag.Length; i++)
			{
				if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00016520 File Offset: 0x00014720
		private bool IsTagName(ref char[] text, string tag, int index)
		{
			if (text.Length < index + tag.Length)
			{
				return false;
			}
			for (int i = 0; i < tag.Length; i++)
			{
				if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00016568 File Offset: 0x00014768
		private bool IsTagName(ref int[] text, string tag, int index)
		{
			if (text.Length < index + tag.Length)
			{
				return false;
			}
			for (int i = 0; i < tag.Length; i++)
			{
				if (TMP_TextUtilities.ToUpperFast((char)text[index + i]) != tag[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000165B0 File Offset: 0x000147B0
		private bool IsTagName(ref StringBuilder text, string tag, int index)
		{
			if (text.Length < index + tag.Length)
			{
				return false;
			}
			for (int i = 0; i < tag.Length; i++)
			{
				if (TMP_TextUtilities.ToUpperFast(text[index + i]) != tag[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000165FC File Offset: 0x000147FC
		private int GetTagHashCode(ref string text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != '"')
				{
					if (text[i] == '>')
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(text[i]);
				}
			}
			return num;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00016654 File Offset: 0x00014854
		private int GetTagHashCode(ref char[] text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != '"')
				{
					if (text[i] == '>')
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(text[i]);
				}
			}
			return num;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001669C File Offset: 0x0001489C
		private int GetTagHashCode(ref int[] text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != 34)
				{
					if (text[i] == 62)
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast((char)text[i]);
				}
			}
			return num;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000166E4 File Offset: 0x000148E4
		private int GetTagHashCode(ref StringBuilder text, int index, out int closeIndex)
		{
			int num = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != '"')
				{
					if (text[i] == '>')
					{
						closeIndex = i;
						break;
					}
					num = ((num << 5) + num) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(text[i]);
				}
			}
			return num;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001673C File Offset: 0x0001493C
		private void ResizeInternalArray<T>(ref T[] array)
		{
			int num = Mathf.NextPowerOfTwo(array.Length + 1);
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001675C File Offset: 0x0001495C
		protected void AddFloatToCharArray(double number, ref int index, int precision)
		{
			if (number < 0.0)
			{
				char[] input_CharArray = this.m_input_CharArray;
				int num = index;
				index = num + 1;
				input_CharArray[num] = 45;
				number = -number;
			}
			number += (double)this.k_Power[Mathf.Min(9, precision)];
			double num2 = Math.Truncate(number);
			this.AddIntToCharArray(num2, ref index, precision);
			if (precision > 0)
			{
				char[] input_CharArray2 = this.m_input_CharArray;
				int num = index;
				index = num + 1;
				input_CharArray2[num] = 46;
				number -= num2;
				for (int i = 0; i < precision; i++)
				{
					number *= 10.0;
					long num3 = (long)number;
					char[] input_CharArray3 = this.m_input_CharArray;
					num = index;
					index = num + 1;
					input_CharArray3[num] = (ushort)(num3 + 48L);
					number -= (double)num3;
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016804 File Offset: 0x00014A04
		protected void AddIntToCharArray(double number, ref int index, int precision)
		{
			if (number < 0.0)
			{
				char[] input_CharArray = this.m_input_CharArray;
				int num = index;
				index = num + 1;
				input_CharArray[num] = 45;
				number = -number;
			}
			int num2 = index;
			do
			{
				this.m_input_CharArray[num2++] = (char)(number % 10.0 + 48.0);
				number /= 10.0;
			}
			while (number > 0.999);
			int num3 = num2;
			while (index + 1 < num2)
			{
				num2--;
				char c = this.m_input_CharArray[index];
				this.m_input_CharArray[index] = this.m_input_CharArray[num2];
				this.m_input_CharArray[num2] = c;
				index++;
			}
			index = num3;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000168AD File Offset: 0x00014AAD
		protected virtual int SetArraySizes(TMP_Text.UnicodeChar[] chars)
		{
			return 0;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void GenerateTextMesh()
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000168B0 File Offset: 0x00014AB0
		public Vector2 GetPreferredValues()
		{
			if (this.m_isInputParsingRequired || this.m_isTextTruncated)
			{
				this.m_isCalculatingPreferredValues = true;
				this.ParseInputText();
			}
			float preferredWidth = this.GetPreferredWidth();
			float preferredHeight = this.GetPreferredHeight();
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000168F0 File Offset: 0x00014AF0
		public Vector2 GetPreferredValues(float width, float height)
		{
			if (this.m_isInputParsingRequired || this.m_isTextTruncated)
			{
				this.m_isCalculatingPreferredValues = true;
				this.ParseInputText();
			}
			Vector2 vector = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(vector);
			float preferredHeight = this.GetPreferredHeight(vector);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00016938 File Offset: 0x00014B38
		public Vector2 GetPreferredValues(string text)
		{
			this.m_isCalculatingPreferredValues = true;
			this.StringToCharArray(text, ref this.m_TextParsingBuffer);
			this.SetArraySizes(this.m_TextParsingBuffer);
			Vector2 vector = TMP_Text.k_LargePositiveVector2;
			float preferredWidth = this.GetPreferredWidth(vector);
			float preferredHeight = this.GetPreferredHeight(vector);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00016984 File Offset: 0x00014B84
		public Vector2 GetPreferredValues(string text, float width, float height)
		{
			this.m_isCalculatingPreferredValues = true;
			this.StringToCharArray(text, ref this.m_TextParsingBuffer);
			this.SetArraySizes(this.m_TextParsingBuffer);
			Vector2 vector = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(vector);
			float preferredHeight = this.GetPreferredHeight(vector);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000169D0 File Offset: 0x00014BD0
		protected float GetPreferredWidth()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			float num = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 vector = TMP_Text.k_LargePositiveVector2;
			if (this.m_isInputParsingRequired || this.m_isTextTruncated)
			{
				this.m_isCalculatingPreferredValues = true;
				this.ParseInputText();
			}
			this.m_AutoSizeIterationCount = 0;
			float x = this.CalculatePreferredValues(num, vector, true).x;
			this.m_isPreferredWidthDirty = false;
			return x;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00016A6C File Offset: 0x00014C6C
		protected float GetPreferredWidth(Vector2 margin)
		{
			float num = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_AutoSizeIterationCount = 0;
			return this.CalculatePreferredValues(num, margin, true).x;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00016AC8 File Offset: 0x00014CC8
		protected float GetPreferredHeight()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			float num = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 vector = new Vector2((this.m_marginWidth != 0f) ? this.m_marginWidth : TMP_Text.k_LargePositiveFloat, TMP_Text.k_LargePositiveFloat);
			if (this.m_isInputParsingRequired || this.m_isTextTruncated)
			{
				this.m_isCalculatingPreferredValues = true;
				this.ParseInputText();
			}
			this.m_AutoSizeIterationCount = 0;
			float y = this.CalculatePreferredValues(num, vector, !this.m_enableAutoSizing).y;
			this.m_isPreferredHeightDirty = false;
			return y;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00016B8C File Offset: 0x00014D8C
		protected float GetPreferredHeight(Vector2 margin)
		{
			float num = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_AutoSizeIterationCount = 0;
			return this.CalculatePreferredValues(num, margin, true).y;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00016BE8 File Offset: 0x00014DE8
		public Vector2 GetRenderedValues()
		{
			return this.GetTextBounds().size;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00016C08 File Offset: 0x00014E08
		public Vector2 GetRenderedValues(bool onlyVisibleCharacters)
		{
			return this.GetTextBounds(onlyVisibleCharacters).size;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00016C29 File Offset: 0x00014E29
		protected float GetRenderedWidth()
		{
			return this.GetRenderedValues().x;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00016C36 File Offset: 0x00014E36
		protected float GetRenderedWidth(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).x;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00016C44 File Offset: 0x00014E44
		protected float GetRenderedHeight()
		{
			return this.GetRenderedValues().y;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00016C51 File Offset: 0x00014E51
		protected float GetRenderedHeight(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).y;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016C60 File Offset: 0x00014E60
		protected virtual Vector2 CalculatePreferredValues(float defaultFontSize, Vector2 marginSize, bool ignoreTextAutoSizing)
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID());
				return Vector2.zero;
			}
			if (this.m_TextParsingBuffer == null || this.m_TextParsingBuffer.Length == 0 || this.m_TextParsingBuffer[0].unicode == 0)
			{
				return Vector2.zero;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			this.m_materialReferenceStack.SetDefault(new MaterialReference(0, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			int totalCharacterCount = this.m_totalCharacterCount;
			if (this.m_internalCharacterInfo == null || totalCharacterCount > this.m_internalCharacterInfo.Length)
			{
				this.m_internalCharacterInfo = new TMP_CharacterInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
			}
			float num = (this.m_fontScale = defaultFontSize / (float)this.m_fontAsset.faceInfo.pointSize * this.m_fontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f));
			float num2 = num;
			float num3 = this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = defaultFontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float num4 = this.m_currentFontAsset.faceInfo.lineHeight - (this.m_currentFontAsset.faceInfo.ascentLine - this.m_currentFontAsset.faceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			float num5 = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			bool flag = false;
			float x = marginSize.x;
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			float num6 = 0f;
			float num7 = 0f;
			this.m_width = -1f;
			float num8 = x + 0.0001f - this.m_marginLeft - this.m_marginRight;
			float num9 = 0f;
			float num10 = 0f;
			float num11 = 0f;
			this.m_isCalculatingPreferredValues = true;
			this.m_maxCapHeight = 0f;
			this.m_maxAscender = 0f;
			this.m_maxDescender = 0f;
			bool flag2 = false;
			bool flag3 = true;
			bool flag4 = false;
			TMP_Text.CharacterSubstitution characterSubstitution = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool flag5 = false;
			WordWrapState wordWrapState = default(WordWrapState);
			WordWrapState wordWrapState2 = default(WordWrapState);
			this.m_AutoSizeIterationCount++;
			int num12 = 0;
			while (num12 < this.m_TextParsingBuffer.Length && this.m_TextParsingBuffer[num12].unicode != 0)
			{
				int num13 = this.m_TextParsingBuffer[num12].unicode;
				if (!this.m_isRichText || num13 != 60)
				{
					this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					goto IL_0427;
				}
				this.m_isParsingText = true;
				this.m_textElementType = TMP_TextElementType.Character;
				int num14;
				if (!this.ValidateHtmlTag(this.m_TextParsingBuffer, num12 + 1, out num14))
				{
					goto IL_0427;
				}
				num12 = num14;
				if (this.m_textElementType != TMP_TextElementType.Character)
				{
					goto IL_0427;
				}
				IL_1770:
				num12++;
				continue;
				IL_0427:
				int currentMaterialIndex = this.m_currentMaterialIndex;
				bool isUsingAlternateTypeface = this.m_internalCharacterInfo[this.m_characterCount].isUsingAlternateTypeface;
				this.m_isParsingText = false;
				bool flag6 = false;
				if (characterSubstitution.index == this.m_characterCount)
				{
					num13 = (int)characterSubstitution.unicode;
					this.m_textElementType = TMP_TextElementType.Character;
					flag6 = true;
					if (num13 != 3 && num13 != 45 && num13 == 8230)
					{
						this.m_internalCharacterInfo[this.m_characterCount].textElement = this.m_cached_Ellipsis_Character;
						this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
						this.m_internalCharacterInfo[this.m_characterCount].fontAsset = this.m_materialReferences[0].fontAsset;
						this.m_internalCharacterInfo[this.m_characterCount].material = this.m_materialReferences[0].material;
						this.m_internalCharacterInfo[this.m_characterCount].materialReferenceIndex = 0;
						this.m_isTextTruncated = true;
						characterSubstitution.index = this.m_characterCount + 1;
						characterSubstitution.unicode = 3U;
					}
				}
				if (this.m_characterCount < this.m_firstVisibleCharacter && num13 != 3)
				{
					this.m_internalCharacterInfo[this.m_characterCount].isVisible = false;
					this.m_internalCharacterInfo[this.m_characterCount].character = '\u200b';
					this.m_internalCharacterInfo[this.m_characterCount].lineNumber = 0;
					this.m_characterCount++;
					goto IL_1770;
				}
				float num15 = 1f;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num13))
						{
							num13 = (int)char.ToUpper((char)num13);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num13))
						{
							num13 = (int)char.ToLower((char)num13);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num13))
					{
						num15 = 0.8f;
						num13 = (int)char.ToUpper((char)num13);
					}
				}
				float num16 = 1f;
				float num17 = 0f;
				float num18 = 0f;
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					this.m_currentSpriteAsset = this.m_textInfo.characterInfo[this.m_characterCount].spriteAsset;
					this.m_spriteIndex = this.m_textInfo.characterInfo[this.m_characterCount].spriteIndex;
					TMP_SpriteCharacter tmp_SpriteCharacter = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					if (tmp_SpriteCharacter == null)
					{
						goto IL_1770;
					}
					if (num13 == 60)
					{
						num13 = 57344 + this.m_spriteIndex;
					}
					this.m_currentFontAsset = this.m_fontAsset;
					if (this.m_currentSpriteAsset.faceInfo.pointSize > 0)
					{
						num16 = this.m_currentFontSize / (float)this.m_currentSpriteAsset.faceInfo.pointSize * this.m_currentSpriteAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale * num16;
						num17 = this.m_currentSpriteAsset.faceInfo.ascentLine;
						float baseline = this.m_currentSpriteAsset.faceInfo.baseline;
						float fontScale = this.m_fontScale;
						float fontScaleMultiplier = this.m_fontScaleMultiplier;
						float scale = this.m_currentSpriteAsset.faceInfo.scale;
						num18 = this.m_currentSpriteAsset.faceInfo.descentLine;
					}
					else
					{
						num16 = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						num2 = this.m_currentFontAsset.faceInfo.ascentLine / tmp_SpriteCharacter.glyph.metrics.height * tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale * num16;
						num17 = this.m_currentFontAsset.faceInfo.ascentLine;
						float baseline2 = this.m_currentFontAsset.faceInfo.baseline;
						float fontScale2 = this.m_fontScale;
						float fontScaleMultiplier2 = this.m_fontScaleMultiplier;
						float scale2 = this.m_currentFontAsset.faceInfo.scale;
						num18 = this.m_currentFontAsset.faceInfo.descentLine;
					}
					this.m_cached_TextElement = tmp_SpriteCharacter;
					this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
					this.m_internalCharacterInfo[this.m_characterCount].scale = num16;
					this.m_currentMaterialIndex = currentMaterialIndex;
				}
				else if (this.m_textElementType == TMP_TextElementType.Character)
				{
					this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
					if (this.m_cached_TextElement == null)
					{
						goto IL_1770;
					}
					this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
					this.m_fontScale = this.m_currentFontSize * num15 / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					num2 = this.m_fontScale * this.m_fontScaleMultiplier * this.m_cached_TextElement.scale;
					float baseline3 = this.m_currentFontAsset.faceInfo.baseline;
					float fontScale3 = this.m_fontScale;
					float fontScaleMultiplier3 = this.m_fontScaleMultiplier;
					float scale3 = this.m_currentFontAsset.faceInfo.scale;
					this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
				}
				float num19 = num2;
				if (num13 == 173 || num13 == 3)
				{
					num2 = 0f;
				}
				this.m_internalCharacterInfo[this.m_characterCount].character = (char)num13;
				GlyphMetrics metrics = this.m_cached_TextElement.m_Glyph.metrics;
				char.IsWhiteSpace((char)num13);
				TMP_GlyphValueRecord tmp_GlyphValueRecord = default(TMP_GlyphValueRecord);
				float num20 = this.m_characterSpacing;
				if (this.m_enableKerning)
				{
					if (this.m_characterCount < totalCharacterCount - 1)
					{
						uint glyphIndex = this.m_cached_TextElement.glyphIndex;
						uint glyphIndex2 = this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.glyphIndex;
						uint key = new GlyphPairKey(glyphIndex, glyphIndex2).key;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord = tmp_GlyphPairAdjustmentRecord.firstAdjustmentRecord.glyphValueRecord;
							num20 = (((tmp_GlyphPairAdjustmentRecord.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num20);
						}
					}
					if (this.m_characterCount >= 1)
					{
						uint glyphIndex3 = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.glyphIndex;
						uint glyphIndex4 = this.m_cached_TextElement.glyphIndex;
						uint key2 = new GlyphPairKey(glyphIndex3, glyphIndex4).key;
						TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord;
						if (this.m_currentFontAsset.fontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.TryGetValue(key2, out tmp_GlyphPairAdjustmentRecord))
						{
							tmp_GlyphValueRecord += tmp_GlyphPairAdjustmentRecord.secondAdjustmentRecord.glyphValueRecord;
							num20 = (((tmp_GlyphPairAdjustmentRecord.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num20);
						}
					}
				}
				float num21 = 0f;
				if (this.m_monoSpacing != 0f)
				{
					num21 = (this.m_monoSpacing / 2f - (this.m_cached_TextElement.glyph.metrics.width / 2f + this.m_cached_TextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - this.m_charWidthAdjDelta);
					this.m_xAdvance += num21;
				}
				float num22;
				if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAlternateTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					num22 = this.m_currentFontAsset.boldSpacing;
				}
				else
				{
					num22 = 0f;
				}
				this.m_internalCharacterInfo[this.m_characterCount].baseLine = 0f - this.m_lineOffset + this.m_baselineOffset;
				float num23 = ((this.m_textElementType == TMP_TextElementType.Character) ? (this.m_currentFontAsset.faceInfo.ascentLine * num2 / num15 + this.m_baselineOffset) : (num17 * num16 + this.m_baselineOffset));
				this.m_internalCharacterInfo[this.m_characterCount].ascender = num23 - this.m_lineOffset;
				float num24 = ((this.m_textElementType == TMP_TextElementType.Character) ? (this.m_currentFontAsset.faceInfo.descentLine * num2 / num15 + this.m_baselineOffset) : (num18 * num16 + this.m_baselineOffset));
				float num25 = (this.m_internalCharacterInfo[this.m_characterCount].descender = num24 - this.m_lineOffset);
				if (num13 != 10 || this.m_characterCount == this.m_firstCharacterOfLine)
				{
					this.m_maxLineAscender = ((num23 > this.m_maxLineAscender) ? num23 : this.m_maxLineAscender);
					this.m_maxLineDescender = ((num24 < this.m_maxLineDescender) ? num24 : this.m_maxLineDescender);
				}
				if ((this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript || (this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
				{
					float num26 = (num23 - this.m_baselineOffset) / this.m_currentFontAsset.faceInfo.subscriptSize;
					num23 = this.m_maxLineAscender;
					this.m_maxLineAscender = ((num26 > this.m_maxLineAscender) ? num26 : this.m_maxLineAscender);
					float num27 = (num24 - this.m_baselineOffset) / this.m_currentFontAsset.faceInfo.subscriptSize;
					num24 = this.m_maxLineDescender;
					this.m_maxLineDescender = ((num27 < this.m_maxLineDescender) ? num27 : this.m_maxLineDescender);
				}
				if ((this.m_lineNumber == 0 || this.m_isNewPage) && (num13 != 10 || this.m_characterCount == this.m_firstCharacterOfLine))
				{
					this.m_maxAscender = ((this.m_maxAscender > num23) ? this.m_maxAscender : num23);
					this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * num2 / num15);
				}
				bool flag7 = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
				if (num13 == 9 || num13 == 160 || num13 == 8199 || (!char.IsWhiteSpace((char)num13) && num13 != 8203 && num13 != 173 && num13 != 3) || (num13 == 173 && !flag5) || this.m_textElementType == TMP_TextElementType.Sprite)
				{
					num8 = ((this.m_width != -1f) ? Mathf.Min(x + 0.0001f - this.m_marginLeft - this.m_marginRight, this.m_width) : (x + 0.0001f - this.m_marginLeft - this.m_marginRight));
					num11 = Mathf.Abs(this.m_xAdvance) + metrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * ((num13 != 173) ? num2 : num19);
					int characterCount = this.m_characterCount;
					if (num11 > num8 * (flag7 ? 1.05f : 1f) && this.m_enableWordWrapping && this.m_characterCount != this.m_firstCharacterOfLine)
					{
						num12 = this.RestoreWordWrappingState(ref wordWrapState);
						if (this.m_internalCharacterInfo[this.m_characterCount - 1].character == '\u00ad' && !flag5)
						{
							characterSubstitution.index = this.m_characterCount - 1;
							characterSubstitution.unicode = 45U;
							num12--;
							this.m_characterCount--;
							goto IL_1770;
						}
						flag5 = false;
						if (this.m_internalCharacterInfo[this.m_characterCount].character == '\u00ad')
						{
							flag5 = true;
							goto IL_1770;
						}
						float num28 = this.m_maxLineAscender - this.m_lineOffset;
						float num29 = this.m_maxLineDescender - this.m_lineOffset;
						this.m_maxDescender = ((this.m_maxDescender < num29) ? this.m_maxDescender : num29);
						if (!flag2)
						{
							float maxDescender = this.m_maxDescender;
						}
						if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
						{
							flag2 = true;
						}
						this.m_firstCharacterOfLine = this.m_characterCount;
						this.m_lineVisibleCharacterCount = 0;
						num9 += this.m_xAdvance;
						if (this.m_enableWordWrapping)
						{
							num10 = this.m_maxAscender - this.m_maxDescender;
						}
						else
						{
							num10 = Mathf.Max(num10, num28 - num29);
						}
						this.SaveWordWrappingState(ref wordWrapState2, num12, this.m_characterCount - 1);
						this.m_lineNumber++;
						if (this.m_lineHeight == -32767f)
						{
							float num30 = this.m_internalCharacterInfo[this.m_characterCount].ascender - this.m_internalCharacterInfo[this.m_characterCount].baseLine;
							this.m_lineOffset += 0f - this.m_maxLineDescender + num30 + (num4 + this.m_lineSpacingDelta) * num + this.m_lineSpacing * num3;
							flag = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * num3;
							flag = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = num23;
						this.m_xAdvance = 0f + this.tag_Indent;
						flag3 = true;
						goto IL_1770;
					}
					else
					{
						num6 = this.m_marginLeft;
						num7 = this.m_marginRight;
					}
				}
				if (this.m_lineNumber > 0 && !TMP_Math.Approximately(this.m_maxLineAscender, this.m_startOfLineAscender) && !flag && !this.m_isNewPage && !flag6)
				{
					float num31 = this.m_maxLineAscender - this.m_startOfLineAscender;
					num25 -= num31;
					this.m_lineOffset += num31;
					this.m_startOfLineAscender += num31;
					wordWrapState.lineOffset = this.m_lineOffset;
					wordWrapState.previousLineAscender = this.m_startOfLineAscender;
				}
				if (num13 == 9)
				{
					float num32 = this.m_currentFontAsset.faceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * num2;
					float num33 = Mathf.Ceil(this.m_xAdvance / num32) * num32;
					this.m_xAdvance = ((num33 > this.m_xAdvance) ? num33 : (this.m_xAdvance + num32));
				}
				else if (this.m_monoSpacing != 0f)
				{
					this.m_xAdvance += (this.m_monoSpacing - num21 + (this.m_currentFontAsset.normalSpacingOffset + num20) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (char.IsWhiteSpace((char)num13) || num13 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				else
				{
					this.m_xAdvance += ((metrics.horizontalAdvance + tmp_GlyphValueRecord.xAdvance) * num2 + (this.m_currentFontAsset.normalSpacingOffset + num20 + num22) * num3 + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
					if (char.IsWhiteSpace((char)num13) || num13 == 8203)
					{
						this.m_xAdvance += this.m_wordSpacing * num3;
					}
				}
				if (num13 == 13)
				{
					num5 = Mathf.Max(num5, num9 + this.m_xAdvance);
					num9 = 0f;
					this.m_xAdvance = 0f + this.tag_Indent;
				}
				if (num13 == 10 || num13 == 11 || num13 == 3 || this.m_characterCount == totalCharacterCount - 1)
				{
					if (this.m_lineNumber > 0 && !TMP_Math.Approximately(this.m_maxLineAscender, this.m_startOfLineAscender) && !flag && !this.m_isNewPage && !flag6)
					{
						float num34 = this.m_maxLineAscender - this.m_startOfLineAscender;
						num25 -= num34;
						this.m_lineOffset += num34;
					}
					float num35 = this.m_maxLineDescender - this.m_lineOffset;
					this.m_maxDescender = ((this.m_maxDescender < num35) ? this.m_maxDescender : num35);
					if (this.m_characterCount == totalCharacterCount - 1)
					{
						num9 = Mathf.Max(num5, num9 + num11 + num6 + num7);
					}
					else
					{
						num5 = Mathf.Max(num5, num9 + num11 + num6 + num7);
						num9 = 0f;
					}
					num10 = this.m_maxAscender - this.m_maxDescender;
					if (num13 == 10 || num13 == 11 || num13 == 45)
					{
						this.SaveWordWrappingState(ref wordWrapState2, num12, this.m_characterCount);
						this.SaveWordWrappingState(ref wordWrapState, num12, this.m_characterCount);
						this.m_lineNumber++;
						this.m_firstCharacterOfLine = this.m_characterCount + 1;
						if (this.m_lineHeight == -32767f)
						{
							float num36 = 0f - this.m_maxLineDescender + num23 + (num4 + this.m_lineSpacingDelta) * num + (this.m_lineSpacing + ((num13 == 10) ? this.m_paragraphSpacing : 0f)) * num3;
							this.m_lineOffset += num36;
							flag = false;
						}
						else
						{
							this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((num13 == 10) ? this.m_paragraphSpacing : 0f)) * num3;
							flag = true;
						}
						this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
						this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
						this.m_startOfLineAscender = num23;
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						this.m_characterCount++;
						goto IL_1770;
					}
					if (num13 == 3)
					{
						num12 = this.m_TextParsingBuffer.Length;
					}
				}
				if (this.m_enableWordWrapping || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis)
				{
					if ((char.IsWhiteSpace((char)num13) || num13 == 8203 || num13 == 45 || num13 == 173) && !this.m_isNonBreakingSpace && num13 != 160 && num13 != 8199 && num13 != 8209 && num13 != 8239 && num13 != 8288)
					{
						this.SaveWordWrappingState(ref wordWrapState, num12, this.m_characterCount);
						flag3 = false;
					}
					else if (((num13 > 4352 && num13 < 4607) || (num13 > 11904 && num13 < 40959) || (num13 > 43360 && num13 < 43391) || (num13 > 44032 && num13 < 55295) || (num13 > 63744 && num13 < 64255) || (num13 > 65072 && num13 < 65103) || (num13 > 65280 && num13 < 65519)) && !this.m_isNonBreakingSpace && !TMP_Settings.useModernHangulLineBreakingRules)
					{
						if (flag3 || flag4 || (!TMP_Settings.linebreakingRules.leadingCharacters.ContainsKey(num13) && this.m_characterCount < totalCharacterCount - 1 && !TMP_Settings.linebreakingRules.followingCharacters.ContainsKey((int)this.m_internalCharacterInfo[this.m_characterCount + 1].character)))
						{
							this.SaveWordWrappingState(ref wordWrapState, num12, this.m_characterCount);
							flag3 = false;
						}
					}
					else if (flag3 || flag4)
					{
						this.SaveWordWrappingState(ref wordWrapState, num12, this.m_characterCount);
					}
				}
				this.m_characterCount++;
				goto IL_1770;
			}
			this.m_isCalculatingPreferredValues = false;
			num9 += ((this.m_margin.x > 0f) ? this.m_margin.x : 0f);
			num9 += ((this.m_margin.z > 0f) ? this.m_margin.z : 0f);
			num10 += ((this.m_margin.y > 0f) ? this.m_margin.y : 0f);
			num10 += ((this.m_margin.w > 0f) ? this.m_margin.w : 0f);
			num9 = (float)((int)(num9 * 100f + 1f)) / 100f;
			num10 = (float)((int)(num10 * 100f + 1f)) / 100f;
			return new Vector2(num9, num10);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000184EC File Offset: 0x000166EC
		protected virtual Bounds GetCompoundBounds()
		{
			return default(Bounds);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00018504 File Offset: 0x00016704
		protected Bounds GetTextBounds()
		{
			if (this.m_textInfo == null || this.m_textInfo.characterCount > this.m_textInfo.characterInfo.Length)
			{
				return default(Bounds);
			}
			Extents extents = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int num = 0;
			while (num < this.m_textInfo.characterCount && num < this.m_textInfo.characterInfo.Length)
			{
				if (this.m_textInfo.characterInfo[num].isVisible)
				{
					extents.min.x = Mathf.Min(extents.min.x, this.m_textInfo.characterInfo[num].bottomLeft.x);
					extents.min.y = Mathf.Min(extents.min.y, this.m_textInfo.characterInfo[num].descender);
					extents.max.x = Mathf.Max(extents.max.x, this.m_textInfo.characterInfo[num].xAdvance);
					extents.max.y = Mathf.Max(extents.max.y, this.m_textInfo.characterInfo[num].ascender);
				}
				num++;
			}
			Vector2 vector;
			vector.x = extents.max.x - extents.min.x;
			vector.y = extents.max.y - extents.min.y;
			return new Bounds((extents.min + extents.max) / 2f, vector);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000186CC File Offset: 0x000168CC
		protected Bounds GetTextBounds(bool onlyVisibleCharacters)
		{
			if (this.m_textInfo == null)
			{
				return default(Bounds);
			}
			Extents extents = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int num = 0;
			while (num < this.m_textInfo.characterCount && ((num <= this.maxVisibleCharacters && this.m_textInfo.characterInfo[num].lineNumber <= this.m_maxVisibleLines) || !onlyVisibleCharacters))
			{
				if (!onlyVisibleCharacters || this.m_textInfo.characterInfo[num].isVisible)
				{
					extents.min.x = Mathf.Min(extents.min.x, this.m_textInfo.characterInfo[num].origin);
					extents.min.y = Mathf.Min(extents.min.y, this.m_textInfo.characterInfo[num].descender);
					extents.max.x = Mathf.Max(extents.max.x, this.m_textInfo.characterInfo[num].xAdvance);
					extents.max.y = Mathf.Max(extents.max.y, this.m_textInfo.characterInfo[num].ascender);
				}
				num++;
			}
			Vector2 vector;
			vector.x = extents.max.x - extents.min.x;
			vector.y = extents.max.y - extents.min.y;
			return new Bounds((extents.min + extents.max) / 2f, vector);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00018898 File Offset: 0x00016A98
		protected void AdjustLineOffset(int startIndex, int endIndex, float offset)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
				int num = i;
				characterInfo[num].bottomLeft = characterInfo[num].bottomLeft - vector;
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num2 = i;
				characterInfo2[num2].topLeft = characterInfo2[num2].topLeft - vector;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num3 = i;
				characterInfo3[num3].topRight = characterInfo3[num3].topRight - vector;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num4 = i;
				characterInfo4[num4].bottomRight = characterInfo4[num4].bottomRight - vector;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num5 = i;
				characterInfo5[num5].ascender = characterInfo5[num5].ascender - vector.y;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num6 = i;
				characterInfo6[num6].baseLine = characterInfo6[num6].baseLine - vector.y;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num7 = i;
				characterInfo7[num7].descender = characterInfo7[num7].descender - vector.y;
				if (this.m_textInfo.characterInfo[i].isVisible)
				{
					TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
					int num8 = i;
					characterInfo8[num8].vertex_BL.position = characterInfo8[num8].vertex_BL.position - vector;
					TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
					int num9 = i;
					characterInfo9[num9].vertex_TL.position = characterInfo9[num9].vertex_TL.position - vector;
					TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
					int num10 = i;
					characterInfo10[num10].vertex_TR.position = characterInfo10[num10].vertex_TR.position - vector;
					TMP_CharacterInfo[] characterInfo11 = this.m_textInfo.characterInfo;
					int num11 = i;
					characterInfo11[num11].vertex_BR.position = characterInfo11[num11].vertex_BR.position - vector;
				}
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00018A90 File Offset: 0x00016C90
		protected void ResizeLineExtents(int size)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
			TMP_LineInfo[] array = new TMP_LineInfo[size];
			for (int i = 0; i < size; i++)
			{
				if (i < this.m_textInfo.lineInfo.Length)
				{
					array[i] = this.m_textInfo.lineInfo[i];
				}
				else
				{
					array[i].lineExtents.min = TMP_Text.k_LargePositiveVector2;
					array[i].lineExtents.max = TMP_Text.k_LargeNegativeVector2;
					array[i].ascender = TMP_Text.k_LargeNegativeFloat;
					array[i].descender = TMP_Text.k_LargePositiveFloat;
				}
			}
			this.m_textInfo.lineInfo = array;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013544 File Offset: 0x00011744
		public virtual TMP_TextInfo GetTextInfo(string text)
		{
			return null;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void ComputeMarginSize()
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00018B50 File Offset: 0x00016D50
		protected void InsertNewLine(int i, float baseScale, float currentEmScale, float characterSpacingAdjustment, float width, float lineGap, ref bool isMaxVisibleDescenderSet, ref float maxVisibleDescender)
		{
			float num = this.m_maxLineAscender - this.m_lineOffset;
			float num2 = this.m_maxLineDescender - this.m_lineOffset;
			this.m_maxDescender = ((this.m_maxDescender < num2) ? this.m_maxDescender : num2);
			if (!isMaxVisibleDescenderSet)
			{
				maxVisibleDescender = this.m_maxDescender;
			}
			if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
			{
				isMaxVisibleDescenderSet = true;
			}
			this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
			this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
			this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = ((this.m_characterCount - 1 > 0) ? (this.m_characterCount - 1) : 0));
			this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
			this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
			this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, num2);
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, num);
			this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x;
			this.m_textInfo.lineInfo[this.m_lineNumber].width = width;
			this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance - (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment) * currentEmScale - this.m_cSpacing;
			this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
			this.m_textInfo.lineInfo[this.m_lineNumber].ascender = num;
			this.m_textInfo.lineInfo[this.m_lineNumber].descender = num2;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = num - num2 + lineGap * baseScale;
			this.m_firstCharacterOfLine = this.m_characterCount;
			this.m_lineVisibleCharacterCount = 0;
			this.SaveWordWrappingState(ref this.m_SavedLineState, i, this.m_characterCount - 1);
			this.m_lineNumber++;
			if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
			{
				this.ResizeLineExtents(this.m_lineNumber);
			}
			if (this.m_lineHeight == -32767f)
			{
				float num3 = this.m_textInfo.characterInfo[this.m_characterCount].ascender - this.m_textInfo.characterInfo[this.m_characterCount].baseLine;
				float num4 = 0f - this.m_maxLineDescender + num3 + (lineGap + this.m_lineSpacingDelta) * baseScale + this.m_lineSpacing * currentEmScale;
				this.m_lineOffset += num4;
				this.m_startOfLineAscender = num3;
			}
			else
			{
				this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * currentEmScale;
			}
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_xAdvance = 0f + this.tag_Indent;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00018FF0 File Offset: 0x000171F0
		protected void SaveWordWrappingState(ref WordWrapState state, int index, int count)
		{
			state.currentFontAsset = this.m_currentFontAsset;
			state.currentSpriteAsset = this.m_currentSpriteAsset;
			state.currentMaterial = this.m_currentMaterial;
			state.currentMaterialIndex = this.m_currentMaterialIndex;
			state.previous_WordBreak = index;
			state.total_CharacterCount = count;
			state.visible_CharacterCount = this.m_lineVisibleCharacterCount;
			state.visible_LinkCount = this.m_textInfo.linkCount;
			state.firstCharacterIndex = this.m_firstCharacterOfLine;
			state.firstVisibleCharacterIndex = this.m_firstVisibleCharacterOfLine;
			state.lastVisibleCharIndex = this.m_lastVisibleCharacterOfLine;
			state.fontStyle = this.m_FontStyleInternal;
			state.italicAngle = this.m_ItalicAngle;
			state.fontScale = this.m_fontScale;
			state.fontScaleMultiplier = this.m_fontScaleMultiplier;
			state.currentFontSize = this.m_currentFontSize;
			state.xAdvance = this.m_xAdvance;
			state.maxCapHeight = this.m_maxCapHeight;
			state.maxAscender = this.m_maxAscender;
			state.maxDescender = this.m_maxDescender;
			state.maxLineAscender = this.m_maxLineAscender;
			state.maxLineDescender = this.m_maxLineDescender;
			state.previousLineAscender = this.m_startOfLineAscender;
			state.preferredWidth = this.m_preferredWidth;
			state.preferredHeight = this.m_preferredHeight;
			state.meshExtents = this.m_meshExtents;
			state.lineNumber = this.m_lineNumber;
			state.lineOffset = this.m_lineOffset;
			state.baselineOffset = this.m_baselineOffset;
			state.cSpace = this.m_cSpacing;
			state.mSpace = this.m_monoSpacing;
			state.horizontalAlignment = this.m_lineJustification;
			state.marginLeft = this.m_marginLeft;
			state.marginRight = this.m_marginRight;
			state.vertexColor = this.m_htmlColor;
			state.underlineColor = this.m_underlineColor;
			state.strikethroughColor = this.m_strikethroughColor;
			state.isNonBreakingSpace = this.m_isNonBreakingSpace;
			state.tagNoParsing = this.tag_NoParsing;
			state.basicStyleStack = this.m_fontStyleStack;
			state.italicAngleStack = this.m_ItalicAngleStack;
			state.colorStack = this.m_colorStack;
			state.underlineColorStack = this.m_underlineColorStack;
			state.strikethroughColorStack = this.m_strikethroughColorStack;
			state.highlightStateStack = this.m_HighlightStateStack;
			state.colorGradientStack = this.m_colorGradientStack;
			state.sizeStack = this.m_sizeStack;
			state.indentStack = this.m_indentStack;
			state.fontWeightStack = this.m_FontWeightStack;
			state.baselineStack = this.m_baselineOffsetStack;
			state.actionStack = this.m_actionStack;
			state.materialReferenceStack = this.m_materialReferenceStack;
			state.lineJustificationStack = this.m_lineJustificationStack;
			state.spriteAnimationID = this.m_spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				state.lineInfo = this.m_textInfo.lineInfo[this.m_lineNumber];
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000192B4 File Offset: 0x000174B4
		protected int RestoreWordWrappingState(ref WordWrapState state)
		{
			int previous_WordBreak = state.previous_WordBreak;
			this.m_currentFontAsset = state.currentFontAsset;
			this.m_currentSpriteAsset = state.currentSpriteAsset;
			this.m_currentMaterial = state.currentMaterial;
			this.m_currentMaterialIndex = state.currentMaterialIndex;
			this.m_characterCount = state.total_CharacterCount + 1;
			this.m_lineVisibleCharacterCount = state.visible_CharacterCount;
			this.m_textInfo.linkCount = state.visible_LinkCount;
			this.m_firstCharacterOfLine = state.firstCharacterIndex;
			this.m_firstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
			this.m_lastVisibleCharacterOfLine = state.lastVisibleCharIndex;
			this.m_FontStyleInternal = state.fontStyle;
			this.m_ItalicAngle = state.italicAngle;
			this.m_fontScale = state.fontScale;
			this.m_fontScaleMultiplier = state.fontScaleMultiplier;
			this.m_currentFontSize = state.currentFontSize;
			this.m_xAdvance = state.xAdvance;
			this.m_maxCapHeight = state.maxCapHeight;
			this.m_maxAscender = state.maxAscender;
			this.m_maxDescender = state.maxDescender;
			this.m_maxLineAscender = state.maxLineAscender;
			this.m_maxLineDescender = state.maxLineDescender;
			this.m_startOfLineAscender = state.previousLineAscender;
			this.m_preferredWidth = state.preferredWidth;
			this.m_preferredHeight = state.preferredHeight;
			this.m_meshExtents = state.meshExtents;
			this.m_lineNumber = state.lineNumber;
			this.m_lineOffset = state.lineOffset;
			this.m_baselineOffset = state.baselineOffset;
			this.m_cSpacing = state.cSpace;
			this.m_monoSpacing = state.mSpace;
			this.m_lineJustification = state.horizontalAlignment;
			this.m_marginLeft = state.marginLeft;
			this.m_marginRight = state.marginRight;
			this.m_htmlColor = state.vertexColor;
			this.m_underlineColor = state.underlineColor;
			this.m_strikethroughColor = state.strikethroughColor;
			this.m_isNonBreakingSpace = state.isNonBreakingSpace;
			this.tag_NoParsing = state.tagNoParsing;
			this.m_fontStyleStack = state.basicStyleStack;
			this.m_ItalicAngleStack = state.italicAngleStack;
			this.m_colorStack = state.colorStack;
			this.m_underlineColorStack = state.underlineColorStack;
			this.m_strikethroughColorStack = state.strikethroughColorStack;
			this.m_HighlightStateStack = state.highlightStateStack;
			this.m_colorGradientStack = state.colorGradientStack;
			this.m_sizeStack = state.sizeStack;
			this.m_indentStack = state.indentStack;
			this.m_FontWeightStack = state.fontWeightStack;
			this.m_baselineOffsetStack = state.baselineStack;
			this.m_actionStack = state.actionStack;
			this.m_materialReferenceStack = state.materialReferenceStack;
			this.m_lineJustificationStack = state.lineJustificationStack;
			this.m_spriteAnimationID = state.spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				this.m_textInfo.lineInfo[this.m_lineNumber] = state.lineInfo;
			}
			return previous_WordBreak;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001957C File Offset: 0x0001777C
		protected virtual void SaveGlyphVertexInfo(float padding, float style_padding, Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			vertexColor.a = ((this.m_fontColor32.a < vertexColor.a) ? this.m_fontColor32.a : vertexColor.a);
			if (!this.m_enableVertexGradient)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (!this.m_overrideHtmlColors && this.m_colorStack.index > 1)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (this.m_fontColorGradientPreset != null)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradientPreset.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradientPreset.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradientPreset.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradientPreset.bottomRight * vertexColor;
			}
			else
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradient.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradient.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradient.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradient.bottomRight * vertexColor;
			}
			if (this.m_colorGradientPreset != null)
			{
				if (this.m_colorGradientPresetIsTinted)
				{
					TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
					int characterCount = this.m_characterCount;
					characterInfo[characterCount].vertex_BL.color = characterInfo[characterCount].vertex_BL.color * this.m_colorGradientPreset.bottomLeft;
					TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
					int characterCount2 = this.m_characterCount;
					characterInfo2[characterCount2].vertex_TL.color = characterInfo2[characterCount2].vertex_TL.color * this.m_colorGradientPreset.topLeft;
					TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
					int characterCount3 = this.m_characterCount;
					characterInfo3[characterCount3].vertex_TR.color = characterInfo3[characterCount3].vertex_TR.color * this.m_colorGradientPreset.topRight;
					TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
					int characterCount4 = this.m_characterCount;
					characterInfo4[characterCount4].vertex_BR.color = characterInfo4[characterCount4].vertex_BR.color * this.m_colorGradientPreset.bottomRight;
				}
				else
				{
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_colorGradientPreset.bottomLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_colorGradientPreset.topLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_colorGradientPreset.topRight.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_colorGradientPreset.bottomRight.MinAlpha(vertexColor);
				}
			}
			if (!this.m_isSDFShader)
			{
				style_padding = 0f;
			}
			GlyphRect glyphRect = this.m_cached_TextElement.m_Glyph.glyphRect;
			Vector2 vector;
			vector.x = ((float)glyphRect.x - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasWidth;
			vector.y = ((float)glyphRect.y - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 vector2;
			vector2.x = vector.x;
			vector2.y = ((float)glyphRect.y + padding + style_padding + (float)glyphRect.height) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 vector3;
			vector3.x = ((float)glyphRect.x + padding + style_padding + (float)glyphRect.width) / (float)this.m_currentFontAsset.m_AtlasWidth;
			vector3.y = vector2.y;
			Vector2 vector4;
			vector4.x = vector3.x;
			vector4.y = vector.y;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = vector;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = vector2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = vector3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = vector4;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00019D58 File Offset: 0x00017F58
		protected virtual void SaveSpriteVertexInfo(Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			if (this.m_tintAllSprites)
			{
				this.m_tintSprite = true;
			}
			Color32 color = (this.m_tintSprite ? this.m_spriteColor.Multiply(vertexColor) : this.m_spriteColor);
			color.a = ((color.a < this.m_fontColor32.a) ? (color.a = ((color.a < vertexColor.a) ? color.a : vertexColor.a)) : this.m_fontColor32.a);
			Color32 color2 = color;
			Color32 color3 = color;
			Color32 color4 = color;
			Color32 color5 = color;
			if (this.m_enableVertexGradient)
			{
				if (this.m_fontColorGradientPreset != null)
				{
					color2 = (this.m_tintSprite ? color2.Multiply(this.m_fontColorGradientPreset.bottomLeft) : color2);
					color3 = (this.m_tintSprite ? color3.Multiply(this.m_fontColorGradientPreset.topLeft) : color3);
					color4 = (this.m_tintSprite ? color4.Multiply(this.m_fontColorGradientPreset.topRight) : color4);
					color5 = (this.m_tintSprite ? color5.Multiply(this.m_fontColorGradientPreset.bottomRight) : color5);
				}
				else
				{
					color2 = (this.m_tintSprite ? color2.Multiply(this.m_fontColorGradient.bottomLeft) : color2);
					color3 = (this.m_tintSprite ? color3.Multiply(this.m_fontColorGradient.topLeft) : color3);
					color4 = (this.m_tintSprite ? color4.Multiply(this.m_fontColorGradient.topRight) : color4);
					color5 = (this.m_tintSprite ? color5.Multiply(this.m_fontColorGradient.bottomRight) : color5);
				}
			}
			if (this.m_colorGradientPreset != null)
			{
				color2 = (this.m_tintSprite ? color2.Multiply(this.m_colorGradientPreset.bottomLeft) : color2);
				color3 = (this.m_tintSprite ? color3.Multiply(this.m_colorGradientPreset.topLeft) : color3);
				color4 = (this.m_tintSprite ? color4.Multiply(this.m_colorGradientPreset.topRight) : color4);
				color5 = (this.m_tintSprite ? color5.Multiply(this.m_colorGradientPreset.bottomRight) : color5);
			}
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = color2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = color3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = color4;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = color5;
			GlyphRect glyphRect = this.m_cached_TextElement.m_Glyph.glyphRect;
			Vector2 vector = new Vector2((float)glyphRect.x / (float)this.m_currentSpriteAsset.spriteSheet.width, (float)glyphRect.y / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 vector2 = new Vector2(vector.x, (float)(glyphRect.y + glyphRect.height) / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 vector3 = new Vector2((float)(glyphRect.x + glyphRect.width) / (float)this.m_currentSpriteAsset.spriteSheet.width, vector2.y);
			Vector2 vector4 = new Vector2(vector3.x, vector.y);
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = vector;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = vector2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = vector3;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = vector4;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001A278 File Offset: 0x00018478
		protected virtual void FillCharacterVertexBuffers(int i, int index_X4)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001A5D8 File Offset: 0x000187D8
		protected virtual void FillCharacterVertexBuffers(int i, int index_X4, bool isVolumetric)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			if (isVolumetric)
			{
				Vector3 vector = new Vector3(0f, 0f, this.m_fontSize * this.m_fontScale);
				this.m_textInfo.meshInfo[materialReferenceIndex].vertices[4 + index_X4] = characterInfo[i].vertex_BL.position + vector;
				this.m_textInfo.meshInfo[materialReferenceIndex].vertices[5 + index_X4] = characterInfo[i].vertex_TL.position + vector;
				this.m_textInfo.meshInfo[materialReferenceIndex].vertices[6 + index_X4] = characterInfo[i].vertex_TR.position + vector;
				this.m_textInfo.meshInfo[materialReferenceIndex].vertices[7 + index_X4] = characterInfo[i].vertex_BR.position + vector;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[4 + index_X4] = characterInfo[i].vertex_BL.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[5 + index_X4] = characterInfo[i].vertex_TL.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[6 + index_X4] = characterInfo[i].vertex_TR.uv;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[7 + index_X4] = characterInfo[i].vertex_BR.uv;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[4 + index_X4] = characterInfo[i].vertex_BL.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[5 + index_X4] = characterInfo[i].vertex_TL.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[6 + index_X4] = characterInfo[i].vertex_TR.uv2;
				this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[7 + index_X4] = characterInfo[i].vertex_BR.uv2;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			if (isVolumetric)
			{
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 128, byte.MaxValue);
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[4 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[5 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[6 + index_X4] = color;
				this.m_textInfo.meshInfo[materialReferenceIndex].colors32[7 + index_X4] = color;
			}
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + ((!isVolumetric) ? 4 : 8);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001AC58 File Offset: 0x00018E58
		protected virtual void FillSpriteVertexBuffers(int i, int index_X4)
		{
			int materialReferenceIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			index_X4 = this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount;
			TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[index_X4] = characterInfo[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[1 + index_X4] = characterInfo[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[2 + index_X4] = characterInfo[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertices[3 + index_X4] = characterInfo[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[index_X4] = characterInfo[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[1 + index_X4] = characterInfo[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[2 + index_X4] = characterInfo[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs0[3 + index_X4] = characterInfo[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[index_X4] = characterInfo[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[1 + index_X4] = characterInfo[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[2 + index_X4] = characterInfo[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].uvs2[3 + index_X4] = characterInfo[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[index_X4] = characterInfo[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[1 + index_X4] = characterInfo[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[2 + index_X4] = characterInfo[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].colors32[3 + index_X4] = characterInfo[i].vertex_BR.color;
			this.m_textInfo.meshInfo[materialReferenceIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001AFB8 File Offset: 0x000191B8
		protected virtual void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor)
		{
			if (this.m_cached_Underline_Character == null)
			{
				if (!TMP_Settings.warningsDisabled)
				{
					Debug.LogWarning("Unable to add underline since the Font Asset doesn't contain the underline character.", this);
				}
				return;
			}
			int num = index + 12;
			if (num > this.m_textInfo.meshInfo[0].vertices.Length)
			{
				this.m_textInfo.meshInfo[0].ResizeMeshInfo(num / 4);
			}
			start.y = Mathf.Min(start.y, end.y);
			end.y = Mathf.Min(start.y, end.y);
			float num2 = this.m_cached_Underline_Character.glyph.metrics.width / 2f * maxScale;
			if (end.x - start.x < this.m_cached_Underline_Character.glyph.metrics.width * maxScale)
			{
				num2 = (end.x - start.x) / 2f;
			}
			float num3 = this.m_padding * startScale / maxScale;
			float num4 = this.m_padding * endScale / maxScale;
			float underlineThickness = this.m_fontAsset.faceInfo.underlineThickness;
			Vector3[] vertices = this.m_textInfo.meshInfo[0].vertices;
			vertices[index] = start + new Vector3(0f, 0f - (underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 1] = start + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 2] = vertices[index + 1] + new Vector3(num2, 0f, 0f);
			vertices[index + 3] = vertices[index] + new Vector3(num2, 0f, 0f);
			vertices[index + 4] = vertices[index + 3];
			vertices[index + 5] = vertices[index + 2];
			vertices[index + 6] = end + new Vector3(-num2, this.m_padding * maxScale, 0f);
			vertices[index + 7] = end + new Vector3(-num2, -(underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 8] = vertices[index + 7];
			vertices[index + 9] = vertices[index + 6];
			vertices[index + 10] = end + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 11] = end + new Vector3(0f, -(underlineThickness + this.m_padding) * maxScale, 0f);
			Vector2[] uvs = this.m_textInfo.meshInfo[0].uvs0;
			Vector2 vector = new Vector2(((float)this.m_cached_Underline_Character.glyph.glyphRect.x - num3) / (float)this.m_fontAsset.atlasWidth, ((float)this.m_cached_Underline_Character.glyph.glyphRect.y - this.m_padding) / (float)this.m_fontAsset.atlasHeight);
			Vector2 vector2 = new Vector2(vector.x, ((float)(this.m_cached_Underline_Character.glyph.glyphRect.y + this.m_cached_Underline_Character.glyph.glyphRect.height) + this.m_padding) / (float)this.m_fontAsset.atlasHeight);
			Vector2 vector3 = new Vector2(((float)this.m_cached_Underline_Character.glyph.glyphRect.x - num3 + (float)this.m_cached_Underline_Character.glyph.glyphRect.width / 2f) / (float)this.m_fontAsset.atlasWidth, vector2.y);
			Vector2 vector4 = new Vector2(vector3.x, vector.y);
			Vector2 vector5 = new Vector2(((float)this.m_cached_Underline_Character.glyph.glyphRect.x + num4 + (float)this.m_cached_Underline_Character.glyph.glyphRect.width / 2f) / (float)this.m_fontAsset.atlasWidth, vector2.y);
			Vector2 vector6 = new Vector2(vector5.x, vector.y);
			Vector2 vector7 = new Vector2(((float)this.m_cached_Underline_Character.glyph.glyphRect.x + num4 + (float)this.m_cached_Underline_Character.glyph.glyphRect.width) / (float)this.m_fontAsset.atlasWidth, vector2.y);
			Vector2 vector8 = new Vector2(vector7.x, vector.y);
			uvs[index] = vector;
			uvs[1 + index] = vector2;
			uvs[2 + index] = vector3;
			uvs[3 + index] = vector4;
			uvs[4 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector.y);
			uvs[5 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector2.y);
			uvs[6 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector2.y);
			uvs[7 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector.y);
			uvs[8 + index] = vector6;
			uvs[9 + index] = vector5;
			uvs[10 + index] = vector7;
			uvs[11 + index] = vector8;
			float num5 = (vertices[index + 2].x - start.x) / (end.x - start.x);
			float num6 = Mathf.Abs(sdfScale);
			Vector2[] uvs2 = this.m_textInfo.meshInfo[0].uvs2;
			uvs2[index] = this.PackUV(0f, 0f, num6);
			uvs2[1 + index] = this.PackUV(0f, 1f, num6);
			uvs2[2 + index] = this.PackUV(num5, 1f, num6);
			uvs2[3 + index] = this.PackUV(num5, 0f, num6);
			float num7 = (vertices[index + 4].x - start.x) / (end.x - start.x);
			num5 = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[4 + index] = this.PackUV(num7, 0f, num6);
			uvs2[5 + index] = this.PackUV(num7, 1f, num6);
			uvs2[6 + index] = this.PackUV(num5, 1f, num6);
			uvs2[7 + index] = this.PackUV(num5, 0f, num6);
			num7 = (vertices[index + 8].x - start.x) / (end.x - start.x);
			num5 = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[8 + index] = this.PackUV(num7, 0f, num6);
			uvs2[9 + index] = this.PackUV(num7, 1f, num6);
			uvs2[10 + index] = this.PackUV(1f, 1f, num6);
			uvs2[11 + index] = this.PackUV(1f, 0f, num6);
			underlineColor.a = ((this.m_fontColor32.a < underlineColor.a) ? this.m_fontColor32.a : underlineColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[0].colors32;
			colors[index] = underlineColor;
			colors[1 + index] = underlineColor;
			colors[2 + index] = underlineColor;
			colors[3 + index] = underlineColor;
			colors[4 + index] = underlineColor;
			colors[5 + index] = underlineColor;
			colors[6 + index] = underlineColor;
			colors[7 + index] = underlineColor;
			colors[8 + index] = underlineColor;
			colors[9 + index] = underlineColor;
			colors[10 + index] = underlineColor;
			colors[11 + index] = underlineColor;
			index += 12;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		protected virtual void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor)
		{
			if (this.m_cached_Underline_Character == null)
			{
				if (!TMP_Settings.warningsDisabled)
				{
					Debug.LogWarning("Unable to add highlight since the Font Asset doesn't contain the underline character.", this);
				}
				return;
			}
			int num = index + 4;
			if (num > this.m_textInfo.meshInfo[0].vertices.Length)
			{
				this.m_textInfo.meshInfo[0].ResizeMeshInfo(num / 4);
			}
			Vector3[] vertices = this.m_textInfo.meshInfo[0].vertices;
			vertices[index] = start;
			vertices[index + 1] = new Vector3(start.x, end.y, 0f);
			vertices[index + 2] = end;
			vertices[index + 3] = new Vector3(end.x, start.y, 0f);
			Vector2[] uvs = this.m_textInfo.meshInfo[0].uvs0;
			Vector2 vector = new Vector2(((float)this.m_cached_Underline_Character.glyph.glyphRect.x + (float)(this.m_cached_Underline_Character.glyph.glyphRect.width / 2)) / (float)this.m_fontAsset.atlasWidth, ((float)this.m_cached_Underline_Character.glyph.glyphRect.y + (float)this.m_cached_Underline_Character.glyph.glyphRect.height / 2f) / (float)this.m_fontAsset.atlasHeight);
			uvs[index] = vector;
			uvs[1 + index] = vector;
			uvs[2 + index] = vector;
			uvs[3 + index] = vector;
			Vector2[] uvs2 = this.m_textInfo.meshInfo[0].uvs2;
			Vector2 vector2 = new Vector2(0f, 1f);
			uvs2[index] = vector2;
			uvs2[1 + index] = vector2;
			uvs2[2 + index] = vector2;
			uvs2[3 + index] = vector2;
			highlightColor.a = ((this.m_fontColor32.a < highlightColor.a) ? this.m_fontColor32.a : highlightColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[0].colors32;
			colors[index] = highlightColor;
			colors[1 + index] = highlightColor;
			colors[2 + index] = highlightColor;
			colors[3 + index] = highlightColor;
			index += 4;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001BB18 File Offset: 0x00019D18
		protected void LoadDefaultSettings()
		{
			if (this.m_text == null || this.m_isWaitingOnResourceLoad)
			{
				this.m_rectTransform = this.rectTransform;
				if (TMP_Settings.autoSizeTextContainer)
				{
					this.autoSizeTextContainer = true;
				}
				else if (base.GetType() == typeof(TextMeshPro))
				{
					if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
					{
						this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProTextContainerSize;
					}
				}
				else if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
				{
					this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProUITextContainerSize;
				}
				this.m_enableWordWrapping = TMP_Settings.enableWordWrapping;
				this.m_enableKerning = TMP_Settings.enableKerning;
				this.m_enableExtraPadding = TMP_Settings.enableExtraPadding;
				this.m_tintAllSprites = TMP_Settings.enableTintAllSprites;
				this.m_parseCtrlCharacters = TMP_Settings.enableParseEscapeCharacters;
				this.m_fontSize = (this.m_fontSizeBase = TMP_Settings.defaultFontSize);
				this.m_fontSizeMin = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMinRatio;
				this.m_fontSizeMax = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMaxRatio;
				this.m_isAlignmentEnumConverted = true;
				this.m_isWaitingOnResourceLoad = false;
				this.raycastTarget = TMP_Settings.enableRaycastTarget;
			}
			else if (this.m_textAlignment < (TextAlignmentOptions)255)
			{
				this.m_textAlignment = TMP_Compatibility.ConvertTextAlignmentEnumValues(this.m_textAlignment);
			}
			if (this.m_textAlignment != TextAlignmentOptions.Converted)
			{
				this.m_HorizontalAlignment = (HorizontalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)255);
				this.m_VerticalAlignment = (VerticalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)65280);
				this.m_textAlignment = TextAlignmentOptions.Converted;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		protected void GetSpecialCharacters(TMP_FontAsset fontAsset)
		{
			if (!fontAsset.characterLookupTable.TryGetValue(95U, out this.m_cached_Underline_Character))
			{
				bool flag;
				TMP_FontAsset tmp_FontAsset;
				this.m_cached_Underline_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(95U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				if (this.m_cached_Underline_Character == null && !TMP_Settings.warningsDisabled)
				{
					Debug.LogWarning("The character used for Underline and Strikethrough is not available in font asset [" + fontAsset.name + "].", this);
				}
			}
			if (!fontAsset.characterLookupTable.TryGetValue(8230U, out this.m_cached_Ellipsis_Character))
			{
				bool flag;
				TMP_FontAsset tmp_FontAsset;
				this.m_cached_Ellipsis_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out flag, out tmp_FontAsset);
				if (this.m_cached_Ellipsis_Character == null && !TMP_Settings.warningsDisabled)
				{
					Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + fontAsset.name + "].", this);
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001BD84 File Offset: 0x00019F84
		protected void ReplaceTagWithCharacter(int[] chars, int insertionIndex, int tagLength, char c)
		{
			chars[insertionIndex] = (int)c;
			for (int i = insertionIndex + tagLength; i < chars.Length; i++)
			{
				chars[i - 3] = chars[i];
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		protected TMP_FontAsset GetFontAssetForWeight(int fontWeight)
		{
			bool flag = (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic || (this.m_fontStyle & FontStyles.Italic) == FontStyles.Italic;
			int num = fontWeight / 100;
			TMP_FontAsset tmp_FontAsset;
			if (flag)
			{
				tmp_FontAsset = this.m_currentFontAsset.fontWeightTable[num].italicTypeface;
			}
			else
			{
				tmp_FontAsset = this.m_currentFontAsset.fontWeightTable[num].regularTypeface;
			}
			return tmp_FontAsset;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void SetActiveSubMeshes(bool state)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000027BA File Offset: 0x000009BA
		protected virtual void ClearSubMeshObjects()
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void ClearMesh()
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void ClearMesh(bool uploadGeometry)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001BE10 File Offset: 0x0001A010
		public virtual string GetParsedText()
		{
			if (this.m_textInfo == null)
			{
				return string.Empty;
			}
			int characterCount = this.m_textInfo.characterCount;
			char[] array = new char[characterCount];
			int num = 0;
			while (num < characterCount && num < this.m_textInfo.characterInfo.Length)
			{
				array[num] = this.m_textInfo.characterInfo[num].character;
				num++;
			}
			return new string(array);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001BE79 File Offset: 0x0001A079
		internal bool IsSelfOrLinkedAncestor(TMP_Text targetTextComponent)
		{
			return targetTextComponent == null || (this.parentLinkedComponent != null && this.parentLinkedComponent.IsSelfOrLinkedAncestor(targetTextComponent)) || base.GetInstanceID() == targetTextComponent.GetInstanceID();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		internal void ReleaseLinkedTextComponent(TMP_Text targetTextComponent)
		{
			if (targetTextComponent == null)
			{
				return;
			}
			TMP_Text linkedTextComponent = targetTextComponent.linkedTextComponent;
			if (linkedTextComponent != null)
			{
				this.ReleaseLinkedTextComponent(linkedTextComponent);
			}
			targetTextComponent.text = string.Empty;
			targetTextComponent.firstVisibleCharacter = 0;
			targetTextComponent.linkedTextComponent = null;
			targetTextComponent.parentLinkedComponent = null;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001BF08 File Offset: 0x0001A108
		protected Vector2 PackUV(float x, float y, float scale)
		{
			Vector2 vector;
			vector.x = (float)((int)(x * 511f));
			vector.y = (float)((int)(y * 511f));
			vector.x = vector.x * 4096f + vector.y;
			vector.y = scale;
			return vector;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001BF58 File Offset: 0x0001A158
		protected float PackUV(float x, float y)
		{
			float num = (float)((double)((int)(x * 511f)));
			double num2 = (double)((int)(y * 511f));
			return (float)((double)num * 4096.0 + num2);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000027BA File Offset: 0x000009BA
		internal virtual void InternalUpdate()
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001BF88 File Offset: 0x0001A188
		protected int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001C058 File Offset: 0x0001A258
		protected int GetUTF16(string text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 12) + (this.HexToInt(text[i + 1]) << 8) + (this.HexToInt(text[i + 2]) << 4) + this.HexToInt(text[i + 3]);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001C0AB File Offset: 0x0001A2AB
		protected int GetUTF16(int[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		protected int GetUTF16(StringBuilder text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 12) + (this.HexToInt(text[i + 1]) << 8) + (this.HexToInt(text[i + 2]) << 4) + this.HexToInt(text[i + 3]);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001C13C File Offset: 0x0001A33C
		protected int GetUTF32(string text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 28) + (this.HexToInt(text[i + 1]) << 24) + (this.HexToInt(text[i + 2]) << 20) + (this.HexToInt(text[i + 3]) << 16) + (this.HexToInt(text[i + 4]) << 12) + (this.HexToInt(text[i + 5]) << 8) + (this.HexToInt(text[i + 6]) << 4) + this.HexToInt(text[i + 7]);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001C1DC File Offset: 0x0001A3DC
		protected int GetUTF32(int[] text, int i)
		{
			return 0 + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001C264 File Offset: 0x0001A464
		protected int GetUTF32(StringBuilder text, int i)
		{
			return 0 + (this.HexToInt(text[i]) << 28) + (this.HexToInt(text[i + 1]) << 24) + (this.HexToInt(text[i + 2]) << 20) + (this.HexToInt(text[i + 3]) << 16) + (this.HexToInt(text[i + 4]) << 12) + (this.HexToInt(text[i + 5]) << 8) + (this.HexToInt(text[i + 6]) << 4) + this.HexToInt(text[i + 7]);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001C304 File Offset: 0x0001A504
		protected Color32 HexCharsToColor(char[] hexChars, int tagCount)
		{
			if (tagCount == 4)
			{
				byte b = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[1]));
				byte b2 = (byte)(this.HexToInt(hexChars[2]) * 16 + this.HexToInt(hexChars[2]));
				byte b3 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[3]));
				return new Color32(b, b2, b3, byte.MaxValue);
			}
			if (tagCount == 5)
			{
				byte b4 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[1]));
				byte b5 = (byte)(this.HexToInt(hexChars[2]) * 16 + this.HexToInt(hexChars[2]));
				byte b6 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[3]));
				byte b7 = (byte)(this.HexToInt(hexChars[4]) * 16 + this.HexToInt(hexChars[4]));
				return new Color32(b4, b5, b6, b7);
			}
			if (tagCount == 7)
			{
				byte b8 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[2]));
				byte b9 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[4]));
				byte b10 = (byte)(this.HexToInt(hexChars[5]) * 16 + this.HexToInt(hexChars[6]));
				return new Color32(b8, b9, b10, byte.MaxValue);
			}
			if (tagCount == 9)
			{
				byte b11 = (byte)(this.HexToInt(hexChars[1]) * 16 + this.HexToInt(hexChars[2]));
				byte b12 = (byte)(this.HexToInt(hexChars[3]) * 16 + this.HexToInt(hexChars[4]));
				byte b13 = (byte)(this.HexToInt(hexChars[5]) * 16 + this.HexToInt(hexChars[6]));
				byte b14 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				return new Color32(b11, b12, b13, b14);
			}
			if (tagCount == 10)
			{
				byte b15 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[7]));
				byte b16 = (byte)(this.HexToInt(hexChars[8]) * 16 + this.HexToInt(hexChars[8]));
				byte b17 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[9]));
				return new Color32(b15, b16, b17, byte.MaxValue);
			}
			if (tagCount == 11)
			{
				byte b18 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[7]));
				byte b19 = (byte)(this.HexToInt(hexChars[8]) * 16 + this.HexToInt(hexChars[8]));
				byte b20 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[9]));
				byte b21 = (byte)(this.HexToInt(hexChars[10]) * 16 + this.HexToInt(hexChars[10]));
				return new Color32(b18, b19, b20, b21);
			}
			if (tagCount == 13)
			{
				byte b22 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				byte b23 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[10]));
				byte b24 = (byte)(this.HexToInt(hexChars[11]) * 16 + this.HexToInt(hexChars[12]));
				return new Color32(b22, b23, b24, byte.MaxValue);
			}
			if (tagCount == 15)
			{
				byte b25 = (byte)(this.HexToInt(hexChars[7]) * 16 + this.HexToInt(hexChars[8]));
				byte b26 = (byte)(this.HexToInt(hexChars[9]) * 16 + this.HexToInt(hexChars[10]));
				byte b27 = (byte)(this.HexToInt(hexChars[11]) * 16 + this.HexToInt(hexChars[12]));
				byte b28 = (byte)(this.HexToInt(hexChars[13]) * 16 + this.HexToInt(hexChars[14]));
				return new Color32(b25, b26, b27, b28);
			}
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001C670 File Offset: 0x0001A870
		protected Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
		{
			if (length == 7)
			{
				byte b = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16 + this.HexToInt(hexChars[startIndex + 2]));
				byte b2 = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16 + this.HexToInt(hexChars[startIndex + 4]));
				byte b3 = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16 + this.HexToInt(hexChars[startIndex + 6]));
				return new Color32(b, b2, b3, byte.MaxValue);
			}
			if (length == 9)
			{
				byte b4 = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16 + this.HexToInt(hexChars[startIndex + 2]));
				byte b5 = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16 + this.HexToInt(hexChars[startIndex + 4]));
				byte b6 = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16 + this.HexToInt(hexChars[startIndex + 6]));
				byte b7 = (byte)(this.HexToInt(hexChars[startIndex + 7]) * 16 + this.HexToInt(hexChars[startIndex + 8]));
				return new Color32(b4, b5, b6, b7);
			}
			return TMP_Text.s_colorWhite;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001C768 File Offset: 0x0001A968
		private int GetAttributeParameters(char[] chars, int startIndex, int length, ref float[] parameters)
		{
			int i = startIndex;
			int num = 0;
			while (i < startIndex + length)
			{
				parameters[num] = this.ConvertToFloat(chars, startIndex, length, out i);
				length -= i - startIndex + 1;
				startIndex = i + 1;
				num++;
			}
			return num;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		protected float ConvertToFloat(char[] chars, int startIndex, int length)
		{
			int num;
			return this.ConvertToFloat(chars, startIndex, length, out num);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		protected float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
		{
			if (startIndex == 0)
			{
				lastIndex = 0;
				return -32768f;
			}
			int num = startIndex + length;
			bool flag = true;
			float num2 = 0f;
			int num3 = 1;
			if (chars[startIndex] == '+')
			{
				num3 = 1;
				startIndex++;
			}
			else if (chars[startIndex] == '-')
			{
				num3 = -1;
				startIndex++;
			}
			float num4 = 0f;
			for (int i = startIndex; i < num; i++)
			{
				uint num5 = (uint)chars[i];
				if ((num5 >= 48U && num5 <= 57U) || num5 == 46U)
				{
					if (num5 == 46U)
					{
						flag = false;
						num2 = 0.1f;
					}
					else if (flag)
					{
						num4 = num4 * 10f + (float)((ulong)(num5 - 48U) * (ulong)((long)num3));
					}
					else
					{
						num4 += (num5 - 48U) * num2 * (float)num3;
						num2 *= 0.1f;
					}
				}
				else if (num5 == 44U)
				{
					if (i + 1 < num && chars[i + 1] == ' ')
					{
						lastIndex = i + 1;
					}
					else
					{
						lastIndex = i;
					}
					if (num4 > 32767f)
					{
						return -32768f;
					}
					return num4;
				}
			}
			lastIndex = num;
			if (num4 > 32767f)
			{
				return -32768f;
			}
			return num4;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		protected bool ValidateHtmlTag(TMP_Text.UnicodeChar[] chars, int startIndex, out int endIndex)
		{
			int num = 0;
			byte b = 0;
			int num2 = 0;
			this.m_xmlAttribute[num2].nameHashCode = 0;
			this.m_xmlAttribute[num2].valueHashCode = 0;
			this.m_xmlAttribute[num2].valueStartIndex = 0;
			this.m_xmlAttribute[num2].valueLength = 0;
			TagValueType tagValueType = (this.m_xmlAttribute[num2].valueType = TagValueType.None);
			TagUnitType tagUnitType = (this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels);
			this.m_xmlAttribute[1].nameHashCode = 0;
			this.m_xmlAttribute[2].nameHashCode = 0;
			this.m_xmlAttribute[3].nameHashCode = 0;
			this.m_xmlAttribute[4].nameHashCode = 0;
			endIndex = startIndex;
			bool flag = false;
			bool flag2 = false;
			int num3 = startIndex;
			while (num3 < chars.Length && chars[num3].unicode != 0 && num < this.m_htmlTag.Length && chars[num3].unicode != 60)
			{
				int unicode = chars[num3].unicode;
				if (unicode == 62)
				{
					flag2 = true;
					endIndex = num3;
					this.m_htmlTag[num] = '\0';
					break;
				}
				this.m_htmlTag[num] = (char)unicode;
				num++;
				if (b == 1)
				{
					if (tagValueType == TagValueType.None)
					{
						if (unicode == 43 || unicode == 45 || unicode == 46 || (unicode >= 48 && unicode <= 57))
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (this.m_xmlAttribute[num2].valueType = TagValueType.NumericalValue);
							this.m_xmlAttribute[num2].valueStartIndex = num - 1;
							RichTextTagAttribute[] xmlAttribute = this.m_xmlAttribute;
							int num4 = num2;
							xmlAttribute[num4].valueLength = xmlAttribute[num4].valueLength + 1;
						}
						else if (unicode == 35)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (this.m_xmlAttribute[num2].valueType = TagValueType.ColorValue);
							this.m_xmlAttribute[num2].valueStartIndex = num - 1;
							RichTextTagAttribute[] xmlAttribute2 = this.m_xmlAttribute;
							int num5 = num2;
							xmlAttribute2[num5].valueLength = xmlAttribute2[num5].valueLength + 1;
						}
						else if (unicode == 34)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (this.m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							this.m_xmlAttribute[num2].valueStartIndex = num;
						}
						else
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (this.m_xmlAttribute[num2].valueType = TagValueType.StringValue);
							this.m_xmlAttribute[num2].valueStartIndex = num - 1;
							this.m_xmlAttribute[num2].valueHashCode = ((this.m_xmlAttribute[num2].valueHashCode << 5) + this.m_xmlAttribute[num2].valueHashCode) ^ unicode;
							RichTextTagAttribute[] xmlAttribute3 = this.m_xmlAttribute;
							int num6 = num2;
							xmlAttribute3[num6].valueLength = xmlAttribute3[num6].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.NumericalValue)
					{
						if (unicode == 112 || unicode == 101 || unicode == 37 || unicode == 32)
						{
							b = 2;
							tagValueType = TagValueType.None;
							if (unicode != 37)
							{
								if (unicode == 101)
								{
									tagUnitType = (this.m_xmlAttribute[num2].unitType = TagUnitType.FontUnits);
								}
								else
								{
									tagUnitType = (this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels);
								}
							}
							else
							{
								tagUnitType = (this.m_xmlAttribute[num2].unitType = TagUnitType.Percentage);
							}
							num2++;
							this.m_xmlAttribute[num2].nameHashCode = 0;
							this.m_xmlAttribute[num2].valueHashCode = 0;
							this.m_xmlAttribute[num2].valueType = TagValueType.None;
							this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							this.m_xmlAttribute[num2].valueStartIndex = 0;
							this.m_xmlAttribute[num2].valueLength = 0;
						}
						else if (b != 2)
						{
							RichTextTagAttribute[] xmlAttribute4 = this.m_xmlAttribute;
							int num7 = num2;
							xmlAttribute4[num7].valueLength = xmlAttribute4[num7].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.ColorValue)
					{
						if (unicode != 32)
						{
							RichTextTagAttribute[] xmlAttribute5 = this.m_xmlAttribute;
							int num8 = num2;
							xmlAttribute5[num8].valueLength = xmlAttribute5[num8].valueLength + 1;
						}
						else
						{
							b = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							num2++;
							this.m_xmlAttribute[num2].nameHashCode = 0;
							this.m_xmlAttribute[num2].valueType = TagValueType.None;
							this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							this.m_xmlAttribute[num2].valueHashCode = 0;
							this.m_xmlAttribute[num2].valueStartIndex = 0;
							this.m_xmlAttribute[num2].valueLength = 0;
						}
					}
					else if (tagValueType == TagValueType.StringValue)
					{
						if (unicode != 34)
						{
							this.m_xmlAttribute[num2].valueHashCode = ((this.m_xmlAttribute[num2].valueHashCode << 5) + this.m_xmlAttribute[num2].valueHashCode) ^ unicode;
							RichTextTagAttribute[] xmlAttribute6 = this.m_xmlAttribute;
							int num9 = num2;
							xmlAttribute6[num9].valueLength = xmlAttribute6[num9].valueLength + 1;
						}
						else
						{
							b = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							num2++;
							this.m_xmlAttribute[num2].nameHashCode = 0;
							this.m_xmlAttribute[num2].valueType = TagValueType.None;
							this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
							this.m_xmlAttribute[num2].valueHashCode = 0;
							this.m_xmlAttribute[num2].valueStartIndex = 0;
							this.m_xmlAttribute[num2].valueLength = 0;
						}
					}
				}
				if (unicode == 61)
				{
					b = 1;
				}
				if (b == 0 && unicode == 32)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					this.m_xmlAttribute[num2].nameHashCode = 0;
					this.m_xmlAttribute[num2].valueType = TagValueType.None;
					this.m_xmlAttribute[num2].unitType = TagUnitType.Pixels;
					this.m_xmlAttribute[num2].valueHashCode = 0;
					this.m_xmlAttribute[num2].valueStartIndex = 0;
					this.m_xmlAttribute[num2].valueLength = 0;
				}
				if (b == 0)
				{
					this.m_xmlAttribute[num2].nameHashCode = (this.m_xmlAttribute[num2].nameHashCode << 3) - this.m_xmlAttribute[num2].nameHashCode + unicode;
				}
				if (b == 2 && unicode == 32)
				{
					b = 0;
				}
				num3++;
			}
			if (!flag2)
			{
				return false;
			}
			if (this.tag_NoParsing && this.m_xmlAttribute[0].nameHashCode != 53822163 && this.m_xmlAttribute[0].nameHashCode != 49429939)
			{
				return false;
			}
			if (this.m_xmlAttribute[0].nameHashCode == 53822163 || this.m_xmlAttribute[0].nameHashCode == 49429939)
			{
				this.tag_NoParsing = false;
				return true;
			}
			if (this.m_htmlTag[0] == '#' && num == 4)
			{
				this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (this.m_htmlTag[0] == '#' && num == 5)
			{
				this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (this.m_htmlTag[0] == '#' && num == 7)
			{
				this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (this.m_htmlTag[0] == '#' && num == 9)
			{
				this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			int num10 = this.m_xmlAttribute[0].nameHashCode;
			float num11;
			if (num10 <= 186622)
			{
				if (num10 <= 2963)
				{
					if (num10 > 98)
					{
						if (num10 <= 434)
						{
							if (num10 <= 402)
							{
								if (num10 <= 115)
								{
									if (num10 == 105)
									{
										goto IL_130E;
									}
									if (num10 != 115)
									{
										return false;
									}
									goto IL_1409;
								}
								else
								{
									if (num10 == 117)
									{
										goto IL_1513;
									}
									if (num10 != 395)
									{
										if (num10 != 402)
										{
											return false;
										}
										goto IL_13CE;
									}
								}
							}
							else if (num10 <= 414)
							{
								if (num10 == 412)
								{
									goto IL_14E6;
								}
								if (num10 != 414)
								{
									return false;
								}
								goto IL_15EE;
							}
							else
							{
								if (num10 == 426)
								{
									return true;
								}
								if (num10 != 427)
								{
									if (num10 != 434)
									{
										return false;
									}
									goto IL_13CE;
								}
							}
							if ((this.m_fontStyle & FontStyles.Bold) != FontStyles.Bold && this.m_fontStyleStack.Remove(FontStyles.Bold) == 0)
							{
								this.m_FontStyleInternal &= (FontStyles)(-2);
								this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
							}
							return true;
							IL_13CE:
							if ((this.m_fontStyle & FontStyles.Italic) != FontStyles.Italic)
							{
								this.m_ItalicAngle = this.m_ItalicAngleStack.Remove();
								if (this.m_fontStyleStack.Remove(FontStyles.Italic) == 0)
								{
									this.m_FontStyleInternal &= (FontStyles)(-3);
								}
							}
							return true;
						}
						if (num10 <= 670)
						{
							if (num10 <= 446)
							{
								if (num10 == 444)
								{
									goto IL_14E6;
								}
								if (num10 != 446)
								{
									return false;
								}
								goto IL_15EE;
							}
							else
							{
								if (num10 == 656)
								{
									return false;
								}
								if (num10 == 660)
								{
									return true;
								}
								if (num10 != 670)
								{
									return false;
								}
							}
						}
						else if (num10 <= 916)
						{
							if (num10 == 912)
							{
								return false;
							}
							if (num10 != 916)
							{
								return false;
							}
							return true;
						}
						else if (num10 != 926)
						{
							if (num10 == 2959)
							{
								return false;
							}
							if (num10 != 2963)
							{
								return false;
							}
							return true;
						}
						return true;
						IL_14E6:
						if ((this.m_fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough && this.m_fontStyleStack.Remove(FontStyles.Strikethrough) == 0)
						{
							this.m_FontStyleInternal &= (FontStyles)(-65);
						}
						return true;
						IL_15EE:
						if ((this.m_fontStyle & FontStyles.Underline) != FontStyles.Underline)
						{
							this.m_underlineColor = this.m_underlineColorStack.Remove();
							if (this.m_fontStyleStack.Remove(FontStyles.Underline) == 0)
							{
								this.m_FontStyleInternal &= (FontStyles)(-5);
							}
						}
						return true;
					}
					if (num10 <= -855002522)
					{
						if (num10 <= -1690034531)
						{
							if (num10 <= -1883544150)
							{
								if (num10 == -1885698441)
								{
									goto IL_1C4B;
								}
								if (num10 != -1883544150)
								{
									return false;
								}
							}
							else
							{
								if (num10 == -1847322671)
								{
									goto IL_3780;
								}
								if (num10 == -1831660941)
								{
									goto IL_3734;
								}
								if (num10 != -1690034531)
								{
									return false;
								}
								goto IL_3C1F;
							}
						}
						else if (num10 <= -1632103439)
						{
							if (num10 != -1668324918)
							{
								if (num10 != -1632103439)
								{
									return false;
								}
								goto IL_3780;
							}
						}
						else
						{
							if (num10 == -1616441709)
							{
								goto IL_3734;
							}
							if (num10 == -884817987)
							{
								goto IL_3C1F;
							}
							if (num10 != -855002522)
							{
								return false;
							}
							goto IL_3B38;
						}
						if ((this.m_fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase && this.m_fontStyleStack.Remove(FontStyles.LowerCase) == 0)
						{
							this.m_FontStyleInternal &= (FontStyles)(-9);
						}
						return true;
						IL_3780:
						if ((this.m_fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps && this.m_fontStyleStack.Remove(FontStyles.SmallCaps) == 0)
						{
							this.m_FontStyleInternal &= (FontStyles)(-33);
						}
						return true;
						IL_3C1F:
						num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
						if (num11 == -32768f)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_marginRight = num11 * (this.m_isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							this.m_marginRight = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
							break;
						case TagUnitType.Percentage:
							this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num11 / 100f;
							break;
						}
						this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
						return true;
					}
					else
					{
						if (num10 > -330774850)
						{
							if (num10 <= 73)
							{
								if (num10 != 66)
								{
									if (num10 != 73)
									{
										return false;
									}
									goto IL_130E;
								}
							}
							else
							{
								if (num10 == 83)
								{
									goto IL_1409;
								}
								if (num10 == 85)
								{
									goto IL_1513;
								}
								if (num10 != 98)
								{
									return false;
								}
							}
							this.m_FontStyleInternal |= FontStyles.Bold;
							this.m_fontStyleStack.Add(FontStyles.Bold);
							this.m_FontWeightInternal = FontWeight.Bold;
							return true;
						}
						if (num10 <= -842656867)
						{
							if (num10 == -842693512)
							{
								goto IL_3D06;
							}
							if (num10 != -842656867)
							{
								return false;
							}
							goto IL_3110;
						}
						else
						{
							if (num10 == -445573839)
							{
								goto IL_3DC7;
							}
							if (num10 == -445537194)
							{
								goto IL_31CF;
							}
							if (num10 != -330774850)
							{
								return false;
							}
							goto IL_1B0E;
						}
					}
					IL_130E:
					this.m_FontStyleInternal |= FontStyles.Italic;
					this.m_fontStyleStack.Add(FontStyles.Italic);
					if (this.m_xmlAttribute[1].nameHashCode == 276531 || this.m_xmlAttribute[1].nameHashCode == 186899)
					{
						this.m_ItalicAngle = (int)this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[1].valueStartIndex, this.m_xmlAttribute[1].valueLength);
						if (this.m_ItalicAngle < -180 || this.m_ItalicAngle > 180)
						{
							return false;
						}
					}
					else
					{
						this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
					}
					this.m_ItalicAngleStack.Add(this.m_ItalicAngle);
					return true;
					IL_1409:
					this.m_FontStyleInternal |= FontStyles.Strikethrough;
					this.m_fontStyleStack.Add(FontStyles.Strikethrough);
					if (this.m_xmlAttribute[1].nameHashCode == 281955 || this.m_xmlAttribute[1].nameHashCode == 192323)
					{
						this.m_strikethroughColor = this.HexCharsToColor(this.m_htmlTag, this.m_xmlAttribute[1].valueStartIndex, this.m_xmlAttribute[1].valueLength);
						this.m_strikethroughColor.a = ((this.m_htmlColor.a < this.m_strikethroughColor.a) ? this.m_htmlColor.a : this.m_strikethroughColor.a);
					}
					else
					{
						this.m_strikethroughColor = this.m_htmlColor;
					}
					this.m_strikethroughColorStack.Add(this.m_strikethroughColor);
					return true;
					IL_1513:
					this.m_FontStyleInternal |= FontStyles.Underline;
					this.m_fontStyleStack.Add(FontStyles.Underline);
					if (this.m_xmlAttribute[1].nameHashCode == 281955 || this.m_xmlAttribute[1].nameHashCode == 192323)
					{
						this.m_underlineColor = this.HexCharsToColor(this.m_htmlTag, this.m_xmlAttribute[1].valueStartIndex, this.m_xmlAttribute[1].valueLength);
						this.m_underlineColor.a = ((this.m_htmlColor.a < this.m_underlineColor.a) ? this.m_htmlColor.a : this.m_underlineColor.a);
					}
					else
					{
						this.m_underlineColor = this.m_htmlColor;
					}
					this.m_underlineColorStack.Add(this.m_underlineColor);
					return true;
				}
				if (num10 > 31169)
				{
					if (num10 > 143092)
					{
						if (num10 <= 155892)
						{
							if (num10 <= 144016)
							{
								if (num10 == 143113)
								{
									goto IL_2890;
								}
								if (num10 != 144016)
								{
									return false;
								}
							}
							else
							{
								if (num10 == 145592)
								{
									goto IL_20C5;
								}
								if (num10 == 154158)
								{
									goto IL_246D;
								}
								if (num10 != 155892)
								{
									return false;
								}
								goto IL_183F;
							}
						}
						else if (num10 <= 156816)
						{
							if (num10 == 155913)
							{
								goto IL_2890;
							}
							if (num10 != 156816)
							{
								return false;
							}
						}
						else
						{
							if (num10 == 158392)
							{
								goto IL_20C5;
							}
							if (num10 == 186285)
							{
								goto IL_2916;
							}
							if (num10 != 186622)
							{
								return false;
							}
							goto IL_270E;
						}
						this.m_isNonBreakingSpace = false;
						return true;
						IL_20C5:
						this.m_currentFontSize = this.m_sizeStack.Remove();
						this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
						IL_2890:
						if (this.m_isParsingText && !this.m_isCalculatingPreferredValues && this.m_textInfo.linkCount < this.m_textInfo.linkInfo.Length)
						{
							this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextLength = this.m_characterCount - this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextfirstCharacterIndex;
							this.m_textInfo.linkCount++;
						}
						return true;
					}
					if (num10 <= 43066)
					{
						if (num10 <= 32745)
						{
							if (num10 != 31191)
							{
								if (num10 != 32745)
								{
									return false;
								}
								goto IL_1E33;
							}
						}
						else
						{
							if (num10 == 41311)
							{
								goto IL_2124;
							}
							if (num10 == 43045)
							{
								goto IL_1629;
							}
							if (num10 != 43066)
							{
								return false;
							}
							goto IL_2756;
						}
					}
					else if (num10 <= 43991)
					{
						if (num10 == 43969)
						{
							goto IL_1E21;
						}
						if (num10 != 43991)
						{
							return false;
						}
					}
					else
					{
						if (num10 == 45545)
						{
							goto IL_1E33;
						}
						if (num10 == 141358)
						{
							goto IL_246D;
						}
						if (num10 != 143092)
						{
							return false;
						}
						goto IL_183F;
					}
					if (this.m_overflowMode == TextOverflowModes.Page)
					{
						this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
						this.m_lineOffset = 0f;
						this.m_pageNumber++;
						this.m_isNewPage = true;
					}
					return true;
					IL_1E33:
					num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
					if (num11 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						if (this.m_htmlTag[5] == '+')
						{
							this.m_currentFontSize = this.m_fontSize + num11;
							this.m_sizeStack.Add(this.m_currentFontSize);
							this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							return true;
						}
						if (this.m_htmlTag[5] == '-')
						{
							this.m_currentFontSize = this.m_fontSize + num11;
							this.m_sizeStack.Add(this.m_currentFontSize);
							this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							return true;
						}
						this.m_currentFontSize = num11;
						this.m_sizeStack.Add(this.m_currentFontSize);
						this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
					case TagUnitType.FontUnits:
						this.m_currentFontSize = this.m_fontSize * num11;
						this.m_sizeStack.Add(this.m_currentFontSize);
						this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
					case TagUnitType.Percentage:
						this.m_currentFontSize = this.m_fontSize * num11 / 100f;
						this.m_sizeStack.Add(this.m_currentFontSize);
						this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
					default:
						return false;
					}
					IL_183F:
					if ((this.m_fontStyle & FontStyles.Highlight) != FontStyles.Highlight)
					{
						this.m_HighlightStateStack.Remove();
						if (this.m_fontStyleStack.Remove(FontStyles.Highlight) == 0)
						{
							this.m_FontStyleInternal &= (FontStyles)(-513);
						}
					}
					return true;
					IL_246D:
					MaterialReference materialReference = this.m_materialReferenceStack.Remove();
					this.m_currentFontAsset = materialReference.fontAsset;
					this.m_currentMaterial = materialReference.material;
					this.m_currentMaterialIndex = materialReference.index;
					this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				}
				if (num10 > 6566)
				{
					if (num10 <= 22673)
					{
						if (num10 <= 20849)
						{
							if (num10 == 20677)
							{
								goto IL_1D2E;
							}
							if (num10 != 20849)
							{
								return false;
							}
						}
						else
						{
							if (num10 == 20863)
							{
								goto IL_1A71;
							}
							if (num10 == 22501)
							{
								goto IL_1D2E;
							}
							if (num10 != 22673)
							{
								return false;
							}
						}
						if ((this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript)
						{
							if (this.m_fontScaleMultiplier < 1f)
							{
								this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
								this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
							}
							if (this.m_fontStyleStack.Remove(FontStyles.Subscript) == 0)
							{
								this.m_FontStyleInternal &= (FontStyles)(-257);
							}
						}
						return true;
						IL_1D2E:
						this.m_isIgnoringAlignment = false;
						return true;
					}
					if (num10 <= 28511)
					{
						if (num10 != 22687)
						{
							if (num10 != 28511)
							{
								return false;
							}
							goto IL_2124;
						}
					}
					else
					{
						if (num10 == 30245)
						{
							goto IL_1629;
						}
						if (num10 == 30266)
						{
							goto IL_2756;
						}
						if (num10 != 31169)
						{
							return false;
						}
						goto IL_1E21;
					}
					IL_1A71:
					if ((this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
					{
						if (this.m_fontScaleMultiplier < 1f)
						{
							this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
							this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
						}
						if (this.m_fontStyleStack.Remove(FontStyles.Superscript) == 0)
						{
							this.m_FontStyleInternal &= (FontStyles)(-129);
						}
					}
					return true;
				}
				if (num10 <= 4556)
				{
					if (num10 <= 3215)
					{
						if (num10 != 2973)
						{
							if (num10 != 3215)
							{
								return false;
							}
							return false;
						}
					}
					else
					{
						if (num10 == 3219)
						{
							return true;
						}
						if (num10 != 3229)
						{
							if (num10 != 4556)
							{
								return false;
							}
							goto IL_1C80;
						}
					}
					return true;
				}
				if (num10 <= 4742)
				{
					if (num10 != 4728)
					{
						if (num10 != 4742)
						{
							return false;
						}
						goto IL_19C9;
					}
				}
				else
				{
					if (num10 == 6380)
					{
						goto IL_1C80;
					}
					if (num10 != 6552)
					{
						if (num10 != 6566)
						{
							return false;
						}
						goto IL_19C9;
					}
				}
				this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
				this.m_baselineOffsetStack.Push(this.m_baselineOffset);
				this.m_baselineOffset += this.m_currentFontAsset.faceInfo.subscriptOffset * this.m_fontScale * this.m_fontScaleMultiplier;
				this.m_fontStyleStack.Add(FontStyles.Subscript);
				this.m_FontStyleInternal |= FontStyles.Subscript;
				return true;
				IL_19C9:
				this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
				this.m_baselineOffsetStack.Push(this.m_baselineOffset);
				this.m_baselineOffset += this.m_currentFontAsset.faceInfo.superscriptOffset * this.m_fontScale * this.m_fontScaleMultiplier;
				this.m_fontStyleStack.Add(FontStyles.Superscript);
				this.m_FontStyleInternal |= FontStyles.Superscript;
				return true;
				IL_1C80:
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_xAdvance = num11 * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.FontUnits:
					this.m_xAdvance = num11 * this.m_currentFontSize * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.Percentage:
					this.m_xAdvance = this.m_marginWidth * num11 / 100f;
					return true;
				default:
					return false;
				}
				IL_1629:
				this.m_FontStyleInternal |= FontStyles.Highlight;
				this.m_fontStyleStack.Add(FontStyles.Highlight);
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
				TMP_Offset tmp_Offset = TMP_Offset.zero;
				int num12 = 0;
				while (num12 < this.m_xmlAttribute.Length && this.m_xmlAttribute[num12].nameHashCode != 0)
				{
					int nameHashCode = this.m_xmlAttribute[num12].nameHashCode;
					if (nameHashCode <= 43045)
					{
						if (nameHashCode == 30245 || nameHashCode == 43045)
						{
							if (this.m_xmlAttribute[num12].valueType == TagValueType.ColorValue)
							{
								color = this.HexCharsToColor(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
							}
						}
					}
					else if (nameHashCode != 281955)
					{
						if (nameHashCode == 15087385)
						{
							if (this.GetAttributeParameters(this.m_htmlTag, this.m_xmlAttribute[num12].valueStartIndex, this.m_xmlAttribute[num12].valueLength, ref this.m_attributeParameterValues) != 4)
							{
								return false;
							}
							tmp_Offset = new TMP_Offset(this.m_attributeParameterValues[0], this.m_attributeParameterValues[1], this.m_attributeParameterValues[2], this.m_attributeParameterValues[3]);
							tmp_Offset *= this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
						}
					}
					else
					{
						color = this.HexCharsToColor(this.m_htmlTag, this.m_xmlAttribute[num12].valueStartIndex, this.m_xmlAttribute[num12].valueLength);
					}
					num12++;
				}
				color.a = ((this.m_htmlColor.a < color.a) ? this.m_htmlColor.a : color.a);
				HighlightState highlightState = new HighlightState(color, tmp_Offset);
				this.m_HighlightStateStack.Push(highlightState);
				return true;
				IL_1E21:
				this.m_isNonBreakingSpace = true;
				return true;
				IL_2124:
				int valueHashCode = this.m_xmlAttribute[0].valueHashCode;
				int nameHashCode2 = this.m_xmlAttribute[1].nameHashCode;
				int num13 = this.m_xmlAttribute[1].valueHashCode;
				if (valueHashCode == 764638571 || valueHashCode == 523367755)
				{
					this.m_currentFontAsset = this.m_materialReferences[0].fontAsset;
					this.m_currentMaterial = this.m_materialReferences[0].material;
					this.m_currentMaterialIndex = 0;
					this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
					this.m_materialReferenceStack.Add(this.m_materialReferences[0]);
					return true;
				}
				TMP_FontAsset tmp_FontAsset;
				MaterialReferenceManager.TryGetFontAsset(valueHashCode, out tmp_FontAsset);
				if (tmp_FontAsset == null)
				{
					if (TMP_Text.onFontAssetRequest != null)
					{
						tmp_FontAsset = TMP_Text.onFontAssetRequest(valueHashCode, new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
					}
					if (tmp_FontAsset == null)
					{
						tmp_FontAsset = Resources.Load<TMP_FontAsset>(TMP_Settings.defaultFontAssetPath + new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
					}
					if (tmp_FontAsset == null)
					{
						return false;
					}
					MaterialReferenceManager.AddFontAsset(tmp_FontAsset);
				}
				if (nameHashCode2 == 0 && num13 == 0)
				{
					this.m_currentMaterial = tmp_FontAsset.material;
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
					this.m_materialReferenceStack.Add(this.m_materialReferences[this.m_currentMaterialIndex]);
				}
				else
				{
					if (nameHashCode2 != 103415287 && nameHashCode2 != 72669687)
					{
						return false;
					}
					Material material;
					if (MaterialReferenceManager.TryGetMaterial(num13, out material))
					{
						this.m_currentMaterial = material;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
						this.m_materialReferenceStack.Add(this.m_materialReferences[this.m_currentMaterialIndex]);
					}
					else
					{
						material = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(this.m_htmlTag, this.m_xmlAttribute[1].valueStartIndex, this.m_xmlAttribute[1].valueLength));
						if (material == null)
						{
							return false;
						}
						MaterialReferenceManager.AddFontMaterial(num13, material);
						this.m_currentMaterial = material;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tmp_FontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
						this.m_materialReferenceStack.Add(this.m_materialReferences[this.m_currentMaterialIndex]);
					}
				}
				this.m_currentFontAsset = tmp_FontAsset;
				this.m_fontScale = this.m_currentFontSize / (float)this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
				return true;
				IL_2756:
				if (this.m_isParsingText && !this.m_isCalculatingPreferredValues)
				{
					int linkCount = this.m_textInfo.linkCount;
					if (linkCount + 1 > this.m_textInfo.linkInfo.Length)
					{
						TMP_TextInfo.Resize<TMP_LinkInfo>(ref this.m_textInfo.linkInfo, linkCount + 1);
					}
					this.m_textInfo.linkInfo[linkCount].textComponent = this;
					this.m_textInfo.linkInfo[linkCount].hashCode = this.m_xmlAttribute[0].valueHashCode;
					this.m_textInfo.linkInfo[linkCount].linkTextfirstCharacterIndex = this.m_characterCount;
					this.m_textInfo.linkInfo[linkCount].linkIdFirstCharacterIndex = startIndex + this.m_xmlAttribute[0].valueStartIndex;
					this.m_textInfo.linkInfo[linkCount].linkIdLength = this.m_xmlAttribute[0].valueLength;
					this.m_textInfo.linkInfo[linkCount].SetLinkID(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				}
				return true;
			}
			if (num10 <= 6886018)
			{
				if (num10 <= 1071884)
				{
					if (num10 > 315682)
					{
						if (num10 <= 982252)
						{
							if (num10 <= 320078)
							{
								if (num10 == 317446)
								{
									return false;
								}
								if (num10 != 320078)
								{
									return false;
								}
								goto IL_2667;
							}
							else
							{
								if (num10 == 327550)
								{
									goto IL_2A01;
								}
								if (num10 != 976214)
								{
									if (num10 != 982252)
									{
										return false;
									}
									goto IL_3021;
								}
							}
						}
						else if (num10 <= 1017743)
						{
							if (num10 == 1015979)
							{
								goto IL_3F02;
							}
							if (num10 != 1017743)
							{
								return false;
							}
							return true;
						}
						else
						{
							if (num10 == 1027847)
							{
								goto IL_2A89;
							}
							if (num10 != 1065846)
							{
								if (num10 != 1071884)
								{
									return false;
								}
								goto IL_3021;
							}
						}
						this.m_lineJustification = this.m_lineJustificationStack.Remove();
						return true;
						IL_3021:
						this.m_htmlColor = this.m_colorStack.Remove();
						return true;
					}
					if (num10 <= 237918)
					{
						if (num10 <= 226050)
						{
							if (num10 != 192323)
							{
								if (num10 != 226050)
								{
									return false;
								}
								goto IL_3E98;
							}
						}
						else
						{
							if (num10 == 227814)
							{
								return false;
							}
							if (num10 == 230446)
							{
								goto IL_2667;
							}
							if (num10 != 237918)
							{
								return false;
							}
							goto IL_2A01;
						}
					}
					else if (num10 <= 276254)
					{
						if (num10 == 275917)
						{
							goto IL_2916;
						}
						if (num10 != 276254)
						{
							return false;
						}
						goto IL_270E;
					}
					else
					{
						if (num10 == 280416)
						{
							return false;
						}
						if (num10 != 281955)
						{
							if (num10 != 315682)
							{
								return false;
							}
							goto IL_3E98;
						}
					}
					if (this.m_htmlTag[6] == '#' && num == 10)
					{
						this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (this.m_htmlTag[6] == '#' && num == 11)
					{
						this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (this.m_htmlTag[6] == '#' && num == 13)
					{
						this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					if (this.m_htmlTag[6] == '#' && num == 15)
					{
						this.m_htmlColor = this.HexCharsToColor(this.m_htmlTag, num);
						this.m_colorStack.Add(this.m_htmlColor);
						return true;
					}
					num10 = this.m_xmlAttribute[0].valueHashCode;
					if (num10 <= 26556144)
					{
						if (num10 <= 125395)
						{
							if (num10 == -36881330)
							{
								this.m_htmlColor = new Color32(160, 32, 240, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num10 == 125395)
							{
								this.m_htmlColor = Color.red;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
						else
						{
							if (num10 == 3573310)
							{
								this.m_htmlColor = Color.blue;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num10 == 26556144)
							{
								this.m_htmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
					}
					else if (num10 <= 121463835)
					{
						if (num10 == 117905991)
						{
							this.m_htmlColor = Color.black;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (num10 == 121463835)
						{
							this.m_htmlColor = Color.green;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
					}
					else
					{
						if (num10 == 140357351)
						{
							this.m_htmlColor = Color.white;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (num10 == 554054276)
						{
							this.m_htmlColor = Color.yellow;
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
					}
					return false;
					IL_3E98:
					num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
					if (num11 == -32768f)
					{
						return false;
					}
					this.m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(num11, 1f, 1f));
					this.m_isFXMatrixSet = true;
					return true;
					IL_2667:
					num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
					if (num11 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_xAdvance += num11 * (this.m_isOrthographic ? 1f : 0.1f);
						return true;
					case TagUnitType.FontUnits:
						this.m_xAdvance += num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
						return true;
					case TagUnitType.Percentage:
						return false;
					default:
						return false;
					}
					IL_2A01:
					num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
					if (num11 == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_width = num11 * (this.m_isOrthographic ? 1f : 0.1f);
						break;
					case TagUnitType.FontUnits:
						return false;
					case TagUnitType.Percentage:
						this.m_width = this.m_marginWidth * num11 / 100f;
						break;
					}
					return true;
				}
				if (num10 <= 1619421)
				{
					if (num10 <= 1356515)
					{
						if (num10 <= 1107375)
						{
							if (num10 == 1105611)
							{
								goto IL_3F02;
							}
							if (num10 != 1107375)
							{
								return false;
							}
							return true;
						}
						else
						{
							if (num10 == 1117479)
							{
								goto IL_2A89;
							}
							if (num10 == 1286342)
							{
								goto IL_3DDD;
							}
							if (num10 != 1356515)
							{
								return false;
							}
						}
					}
					else if (num10 <= 1482398)
					{
						if (num10 == 1441524)
						{
							goto IL_3034;
						}
						if (num10 != 1482398)
						{
							return false;
						}
						goto IL_37AD;
					}
					else
					{
						if (num10 == 1524585)
						{
							goto IL_2F79;
						}
						if (num10 == 1600507)
						{
							goto IL_3F0B;
						}
						if (num10 != 1619421)
						{
							return false;
						}
						goto IL_31DC;
					}
				}
				else if (num10 <= 2109854)
				{
					if (num10 <= 1913798)
					{
						if (num10 == 1750458)
						{
							return false;
						}
						if (num10 != 1913798)
						{
							return false;
						}
						goto IL_3DDD;
					}
					else if (num10 != 1983971)
					{
						if (num10 == 2068980)
						{
							goto IL_3034;
						}
						if (num10 != 2109854)
						{
							return false;
						}
						goto IL_37AD;
					}
				}
				else if (num10 <= 2227963)
				{
					if (num10 == 2152041)
					{
						goto IL_2F79;
					}
					if (num10 != 2227963)
					{
						return false;
					}
					goto IL_3F0B;
				}
				else
				{
					if (num10 == 2246877)
					{
						goto IL_31DC;
					}
					if (num10 == 6815845)
					{
						goto IL_3E3F;
					}
					if (num10 != 6886018)
					{
						return false;
					}
					goto IL_2F23;
				}
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_cSpacing = num11 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_cSpacing = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					return false;
				}
				return true;
				IL_2F79:
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_monoSpacing = num11 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_monoSpacing = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					return false;
				}
				return true;
				IL_3034:
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.tag_Indent = num11 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.tag_Indent = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					this.tag_Indent = this.m_marginWidth * num11 / 100f;
					break;
				}
				this.m_indentStack.Add(this.tag_Indent);
				this.m_xAdvance = this.tag_Indent;
				return true;
				IL_31DC:
				int valueHashCode2 = this.m_xmlAttribute[0].valueHashCode;
				this.m_spriteIndex = -1;
				TMP_SpriteAsset tmp_SpriteAsset;
				if (this.m_xmlAttribute[0].valueType == TagValueType.None || this.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
				{
					if (this.m_spriteAsset != null)
					{
						this.m_currentSpriteAsset = this.m_spriteAsset;
					}
					else if (this.m_defaultSpriteAsset != null)
					{
						this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
					}
					else if (this.m_defaultSpriteAsset == null)
					{
						if (TMP_Settings.defaultSpriteAsset != null)
						{
							this.m_defaultSpriteAsset = TMP_Settings.defaultSpriteAsset;
						}
						else
						{
							this.m_defaultSpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Default Sprite Asset");
						}
						this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
					}
					if (this.m_currentSpriteAsset == null)
					{
						return false;
					}
				}
				else if (MaterialReferenceManager.TryGetSpriteAsset(valueHashCode2, out tmp_SpriteAsset))
				{
					this.m_currentSpriteAsset = tmp_SpriteAsset;
				}
				else
				{
					if (tmp_SpriteAsset == null)
					{
						if (TMP_Text.onSpriteAssetRequest != null)
						{
							tmp_SpriteAsset = TMP_Text.onSpriteAssetRequest(valueHashCode2, new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
						}
						if (tmp_SpriteAsset == null)
						{
							tmp_SpriteAsset = Resources.Load<TMP_SpriteAsset>(TMP_Settings.defaultSpriteAssetPath + new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
						}
					}
					if (tmp_SpriteAsset == null)
					{
						return false;
					}
					MaterialReferenceManager.AddSpriteAsset(valueHashCode2, tmp_SpriteAsset);
					this.m_currentSpriteAsset = tmp_SpriteAsset;
				}
				if (this.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
				{
					int num14 = (int)this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
					if (num14 == -32768)
					{
						return false;
					}
					if (num14 > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
					{
						return false;
					}
					this.m_spriteIndex = num14;
				}
				this.m_spriteColor = TMP_Text.s_colorWhite;
				this.m_tintSprite = false;
				int num15 = 0;
				while (num15 < this.m_xmlAttribute.Length && this.m_xmlAttribute[num15].nameHashCode != 0)
				{
					int nameHashCode3 = this.m_xmlAttribute[num15].nameHashCode;
					int num16 = 0;
					if (nameHashCode3 <= 43347)
					{
						if (nameHashCode3 <= 30547)
						{
							if (nameHashCode3 == 26705)
							{
								goto IL_35D2;
							}
							if (nameHashCode3 != 30547)
							{
								goto IL_3655;
							}
						}
						else
						{
							if (nameHashCode3 == 33019)
							{
								goto IL_3552;
							}
							if (nameHashCode3 == 39505)
							{
								goto IL_35D2;
							}
							if (nameHashCode3 != 43347)
							{
								goto IL_3655;
							}
						}
						this.m_currentSpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCode(this.m_currentSpriteAsset, this.m_xmlAttribute[num15].valueHashCode, true, out num16);
						if (num16 == -1)
						{
							return false;
						}
						this.m_spriteIndex = num16;
						goto IL_3669;
						IL_35D2:
						if (this.GetAttributeParameters(this.m_htmlTag, this.m_xmlAttribute[num15].valueStartIndex, this.m_xmlAttribute[num15].valueLength, ref this.m_attributeParameterValues) != 3)
						{
							return false;
						}
						this.m_spriteIndex = (int)this.m_attributeParameterValues[0];
						if (this.m_isParsingText)
						{
							this.spriteAnimator.DoSpriteAnimation(this.m_characterCount, this.m_currentSpriteAsset, this.m_spriteIndex, (int)this.m_attributeParameterValues[1], (int)this.m_attributeParameterValues[2]);
						}
					}
					else
					{
						if (nameHashCode3 <= 192323)
						{
							if (nameHashCode3 == 45819)
							{
								goto IL_3552;
							}
							if (nameHashCode3 != 192323)
							{
								goto IL_3655;
							}
						}
						else
						{
							if (nameHashCode3 != 205930)
							{
								if (nameHashCode3 == 281955)
								{
									goto IL_3597;
								}
								if (nameHashCode3 != 295562)
								{
									goto IL_3655;
								}
							}
							num16 = (int)this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[1].valueStartIndex, this.m_xmlAttribute[1].valueLength);
							if (num16 == -32768)
							{
								return false;
							}
							if (num16 > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
							{
								return false;
							}
							this.m_spriteIndex = num16;
							goto IL_3669;
						}
						IL_3597:
						this.m_spriteColor = this.HexCharsToColor(this.m_htmlTag, this.m_xmlAttribute[num15].valueStartIndex, this.m_xmlAttribute[num15].valueLength);
					}
					IL_3669:
					num15++;
					continue;
					IL_3552:
					this.m_tintSprite = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[num15].valueStartIndex, this.m_xmlAttribute[num15].valueLength) != 0f;
					goto IL_3669;
					IL_3655:
					if (nameHashCode3 != 2246877 && nameHashCode3 != 1619421)
					{
						return false;
					}
					goto IL_3669;
				}
				if (this.m_spriteIndex == -1)
				{
					return false;
				}
				this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentSpriteAsset.material, this.m_currentSpriteAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
				this.m_textElementType = TMP_TextElementType.Sprite;
				return true;
				IL_37AD:
				TagValueType valueType = this.m_xmlAttribute[0].valueType;
				if (valueType == TagValueType.None)
				{
					int num17 = 1;
					while (num17 < this.m_xmlAttribute.Length && this.m_xmlAttribute[num17].nameHashCode != 0)
					{
						int nameHashCode4 = this.m_xmlAttribute[num17].nameHashCode;
						if (nameHashCode4 != 42823)
						{
							if (nameHashCode4 == 315620)
							{
								num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[num17].valueStartIndex, this.m_xmlAttribute[num17].valueLength);
								if (num11 == -32768f)
								{
									return false;
								}
								switch (this.m_xmlAttribute[num17].unitType)
								{
								case TagUnitType.Pixels:
									this.m_marginRight = num11 * (this.m_isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_marginRight = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
									break;
								case TagUnitType.Percentage:
									this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num11 / 100f;
									break;
								}
								this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
							}
						}
						else
						{
							num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[num17].valueStartIndex, this.m_xmlAttribute[num17].valueLength);
							if (num11 == -32768f)
							{
								return false;
							}
							switch (this.m_xmlAttribute[num17].unitType)
							{
							case TagUnitType.Pixels:
								this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num11 / 100f;
								break;
							}
							this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
						}
						num17++;
					}
					return true;
				}
				if (valueType != TagValueType.NumericalValue)
				{
					return false;
				}
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					break;
				case TagUnitType.Percentage:
					this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num11 / 100f;
					break;
				}
				this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
				this.m_marginRight = this.m_marginLeft;
				return true;
				IL_3DDD:
				int valueHashCode3 = this.m_xmlAttribute[0].valueHashCode;
				if (this.m_isParsingText)
				{
					this.m_actionStack.Add(valueHashCode3);
					Debug.Log(string.Concat(new object[] { "Action ID: [", valueHashCode3, "] First character index: ", this.m_characterCount }));
				}
				return true;
				IL_3F0B:
				num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
				if (num11 == -32768f)
				{
					return false;
				}
				this.m_FXMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, num11), Vector3.one);
				this.m_isFXMatrixSet = true;
				return true;
				IL_2A89:
				this.m_width = -1f;
				return true;
				IL_3F02:
				this.m_isFXMatrixSet = false;
				return true;
			}
			if (num10 > 54741026)
			{
				if (num10 <= 514803617)
				{
					if (num10 <= 340349191)
					{
						if (num10 <= 72669687)
						{
							if (num10 == 69403544)
							{
								goto IL_2D3C;
							}
							if (num10 != 72669687)
							{
								return false;
							}
						}
						else
						{
							if (num10 == 100149144)
							{
								goto IL_2D3C;
							}
							if (num10 != 103415287)
							{
								if (num10 != 340349191)
								{
									return false;
								}
								goto IL_2E77;
							}
						}
						int num13 = this.m_xmlAttribute[0].valueHashCode;
						if (num13 == 764638571 || num13 == 523367755)
						{
							this.m_currentMaterial = this.m_materialReferences[0].material;
							this.m_currentMaterialIndex = 0;
							this.m_materialReferenceStack.Add(this.m_materialReferences[0]);
							return true;
						}
						Material material;
						if (MaterialReferenceManager.TryGetMaterial(num13, out material))
						{
							this.m_currentMaterial = material;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
							this.m_materialReferenceStack.Add(this.m_materialReferences[this.m_currentMaterialIndex]);
						}
						else
						{
							material = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
							if (material == null)
							{
								return false;
							}
							MaterialReferenceManager.AddFontMaterial(num13, material);
							this.m_currentMaterial = material;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, this.m_materialReferences, this.m_materialReferenceIndexLookup);
							this.m_materialReferenceStack.Add(this.m_materialReferences[this.m_currentMaterialIndex]);
						}
						return true;
						IL_2D3C:
						int valueHashCode4 = this.m_xmlAttribute[0].valueHashCode;
						TMP_ColorGradient tmp_ColorGradient;
						if (MaterialReferenceManager.TryGetColorGradientPreset(valueHashCode4, out tmp_ColorGradient))
						{
							this.m_colorGradientPreset = tmp_ColorGradient;
						}
						else
						{
							if (tmp_ColorGradient == null)
							{
								tmp_ColorGradient = Resources.Load<TMP_ColorGradient>(TMP_Settings.defaultColorGradientPresetsPath + new string(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength));
							}
							if (tmp_ColorGradient == null)
							{
								return false;
							}
							MaterialReferenceManager.AddColorGradientPreset(valueHashCode4, tmp_ColorGradient);
							this.m_colorGradientPreset = tmp_ColorGradient;
						}
						this.m_colorGradientPresetIsTinted = false;
						int num18 = 1;
						while (num18 < this.m_xmlAttribute.Length && this.m_xmlAttribute[num18].nameHashCode != 0)
						{
							int nameHashCode5 = this.m_xmlAttribute[num18].nameHashCode;
							if (nameHashCode5 == 33019 || nameHashCode5 == 45819)
							{
								this.m_colorGradientPresetIsTinted = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[num18].valueStartIndex, this.m_xmlAttribute[num18].valueLength) != 0f;
							}
							num18++;
						}
						this.m_colorGradientStack.Add(this.m_colorGradientPreset);
						return true;
					}
					if (num10 <= 371094791)
					{
						if (num10 != 343615334)
						{
							if (num10 != 371094791)
							{
								return false;
							}
							goto IL_2E77;
						}
					}
					else if (num10 != 374360934)
					{
						if (num10 == 457225591)
						{
							goto IL_1C4B;
						}
						if (num10 != 514803617)
						{
							return false;
						}
						goto IL_36CE;
					}
					MaterialReference materialReference2 = this.m_materialReferenceStack.Remove();
					this.m_currentMaterial = materialReference2.material;
					this.m_currentMaterialIndex = materialReference2.index;
					return true;
					IL_2E77:
					this.m_colorGradientPreset = this.m_colorGradientStack.Remove();
					return true;
				}
				if (num10 <= 781906058)
				{
					if (num10 <= 566686826)
					{
						if (num10 != 551025096)
						{
							if (num10 != 566686826)
							{
								return false;
							}
							goto IL_3715;
						}
					}
					else
					{
						if (num10 == 730022849)
						{
							goto IL_36CE;
						}
						if (num10 != 766244328)
						{
							if (num10 != 781906058)
							{
								return false;
							}
							goto IL_3715;
						}
					}
					this.m_FontStyleInternal |= FontStyles.SmallCaps;
					this.m_fontStyleStack.Add(FontStyles.SmallCaps);
					return true;
				}
				if (num10 <= 1109386397)
				{
					if (num10 == 1100728678)
					{
						goto IL_3B38;
					}
					if (num10 == 1109349752)
					{
						goto IL_3D06;
					}
					if (num10 != 1109386397)
					{
						return false;
					}
					goto IL_3110;
				}
				else
				{
					if (num10 == 1897350193)
					{
						goto IL_3DC7;
					}
					if (num10 == 1897386838)
					{
						goto IL_31CF;
					}
					if (num10 != 2012149182)
					{
						return false;
					}
					goto IL_1B0E;
				}
				IL_36CE:
				this.m_FontStyleInternal |= FontStyles.LowerCase;
				this.m_fontStyleStack.Add(FontStyles.LowerCase);
				return true;
			}
			if (num10 <= 7757466)
			{
				if (num10 <= 7443301)
				{
					if (num10 <= 7011901)
					{
						if (num10 == 6971027)
						{
							goto IL_30FD;
						}
						if (num10 != 7011901)
						{
							return false;
						}
						goto IL_3B20;
					}
					else if (num10 != 7054088)
					{
						if (num10 == 7130010)
						{
							goto IL_3F75;
						}
						if (num10 != 7443301)
						{
							return false;
						}
						goto IL_3E3F;
					}
				}
				else if (num10 <= 7598483)
				{
					if (num10 == 7513474)
					{
						goto IL_2F23;
					}
					if (num10 != 7598483)
					{
						return false;
					}
					goto IL_30FD;
				}
				else
				{
					if (num10 == 7639357)
					{
						goto IL_3B20;
					}
					if (num10 != 7681544)
					{
						if (num10 != 7757466)
						{
							return false;
						}
						goto IL_3F75;
					}
				}
				this.m_monoSpacing = 0f;
				return true;
				IL_30FD:
				this.tag_Indent = this.m_indentStack.Remove();
				return true;
				IL_3B20:
				this.m_marginLeft = 0f;
				this.m_marginRight = 0f;
				return true;
				IL_3F75:
				this.m_isFXMatrixSet = false;
				return true;
			}
			if (num10 <= 15115642)
			{
				if (num10 <= 10723418)
				{
					if (num10 == 9133802)
					{
						goto IL_3715;
					}
					if (num10 != 10723418)
					{
						return false;
					}
				}
				else
				{
					if (num10 == 11642281)
					{
						goto IL_1D37;
					}
					if (num10 == 13526026)
					{
						goto IL_3715;
					}
					if (num10 != 15115642)
					{
						return false;
					}
				}
				this.tag_NoParsing = true;
				return true;
			}
			if (num10 > 47840323)
			{
				if (num10 != 50348802)
				{
					if (num10 == 52232547)
					{
						goto IL_3734;
					}
					if (num10 != 54741026)
					{
						return false;
					}
				}
				this.m_baselineOffset = 0f;
				return true;
			}
			if (num10 != 16034505)
			{
				if (num10 != 47840323)
				{
					return false;
				}
				goto IL_3734;
			}
			IL_1D37:
			num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
			if (num11 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_baselineOffset = num11 * (this.m_isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				this.m_baselineOffset = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
			IL_3715:
			this.m_FontStyleInternal |= FontStyles.UpperCase;
			this.m_fontStyleStack.Add(FontStyles.UpperCase);
			return true;
			IL_2F23:
			if (!this.m_isParsingText)
			{
				return true;
			}
			if (this.m_characterCount > 0)
			{
				this.m_xAdvance -= this.m_cSpacing;
				this.m_textInfo.characterInfo[this.m_characterCount - 1].xAdvance = this.m_xAdvance;
			}
			this.m_cSpacing = 0f;
			return true;
			IL_3E3F:
			if (this.m_isParsingText)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Action ID: [",
					this.m_actionStack.CurrentItem(),
					"] Last character index: ",
					this.m_characterCount - 1
				}));
			}
			this.m_actionStack.Remove();
			return true;
			IL_1B0E:
			num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
			if (num11 == -32768f)
			{
				return false;
			}
			num10 = (int)num11;
			if (num10 <= 400)
			{
				if (num10 <= 200)
				{
					if (num10 != 100)
					{
						if (num10 == 200)
						{
							this.m_FontWeightInternal = FontWeight.ExtraLight;
						}
					}
					else
					{
						this.m_FontWeightInternal = FontWeight.Thin;
					}
				}
				else if (num10 != 300)
				{
					if (num10 == 400)
					{
						this.m_FontWeightInternal = FontWeight.Regular;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Light;
				}
			}
			else if (num10 <= 600)
			{
				if (num10 != 500)
				{
					if (num10 == 600)
					{
						this.m_FontWeightInternal = FontWeight.SemiBold;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Medium;
				}
			}
			else if (num10 != 700)
			{
				if (num10 != 800)
				{
					if (num10 == 900)
					{
						this.m_FontWeightInternal = FontWeight.Black;
					}
				}
				else
				{
					this.m_FontWeightInternal = FontWeight.Heavy;
				}
			}
			else
			{
				this.m_FontWeightInternal = FontWeight.Bold;
			}
			this.m_FontWeightStack.Add(this.m_FontWeightInternal);
			return true;
			IL_1C4B:
			this.m_FontWeightStack.Remove();
			if (this.m_FontStyleInternal == FontStyles.Bold)
			{
				this.m_FontWeightInternal = FontWeight.Bold;
			}
			else
			{
				this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
			}
			return true;
			IL_270E:
			if (this.m_xmlAttribute[0].valueLength != 3)
			{
				return false;
			}
			this.m_htmlColor.a = (byte)(this.HexToInt(this.m_htmlTag[7]) * 16 + this.HexToInt(this.m_htmlTag[8]));
			return true;
			IL_2916:
			num10 = this.m_xmlAttribute[0].valueHashCode;
			if (num10 <= -458210101)
			{
				if (num10 == -523808257)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Justified;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num10 == -458210101)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Center;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
			}
			else
			{
				if (num10 == 3774683)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Left;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num10 == 122383428)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Flush;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
				if (num10 == 136703040)
				{
					this.m_lineJustification = HorizontalAlignmentOptions.Right;
					this.m_lineJustificationStack.Add(this.m_lineJustification);
					return true;
				}
			}
			return false;
			IL_3110:
			num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
			if (num11 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.tag_LineIndent = num11 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.tag_LineIndent = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				this.tag_LineIndent = this.m_marginWidth * num11 / 100f;
				break;
			}
			this.m_xAdvance += this.tag_LineIndent;
			return true;
			IL_31CF:
			this.tag_LineIndent = 0f;
			return true;
			IL_3734:
			if ((this.m_fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase && this.m_fontStyleStack.Remove(FontStyles.UpperCase) == 0)
			{
				this.m_FontStyleInternal &= (FontStyles)(-17);
			}
			return true;
			IL_3B38:
			num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
			if (num11 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.m_marginLeft = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * num11 / 100f;
				break;
			}
			this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
			return true;
			IL_3D06:
			num11 = this.ConvertToFloat(this.m_htmlTag, this.m_xmlAttribute[0].valueStartIndex, this.m_xmlAttribute[0].valueLength);
			if (num11 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				this.m_lineHeight = num11 * (this.m_isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				this.m_lineHeight = num11 * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
				break;
			case TagUnitType.Percentage:
				this.m_lineHeight = this.m_fontAsset.faceInfo.lineHeight * num11 / 100f * this.m_fontScale;
				break;
			}
			return true;
			IL_3DC7:
			this.m_lineHeight = -32767f;
			return true;
		}

		// Token: 0x04000333 RID: 819
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_text;

		// Token: 0x04000334 RID: 820
		[SerializeField]
		protected bool m_isRightToLeft;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		protected TMP_FontAsset m_fontAsset;

		// Token: 0x04000336 RID: 822
		protected TMP_FontAsset m_currentFontAsset;

		// Token: 0x04000337 RID: 823
		protected bool m_isSDFShader;

		// Token: 0x04000338 RID: 824
		[SerializeField]
		protected Material m_sharedMaterial;

		// Token: 0x04000339 RID: 825
		protected Material m_currentMaterial;

		// Token: 0x0400033A RID: 826
		protected MaterialReference[] m_materialReferences = new MaterialReference[32];

		// Token: 0x0400033B RID: 827
		protected Dictionary<int, int> m_materialReferenceIndexLookup = new Dictionary<int, int>();

		// Token: 0x0400033C RID: 828
		protected TMP_RichTextTagStack<MaterialReference> m_materialReferenceStack = new TMP_RichTextTagStack<MaterialReference>(new MaterialReference[16]);

		// Token: 0x0400033D RID: 829
		protected int m_currentMaterialIndex;

		// Token: 0x0400033E RID: 830
		[SerializeField]
		protected Material[] m_fontSharedMaterials;

		// Token: 0x0400033F RID: 831
		[SerializeField]
		protected Material m_fontMaterial;

		// Token: 0x04000340 RID: 832
		[SerializeField]
		protected Material[] m_fontMaterials;

		// Token: 0x04000341 RID: 833
		protected bool m_isMaterialDirty;

		// Token: 0x04000342 RID: 834
		[SerializeField]
		protected Color32 m_fontColor32 = Color.white;

		// Token: 0x04000343 RID: 835
		[SerializeField]
		protected Color m_fontColor = Color.white;

		// Token: 0x04000344 RID: 836
		protected static Color32 s_colorWhite = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000345 RID: 837
		protected Color32 m_underlineColor = TMP_Text.s_colorWhite;

		// Token: 0x04000346 RID: 838
		protected Color32 m_strikethroughColor = TMP_Text.s_colorWhite;

		// Token: 0x04000347 RID: 839
		[SerializeField]
		protected bool m_enableVertexGradient;

		// Token: 0x04000348 RID: 840
		[SerializeField]
		protected ColorMode m_colorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000349 RID: 841
		[SerializeField]
		protected VertexGradient m_fontColorGradient = new VertexGradient(Color.white);

		// Token: 0x0400034A RID: 842
		[SerializeField]
		protected TMP_ColorGradient m_fontColorGradientPreset;

		// Token: 0x0400034B RID: 843
		[SerializeField]
		protected TMP_SpriteAsset m_spriteAsset;

		// Token: 0x0400034C RID: 844
		[SerializeField]
		protected bool m_tintAllSprites;

		// Token: 0x0400034D RID: 845
		protected bool m_tintSprite;

		// Token: 0x0400034E RID: 846
		protected Color32 m_spriteColor;

		// Token: 0x0400034F RID: 847
		[SerializeField]
		protected TMP_StyleSheet m_StyleSheet;

		// Token: 0x04000350 RID: 848
		internal TMP_Style m_TextStyle;

		// Token: 0x04000351 RID: 849
		[SerializeField]
		protected int m_TextStyleHashCode;

		// Token: 0x04000352 RID: 850
		[SerializeField]
		protected bool m_overrideHtmlColors;

		// Token: 0x04000353 RID: 851
		[SerializeField]
		protected Color32 m_faceColor = Color.white;

		// Token: 0x04000354 RID: 852
		[SerializeField]
		protected Color32 m_outlineColor = Color.black;

		// Token: 0x04000355 RID: 853
		protected float m_outlineWidth;

		// Token: 0x04000356 RID: 854
		[SerializeField]
		protected float m_fontSize = 36f;

		// Token: 0x04000357 RID: 855
		protected float m_currentFontSize;

		// Token: 0x04000358 RID: 856
		[SerializeField]
		protected float m_fontSizeBase = 36f;

		// Token: 0x04000359 RID: 857
		protected TMP_RichTextTagStack<float> m_sizeStack = new TMP_RichTextTagStack<float>(16);

		// Token: 0x0400035A RID: 858
		[SerializeField]
		protected FontWeight m_fontWeight = FontWeight.Regular;

		// Token: 0x0400035B RID: 859
		protected FontWeight m_FontWeightInternal = FontWeight.Regular;

		// Token: 0x0400035C RID: 860
		protected TMP_RichTextTagStack<FontWeight> m_FontWeightStack = new TMP_RichTextTagStack<FontWeight>(8);

		// Token: 0x0400035D RID: 861
		[SerializeField]
		protected bool m_enableAutoSizing;

		// Token: 0x0400035E RID: 862
		protected float m_maxFontSize;

		// Token: 0x0400035F RID: 863
		protected float m_minFontSize;

		// Token: 0x04000360 RID: 864
		protected int m_AutoSizeIterationCount;

		// Token: 0x04000361 RID: 865
		protected int m_AutoSizeMaxIterationCount = 100;

		// Token: 0x04000362 RID: 866
		protected bool m_IsAutoSizePointSizeSet;

		// Token: 0x04000363 RID: 867
		[SerializeField]
		protected float m_fontSizeMin;

		// Token: 0x04000364 RID: 868
		[SerializeField]
		protected float m_fontSizeMax;

		// Token: 0x04000365 RID: 869
		[SerializeField]
		protected FontStyles m_fontStyle;

		// Token: 0x04000366 RID: 870
		protected FontStyles m_FontStyleInternal;

		// Token: 0x04000367 RID: 871
		protected TMP_FontStyleStack m_fontStyleStack;

		// Token: 0x04000368 RID: 872
		protected bool m_isUsingBold;

		// Token: 0x04000369 RID: 873
		[SerializeField]
		protected HorizontalAlignmentOptions m_HorizontalAlignment = HorizontalAlignmentOptions.Left;

		// Token: 0x0400036A RID: 874
		[SerializeField]
		protected VerticalAlignmentOptions m_VerticalAlignment = VerticalAlignmentOptions.Top;

		// Token: 0x0400036B RID: 875
		[SerializeField]
		[FormerlySerializedAs("m_lineJustification")]
		protected TextAlignmentOptions m_textAlignment = TextAlignmentOptions.TopLeft;

		// Token: 0x0400036C RID: 876
		protected HorizontalAlignmentOptions m_lineJustification;

		// Token: 0x0400036D RID: 877
		protected TMP_RichTextTagStack<HorizontalAlignmentOptions> m_lineJustificationStack = new TMP_RichTextTagStack<HorizontalAlignmentOptions>(new HorizontalAlignmentOptions[16]);

		// Token: 0x0400036E RID: 878
		protected Vector3[] m_textContainerLocalCorners = new Vector3[4];

		// Token: 0x0400036F RID: 879
		[SerializeField]
		protected bool m_isAlignmentEnumConverted;

		// Token: 0x04000370 RID: 880
		[SerializeField]
		protected float m_characterSpacing;

		// Token: 0x04000371 RID: 881
		protected float m_cSpacing;

		// Token: 0x04000372 RID: 882
		protected float m_monoSpacing;

		// Token: 0x04000373 RID: 883
		[SerializeField]
		protected float m_wordSpacing;

		// Token: 0x04000374 RID: 884
		[SerializeField]
		protected float m_lineSpacing;

		// Token: 0x04000375 RID: 885
		protected float m_lineSpacingDelta;

		// Token: 0x04000376 RID: 886
		protected float m_lineHeight = -32767f;

		// Token: 0x04000377 RID: 887
		[SerializeField]
		protected float m_lineSpacingMax;

		// Token: 0x04000378 RID: 888
		[SerializeField]
		protected float m_paragraphSpacing;

		// Token: 0x04000379 RID: 889
		[SerializeField]
		protected float m_charWidthMaxAdj;

		// Token: 0x0400037A RID: 890
		protected float m_charWidthAdjDelta;

		// Token: 0x0400037B RID: 891
		[SerializeField]
		protected bool m_enableWordWrapping;

		// Token: 0x0400037C RID: 892
		protected bool m_isCharacterWrappingEnabled;

		// Token: 0x0400037D RID: 893
		protected bool m_isNonBreakingSpace;

		// Token: 0x0400037E RID: 894
		protected bool m_isIgnoringAlignment;

		// Token: 0x0400037F RID: 895
		[SerializeField]
		protected float m_wordWrappingRatios = 0.4f;

		// Token: 0x04000380 RID: 896
		[SerializeField]
		protected TextOverflowModes m_overflowMode;

		// Token: 0x04000381 RID: 897
		[SerializeField]
		protected int m_firstOverflowCharacterIndex = -1;

		// Token: 0x04000382 RID: 898
		[SerializeField]
		protected TMP_Text m_linkedTextComponent;

		// Token: 0x04000383 RID: 899
		[SerializeField]
		internal TMP_Text parentLinkedComponent;

		// Token: 0x04000384 RID: 900
		[SerializeField]
		protected bool m_isTextTruncated;

		// Token: 0x04000385 RID: 901
		[SerializeField]
		protected bool m_enableKerning;

		// Token: 0x04000386 RID: 902
		[SerializeField]
		protected bool m_enableExtraPadding;

		// Token: 0x04000387 RID: 903
		[SerializeField]
		protected bool checkPaddingRequired;

		// Token: 0x04000388 RID: 904
		[SerializeField]
		protected bool m_isRichText = true;

		// Token: 0x04000389 RID: 905
		[SerializeField]
		protected bool m_parseCtrlCharacters = true;

		// Token: 0x0400038A RID: 906
		protected bool m_isOverlay;

		// Token: 0x0400038B RID: 907
		[SerializeField]
		protected bool m_isOrthographic;

		// Token: 0x0400038C RID: 908
		[SerializeField]
		protected bool m_isCullingEnabled;

		// Token: 0x0400038D RID: 909
		protected bool m_isMaskingEnabled;

		// Token: 0x0400038E RID: 910
		protected bool isMaskUpdateRequired;

		// Token: 0x0400038F RID: 911
		[SerializeField]
		protected bool m_ignoreCulling = true;

		// Token: 0x04000390 RID: 912
		[SerializeField]
		protected TextureMappingOptions m_horizontalMapping;

		// Token: 0x04000391 RID: 913
		[SerializeField]
		protected TextureMappingOptions m_verticalMapping;

		// Token: 0x04000392 RID: 914
		[SerializeField]
		protected float m_uvLineOffset;

		// Token: 0x04000393 RID: 915
		protected TextRenderFlags m_renderMode = TextRenderFlags.Render;

		// Token: 0x04000394 RID: 916
		[SerializeField]
		protected VertexSortingOrder m_geometrySortingOrder;

		// Token: 0x04000395 RID: 917
		[SerializeField]
		protected bool m_IsTextObjectScaleStatic;

		// Token: 0x04000396 RID: 918
		[SerializeField]
		protected bool m_VertexBufferAutoSizeReduction = true;

		// Token: 0x04000397 RID: 919
		[SerializeField]
		protected int m_firstVisibleCharacter;

		// Token: 0x04000398 RID: 920
		protected int m_maxVisibleCharacters = 99999;

		// Token: 0x04000399 RID: 921
		protected int m_maxVisibleWords = 99999;

		// Token: 0x0400039A RID: 922
		protected int m_maxVisibleLines = 99999;

		// Token: 0x0400039B RID: 923
		[SerializeField]
		protected bool m_useMaxVisibleDescender = true;

		// Token: 0x0400039C RID: 924
		[SerializeField]
		protected int m_pageToDisplay = 1;

		// Token: 0x0400039D RID: 925
		protected bool m_isNewPage;

		// Token: 0x0400039E RID: 926
		[SerializeField]
		protected Vector4 m_margin = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x0400039F RID: 927
		protected float m_marginLeft;

		// Token: 0x040003A0 RID: 928
		protected float m_marginRight;

		// Token: 0x040003A1 RID: 929
		protected float m_marginWidth;

		// Token: 0x040003A2 RID: 930
		protected float m_marginHeight;

		// Token: 0x040003A3 RID: 931
		protected float m_width = -1f;

		// Token: 0x040003A4 RID: 932
		[SerializeField]
		protected TMP_TextInfo m_textInfo;

		// Token: 0x040003A5 RID: 933
		protected bool m_havePropertiesChanged;

		// Token: 0x040003A6 RID: 934
		[SerializeField]
		protected bool m_isUsingLegacyAnimationComponent;

		// Token: 0x040003A7 RID: 935
		protected Transform m_transform;

		// Token: 0x040003A8 RID: 936
		protected RectTransform m_rectTransform;

		// Token: 0x040003AA RID: 938
		protected bool m_autoSizeTextContainer;

		// Token: 0x040003AB RID: 939
		protected Mesh m_mesh;

		// Token: 0x040003AC RID: 940
		[SerializeField]
		protected bool m_isVolumetricText;

		// Token: 0x040003AF RID: 943
		[SerializeField]
		protected TMP_SpriteAnimator m_spriteAnimator;

		// Token: 0x040003B0 RID: 944
		protected float m_flexibleHeight = -1f;

		// Token: 0x040003B1 RID: 945
		protected float m_flexibleWidth = -1f;

		// Token: 0x040003B2 RID: 946
		protected float m_minWidth;

		// Token: 0x040003B3 RID: 947
		protected float m_minHeight;

		// Token: 0x040003B4 RID: 948
		protected float m_maxWidth;

		// Token: 0x040003B5 RID: 949
		protected float m_maxHeight;

		// Token: 0x040003B6 RID: 950
		protected LayoutElement m_LayoutElement;

		// Token: 0x040003B7 RID: 951
		protected float m_preferredWidth;

		// Token: 0x040003B8 RID: 952
		protected float m_renderedWidth;

		// Token: 0x040003B9 RID: 953
		protected bool m_isPreferredWidthDirty;

		// Token: 0x040003BA RID: 954
		protected float m_preferredHeight;

		// Token: 0x040003BB RID: 955
		protected float m_renderedHeight;

		// Token: 0x040003BC RID: 956
		protected bool m_isPreferredHeightDirty;

		// Token: 0x040003BD RID: 957
		protected bool m_isCalculatingPreferredValues;

		// Token: 0x040003BE RID: 958
		protected int m_layoutPriority;

		// Token: 0x040003BF RID: 959
		protected bool m_isCalculateSizeRequired;

		// Token: 0x040003C0 RID: 960
		protected bool m_isLayoutDirty;

		// Token: 0x040003C1 RID: 961
		protected bool m_verticesAlreadyDirty;

		// Token: 0x040003C2 RID: 962
		protected bool m_layoutAlreadyDirty;

		// Token: 0x040003C3 RID: 963
		protected bool m_isAwake;

		// Token: 0x040003C4 RID: 964
		internal bool m_isWaitingOnResourceLoad;

		// Token: 0x040003C5 RID: 965
		internal bool m_isInputParsingRequired;

		// Token: 0x040003C6 RID: 966
		internal TMP_Text.TextInputSources m_inputSource;

		// Token: 0x040003C7 RID: 967
		protected float m_fontScale;

		// Token: 0x040003C8 RID: 968
		protected float m_fontScaleMultiplier;

		// Token: 0x040003C9 RID: 969
		protected char[] m_htmlTag = new char[128];

		// Token: 0x040003CA RID: 970
		protected RichTextTagAttribute[] m_xmlAttribute = new RichTextTagAttribute[8];

		// Token: 0x040003CB RID: 971
		protected float[] m_attributeParameterValues = new float[16];

		// Token: 0x040003CC RID: 972
		protected float tag_LineIndent;

		// Token: 0x040003CD RID: 973
		protected float tag_Indent;

		// Token: 0x040003CE RID: 974
		protected TMP_RichTextTagStack<float> m_indentStack = new TMP_RichTextTagStack<float>(new float[16]);

		// Token: 0x040003CF RID: 975
		protected bool tag_NoParsing;

		// Token: 0x040003D0 RID: 976
		protected bool m_isParsingText;

		// Token: 0x040003D1 RID: 977
		protected Matrix4x4 m_FXMatrix;

		// Token: 0x040003D2 RID: 978
		protected bool m_isFXMatrixSet;

		// Token: 0x040003D3 RID: 979
		protected TMP_Text.UnicodeChar[] m_TextParsingBuffer;

		// Token: 0x040003D4 RID: 980
		private TMP_CharacterInfo[] m_internalCharacterInfo;

		// Token: 0x040003D5 RID: 981
		protected char[] m_input_CharArray = new char[256];

		// Token: 0x040003D6 RID: 982
		private int m_charArray_Length;

		// Token: 0x040003D7 RID: 983
		protected int m_totalCharacterCount;

		// Token: 0x040003D8 RID: 984
		protected WordWrapState m_SavedWordWrapState;

		// Token: 0x040003D9 RID: 985
		protected WordWrapState m_SavedLineState;

		// Token: 0x040003DA RID: 986
		protected WordWrapState m_SavedEllipsisState;

		// Token: 0x040003DB RID: 987
		protected WordWrapState m_SavedLastValidState;

		// Token: 0x040003DC RID: 988
		protected int m_characterCount;

		// Token: 0x040003DD RID: 989
		protected int m_firstCharacterOfLine;

		// Token: 0x040003DE RID: 990
		protected int m_firstVisibleCharacterOfLine;

		// Token: 0x040003DF RID: 991
		protected int m_lastCharacterOfLine;

		// Token: 0x040003E0 RID: 992
		protected int m_lastVisibleCharacterOfLine;

		// Token: 0x040003E1 RID: 993
		protected int m_lineNumber;

		// Token: 0x040003E2 RID: 994
		protected int m_lineVisibleCharacterCount;

		// Token: 0x040003E3 RID: 995
		protected int m_pageNumber;

		// Token: 0x040003E4 RID: 996
		protected float m_maxAscender;

		// Token: 0x040003E5 RID: 997
		protected float m_maxCapHeight;

		// Token: 0x040003E6 RID: 998
		protected float m_maxDescender;

		// Token: 0x040003E7 RID: 999
		protected float m_maxLineAscender;

		// Token: 0x040003E8 RID: 1000
		protected float m_maxLineDescender;

		// Token: 0x040003E9 RID: 1001
		protected float m_startOfLineAscender;

		// Token: 0x040003EA RID: 1002
		protected float m_lineOffset;

		// Token: 0x040003EB RID: 1003
		protected Extents m_meshExtents;

		// Token: 0x040003EC RID: 1004
		protected Color32 m_htmlColor = new Color(255f, 255f, 255f, 128f);

		// Token: 0x040003ED RID: 1005
		protected TMP_RichTextTagStack<Color32> m_colorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

		// Token: 0x040003EE RID: 1006
		protected TMP_RichTextTagStack<Color32> m_underlineColorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

		// Token: 0x040003EF RID: 1007
		protected TMP_RichTextTagStack<Color32> m_strikethroughColorStack = new TMP_RichTextTagStack<Color32>(new Color32[16]);

		// Token: 0x040003F0 RID: 1008
		protected TMP_RichTextTagStack<HighlightState> m_HighlightStateStack = new TMP_RichTextTagStack<HighlightState>(new HighlightState[16]);

		// Token: 0x040003F1 RID: 1009
		protected TMP_ColorGradient m_colorGradientPreset;

		// Token: 0x040003F2 RID: 1010
		protected TMP_RichTextTagStack<TMP_ColorGradient> m_colorGradientStack = new TMP_RichTextTagStack<TMP_ColorGradient>(new TMP_ColorGradient[16]);

		// Token: 0x040003F3 RID: 1011
		protected bool m_colorGradientPresetIsTinted;

		// Token: 0x040003F4 RID: 1012
		protected float m_tabSpacing;

		// Token: 0x040003F5 RID: 1013
		protected float m_spacing;

		// Token: 0x040003F6 RID: 1014
		protected TMP_RichTextTagStack<int>[] m_TextStyleStacks = new TMP_RichTextTagStack<int>[8];

		// Token: 0x040003F7 RID: 1015
		protected int m_TextStyleStackDepth;

		// Token: 0x040003F8 RID: 1016
		protected TMP_RichTextTagStack<int> m_ItalicAngleStack = new TMP_RichTextTagStack<int>(new int[16]);

		// Token: 0x040003F9 RID: 1017
		protected int m_ItalicAngle;

		// Token: 0x040003FA RID: 1018
		protected TMP_RichTextTagStack<int> m_actionStack = new TMP_RichTextTagStack<int>(new int[16]);

		// Token: 0x040003FB RID: 1019
		protected float m_padding;

		// Token: 0x040003FC RID: 1020
		protected float m_baselineOffset;

		// Token: 0x040003FD RID: 1021
		protected TMP_RichTextTagStack<float> m_baselineOffsetStack = new TMP_RichTextTagStack<float>(new float[16]);

		// Token: 0x040003FE RID: 1022
		protected float m_xAdvance;

		// Token: 0x040003FF RID: 1023
		protected TMP_TextElementType m_textElementType;

		// Token: 0x04000400 RID: 1024
		protected TMP_TextElement m_cached_TextElement;

		// Token: 0x04000401 RID: 1025
		protected TMP_Character m_cached_Underline_Character;

		// Token: 0x04000402 RID: 1026
		protected TMP_Character m_cached_Ellipsis_Character;

		// Token: 0x04000403 RID: 1027
		protected TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x04000404 RID: 1028
		protected TMP_SpriteAsset m_currentSpriteAsset;

		// Token: 0x04000405 RID: 1029
		protected int m_spriteCount;

		// Token: 0x04000406 RID: 1030
		protected int m_spriteIndex;

		// Token: 0x04000407 RID: 1031
		protected int m_spriteAnimationID;

		// Token: 0x04000408 RID: 1032
		protected bool m_ignoreActiveState;

		// Token: 0x04000409 RID: 1033
		private readonly float[] k_Power = new float[] { 0.5f, 0.05f, 0.005f, 0.0005f, 5E-05f, 5E-06f, 5E-07f, 5E-08f, 5E-09f, 5E-10f };

		// Token: 0x0400040A RID: 1034
		protected static Vector2 k_LargePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

		// Token: 0x0400040B RID: 1035
		protected static Vector2 k_LargeNegativeVector2 = new Vector2(-2.1474836E+09f, -2.1474836E+09f);

		// Token: 0x0400040C RID: 1036
		protected static float k_LargePositiveFloat = 32767f;

		// Token: 0x0400040D RID: 1037
		protected static float k_LargeNegativeFloat = -32767f;

		// Token: 0x0400040E RID: 1038
		protected static int k_LargePositiveInt = int.MaxValue;

		// Token: 0x0400040F RID: 1039
		protected static int k_LargeNegativeInt = -2147483647;

		// Token: 0x0200009E RID: 158
		protected struct CharacterSubstitution
		{
			// Token: 0x060005EF RID: 1519 RVA: 0x000378E3 File Offset: 0x00035AE3
			public CharacterSubstitution(int index, uint unicode)
			{
				this.index = index;
				this.unicode = unicode;
			}

			// Token: 0x0400058C RID: 1420
			public int index;

			// Token: 0x0400058D RID: 1421
			public uint unicode;
		}

		// Token: 0x0200009F RID: 159
		internal enum TextInputSources
		{
			// Token: 0x0400058F RID: 1423
			Text,
			// Token: 0x04000590 RID: 1424
			SetText,
			// Token: 0x04000591 RID: 1425
			SetCharArray,
			// Token: 0x04000592 RID: 1426
			String
		}

		// Token: 0x020000A0 RID: 160
		protected struct UnicodeChar
		{
			// Token: 0x04000593 RID: 1427
			public int unicode;

			// Token: 0x04000594 RID: 1428
			public int stringIndex;

			// Token: 0x04000595 RID: 1429
			public int length;
		}
	}
}
