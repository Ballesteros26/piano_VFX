using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACA RID: 2762
	internal sealed class UInt32ArrayTypeInfo : TraceLoggingTypeInfo<uint[]>
	{
		// Token: 0x0600639A RID: 25498 RVA: 0x00143916 File Offset: 0x00141B16
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x00143926 File Offset: 0x00141B26
		public override void WriteData(TraceLoggingDataCollector collector, ref uint[] value)
		{
			collector.AddArray(value);
		}
	}
}
