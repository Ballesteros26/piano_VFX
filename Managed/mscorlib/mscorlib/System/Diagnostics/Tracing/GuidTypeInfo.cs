using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADB RID: 2779
	internal sealed class GuidTypeInfo : TraceLoggingTypeInfo<Guid>
	{
		// Token: 0x060063D6 RID: 25558 RVA: 0x00143B13 File Offset: 0x00141D13
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x00143B24 File Offset: 0x00141D24
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid value)
		{
			collector.AddScalar(value);
		}
	}
}
