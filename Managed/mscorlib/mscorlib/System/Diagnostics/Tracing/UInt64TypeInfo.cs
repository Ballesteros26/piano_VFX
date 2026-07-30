using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABE RID: 2750
	internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
	{
		// Token: 0x06006376 RID: 25462 RVA: 0x00143701 File Offset: 0x00141901
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x00143712 File Offset: 0x00141912
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
		{
			collector.AddScalar(value);
		}
	}
}
