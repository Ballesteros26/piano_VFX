using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC4 RID: 2756
	internal sealed class BooleanArrayTypeInfo : TraceLoggingTypeInfo<bool[]>
	{
		// Token: 0x06006388 RID: 25480 RVA: 0x001437DC File Offset: 0x001419DC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x001437F0 File Offset: 0x001419F0
		public override void WriteData(TraceLoggingDataCollector collector, ref bool[] value)
		{
			collector.AddArray(value);
		}
	}
}
