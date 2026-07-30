using System;

namespace UnityEngine
{
	// Token: 0x020001CB RID: 459
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x000218A5 File Offset: 0x0001FAA5
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x000218AD File Offset: 0x0001FAAD
		public float waitTime { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000218B8 File Offset: 0x0001FAB8
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_WaitUntilTime < 0f;
				if (flag)
				{
					this.m_WaitUntilTime = Time.realtimeSinceStartup + this.waitTime;
				}
				bool flag2 = Time.realtimeSinceStartup < this.m_WaitUntilTime;
				bool flag3 = !flag2;
				if (flag3)
				{
					this.Reset();
				}
				return flag2;
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0002190F File Offset: 0x0001FB0F
		public WaitForSecondsRealtime(float time)
		{
			this.waitTime = time;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0002192C File Offset: 0x0001FB2C
		public override void Reset()
		{
			this.m_WaitUntilTime = -1f;
		}

		// Token: 0x04000684 RID: 1668
		private float m_WaitUntilTime = -1f;
	}
}
