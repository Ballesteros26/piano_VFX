using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC7 RID: 2759
	internal sealed class Int16ArrayTypeInfo : TraceLoggingTypeInfo<short[]>
	{
		// Token: 0x06006391 RID: 25489 RVA: 0x001438B0 File Offset: 0x00141AB0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x001438C0 File Offset: 0x00141AC0
		public override void WriteData(TraceLoggingDataCollector collector, ref short[] value)
		{
			collector.AddArray(value);
		}
	}
}
