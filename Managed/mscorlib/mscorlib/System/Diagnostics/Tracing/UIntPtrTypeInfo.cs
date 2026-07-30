using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC0 RID: 2752
	internal sealed class UIntPtrTypeInfo : TraceLoggingTypeInfo<UIntPtr>
	{
		// Token: 0x0600637C RID: 25468 RVA: 0x0014374A File Offset: 0x0014194A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0014375E File Offset: 0x0014195E
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
