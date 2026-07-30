using System;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	public struct JointMotor2D
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005DA8 File Offset: 0x00003FA8
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public float motorSpeed
		{
			get
			{
				return this.m_MotorSpeed;
			}
			set
			{
				this.m_MotorSpeed = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00005DCC File Offset: 0x00003FCC
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public float maxMotorTorque
		{
			get
			{
				return this.m_MaximumMotorTorque;
			}
			set
			{
				this.m_MaximumMotorTorque = value;
			}
		}

		// Token: 0x04000060 RID: 96
		private float m_MotorSpeed;

		// Token: 0x04000061 RID: 97
		private float m_MaximumMotorTorque;
	}
}
