using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020000E8 RID: 232
	internal interface ITupleInternal : ITuple
	{
		// Token: 0x060008A8 RID: 2216
		string ToString(StringBuilder sb);

		// Token: 0x060008A9 RID: 2217
		int GetHashCode(IEqualityComparer comparer);
	}
}
