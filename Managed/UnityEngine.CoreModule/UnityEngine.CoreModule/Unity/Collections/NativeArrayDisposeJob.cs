using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000060 RID: 96
	internal struct NativeArrayDisposeJob : IJob
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000348A File Offset: 0x0000168A
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x0400011D RID: 285
		internal NativeArrayDispose Data;
	}
}
