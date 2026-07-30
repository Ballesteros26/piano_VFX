using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036B RID: 875
	public struct LODParameters : IEquatable<LODParameters>
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00032E74 File Offset: 0x00031074
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x00032E91 File Offset: 0x00031091
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x00032EA0 File Offset: 0x000310A0
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x00032EB8 File Offset: 0x000310B8
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x00032EC4 File Offset: 0x000310C4
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x00032EDC File Offset: 0x000310DC
		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
			set
			{
				this.m_FieldOfView = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00032EE8 File Offset: 0x000310E8
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x00032F00 File Offset: 0x00031100
		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
			set
			{
				this.m_OrthoSize = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00032F0C File Offset: 0x0003110C
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x00032F24 File Offset: 0x00031124
		public int cameraPixelHeight
		{
			get
			{
				return this.m_CameraPixelHeight;
			}
			set
			{
				this.m_CameraPixelHeight = value;
			}
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00032F30 File Offset: 0x00031130
		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00032F9C File Offset: 0x0003119C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00032FD4 File Offset: 0x000311D4
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397) ^ this.m_CameraPosition.GetHashCode();
			num = (num * 397) ^ this.m_FieldOfView.GetHashCode();
			num = (num * 397) ^ this.m_OrthoSize.GetHashCode();
			return (num * 397) ^ this.m_CameraPixelHeight;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00033040 File Offset: 0x00031240
		public static bool operator ==(LODParameters left, LODParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0003305C File Offset: 0x0003125C
		public static bool operator !=(LODParameters left, LODParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AAD RID: 2733
		private int m_IsOrthographic;

		// Token: 0x04000AAE RID: 2734
		private Vector3 m_CameraPosition;

		// Token: 0x04000AAF RID: 2735
		private float m_FieldOfView;

		// Token: 0x04000AB0 RID: 2736
		private float m_OrthoSize;

		// Token: 0x04000AB1 RID: 2737
		private int m_CameraPixelHeight;
	}
}
