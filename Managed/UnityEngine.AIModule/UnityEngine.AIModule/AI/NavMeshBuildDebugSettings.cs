using System;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/AI/Public/NavMeshBuildDebugSettings.h")]
	public struct NavMeshBuildDebugSettings
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000030A4 File Offset: 0x000012A4
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000030BC File Offset: 0x000012BC
		public NavMeshBuildDebugFlags flags
		{
			get
			{
				return (NavMeshBuildDebugFlags)this.m_Flags;
			}
			set
			{
				this.m_Flags = (byte)value;
			}
		}

		// Token: 0x0400005F RID: 95
		private byte m_Flags;
	}
}
