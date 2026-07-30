using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B9 RID: 1209
	internal struct EventTypeSpec
	{
		// Token: 0x06004C1C RID: 19484 RVA: 0x0012EEE8 File Offset: 0x0012D0E8
		public EventTypeSpec(uint eventClass, uint eventKind)
		{
			this.eventClass = eventClass;
			this.eventKind = eventKind;
		}

		// Token: 0x04002970 RID: 10608
		public uint eventClass;

		// Token: 0x04002971 RID: 10609
		public uint eventKind;
	}
}
