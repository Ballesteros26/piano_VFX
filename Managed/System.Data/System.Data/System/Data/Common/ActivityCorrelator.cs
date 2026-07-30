using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000380 RID: 896
	internal static class ActivityCorrelator
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000BCEB3 File Offset: 0x000BB0B3
		internal static ActivityCorrelator.ActivityId Current
		{
			get
			{
				if (ActivityCorrelator.t_tlsActivity == null)
				{
					ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
				}
				return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000BCED0 File Offset: 0x000BB0D0
		internal static ActivityCorrelator.ActivityId Next()
		{
			if (ActivityCorrelator.t_tlsActivity == null)
			{
				ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
			}
			ActivityCorrelator.t_tlsActivity.Increment();
			return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
		}

		// Token: 0x04001988 RID: 6536
		[ThreadStatic]
		private static ActivityCorrelator.ActivityId t_tlsActivity;

		// Token: 0x02000381 RID: 897
		internal class ActivityId
		{
			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000BCEF7 File Offset: 0x000BB0F7
			// (set) Token: 0x06002A9C RID: 10908 RVA: 0x000BCEFF File Offset: 0x000BB0FF
			internal Guid Id { get; private set; }

			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000BCF08 File Offset: 0x000BB108
			// (set) Token: 0x06002A9E RID: 10910 RVA: 0x000BCF10 File Offset: 0x000BB110
			internal uint Sequence { get; private set; }

			// Token: 0x06002A9F RID: 10911 RVA: 0x000BCF19 File Offset: 0x000BB119
			internal ActivityId()
			{
				this.Id = Guid.NewGuid();
				this.Sequence = 0U;
			}

			// Token: 0x06002AA0 RID: 10912 RVA: 0x000BCF33 File Offset: 0x000BB133
			internal ActivityId(ActivityCorrelator.ActivityId activity)
			{
				this.Id = activity.Id;
				this.Sequence = activity.Sequence;
			}

			// Token: 0x06002AA1 RID: 10913 RVA: 0x000BCF54 File Offset: 0x000BB154
			internal void Increment()
			{
				uint num = this.Sequence + 1U;
				this.Sequence = num;
			}

			// Token: 0x06002AA2 RID: 10914 RVA: 0x000BCF71 File Offset: 0x000BB171
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Id, this.Sequence);
			}
		}
	}
}
