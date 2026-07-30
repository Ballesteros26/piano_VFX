using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000010 RID: 16
	public static class FontUpdateTracker
	{
		// Token: 0x060000AF RID: 175 RVA: 0x0000511C File Offset: 0x0000331C
		public static void TrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			HashSet<Text> hashSet;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out hashSet);
			if (hashSet == null)
			{
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt += FontUpdateTracker.RebuildForFont;
				}
				hashSet = new HashSet<Text>();
				FontUpdateTracker.m_Tracked.Add(t.font, hashSet);
			}
			hashSet.Add(t);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000518C File Offset: 0x0000338C
		private static void RebuildForFont(Font f)
		{
			HashSet<Text> hashSet;
			FontUpdateTracker.m_Tracked.TryGetValue(f, out hashSet);
			if (hashSet == null)
			{
				return;
			}
			foreach (Text text in hashSet)
			{
				text.FontTextureChanged();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000051EC File Offset: 0x000033EC
		public static void UntrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			HashSet<Text> hashSet;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out hashSet);
			if (hashSet == null)
			{
				return;
			}
			hashSet.Remove(t);
			if (hashSet.Count == 0)
			{
				FontUpdateTracker.m_Tracked.Remove(t.font);
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt -= FontUpdateTracker.RebuildForFont;
				}
			}
		}

		// Token: 0x0400004B RID: 75
		private static Dictionary<Font, HashSet<Text>> m_Tracked = new Dictionary<Font, HashSet<Text>>();
	}
}
