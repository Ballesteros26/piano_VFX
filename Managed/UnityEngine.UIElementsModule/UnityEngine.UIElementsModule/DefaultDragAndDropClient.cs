using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010F RID: 271
	internal class DefaultDragAndDropClient : IDragAndDrop, IDragAndDropData
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x000218BF File Offset: 0x0001FABF
		public object userData
		{
			get
			{
				StartDragArgs startDragArgs = this.m_StartDragArgs;
				return (startDragArgs != null) ? startDragArgs.userData : null;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x000218D3 File Offset: 0x0001FAD3
		public IEnumerable<Object> unityObjectReferences
		{
			get
			{
				StartDragArgs startDragArgs = this.m_StartDragArgs;
				return (startDragArgs != null) ? startDragArgs.unityObjectReferences : null;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000218E7 File Offset: 0x0001FAE7
		public void StartDrag(StartDragArgs args)
		{
			this.m_StartDragArgs = args;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000218F1 File Offset: 0x0001FAF1
		public void AcceptDrag()
		{
			this.m_StartDragArgs = null;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000062F3 File Offset: 0x000044F3
		public void SetVisualMode(DragVisualMode visualMode)
		{
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000218FC File Offset: 0x0001FAFC
		public IDragAndDropData data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00021910 File Offset: 0x0001FB10
		public object GetGenericData(string key)
		{
			bool flag = this.m_StartDragArgs == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = (this.m_StartDragArgs.genericData.ContainsKey(key) ? this.m_StartDragArgs.genericData[key] : null);
			}
			return obj;
		}

		// Token: 0x040003B4 RID: 948
		private StartDragArgs m_StartDragArgs;
	}
}
