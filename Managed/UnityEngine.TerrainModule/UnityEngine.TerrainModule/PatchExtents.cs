using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[UsedByNativeCode]
	public struct PatchExtents
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002B30 File Offset: 0x00000D30
		public float min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002B54 File Offset: 0x00000D54
		public float max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x04000039 RID: 57
		internal float m_min;

		// Token: 0x0400003A RID: 58
		internal float m_max;
	}
}
