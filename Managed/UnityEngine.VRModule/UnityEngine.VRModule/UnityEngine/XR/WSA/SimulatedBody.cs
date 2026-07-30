using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000014 RID: 20
	internal class SimulatedBody
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000024EF File Offset: 0x000006EF
		public Vector3 position
		{
			get
			{
				return HolographicAutomation.GetBodyPosition();
			}
			set
			{
				HolographicAutomation.SetBodyPosition(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000024FC File Offset: 0x000006FC
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002513 File Offset: 0x00000713
		public float rotation
		{
			get
			{
				return HolographicAutomation.GetBodyRotation();
			}
			set
			{
				HolographicAutomation.SetBodyRotation(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002520 File Offset: 0x00000720
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002537 File Offset: 0x00000737
		public float height
		{
			get
			{
				return HolographicAutomation.GetBodyHeight();
			}
			set
			{
				HolographicAutomation.SetBodyHeight(value);
			}
		}
	}
}
