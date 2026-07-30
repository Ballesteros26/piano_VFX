using System;
using System.Collections.Generic;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000023 RID: 35
	public class TMP_FontAssetUtilities
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000079E0 File Offset: 0x00005BE0
		public static TMP_FontAssetUtilities instance
		{
			get
			{
				return TMP_FontAssetUtilities.s_Instance;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000079E7 File Offset: 0x00005BE7
		public static TMP_Character GetCharacterFromFontAsset(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedFontAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedFontAssets = new List<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedFontAssets.Clear();
				}
			}
			return TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007A18 File Offset: 0x00005C18
		private static TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			isAlternativeTypeface = false;
			TMP_Character tmp_Character = null;
			bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			if (flag || fontWeight != FontWeight.Regular)
			{
				TMP_FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int num = 4;
				if (fontWeight <= FontWeight.Regular)
				{
					if (fontWeight <= FontWeight.ExtraLight)
					{
						if (fontWeight != FontWeight.Thin)
						{
							if (fontWeight == FontWeight.ExtraLight)
							{
								num = 2;
							}
						}
						else
						{
							num = 1;
						}
					}
					else if (fontWeight != FontWeight.Light)
					{
						if (fontWeight == FontWeight.Regular)
						{
							num = 4;
						}
					}
					else
					{
						num = 3;
					}
				}
				else if (fontWeight <= FontWeight.SemiBold)
				{
					if (fontWeight != FontWeight.Medium)
					{
						if (fontWeight == FontWeight.SemiBold)
						{
							num = 6;
						}
					}
					else
					{
						num = 5;
					}
				}
				else if (fontWeight != FontWeight.Bold)
				{
					if (fontWeight != FontWeight.Heavy)
					{
						if (fontWeight == FontWeight.Black)
						{
							num = 9;
						}
					}
					else
					{
						num = 8;
					}
				}
				else
				{
					num = 7;
				}
				fontAsset = (flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface);
				if (fontAsset != null)
				{
					if (fontAsset.characterLookupTable.TryGetValue(unicode, out tmp_Character))
					{
						isAlternativeTypeface = true;
						return tmp_Character;
					}
					if (fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && fontAsset.TryAddCharacterInternal(unicode, out tmp_Character))
					{
						isAlternativeTypeface = true;
						return tmp_Character;
					}
				}
			}
			if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out tmp_Character))
			{
				fontAsset = sourceFontAsset;
				return tmp_Character;
			}
			if (sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic && sourceFontAsset.TryAddCharacterInternal(unicode, out tmp_Character))
			{
				fontAsset = sourceFontAsset;
				return tmp_Character;
			}
			if (tmp_Character == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
			{
				List<TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
				int count = fallbackFontAssetTable.Count;
				if (fallbackFontAssetTable != null && count > 0)
				{
					int num2 = 0;
					while (num2 < count && tmp_Character == null)
					{
						TMP_FontAsset tmp_FontAsset = fallbackFontAssetTable[num2];
						if (!(tmp_FontAsset == null))
						{
							int instanceID = tmp_FontAsset.GetInstanceID();
							if (!TMP_FontAssetUtilities.k_SearchedFontAssets.Contains(instanceID))
							{
								TMP_FontAssetUtilities.k_SearchedFontAssets.Add(instanceID);
								tmp_Character = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, tmp_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
								if (tmp_Character != null)
								{
									return tmp_Character;
								}
							}
						}
						num2++;
					}
				}
			}
			return null;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007C04 File Offset: 0x00005E04
		public static TMP_Character GetCharacterFromFontAssets(uint unicode, List<TMP_FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface, out TMP_FontAsset fontAsset)
		{
			isAlternativeTypeface = false;
			if (fontAssets == null || fontAssets.Count == 0)
			{
				fontAsset = null;
				return null;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedFontAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedFontAssets = new List<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedFontAssets.Clear();
				}
			}
			int count = fontAssets.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(fontAssets[i] == null))
				{
					TMP_Character characterFromFontAsset_Internal = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAssets[i], includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, out fontAsset);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
			}
			fontAsset = null;
			return null;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007C88 File Offset: 0x00005E88
		private static bool TryGetCharacterFromFontFile(uint unicode, TMP_FontAsset fontAsset, out TMP_Character character)
		{
			character = null;
			if (!TMP_FontAssetUtilities.k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
			{
				TMP_FontAssetUtilities.k_IsFontEngineInitialized = true;
			}
			FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
			Glyph glyph = null;
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (fontAsset.glyphLookupTable.TryGetValue(glyphIndex, out glyph))
			{
				character = fontAsset.AddCharacter_Internal(unicode, glyph);
				return true;
			}
			GlyphLoadFlags glyphLoadFlags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : ((GlyphLoadFlags)6));
			if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, glyphLoadFlags, out glyph))
			{
				character = fontAsset.AddCharacter_Internal(unicode, glyph);
				return true;
			}
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007D14 File Offset: 0x00005F14
		public static bool TryGetGlyphFromFontFile(uint glyphIndex, TMP_FontAsset fontAsset, out Glyph glyph)
		{
			glyph = null;
			if (!TMP_FontAssetUtilities.k_IsFontEngineInitialized && FontEngine.InitializeFontEngine() == FontEngineError.Success)
			{
				TMP_FontAssetUtilities.k_IsFontEngineInitialized = true;
			}
			FontEngine.LoadFontFace(fontAsset.sourceFontFile, fontAsset.faceInfo.pointSize);
			GlyphLoadFlags glyphLoadFlags = (((fontAsset.atlasRenderMode & (GlyphRenderMode)8) == (GlyphRenderMode)8) ? GlyphLoadFlags.LOAD_RENDER : ((GlyphLoadFlags)6));
			return FontEngine.TryGetGlyphWithIndexValue(glyphIndex, glyphLoadFlags, out glyph);
		}

		// Token: 0x040000EE RID: 238
		private static readonly TMP_FontAssetUtilities s_Instance = new TMP_FontAssetUtilities();

		// Token: 0x040000EF RID: 239
		private static List<int> k_SearchedFontAssets;

		// Token: 0x040000F0 RID: 240
		private static bool k_IsFontEngineInitialized;
	}
}
