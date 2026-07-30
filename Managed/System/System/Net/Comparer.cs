using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020004B5 RID: 1205
	internal class Comparer : IComparer
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x0008A638 File Offset: 0x00088838
		int IComparer.Compare(object ol, object or)
		{
			Cookie cookie = (Cookie)ol;
			Cookie cookie2 = (Cookie)or;
			int num;
			if ((num = string.Compare(cookie.Name, cookie2.Name, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return num;
			}
			if ((num = string.Compare(cookie.Domain, cookie2.Domain, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return num;
			}
			if ((num = string.Compare(cookie.Path, cookie2.Path, StringComparison.Ordinal)) != 0)
			{
				return num;
			}
			return 0;
		}
	}
}
