using System;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public struct JointDrive
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("JointDriveMode is obsolete")]
		public JointDriveMode mode
		{
			get
			{
				return JointDriveMode.None;
			}
			set
			{
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002218 File Offset: 0x00000418
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002230 File Offset: 0x00000430
		public float positionSpring
		{
			get
			{
				return this.m_PositionSpring;
			}
			set
			{
				this.m_PositionSpring = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002254 File Offset: 0x00000454
		public float positionDamper
		{
			get
			{
				return this.m_PositionDamper;
			}
			set
			{
				this.m_PositionDamper = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002260 File Offset: 0x00000460
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002278 File Offset: 0x00000478
		public float maximumForce
		{
			get
			{
				return this.m_MaximumForce;
			}
			set
			{
				this.m_MaximumForce = value;
			}
		}

		// Token: 0x0400002B RID: 43
		private float m_PositionSpring;

		// Token: 0x0400002C RID: 44
		private float m_PositionDamper;

		// Token: 0x0400002D RID: 45
		private float m_MaximumForce;
	}
}
