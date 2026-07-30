using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AB RID: 171
	internal static class StyleCache
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00013420 File Offset: 0x00011620
		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, ref data);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001343E File Offset: 0x0001163E
		public static void SetValue(long hash, ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013450 File Offset: 0x00011650
		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, ref data);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001346E File Offset: 0x0001166E
		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001347E File Offset: 0x0001167E
		public static void ClearStyleCache()
		{
			StyleCache.s_ComputedStyleCache.Clear();
			StyleCache.s_StyleVariableContextCache.Clear();
		}

		// Token: 0x04000219 RID: 537
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		// Token: 0x0400021A RID: 538
		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();
	}
}
