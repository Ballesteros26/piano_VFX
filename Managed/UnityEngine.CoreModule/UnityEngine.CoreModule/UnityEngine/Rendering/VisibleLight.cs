using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000385 RID: 901
	[UsedByNativeCode]
	public struct VisibleLight : IEquatable<VisibleLight>
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00035990 File Offset: 0x00033B90
		public Light light
		{
			get
			{
				return (Light)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000359A4 File Offset: 0x00033BA4
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x000359BC File Offset: 0x00033BBC
		public LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				this.m_LightType = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000359C8 File Offset: 0x00033BC8
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x000359E0 File Offset: 0x00033BE0
		public Color finalColor
		{
			get
			{
				return this.m_FinalColor;
			}
			set
			{
				this.m_FinalColor = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000359EC File Offset: 0x00033BEC
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x00035A04 File Offset: 0x00033C04
		public Rect screenRect
		{
			get
			{
				return this.m_ScreenRect;
			}
			set
			{
				this.m_ScreenRect = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x00035A10 File Offset: 0x00033C10
		// (set) Token: 0x06001F7C RID: 8060 RVA: 0x00035A28 File Offset: 0x00033C28
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

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x00035A34 File Offset: 0x00033C34
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x00035A4C File Offset: 0x00033C4C
		public float range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				this.m_Range = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00035A58 File Offset: 0x00033C58
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x00035A70 File Offset: 0x00033C70
		public float spotAngle
		{
			get
			{
				return this.m_SpotAngle;
			}
			set
			{
				this.m_SpotAngle = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x00035A7C File Offset: 0x00033C7C
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00035A9C File Offset: 0x00033C9C
		public bool intersectsNearPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsNearPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsNearPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsNearPlane;
				}
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00035AD0 File Offset: 0x00033CD0
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x00035AF0 File Offset: 0x00033CF0
		public bool intersectsFarPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsFarPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsFarPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsFarPlane;
				}
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00035B24 File Offset: 0x00033D24
		public bool Equals(VisibleLight other)
		{
			return this.m_LightType == other.m_LightType && this.m_FinalColor.Equals(other.m_FinalColor) && this.m_ScreenRect.Equals(other.m_ScreenRect) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_Range.Equals(other.m_Range) && this.m_SpotAngle.Equals(other.m_SpotAngle) && this.m_InstanceId == other.m_InstanceId && this.m_Flags == other.m_Flags;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00035BC4 File Offset: 0x00033DC4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleLight && this.Equals((VisibleLight)obj);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00035BFC File Offset: 0x00033DFC
		public override int GetHashCode()
		{
			int num = (int)this.m_LightType;
			num = (num * 397) ^ this.m_FinalColor.GetHashCode();
			num = (num * 397) ^ this.m_ScreenRect.GetHashCode();
			num = (num * 397) ^ this.m_LocalToWorldMatrix.GetHashCode();
			num = (num * 397) ^ this.m_Range.GetHashCode();
			num = (num * 397) ^ this.m_SpotAngle.GetHashCode();
			num = (num * 397) ^ this.m_InstanceId;
			return (num * 397) ^ (int)this.m_Flags;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00035CAC File Offset: 0x00033EAC
		public static bool operator ==(VisibleLight left, VisibleLight right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00035CC8 File Offset: 0x00033EC8
		public static bool operator !=(VisibleLight left, VisibleLight right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B42 RID: 2882
		private LightType m_LightType;

		// Token: 0x04000B43 RID: 2883
		private Color m_FinalColor;

		// Token: 0x04000B44 RID: 2884
		private Rect m_ScreenRect;

		// Token: 0x04000B45 RID: 2885
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000B46 RID: 2886
		private float m_Range;

		// Token: 0x04000B47 RID: 2887
		private float m_SpotAngle;

		// Token: 0x04000B48 RID: 2888
		private int m_InstanceId;

		// Token: 0x04000B49 RID: 2889
		private VisibleLightFlags m_Flags;
	}
}
