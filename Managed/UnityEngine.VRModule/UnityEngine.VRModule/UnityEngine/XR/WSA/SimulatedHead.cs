using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000015 RID: 21
	internal class SimulatedHead
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000255B File Offset: 0x0000075B
		public float diameter
		{
			get
			{
				return HolographicAutomation.GetHeadDiameter();
			}
			set
			{
				HolographicAutomation.SetHeadDiameter(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000257F File Offset: 0x0000077F
		public Vector3 eulerAngles
		{
			get
			{
				return HolographicAutomation.GetHeadRotation();
			}
			set
			{
				HolographicAutomation.SetHeadRotation(value);
			}
		}
	}
}
