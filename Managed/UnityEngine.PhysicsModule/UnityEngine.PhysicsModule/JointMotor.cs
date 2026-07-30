using System;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	public struct JointMotor
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000229C File Offset: 0x0000049C
		public float targetVelocity
		{
			get
			{
				return this.m_TargetVelocity;
			}
			set
			{
				this.m_TargetVelocity = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022A8 File Offset: 0x000004A8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022C0 File Offset: 0x000004C0
		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022E7 File Offset: 0x000004E7
		public bool freeSpin
		{
			get
			{
				return this.m_FreeSpin == 1;
			}
			set
			{
				this.m_FreeSpin = (value ? 1 : 0);
			}
		}

		// Token: 0x04000032 RID: 50
		private float m_TargetVelocity;

		// Token: 0x04000033 RID: 51
		private float m_Force;

		// Token: 0x04000034 RID: 52
		private int m_FreeSpin;
	}
}
