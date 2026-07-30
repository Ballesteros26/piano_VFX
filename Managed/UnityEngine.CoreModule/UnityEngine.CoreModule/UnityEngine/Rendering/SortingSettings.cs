using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000380 RID: 896
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x00034D34 File Offset: 0x00032F34
		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00034D4C File Offset: 0x00032F4C
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x00034D64 File Offset: 0x00032F64
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				return this.m_WorldToCameraMatrix;
			}
			set
			{
				this.m_WorldToCameraMatrix = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00034D70 File Offset: 0x00032F70
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x00034D88 File Offset: 0x00032F88
		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
			set
			{
				this.m_CameraPosition = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00034D94 File Offset: 0x00032F94
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x00034DAC File Offset: 0x00032FAC
		public Vector3 customAxis
		{
			get
			{
				return this.m_CustomAxis;
			}
			set
			{
				this.m_CustomAxis = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00034DB8 File Offset: 0x00032FB8
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00034DD0 File Offset: 0x00032FD0
		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00034DDC File Offset: 0x00032FDC
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00034DF4 File Offset: 0x00032FF4
		public DistanceMetric distanceMetric
		{
			get
			{
				return this.m_DistanceMetric;
			}
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00034E00 File Offset: 0x00033000
		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric && this.m_PreviousVPMatrix.Equals(other.m_PreviousVPMatrix) && this.m_NonJitteredVPMatrix.Equals(other.m_NonJitteredVPMatrix);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00034E90 File Offset: 0x00033090
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00034EC8 File Offset: 0x000330C8
		public override int GetHashCode()
		{
			int num = this.m_WorldToCameraMatrix.GetHashCode();
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_CustomAxis.GetHashCode();
			num = (num * 397) ^ (int)this.m_Criteria;
			num = (num * 397) ^ (int)this.m_DistanceMetric;
			num = (num * 397) ^ this.m_PreviousVPMatrix.GetHashCode();
			return (num * 397) ^ this.m_NonJitteredVPMatrix.GetHashCode();
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00034F74 File Offset: 0x00033174
		public static bool operator ==(SortingSettings left, SortingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00034F90 File Offset: 0x00033190
		public static bool operator !=(SortingSettings left, SortingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B11 RID: 2833
		private Matrix4x4 m_WorldToCameraMatrix;

		// Token: 0x04000B12 RID: 2834
		private Vector3 m_CameraPosition;

		// Token: 0x04000B13 RID: 2835
		private Vector3 m_CustomAxis;

		// Token: 0x04000B14 RID: 2836
		private SortingCriteria m_Criteria;

		// Token: 0x04000B15 RID: 2837
		private DistanceMetric m_DistanceMetric;

		// Token: 0x04000B16 RID: 2838
		private Matrix4x4 m_PreviousVPMatrix;

		// Token: 0x04000B17 RID: 2839
		private Matrix4x4 m_NonJitteredVPMatrix;
	}
}
