using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000123 RID: 291
	public class AOVRequestDataCollection : IEnumerable<AOVRequestData>, IEnumerable, IDisposable
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0004874B File Offset: 0x0004694B
		public AOVRequestDataCollection(List<AOVRequestData> aovRequestData)
		{
			this.m_AOVRequestData = aovRequestData;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0004875C File Offset: 0x0004695C
		public IEnumerator<AOVRequestData> GetEnumerator()
		{
			IEnumerable<AOVRequestData> aovrequestData = this.m_AOVRequestData;
			return (aovrequestData ?? Enumerable.Empty<AOVRequestData>()).GetEnumerator();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0004877F File Offset: 0x0004697F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00048787 File Offset: 0x00046987
		public void Dispose()
		{
			if (this.m_AOVRequestData == null)
			{
				return;
			}
			ListPool<AOVRequestData>.Release(this.m_AOVRequestData);
			this.m_AOVRequestData = null;
		}

		// Token: 0x04000D9C RID: 3484
		private List<AOVRequestData> m_AOVRequestData;
	}
}
