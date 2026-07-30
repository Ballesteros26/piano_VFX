using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB8 RID: 2744
	internal sealed class SByteTypeInfo : TraceLoggingTypeInfo<sbyte>
	{
		// Token: 0x06006364 RID: 25444 RVA: 0x00143634 File Offset: 0x00141834
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x00143644 File Offset: 0x00141844
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte value)
		{
			collector.AddScalar(value);
		}
	}
}
