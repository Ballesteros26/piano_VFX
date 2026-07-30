using System;

namespace System.Data
{
	// Token: 0x020000DC RID: 220
	internal struct Range
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003641C File Offset: 0x0003461C
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw ExceptionBuilder.RangeArgument(min, max);
			}
			this._min = min;
			this._max = max;
			this._isNotNull = true;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0003643F File Offset: 0x0003463F
		public int Count
		{
			get
			{
				if (!this.IsNull)
				{
					return this._max - this._min + 1;
				}
				return 0;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0003645A File Offset: 0x0003465A
		public bool IsNull
		{
			get
			{
				return !this._isNotNull;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00036465 File Offset: 0x00034665
		public int Max
		{
			get
			{
				this.CheckNull();
				return this._max;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00036473 File Offset: 0x00034673
		public int Min
		{
			get
			{
				this.CheckNull();
				return this._min;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00036481 File Offset: 0x00034681
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x040007F3 RID: 2035
		private int _min;

		// Token: 0x040007F4 RID: 2036
		private int _max;

		// Token: 0x040007F5 RID: 2037
		private bool _isNotNull;
	}
}
