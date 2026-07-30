using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD5 RID: 2773
	internal sealed class EnumUInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063BE RID: 25534 RVA: 0x00143678 File Offset: 0x00141878
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00143A6F File Offset: 0x00141C6F
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ushort>.Cast<EnumType>(value));
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
