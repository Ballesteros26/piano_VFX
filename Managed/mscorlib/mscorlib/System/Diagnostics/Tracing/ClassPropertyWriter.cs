using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AB1 RID: 2737
	internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600634F RID: 25423 RVA: 0x001434A2 File Offset: 0x001416A2
		public ClassPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x001434DC File Offset: 0x001416DC
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x00143524 File Offset: 0x00141724
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(container);
		}

		// Token: 0x0400317F RID: 12671
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x04003180 RID: 12672
		private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000AB2 RID: 2738
		// (Invoke) Token: 0x06006353 RID: 25427
		private delegate ValueType Getter(ContainerType container);
	}
}
