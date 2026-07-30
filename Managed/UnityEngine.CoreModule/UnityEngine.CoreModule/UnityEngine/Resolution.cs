using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E8 RID: 232
	[RequiredByNativeCode]
	public struct Resolution
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000C300 File Offset: 0x0000A500
		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000C30C File Offset: 0x0000A50C
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0000C324 File Offset: 0x0000A524
		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0000C330 File Offset: 0x0000A530
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0000C348 File Offset: 0x0000A548
		public int refreshRate
		{
			get
			{
				return this.m_RefreshRate;
			}
			set
			{
				this.m_RefreshRate = value;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000C354 File Offset: 0x0000A554
		public override string ToString()
		{
			return UnityString.Format("{0} x {1} @ {2}Hz", new object[] { this.m_Width, this.m_Height, this.m_RefreshRate });
		}

		// Token: 0x04000284 RID: 644
		private int m_Width;

		// Token: 0x04000285 RID: 645
		private int m_Height;

		// Token: 0x04000286 RID: 646
		private int m_RefreshRate;
	}
}
