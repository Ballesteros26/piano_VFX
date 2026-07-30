using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADD RID: 2781
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo<DateTime>
	{
		// Token: 0x060063DC RID: 25564 RVA: 0x00143B5D File Offset: 0x00141D5D
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x00143B70 File Offset: 0x00141D70
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTime value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
		}
	}
}
