using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000017 RID: 23
	internal static class HolographicEmulationHelpers
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00002638 File Offset: 0x00000838
		public static Vector3 CalcExpectedCameraPosition(SimulatedHead head, SimulatedBody body)
		{
			Vector3 vector = body.position;
			vector.y += body.height - 1.776f;
			vector.y -= head.diameter / 2f;
			vector.y += 0.11599995f;
			Vector3 eulerAngles = head.eulerAngles;
			eulerAngles.y += body.rotation;
			Quaternion quaternion = Quaternion.Euler(eulerAngles);
			vector += quaternion * (0.0985f * Vector3.forward);
			return vector;
		}

		// Token: 0x04000038 RID: 56
		public const float k_DefaultBodyHeight = 1.776f;

		// Token: 0x04000039 RID: 57
		public const float k_DefaultHeadDiameter = 0.2319999f;

		// Token: 0x0400003A RID: 58
		public const float k_ForwardOffset = 0.0985f;
	}
}
