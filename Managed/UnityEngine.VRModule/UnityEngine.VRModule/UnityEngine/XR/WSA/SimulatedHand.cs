using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000016 RID: 22
	internal class SimulatedHand
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00002589 File Offset: 0x00000789
		internal SimulatedHand(Handedness hand)
		{
			this.m_Hand = hand;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000259C File Offset: 0x0000079C
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000025B9 File Offset: 0x000007B9
		public Vector3 position
		{
			get
			{
				return HolographicAutomation.GetHandPosition(this.m_Hand);
			}
			set
			{
				HolographicAutomation.SetHandPosition(this.m_Hand, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000025E9 File Offset: 0x000007E9
		public bool activated
		{
			get
			{
				return HolographicAutomation.GetHandActivated(this.m_Hand);
			}
			set
			{
				HolographicAutomation.SetHandActivated(this.m_Hand, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000025FC File Offset: 0x000007FC
		public bool visible
		{
			get
			{
				return HolographicAutomation.GetHandVisible(this.m_Hand);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002619 File Offset: 0x00000819
		public void EnsureVisible()
		{
			HolographicAutomation.EnsureHandVisible(this.m_Hand);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002628 File Offset: 0x00000828
		public void PerformGesture(SimulatedGesture gesture)
		{
			HolographicAutomation.PerformGesture(this.m_Hand, gesture);
		}

		// Token: 0x04000037 RID: 55
		public Handedness m_Hand;
	}
}
