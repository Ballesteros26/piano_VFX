using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000836 RID: 2102
	public interface ITuple
	{
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060053A3 RID: 21411
		int Length { get; }

		// Token: 0x17000EA2 RID: 3746
		object this[int index] { get; }
	}
}
