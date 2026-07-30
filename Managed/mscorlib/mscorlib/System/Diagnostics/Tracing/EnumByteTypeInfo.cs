using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD2 RID: 2770
	internal sealed class EnumByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063B2 RID: 25522 RVA: 0x00143612 File Offset: 0x00141812
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00143A36 File Offset: 0x00141C36
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<byte>.Cast<EnumType>(value));
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
