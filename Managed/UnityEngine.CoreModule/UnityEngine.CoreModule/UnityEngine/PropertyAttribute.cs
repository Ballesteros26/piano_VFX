using System;

namespace UnityEngine
{
	// Token: 0x0200017E RID: 382
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0001E785 File Offset: 0x0001C985
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0001E78D File Offset: 0x0001C98D
		public int order { get; set; }
	}
}
