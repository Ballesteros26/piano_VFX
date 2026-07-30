using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000099 RID: 153
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0000422C File Offset: 0x0000242C
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000427B File Offset: 0x0000247B
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000042B8 File Offset: 0x000024B8
		public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 3;
			this.m_InWeight = inWeight;
			this.m_OutWeight = outWeight;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000042F0 File Offset: 0x000024F0
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00004308 File Offset: 0x00002508
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004314 File Offset: 0x00002514
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000432C File Offset: 0x0000252C
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00004338 File Offset: 0x00002538
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00004350 File Offset: 0x00002550
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000435C File Offset: 0x0000255C
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00004374 File Offset: 0x00002574
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004380 File Offset: 0x00002580
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00004398 File Offset: 0x00002598
		public float inWeight
		{
			get
			{
				return this.m_InWeight;
			}
			set
			{
				this.m_InWeight = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000043A4 File Offset: 0x000025A4
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000043BC File Offset: 0x000025BC
		public float outWeight
		{
			get
			{
				return this.m_OutWeight;
			}
			set
			{
				this.m_OutWeight = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000043C8 File Offset: 0x000025C8
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000043E0 File Offset: 0x000025E0
		public WeightedMode weightedMode
		{
			get
			{
				return (WeightedMode)this.m_WeightedMode;
			}
			set
			{
				this.m_WeightedMode = (int)value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000043EC File Offset: 0x000025EC
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00004404 File Offset: 0x00002604
		[Obsolete("Use AnimationUtility.SetKeyLeftTangentMode, AnimationUtility.SetKeyRightTangentMode, AnimationUtility.GetKeyLeftTangentMode or AnimationUtility.GetKeyRightTangentMode instead.")]
		public int tangentMode
		{
			get
			{
				return this.tangentModeInternal;
			}
			set
			{
				this.tangentModeInternal = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004410 File Offset: 0x00002610
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00002EC3 File Offset: 0x000010C3
		internal int tangentModeInternal
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x040001B3 RID: 435
		private float m_Time;

		// Token: 0x040001B4 RID: 436
		private float m_Value;

		// Token: 0x040001B5 RID: 437
		private float m_InTangent;

		// Token: 0x040001B6 RID: 438
		private float m_OutTangent;

		// Token: 0x040001B7 RID: 439
		private int m_WeightedMode;

		// Token: 0x040001B8 RID: 440
		private float m_InWeight;

		// Token: 0x040001B9 RID: 441
		private float m_OutWeight;
	}
}
