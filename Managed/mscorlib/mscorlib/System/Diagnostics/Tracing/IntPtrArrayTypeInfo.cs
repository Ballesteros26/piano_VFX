using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACD RID: 2765
	internal sealed class IntPtrArrayTypeInfo : TraceLoggingTypeInfo<IntPtr[]>
	{
		// Token: 0x060063A3 RID: 25507 RVA: 0x0014397E File Offset: 0x00141B7E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x00143992 File Offset: 0x00141B92
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr[] value)
		{
			collector.AddArray(value);
		}
	}
}
