using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class TMP_Asset : ScriptableObject
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002712 File Offset: 0x00000912
		public int instanceID
		{
			get
			{
				if (this.m_InstanceID == 0)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x04000019 RID: 25
		private int m_InstanceID;

		// Token: 0x0400001A RID: 26
		public int hashCode;

		// Token: 0x0400001B RID: 27
		public Material material;

		// Token: 0x0400001C RID: 28
		public int materialHashCode;
	}
}
