using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000115 RID: 277
	internal class JaggedArray<TElement>
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public static TElement[][] Allocate(int size1, int size2)
		{
			TElement[][] array = new TElement[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TElement[size2];
			}
			return array;
		}
	}
}
