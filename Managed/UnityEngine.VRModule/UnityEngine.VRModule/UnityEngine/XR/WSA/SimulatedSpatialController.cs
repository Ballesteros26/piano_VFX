using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000013 RID: 19
	internal class SimulatedSpatialController
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000023ED File Offset: 0x000005ED
		internal SimulatedSpatialController(Handedness controller)
		{
			this.m_ControllerHandednss = controller;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000241D File Offset: 0x0000061D
		public Quaternion orientation
		{
			get
			{
				return HolographicAutomation.GetHandOrientation(this.m_ControllerHandednss);
			}
			set
			{
				HolographicAutomation.TrySetHandOrientation(this.m_ControllerHandednss, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002430 File Offset: 0x00000630
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000244D File Offset: 0x0000064D
		public Vector3 position
		{
			get
			{
				return HolographicAutomation.GetControllerPosition(this.m_ControllerHandednss);
			}
			set
			{
				HolographicAutomation.TrySetControllerPosition(this.m_ControllerHandednss, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002460 File Offset: 0x00000660
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000247D File Offset: 0x0000067D
		public bool activated
		{
			get
			{
				return HolographicAutomation.GetControllerActivated(this.m_ControllerHandednss);
			}
			set
			{
				HolographicAutomation.TrySetControllerActivated(this.m_ControllerHandednss, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002490 File Offset: 0x00000690
		public bool visible
		{
			get
			{
				return HolographicAutomation.GetControllerVisible(this.m_ControllerHandednss);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000024AD File Offset: 0x000006AD
		public void EnsureVisible()
		{
			HolographicAutomation.TryEnsureControllerVisible(this.m_ControllerHandednss);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000024BC File Offset: 0x000006BC
		public void PerformControllerPress(SimulatedControllerPress button)
		{
			HolographicAutomation.PerformButtonPress(this.m_ControllerHandednss, button);
		}

		// Token: 0x04000036 RID: 54
		public Handedness m_ControllerHandednss;
	}
}
