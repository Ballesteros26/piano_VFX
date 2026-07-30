using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC3 RID: 2755
	internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
	{
		// Token: 0x06006385 RID: 25477 RVA: 0x001437B6 File Offset: 0x001419B6
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x001437CA File Offset: 0x001419CA
		public override void WriteData(TraceLoggingDataCollector collector, ref char value)
		{
			collector.AddScalar(value);
		}
	}
}
