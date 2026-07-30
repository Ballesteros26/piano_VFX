using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADC RID: 2780
	internal sealed class GuidArrayTypeInfo : TraceLoggingTypeInfo<Guid[]>
	{
		// Token: 0x060063D9 RID: 25561 RVA: 0x00143B3A File Offset: 0x00141D3A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x00143B4B File Offset: 0x00141D4B
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid[] value)
		{
			collector.AddArray(value);
		}
	}
}
