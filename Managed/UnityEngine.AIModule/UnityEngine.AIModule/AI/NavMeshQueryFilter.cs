using System;

namespace UnityEngine.AI
{
	// Token: 0x02000012 RID: 18
	public struct NavMeshQueryFilter
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000028EB File Offset: 0x00000AEB
		internal float[] costs { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000028F4 File Offset: 0x00000AF4
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000028FC File Offset: 0x00000AFC
		public int areaMask { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002905 File Offset: 0x00000B05
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000290D File Offset: 0x00000B0D
		public int agentTypeID { get; set; }

		// Token: 0x060000E9 RID: 233 RVA: 0x00002918 File Offset: 0x00000B18
		public float GetAreaCost(int areaIndex)
		{
			bool flag = this.costs == null;
			float num;
			if (flag)
			{
				bool flag2 = areaIndex < 0 || areaIndex >= 32;
				if (flag2)
				{
					string text = string.Format("The valid range is [0:{0}]", 31);
					throw new IndexOutOfRangeException(text);
				}
				num = 1f;
			}
			else
			{
				num = this.costs[areaIndex];
			}
			return num;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002978 File Offset: 0x00000B78
		public void SetAreaCost(int areaIndex, float cost)
		{
			bool flag = this.costs == null;
			if (flag)
			{
				this.costs = new float[32];
				for (int i = 0; i < 32; i++)
				{
					this.costs[i] = 1f;
				}
			}
			this.costs[areaIndex] = cost;
		}

		// Token: 0x0400002B RID: 43
		private const int k_AreaCostElementCount = 32;
	}
}
