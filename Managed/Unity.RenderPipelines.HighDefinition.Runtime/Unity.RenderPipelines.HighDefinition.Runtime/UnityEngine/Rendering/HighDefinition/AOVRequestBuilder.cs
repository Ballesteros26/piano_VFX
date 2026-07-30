using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200011F RID: 287
	public class AOVRequestBuilder : IDisposable
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x000484DC File Offset: 0x000466DC
		public AOVRequestBuilder Add(AOVRequest settings, AOVRequestBufferAllocator bufferAllocator, List<GameObject> includedLightList, AOVBuffers[] aovBuffers, FramePassCallback callback)
		{
			List<AOVRequestData> list;
			if ((list = this.m_AOVRequestDataData) == null)
			{
				list = (this.m_AOVRequestDataData = ListPool<AOVRequestData>.Get());
			}
			list.Add(new AOVRequestData(settings, bufferAllocator, includedLightList, aovBuffers, callback));
			return this;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00048513 File Offset: 0x00046713
		public AOVRequestDataCollection Build()
		{
			AOVRequestDataCollection aovrequestDataCollection = new AOVRequestDataCollection(this.m_AOVRequestDataData);
			this.m_AOVRequestDataData = null;
			return aovrequestDataCollection;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00048527 File Offset: 0x00046727
		public void Dispose()
		{
			if (this.m_AOVRequestDataData == null)
			{
				return;
			}
			ListPool<AOVRequestData>.Release(this.m_AOVRequestDataData);
			this.m_AOVRequestDataData = null;
		}

		// Token: 0x04000D94 RID: 3476
		private List<AOVRequestData> m_AOVRequestDataData;
	}
}
