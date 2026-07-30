using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000064 RID: 100
	internal class TargetPool
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000FE26 File Offset: 0x0000E026
		internal TargetPool()
		{
			this.m_Pool = new List<int>();
			this.Get();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000FE40 File Offset: 0x0000E040
		internal int Get()
		{
			int num = this.Get(this.m_Current);
			this.m_Current++;
			return num;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000FE5C File Offset: 0x0000E05C
		private int Get(int i)
		{
			int num;
			if (this.m_Pool.Count > i)
			{
				num = this.m_Pool[i];
			}
			else
			{
				while (this.m_Pool.Count <= i)
				{
					this.m_Pool.Add(Shader.PropertyToID("_TargetPool" + i));
				}
				num = this.m_Pool[i];
			}
			return num;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000FEC2 File Offset: 0x0000E0C2
		internal void Reset()
		{
			this.m_Current = 0;
		}

		// Token: 0x0400023C RID: 572
		private readonly List<int> m_Pool;

		// Token: 0x0400023D RID: 573
		private int m_Current;
	}
}
