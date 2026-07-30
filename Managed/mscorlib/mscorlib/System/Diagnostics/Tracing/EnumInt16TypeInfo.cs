using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD4 RID: 2772
	internal sealed class EnumInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063BA RID: 25530 RVA: 0x00143656 File Offset: 0x00141856
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00143A5C File Offset: 0x00141C5C
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<short>.Cast<EnumType>(value));
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
