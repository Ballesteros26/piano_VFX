using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000033 RID: 51
	public class TMP_ResourceManager
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000F501 File Offset: 0x0000D701
		private static TMP_ResourceManager instance
		{
			get
			{
				return TMP_ResourceManager.s_instance;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F520 File Offset: 0x0000D720
		public static void AddFontAsset(TMP_FontAsset fontAsset)
		{
			int hashCode = fontAsset.hashCode;
			if (TMP_ResourceManager.s_FontAssetReferenceLookup.ContainsKey(hashCode))
			{
				return;
			}
			TMP_ResourceManager.s_FontAssetReferenceLookup.Add(hashCode, fontAsset);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F54E File Offset: 0x0000D74E
		public static bool TryGetFontAsset(int hashcode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return TMP_ResourceManager.s_FontAssetReferenceLookup.TryGetValue(hashcode, out fontAsset);
		}

		// Token: 0x04000192 RID: 402
		private static Dictionary<int, TMP_FontAsset> s_FontAssetReferenceLookup = new Dictionary<int, TMP_FontAsset>();

		// Token: 0x04000193 RID: 403
		private static readonly TMP_ResourceManager s_instance = new TMP_ResourceManager();
	}
}
