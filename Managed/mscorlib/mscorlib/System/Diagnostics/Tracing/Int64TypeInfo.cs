using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABD RID: 2749
	internal sealed class Int64TypeInfo : TraceLoggingTypeInfo<long>
	{
		// Token: 0x06006373 RID: 25459 RVA: 0x001436DE File Offset: 0x001418DE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001436EF File Offset: 0x001418EF
		public override void WriteData(TraceLoggingDataCollector collector, ref long value)
		{
			collector.AddScalar(value);
		}
	}
}
