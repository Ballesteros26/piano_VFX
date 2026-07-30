using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000079 RID: 121
	internal struct HDProbeCullState
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0002BC52 File Offset: 0x00029E52
		internal CullingGroup cullingGroup
		{
			get
			{
				return this.m_CullingGroup;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0002BC5A File Offset: 0x00029E5A
		internal HDProbe[] hdProbes
		{
			get
			{
				return this.m_HDProbes;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0002BC62 File Offset: 0x00029E62
		internal Hash128 stateHash
		{
			get
			{
				return this.m_StateHash;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002BC6A File Offset: 0x00029E6A
		internal HDProbeCullState(CullingGroup cullingGroup, HDProbe[] hdProbes, Hash128 stateHash)
		{
			this.m_CullingGroup = cullingGroup;
			this.m_HDProbes = hdProbes;
			this.m_StateHash = stateHash;
		}

		// Token: 0x04000522 RID: 1314
		private CullingGroup m_CullingGroup;

		// Token: 0x04000523 RID: 1315
		private HDProbe[] m_HDProbes;

		// Token: 0x04000524 RID: 1316
		private Hash128 m_StateHash;
	}
}
