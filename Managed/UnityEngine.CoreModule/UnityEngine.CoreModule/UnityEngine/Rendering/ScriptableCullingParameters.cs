using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000360 RID: 864
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00031A70 File Offset: 0x0002FC70
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00031A88 File Offset: 0x0002FC88
		public int maximumVisibleLights
		{
			get
			{
				return this.m_maximumVisibleLights;
			}
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00031A94 File Offset: 0x0002FC94
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x00031AAC File Offset: 0x0002FCAC
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "value", value, 10));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00031AF4 File Offset: 0x0002FCF4
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x00031B11 File Offset: 0x0002FD11
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00031B20 File Offset: 0x0002FD20
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00031B38 File Offset: 0x0002FD38
		public LODParameters lodParameters
		{
			get
			{
				return this.m_LODParameters;
			}
			set
			{
				this.m_LODParameters = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00031B44 File Offset: 0x0002FD44
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00031B5C File Offset: 0x0002FD5C
		public uint cullingMask
		{
			get
			{
				return this.m_CullingMask;
			}
			set
			{
				this.m_CullingMask = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00031B68 File Offset: 0x0002FD68
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x00031B80 File Offset: 0x0002FD80
		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00031B8C File Offset: 0x0002FD8C
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x00031BA4 File Offset: 0x0002FDA4
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x00031BD4 File Offset: 0x0002FDD4
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x00031BEC File Offset: 0x0002FDEC
		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00031BF8 File Offset: 0x0002FDF8
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x00031C10 File Offset: 0x0002FE10
		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			get
			{
				return this.m_ReflectionProbeSortingCriteria;
			}
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00031C1C File Offset: 0x0002FE1C
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x00031C34 File Offset: 0x0002FE34
		public CameraProperties cameraProperties
		{
			get
			{
				return this.m_CameraProperties;
			}
			set
			{
				this.m_CameraProperties = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00031C40 File Offset: 0x0002FE40
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x00031C58 File Offset: 0x0002FE58
		public Matrix4x4 stereoViewMatrix
		{
			get
			{
				return this.m_StereoViewMatrix;
			}
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00031C64 File Offset: 0x0002FE64
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x00031C7C File Offset: 0x0002FE7C
		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00031C88 File Offset: 0x0002FE88
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		public float stereoSeparationDistance
		{
			get
			{
				return this.m_StereoSeparationDistance;
			}
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x00031CAC File Offset: 0x0002FEAC
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		public float accurateOcclusionThreshold
		{
			get
			{
				return this.m_AccurateOcclusionThreshold;
			}
			set
			{
				this.m_AccurateOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00031CD8 File Offset: 0x0002FED8
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x00031CF0 File Offset: 0x0002FEF0
		public int maximumPortalCullingJobs
		{
			get
			{
				return this.m_MaximumPortalCullingJobs;
			}
			set
			{
				bool flag = value < 1 || value > 16;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be in range {2} to {3}", new object[] { "maximumPortalCullingJobs", this.maximumPortalCullingJobs, 1, 16 }));
				}
				this.m_MaximumPortalCullingJobs = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00031D54 File Offset: 0x0002FF54
		public static int cullingJobsLowerLimit
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00031D68 File Offset: 0x0002FF68
		public static int cullingJobsUpperLimit
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00031D7C File Offset: 0x0002FF7C
		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				return ptr2[layerIndex];
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00031DDC File Offset: 0x0002FFDC
		public unsafe void SetLayerCullingDistance(int layerIndex, float distance)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				ptr2[layerIndex] = distance;
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00031E3C File Offset: 0x0003003C
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00031EB0 File Offset: 0x000300B0
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00031F24 File Offset: 0x00030124
		public bool Equals(ScriptableCullingParameters other)
		{
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000320FC File Offset: 0x000302FC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00032134 File Offset: 0x00030334
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397) ^ this.m_LODParameters.GetHashCode();
			num = (num * 397) ^ this.m_CullingPlaneCount;
			num = (num * 397) ^ (int)this.m_CullingMask;
			num = (num * 397) ^ this.m_SceneMask.GetHashCode();
			num = (num * 397) ^ this.m_LayerCull;
			num = (num * 397) ^ this.m_CullingMatrix.GetHashCode();
			num = (num * 397) ^ this.m_Origin.GetHashCode();
			num = (num * 397) ^ this.m_ShadowDistance.GetHashCode();
			num = (num * 397) ^ (int)this.m_CullingOptions;
			num = (num * 397) ^ (int)this.m_ReflectionProbeSortingCriteria;
			num = (num * 397) ^ this.m_CameraProperties.GetHashCode();
			num = (num * 397) ^ this.m_AccurateOcclusionThreshold.GetHashCode();
			num = (num * 397) ^ this.m_MaximumPortalCullingJobs.GetHashCode();
			num = (num * 397) ^ this.m_StereoViewMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoProjectionMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoSeparationDistance.GetHashCode();
			return (num * 397) ^ this.m_maximumVisibleLights;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000322AC File Offset: 0x000304AC
		public static bool operator ==(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000322C8 File Offset: 0x000304C8
		public static bool operator !=(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A70 RID: 2672
		private int m_IsOrthographic;

		// Token: 0x04000A71 RID: 2673
		private LODParameters m_LODParameters;

		// Token: 0x04000A72 RID: 2674
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000A73 RID: 2675
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000A74 RID: 2676
		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000A75 RID: 2677
		private int m_CullingPlaneCount;

		// Token: 0x04000A76 RID: 2678
		private uint m_CullingMask;

		// Token: 0x04000A77 RID: 2679
		private ulong m_SceneMask;

		// Token: 0x04000A78 RID: 2680
		private const int k_LayerCount = 32;

		// Token: 0x04000A79 RID: 2681
		public static readonly int layerCount = 32;

		// Token: 0x04000A7A RID: 2682
		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		// Token: 0x04000A7B RID: 2683
		private int m_LayerCull;

		// Token: 0x04000A7C RID: 2684
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x04000A7D RID: 2685
		private Vector3 m_Origin;

		// Token: 0x04000A7E RID: 2686
		private float m_ShadowDistance;

		// Token: 0x04000A7F RID: 2687
		private CullingOptions m_CullingOptions;

		// Token: 0x04000A80 RID: 2688
		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		// Token: 0x04000A81 RID: 2689
		private CameraProperties m_CameraProperties;

		// Token: 0x04000A82 RID: 2690
		private float m_AccurateOcclusionThreshold;

		// Token: 0x04000A83 RID: 2691
		private int m_MaximumPortalCullingJobs;

		// Token: 0x04000A84 RID: 2692
		private const int k_CullingJobCountLowerLimit = 1;

		// Token: 0x04000A85 RID: 2693
		private const int k_CullingJobCountUpperLimit = 16;

		// Token: 0x04000A86 RID: 2694
		private Matrix4x4 m_StereoViewMatrix;

		// Token: 0x04000A87 RID: 2695
		private Matrix4x4 m_StereoProjectionMatrix;

		// Token: 0x04000A88 RID: 2696
		private float m_StereoSeparationDistance;

		// Token: 0x04000A89 RID: 2697
		private int m_maximumVisibleLights;

		// Token: 0x02000361 RID: 865
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000A8A RID: 2698
			public byte FixedElementField;
		}

		// Token: 0x02000362 RID: 866
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			// Token: 0x04000A8B RID: 2699
			public float FixedElementField;
		}
	}
}
