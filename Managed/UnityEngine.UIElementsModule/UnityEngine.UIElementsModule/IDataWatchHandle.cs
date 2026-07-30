using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000022 RID: 34
	internal interface IDataWatchHandle : IDisposable
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B4 RID: 180
		Object watched { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B5 RID: 181
		bool disposed { get; }
	}
}
