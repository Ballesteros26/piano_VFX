using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildMarkup
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00002DF3 File Offset: 0x00000FF3
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea != 0;
			}
			set
			{
				this.m_OverrideArea = (value ? 1 : 0);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002E04 File Offset: 0x00001004
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00002E1C File Offset: 0x0000101C
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002E28 File Offset: 0x00001028
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002E43 File Offset: 0x00001043
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild != 0;
			}
			set
			{
				this.m_IgnoreFromBuild = (value ? 1 : 0);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002E54 File Offset: 0x00001054
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00002E71 File Offset: 0x00001071
		public Transform root
		{
			get
			{
				return NavMeshBuildMarkup.InternalGetRootGO(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x06000147 RID: 327
		[StaticAccessor("NavMeshBuildMarkup", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern Transform InternalGetRootGO(int instanceID);

		// Token: 0x0400004B RID: 75
		private int m_OverrideArea;

		// Token: 0x0400004C RID: 76
		private int m_Area;

		// Token: 0x0400004D RID: 77
		private int m_IgnoreFromBuild;

		// Token: 0x0400004E RID: 78
		private int m_InstanceID;
	}
}
