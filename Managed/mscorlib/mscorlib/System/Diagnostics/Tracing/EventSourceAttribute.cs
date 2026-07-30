using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Allows the event tracing for Windows (ETW) name to be defined independently of the name of the event source class.   </summary>
	// Token: 0x02000B05 RID: 2821
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the event source.</summary>
		/// <returns>The name of the event source.</returns>
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x0014D53C File Offset: 0x0014B73C
		// (set) Token: 0x0600657E RID: 25982 RVA: 0x0014D544 File Offset: 0x0014B744
		public string Name { get; set; }

		/// <summary>Gets or sets the event source identifier.</summary>
		/// <returns>The event source identifier.</returns>
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x0600657F RID: 25983 RVA: 0x0014D54D File Offset: 0x0014B74D
		// (set) Token: 0x06006580 RID: 25984 RVA: 0x0014D555 File Offset: 0x0014B755
		public string Guid { get; set; }

		/// <summary>Gets or sets the name of the localization resource file.</summary>
		/// <returns>The name of the localization resource file, or null if the localization resource file does not exist.</returns>
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x0014D55E File Offset: 0x0014B75E
		// (set) Token: 0x06006582 RID: 25986 RVA: 0x0014D566 File Offset: 0x0014B766
		public string LocalizationResources { get; set; }
	}
}
