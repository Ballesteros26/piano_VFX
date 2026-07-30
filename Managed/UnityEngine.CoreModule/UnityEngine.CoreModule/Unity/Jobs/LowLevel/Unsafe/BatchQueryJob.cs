using System;
using Unity.Collections;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000040 RID: 64
	public struct BatchQueryJob<CommandT, ResultT> where CommandT : struct where ResultT : struct
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000294A File Offset: 0x00000B4A
		public BatchQueryJob(NativeArray<CommandT> commands, NativeArray<ResultT> results)
		{
			this.commands = commands;
			this.results = results;
		}

		// Token: 0x040000D0 RID: 208
		[ReadOnly]
		internal NativeArray<CommandT> commands;

		// Token: 0x040000D1 RID: 209
		internal NativeArray<ResultT> results;
	}
}
