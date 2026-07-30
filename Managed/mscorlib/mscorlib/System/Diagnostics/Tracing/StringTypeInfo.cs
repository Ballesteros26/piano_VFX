using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000ADA RID: 2778
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo<string>
	{
		// Token: 0x060063D2 RID: 25554 RVA: 0x00143ACE File Offset: 0x00141CCE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.CountedUtf16String, format));
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00143ADF File Offset: 0x00141CDF
		public override void WriteData(TraceLoggingDataCollector collector, ref string value)
		{
			collector.AddBinary(value);
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x00143AEC File Offset: 0x00141CEC
		public override object GetData(object value)
		{
			object obj = base.GetData(value);
			if (obj == null)
			{
				obj = "";
			}
			return obj;
		}
	}
}
