using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC2 RID: 2754
	internal sealed class SingleTypeInfo : TraceLoggingTypeInfo<float>
	{
		// Token: 0x06006382 RID: 25474 RVA: 0x00143793 File Offset: 0x00141993
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x001437A4 File Offset: 0x001419A4
		public override void WriteData(TraceLoggingDataCollector collector, ref float value)
		{
			collector.AddScalar(value);
		}
	}
}
