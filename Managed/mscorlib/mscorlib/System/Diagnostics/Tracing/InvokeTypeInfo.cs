using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAB RID: 2731
	internal sealed class InvokeTypeInfo<ContainerType> : TraceLoggingTypeInfo<ContainerType>
	{
		// Token: 0x06006336 RID: 25398 RVA: 0x00143000 File Offset: 0x00141200
		public InvokeTypeInfo(TypeAnalysis typeAnalysis)
			: base(typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
				this.accessors = new PropertyAccessor<ContainerType>[this.properties.Length];
				for (int i = 0; i < this.accessors.Length; i++)
				{
					this.accessors[i] = PropertyAccessor<ContainerType>.Create(this.properties[i]);
				}
			}
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00143084 File Offset: 0x00141284
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat eventFieldFormat = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						eventFieldFormat = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, eventFieldFormat);
				}
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001430F4 File Offset: 0x001412F4
		public override void WriteData(TraceLoggingDataCollector collector, ref ContainerType value)
		{
			if (this.accessors != null)
			{
				PropertyAccessor<ContainerType>[] array = this.accessors;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Write(collector, ref value);
				}
			}
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00143128 File Offset: 0x00141328
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object data = this.accessors[i].GetData((ContainerType)((object)value));
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(data));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x001431A8 File Offset: 0x001413A8
		public override void WriteObjectData(TraceLoggingDataCollector collector, object valueObj)
		{
			if (this.accessors != null)
			{
				ContainerType containerType = ((valueObj == null) ? default(ContainerType) : ((ContainerType)((object)valueObj)));
				this.WriteData(collector, ref containerType);
			}
		}

		// Token: 0x04003174 RID: 12660
		private readonly PropertyAnalysis[] properties;

		// Token: 0x04003175 RID: 12661
		private readonly PropertyAccessor<ContainerType>[] accessors;
	}
}
