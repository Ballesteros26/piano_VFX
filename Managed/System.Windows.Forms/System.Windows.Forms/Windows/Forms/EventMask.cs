using System;

namespace System.Windows.Forms
{
	// Token: 0x02000403 RID: 1027
	[Flags]
	internal enum EventMask
	{
		// Token: 0x04001FE6 RID: 8166
		NoEventMask = 0,
		// Token: 0x04001FE7 RID: 8167
		KeyPressMask = 1,
		// Token: 0x04001FE8 RID: 8168
		KeyReleaseMask = 2,
		// Token: 0x04001FE9 RID: 8169
		ButtonPressMask = 4,
		// Token: 0x04001FEA RID: 8170
		ButtonReleaseMask = 8,
		// Token: 0x04001FEB RID: 8171
		EnterWindowMask = 16,
		// Token: 0x04001FEC RID: 8172
		LeaveWindowMask = 32,
		// Token: 0x04001FED RID: 8173
		PointerMotionMask = 64,
		// Token: 0x04001FEE RID: 8174
		PointerMotionHintMask = 128,
		// Token: 0x04001FEF RID: 8175
		Button1MotionMask = 256,
		// Token: 0x04001FF0 RID: 8176
		Button2MotionMask = 512,
		// Token: 0x04001FF1 RID: 8177
		Button3MotionMask = 1024,
		// Token: 0x04001FF2 RID: 8178
		Button4MotionMask = 2048,
		// Token: 0x04001FF3 RID: 8179
		Button5MotionMask = 4096,
		// Token: 0x04001FF4 RID: 8180
		ButtonMotionMask = 8192,
		// Token: 0x04001FF5 RID: 8181
		KeymapStateMask = 16384,
		// Token: 0x04001FF6 RID: 8182
		ExposureMask = 32768,
		// Token: 0x04001FF7 RID: 8183
		VisibilityChangeMask = 65536,
		// Token: 0x04001FF8 RID: 8184
		StructureNotifyMask = 131072,
		// Token: 0x04001FF9 RID: 8185
		ResizeRedirectMask = 262144,
		// Token: 0x04001FFA RID: 8186
		SubstructureNotifyMask = 524288,
		// Token: 0x04001FFB RID: 8187
		SubstructureRedirectMask = 1048576,
		// Token: 0x04001FFC RID: 8188
		FocusChangeMask = 2097152,
		// Token: 0x04001FFD RID: 8189
		PropertyChangeMask = 4194304,
		// Token: 0x04001FFE RID: 8190
		ColormapChangeMask = 8388608,
		// Token: 0x04001FFF RID: 8191
		OwnerGrabButtonMask = 16777216
	}
}
