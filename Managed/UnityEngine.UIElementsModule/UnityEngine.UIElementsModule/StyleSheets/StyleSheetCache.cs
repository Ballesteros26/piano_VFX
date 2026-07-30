using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000269 RID: 617
	internal static class StyleSheetCache
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x000510EC File Offset: 0x0004F2EC
		internal static void ClearCaches()
		{
			StyleSheetCache.s_RulePropertyIdsCache.Clear();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000510FC File Offset: 0x0004F2FC
		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey sheetHandleKey = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] array;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(sheetHandleKey, ref array);
			if (flag)
			{
				StyleRule styleRule = sheet.rules[ruleIndex];
				array = new StylePropertyId[styleRule.properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StyleSheetCache.GetPropertyId(styleRule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(sheetHandleKey, array);
			}
			return array;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0005117C File Offset: 0x0004F37C
		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] array = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return array;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000511BC File Offset: 0x0004F3BC
		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty styleProperty = rule.properties[index];
			string name = styleProperty.name;
			StylePropertyId stylePropertyId;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			if (flag)
			{
				stylePropertyId = (styleProperty.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return stylePropertyId;
		}

		// Token: 0x04000915 RID: 2325
		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		// Token: 0x04000916 RID: 2326
		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		// Token: 0x0200026A RID: 618
		private struct SheetHandleKey
		{
			// Token: 0x06001246 RID: 4678 RVA: 0x00051220 File Offset: 0x0004F420
			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}

			// Token: 0x04000917 RID: 2327
			public readonly int sheetInstanceID;

			// Token: 0x04000918 RID: 2328
			public readonly int index;
		}

		// Token: 0x0200026B RID: 619
		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x00051238 File Offset: 0x0004F438
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x0005126C File Offset: 0x0004F46C
			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}
		}
	}
}
