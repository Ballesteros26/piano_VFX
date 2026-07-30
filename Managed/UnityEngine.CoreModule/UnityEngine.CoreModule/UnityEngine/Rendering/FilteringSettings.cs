using System;
using UnityEngine.Internal;

namespace UnityEngine.Rendering
{
	// Token: 0x02000369 RID: 873
	public struct FilteringSettings : IEquatable<FilteringSettings>
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00032C2E File Offset: 0x00030E2E
		public static FilteringSettings defaultValue
		{
			get
			{
				return new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, uint.MaxValue, 0);
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00032C44 File Offset: 0x00030E44
		public FilteringSettings([DefaultValue("RenderQueueRange.all")] RenderQueueRange? renderQueueRange = null, int layerMask = -1, uint renderingLayerMask = 4294967295U, int excludeMotionVectorObjects = 0)
		{
			this = default(FilteringSettings);
			this.m_RenderQueueRange = renderQueueRange ?? RenderQueueRange.all;
			this.m_LayerMask = layerMask;
			this.m_RenderingLayerMask = renderingLayerMask;
			this.m_ExcludeMotionVectorObjects = excludeMotionVectorObjects;
			this.m_SortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00032C9C File Offset: 0x00030E9C
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x00032CB4 File Offset: 0x00030EB4
		public RenderQueueRange renderQueueRange
		{
			get
			{
				return this.m_RenderQueueRange;
			}
			set
			{
				this.m_RenderQueueRange = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00032CC0 File Offset: 0x00030EC0
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x00032CD8 File Offset: 0x00030ED8
		public int layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00032CE4 File Offset: 0x00030EE4
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x00032CFC File Offset: 0x00030EFC
		public uint renderingLayerMask
		{
			get
			{
				return this.m_RenderingLayerMask;
			}
			set
			{
				this.m_RenderingLayerMask = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00032D08 File Offset: 0x00030F08
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x00032D23 File Offset: 0x00030F23
		public bool excludeMotionVectorObjects
		{
			get
			{
				return this.m_ExcludeMotionVectorObjects != 0;
			}
			set
			{
				this.m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00032D34 File Offset: 0x00030F34
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x00032D4C File Offset: 0x00030F4C
		public SortingLayerRange sortingLayerRange
		{
			get
			{
				return this.m_SortingLayerRange;
			}
			set
			{
				this.m_SortingLayerRange = value;
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00032D58 File Offset: 0x00030F58
		public bool Equals(FilteringSettings other)
		{
			return this.m_RenderQueueRange.Equals(other.m_RenderQueueRange) && this.m_LayerMask == other.m_LayerMask && this.m_RenderingLayerMask == other.m_RenderingLayerMask && this.m_ExcludeMotionVectorObjects == other.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00032DAC File Offset: 0x00030FAC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is FilteringSettings && this.Equals((FilteringSettings)obj);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00032DE4 File Offset: 0x00030FE4
		public override int GetHashCode()
		{
			int num = this.m_RenderQueueRange.GetHashCode();
			num = (num * 397) ^ this.m_LayerMask;
			num = (num * 397) ^ (int)this.m_RenderingLayerMask;
			return (num * 397) ^ this.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00032E38 File Offset: 0x00031038
		public static bool operator ==(FilteringSettings left, FilteringSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00032E54 File Offset: 0x00031054
		public static bool operator !=(FilteringSettings left, FilteringSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000AA5 RID: 2725
		private RenderQueueRange m_RenderQueueRange;

		// Token: 0x04000AA6 RID: 2726
		private int m_LayerMask;

		// Token: 0x04000AA7 RID: 2727
		private uint m_RenderingLayerMask;

		// Token: 0x04000AA8 RID: 2728
		private int m_ExcludeMotionVectorObjects;

		// Token: 0x04000AA9 RID: 2729
		private SortingLayerRange m_SortingLayerRange;
	}
}
