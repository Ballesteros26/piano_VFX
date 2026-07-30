using System;
using System.Threading;

namespace System
{
	// Token: 0x02000102 RID: 258
	internal sealed class System_LazyDebugView<T>
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x00031646 File Offset: 0x0002F846
		public System_LazyDebugView(Lazy<T> lazy)
		{
			this.m_lazy = lazy;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00031655 File Offset: 0x0002F855
		public bool IsValueCreated
		{
			get
			{
				return this.m_lazy.IsValueCreated;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00031662 File Offset: 0x0002F862
		public T Value
		{
			get
			{
				return this.m_lazy.ValueForDebugDisplay;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0003166F File Offset: 0x0002F86F
		public LazyThreadSafetyMode Mode
		{
			get
			{
				return this.m_lazy.Mode;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0003167C File Offset: 0x0002F87C
		public bool IsValueFaulted
		{
			get
			{
				return this.m_lazy.IsValueFaulted;
			}
		}

		// Token: 0x04000716 RID: 1814
		private readonly Lazy<T> m_lazy;
	}
}
