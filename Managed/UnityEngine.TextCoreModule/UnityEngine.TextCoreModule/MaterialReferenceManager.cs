using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	// Token: 0x02000019 RID: 25
	internal class MaterialReferenceManager
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000058C8 File Offset: 0x00003AC8
		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000058F5 File Offset: 0x00003AF5
		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005904 File Offset: 0x00003B04
		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005954 File Offset: 0x00003B54
		public static void AddSpriteAsset(TextSpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005964 File Offset: 0x00003B64
		private void AddSpriteAssetInternal(TextSpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000059B4 File Offset: 0x00003BB4
		public static void AddSpriteAsset(int hashCode, TextSpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000059C4 File Offset: 0x00003BC4
		private void AddSpriteAssetInternal(int hashCode, TextSpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
				bool flag2 = spriteAsset.hashCode == 0;
				if (flag2)
				{
					spriteAsset.hashCode = hashCode;
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005A1A File Offset: 0x00003C1A
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005A2A File Offset: 0x00003C2A
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005A3B File Offset: 0x00003C3B
		public static void AddColorGradientPreset(int hashCode, TextGradientPreset spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005A4C File Offset: 0x00003C4C
		private void AddColorGradientPreset_Internal(int hashCode, TextGradientPreset spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005A7C File Offset: 0x00003C7C
		public bool Contains(FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public bool Contains(TextSpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, ref fontAsset);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005B08 File Offset: 0x00003D08
		public static bool TryGetSpriteAsset(int hashCode, out TextSpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005B28 File Offset: 0x00003D28
		private bool TryGetSpriteAssetInternal(int hashCode, out TextSpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, ref spriteAsset);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005B4C File Offset: 0x00003D4C
		public static bool TryGetColorGradientPreset(int hashCode, out TextGradientPreset gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005B6C File Offset: 0x00003D6C
		private bool TryGetColorGradientPresetInternal(int hashCode, out TextGradientPreset gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, ref gradientPreset);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005B90 File Offset: 0x00003D90
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, ref material);
		}

		// Token: 0x04000099 RID: 153
		private static MaterialReferenceManager s_Instance;

		// Token: 0x0400009A RID: 154
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x0400009B RID: 155
		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x0400009C RID: 156
		private Dictionary<int, TextSpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, TextSpriteAsset>();

		// Token: 0x0400009D RID: 157
		private Dictionary<int, TextGradientPreset> m_ColorGradientReferenceLookup = new Dictionary<int, TextGradientPreset>();
	}
}
