using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001FC RID: 508
	internal static class CancellableEnumerable
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x0002A698 File Offset: 0x00028898
		internal static IEnumerable<TElement> Wrap<TElement>(IEnumerable<TElement> source, CancellationToken token)
		{
			int count = 0;
			foreach (TElement telement in source)
			{
				int num = count;
				count = num + 1;
				if ((num & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(token);
				}
				yield return telement;
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}
	}
}
