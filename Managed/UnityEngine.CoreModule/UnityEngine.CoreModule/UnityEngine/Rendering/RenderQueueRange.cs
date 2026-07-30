using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000372 RID: 882
	public struct RenderQueueRange : IEquatable<RenderQueueRange>
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x0003378C File Offset: 0x0003198C
		public RenderQueueRange(int lowerBound, int upperBound)
		{
			bool flag = lowerBound < 0 || lowerBound > 5000;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("lowerBound", lowerBound, string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
			}
			bool flag2 = upperBound < 0 || upperBound > 5000;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("upperBound", upperBound, string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
			}
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00033824 File Offset: 0x00031A24
		public static RenderQueueRange all
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00033850 File Offset: 0x00031A50
		public static RenderQueueRange opaque
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 2500
				};
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0003387C File Offset: 0x00031A7C
		public static RenderQueueRange transparent
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 2501,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000338AC File Offset: 0x00031AAC
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x000338C4 File Offset: 0x00031AC4
		public int lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_LowerBound = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0003390C File Offset: 0x00031B0C
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x00033924 File Offset: 0x00031B24
		public int upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_UpperBound = value;
			}
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0003396C File Offset: 0x00031B6C
		public bool Equals(RenderQueueRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000339A0 File Offset: 0x00031BA0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderQueueRange && this.Equals((RenderQueueRange)obj);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000339D8 File Offset: 0x00031BD8
		public override int GetHashCode()
		{
			return (this.m_LowerBound * 397) ^ this.m_UpperBound;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00033A00 File Offset: 0x00031C00
		public static bool operator ==(RenderQueueRange left, RenderQueueRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00033A1C File Offset: 0x00031C1C
		public static bool operator !=(RenderQueueRange left, RenderQueueRange right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AD5 RID: 2773
		private int m_LowerBound;

		// Token: 0x04000AD6 RID: 2774
		private int m_UpperBound;

		// Token: 0x04000AD7 RID: 2775
		private const int k_MinimumBound = 0;

		// Token: 0x04000AD8 RID: 2776
		public static readonly int minimumBound = 0;

		// Token: 0x04000AD9 RID: 2777
		private const int k_MaximumBound = 5000;

		// Token: 0x04000ADA RID: 2778
		public static readonly int maximumBound = 5000;
	}
}
