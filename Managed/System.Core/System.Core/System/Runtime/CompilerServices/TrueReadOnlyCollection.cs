using System;
using System.Collections.ObjectModel;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020002FF RID: 767
	internal sealed class TrueReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x0004CDA5 File Offset: 0x0004AFA5
		public TrueReadOnlyCollection(params T[] list)
			: base(list)
		{
		}
	}
}
