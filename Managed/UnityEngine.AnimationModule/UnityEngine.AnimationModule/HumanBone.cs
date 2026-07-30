using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	public struct HumanBone
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00003BE4 File Offset: 0x00001DE4
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00003BFC File Offset: 0x00001DFC
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00003C08 File Offset: 0x00001E08
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00003C20 File Offset: 0x00001E20
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000112 RID: 274
		private string m_BoneName;

		// Token: 0x04000113 RID: 275
		private string m_HumanName;

		// Token: 0x04000114 RID: 276
		[NativeName("m_Limit")]
		public HumanLimit limit;
	}
}
