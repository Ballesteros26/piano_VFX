using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAF RID: 2735
	internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06006348 RID: 25416 RVA: 0x001433F4 File Offset: 0x001415F4
		public StructPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x00143430 File Offset: 0x00141630
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(ref container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x00143470 File Offset: 0x00141670
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(ref container);
		}

		// Token: 0x0400317D RID: 12669
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x0400317E RID: 12670
		private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000AB0 RID: 2736
		// (Invoke) Token: 0x0600634C RID: 25420
		private delegate ValueType Getter(ref ContainerType container);
	}
}
