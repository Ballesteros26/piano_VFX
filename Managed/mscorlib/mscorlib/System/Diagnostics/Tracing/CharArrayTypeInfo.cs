using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ACF RID: 2767
	internal sealed class CharArrayTypeInfo : TraceLoggingTypeInfo<char[]>
	{
		// Token: 0x060063A9 RID: 25513 RVA: 0x001439CA File Offset: 0x00141BCA
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x001439DE File Offset: 0x00141BDE
		public override void WriteData(TraceLoggingDataCollector collector, ref char[] value)
		{
			collector.AddArray(value);
		}
	}
}
