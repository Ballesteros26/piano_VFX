using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD9 RID: 2777
	internal sealed class EnumUInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063CE RID: 25550 RVA: 0x00143701 File Offset: 0x00141901
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x00143ABB File Offset: 0x00141CBB
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ulong>.Cast<EnumType>(value));
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
