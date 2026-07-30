using System;

namespace UnityEngine
{
	// Token: 0x020000D1 RID: 209
	[AttributeUsage(64)]
	public class BeforeRenderOrderAttribute : Attribute
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00009B6F File Offset: 0x00007D6F
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00009B77 File Offset: 0x00007D77
		public int order { get; private set; }

		// Token: 0x060005FC RID: 1532 RVA: 0x00009B80 File Offset: 0x00007D80
		public BeforeRenderOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
