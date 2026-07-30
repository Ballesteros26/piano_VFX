using System;

namespace System.Web
{
	// Token: 0x0200005D RID: 93
	internal enum WebSocketTransitionState : byte
	{
		// Token: 0x04000E31 RID: 3633
		Inactive,
		// Token: 0x04000E32 RID: 3634
		AcceptWebSocketRequestCalled,
		// Token: 0x04000E33 RID: 3635
		TransitionStarted,
		// Token: 0x04000E34 RID: 3636
		TransitionCompleted
	}
}
