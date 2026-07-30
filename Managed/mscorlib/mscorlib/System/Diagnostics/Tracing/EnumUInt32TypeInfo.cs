using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD7 RID: 2775
	internal sealed class EnumUInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063C6 RID: 25542 RVA: 0x001436BC File Offset: 0x001418BC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x00143A95 File Offset: 0x00141C95
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<uint>.Cast<EnumType>(value));
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
