using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000024 RID: 36
	public struct Frustum
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000487C File Offset: 0x00002A7C
		private static Vector3 IntersectFrustumPlanes(Plane p0, Plane p1, Plane p2)
		{
			Vector3 normal = p0.normal;
			Vector3 normal2 = p1.normal;
			Vector3 normal3 = p2.normal;
			float num = Vector3.Dot(Vector3.Cross(normal, normal2), normal3);
			return (Vector3.Cross(normal3, normal2) * p0.distance + Vector3.Cross(normal, normal3) * p1.distance - Vector3.Cross(normal, normal2) * p2.distance) * (1f / num);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004900 File Offset: 0x00002B00
		public static void Create(ref Frustum frustum, Matrix4x4 viewProjMatrix, Vector3 viewPos, Vector3 viewDir, float nearClipPlane, float farClipPlane)
		{
			GeometryUtility.CalculateFrustumPlanes(viewProjMatrix, frustum.planes);
			Plane plane = default(Plane);
			plane.SetNormalAndPosition(viewDir, viewPos);
			plane.distance -= nearClipPlane;
			Plane plane2 = default(Plane);
			plane2.SetNormalAndPosition(-viewDir, viewPos);
			plane2.distance += farClipPlane;
			frustum.planes[4] = plane;
			frustum.planes[5] = plane2;
			frustum.corners[0] = Frustum.IntersectFrustumPlanes(frustum.planes[0], frustum.planes[3], frustum.planes[4]);
			frustum.corners[1] = Frustum.IntersectFrustumPlanes(frustum.planes[1], frustum.planes[3], frustum.planes[4]);
			frustum.corners[2] = Frustum.IntersectFrustumPlanes(frustum.planes[0], frustum.planes[2], frustum.planes[4]);
			frustum.corners[3] = Frustum.IntersectFrustumPlanes(frustum.planes[1], frustum.planes[2], frustum.planes[4]);
			frustum.corners[4] = Frustum.IntersectFrustumPlanes(frustum.planes[0], frustum.planes[3], frustum.planes[5]);
			frustum.corners[5] = Frustum.IntersectFrustumPlanes(frustum.planes[1], frustum.planes[3], frustum.planes[5]);
			frustum.corners[6] = Frustum.IntersectFrustumPlanes(frustum.planes[0], frustum.planes[2], frustum.planes[5]);
			frustum.corners[7] = Frustum.IntersectFrustumPlanes(frustum.planes[1], frustum.planes[2], frustum.planes[5]);
		}

		// Token: 0x04000098 RID: 152
		public Plane[] planes;

		// Token: 0x04000099 RID: 153
		public Vector3[] corners;
	}
}
