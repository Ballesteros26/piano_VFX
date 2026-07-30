using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD1 RID: 2769
	internal sealed class SingleArrayTypeInfo : TraceLoggingTypeInfo<float[]>
	{
		// Token: 0x060063AF RID: 25519 RVA: 0x00143A13 File Offset: 0x00141C13
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x00143A24 File Offset: 0x00141C24
		public override void WriteData(TraceLoggingDataCollector collector, ref float[] value)
		{
			collector.AddArray(value);
		}
	}
}
