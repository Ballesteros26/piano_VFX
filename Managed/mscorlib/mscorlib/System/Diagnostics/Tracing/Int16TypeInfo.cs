using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB9 RID: 2745
	internal sealed class Int16TypeInfo : TraceLoggingTypeInfo<short>
	{
		// Token: 0x06006367 RID: 25447 RVA: 0x00143656 File Offset: 0x00141856
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x00143666 File Offset: 0x00141866
		public override void WriteData(TraceLoggingDataCollector collector, ref short value)
		{
			collector.AddScalar(value);
		}
	}
}
