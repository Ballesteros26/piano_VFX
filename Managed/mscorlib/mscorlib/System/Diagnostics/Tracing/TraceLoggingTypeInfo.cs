using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AEF RID: 2799
	internal abstract class TraceLoggingTypeInfo
	{
		// Token: 0x060064DF RID: 25823 RVA: 0x0014AB03 File Offset: 0x00148D03
		internal TraceLoggingTypeInfo(Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			this.name = dataType.Name;
			this.dataType = dataType;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x0014AB40 File Offset: 0x00148D40
		internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (name == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Statics.CheckName(name);
			this.name = name;
			this.keywords = keywords;
			this.level = level;
			this.opcode = opcode;
			this.tags = tags;
			this.dataType = dataType;
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060064E1 RID: 25825 RVA: 0x0014ABB6 File Offset: 0x00148DB6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0014ABBE File Offset: 0x00148DBE
		public EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x0014ABC6 File Offset: 0x00148DC6
		public EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0014ABCE File Offset: 0x00148DCE
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x0014ABD6 File Offset: 0x00148DD6
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0014ABDE File Offset: 0x00148DDE
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x060064E7 RID: 25831
		public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

		// Token: 0x060064E8 RID: 25832
		public abstract void WriteObjectData(TraceLoggingDataCollector collector, object value);

		// Token: 0x060064E9 RID: 25833 RVA: 0x0000213D File Offset: 0x0000033D
		public virtual object GetData(object value)
		{
			return value;
		}

		// Token: 0x04003207 RID: 12807
		private readonly string name;

		// Token: 0x04003208 RID: 12808
		private readonly EventKeywords keywords;

		// Token: 0x04003209 RID: 12809
		private readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x0400320A RID: 12810
		private readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x0400320B RID: 12811
		private readonly EventTags tags;

		// Token: 0x0400320C RID: 12812
		private readonly Type dataType;
	}
}
