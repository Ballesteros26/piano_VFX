using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000040 RID: 64
	public class TMP_SpriteAsset : TMP_Asset
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010DEB File Offset: 0x0000EFEB
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00010DF3 File Offset: 0x0000EFF3
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00010DFC File Offset: 0x0000EFFC
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00010E04 File Offset: 0x0000F004
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00010E0D File Offset: 0x0000F00D
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00010E23 File Offset: 0x0000F023
		public List<TMP_SpriteCharacter> spriteCharacterTable
		{
			get
			{
				if (this.m_GlyphIndexLookup == null)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00010E2C File Offset: 0x0000F02C
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00010E34 File Offset: 0x0000F034
		public List<TMP_SpriteGlyph> spriteGlyphTable
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

		// Token: 0x060002BB RID: 699 RVA: 0x00010E3D File Offset: 0x0000F03D
		private void Awake()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00010E60 File Offset: 0x0000F060
		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Material material = new Material(Shader.Find("TextMeshPro/Sprite"));
			material.SetTexture(ShaderUtilities.ID_MainTex, this.spriteSheet);
			material.hideFlags = HideFlags.HideInHierarchy;
			return material;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010E90 File Offset: 0x0000F090
		public void UpdateLookupTables()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
			if (this.m_GlyphIndexLookup == null)
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
				if (!this.m_GlyphIndexLookup.ContainsKey(index))
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
			}
			if (this.m_NameLookup == null)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			if (this.m_UnicodeLookup == null)
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
				if (!this.m_NameLookup.ContainsKey(hashCode))
				{
					this.m_NameLookup.Add(hashCode, j);
				}
				uint unicode = this.m_SpriteCharacterTable[j].unicode;
				if (!this.m_UnicodeLookup.ContainsKey(unicode))
				{
					this.m_UnicodeLookup.Add(unicode, j);
				}
				uint glyphIndex = this.m_SpriteCharacterTable[j].glyphIndex;
				int num;
				if (this.m_GlyphIndexLookup.TryGetValue(glyphIndex, out num))
				{
					this.m_SpriteCharacterTable[j].glyph = this.m_SpriteGlyphTable[num];
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011024 File Offset: 0x0000F224
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int num;
			if (this.m_NameLookup.TryGetValue(hashCode, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011054 File Offset: 0x0000F254
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			if (this.m_UnicodeLookup == null)
			{
				this.UpdateLookupTables();
			}
			int num;
			if (this.m_UnicodeLookup.TryGetValue(unicode, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011084 File Offset: 0x0000F284
		public int GetSpriteIndexFromName(string name)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int simpleHashCode = TMP_TextUtilities.GetSimpleHashCode(name);
			return this.GetSpriteIndexFromHashcode(simpleHashCode);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public static TMP_SpriteAsset SearchForSpriteByUnicode(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new List<int>();
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			int instanceID = spriteAsset.GetInstanceID();
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, unicode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011154 File Offset: 0x0000F354
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(List<TMP_SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tmp_SpriteAsset = spriteAssets[i];
				if (!(tmp_SpriteAsset == null))
				{
					int instanceID = tmp_SpriteAsset.GetInstanceID();
					if (!TMP_SpriteAsset.k_searchedSpriteAssets.Contains(instanceID))
					{
						TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
						tmp_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(tmp_SpriteAsset, unicode, includeFallbacks, out spriteIndex);
						if (tmp_SpriteAsset != null)
						{
							return tmp_SpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000111BB File Offset: 0x0000F3BB
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000111FC File Offset: 0x0000F3FC
		public static TMP_SpriteAsset SearchForSpriteByHashCode(TMP_SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new List<int>();
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			int instanceID = spriteAsset.GetInstanceID();
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, includeFallbacks, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(TMP_Settings.defaultSpriteAsset, hashCode, includeFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000112A0 File Offset: 0x0000F4A0
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(List<TMP_SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tmp_SpriteAsset = spriteAssets[i];
				if (!(tmp_SpriteAsset == null))
				{
					int instanceID = tmp_SpriteAsset.GetInstanceID();
					if (!TMP_SpriteAsset.k_searchedSpriteAssets.Contains(instanceID))
					{
						TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
						tmp_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(tmp_SpriteAsset, hashCode, searchFallbacks, out spriteIndex);
						if (tmp_SpriteAsset != null)
						{
							return tmp_SpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011307 File Offset: 0x0000F507
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(TMP_SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011348 File Offset: 0x0000F548
		public void SortGlyphTable()
		{
			if (this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0)
			{
				return;
			}
			this.m_SpriteGlyphTable = this.m_SpriteGlyphTable.OrderBy((TMP_SpriteGlyph item) => item.index).ToList<TMP_SpriteGlyph>();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000113A0 File Offset: 0x0000F5A0
		internal void SortCharacterTable()
		{
			if (this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0)
			{
				this.m_SpriteCharacterTable = this.m_SpriteCharacterTable.OrderBy((TMP_SpriteCharacter c) => c.unicode).ToList<TMP_SpriteCharacter>();
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000113F8 File Offset: 0x0000F5F8
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011408 File Offset: 0x0000F608
		private void UpgradeSpriteAsset()
		{
			this.m_Version = "1.1.0";
			Debug.Log(string.Concat(new string[] { "Upgrading sprite asset [", base.name, "] to version ", this.m_Version, "." }), this);
			this.m_SpriteCharacterTable.Clear();
			this.m_SpriteGlyphTable.Clear();
			for (int i = 0; i < this.spriteInfoList.Count; i++)
			{
				TMP_Sprite tmp_Sprite = this.spriteInfoList[i];
				TMP_SpriteGlyph tmp_SpriteGlyph = new TMP_SpriteGlyph();
				tmp_SpriteGlyph.index = (uint)i;
				tmp_SpriteGlyph.sprite = tmp_Sprite.sprite;
				tmp_SpriteGlyph.metrics = new GlyphMetrics(tmp_Sprite.width, tmp_Sprite.height, tmp_Sprite.xOffset, tmp_Sprite.yOffset, tmp_Sprite.xAdvance);
				tmp_SpriteGlyph.glyphRect = new GlyphRect((int)tmp_Sprite.x, (int)tmp_Sprite.y, (int)tmp_Sprite.width, (int)tmp_Sprite.height);
				tmp_SpriteGlyph.scale = 1f;
				tmp_SpriteGlyph.atlasIndex = 0;
				this.m_SpriteGlyphTable.Add(tmp_SpriteGlyph);
				TMP_SpriteCharacter tmp_SpriteCharacter = new TMP_SpriteCharacter((uint)tmp_Sprite.unicode, tmp_SpriteGlyph);
				tmp_SpriteCharacter.name = tmp_Sprite.name;
				tmp_SpriteCharacter.scale = tmp_Sprite.scale;
				this.m_SpriteCharacterTable.Add(tmp_SpriteCharacter);
			}
			this.UpdateLookupTables();
		}

		// Token: 0x0400029D RID: 669
		internal Dictionary<uint, int> m_UnicodeLookup;

		// Token: 0x0400029E RID: 670
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x0400029F RID: 671
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x040002A0 RID: 672
		[SerializeField]
		private string m_Version;

		// Token: 0x040002A1 RID: 673
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x040002A2 RID: 674
		public Texture spriteSheet;

		// Token: 0x040002A3 RID: 675
		[SerializeField]
		private List<TMP_SpriteCharacter> m_SpriteCharacterTable = new List<TMP_SpriteCharacter>();

		// Token: 0x040002A4 RID: 676
		[SerializeField]
		private List<TMP_SpriteGlyph> m_SpriteGlyphTable = new List<TMP_SpriteGlyph>();

		// Token: 0x040002A5 RID: 677
		public List<TMP_Sprite> spriteInfoList;

		// Token: 0x040002A6 RID: 678
		[SerializeField]
		public List<TMP_SpriteAsset> fallbackSpriteAssets;

		// Token: 0x040002A7 RID: 679
		internal bool m_IsSpriteAssetLookupTablesDirty;

		// Token: 0x040002A8 RID: 680
		private static List<int> k_searchedSpriteAssets;
	}
}
