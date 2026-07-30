using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D7 RID: 215
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x06000750 RID: 1872
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x06000751 RID: 1873
		string ToStringEnd();
	}
}
