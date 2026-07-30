using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal struct FontAssetCreationSettings
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002790 File Offset: 0x00000990
		internal FontAssetCreationSettings(string fontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.fontFileGUID = fontFileGUID;
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

		// Token: 0x04000016 RID: 22
		public string fontFileGUID;

		// Token: 0x04000017 RID: 23
		public int pointSizeSamplingMode;

		// Token: 0x04000018 RID: 24
		public int pointSize;

		// Token: 0x04000019 RID: 25
		public int padding;

		// Token: 0x0400001A RID: 26
		public int packingMode;

		// Token: 0x0400001B RID: 27
		public int atlasWidth;

		// Token: 0x0400001C RID: 28
		public int atlasHeight;

		// Token: 0x0400001D RID: 29
		public int characterSetSelectionMode;

		// Token: 0x0400001E RID: 30
		public string characterSequence;

		// Token: 0x0400001F RID: 31
		public string referencedFontAssetGUID;

		// Token: 0x04000020 RID: 32
		public string referencedTextAssetGUID;

		// Token: 0x04000021 RID: 33
		public int fontStyle;

		// Token: 0x04000022 RID: 34
		public float fontStyleModifier;

		// Token: 0x04000023 RID: 35
		public int renderMode;

		// Token: 0x04000024 RID: 36
		public bool includeFontFeatures;
	}
}
