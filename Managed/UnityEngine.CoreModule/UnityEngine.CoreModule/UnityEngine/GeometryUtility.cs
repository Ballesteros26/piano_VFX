using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000C6 RID: 198
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GeometryUtilityScripting", StaticAccessorType.DoubleColon)]
	public sealed class GeometryUtility
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(camera, array);
			return array;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(worldToProjectionMatrix, array);
			return array;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00007D06 File Offset: 0x00005F06
		public static void CalculateFrustumPlanes(Camera camera, Plane[] planes)
		{
			GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix, planes);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00007D24 File Offset: 0x00005F24
		public static void CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix, Plane[] planes)
		{
			bool flag = planes == null;
			if (flag)
			{
				throw new ArgumentNullException("planes");
			}
			bool flag2 = planes.Length != 6;
			if (flag2)
			{
				throw new ArgumentException("Planes array must be of length 6.", "planes");
			}
			GeometryUtility.Internal_ExtractPlanes(planes, worldToProjectionMatrix);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00007D6C File Offset: 0x00005F6C
		public static Bounds CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = positions.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.", "positions");
			}
			return GeometryUtility.Internal_CalculateBounds(positions, transform);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public static bool TryCreatePlaneFromPolygon(Vector3[] vertices, out Plane plane)
		{
			bool flag = vertices == null || vertices.Length < 3;
			bool flag2;
			if (flag)
			{
				plane = new Plane(Vector3.up, 0f);
				flag2 = false;
			}
			else
			{
				bool flag3 = vertices.Length == 3;
				if (flag3)
				{
					Vector3 vector = vertices[0];
					Vector3 vector2 = vertices[1];
					Vector3 vector3 = vertices[2];
					plane = new Plane(vector, vector2, vector3);
					flag2 = plane.normal.sqrMagnitude > 0f;
				}
				else
				{
					Vector3 zero = Vector3.zero;
					int num = vertices.Length - 1;
					Vector3 vector4 = vertices[num];
					foreach (Vector3 vector5 in vertices)
					{
						zero.x += (vector4.y - vector5.y) * (vector4.z + vector5.z);
						zero.y += (vector4.z - vector5.z) * (vector4.x + vector5.x);
						zero.z += (vector4.x - vector5.x) * (vector4.y + vector5.y);
						vector4 = vector5;
					}
					zero.Normalize();
					float num2 = 0f;
					foreach (Vector3 vector6 in vertices)
					{
						num2 -= Vector3.Dot(zero, vector6);
					}
					num2 /= (float)vertices.Length;
					plane = new Plane(zero, num2);
					flag2 = plane.normal.sqrMagnitude > 0f;
				}
			}
			return flag2;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00007F77 File Offset: 0x00006177
		public static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			return GeometryUtility.TestPlanesAABB_Injected(planes, ref bounds);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00007F81 File Offset: 0x00006181
		[NativeName("ExtractPlanes")]
		private static void Internal_ExtractPlanes([Out] Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			GeometryUtility.Internal_ExtractPlanes_Injected(planes, ref worldToProjectionMatrix);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00007F8C File Offset: 0x0000618C
		[NativeName("CalculateBounds")]
		private static Bounds Internal_CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			Bounds bounds;
			GeometryUtility.Internal_CalculateBounds_Injected(positions, ref transform, out bounds);
			return bounds;
		}

		// Token: 0x06000507 RID: 1287
		[MethodImpl(4096)]
		private static extern bool TestPlanesAABB_Injected(Plane[] planes, ref Bounds bounds);

		// Token: 0x06000508 RID: 1288
		[MethodImpl(4096)]
		private static extern void Internal_ExtractPlanes_Injected([Out] Plane[] planes, ref Matrix4x4 worldToProjectionMatrix);

		// Token: 0x06000509 RID: 1289
		[MethodImpl(4096)]
		private static extern void Internal_CalculateBounds_Injected(Vector3[] positions, ref Matrix4x4 transform, out Bounds ret);
	}
}
