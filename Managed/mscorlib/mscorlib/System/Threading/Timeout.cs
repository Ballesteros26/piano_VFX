using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Contains constants that specify infinite time-out intervals. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200049C RID: 1180
	[ComVisible(true)]
	public static class Timeout
	{
		/// <summary>A constant used to specify an infinite waiting period, for methods that accept a <see cref="T:System.TimeSpan" /> parameter.</summary>
		// Token: 0x04001D29 RID: 7465
		[ComVisible(false)]
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		/// <summary>A constant used to specify an infinite waiting period. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001D2A RID: 7466
		public const int Infinite = -1;

		// Token: 0x04001D2B RID: 7467
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
