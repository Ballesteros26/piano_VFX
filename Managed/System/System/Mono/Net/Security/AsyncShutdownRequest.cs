using System;

namespace Mono.Net.Security
{
	// Token: 0x0200006E RID: 110
	internal class AsyncShutdownRequest : AsyncProtocolRequest
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00006219 File Offset: 0x00004419
		public AsyncShutdownRequest(MobileAuthenticatedStream parent)
			: base(parent, false)
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006223 File Offset: 0x00004423
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessShutdown(status);
		}
	}
}
