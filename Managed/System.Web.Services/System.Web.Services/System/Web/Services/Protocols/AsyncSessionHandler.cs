using System;
using System.Web.SessionState;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008D RID: 141
	internal class AsyncSessionHandler : AsyncSessionlessHandler, IRequiresSessionState
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00011D1B File Offset: 0x0000FF1B
		internal AsyncSessionHandler(ServerProtocol protocol)
			: base(protocol)
		{
		}
	}
}
