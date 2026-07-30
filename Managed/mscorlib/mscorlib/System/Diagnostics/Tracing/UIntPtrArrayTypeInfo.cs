using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACE RID: 2766
	internal sealed class UIntPtrArrayTypeInfo : TraceLoggingTypeInfo<UIntPtr[]>
	{
		// Token: 0x060063A6 RID: 25510 RVA: 0x001439A4 File Offset: 0x00141BA4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x001439B8 File Offset: 0x00141BB8
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
