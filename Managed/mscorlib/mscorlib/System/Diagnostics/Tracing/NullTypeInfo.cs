using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB5 RID: 2741
	internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
	{
		// Token: 0x0600635A RID: 25434 RVA: 0x001435DA File Offset: 0x001417DA
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x00002194 File Offset: 0x00000394
		public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
		{
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0000A42E File Offset: 0x0000862E
		public override object GetData(object value)
		{
			return null;
		}
	}
}
