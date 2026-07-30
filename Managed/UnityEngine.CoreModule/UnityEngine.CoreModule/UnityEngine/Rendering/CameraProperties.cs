using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035B RID: 859
	[UsedByNativeCode]
	public struct CameraProperties : IEquatable<CameraProperties>
	{
		// Token: 0x06001D6D RID: 7533 RVA: 0x000311A8 File Offset: 0x0002F3A8
		public unsafe Plane GetShadowCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00031214 File Offset: 0x0002F414
		public unsafe void SetShadowCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00031280 File Offset: 0x0002F480
		public unsafe Plane GetCameraCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000312EC File Offset: 0x0002F4EC
		public unsafe void SetCameraCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00031358 File Offset: 0x0002F558
		public unsafe bool Equals(CameraProperties other)
		{
			for (int i = 0; i < 6; i++)
			{
				bool flag = !this.GetShadowCullingPlane(i).Equals(other.GetShadowCullingPlane(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				bool flag2 = !this.GetCameraCullingPlane(j).Equals(other.GetCameraCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					bool flag3 = ptr2[k] != *((ref other.layerCullDistances.FixedElementField) + (IntPtr)k * 4);
					if (flag3)
					{
						return false;
					}
				}
			}
			return this.screenRect.Equals(other.screenRect) && this.viewDir.Equals(other.viewDir) && this.projectionNear.Equals(other.projectionNear) && this.projectionFar.Equals(other.projectionFar) && this.cameraNear.Equals(other.cameraNear) && this.cameraFar.Equals(other.cameraFar) && this.cameraAspect.Equals(other.cameraAspect) && this.cameraToWorld.Equals(other.cameraToWorld) && this.actualWorldToClip.Equals(other.actualWorldToClip) && this.cameraClipToWorld.Equals(other.cameraClipToWorld) && this.cameraWorldToClip.Equals(other.cameraWorldToClip) && this.implicitProjection.Equals(other.implicitProjection) && this.stereoWorldToClipLeft.Equals(other.stereoWorldToClipLeft) && this.stereoWorldToClipRight.Equals(other.stereoWorldToClipRight) && this.worldToCamera.Equals(other.worldToCamera) && this.up.Equals(other.up) && this.right.Equals(other.right) && this.transformDirection.Equals(other.transformDirection) && this.cameraEuler.Equals(other.cameraEuler) && this.velocity.Equals(other.velocity) && this.farPlaneWorldSpaceLength.Equals(other.farPlaneWorldSpaceLength) && this.rendererCount == other.rendererCount && this.baseFarDistance.Equals(other.baseFarDistance) && this.shadowCullCenter.Equals(other.shadowCullCenter) && this.layerCullSpherical == other.layerCullSpherical && this.coreCameraValues.Equals(other.coreCameraValues) && this.cameraType == other.cameraType && this.projectionIsOblique == other.projectionIsOblique && this.isImplicitProjectionMatrix == other.isImplicitProjectionMatrix;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000316AC File Offset: 0x0002F8AC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CameraProperties && this.Equals((CameraProperties)obj);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public unsafe override int GetHashCode()
		{
			int num = this.screenRect.GetHashCode();
			num = (num * 397) ^ this.viewDir.GetHashCode();
			num = (num * 397) ^ this.projectionNear.GetHashCode();
			num = (num * 397) ^ this.projectionFar.GetHashCode();
			num = (num * 397) ^ this.cameraNear.GetHashCode();
			num = (num * 397) ^ this.cameraFar.GetHashCode();
			num = (num * 397) ^ this.cameraAspect.GetHashCode();
			num = (num * 397) ^ this.cameraToWorld.GetHashCode();
			num = (num * 397) ^ this.actualWorldToClip.GetHashCode();
			num = (num * 397) ^ this.cameraClipToWorld.GetHashCode();
			num = (num * 397) ^ this.cameraWorldToClip.GetHashCode();
			num = (num * 397) ^ this.implicitProjection.GetHashCode();
			num = (num * 397) ^ this.stereoWorldToClipLeft.GetHashCode();
			num = (num * 397) ^ this.stereoWorldToClipRight.GetHashCode();
			num = (num * 397) ^ this.worldToCamera.GetHashCode();
			num = (num * 397) ^ this.up.GetHashCode();
			num = (num * 397) ^ this.right.GetHashCode();
			num = (num * 397) ^ this.transformDirection.GetHashCode();
			num = (num * 397) ^ this.cameraEuler.GetHashCode();
			num = (num * 397) ^ this.velocity.GetHashCode();
			num = (num * 397) ^ this.farPlaneWorldSpaceLength.GetHashCode();
			num = (num * 397) ^ (int)this.rendererCount;
			for (int i = 0; i < 6; i++)
			{
				num = (num * 397) ^ this.GetShadowCullingPlane(i).GetHashCode();
			}
			for (int j = 0; j < 6; j++)
			{
				num = (num * 397) ^ this.GetCameraCullingPlane(j).GetHashCode();
			}
			num = (num * 397) ^ this.baseFarDistance.GetHashCode();
			num = (num * 397) ^ this.shadowCullCenter.GetHashCode();
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					num = (num * 397) ^ ptr2[k].GetHashCode();
				}
			}
			num = (num * 397) ^ this.layerCullSpherical;
			num = (num * 397) ^ this.coreCameraValues.GetHashCode();
			num = (num * 397) ^ (int)this.cameraType;
			num = (num * 397) ^ this.projectionIsOblique;
			return (num * 397) ^ this.isImplicitProjectionMatrix;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00031A34 File Offset: 0x0002FC34
		public static bool operator ==(CameraProperties left, CameraProperties right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00031A50 File Offset: 0x0002FC50
		public static bool operator !=(CameraProperties left, CameraProperties right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A42 RID: 2626
		private const int k_NumLayers = 32;

		// Token: 0x04000A43 RID: 2627
		private Rect screenRect;

		// Token: 0x04000A44 RID: 2628
		private Vector3 viewDir;

		// Token: 0x04000A45 RID: 2629
		private float projectionNear;

		// Token: 0x04000A46 RID: 2630
		private float projectionFar;

		// Token: 0x04000A47 RID: 2631
		private float cameraNear;

		// Token: 0x04000A48 RID: 2632
		private float cameraFar;

		// Token: 0x04000A49 RID: 2633
		private float cameraAspect;

		// Token: 0x04000A4A RID: 2634
		private Matrix4x4 cameraToWorld;

		// Token: 0x04000A4B RID: 2635
		private Matrix4x4 actualWorldToClip;

		// Token: 0x04000A4C RID: 2636
		private Matrix4x4 cameraClipToWorld;

		// Token: 0x04000A4D RID: 2637
		private Matrix4x4 cameraWorldToClip;

		// Token: 0x04000A4E RID: 2638
		private Matrix4x4 implicitProjection;

		// Token: 0x04000A4F RID: 2639
		private Matrix4x4 stereoWorldToClipLeft;

		// Token: 0x04000A50 RID: 2640
		private Matrix4x4 stereoWorldToClipRight;

		// Token: 0x04000A51 RID: 2641
		private Matrix4x4 worldToCamera;

		// Token: 0x04000A52 RID: 2642
		private Vector3 up;

		// Token: 0x04000A53 RID: 2643
		private Vector3 right;

		// Token: 0x04000A54 RID: 2644
		private Vector3 transformDirection;

		// Token: 0x04000A55 RID: 2645
		private Vector3 cameraEuler;

		// Token: 0x04000A56 RID: 2646
		private Vector3 velocity;

		// Token: 0x04000A57 RID: 2647
		private float farPlaneWorldSpaceLength;

		// Token: 0x04000A58 RID: 2648
		private uint rendererCount;

		// Token: 0x04000A59 RID: 2649
		private const int k_PlaneCount = 6;

		// Token: 0x04000A5A RID: 2650
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_ShadowCullPlanes>e__FixedBuffer m_ShadowCullPlanes;

		// Token: 0x04000A5B RID: 2651
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_CameraCullPlanes>e__FixedBuffer m_CameraCullPlanes;

		// Token: 0x04000A5C RID: 2652
		private float baseFarDistance;

		// Token: 0x04000A5D RID: 2653
		private Vector3 shadowCullCenter;

		// Token: 0x04000A5E RID: 2654
		[FixedBuffer(typeof(float), 32)]
		internal CameraProperties.<layerCullDistances>e__FixedBuffer layerCullDistances;

		// Token: 0x04000A5F RID: 2655
		private int layerCullSpherical;

		// Token: 0x04000A60 RID: 2656
		private CoreCameraValues coreCameraValues;

		// Token: 0x04000A61 RID: 2657
		private uint cameraType;

		// Token: 0x04000A62 RID: 2658
		private int projectionIsOblique;

		// Token: 0x04000A63 RID: 2659
		private int isImplicitProjectionMatrix;

		// Token: 0x0200035C RID: 860
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 96)]
		public struct <m_ShadowCullPlanes>e__FixedBuffer
		{
			// Token: 0x04000A64 RID: 2660
			public byte FixedElementField;
		}

		// Token: 0x0200035D RID: 861
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 96)]
		public struct <m_CameraCullPlanes>e__FixedBuffer
		{
			// Token: 0x04000A65 RID: 2661
			public byte FixedElementField;
		}

		// Token: 0x0200035E RID: 862
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 128)]
		public struct <layerCullDistances>e__FixedBuffer
		{
			// Token: 0x04000A66 RID: 2662
			public float FixedElementField;
		}
	}
}
