using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB7 RID: 2743
	internal sealed class ByteTypeInfo : TraceLoggingTypeInfo<byte>
	{
		// Token: 0x06006361 RID: 25441 RVA: 0x00143612 File Offset: 0x00141812
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x00143622 File Offset: 0x00141822
		public override void WriteData(TraceLoggingDataCollector collector, ref byte value)
		{
			collector.AddScalar(value);
		}
	}
}
