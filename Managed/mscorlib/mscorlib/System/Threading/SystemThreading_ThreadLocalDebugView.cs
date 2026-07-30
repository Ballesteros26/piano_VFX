using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000468 RID: 1128
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x060035A9 RID: 13737 RVA: 0x000C6AF4 File Offset: 0x000C4CF4
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000C6B03 File Offset: 0x000C4D03
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000C6B10 File Offset: 0x000C4D10
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000C6B1D File Offset: 0x000C4D1D
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04001C9E RID: 7326
		private readonly ThreadLocal<T> m_tlocal;
	}
}
