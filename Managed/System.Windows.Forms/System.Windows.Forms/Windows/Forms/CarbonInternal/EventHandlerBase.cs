using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A9 RID: 1193
	internal abstract class EventHandlerBase
	{
		// Token: 0x06004BE7 RID: 19431 RVA: 0x0012DE98 File Offset: 0x0012C098
		public EventHandlerBase()
		{
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0012DEA0 File Offset: 0x0012C0A0
		public EventHandlerBase(XplatUICarbon driver)
		{
			this.Driver = driver;
		}

		// Token: 0x040028E4 RID: 10468
		internal XplatUICarbon Driver;
	}
}
