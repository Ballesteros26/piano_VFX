using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABC RID: 2748
	internal sealed class UInt32TypeInfo : TraceLoggingTypeInfo<uint>
	{
		// Token: 0x06006370 RID: 25456 RVA: 0x001436BC File Offset: 0x001418BC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x001436CC File Offset: 0x001418CC
		public override void WriteData(TraceLoggingDataCollector collector, ref uint value)
		{
			collector.AddScalar(value);
		}
	}
}
