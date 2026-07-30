using System;

namespace Mono.Net.Security
{
	// Token: 0x0200006A RID: 106
	internal class AsyncHandshakeRequest : AsyncProtocolRequest
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00006086 File Offset: 0x00004286
		public AsyncHandshakeRequest(MobileAuthenticatedStream parent, bool sync)
			: base(parent, sync)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006090 File Offset: 0x00004290
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status);
		}
	}
}
