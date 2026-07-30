using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADF RID: 2783
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
	{
		// Token: 0x060063E2 RID: 25570 RVA: 0x00143C29 File Offset: 0x00141E29
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x00143C3A File Offset: 0x00141E3A
		public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
		{
			collector.AddScalar(value.Ticks);
		}
	}
}
