using System;

namespace System.Linq
{
	// Token: 0x020000F7 RID: 247
	internal abstract class CachingComparer<TElement>
	{
		// Token: 0x060008B5 RID: 2229
		internal abstract int Compare(TElement element, bool cacheLower);

		// Token: 0x060008B6 RID: 2230
		internal abstract void SetElement(TElement element);
	}
}
