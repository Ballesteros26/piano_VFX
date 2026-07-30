using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Provides contextual information that the provider can use when persisting settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class SettingsContext : Hashtable
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0003C867 File Offset: 0x0003AA67
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0003C86F File Offset: 0x0003AA6F
		internal ApplicationSettingsBase CurrentSettings
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = value;
			}
		}

		// Token: 0x04000FDE RID: 4062
		[NonSerialized]
		private ApplicationSettingsBase current;
	}
}
