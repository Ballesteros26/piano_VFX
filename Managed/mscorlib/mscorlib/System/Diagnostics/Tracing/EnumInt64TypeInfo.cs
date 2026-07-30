using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD8 RID: 2776
	internal sealed class EnumInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063CA RID: 25546 RVA: 0x001436DE File Offset: 0x001418DE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x00143AA8 File Offset: 0x00141CA8
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<long>.Cast<EnumType>(value));
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
