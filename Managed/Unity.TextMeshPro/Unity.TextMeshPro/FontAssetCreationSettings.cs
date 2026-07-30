using System;

namespace TMPro
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public struct FontAssetCreationSettings
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007470 File Offset: 0x00005670
		internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.sourceFontFileName = string.Empty;
			this.sourceFontFileGUID = sourceFontFileGUID;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			this.characterSequence = characterSet;
			this.characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			this.referencedFontAssetGUID = string.Empty;
			this.referencedTextAssetGUID = string.Empty;
			this.fontStyle = 0;
			this.fontStyleModifier = 0f;
			this.includeFontFeatures = false;
		}

		// Token: 0x040000CC RID: 204
		public string sourceFontFileName;

		// Token: 0x040000CD RID: 205
		public string sourceFontFileGUID;

		// Token: 0x040000CE RID: 206
		public int pointSizeSamplingMode;

		// Token: 0x040000CF RID: 207
		public int pointSize;

		// Token: 0x040000D0 RID: 208
		public int padding;

		// Token: 0x040000D1 RID: 209
		public int packingMode;

		// Token: 0x040000D2 RID: 210
		public int atlasWidth;

		// Token: 0x040000D3 RID: 211
		public int atlasHeight;

		// Token: 0x040000D4 RID: 212
		public int characterSetSelectionMode;

		// Token: 0x040000D5 RID: 213
		public string characterSequence;

		// Token: 0x040000D6 RID: 214
		public string referencedFontAssetGUID;

		// Token: 0x040000D7 RID: 215
		public string referencedTextAssetGUID;

		// Token: 0x040000D8 RID: 216
		public int fontStyle;

		// Token: 0x040000D9 RID: 217
		public float fontStyleModifier;

		// Token: 0x040000DA RID: 218
		public int renderMode;

		// Token: 0x040000DB RID: 219
		public bool includeFontFeatures;
	}
}
