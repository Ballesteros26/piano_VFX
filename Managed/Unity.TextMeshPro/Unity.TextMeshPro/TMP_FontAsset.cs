using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class TMP_FontAsset : TMP_Asset
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004A10 File Offset: 0x00002C10
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004A18 File Offset: 0x00002C18
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004A21 File Offset: 0x00002C21
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004A29 File Offset: 0x00002C29
		public Font sourceFontFile
		{
			get
			{
				return this.m_SourceFontFile;
			}
			internal set
			{
				this.m_SourceFontFile = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004A32 File Offset: 0x00002C32
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004A3A File Offset: 0x00002C3A
		public AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return this.m_AtlasPopulationMode;
			}
			set
			{
				this.m_AtlasPopulationMode = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004A43 File Offset: 0x00002C43
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004A4B File Offset: 0x00002C4B
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			internal set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004A54 File Offset: 0x00002C54
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004A5C File Offset: 0x00002C5C
		public List<Glyph> glyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004A65 File Offset: 0x00002C65
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				if (this.m_GlyphLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004A7B File Offset: 0x00002C7B
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004A83 File Offset: 0x00002C83
		public List<TMP_Character> characterTable
		{
			get
			{
				return this.m_CharacterTable;
			}
			internal set
			{
				this.m_CharacterTable = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004A8C File Offset: 0x00002C8C
		public Dictionary<uint, TMP_Character> characterLookupTable
		{
			get
			{
				if (this.m_CharacterLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004AA2 File Offset: 0x00002CA2
		public Texture2D atlasTexture
		{
			get
			{
				if (this.m_AtlasTexture == null)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004AC6 File Offset: 0x00002CC6
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004AD5 File Offset: 0x00002CD5
		public Texture2D[] atlasTextures
		{
			get
			{
				Texture2D[] atlasTextures = this.m_AtlasTextures;
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004ADE File Offset: 0x00002CDE
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004AE8 File Offset: 0x00002CE8
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public bool isMultiAtlasTexturesEnabled
		{
			get
			{
				return this.m_IsMultiAtlasTexturesEnabled;
			}
			set
			{
				this.m_IsMultiAtlasTexturesEnabled = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004AF9 File Offset: 0x00002CF9
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004B01 File Offset: 0x00002D01
		internal List<GlyphRect> usedGlyphRects
		{
			get
			{
				return this.m_UsedGlyphRects;
			}
			set
			{
				this.m_UsedGlyphRects = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004B0A File Offset: 0x00002D0A
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004B12 File Offset: 0x00002D12
		internal List<GlyphRect> freeGlyphRects
		{
			get
			{
				return this.m_FreeGlyphRects;
			}
			set
			{
				this.m_FreeGlyphRects = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004B1B File Offset: 0x00002D1B
		[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public FaceInfo_Legacy fontInfo
		{
			get
			{
				return this.m_fontInfo;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004B23 File Offset: 0x00002D23
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004B2B File Offset: 0x00002D2B
		public int atlasWidth
		{
			get
			{
				return this.m_AtlasWidth;
			}
			internal set
			{
				this.m_AtlasWidth = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004B34 File Offset: 0x00002D34
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004B3C File Offset: 0x00002D3C
		public int atlasHeight
		{
			get
			{
				return this.m_AtlasHeight;
			}
			internal set
			{
				this.m_AtlasHeight = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004B45 File Offset: 0x00002D45
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004B4D File Offset: 0x00002D4D
		public int atlasPadding
		{
			get
			{
				return this.m_AtlasPadding;
			}
			internal set
			{
				this.m_AtlasPadding = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004B56 File Offset: 0x00002D56
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004B5E File Offset: 0x00002D5E
		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return this.m_AtlasRenderMode;
			}
			internal set
			{
				this.m_AtlasRenderMode = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004B67 File Offset: 0x00002D67
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004B6F File Offset: 0x00002D6F
		public TMP_FontFeatureTable fontFeatureTable
		{
			get
			{
				return this.m_FontFeatureTable;
			}
			internal set
			{
				this.m_FontFeatureTable = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004B78 File Offset: 0x00002D78
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004B80 File Offset: 0x00002D80
		public List<TMP_FontAsset> fallbackFontAssetTable
		{
			get
			{
				return this.m_FallbackFontAssetTable;
			}
			set
			{
				this.m_FallbackFontAssetTable = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004B89 File Offset: 0x00002D89
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004B91 File Offset: 0x00002D91
		public FontAssetCreationSettings creationSettings
		{
			get
			{
				return this.m_CreationSettings;
			}
			set
			{
				this.m_CreationSettings = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004B9A File Offset: 0x00002D9A
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004BA2 File Offset: 0x00002DA2
		public TMP_FontWeightPair[] fontWeightTable
		{
			get
			{
				return this.m_FontWeightTable;
			}
			internal set
			{
				this.m_FontWeightTable = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004BAB File Offset: 0x00002DAB
		public static TMP_FontAsset CreateFontAsset(Font font)
		{
			return TMP_FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			TMP_FontAsset tmp_FontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
			tmp_FontAsset.m_Version = "1.1.0";
			FontEngine.InitializeFontEngine();
			FontEngine.LoadFontFace(font, samplingPointSize);
			tmp_FontAsset.faceInfo = FontEngine.GetFaceInfo();
			if (atlasPopulationMode == AtlasPopulationMode.Dynamic)
			{
				tmp_FontAsset.sourceFontFile = font;
			}
			tmp_FontAsset.atlasPopulationMode = atlasPopulationMode;
			tmp_FontAsset.atlasWidth = atlasWidth;
			tmp_FontAsset.atlasHeight = atlasHeight;
			tmp_FontAsset.atlasPadding = atlasPadding;
			tmp_FontAsset.atlasRenderMode = renderMode;
			tmp_FontAsset.atlasTextures = new Texture2D[1];
			Texture2D texture2D = new Texture2D(0, 0, TextureFormat.Alpha8, false);
			tmp_FontAsset.atlasTextures[0] = texture2D;
			tmp_FontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			int num;
			if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
			{
				num = 0;
				Material material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				tmp_FontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(ShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(ShaderUtilities.ID_WeightNormal, tmp_FontAsset.normalStyle);
				material2.SetFloat(ShaderUtilities.ID_WeightBold, tmp_FontAsset.boldStyle);
				tmp_FontAsset.material = material2;
			}
			tmp_FontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			tmp_FontAsset.usedGlyphRects = new List<GlyphRect>(8);
			tmp_FontAsset.ReadFontAssetDefinition();
			return tmp_FontAsset;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D47 File Offset: 0x00002F47
		private void Awake()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeFontAsset();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D6C File Offset: 0x00002F6C
		internal void InitializeDictionaryLookupTables()
		{
			if (this.m_GlyphLookupDictionary == null)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			int count = this.m_GlyphTable.Count;
			if (this.m_GlyphIndexList == null)
			{
				this.m_GlyphIndexList = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexList.Clear();
			}
			for (int i = 0; i < count; i++)
			{
				Glyph glyph = this.m_GlyphTable[i];
				uint index = glyph.index;
				if (!this.m_GlyphLookupDictionary.ContainsKey(index))
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
					this.m_GlyphIndexList.Add(index);
				}
			}
			if (this.m_CharacterLookupDictionary == null)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int j = 0; j < this.m_CharacterTable.Count; j++)
			{
				TMP_Character tmp_Character = this.m_CharacterTable[j];
				uint unicode = tmp_Character.unicode;
				uint glyphIndex = tmp_Character.glyphIndex;
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
				}
				if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (this.m_KerningTable != null && this.m_KerningTable.kerningPairs != null && this.m_KerningTable.kerningPairs.Count > 0)
			{
				this.UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary == null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<uint, TMP_GlyphPairAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Clear();
			}
			List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords != null)
			{
				for (int k = 0; k < glyphPairAdjustmentRecords.Count; k++)
				{
					TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = glyphPairAdjustmentRecords[k];
					uint key = new GlyphPairKey(tmp_GlyphPairAdjustmentRecord).key;
					if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
					{
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tmp_GlyphPairAdjustmentRecord);
					}
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004F70 File Offset: 0x00003170
		public void ReadFontAssetDefinition()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeFontAsset();
			}
			this.InitializeDictionaryLookupTables();
			if (!this.m_CharacterLookupDictionary.ContainsKey(9U))
			{
				Glyph glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, this.m_FaceInfo.tabWidth * (float)this.tabSize), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(9U, new TMP_Character(9U, glyph));
			}
			if (!this.m_CharacterLookupDictionary.ContainsKey(10U))
			{
				Glyph glyph2 = new Glyph(0U, new GlyphMetrics(10f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(10U, new TMP_Character(10U, glyph2));
				if (!this.m_CharacterLookupDictionary.ContainsKey(11U))
				{
					this.m_CharacterLookupDictionary.Add(11U, new TMP_Character(11U, glyph2));
				}
				if (!this.m_CharacterLookupDictionary.ContainsKey(13U))
				{
					this.m_CharacterLookupDictionary.Add(13U, new TMP_Character(13U, glyph2));
				}
				if (!this.m_CharacterLookupDictionary.ContainsKey(3U))
				{
					this.m_CharacterLookupDictionary.Add(3U, new TMP_Character(3U, glyph2));
				}
			}
			if (!this.m_CharacterLookupDictionary.ContainsKey(8203U))
			{
				Glyph glyph3 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(8203U, new TMP_Character(8203U, glyph3));
			}
			if (!this.m_CharacterLookupDictionary.ContainsKey(8288U))
			{
				Glyph glyph4 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(8288U, new TMP_Character(8288U, glyph4));
			}
			TMP_Character tmp_Character;
			if (!this.m_CharacterLookupDictionary.ContainsKey(8209U) && this.m_CharacterLookupDictionary.TryGetValue(45U, out tmp_Character))
			{
				this.m_CharacterLookupDictionary.Add(8209U, new TMP_Character(8209U, tmp_Character.glyph));
			}
			if (this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(72U))
			{
				uint glyphIndex = this.m_CharacterLookupDictionary[72U].glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.scale == 0f)
			{
				this.m_FaceInfo.scale = 1f;
			}
			if (this.m_FaceInfo.strikethroughOffset == 0f)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			if (this.m_AtlasPadding == 0 && this.material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				this.m_AtlasPadding = (int)this.material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
			}
			this.hashCode = TMP_TextUtilities.GetSimpleHashCode(base.name);
			this.materialHashCode = TMP_TextUtilities.GetSimpleHashCode(this.material.name);
			this.m_IsFontAssetLookupTablesDirty = false;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000052D0 File Offset: 0x000034D0
		internal void SortCharacterTable()
		{
			if (this.m_CharacterTable != null && this.m_CharacterTable.Count > 0)
			{
				this.m_CharacterTable = this.m_CharacterTable.OrderBy((TMP_Character c) => c.unicode).ToList<TMP_Character>();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005328 File Offset: 0x00003528
		internal void SortGlyphTable()
		{
			if (this.m_GlyphTable != null && this.m_GlyphTable.Count > 0)
			{
				this.m_GlyphTable = this.m_GlyphTable.OrderBy((Glyph c) => c.index).ToList<Glyph>();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005380 File Offset: 0x00003580
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000538E File Offset: 0x0000358E
		public bool HasCharacter(int character)
		{
			return this.m_CharacterLookupDictionary != null && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000538E File Offset: 0x0000358E
		public bool HasCharacter(char character)
		{
			return this.m_CharacterLookupDictionary != null && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000053AC File Offset: 0x000035AC
		public bool HasCharacter(char character, bool searchFallbacks)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey((uint)character))
			{
				return true;
			}
			TMP_Character tmp_Character;
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic && this.TryAddCharacterInternal((uint)character, out tmp_Character))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
				{
					int num = 0;
					while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
					{
						if (this.fallbackFontAssetTable[num].HasCharacter_Internal((uint)character, searchFallbacks))
						{
							return true;
						}
						num++;
					}
				}
				if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					int num2 = 0;
					while (num2 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num2] != null)
					{
						if (TMP_Settings.fallbackFontAssets[num2].m_CharacterLookupDictionary == null)
						{
							TMP_Settings.fallbackFontAssets[num2].ReadFontAssetDefinition();
						}
						if (TMP_Settings.fallbackFontAssets[num2].m_CharacterLookupDictionary != null && TMP_Settings.fallbackFontAssets[num2].HasCharacter_Internal((uint)character, searchFallbacks))
						{
							return true;
						}
						num2++;
					}
				}
				if (TMP_Settings.defaultFontAsset != null)
				{
					if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary == null)
					{
						TMP_Settings.defaultFontAsset.ReadFontAssetDefinition();
					}
					if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary != null && TMP_Settings.defaultFontAsset.HasCharacter_Internal((uint)character, searchFallbacks))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000551C File Offset: 0x0000371C
		private bool HasCharacter_Internal(uint character, bool searchFallbacks)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			if (searchFallbacks && this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
			{
				int num = 0;
				while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
				{
					if (this.fallbackFontAssetTable[num].HasCharacter_Internal(character, searchFallbacks))
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000055AC File Offset: 0x000037AC
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					missingCharacters.Add(text[i]);
				}
			}
			return missingCharacters.Count == 0;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000560C File Offset: 0x0000380C
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false)
		{
			missingCharacters = null;
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			TMP_FontAsset.s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = true;
				uint num = (uint)text[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num))
				{
					if (searchFallbacks)
					{
						if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
						{
							int num2 = 0;
							while (num2 < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num2] != null)
							{
								if (this.fallbackFontAssetTable[num2].HasCharacter_Internal(num, searchFallbacks))
								{
									flag = false;
									break;
								}
								num2++;
							}
						}
						if (flag && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
						{
							int num3 = 0;
							while (num3 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num3] != null)
							{
								if (TMP_Settings.fallbackFontAssets[num3].m_CharacterLookupDictionary == null)
								{
									TMP_Settings.fallbackFontAssets[num3].ReadFontAssetDefinition();
								}
								if (TMP_Settings.fallbackFontAssets[num3].m_CharacterLookupDictionary != null && TMP_Settings.fallbackFontAssets[num3].HasCharacter_Internal(num, searchFallbacks))
								{
									flag = false;
									break;
								}
								num3++;
							}
						}
						if (flag && TMP_Settings.defaultFontAsset != null)
						{
							if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary == null)
							{
								TMP_Settings.defaultFontAsset.ReadFontAssetDefinition();
							}
							if (TMP_Settings.defaultFontAsset.m_CharacterLookupDictionary != null && TMP_Settings.defaultFontAsset.HasCharacter_Internal(num, searchFallbacks))
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						TMP_FontAsset.s_MissingCharacterList.Add(num);
					}
				}
			}
			if (TMP_FontAsset.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = TMP_FontAsset.s_MissingCharacterList.ToArray();
				return false;
			}
			return true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000057D8 File Offset: 0x000039D8
		public bool HasCharacters(string text)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005818 File Offset: 0x00003A18
		public static string GetCharacters(TMP_FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005864 File Offset: 0x00003A64
		public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000058B0 File Offset: 0x00003AB0
		public bool TryAddCharacters(uint[] unicodes)
		{
			uint[] array;
			return this.TryAddCharacters(unicodes, out array);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000058C8 File Offset: 0x00003AC8
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes)
		{
			if (unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = unicodes.ToArray<uint>();
				return false;
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				missingUnicodes = unicodes.ToArray<uint>();
				return false;
			}
			TMP_FontAsset.s_GlyphsToAdd.Clear();
			TMP_FontAsset.s_GlyphsToAddLookup.Clear();
			TMP_FontAsset.s_CharactersToAdd.Clear();
			TMP_FontAsset.s_CharactersToAddLookup.Clear();
			TMP_FontAsset.s_MissingCharacterList.Clear();
			bool flag = false;
			int num = unicodes.Length;
			for (int i = 0; i < num; i++)
			{
				uint num2 = unicodes[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num2))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(num2);
					if (glyphIndex == 0U)
					{
						TMP_FontAsset.s_MissingCharacterList.Add(num2);
						flag = true;
					}
					else
					{
						TMP_Character tmp_Character = new TMP_Character(num2, glyphIndex);
						if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
						{
							tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
							this.m_CharacterTable.Add(tmp_Character);
							this.m_CharacterLookupDictionary.Add(num2, tmp_Character);
						}
						else
						{
							if (!TMP_FontAsset.s_GlyphsToAddLookup.Contains(glyphIndex))
							{
								TMP_FontAsset.s_GlyphsToAddLookup.Add(glyphIndex);
								TMP_FontAsset.s_GlyphsToAdd.Add(glyphIndex);
							}
							if (!TMP_FontAsset.s_CharactersToAddLookup.Contains(num2))
							{
								TMP_FontAsset.s_CharactersToAddLookup.Add(num2);
								TMP_FontAsset.s_CharactersToAdd.Add(tmp_Character);
							}
						}
					}
				}
			}
			if (TMP_FontAsset.s_GlyphsToAdd.Count == 0)
			{
				missingUnicodes = unicodes;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(TMP_FontAsset.s_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num3 = 0;
			while (num3 < array.Length && array[num3] != null)
			{
				Glyph glyph = array[num3];
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyph.index, glyph);
				num3++;
			}
			TMP_FontAsset.s_GlyphsToAdd.Clear();
			for (int j = 0; j < TMP_FontAsset.s_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = TMP_FontAsset.s_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					TMP_FontAsset.s_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					TMP_FontAsset.s_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			for (int k = 0; k < TMP_FontAsset.s_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = TMP_FontAsset.s_CharactersToAdd[k];
				TMP_FontAsset.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			missingUnicodes = null;
			if (TMP_FontAsset.s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = TMP_FontAsset.s_MissingCharacterList.ToArray();
			}
			return flag2 && !flag;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005C68 File Offset: 0x00003E68
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string text;
			return this.TryAddCharacters(characters, out text, includeFontFeatures);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005C80 File Offset: 0x00003E80
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			if (string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				missingCharacters = characters;
				return false;
			}
			TMP_FontAsset.s_GlyphsToAdd.Clear();
			TMP_FontAsset.s_GlyphsToAddLookup.Clear();
			TMP_FontAsset.s_CharactersToAdd.Clear();
			TMP_FontAsset.s_CharactersToAddLookup.Clear();
			TMP_FontAsset.s_MissingCharacterList.Clear();
			bool flag = false;
			int length = characters.Length;
			for (int i = 0; i < length; i++)
			{
				uint num = (uint)characters[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(num);
					if (glyphIndex == 0U)
					{
						TMP_FontAsset.s_MissingCharacterList.Add(num);
						flag = true;
					}
					else
					{
						TMP_Character tmp_Character = new TMP_Character(num, glyphIndex);
						if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
						{
							tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
							this.m_CharacterTable.Add(tmp_Character);
							this.m_CharacterLookupDictionary.Add(num, tmp_Character);
						}
						else
						{
							if (!TMP_FontAsset.s_GlyphsToAddLookup.Contains(glyphIndex))
							{
								TMP_FontAsset.s_GlyphsToAddLookup.Add(glyphIndex);
								TMP_FontAsset.s_GlyphsToAdd.Add(glyphIndex);
							}
							if (!TMP_FontAsset.s_CharactersToAddLookup.Contains(num))
							{
								TMP_FontAsset.s_CharactersToAddLookup.Add(num);
								TMP_FontAsset.s_CharactersToAdd.Add(tmp_Character);
							}
						}
					}
				}
			}
			if (TMP_FontAsset.s_GlyphsToAdd.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(TMP_FontAsset.s_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num2 = 0;
			while (num2 < array.Length && array[num2] != null)
			{
				Glyph glyph = array[num2];
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyph.index, glyph);
				num2++;
			}
			TMP_FontAsset.s_GlyphsToAdd.Clear();
			for (int j = 0; j < TMP_FontAsset.s_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = TMP_FontAsset.s_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					TMP_FontAsset.s_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					TMP_FontAsset.s_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			missingCharacters = string.Empty;
			for (int k = 0; k < TMP_FontAsset.s_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = TMP_FontAsset.s_CharactersToAdd[k];
				TMP_FontAsset.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			if (TMP_FontAsset.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = TMP_FontAsset.s_MissingCharacterList.UintToString();
			}
			return flag2 && !flag;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006020 File Offset: 0x00004220
		internal bool TryAddCharacter_Internal(uint unicode)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return true;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				return false;
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				TMP_Character tmp_Character = new TMP_Character(unicode, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(tmp_Character);
				this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
				return true;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph glyph;
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				TMP_Character tmp_Character = new TMP_Character(unicode, glyph);
				this.m_CharacterTable.Add(tmp_Character);
				this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
				return true;
			}
			return false;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006150 File Offset: 0x00004350
		internal TMP_Character AddCharacter_Internal(uint unicode, Glyph glyph)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return this.m_CharacterLookupDictionary[unicode];
			}
			uint index = glyph.index;
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (!this.m_GlyphLookupDictionary.ContainsKey(index))
			{
				if (glyph.glyphRect.width == 0 || glyph.glyphRect.width == 0)
				{
					this.m_GlyphTable.Add(glyph);
				}
				else
				{
					if (!FontEngine.TryPackGlyphInAtlas(glyph, this.m_AtlasPadding, GlyphPackingMode.ContactPointRule, this.m_AtlasRenderMode, this.m_AtlasWidth, this.m_AtlasHeight, this.m_FreeGlyphRects, this.m_UsedGlyphRects))
					{
						return null;
					}
					this.m_GlyphsToRender.Add(glyph);
				}
			}
			TMP_Character tmp_Character = new TMP_Character(unicode, glyph);
			this.m_CharacterTable.Add(tmp_Character);
			this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
			this.UpdateAtlasTexture();
			return tmp_Character;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006278 File Offset: 0x00004478
		internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				this.m_MissingUnicodesFromFontFile.Add(unicode);
				return false;
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				if (TMP_Settings.getFontFeaturesAtRuntime && TMP_FontAsset.k_FontAssetsToUpdateLookup.Add(base.instanceID))
				{
					TMP_FontAsset.k_FontAssetsToUpdate.Add(this);
				}
				return true;
			}
			Glyph glyph = null;
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Unable to add the requested character to font asset [",
						base.name,
						"]'s atlas texture. Please make the texture [",
						this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
						"] readable."
					}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
					return false;
				}
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(glyphIndex);
				if (TMP_Settings.getFontFeaturesAtRuntime && TMP_FontAsset.k_FontAssetsToUpdateLookup.Add(base.instanceID))
				{
					TMP_FontAsset.k_FontAssetsToUpdate.Add(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					character = new TMP_Character(unicode, glyph);
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(unicode, character);
					this.m_GlyphIndexList.Add(glyphIndex);
					if (TMP_Settings.getFontFeaturesAtRuntime && TMP_FontAsset.k_FontAssetsToUpdateLookup.Add(base.instanceID))
					{
						TMP_FontAsset.k_FontAssetsToUpdate.Add(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006560 File Offset: 0x00004760
		private bool TryAddGlyphsToNewAtlasTexture()
		{
			this.SetupNewAtlasTexture();
			Glyph[] array;
			bool flag = FontEngine.TryAddGlyphsToTexture(TMP_FontAsset.s_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				Glyph glyph = array[num];
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyph.index, glyph);
				num++;
			}
			TMP_FontAsset.s_GlyphsToAdd.Clear();
			for (int i = 0; i < TMP_FontAsset.s_CharactersToAdd.Count; i++)
			{
				TMP_Character tmp_Character = TMP_FontAsset.s_CharactersToAdd[i];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character.glyphIndex, out glyph2))
				{
					TMP_FontAsset.s_GlyphsToAdd.Add(tmp_Character.glyphIndex);
				}
				else
				{
					tmp_Character.glyph = glyph2;
					this.m_CharacterTable.Add(tmp_Character);
					this.m_CharacterLookupDictionary.Add(tmp_Character.unicode, tmp_Character);
					TMP_FontAsset.s_CharactersToAdd.RemoveAt(i);
					i--;
				}
			}
			return flag;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006684 File Offset: 0x00004884
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			if (this.m_AtlasTextures.Length == this.m_AtlasTextureIndex)
			{
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, this.m_AtlasTextures.Length * 2);
			}
			this.m_AtlasTextures[this.m_AtlasTextureIndex] = new Texture2D(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
			FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
			this.m_FreeGlyphRects.Clear();
			this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			this.m_UsedGlyphRects.Clear();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006740 File Offset: 0x00004940
		internal uint GetGlyphIndex(uint unicode)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return this.m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			if (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetGlyphIndex(unicode);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006790 File Offset: 0x00004990
		internal void UpdateAtlasTexture()
		{
			if (this.m_GlyphsToRender.Count == 0)
			{
				return;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			FontEngine.RenderGlyphsToTexture(this.m_GlyphsToRender, this.m_AtlasPadding, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			this.m_AtlasTextures[this.m_AtlasTextureIndex].Apply(false, false);
			for (int i = 0; i < this.m_GlyphsToRender.Count; i++)
			{
				Glyph glyph = this.m_GlyphsToRender[i];
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyph.index, glyph);
			}
			this.m_GlyphsPacked.Clear();
			this.m_GlyphsToRender.Clear();
			int count = this.m_GlyphsToPack.Count;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000068B0 File Offset: 0x00004AB0
		public static void UpdateFontAssets()
		{
			int count = TMP_FontAsset.k_FontAssetsToUpdate.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssetsToUpdate[i].UpdateGlyphAdjustmentRecords();
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssetsToUpdate.Clear();
				TMP_FontAsset.k_FontAssetsToUpdateLookup.Clear();
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000068FC File Offset: 0x00004AFC
		internal void UpdateGlyphAdjustmentRecords()
		{
			int count = this.m_GlyphIndexList.Count;
			if (TMP_FontAsset.s_GlyphIndexArray.Length < count)
			{
				TMP_FontAsset.s_GlyphIndexArray = new uint[Mathf.NextPowerOfTwo(count + 1)];
			}
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.s_GlyphIndexArray[i] = this.m_GlyphIndexList[i];
			}
			Array.Clear(TMP_FontAsset.s_GlyphIndexArray, count, TMP_FontAsset.s_GlyphIndexArray.Length - count);
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(TMP_FontAsset.s_GlyphIndexArray);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int num = 0;
			while (num < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex != 0U)
			{
				uint num2 = (glyphPairAdjustmentTable[num].secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(num2))
				{
					TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(glyphPairAdjustmentTable[num]);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tmp_GlyphPairAdjustmentRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(num2, tmp_GlyphPairAdjustmentRecord);
				}
				num++;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006A2C File Offset: 0x00004C2C
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			if (this.m_GlyphTable != null)
			{
				this.m_GlyphTable.Clear();
			}
			if (this.m_CharacterTable != null)
			{
				this.m_CharacterTable.Clear();
			}
			if (this.m_UsedGlyphRects != null)
			{
				this.m_UsedGlyphRects.Clear();
			}
			if (this.m_FreeGlyphRects != null)
			{
				int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			}
			if (this.m_GlyphsToPack != null)
			{
				this.m_GlyphsToPack.Clear();
			}
			if (this.m_GlyphsPacked != null)
			{
				this.m_GlyphsPacked.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
			}
			this.m_AtlasTextureIndex = 0;
			if (this.m_AtlasTextures != null)
			{
				for (int i = 0; i < this.m_AtlasTextures.Length; i++)
				{
					Texture2D texture2D = this.m_AtlasTextures[i];
					if (i > 0)
					{
						global::UnityEngine.Object.DestroyImmediate(texture2D, true);
					}
					if (!(texture2D == null))
					{
						if (!this.m_AtlasTextures[i].isReadable)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Unable to reset font asset [",
								base.name,
								"]'s atlas texture. Please make the texture [",
								this.m_AtlasTextures[i].name,
								"] readable."
							}), this.m_AtlasTextures[i]);
						}
						else
						{
							if (setAtlasSizeToZero)
							{
								texture2D.Resize(0, 0, TextureFormat.Alpha8, false);
							}
							else if (texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight)
							{
								texture2D.Resize(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
							}
							FontEngine.ResetAtlasTexture(texture2D);
							texture2D.Apply();
							if (i == 0)
							{
								this.m_AtlasTexture = texture2D;
							}
							this.m_AtlasTextures[i] = texture2D;
						}
					}
				}
			}
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006C10 File Offset: 0x00004E10
		internal void UpgradeFontAsset()
		{
			this.m_Version = "1.1.0";
			Debug.Log(string.Concat(new string[] { "Upgrading font asset [", base.name, "] to version ", this.m_Version, "." }), this);
			this.m_FaceInfo.familyName = this.m_fontInfo.Name;
			this.m_FaceInfo.styleName = string.Empty;
			this.m_FaceInfo.pointSize = (int)this.m_fontInfo.PointSize;
			this.m_FaceInfo.scale = this.m_fontInfo.Scale;
			this.m_FaceInfo.lineHeight = this.m_fontInfo.LineHeight;
			this.m_FaceInfo.ascentLine = this.m_fontInfo.Ascender;
			this.m_FaceInfo.capLine = this.m_fontInfo.CapHeight;
			this.m_FaceInfo.meanLine = this.m_fontInfo.CenterLine;
			this.m_FaceInfo.baseline = this.m_fontInfo.Baseline;
			this.m_FaceInfo.descentLine = this.m_fontInfo.Descender;
			this.m_FaceInfo.superscriptOffset = this.m_fontInfo.SuperscriptOffset;
			this.m_FaceInfo.superscriptSize = this.m_fontInfo.SubSize;
			this.m_FaceInfo.subscriptOffset = this.m_fontInfo.SubscriptOffset;
			this.m_FaceInfo.subscriptSize = this.m_fontInfo.SubSize;
			this.m_FaceInfo.underlineOffset = this.m_fontInfo.Underline;
			this.m_FaceInfo.underlineThickness = this.m_fontInfo.UnderlineThickness;
			this.m_FaceInfo.strikethroughOffset = this.m_fontInfo.strikethrough;
			this.m_FaceInfo.strikethroughThickness = this.m_fontInfo.strikethroughThickness;
			this.m_FaceInfo.tabWidth = this.m_fontInfo.TabWidth;
			if (this.m_AtlasTextures == null || this.m_AtlasTextures.Length == 0)
			{
				this.m_AtlasTextures = new Texture2D[1];
			}
			this.m_AtlasTextures[0] = this.atlas;
			this.m_AtlasWidth = (int)this.m_fontInfo.AtlasWidth;
			this.m_AtlasHeight = (int)this.m_fontInfo.AtlasHeight;
			this.m_AtlasPadding = (int)this.m_fontInfo.Padding;
			switch (this.m_CreationSettings.renderMode)
			{
			case 0:
				this.m_AtlasRenderMode = GlyphRenderMode.SMOOTH_HINTED;
				break;
			case 1:
				this.m_AtlasRenderMode = GlyphRenderMode.SMOOTH;
				break;
			case 2:
				this.m_AtlasRenderMode = GlyphRenderMode.RASTER_HINTED;
				break;
			case 3:
				this.m_AtlasRenderMode = GlyphRenderMode.RASTER;
				break;
			case 6:
				this.m_AtlasRenderMode = GlyphRenderMode.SDF16;
				break;
			case 7:
				this.m_AtlasRenderMode = GlyphRenderMode.SDF32;
				break;
			}
			if (this.fontWeights != null && this.fontWeights.Length != 0)
			{
				this.m_FontWeightTable[4] = this.fontWeights[4];
				this.m_FontWeightTable[7] = this.fontWeights[7];
			}
			if (this.fallbackFontAssets != null && this.fallbackFontAssets.Count > 0)
			{
				if (this.m_FallbackFontAssetTable == null)
				{
					this.m_FallbackFontAssetTable = new List<TMP_FontAsset>(this.fallbackFontAssets.Count);
				}
				for (int i = 0; i < this.fallbackFontAssets.Count; i++)
				{
					this.m_FallbackFontAssetTable.Add(this.fallbackFontAssets[i]);
				}
			}
			if (this.m_CreationSettings.sourceFontFileGUID != null || this.m_CreationSettings.sourceFontFileGUID != string.Empty)
			{
				this.m_SourceFontFileGUID = this.m_CreationSettings.sourceFontFileGUID;
			}
			else
			{
				Debug.LogWarning("Font asset [" + base.name + "] doesn't have a reference to its source font file. Please assign the appropriate source font file for this asset in the Font Atlas & Material section of font asset inspector.", this);
			}
			this.m_GlyphTable.Clear();
			this.m_CharacterTable.Clear();
			bool flag = false;
			for (int j = 0; j < this.m_glyphInfoList.Count; j++)
			{
				TMP_Glyph tmp_Glyph = this.m_glyphInfoList[j];
				Glyph glyph = new Glyph();
				uint num = (uint)(j + 1);
				glyph.index = num;
				glyph.glyphRect = new GlyphRect((int)tmp_Glyph.x, this.m_AtlasHeight - (int)(tmp_Glyph.y + tmp_Glyph.height + 0.5f), (int)(tmp_Glyph.width + 0.5f), (int)(tmp_Glyph.height + 0.5f));
				glyph.metrics = new GlyphMetrics(tmp_Glyph.width, tmp_Glyph.height, tmp_Glyph.xOffset, tmp_Glyph.yOffset, tmp_Glyph.xAdvance);
				glyph.scale = tmp_Glyph.scale;
				glyph.atlasIndex = 0;
				this.m_GlyphTable.Add(glyph);
				TMP_Character tmp_Character = new TMP_Character((uint)tmp_Glyph.id, glyph);
				if (tmp_Glyph.id == 32)
				{
					flag = true;
				}
				this.m_CharacterTable.Add(tmp_Character);
			}
			if (!flag)
			{
				Debug.Log("Synthesizing Space for [" + base.name + "]");
				Glyph glyph2 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, this.m_FaceInfo.ascentLine / 5f), GlyphRect.zero, 1f, 0);
				this.m_GlyphTable.Add(glyph2);
				this.m_CharacterTable.Add(new TMP_Character(32U, glyph2));
			}
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007178 File Offset: 0x00005378
		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int count = this.m_KerningTable.kerningPairs.Count;
			this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>(count);
			for (int i = 0; i < count; i++)
			{
				KerningPair kerningPair = this.m_KerningTable.kerningPairs[i];
				uint num = 0U;
				TMP_Character tmp_Character;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out tmp_Character))
				{
					num = tmp_Character.glyphIndex;
				}
				uint num2 = 0U;
				TMP_Character tmp_Character2;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out tmp_Character2))
				{
					num2 = tmp_Character2.glyphIndex;
				}
				TMP_GlyphAdjustmentRecord tmp_GlyphAdjustmentRecord = new TMP_GlyphAdjustmentRecord(num, new TMP_GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
				TMP_GlyphAdjustmentRecord tmp_GlyphAdjustmentRecord2 = new TMP_GlyphAdjustmentRecord(num2, new TMP_GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
				TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(tmp_GlyphAdjustmentRecord, tmp_GlyphAdjustmentRecord2);
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tmp_GlyphPairAdjustmentRecord);
			}
			this.m_KerningTable.kerningPairs = null;
			this.m_KerningTable = null;
		}

		// Token: 0x04000085 RID: 133
		[SerializeField]
		private string m_Version;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x0400008B RID: 139
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		internal List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

		// Token: 0x0400008D RID: 141
		internal Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

		// Token: 0x0400008E RID: 142
		internal Texture2D m_AtlasTexture;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private FaceInfo_Legacy m_fontInfo;

		// Token: 0x04000095 RID: 149
		[SerializeField]
		public Texture2D atlas;

		// Token: 0x04000096 RID: 150
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x04000097 RID: 151
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x04000099 RID: 153
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		internal List<TMP_Glyph> m_glyphInfoList;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		[FormerlySerializedAs("m_kerningInfo")]
		internal KerningTable m_KerningTable = new KerningTable();

		// Token: 0x0400009C RID: 156
		[SerializeField]
		internal TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private List<TMP_FontAsset> fallbackFontAssets;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		internal List<TMP_FontAsset> m_FallbackFontAssetTable;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		internal FontAssetCreationSettings m_CreationSettings;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private TMP_FontWeightPair[] fontWeights;

		// Token: 0x040000A2 RID: 162
		public float normalStyle;

		// Token: 0x040000A3 RID: 163
		public float normalSpacingOffset;

		// Token: 0x040000A4 RID: 164
		public float boldStyle = 0.75f;

		// Token: 0x040000A5 RID: 165
		public float boldSpacing = 7f;

		// Token: 0x040000A6 RID: 166
		public byte italicStyle = 35;

		// Token: 0x040000A7 RID: 167
		public byte tabSize = 10;

		// Token: 0x040000A8 RID: 168
		private byte m_oldTabSize;

		// Token: 0x040000A9 RID: 169
		internal bool m_IsFontAssetLookupTablesDirty;

		// Token: 0x040000AA RID: 170
		private static List<TMP_FontAsset> k_FontAssetsToUpdate = new List<TMP_FontAsset>();

		// Token: 0x040000AB RID: 171
		private static HashSet<int> k_FontAssetsToUpdateLookup = new HashSet<int>();

		// Token: 0x040000AC RID: 172
		private List<Glyph> m_GlyphsToPack = new List<Glyph>();

		// Token: 0x040000AD RID: 173
		private List<Glyph> m_GlyphsPacked = new List<Glyph>();

		// Token: 0x040000AE RID: 174
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x040000AF RID: 175
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x040000B0 RID: 176
		internal static uint[] s_GlyphIndexArray = new uint[16];

		// Token: 0x040000B1 RID: 177
		internal static List<uint> s_GlyphsToAdd = new List<uint>(16);

		// Token: 0x040000B2 RID: 178
		internal static HashSet<uint> s_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x040000B3 RID: 179
		internal static List<TMP_Character> s_CharactersToAdd = new List<TMP_Character>();

		// Token: 0x040000B4 RID: 180
		internal static HashSet<uint> s_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x040000B5 RID: 181
		internal static List<uint> s_MissingCharacterList = new List<uint>(16);

		// Token: 0x040000B6 RID: 182
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();
	}
}
