using System;

namespace System.Diagnostics
{
	// Token: 0x0200021B RID: 539
	internal class TraceImplSettings
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x0004B6ED File Offset: 0x000498ED
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener
			{
				IndentSize = this.IndentSize
			});
		}

		// Token: 0x04001201 RID: 4609
		public const string Key = ".__TraceInfoSettingsKey__.";

		// Token: 0x04001202 RID: 4610
		public bool AutoFlush;

		// Token: 0x04001203 RID: 4611
		public int IndentSize = 4;

		// Token: 0x04001204 RID: 4612
		public TraceListenerCollection Listeners = new TraceListenerCollection();
	}
}
