using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACB RID: 2763
	internal sealed class Int64ArrayTypeInfo : TraceLoggingTypeInfo<long[]>
	{
		// Token: 0x0600639D RID: 25501 RVA: 0x00143938 File Offset: 0x00141B38
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x00143949 File Offset: 0x00141B49
		public override void WriteData(TraceLoggingDataCollector collector, ref long[] value)
		{
			collector.AddArray(value);
		}
	}
}
