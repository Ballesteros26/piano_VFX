using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC9 RID: 2761
	internal sealed class Int32ArrayTypeInfo : TraceLoggingTypeInfo<int[]>
	{
		// Token: 0x06006397 RID: 25495 RVA: 0x001438F4 File Offset: 0x00141AF4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x00143904 File Offset: 0x00141B04
		public override void WriteData(TraceLoggingDataCollector collector, ref int[] value)
		{
			collector.AddArray(value);
		}
	}
}
