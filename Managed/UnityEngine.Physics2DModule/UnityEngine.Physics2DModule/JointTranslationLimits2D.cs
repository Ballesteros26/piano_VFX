using System;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	public struct JointTranslationLimits2D
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005D60 File Offset: 0x00003F60
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00005D78 File Offset: 0x00003F78
		public float min
		{
			get
			{
				return this.m_LowerTranslation;
			}
			set
			{
				this.m_LowerTranslation = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005D84 File Offset: 0x00003F84
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00005D9C File Offset: 0x00003F9C
		public float max
		{
			get
			{
				return this.m_UpperTranslation;
			}
			set
			{
				this.m_UpperTranslation = value;
			}
		}

		// Token: 0x0400005E RID: 94
		private float m_LowerTranslation;

		// Token: 0x0400005F RID: 95
		private float m_UpperTranslation;
	}
}
