using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AC5 RID: 2757
	internal sealed class ByteArrayTypeInfo : TraceLoggingTypeInfo<byte[]>
	{
		// Token: 0x0600638B RID: 25483 RVA: 0x00143804 File Offset: 0x00141A04
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			switch (format)
			{
			case EventFieldFormat.String:
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsString);
				return;
			case EventFieldFormat.Boolean:
				collector.AddArray(name, TraceLoggingDataType.Boolean8);
				return;
			case EventFieldFormat.Hexadecimal:
				collector.AddArray(name, TraceLoggingDataType.HexInt8);
				return;
			default:
				if (format == EventFieldFormat.Xml)
				{
					collector.AddBinary(name, TraceLoggingDataType.CountedMbcsXml);
					return;
				}
				if (format != EventFieldFormat.Json)
				{
					collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.Binary, format));
					return;
				}
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsJson);
				return;
			}
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x0014387C File Offset: 0x00141A7C
		public override void WriteData(TraceLoggingDataCollector collector, ref byte[] value)
		{
			collector.AddBinary(value);
		}
	}
}
