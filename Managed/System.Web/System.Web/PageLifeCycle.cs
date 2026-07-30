using System;

// Token: 0x02000005 RID: 5
internal enum PageLifeCycle
{
	// Token: 0x04000D19 RID: 3353
	Unknown = 1,
	// Token: 0x04000D1A RID: 3354
	Start,
	// Token: 0x04000D1B RID: 3355
	PreInit,
	// Token: 0x04000D1C RID: 3356
	Init,
	// Token: 0x04000D1D RID: 3357
	InitComplete,
	// Token: 0x04000D1E RID: 3358
	PreLoad,
	// Token: 0x04000D1F RID: 3359
	Load,
	// Token: 0x04000D20 RID: 3360
	ControlEvents,
	// Token: 0x04000D21 RID: 3361
	LoadComplete,
	// Token: 0x04000D22 RID: 3362
	PreRender,
	// Token: 0x04000D23 RID: 3363
	PreRenderComplete,
	// Token: 0x04000D24 RID: 3364
	SaveStateComplete,
	// Token: 0x04000D25 RID: 3365
	Render,
	// Token: 0x04000D26 RID: 3366
	Unload,
	// Token: 0x04000D27 RID: 3367
	End
}
