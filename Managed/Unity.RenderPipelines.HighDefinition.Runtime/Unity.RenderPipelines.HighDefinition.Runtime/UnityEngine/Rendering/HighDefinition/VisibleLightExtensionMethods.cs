using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000067 RID: 103
	internal static class VisibleLightExtensionMethods
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000F850 File Offset: 0x0000DA50
		public static Vector3 GetPosition(this VisibleLight value)
		{
			return value.localToWorldMatrix.GetColumn(3);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F874 File Offset: 0x0000DA74
		public static Vector3 GetForward(this VisibleLight value)
		{
			return value.localToWorldMatrix.GetColumn(2);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000F898 File Offset: 0x0000DA98
		public static Vector3 GetUp(this VisibleLight value)
		{
			return value.localToWorldMatrix.GetColumn(1);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000F8BC File Offset: 0x0000DABC
		public static Vector3 GetRight(this VisibleLight value)
		{
			return value.localToWorldMatrix.GetColumn(0);
		}
	}
}
