using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036D RID: 877
	public struct RasterState : IEquatable<RasterState>
	{
		// Token: 0x06001E0A RID: 7690 RVA: 0x00033079 File Offset: 0x00031279
		public RasterState(CullMode cullingMode = CullMode.Back, int offsetUnits = 0, float offsetFactor = 0f, bool depthClip = true)
		{
			this.m_CullingMode = cullingMode;
			this.m_OffsetUnits = offsetUnits;
			this.m_OffsetFactor = offsetFactor;
			this.m_DepthClip = Convert.ToByte(depthClip);
			this.m_Conservative = Convert.ToByte(false);
			this.m_Padding1 = 0;
			this.m_Padding2 = 0;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000330B8 File Offset: 0x000312B8
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x000330D0 File Offset: 0x000312D0
		public CullMode cullingMode
		{
			get
			{
				return this.m_CullingMode;
			}
			set
			{
				this.m_CullingMode = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x000330DC File Offset: 0x000312DC
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x000330F9 File Offset: 0x000312F9
		public bool depthClip
		{
			get
			{
				return Convert.ToBoolean(this.m_DepthClip);
			}
			set
			{
				this.m_DepthClip = Convert.ToByte(value);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00033108 File Offset: 0x00031308
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x00033125 File Offset: 0x00031325
		public bool conservative
		{
			get
			{
				return Convert.ToBoolean(this.m_Conservative);
			}
			set
			{
				this.m_Conservative = Convert.ToByte(value);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00033134 File Offset: 0x00031334
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0003314C File Offset: 0x0003134C
		public int offsetUnits
		{
			get
			{
				return this.m_OffsetUnits;
			}
			set
			{
				this.m_OffsetUnits = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00033158 File Offset: 0x00031358
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x00033170 File Offset: 0x00031370
		public float offsetFactor
		{
			get
			{
				return this.m_OffsetFactor;
			}
			set
			{
				this.m_OffsetFactor = value;
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0003317C File Offset: 0x0003137C
		public bool Equals(RasterState other)
		{
			return this.m_CullingMode == other.m_CullingMode && this.m_OffsetUnits == other.m_OffsetUnits && this.m_OffsetFactor.Equals(other.m_OffsetFactor) && this.m_DepthClip == other.m_DepthClip && this.m_Conservative == other.m_Conservative;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000331DC File Offset: 0x000313DC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RasterState && this.Equals((RasterState)obj);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00033214 File Offset: 0x00031414
		public override int GetHashCode()
		{
			int num = (int)this.m_CullingMode;
			num = (num * 397) ^ this.m_OffsetUnits;
			num = (num * 397) ^ this.m_OffsetFactor.GetHashCode();
			num = (num * 397) ^ this.m_DepthClip.GetHashCode();
			return (num * 397) ^ this.m_Conservative.GetHashCode();
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0003327C File Offset: 0x0003147C
		public static bool operator ==(RasterState left, RasterState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00033298 File Offset: 0x00031498
		public static bool operator !=(RasterState left, RasterState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000ABF RID: 2751
		public static readonly RasterState defaultValue = new RasterState(CullMode.Back, 0, 0f, true);

		// Token: 0x04000AC0 RID: 2752
		private CullMode m_CullingMode;

		// Token: 0x04000AC1 RID: 2753
		private int m_OffsetUnits;

		// Token: 0x04000AC2 RID: 2754
		private float m_OffsetFactor;

		// Token: 0x04000AC3 RID: 2755
		private byte m_DepthClip;

		// Token: 0x04000AC4 RID: 2756
		private byte m_Conservative;

		// Token: 0x04000AC5 RID: 2757
		private byte m_Padding1;

		// Token: 0x04000AC6 RID: 2758
		private byte m_Padding2;
	}
}
