using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether the performance counter category can have multiple instances.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000209 RID: 521
	public enum PerformanceCounterCategoryType
	{
		/// <summary>The performance counter category can have only a single instance.</summary>
		// Token: 0x04001197 RID: 4503
		SingleInstance,
		/// <summary>The performance counter category can have multiple instances.</summary>
		// Token: 0x04001198 RID: 4504
		MultiInstance,
		/// <summary>The instance functionality for the performance counter category is unknown. </summary>
		// Token: 0x04001199 RID: 4505
		Unknown = -1
	}
}
