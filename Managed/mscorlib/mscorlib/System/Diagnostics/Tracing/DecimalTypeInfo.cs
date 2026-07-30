using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE0 RID: 2784
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<decimal>
	{
		// Token: 0x060063E5 RID: 25573 RVA: 0x00143C50 File Offset: 0x00141E50
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x00143C61 File Offset: 0x00141E61
		public override void WriteData(TraceLoggingDataCollector collector, ref decimal value)
		{
			collector.AddScalar((double)value);
		}
	}
}
