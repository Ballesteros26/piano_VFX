using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037B RID: 891
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x000348C8 File Offset: 0x00032AC8
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x000348E0 File Offset: 0x00032AE0
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
					throw new ArgumentException(string.Format("Value should range from {0} to ShadowSplitData.maximumCullingPlaneCount ({1}), but was {2}.", 0, 10, value));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00034928 File Offset: 0x00032B28
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x00034940 File Offset: 0x00032B40
		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
			set
			{
				this.m_CullingSphere = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0003494C File Offset: 0x00032B4C
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x00034964 File Offset: 0x00032B64
		public float shadowCascadeBlendCullingFactor
		{
			get
			{
				return this.m_ShadowCascadeBlendCullingFactor;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000349B4 File Offset: 0x00032BB4
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00034A30 File Offset: 0x00032C30
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00034AA8 File Offset: 0x00032CA8
		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag3 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag3)
					{
						return false;
					}
				}
				flag2 = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return flag2;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00034B30 File Offset: 0x00032D30
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00034B68 File Offset: 0x00032D68
		public override int GetHashCode()
		{
			return (this.m_CullingPlaneCount * 397) ^ this.m_CullingSphere.GetHashCode();
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00034B9C File Offset: 0x00032D9C
		public static bool operator ==(ShadowSplitData left, ShadowSplitData right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00034BB8 File Offset: 0x00032DB8
		public static bool operator !=(ShadowSplitData left, ShadowSplitData right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AF9 RID: 2809
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000AFA RID: 2810
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000AFB RID: 2811
		private int m_CullingPlaneCount;

		// Token: 0x04000AFC RID: 2812
		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000AFD RID: 2813
		private Vector4 m_CullingSphere;

		// Token: 0x04000AFE RID: 2814
		private float m_ShadowCascadeBlendCullingFactor;

		// Token: 0x0200037C RID: 892
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(0, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000AFF RID: 2815
			public byte FixedElementField;
		}
	}
}
