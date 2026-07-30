using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADE RID: 2782
	internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo<DateTimeOffset>
	{
		// Token: 0x060063DF RID: 25567 RVA: 0x00143BAD File Offset: 0x00141DAD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("Ticks", Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
			traceLoggingMetadataCollector.AddScalar("Offset", TraceLoggingDataType.Int64);
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x00143BD8 File Offset: 0x00141DD8
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTimeOffset value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
			collector.AddScalar(value.Offset.Ticks);
		}
	}
}
