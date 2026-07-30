using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B7 RID: 439
	internal interface IStyleValue<T>
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000D6E RID: 3438
		// (set) Token: 0x06000D6F RID: 3439
		T value { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000D70 RID: 3440
		// (set) Token: 0x06000D71 RID: 3441
		StyleKeyword keyword { get; set; }
	}
}
