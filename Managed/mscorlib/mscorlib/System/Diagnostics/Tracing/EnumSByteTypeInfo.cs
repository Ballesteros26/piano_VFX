using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD3 RID: 2771
	internal sealed class EnumSByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063B6 RID: 25526 RVA: 0x00143634 File Offset: 0x00141834
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x00143A49 File Offset: 0x00141C49
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<sbyte>.Cast<EnumType>(value));
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
