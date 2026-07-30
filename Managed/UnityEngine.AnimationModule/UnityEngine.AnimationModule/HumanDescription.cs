using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public struct HumanDescription
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00003C2C File Offset: 0x00001E2C
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00003C44 File Offset: 0x00001E44
		public float upperArmTwist
		{
			get
			{
				return this.m_ArmTwist;
			}
			set
			{
				this.m_ArmTwist = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00003C50 File Offset: 0x00001E50
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00003C68 File Offset: 0x00001E68
		public float lowerArmTwist
		{
			get
			{
				return this.m_ForeArmTwist;
			}
			set
			{
				this.m_ForeArmTwist = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00003C74 File Offset: 0x00001E74
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00003C8C File Offset: 0x00001E8C
		public float upperLegTwist
		{
			get
			{
				return this.m_UpperLegTwist;
			}
			set
			{
				this.m_UpperLegTwist = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00003C98 File Offset: 0x00001E98
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public float lowerLegTwist
		{
			get
			{
				return this.m_LegTwist;
			}
			set
			{
				this.m_LegTwist = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00003CBC File Offset: 0x00001EBC
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public float armStretch
		{
			get
			{
				return this.m_ArmStretch;
			}
			set
			{
				this.m_ArmStretch = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00003CE0 File Offset: 0x00001EE0
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public float legStretch
		{
			get
			{
				return this.m_LegStretch;
			}
			set
			{
				this.m_LegStretch = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00003D04 File Offset: 0x00001F04
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00003D1C File Offset: 0x00001F1C
		public float feetSpacing
		{
			get
			{
				return this.m_FeetSpacing;
			}
			set
			{
				this.m_FeetSpacing = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00003D28 File Offset: 0x00001F28
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00003D40 File Offset: 0x00001F40
		public bool hasTranslationDoF
		{
			get
			{
				return this.m_HasTranslationDoF;
			}
			set
			{
				this.m_HasTranslationDoF = value;
			}
		}

		// Token: 0x04000115 RID: 277
		[NativeName("m_Human")]
		public HumanBone[] human;

		// Token: 0x04000116 RID: 278
		[NativeName("m_Skeleton")]
		public SkeletonBone[] skeleton;

		// Token: 0x04000117 RID: 279
		internal float m_ArmTwist;

		// Token: 0x04000118 RID: 280
		internal float m_ForeArmTwist;

		// Token: 0x04000119 RID: 281
		internal float m_UpperLegTwist;

		// Token: 0x0400011A RID: 282
		internal float m_LegTwist;

		// Token: 0x0400011B RID: 283
		internal float m_ArmStretch;

		// Token: 0x0400011C RID: 284
		internal float m_LegStretch;

		// Token: 0x0400011D RID: 285
		internal float m_FeetSpacing;

		// Token: 0x0400011E RID: 286
		internal float m_GlobalScale;

		// Token: 0x0400011F RID: 287
		internal string m_RootMotionBoneName;

		// Token: 0x04000120 RID: 288
		internal bool m_HasTranslationDoF;

		// Token: 0x04000121 RID: 289
		internal bool m_HasExtraRoot;

		// Token: 0x04000122 RID: 290
		internal bool m_SkeletonHasParents;
	}
}
