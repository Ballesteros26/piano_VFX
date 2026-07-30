using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000387 RID: 903
	[UsedByNativeCode]
	public struct VisibleReflectionProbe : IEquatable<VisibleReflectionProbe>
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00035CE5 File Offset: 0x00033EE5
		public Texture texture
		{
			get
			{
				return (Texture)Object.FindObjectFromInstanceID(this.m_TextureId);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00035CF7 File Offset: 0x00033EF7
		public ReflectionProbe reflectionProbe
		{
			get
			{
				return (ReflectionProbe)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x00035D0C File Offset: 0x00033F0C
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x00035D24 File Offset: 0x00033F24
		public Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
			set
			{
				this.m_Bounds = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00035D30 File Offset: 0x00033F30
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x00035D48 File Offset: 0x00033F48
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
			set
			{
				this.m_LocalToWorldMatrix = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00035D54 File Offset: 0x00033F54
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00035D6C File Offset: 0x00033F6C
		public Vector4 hdrData
		{
			get
			{
				return this.m_HdrData;
			}
			set
			{
				this.m_HdrData = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x00035D78 File Offset: 0x00033F78
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x00035D90 File Offset: 0x00033F90
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00035D9C File Offset: 0x00033F9C
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00035DB4 File Offset: 0x00033FB4
		public float blendDistance
		{
			get
			{
				return this.m_BlendDistance;
			}
			set
			{
				this.m_BlendDistance = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00035DC0 File Offset: 0x00033FC0
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00035DD8 File Offset: 0x00033FD8
		public int importance
		{
			get
			{
				return this.m_Importance;
			}
			set
			{
				this.m_Importance = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00035DE4 File Offset: 0x00033FE4
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x00035E01 File Offset: 0x00034001
		public bool isBoxProjection
		{
			get
			{
				return Convert.ToBoolean(this.m_BoxProjection);
			}
			set
			{
				this.m_BoxProjection = Convert.ToInt32(value);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00035E10 File Offset: 0x00034010
		public bool Equals(VisibleReflectionProbe other)
		{
			return this.m_Bounds.Equals(other.m_Bounds) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_HdrData.Equals(other.m_HdrData) && this.m_Center.Equals(other.m_Center) && this.m_BlendDistance.Equals(other.m_BlendDistance) && this.m_Importance == other.m_Importance && this.m_BoxProjection == other.m_BoxProjection && this.m_InstanceId == other.m_InstanceId && this.m_TextureId == other.m_TextureId;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00035EC0 File Offset: 0x000340C0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleReflectionProbe && this.Equals((VisibleReflectionProbe)obj);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00035EF8 File Offset: 0x000340F8
		public override int GetHashCode()
		{
			int num = this.m_Bounds.GetHashCode();
			num = (num * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			num = (num * 397) ^ this.m_HdrData.GetHashCode();
			num = (num * 397) ^ this.m_Center.GetHashCode();
			num = (num * 397) ^ this.m_BlendDistance.GetHashCode();
			num = (num * 397) ^ this.m_Importance;
			num = (num * 397) ^ this.m_BoxProjection;
			num = (num * 397) ^ this.m_InstanceId;
			return (num * 397) ^ this.m_TextureId;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00035FBC File Offset: 0x000341BC
		public static bool operator ==(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00035FD8 File Offset: 0x000341D8
		public static bool operator !=(VisibleReflectionProbe left, VisibleReflectionProbe right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B4D RID: 2893
		private Bounds m_Bounds;

		// Token: 0x04000B4E RID: 2894
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000B4F RID: 2895
		private Vector4 m_HdrData;

		// Token: 0x04000B50 RID: 2896
		private Vector3 m_Center;

		// Token: 0x04000B51 RID: 2897
		private float m_BlendDistance;

		// Token: 0x04000B52 RID: 2898
		private int m_Importance;

		// Token: 0x04000B53 RID: 2899
		private int m_BoxProjection;

		// Token: 0x04000B54 RID: 2900
		private int m_InstanceId;

		// Token: 0x04000B55 RID: 2901
		private int m_TextureId;
	}
}
