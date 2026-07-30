using System;
using System.Runtime.ExceptionServices;

namespace Mono.Net.Security
{
	// Token: 0x02000065 RID: 101
	internal class AsyncProtocolResult
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000059B8 File Offset: 0x00003BB8
		public int UserResult { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000059C0 File Offset: 0x00003BC0
		public ExceptionDispatchInfo Error { get; }

		// Token: 0x060001D7 RID: 471 RVA: 0x000059C8 File Offset: 0x00003BC8
		public AsyncProtocolResult(int result)
		{
			this.UserResult = result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000059D7 File Offset: 0x00003BD7
		public AsyncProtocolResult(ExceptionDispatchInfo error)
		{
			this.Error = error;
		}
	}
}
