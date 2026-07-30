using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x02000611 RID: 1553
	internal class AppResourcesLengthComparer<T> : IComparer<T>
	{
		// Token: 0x060042DB RID: 17115 RVA: 0x000B0F84 File Offset: 0x000AF184
		private int CompareStrings(string a, string b)
		{
			if (a == null || b == null)
			{
				return 0;
			}
			return b.Length - a.Length;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x000B0F9C File Offset: 0x000AF19C
		int IComparer<T>.Compare(T _a, T _b)
		{
			string text;
			string text2;
			if (_a is string && _b is string)
			{
				text = _a as string;
				text2 = _b as string;
			}
			else if (_a is List<string> && _b is List<string>)
			{
				text = (_a as List<string>)[0];
				text2 = (_b as List<string>)[0];
			}
			else
			{
				if (!(_a is AppResourceFileInfo) || !(_b is AppResourceFileInfo))
				{
					return 0;
				}
				text = (_a as AppResourceFileInfo).Info.Name;
				text2 = (_b as AppResourceFileInfo).Info.Name;
			}
			return this.CompareStrings(text, text2);
		}
	}
}
