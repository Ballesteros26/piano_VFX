using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB6 RID: 2742
	internal sealed class BooleanTypeInfo : TraceLoggingTypeInfo<bool>
	{
		// Token: 0x0600635E RID: 25438 RVA: 0x001435EC File Offset: 0x001417EC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x00143600 File Offset: 0x00141800
		public override void WriteData(TraceLoggingDataCollector collector, ref bool value)
		{
			collector.AddScalar(value);
		}
	}
}
