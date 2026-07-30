using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A6 RID: 166
	internal static class VisualElementUtils
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00012FE4 File Offset: 0x000111E4
		public static string GetUniqueName(string nameBase)
		{
			string text = nameBase;
			int num = 2;
			while (VisualElementUtils.s_usedNames.Contains(text))
			{
				text = nameBase + num;
				num++;
			}
			VisualElementUtils.s_usedNames.Add(text);
			return text;
		}

		// Token: 0x04000206 RID: 518
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();
	}
}
