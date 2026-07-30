using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE1 RID: 2785
	internal sealed class KeyValuePairTypeInfo<K, V> : TraceLoggingTypeInfo<KeyValuePair<K, V>>
	{
		// Token: 0x060063E8 RID: 25576 RVA: 0x00143C7D File Offset: 0x00141E7D
		public KeyValuePairTypeInfo(List<Type> recursionCheck)
		{
			this.keyInfo = TraceLoggingTypeInfo<K>.GetInstance(recursionCheck);
			this.valueInfo = TraceLoggingTypeInfo<V>.GetInstance(recursionCheck);
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x00143CA0 File Offset: 0x00141EA0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			this.keyInfo.WriteMetadata(traceLoggingMetadataCollector, "Key", EventFieldFormat.Default);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x00143CDC File Offset: 0x00141EDC
		public override void WriteData(TraceLoggingDataCollector collector, ref KeyValuePair<K, V> value)
		{
			K key = value.Key;
			V value2 = value.Value;
			this.keyInfo.WriteData(collector, ref key);
			this.valueInfo.WriteData(collector, ref value2);
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x00143D14 File Offset: 0x00141F14
		public override object GetData(object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)value;
			dictionary.Add("Key", this.keyInfo.GetData(keyValuePair.Key));
			dictionary.Add("Value", this.valueInfo.GetData(keyValuePair.Value));
			return dictionary;
		}

		// Token: 0x04003187 RID: 12679
		private readonly TraceLoggingTypeInfo<K> keyInfo;

		// Token: 0x04003188 RID: 12680
		private readonly TraceLoggingTypeInfo<V> valueInfo;
	}
}
