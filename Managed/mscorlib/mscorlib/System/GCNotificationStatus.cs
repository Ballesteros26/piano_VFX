using System;

namespace System
{
	/// <summary>Provides information about the current registration for notification of the next full garbage collection. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000166 RID: 358
	[Serializable]
	public enum GCNotificationStatus
	{
		/// <summary>The notification was successful and the registration was not canceled.</summary>
		// Token: 0x04000921 RID: 2337
		Succeeded,
		/// <summary>The notification failed for any reason.</summary>
		// Token: 0x04000922 RID: 2338
		Failed,
		/// <summary>The current registration was canceled by the user. </summary>
		// Token: 0x04000923 RID: 2339
		Canceled,
		/// <summary>The time specified by the <paramref name="millisecondsTimeout" /> parameter for either <see cref="M:System.GC.WaitForFullGCApproach(System.Int32)" /> or <see cref="M:System.GC.WaitForFullGCComplete(System.Int32)" /> has elapsed.</summary>
		// Token: 0x04000924 RID: 2340
		Timeout,
		/// <summary>This result can be caused by the following: there is no current registration for a garbage collection notification, concurrent garbage collection is enabled, or the time specified for the <paramref name="millisecondsTimeout" /> parameter has expired and no garbage collection notification was obtained. (See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.)</summary>
		// Token: 0x04000925 RID: 2341
		NotApplicable
	}
}
