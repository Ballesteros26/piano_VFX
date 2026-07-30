using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC8 RID: 2760
	internal sealed class UInt16ArrayTypeInfo : TraceLoggingTypeInfo<ushort[]>
	{
		// Token: 0x06006394 RID: 25492 RVA: 0x001438D2 File Offset: 0x00141AD2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x001438E2 File Offset: 0x00141AE2
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort[] value)
		{
			collector.AddArray(value);
		}
	}
}
