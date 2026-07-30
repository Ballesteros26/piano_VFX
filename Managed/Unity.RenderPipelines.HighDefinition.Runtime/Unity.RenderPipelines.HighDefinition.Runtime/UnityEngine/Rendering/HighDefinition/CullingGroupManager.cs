using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000FA RID: 250
	internal class CullingGroupManager
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00041B89 File Offset: 0x0003FD89
		public static CullingGroupManager instance
		{
			get
			{
				if (CullingGroupManager.m_Instance == null)
				{
					CullingGroupManager.m_Instance = new CullingGroupManager();
				}
				return CullingGroupManager.m_Instance;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00041BA4 File Offset: 0x0003FDA4
		public CullingGroup Alloc()
		{
			CullingGroup cullingGroup;
			if (this.m_FreeList.Count > 0)
			{
				cullingGroup = this.m_FreeList.Pop();
			}
			else
			{
				cullingGroup = new CullingGroup();
			}
			return cullingGroup;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		public void Free(CullingGroup group)
		{
			this.m_FreeList.Push(group);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00041BE4 File Offset: 0x0003FDE4
		public void Cleanup()
		{
			foreach (CullingGroup cullingGroup in this.m_FreeList)
			{
				cullingGroup.Dispose();
			}
			this.m_FreeList.Clear();
		}

		// Token: 0x040008DC RID: 2268
		private static CullingGroupManager m_Instance;

		// Token: 0x040008DD RID: 2269
		private Stack<CullingGroup> m_FreeList = new Stack<CullingGroup>();
	}
}
