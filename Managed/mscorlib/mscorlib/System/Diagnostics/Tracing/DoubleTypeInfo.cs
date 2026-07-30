using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC1 RID: 2753
	internal sealed class DoubleTypeInfo : TraceLoggingTypeInfo<double>
	{
		// Token: 0x0600637F RID: 25471 RVA: 0x00143770 File Offset: 0x00141970
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x00143781 File Offset: 0x00141981
		public override void WriteData(TraceLoggingDataCollector collector, ref double value)
		{
			collector.AddScalar(value);
		}
	}
}
