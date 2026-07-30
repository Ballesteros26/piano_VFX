using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000115 RID: 277
	internal class StartDragArgs
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00021C84 File Offset: 0x0001FE84
		public string title { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00021C8C File Offset: 0x0001FE8C
		public object userData { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00021C94 File Offset: 0x0001FE94
		internal Hashtable genericData
		{
			get
			{
				return this.m_GenericData;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00021C9C File Offset: 0x0001FE9C
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		internal IEnumerable<Object> unityObjectReferences { get; private set; } = null;

		// Token: 0x06000851 RID: 2129 RVA: 0x00021CAD File Offset: 0x0001FEAD
		internal StartDragArgs()
		{
			this.title = string.Empty;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public StartDragArgs(string title, object userData)
		{
			this.title = title;
			this.userData = userData;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00021CFE File Offset: 0x0001FEFE
		public void SetGenericData(string key, object data)
		{
			this.m_GenericData[key] = data;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00021D0F File Offset: 0x0001FF0F
		public void SetUnityObjectReferences(IEnumerable<Object> references)
		{
			this.unityObjectReferences = references;
		}

		// Token: 0x040003C0 RID: 960
		private readonly Hashtable m_GenericData = new Hashtable();
	}
}
