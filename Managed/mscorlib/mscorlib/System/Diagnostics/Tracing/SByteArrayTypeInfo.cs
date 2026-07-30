using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC6 RID: 2758
	internal sealed class SByteArrayTypeInfo : TraceLoggingTypeInfo<sbyte[]>
	{
		// Token: 0x0600638E RID: 25486 RVA: 0x0014388E File Offset: 0x00141A8E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x0014389E File Offset: 0x00141A9E
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte[] value)
		{
			collector.AddArray(value);
		}
	}
}
