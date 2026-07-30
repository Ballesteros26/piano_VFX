using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABA RID: 2746
	internal sealed class UInt16TypeInfo : TraceLoggingTypeInfo<ushort>
	{
		// Token: 0x0600636A RID: 25450 RVA: 0x00143678 File Offset: 0x00141878
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x00143688 File Offset: 0x00141888
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort value)
		{
			collector.AddScalar(value);
		}
	}
}
