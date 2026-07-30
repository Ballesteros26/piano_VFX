using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABF RID: 2751
	internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
	{
		// Token: 0x06006379 RID: 25465 RVA: 0x00143724 File Offset: 0x00141924
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x00143738 File Offset: 0x00141938
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
