using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004F RID: 79
	public interface IBitArray
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001DB RID: 475
		uint capacity { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001DC RID: 476
		bool allFalse { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001DD RID: 477
		bool allTrue { get; }

		// Token: 0x1700004A RID: 74
		bool this[uint index] { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E0 RID: 480
		string humanizedData { get; }

		// Token: 0x060001E1 RID: 481
		IBitArray BitAnd(IBitArray other);

		// Token: 0x060001E2 RID: 482
		IBitArray BitOr(IBitArray other);

		// Token: 0x060001E3 RID: 483
		IBitArray BitNot();
	}
}
