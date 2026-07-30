using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AF0 RID: 2800
	internal abstract class TraceLoggingTypeInfo<DataType> : TraceLoggingTypeInfo
	{
		// Token: 0x060064EA RID: 25834 RVA: 0x0014ABE6 File Offset: 0x00148DE6
		protected TraceLoggingTypeInfo()
			: base(typeof(DataType))
		{
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0014ABF8 File Offset: 0x00148DF8
		protected TraceLoggingTypeInfo(string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
			: base(typeof(DataType), name, level, opcode, keywords, tags)
		{
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x0014AC11 File Offset: 0x00148E11
		public static TraceLoggingTypeInfo<DataType> Instance
		{
			get
			{
				return TraceLoggingTypeInfo<DataType>.instance ?? TraceLoggingTypeInfo<DataType>.InitInstance();
			}
		}

		// Token: 0x060064ED RID: 25837
		public abstract void WriteData(TraceLoggingDataCollector collector, ref DataType value);

		// Token: 0x060064EE RID: 25838 RVA: 0x0014AC24 File Offset: 0x00148E24
		public override void WriteObjectData(TraceLoggingDataCollector collector, object value)
		{
			DataType dataType = ((value == null) ? default(DataType) : ((DataType)((object)value)));
			this.WriteData(collector, ref dataType);
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x0014AC50 File Offset: 0x00148E50
		internal static TraceLoggingTypeInfo<DataType> GetInstance(List<Type> recursionCheck)
		{
			if (TraceLoggingTypeInfo<DataType>.instance == null)
			{
				int count = recursionCheck.Count;
				TraceLoggingTypeInfo<DataType> traceLoggingTypeInfo = Statics.CreateDefaultTypeInfo<DataType>(recursionCheck);
				Interlocked.CompareExchange<TraceLoggingTypeInfo<DataType>>(ref TraceLoggingTypeInfo<DataType>.instance, traceLoggingTypeInfo, null);
				recursionCheck.RemoveRange(count, recursionCheck.Count - count);
			}
			return TraceLoggingTypeInfo<DataType>.instance;
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x0014AC93 File Offset: 0x00148E93
		private static TraceLoggingTypeInfo<DataType> InitInstance()
		{
			return TraceLoggingTypeInfo<DataType>.GetInstance(new List<Type>());
		}

		// Token: 0x0400320D RID: 12813
		private static TraceLoggingTypeInfo<DataType> instance;
	}
}
