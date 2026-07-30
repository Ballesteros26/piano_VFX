using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000042 RID: 66
	public static class JobHandleUnsafeUtility
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000029A0 File Offset: 0x00000BA0
		public unsafe static JobHandle CombineDependencies(JobHandle* jobs, int count)
		{
			return JobHandle.CombineDependenciesInternalPtr((void*)jobs, count);
		}
	}
}
