using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class FontAsset : ScriptableObject
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000281C File Offset: 0x00000A1C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002834 File Offset: 0x00000A34
		public string version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002840 File Offset: 0x00000A40
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002858 File Offset: 0x00000A58
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002864 File Offset: 0x00000A64
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000287C File Offset: 0x00000A7C
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002888 File Offset: 0x00000A88
		public Font sourceFontFile
		{
			get
			{
				return this.m_SourceFontFile;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000028B8 File Offset: 0x00000AB8
		public FontAsset.AtlasPopulationMode atlasPopulationMode
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000028DC File Offset: 0x00000ADC
		public List<Glyph> glyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028E8 File Offset: 0x00000AE8
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				bool flag = this.m_GlyphLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002914 File Offset: 0x00000B14
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000292C File Offset: 0x00000B2C
		public List<Character> characterTable
		{
			get
			{
				return this.m_CharacterTable;
			}
			set
			{
				this.m_CharacterTable = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002938 File Offset: 0x00000B38
		public Dictionary<uint, Character> characterLookupTable
		{
			get
			{
				bool flag = this.m_CharacterLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002964 File Offset: 0x00000B64
		public Texture2D atlasTexture
		{
			get
			{
				bool flag = this.m_AtlasTexture == null;
				if (flag)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000299C File Offset: 0x00000B9C
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000029C3 File Offset: 0x00000BC3
		public Texture2D[] atlasTextures
		{
			get
			{
				bool flag = this.m_AtlasTextures == null;
				if (flag)
				{
				}
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000029E8 File Offset: 0x00000BE8
		public int atlasWidth
		{
			get
			{
				return this.m_AtlasWidth;
			}
			set
			{
				this.m_AtlasWidth = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002A0C File Offset: 0x00000C0C
		public int atlasHeight
		{
			get
			{
				return this.m_AtlasHeight;
			}
			set
			{
				this.m_AtlasHeight = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A18 File Offset: 0x00000C18
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002A30 File Offset: 0x00000C30
		public int atlasPadding
		{
			get
			{
				return this.m_AtlasPadding;
			}
			set
			{
				this.m_AtlasPadding = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002A54 File Offset: 0x00000C54
		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return this.m_AtlasRenderMode;
			}
			set
			{
				this.m_AtlasRenderMode = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002A78 File Offset: 0x00000C78
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A84 File Offset: 0x00000C84
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A9C File Offset: 0x00000C9C
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
				this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public int materialHashCode
		{
			get
			{
				return this.m_MaterialHashCode;
			}
			set
			{
				bool flag = this.m_MaterialHashCode == 0;
				if (flag)
				{
					this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
				}
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002B48 File Offset: 0x00000D48
		public KerningTable kerningTable
		{
			get
			{
				return this.m_KerningTable;
			}
			set
			{
				this.m_KerningTable = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002B54 File Offset: 0x00000D54
		public Dictionary<int, KerningPair> kerningLookupDictionary
		{
			get
			{
				return this.m_KerningLookupDictionary;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002B84 File Offset: 0x00000D84
		public List<FontAsset> fallbackFontAssetTable
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B90 File Offset: 0x00000D90
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public FontAssetCreationSettings fontAssetCreationSettings
		{
			get
			{
				return this.m_FontAssetCreationSettings;
			}
			set
			{
				this.m_FontAssetCreationSettings = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002BCC File Offset: 0x00000DCC
		public FontWeights[] fontWeightTable
		{
			get
			{
				return this.m_FontWeightTable;
			}
			set
			{
				this.m_FontWeightTable = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BD8 File Offset: 0x00000DD8
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public float regularStyleWeight
		{
			get
			{
				return this.m_RegularStyleWeight;
			}
			set
			{
				this.m_RegularStyleWeight = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002BFC File Offset: 0x00000DFC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002C14 File Offset: 0x00000E14
		public float regularStyleSpacing
		{
			get
			{
				return this.m_RegularStyleSpacing;
			}
			set
			{
				this.m_RegularStyleSpacing = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002C20 File Offset: 0x00000E20
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002C38 File Offset: 0x00000E38
		public float boldStyleWeight
		{
			get
			{
				return this.m_BoldStyleWeight;
			}
			set
			{
				this.m_BoldStyleWeight = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002C5C File Offset: 0x00000E5C
		public float boldStyleSpacing
		{
			get
			{
				return this.m_BoldStyleSpacing;
			}
			set
			{
				this.m_BoldStyleSpacing = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C68 File Offset: 0x00000E68
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002C80 File Offset: 0x00000E80
		public byte italicStyleSlant
		{
			get
			{
				return this.m_ItalicStyleSlant;
			}
			set
			{
				this.m_ItalicStyleSlant = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C8C File Offset: 0x00000E8C
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public byte tabMultiple
		{
			get
			{
				return this.m_TabMultiple;
			}
			set
			{
				this.m_TabMultiple = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static FontAsset CreateFontAsset(Font font)
		{
			return FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, FontAsset.AtlasPopulationMode.Dynamic);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002CDC File Offset: 0x00000EDC
		public static FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, FontAsset.AtlasPopulationMode atlasPopulationMode = FontAsset.AtlasPopulationMode.Dynamic)
		{
			FontAsset fontAsset = ScriptableObject.CreateInstance<FontAsset>();
			FontEngine.InitializeFontEngine();
			FontEngine.LoadFontFace(font, samplingPointSize);
			fontAsset.faceInfo = FontEngine.GetFaceInfo();
			bool flag = atlasPopulationMode == FontAsset.AtlasPopulationMode.Dynamic;
			if (flag)
			{
				fontAsset.m_SourceFontFile = font;
			}
			fontAsset.atlasPopulationMode = atlasPopulationMode;
			fontAsset.atlasWidth = atlasWidth;
			fontAsset.atlasHeight = atlasHeight;
			fontAsset.atlasPadding = atlasPadding;
			fontAsset.atlasRenderMode = renderMode;
			fontAsset.atlasTextures = new Texture2D[1];
			Texture2D texture2D = new Texture2D(0, 0, TextureFormat.Alpha8, false);
			fontAsset.atlasTextures[0] = texture2D;
			bool flag2 = (renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16;
			int num;
			if (flag2)
			{
				num = 0;
				Material material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				fontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(ShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(ShaderUtilities.ID_WeightNormal, fontAsset.regularStyleWeight);
				material2.SetFloat(ShaderUtilities.ID_WeightBold, fontAsset.boldStyleWeight);
				fontAsset.material = material2;
			}
			FontAsset fontAsset2 = fontAsset;
			List<GlyphRect> list = new List<GlyphRect>();
			list.Add(new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num));
			fontAsset2.freeGlyphRects = list;
			fontAsset.usedGlyphRects = new List<GlyphRect>();
			fontAsset.ReadFontAssetDefinition();
			return fontAsset;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E78 File Offset: 0x00001078
		private void Awake()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			bool flag = this.m_Material != null;
			if (flag)
			{
				this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EC0 File Offset: 0x000010C0
		internal void InitializeDictionaryLookupTables()
		{
			bool flag = this.m_GlyphLookupDictionary == null;
			if (flag)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_GlyphTable.Count; i++)
			{
				Glyph glyph = this.m_GlyphTable[i];
				uint index = glyph.index;
				bool flag2 = !this.m_GlyphLookupDictionary.ContainsKey(index);
				if (flag2)
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
				}
			}
			bool flag3 = this.m_CharacterLookupDictionary == null;
			if (flag3)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int j = 0; j < this.m_CharacterTable.Count; j++)
			{
				Character character = this.m_CharacterTable[j];
				uint unicode = character.unicode;
				bool flag4 = !this.m_CharacterLookupDictionary.ContainsKey(unicode);
				if (flag4)
				{
					this.m_CharacterLookupDictionary.Add(unicode, character);
				}
				bool flag5 = this.m_GlyphLookupDictionary.ContainsKey(character.glyphIndex);
				if (flag5)
				{
					character.glyph = this.m_GlyphLookupDictionary[character.glyphIndex];
				}
			}
			bool flag6 = this.m_KerningLookupDictionary == null;
			if (flag6)
			{
				this.m_KerningLookupDictionary = new Dictionary<int, KerningPair>();
			}
			else
			{
				this.m_KerningLookupDictionary.Clear();
			}
			List<KerningPair> kerningPairs = this.m_KerningTable.kerningPairs;
			bool flag7 = kerningPairs != null;
			if (flag7)
			{
				for (int k = 0; k < kerningPairs.Count; k++)
				{
					KerningPair kerningPair = kerningPairs[k];
					KerningPairKey kerningPairKey = new KerningPairKey(kerningPair.firstGlyph, kerningPair.secondGlyph);
					bool flag8 = !this.m_KerningLookupDictionary.ContainsKey((int)kerningPairKey.key);
					if (flag8)
					{
						this.m_KerningLookupDictionary.Add((int)kerningPairKey.key, kerningPair);
					}
					else
					{
						bool flag9 = !TextSettings.warningsDisabled;
						if (flag9)
						{
							Debug.LogWarning(string.Concat(new object[] { "Kerning Key for [", kerningPairKey.ascii_Left, "] and [", kerningPairKey.ascii_Right, "] already exists." }));
						}
					}
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000311C File Offset: 0x0000131C
		internal void ReadFontAssetDefinition()
		{
			this.InitializeDictionaryLookupTables();
			bool flag = !this.m_CharacterLookupDictionary.ContainsKey(9U);
			if (flag)
			{
				Glyph glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, this.m_FaceInfo.tabWidth * (float)this.tabMultiple), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(9U, new Character(9U, glyph));
			}
			bool flag2 = !this.m_CharacterLookupDictionary.ContainsKey(10U);
			if (flag2)
			{
				Glyph glyph2 = new Glyph(0U, new GlyphMetrics(10f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(10U, new Character(10U, glyph2));
				bool flag3 = !this.m_CharacterLookupDictionary.ContainsKey(13U);
				if (flag3)
				{
					this.m_CharacterLookupDictionary.Add(13U, new Character(13U, glyph2));
				}
			}
			bool flag4 = !this.m_CharacterLookupDictionary.ContainsKey(8203U);
			if (flag4)
			{
				Glyph glyph3 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(8203U, new Character(8203U, glyph3));
			}
			bool flag5 = !this.m_CharacterLookupDictionary.ContainsKey(8288U);
			if (flag5)
			{
				Glyph glyph4 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(8288U, new Character(8288U, glyph4));
			}
			bool flag6 = this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(72U);
			if (flag6)
			{
				this.m_FaceInfo.capLine = this.m_CharacterLookupDictionary[72U].glyph.metrics.horizontalBearingY;
			}
			bool flag7 = this.m_FaceInfo.scale == 0f;
			if (flag7)
			{
				this.m_FaceInfo.scale = 1f;
			}
			bool flag8 = this.m_FaceInfo.strikethroughOffset == 0f;
			if (flag8)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			bool flag9 = this.m_AtlasPadding == 0;
			if (flag9)
			{
				bool flag10 = this.material.HasProperty(ShaderUtilities.ID_GradientScale);
				if (flag10)
				{
					this.m_AtlasPadding = (int)this.material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
				}
			}
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.material.name);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003410 File Offset: 0x00001610
		internal void SortCharacterTable()
		{
			bool flag = this.m_CharacterTable != null && this.m_CharacterTable.Count > 0;
			if (flag)
			{
				this.m_CharacterTable = Enumerable.ToList<Character>(Enumerable.OrderBy<Character, uint>(this.m_CharacterTable, (Character c) => c.unicode));
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003470 File Offset: 0x00001670
		internal void SortGlyphTable()
		{
			bool flag = this.m_GlyphTable != null && this.m_GlyphTable.Count > 0;
			if (flag)
			{
				this.m_GlyphTable = Enumerable.ToList<Glyph>(Enumerable.OrderBy<Glyph, uint>(this.m_GlyphTable, (Glyph c) => c.index));
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034D0 File Offset: 0x000016D0
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034E4 File Offset: 0x000016E4
		internal bool HasCharacter(int character)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			return !flag && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003514 File Offset: 0x00001714
		internal bool HasCharacter(char character)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			return !flag && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003544 File Offset: 0x00001744
		internal bool HasCharacter(char character, bool searchFallbacks)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			bool flag3 = this.m_CharacterLookupDictionary.ContainsKey((uint)character);
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				if (searchFallbacks)
				{
					bool flag5 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
					if (flag5)
					{
						int num = 0;
						while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
						{
							bool flag6 = this.fallbackFontAssetTable[num].HasCharacter_Internal(character, searchFallbacks);
							if (flag6)
							{
								return true;
							}
							num++;
						}
					}
					bool flag7 = TextSettings.fallbackFontAssets != null && TextSettings.fallbackFontAssets.Count > 0;
					if (flag7)
					{
						int num2 = 0;
						while (num2 < TextSettings.fallbackFontAssets.Count && TextSettings.fallbackFontAssets[num2] != null)
						{
							bool flag8 = TextSettings.fallbackFontAssets[num2].m_CharacterLookupDictionary == null;
							if (flag8)
							{
								TextSettings.fallbackFontAssets[num2].ReadFontAssetDefinition();
							}
							bool flag9 = TextSettings.fallbackFontAssets[num2].m_CharacterLookupDictionary != null && TextSettings.fallbackFontAssets[num2].HasCharacter_Internal(character, searchFallbacks);
							if (flag9)
							{
								return true;
							}
							num2++;
						}
					}
					bool flag10 = TextSettings.defaultFontAsset != null;
					if (flag10)
					{
						bool flag11 = TextSettings.defaultFontAsset.m_CharacterLookupDictionary == null;
						if (flag11)
						{
							TextSettings.defaultFontAsset.ReadFontAssetDefinition();
						}
						bool flag12 = TextSettings.defaultFontAsset.m_CharacterLookupDictionary != null && TextSettings.defaultFontAsset.HasCharacter_Internal(character, searchFallbacks);
						if (flag12)
						{
							return true;
						}
					}
				}
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003734 File Offset: 0x00001934
		private bool HasCharacter_Internal(char character, bool searchFallbacks)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			bool flag3 = this.m_CharacterLookupDictionary.ContainsKey((uint)character);
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				if (searchFallbacks)
				{
					bool flag5 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
					if (flag5)
					{
						int num = 0;
						while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
						{
							bool flag6 = this.fallbackFontAssetTable[num].HasCharacter_Internal(character, searchFallbacks);
							if (flag6)
							{
								return true;
							}
							num++;
						}
					}
				}
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003808 File Offset: 0x00001A08
		internal bool HasCharacters(string text, out List<char> missingCharacters)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			bool flag2;
			if (flag)
			{
				missingCharacters = null;
				flag2 = false;
			}
			else
			{
				missingCharacters = new List<char>();
				for (int i = 0; i < text.Length; i++)
				{
					bool flag3 = !this.m_CharacterLookupDictionary.ContainsKey((uint)text.get_Chars(i));
					if (flag3)
					{
						missingCharacters.Add(text.get_Chars(i));
					}
				}
				flag2 = missingCharacters.Count == 0;
			}
			return flag2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003884 File Offset: 0x00001A84
		internal bool HasCharacters(string text)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					bool flag3 = !this.m_CharacterLookupDictionary.ContainsKey((uint)text.get_Chars(i));
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000038E0 File Offset: 0x00001AE0
		internal static string GetCharacters(FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003938 File Offset: 0x00001B38
		internal static int[] GetCharactersArray(FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000398C File Offset: 0x00001B8C
		internal Character AddCharacter_Internal(uint unicode, Glyph glyph)
		{
			bool flag = this.m_CharacterLookupDictionary.ContainsKey(unicode);
			Character character;
			if (flag)
			{
				character = this.m_CharacterLookupDictionary[unicode];
			}
			else
			{
				uint index = glyph.index;
				bool flag2 = !this.m_GlyphLookupDictionary.ContainsKey(index);
				if (flag2)
				{
					bool flag3 = glyph.glyphRect.width == 0 || glyph.glyphRect.width == 0;
					if (flag3)
					{
						this.m_GlyphTable.Add(glyph);
					}
					else
					{
						bool flag4 = !FontEngine.TryPackGlyphInAtlas(glyph, this.m_AtlasPadding, GlyphPackingMode.ContactPointRule, this.m_AtlasRenderMode, this.m_AtlasWidth, this.m_AtlasHeight, this.m_FreeGlyphRects, this.m_UsedGlyphRects);
						if (flag4)
						{
							return null;
						}
						this.m_GlyphsToRender.Add(glyph);
					}
				}
				Character character2 = new Character(unicode, glyph);
				this.m_CharacterTable.Add(character2);
				this.m_CharacterLookupDictionary.Add(unicode, character2);
				this.UpdateAtlasTexture();
				character = character2;
			}
			return character;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A94 File Offset: 0x00001C94
		internal bool TryAddCharacter(uint unicode, out Character character)
		{
			bool flag = this.m_CharacterLookupDictionary.ContainsKey(unicode);
			bool flag2;
			if (flag)
			{
				character = this.m_CharacterLookupDictionary[unicode];
				flag2 = true;
			}
			else
			{
				character = null;
				bool flag3 = FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) > FontEngineError.Success;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
					bool flag4 = glyphIndex == 0U;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
						if (flag5)
						{
							character = new Character(unicode, this.m_GlyphLookupDictionary[glyphIndex]);
							this.m_CharacterTable.Add(character);
							this.m_CharacterLookupDictionary.Add(unicode, character);
							flag2 = true;
						}
						else
						{
							bool flag6 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0;
							if (flag6)
							{
								this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
								FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
							}
							Glyph glyph;
							bool flag7 = FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph);
							if (flag7)
							{
								this.m_GlyphTable.Add(glyph);
								this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
								character = new Character(unicode, glyph);
								this.m_CharacterTable.Add(character);
								this.m_CharacterLookupDictionary.Add(unicode, character);
								flag2 = true;
							}
							else
							{
								flag2 = false;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C3C File Offset: 0x00001E3C
		internal void UpdateAtlasTexture()
		{
			bool flag = this.m_GlyphsToRender.Count == 0;
			if (!flag)
			{
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
				bool flag2 = this.m_GlyphsToPack.Count > 0;
				if (flag2)
				{
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D20 File Offset: 0x00001F20
		public bool TryAddCharacters(uint[] unicodes)
		{
			bool flag = false;
			this.m_GlyphIndexes.Clear();
			this.s_GlyphLookupMap.Clear();
			FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize);
			foreach (uint num in unicodes)
			{
				bool flag2 = this.m_CharacterLookupDictionary.ContainsKey(num);
				if (!flag2)
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(num);
					bool flag3 = glyphIndex == 0U;
					if (flag3)
					{
						flag = true;
					}
					else
					{
						bool flag4 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
						if (flag4)
						{
							Character character = new Character(num, this.m_GlyphLookupDictionary[glyphIndex]);
							this.m_CharacterTable.Add(character);
							this.m_CharacterLookupDictionary.Add(num, character);
						}
						else
						{
							bool flag5 = this.s_GlyphLookupMap.ContainsKey(glyphIndex);
							if (flag5)
							{
								this.s_GlyphLookupMap[glyphIndex].Add(num);
							}
							else
							{
								Dictionary<uint, List<uint>> dictionary = this.s_GlyphLookupMap;
								uint num2 = glyphIndex;
								List<uint> list = new List<uint>();
								list.Add(num);
								dictionary.Add(num2, list);
								this.m_GlyphIndexes.Add(glyphIndex);
							}
						}
					}
				}
			}
			bool flag6 = this.m_GlyphIndexes == null || this.m_GlyphIndexes.Count == 0;
			bool flag7;
			if (flag6)
			{
				flag7 = true;
			}
			else
			{
				bool flag8 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0;
				if (flag8)
				{
					this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
					FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				}
				Glyph[] array;
				bool flag9 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphIndexes, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
				int num3 = 0;
				while (num3 < array.Length && array[num3] != null)
				{
					Glyph glyph = array[num3];
					uint index = glyph.index;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(index, glyph);
					foreach (uint num4 in this.s_GlyphLookupMap[index])
					{
						Character character2 = new Character(num4, glyph);
						this.m_CharacterTable.Add(character2);
						this.m_CharacterLookupDictionary.Add(num4, character2);
					}
					num3++;
				}
				flag7 = flag9 && !flag;
			}
			return flag7;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003FF0 File Offset: 0x000021F0
		public bool TryAddCharacters(string characters)
		{
			bool flag = string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == FontAsset.AtlasPopulationMode.Static;
			bool flag3;
			if (flag)
			{
				bool flag2 = this.m_AtlasPopulationMode == FontAsset.AtlasPopulationMode.Static;
				if (flag2)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				flag3 = false;
			}
			else
			{
				bool flag4 = FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize) > FontEngineError.Success;
				if (flag4)
				{
					flag3 = false;
				}
				else
				{
					bool flag5 = false;
					int length = characters.Length;
					this.m_GlyphIndexes.Clear();
					this.s_GlyphLookupMap.Clear();
					for (int i = 0; i < length; i++)
					{
						uint num = (uint)characters.get_Chars(i);
						bool flag6 = this.m_CharacterLookupDictionary.ContainsKey(num);
						if (!flag6)
						{
							uint glyphIndex = FontEngine.GetGlyphIndex(num);
							bool flag7 = glyphIndex == 0U;
							if (flag7)
							{
								flag5 = true;
							}
							else
							{
								bool flag8 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
								if (flag8)
								{
									Character character = new Character(num, this.m_GlyphLookupDictionary[glyphIndex]);
									this.m_CharacterTable.Add(character);
									this.m_CharacterLookupDictionary.Add(num, character);
								}
								else
								{
									bool flag9 = this.s_GlyphLookupMap.ContainsKey(glyphIndex);
									if (flag9)
									{
										bool flag10 = this.s_GlyphLookupMap[glyphIndex].Contains(num);
										if (!flag10)
										{
											this.s_GlyphLookupMap[glyphIndex].Add(num);
										}
									}
									else
									{
										Dictionary<uint, List<uint>> dictionary = this.s_GlyphLookupMap;
										uint num2 = glyphIndex;
										List<uint> list = new List<uint>();
										list.Add(num);
										dictionary.Add(num2, list);
										this.m_GlyphIndexes.Add(glyphIndex);
									}
								}
							}
						}
					}
					bool flag11 = this.m_GlyphIndexes == null || this.m_GlyphIndexes.Count == 0;
					if (flag11)
					{
						Debug.LogWarning("No characters will be added to font asset [" + base.name + "] either because they are already present in the font asset or missing from the font file.");
						flag3 = true;
					}
					else
					{
						bool flag12 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0;
						if (flag12)
						{
							this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
							FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
						}
						Glyph[] array;
						bool flag13 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphIndexes, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
						int num3 = 0;
						while (num3 < array.Length && array[num3] != null)
						{
							Glyph glyph = array[num3];
							uint index = glyph.index;
							this.m_GlyphTable.Add(glyph);
							this.m_GlyphLookupDictionary.Add(index, glyph);
							List<uint> list2 = this.s_GlyphLookupMap[index];
							int count = list2.Count;
							for (int j = 0; j < count; j++)
							{
								uint num4 = list2[j];
								Character character2 = new Character(num4, glyph);
								this.m_CharacterTable.Add(character2);
								this.m_CharacterLookupDictionary.Add(num4, character2);
							}
							num3++;
						}
						flag3 = flag13 && !flag5;
					}
				}
			}
			return flag3;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004374 File Offset: 0x00002574
		internal void ClearFontAssetData()
		{
			bool flag = this.m_GlyphTable != null;
			if (flag)
			{
				this.m_GlyphTable.Clear();
			}
			bool flag2 = this.m_CharacterTable != null;
			if (flag2)
			{
				this.m_CharacterTable.Clear();
			}
			bool flag3 = this.m_UsedGlyphRects != null;
			if (flag3)
			{
				this.m_UsedGlyphRects.Clear();
			}
			bool flag4 = this.m_FreeGlyphRects != null;
			if (flag4)
			{
				int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
				List<GlyphRect> list = new List<GlyphRect>();
				list.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
				this.m_FreeGlyphRects = list;
			}
			bool flag5 = this.m_GlyphsToPack != null;
			if (flag5)
			{
				this.m_GlyphsToPack.Clear();
			}
			bool flag6 = this.m_GlyphsPacked != null;
			if (flag6)
			{
				this.m_GlyphsPacked.Clear();
			}
			bool flag7 = this.m_KerningTable != null && this.m_KerningTable.kerningPairs != null;
			if (flag7)
			{
				this.m_KerningTable.kerningPairs.Clear();
			}
			this.m_AtlasTextureIndex = 0;
			bool flag8 = this.m_AtlasTextures != null;
			if (flag8)
			{
				for (int i = 0; i < this.m_AtlasTextures.Length; i++)
				{
					Texture2D texture2D = this.m_AtlasTextures[i];
					bool flag9 = texture2D == null;
					if (!flag9)
					{
						bool flag10 = texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight;
						if (flag10)
						{
							texture2D.Resize(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
						}
						FontEngine.ResetAtlasTexture(texture2D);
						texture2D.Apply();
						bool flag11 = i == 0;
						if (flag11)
						{
							this.m_AtlasTexture = texture2D;
						}
						this.m_AtlasTextures[i] = texture2D;
					}
				}
			}
			this.ReadFontAssetDefinition();
		}

		// Token: 0x04000025 RID: 37
		[SerializeField]
		private string m_Version = "1.1.0";

		// Token: 0x04000026 RID: 38
		[SerializeField]
		private int m_HashCode;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		private FaceInfo m_FaceInfo;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		internal Font m_SourceFontFile_EditorRef;

		// Token: 0x0400002A RID: 42
		[SerializeField]
		internal Font m_SourceFontFile;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		private FontAsset.AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x0400002C RID: 44
		[SerializeField]
		private List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x0400002D RID: 45
		private Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x0400002E RID: 46
		[SerializeField]
		private List<Character> m_CharacterTable = new List<Character>();

		// Token: 0x0400002F RID: 47
		private Dictionary<uint, Character> m_CharacterLookupDictionary;

		// Token: 0x04000030 RID: 48
		private Texture2D m_AtlasTexture;

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private Texture2D[] m_AtlasTextures;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private int m_AtlasWidth;

		// Token: 0x04000034 RID: 52
		[SerializeField]
		private int m_AtlasHeight;

		// Token: 0x04000035 RID: 53
		[SerializeField]
		private int m_AtlasPadding;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		private GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x04000039 RID: 57
		private List<uint> m_GlyphIndexes = new List<uint>();

		// Token: 0x0400003A RID: 58
		private Dictionary<uint, List<uint>> s_GlyphLookupMap = new Dictionary<uint, List<uint>>();

		// Token: 0x0400003B RID: 59
		[SerializeField]
		private Material m_Material;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		internal int m_MaterialHashCode;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		internal KerningTable m_KerningTable = new KerningTable();

		// Token: 0x0400003E RID: 62
		private Dictionary<int, KerningPair> m_KerningLookupDictionary;

		// Token: 0x0400003F RID: 63
		[SerializeField]
		internal KerningPair m_EmptyKerningPair;

		// Token: 0x04000040 RID: 64
		[SerializeField]
		internal List<FontAsset> m_FallbackFontAssetTable;

		// Token: 0x04000041 RID: 65
		[SerializeField]
		internal FontAssetCreationSettings m_FontAssetCreationSettings;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		internal FontWeights[] m_FontWeightTable = new FontWeights[10];

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private float m_RegularStyleWeight = 0f;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private float m_RegularStyleSpacing = 0f;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private float m_BoldStyleWeight = 0.75f;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private float m_BoldStyleSpacing = 7f;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		private byte m_ItalicStyleSlant = 35;

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private byte m_TabMultiple = 10;

		// Token: 0x04000049 RID: 73
		internal bool m_IsFontAssetLookupTablesDirty = false;

		// Token: 0x0400004A RID: 74
		private List<Glyph> m_GlyphsToPack = new List<Glyph>();

		// Token: 0x0400004B RID: 75
		private List<Glyph> m_GlyphsPacked = new List<Glyph>();

		// Token: 0x0400004C RID: 76
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x02000008 RID: 8
		internal enum AtlasPopulationMode
		{
			// Token: 0x0400004E RID: 78
			Static,
			// Token: 0x0400004F RID: 79
			Dynamic
		}
	}
}
