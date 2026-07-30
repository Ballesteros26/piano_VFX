using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AD6 RID: 2774
	internal sealed class EnumInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060063C2 RID: 25538 RVA: 0x0014369A File Offset: 0x0014189A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x00143A82 File Offset: 0x00141C82
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<int>.Cast<EnumType>(value));
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0000213D File Offset: 0x0000033D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
