using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000026 RID: 38
	internal static class GeometryUtils
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public unsafe static bool Overlap(OrientedBBox obb, Frustum frustum, int numPlanes, int numCorners)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < numPlanes)
			{
				Vector3 normal = frustum.planes[num].normal;
				float distance = frustum.planes[num].distance;
				float num2 = obb.extentX * Mathf.Abs(Vector3.Dot(normal, obb.right)) + obb.extentY * Mathf.Abs(Vector3.Dot(normal, obb.up)) + obb.extentZ * Mathf.Abs(Vector3.Dot(normal, obb.forward));
				float num3 = Vector3.Dot(normal, obb.center) + distance;
				flag = flag && num2 + num3 >= 0f;
				num++;
			}
			if (numCorners == 0)
			{
				return flag;
			}
			Plane* ptr;
			checked
			{
				ptr = stackalloc Plane[unchecked((UIntPtr)3) * (UIntPtr)sizeof(Plane)];
				ptr->normal = obb.right;
				ptr->distance = obb.extentX;
			}
			ptr[1].normal = obb.up;
			ptr[1].distance = obb.extentY;
			ptr[2].normal = obb.forward;
			ptr[2].distance = obb.extentZ;
			int num4 = 0;
			while (flag && num4 < 3)
			{
				Plane plane = ptr[num4];
				bool flag2 = true;
				bool flag3 = true;
				for (int i = 0; i < numCorners; i++)
				{
					float num5 = Vector3.Dot(plane.normal, frustum.corners[i] - obb.center);
					flag2 = flag2 && num5 > plane.distance;
					flag3 = flag3 && -num5 > plane.distance;
				}
				flag = flag && (!flag2 && !flag3);
				num4++;
			}
			return flag;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004DCC File Offset: 0x00002FCC
		public static Vector4 Plane(Vector3 position, Vector3 normal)
		{
			float num = -Vector3.Dot(normal, position);
			return new Vector4(normal.x, normal.y, normal.z, num);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004DFC File Offset: 0x00002FFC
		public static Vector4 CameraSpacePlane(Matrix4x4 worldToCamera, Vector3 positionWS, Vector3 normalWS, float sideSign = 1f, float clipPlaneOffset = 0f)
		{
			Vector3 vector = positionWS + normalWS * clipPlaneOffset;
			Vector3 vector2 = worldToCamera.MultiplyPoint(vector);
			Vector3 vector3 = worldToCamera.MultiplyVector(normalWS).normalized * sideSign;
			return new Vector4(vector3.x, vector3.y, vector3.z, -Vector3.Dot(vector2, vector3));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004E58 File Offset: 0x00003058
		public static Matrix4x4 CalculateWorldToCameraMatrixRHS(Vector3 position, Quaternion rotation)
		{
			return Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * Matrix4x4.TRS(position, rotation, Vector3.one).inverse;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004E98 File Offset: 0x00003098
		public static Matrix4x4 CalculateWorldToCameraMatrixRHS(Transform transform)
		{
			return Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * transform.localToWorldMatrix.inverse;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004ED4 File Offset: 0x000030D4
		public static Matrix4x4 CalculateObliqueMatrix(Matrix4x4 sourceProjection, Vector4 clipPlane)
		{
			Matrix4x4 matrix4x = sourceProjection;
			Matrix4x4 inverse = sourceProjection.inverse;
			Vector4 vector = new Vector4(Mathf.Sign(clipPlane.x), Mathf.Sign(clipPlane.y), 1f, 1f);
			Vector4 vector2 = inverse * vector;
			Vector4 vector3 = new Vector4(matrix4x[3], matrix4x[7], matrix4x[11], matrix4x[15]);
			Vector4 vector4 = clipPlane * (2f * Vector4.Dot(vector3, vector2) / Vector4.Dot(clipPlane, vector2));
			matrix4x[2] = vector4.x - vector3.x;
			matrix4x[6] = vector4.y - vector3.y;
			matrix4x[10] = vector4.z - vector3.z;
			matrix4x[14] = vector4.w - vector3.w;
			return matrix4x;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004FB9 File Offset: 0x000031B9
		public static Matrix4x4 CalculateReflectionMatrix(Vector3 position, Vector3 normal)
		{
			return GeometryUtils.CalculateReflectionMatrix(GeometryUtils.Plane(position, normal.normalized));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004FD0 File Offset: 0x000031D0
		public static Matrix4x4 CalculateReflectionMatrix(Vector4 plane)
		{
			return new Matrix4x4
			{
				m00 = 1f - 2f * plane[0] * plane[0],
				m01 = -2f * plane[0] * plane[1],
				m02 = -2f * plane[0] * plane[2],
				m03 = -2f * plane[3] * plane[0],
				m10 = -2f * plane[1] * plane[0],
				m11 = 1f - 2f * plane[1] * plane[1],
				m12 = -2f * plane[1] * plane[2],
				m13 = -2f * plane[3] * plane[1],
				m20 = -2f * plane[2] * plane[0],
				m21 = -2f * plane[2] * plane[1],
				m22 = 1f - 2f * plane[2] * plane[2],
				m23 = -2f * plane[3] * plane[2],
				m30 = 0f,
				m31 = 0f,
				m32 = 0f,
				m33 = 1f
			};
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005190 File Offset: 0x00003390
		public static bool IsProjectionMatrixOblique(Matrix4x4 projectionMatrix)
		{
			return projectionMatrix[2] != 0f || projectionMatrix[6] != 0f;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000051B8 File Offset: 0x000033B8
		public static Matrix4x4 CalculateProjectionMatrix(Camera camera)
		{
			if (camera.orthographic)
			{
				float orthographicSize = camera.orthographicSize;
				float num = camera.orthographicSize * camera.aspect;
				return Matrix4x4.Ortho(-num, num, -orthographicSize, orthographicSize, camera.nearClipPlane, camera.farClipPlane);
			}
			return Matrix4x4.Perspective(camera.GetGateFittedFieldOfView(), camera.aspect, camera.nearClipPlane, camera.farClipPlane);
		}
	}
}
