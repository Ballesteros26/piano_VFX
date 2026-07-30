using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B2 RID: 434
	[RequiredByNativeCode]
	[AttributeUsage(64, AllowMultiple = false)]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x000204E8 File Offset: 0x0001E6E8
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000204FA File Offset: 0x0001E6FA
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0002050C File Offset: 0x0001E70C
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00020524 File Offset: 0x0001E724
		public RuntimeInitializeLoadType loadType
		{
			get
			{
				return this.m_LoadType;
			}
			private set
			{
				this.m_LoadType = value;
			}
		}

		// Token: 0x04000657 RID: 1623
		private RuntimeInitializeLoadType m_LoadType;
	}
}
