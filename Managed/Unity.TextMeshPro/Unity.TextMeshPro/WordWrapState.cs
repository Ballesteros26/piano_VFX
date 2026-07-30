using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200006B RID: 107
	public struct WordWrapState
	{
		// Token: 0x0400048B RID: 1163
		public int previous_WordBreak;

		// Token: 0x0400048C RID: 1164
		public int total_CharacterCount;

		// Token: 0x0400048D RID: 1165
		public int visible_CharacterCount;

		// Token: 0x0400048E RID: 1166
		public int visible_SpriteCount;

		// Token: 0x0400048F RID: 1167
		public int visible_LinkCount;

		// Token: 0x04000490 RID: 1168
		public int firstCharacterIndex;

		// Token: 0x04000491 RID: 1169
		public int firstVisibleCharacterIndex;

		// Token: 0x04000492 RID: 1170
		public int lastCharacterIndex;

		// Token: 0x04000493 RID: 1171
		public int lastVisibleCharIndex;

		// Token: 0x04000494 RID: 1172
		public int lineNumber;

		// Token: 0x04000495 RID: 1173
		public float maxCapHeight;

		// Token: 0x04000496 RID: 1174
		public float maxAscender;

		// Token: 0x04000497 RID: 1175
		public float maxDescender;

		// Token: 0x04000498 RID: 1176
		public float maxLineAscender;

		// Token: 0x04000499 RID: 1177
		public float maxLineDescender;

		// Token: 0x0400049A RID: 1178
		public float previousLineAscender;

		// Token: 0x0400049B RID: 1179
		public HorizontalAlignmentOptions horizontalAlignment;

		// Token: 0x0400049C RID: 1180
		public float marginLeft;

		// Token: 0x0400049D RID: 1181
		public float marginRight;

		// Token: 0x0400049E RID: 1182
		public float xAdvance;

		// Token: 0x0400049F RID: 1183
		public float preferredWidth;

		// Token: 0x040004A0 RID: 1184
		public float preferredHeight;

		// Token: 0x040004A1 RID: 1185
		public float previousLineScale;

		// Token: 0x040004A2 RID: 1186
		public int wordCount;

		// Token: 0x040004A3 RID: 1187
		public FontStyles fontStyle;

		// Token: 0x040004A4 RID: 1188
		public int italicAngle;

		// Token: 0x040004A5 RID: 1189
		public float fontScale;

		// Token: 0x040004A6 RID: 1190
		public float fontScaleMultiplier;

		// Token: 0x040004A7 RID: 1191
		public float currentFontSize;

		// Token: 0x040004A8 RID: 1192
		public float baselineOffset;

		// Token: 0x040004A9 RID: 1193
		public float lineOffset;

		// Token: 0x040004AA RID: 1194
		public float cSpace;

		// Token: 0x040004AB RID: 1195
		public float mSpace;

		// Token: 0x040004AC RID: 1196
		public TMP_TextInfo textInfo;

		// Token: 0x040004AD RID: 1197
		public TMP_LineInfo lineInfo;

		// Token: 0x040004AE RID: 1198
		public Color32 vertexColor;

		// Token: 0x040004AF RID: 1199
		public Color32 underlineColor;

		// Token: 0x040004B0 RID: 1200
		public Color32 strikethroughColor;

		// Token: 0x040004B1 RID: 1201
		public Color32 highlightColor;

		// Token: 0x040004B2 RID: 1202
		public TMP_FontStyleStack basicStyleStack;

		// Token: 0x040004B3 RID: 1203
		public TMP_RichTextTagStack<int> italicAngleStack;

		// Token: 0x040004B4 RID: 1204
		public TMP_RichTextTagStack<Color32> colorStack;

		// Token: 0x040004B5 RID: 1205
		public TMP_RichTextTagStack<Color32> underlineColorStack;

		// Token: 0x040004B6 RID: 1206
		public TMP_RichTextTagStack<Color32> strikethroughColorStack;

		// Token: 0x040004B7 RID: 1207
		public TMP_RichTextTagStack<Color32> highlightColorStack;

		// Token: 0x040004B8 RID: 1208
		public TMP_RichTextTagStack<HighlightState> highlightStateStack;

		// Token: 0x040004B9 RID: 1209
		public TMP_RichTextTagStack<TMP_ColorGradient> colorGradientStack;

		// Token: 0x040004BA RID: 1210
		public TMP_RichTextTagStack<float> sizeStack;

		// Token: 0x040004BB RID: 1211
		public TMP_RichTextTagStack<float> indentStack;

		// Token: 0x040004BC RID: 1212
		public TMP_RichTextTagStack<FontWeight> fontWeightStack;

		// Token: 0x040004BD RID: 1213
		public TMP_RichTextTagStack<int> styleStack;

		// Token: 0x040004BE RID: 1214
		public TMP_RichTextTagStack<float> baselineStack;

		// Token: 0x040004BF RID: 1215
		public TMP_RichTextTagStack<int> actionStack;

		// Token: 0x040004C0 RID: 1216
		public TMP_RichTextTagStack<MaterialReference> materialReferenceStack;

		// Token: 0x040004C1 RID: 1217
		public TMP_RichTextTagStack<HorizontalAlignmentOptions> lineJustificationStack;

		// Token: 0x040004C2 RID: 1218
		public int spriteAnimationID;

		// Token: 0x040004C3 RID: 1219
		public TMP_FontAsset currentFontAsset;

		// Token: 0x040004C4 RID: 1220
		public TMP_SpriteAsset currentSpriteAsset;

		// Token: 0x040004C5 RID: 1221
		public Material currentMaterial;

		// Token: 0x040004C6 RID: 1222
		public int currentMaterialIndex;

		// Token: 0x040004C7 RID: 1223
		public Extents meshExtents;

		// Token: 0x040004C8 RID: 1224
		public bool tagNoParsing;

		// Token: 0x040004C9 RID: 1225
		public bool isNonBreakingSpace;
	}
}
