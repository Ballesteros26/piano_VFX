using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007A RID: 122
	internal struct HDProbeCullingResults
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0002BC84 File Offset: 0x00029E84
		public IReadOnlyList<HDProbe> visibleProbes
		{
			get
			{
				IReadOnlyList<HDProbe> visibleProbes = this.m_VisibleProbes;
				return visibleProbes ?? HDProbeCullingResults.s_EmptyList;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0002BCA2 File Offset: 0x00029EA2
		internal List<HDProbe> writeableVisibleProbes
		{
			get
			{
				return this.m_VisibleProbes;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002BCAA File Offset: 0x00029EAA
		internal void Reset()
		{
			if (this.m_VisibleProbes == null)
			{
				this.m_VisibleProbes = new List<HDProbe>();
				return;
			}
			this.m_VisibleProbes.Clear();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0002BCCB File Offset: 0x00029ECB
		internal void Set(List<HDProbe> visibleProbes)
		{
			this.m_VisibleProbes.AddRange(visibleProbes);
		}

		// Token: 0x04000525 RID: 1317
		private static readonly IReadOnlyList<HDProbe> s_EmptyList = new List<HDProbe>();

		// Token: 0x04000526 RID: 1318
		private List<HDProbe> m_VisibleProbes;
	}
}
