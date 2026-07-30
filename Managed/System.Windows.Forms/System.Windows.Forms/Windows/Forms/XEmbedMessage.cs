using System;

namespace System.Windows.Forms
{
	// Token: 0x02000437 RID: 1079
	internal enum XEmbedMessage
	{
		// Token: 0x0400226E RID: 8814
		EmbeddedNotify,
		// Token: 0x0400226F RID: 8815
		WindowActivate,
		// Token: 0x04002270 RID: 8816
		WindowDeactivate,
		// Token: 0x04002271 RID: 8817
		RequestFocus,
		// Token: 0x04002272 RID: 8818
		FocusIn,
		// Token: 0x04002273 RID: 8819
		FocusOut,
		// Token: 0x04002274 RID: 8820
		FocusNext,
		// Token: 0x04002275 RID: 8821
		FocusPrev,
		// Token: 0x04002276 RID: 8822
		ModalityOn = 10,
		// Token: 0x04002277 RID: 8823
		ModalityOff,
		// Token: 0x04002278 RID: 8824
		RegisterAccelerator,
		// Token: 0x04002279 RID: 8825
		UnregisterAccelerator,
		// Token: 0x0400227A RID: 8826
		ActivateAccelerator
	}
}
