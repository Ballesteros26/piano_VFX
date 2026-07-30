using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000033 RID: 51
	internal struct WordWrapState
	{
		// Token: 0x040002CB RID: 715
		public int previousWordBreak;

		// Token: 0x040002CC RID: 716
		public int totalCharacterCount;

		// Token: 0x040002CD RID: 717
		public int visibleCharacterCount;

		// Token: 0x040002CE RID: 718
		public int visibleSpriteCount;

		// Token: 0x040002CF RID: 719
		public int visibleLinkCount;

		// Token: 0x040002D0 RID: 720
		public int firstCharacterIndex;

		// Token: 0x040002D1 RID: 721
		public int firstVisibleCharacterIndex;

		// Token: 0x040002D2 RID: 722
		public int lastCharacterIndex;

		// Token: 0x040002D3 RID: 723
		public int lastVisibleCharIndex;

		// Token: 0x040002D4 RID: 724
		public int lineNumber;

		// Token: 0x040002D5 RID: 725
		public float maxCapHeight;

		// Token: 0x040002D6 RID: 726
		public float maxAscender;

		// Token: 0x040002D7 RID: 727
		public float maxDescender;

		// Token: 0x040002D8 RID: 728
		public float maxLineAscender;

		// Token: 0x040002D9 RID: 729
		public float maxLineDescender;

		// Token: 0x040002DA RID: 730
		public float previousLineAscender;

		// Token: 0x040002DB RID: 731
		public float xAdvance;

		// Token: 0x040002DC RID: 732
		public float preferredWidth;

		// Token: 0x040002DD RID: 733
		public float preferredHeight;

		// Token: 0x040002DE RID: 734
		public float previousLineScale;

		// Token: 0x040002DF RID: 735
		public int wordCount;

		// Token: 0x040002E0 RID: 736
		public FontStyles fontStyle;

		// Token: 0x040002E1 RID: 737
		public float fontScale;

		// Token: 0x040002E2 RID: 738
		public float fontScaleMultiplier;

		// Token: 0x040002E3 RID: 739
		public float currentFontSize;

		// Token: 0x040002E4 RID: 740
		public float baselineOffset;

		// Token: 0x040002E5 RID: 741
		public float lineOffset;

		// Token: 0x040002E6 RID: 742
		public TextInfo textInfo;

		// Token: 0x040002E7 RID: 743
		public LineInfo lineInfo;

		// Token: 0x040002E8 RID: 744
		public Color32 vertexColor;

		// Token: 0x040002E9 RID: 745
		public Color32 underlineColor;

		// Token: 0x040002EA RID: 746
		public Color32 strikethroughColor;

		// Token: 0x040002EB RID: 747
		public Color32 highlightColor;

		// Token: 0x040002EC RID: 748
		public FontStyleStack basicStyleStack;

		// Token: 0x040002ED RID: 749
		public RichTextTagStack<Color32> colorStack;

		// Token: 0x040002EE RID: 750
		public RichTextTagStack<Color32> underlineColorStack;

		// Token: 0x040002EF RID: 751
		public RichTextTagStack<Color32> strikethroughColorStack;

		// Token: 0x040002F0 RID: 752
		public RichTextTagStack<Color32> highlightColorStack;

		// Token: 0x040002F1 RID: 753
		public RichTextTagStack<TextGradientPreset> colorGradientStack;

		// Token: 0x040002F2 RID: 754
		public RichTextTagStack<float> sizeStack;

		// Token: 0x040002F3 RID: 755
		public RichTextTagStack<float> indentStack;

		// Token: 0x040002F4 RID: 756
		public RichTextTagStack<FontWeight> fontWeightStack;

		// Token: 0x040002F5 RID: 757
		public RichTextTagStack<int> styleStack;

		// Token: 0x040002F6 RID: 758
		public RichTextTagStack<float> baselineStack;

		// Token: 0x040002F7 RID: 759
		public RichTextTagStack<int> actionStack;

		// Token: 0x040002F8 RID: 760
		public RichTextTagStack<MaterialReference> materialReferenceStack;

		// Token: 0x040002F9 RID: 761
		public RichTextTagStack<TextAlignment> lineJustificationStack;

		// Token: 0x040002FA RID: 762
		public int spriteAnimationId;

		// Token: 0x040002FB RID: 763
		public FontAsset currentFontAsset;

		// Token: 0x040002FC RID: 764
		public TextSpriteAsset currentSpriteAsset;

		// Token: 0x040002FD RID: 765
		public Material currentMaterial;

		// Token: 0x040002FE RID: 766
		public int currentMaterialIndex;

		// Token: 0x040002FF RID: 767
		public Extents meshExtents;

		// Token: 0x04000300 RID: 768
		public bool tagNoParsing;

		// Token: 0x04000301 RID: 769
		public bool isNonBreakingSpace;
	}
}
