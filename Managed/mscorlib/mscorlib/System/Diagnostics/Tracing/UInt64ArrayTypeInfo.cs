using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACC RID: 2764
	internal sealed class UInt64ArrayTypeInfo : TraceLoggingTypeInfo<ulong[]>
	{
		// Token: 0x060063A0 RID: 25504 RVA: 0x0014395B File Offset: 0x00141B5B
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0014396C File Offset: 0x00141B6C
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong[] value)
		{
			collector.AddArray(value);
		}
	}
}
