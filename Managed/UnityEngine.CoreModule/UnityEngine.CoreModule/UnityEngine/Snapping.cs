using System;

namespace UnityEngine
{
	// Token: 0x020001DC RID: 476
	public static class Snapping
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x0002292C File Offset: 0x00020B2C
		internal static bool IsCardinalDirection(Vector3 direction)
		{
			return (Mathf.Abs(direction.x) > 0f && Mathf.Approximately(direction.y, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.y) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.z) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.y, 0f));
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000229E4 File Offset: 0x00020BE4
		public static float Snap(float val, float snap)
		{
			bool flag = snap == 0f;
			float num;
			if (flag)
			{
				num = val;
			}
			else
			{
				num = snap * Mathf.Round(val / snap);
			}
			return num;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00022A10 File Offset: 0x00020C10
		public static Vector2 Snap(Vector2 val, Vector2 snap)
		{
			return new Vector3((Mathf.Abs(snap.x) < Mathf.Epsilon) ? val.x : (snap.x * Mathf.Round(val.x / snap.x)), (Mathf.Abs(snap.y) < Mathf.Epsilon) ? val.y : (snap.y * Mathf.Round(val.y / snap.y)));
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00022A94 File Offset: 0x00020C94
		public static Vector3 Snap(Vector3 val, Vector3 snap, SnapAxis axis = SnapAxis.All)
		{
			return new Vector3(((axis & SnapAxis.X) == SnapAxis.X) ? Snapping.Snap(val.x, snap.x) : val.x, ((axis & SnapAxis.Y) == SnapAxis.Y) ? Snapping.Snap(val.y, snap.y) : val.y, ((axis & SnapAxis.Z) == SnapAxis.Z) ? Snapping.Snap(val.z, snap.z) : val.z);
		}
	}
}
