using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.TextCore
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	internal class TextSpriteAsset : ScriptableObject
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00019054 File Offset: 0x00017254
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0001906C File Offset: 0x0001726C
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00019078 File Offset: 0x00017278
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00019090 File Offset: 0x00017290
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0001909C File Offset: 0x0001729C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000190B4 File Offset: 0x000172B4
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000190C0 File Offset: 0x000172C0
		public int materialHashCode
		{
			get
			{
				return this.m_MaterialHashCode;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000190D8 File Offset: 0x000172D8
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000190F0 File Offset: 0x000172F0
		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000190FC File Offset: 0x000172FC
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00019114 File Offset: 0x00017314
		public List<SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00019120 File Offset: 0x00017320
		private void Awake()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			bool flag = this.m_Material != null;
			if (flag)
			{
				this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00019168 File Offset: 0x00017368
		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Shader shader = Shader.Find("TextMeshPro/Sprite");
			Material material = new Material(shader);
			material.SetTexture(ShaderUtilities.ID_MainTex, this.spriteSheet);
			material.hideFlags = HideFlags.HideInHierarchy;
			return material;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000191B0 File Offset: 0x000173B0
		public void UpdateLookupTables()
		{
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				uint index = this.m_SpriteGlyphTable[i].index;
				bool flag2 = !this.m_GlyphIndexLookup.ContainsKey(index);
				if (flag2)
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
			}
			bool flag3 = this.m_NameLookup == null;
			if (flag3)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag4 = this.m_UnicodeLookup == null;
			if (flag4)
			{
				this.m_UnicodeLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_UnicodeLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				int hashCode = this.m_SpriteCharacterTable[j].hashCode;
				bool flag5 = !this.m_NameLookup.ContainsKey(hashCode);
				if (flag5)
				{
					this.m_NameLookup.Add(hashCode, j);
				}
				uint unicode = this.m_SpriteCharacterTable[j].unicode;
				bool flag6 = !this.m_UnicodeLookup.ContainsKey(unicode);
				if (flag6)
				{
					this.m_UnicodeLookup.Add(unicode, j);
				}
				uint glyphIndex = this.m_SpriteCharacterTable[j].glyphIndex;
				int num;
				bool flag7 = this.m_GlyphIndexLookup.TryGetValue(glyphIndex, ref num);
				if (flag7)
				{
					this.m_SpriteCharacterTable[j].glyph = this.m_SpriteGlyphTable[num];
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00019374 File Offset: 0x00017574
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, ref num);
			int num2;
			if (flag2)
			{
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000193B4 File Offset: 0x000175B4
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_UnicodeLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_UnicodeLookup.TryGetValue(unicode, ref num);
			int num2;
			if (flag2)
			{
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000193F4 File Offset: 0x000175F4
		public int GetSpriteIndexFromName(string spriteName)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(spriteName);
			return this.GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00019428 File Offset: 0x00017628
		public static TextSpriteAsset SearchForSpriteByUnicode(TextSpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			TextSpriteAsset textSpriteAsset;
			if (flag)
			{
				spriteIndex = -1;
				textSpriteAsset = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					textSpriteAsset = spriteAsset;
				}
				else
				{
					bool flag3 = TextSpriteAsset.s_SearchedSpriteAssets == null;
					if (flag3)
					{
						TextSpriteAsset.s_SearchedSpriteAssets = new List<int>();
					}
					TextSpriteAsset.s_SearchedSpriteAssets.Clear();
					int instanceID = spriteAsset.GetInstanceID();
					TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
					}
					else
					{
						bool flag5 = includeFallbacks && TextSettings.defaultSpriteAsset != null;
						if (flag5)
						{
							textSpriteAsset = TextSpriteAsset.SearchForSpriteByUnicodeInternal(TextSettings.defaultSpriteAsset, unicode, includeFallbacks, out spriteIndex);
						}
						else
						{
							spriteIndex = -1;
							textSpriteAsset = null;
						}
					}
				}
			}
			return textSpriteAsset;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00019500 File Offset: 0x00017700
		private static TextSpriteAsset SearchForSpriteByUnicodeInternal(List<TextSpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TextSpriteAsset textSpriteAsset = spriteAssets[i];
				bool flag = textSpriteAsset == null;
				if (!flag)
				{
					int instanceID = textSpriteAsset.GetInstanceID();
					bool flag2 = TextSpriteAsset.s_SearchedSpriteAssets.Contains(instanceID);
					if (!flag2)
					{
						TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByUnicodeInternal(textSpriteAsset, unicode, includeFallbacks, out spriteIndex);
						bool flag3 = textSpriteAsset != null;
						if (flag3)
						{
							return textSpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00019588 File Offset: 0x00017788
		private static TextSpriteAsset SearchForSpriteByUnicodeInternal(TextSpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			TextSpriteAsset textSpriteAsset;
			if (flag)
			{
				textSpriteAsset = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					textSpriteAsset = TextSpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					textSpriteAsset = null;
				}
			}
			return textSpriteAsset;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000195E8 File Offset: 0x000177E8
		public static TextSpriteAsset SearchForSpriteByHashCode(TextSpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			TextSpriteAsset textSpriteAsset;
			if (flag)
			{
				spriteIndex = -1;
				textSpriteAsset = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					textSpriteAsset = spriteAsset;
				}
				else
				{
					bool flag3 = TextSpriteAsset.s_SearchedSpriteAssets == null;
					if (flag3)
					{
						TextSpriteAsset.s_SearchedSpriteAssets = new List<int>();
					}
					TextSpriteAsset.s_SearchedSpriteAssets.Clear();
					int instanceID = spriteAsset.GetInstanceID();
					TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, includeFallbacks, out spriteIndex);
					}
					else
					{
						bool flag5 = includeFallbacks && TextSettings.defaultSpriteAsset != null;
						if (flag5)
						{
							textSpriteAsset = TextSpriteAsset.SearchForSpriteByHashCodeInternal(TextSettings.defaultSpriteAsset, hashCode, includeFallbacks, out spriteIndex);
						}
						else
						{
							spriteIndex = -1;
							textSpriteAsset = null;
						}
					}
				}
			}
			return textSpriteAsset;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000196C0 File Offset: 0x000178C0
		private static TextSpriteAsset SearchForSpriteByHashCodeInternal(List<TextSpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TextSpriteAsset textSpriteAsset = spriteAssets[i];
				bool flag = textSpriteAsset == null;
				if (!flag)
				{
					int instanceID = textSpriteAsset.GetInstanceID();
					bool flag2 = TextSpriteAsset.s_SearchedSpriteAssets.Contains(instanceID);
					if (!flag2)
					{
						TextSpriteAsset.s_SearchedSpriteAssets.Add(instanceID);
						textSpriteAsset = TextSpriteAsset.SearchForSpriteByHashCodeInternal(textSpriteAsset, hashCode, searchFallbacks, out spriteIndex);
						bool flag3 = textSpriteAsset != null;
						if (flag3)
						{
							return textSpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00019748 File Offset: 0x00017948
		private static TextSpriteAsset SearchForSpriteByHashCodeInternal(TextSpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			TextSpriteAsset textSpriteAsset;
			if (flag)
			{
				textSpriteAsset = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					textSpriteAsset = TextSpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					textSpriteAsset = null;
				}
			}
			return textSpriteAsset;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000197A8 File Offset: 0x000179A8
		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				this.m_SpriteGlyphTable = Enumerable.ToList<SpriteGlyph>(Enumerable.OrderBy<SpriteGlyph, uint>(this.m_SpriteGlyphTable, (SpriteGlyph item) => item.index));
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0001980C File Offset: 0x00017A0C
		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				this.m_SpriteCharacterTable = Enumerable.ToList<SpriteCharacter>(Enumerable.OrderBy<SpriteCharacter, uint>(this.m_SpriteCharacterTable, (SpriteCharacter c) => c.unicode));
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0001986C File Offset: 0x00017A6C
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x0400033B RID: 827
		internal Dictionary<uint, int> m_UnicodeLookup;

		// Token: 0x0400033C RID: 828
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x0400033D RID: 829
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x0400033E RID: 830
		[SerializeField]
		private string m_Version;

		// Token: 0x0400033F RID: 831
		[SerializeField]
		private int m_HashCode;

		// Token: 0x04000340 RID: 832
		[SerializeField]
		public Texture spriteSheet;

		// Token: 0x04000341 RID: 833
		[SerializeField]
		private Material m_Material;

		// Token: 0x04000342 RID: 834
		[SerializeField]
		private int m_MaterialHashCode;

		// Token: 0x04000343 RID: 835
		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		// Token: 0x04000344 RID: 836
		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		// Token: 0x04000345 RID: 837
		[SerializeField]
		public List<TextSpriteAsset> fallbackSpriteAssets;

		// Token: 0x04000346 RID: 838
		internal bool m_IsSpriteAssetLookupTablesDirty = false;

		// Token: 0x04000347 RID: 839
		private static List<int> s_SearchedSpriteAssets;
	}
}
