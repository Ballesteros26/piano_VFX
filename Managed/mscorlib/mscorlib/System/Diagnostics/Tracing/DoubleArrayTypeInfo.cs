using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD0 RID: 2768
	internal sealed class DoubleArrayTypeInfo : TraceLoggingTypeInfo<double[]>
	{
		// Token: 0x060063AC RID: 25516 RVA: 0x001439F0 File Offset: 0x00141BF0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x00143A01 File Offset: 0x00141C01
		public override void WriteData(TraceLoggingDataCollector collector, ref double[] value)
		{
			collector.AddArray(value);
		}
	}
}
