using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE2 RID: 2786
	internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
	{
		// Token: 0x060063EC RID: 25580 RVA: 0x00143D71 File Offset: 0x00141F71
		public NullableTypeInfo(List<Type> recursionCheck)
		{
			this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00143D88 File Offset: 0x00141F88
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x00143DC0 File Offset: 0x00141FC0
		public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
		{
			bool flag = value != null;
			collector.AddScalar(flag);
			T t = (flag ? value.Value : default(T));
			this.valueInfo.WriteData(collector, ref t);
		}

		// Token: 0x04003189 RID: 12681
		private readonly TraceLoggingTypeInfo<T> valueInfo;
	}
}
