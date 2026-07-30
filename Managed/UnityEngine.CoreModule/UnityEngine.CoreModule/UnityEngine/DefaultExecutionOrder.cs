using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019C RID: 412
	[UsedByNativeCode]
	[AttributeUsage(4)]
	public class DefaultExecutionOrder : Attribute
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public DefaultExecutionOrder(int order)
		{
			this.m_Order = order;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0001F40C File Offset: 0x0001D60C
		public int order
		{
			get
			{
				return this.m_Order;
			}
		}

		// Token: 0x04000644 RID: 1604
		private int m_Order;
	}
}
