using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037E RID: 894
	public struct SortingLayerRange : IEquatable<SortingLayerRange>
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x00034BDE File Offset: 0x00032DDE
		public SortingLayerRange(short lowerBound, short upperBound)
		{
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00034BF0 File Offset: 0x00032DF0
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x00034C08 File Offset: 0x00032E08
		public short lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				this.m_LowerBound = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00034C14 File Offset: 0x00032E14
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x00034C2C File Offset: 0x00032E2C
		public short upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				this.m_UpperBound = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00034C38 File Offset: 0x00032E38
		public static SortingLayerRange all
		{
			get
			{
				return new SortingLayerRange
				{
					m_LowerBound = short.MinValue,
					m_UpperBound = short.MaxValue
				};
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00034C68 File Offset: 0x00032E68
		public bool Equals(SortingLayerRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00034C9C File Offset: 0x00032E9C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is SortingLayerRange);
			return !flag && this.Equals((SortingLayerRange)obj);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00034CD0 File Offset: 0x00032ED0
		public static bool operator !=(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00034CF0 File Offset: 0x00032EF0
		public static bool operator ==(SortingLayerRange lhs, SortingLayerRange rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00034D0C File Offset: 0x00032F0C
		public override int GetHashCode()
		{
			return ((int)this.m_UpperBound << 16) | ((int)this.m_LowerBound & 65535);
		}

		// Token: 0x04000B0B RID: 2827
		private short m_LowerBound;

		// Token: 0x04000B0C RID: 2828
		private short m_UpperBound;
	}
}
