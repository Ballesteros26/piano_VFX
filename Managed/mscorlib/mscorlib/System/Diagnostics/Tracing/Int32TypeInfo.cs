using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ABB RID: 2747
	internal sealed class Int32TypeInfo : TraceLoggingTypeInfo<int>
	{
		// Token: 0x0600636D RID: 25453 RVA: 0x0014369A File Offset: 0x0014189A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x001436AA File Offset: 0x001418AA
		public override void WriteData(TraceLoggingDataCollector collector, ref int value)
		{
			collector.AddScalar(value);
		}
	}
}
