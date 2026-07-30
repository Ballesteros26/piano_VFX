using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AEC RID: 2796
	internal class TraceLoggingEventTypes
	{
		// Token: 0x060064BB RID: 25787 RVA: 0x0014A332 File Offset: 0x00148532
		internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(types))
		{
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x0014A342 File Offset: 0x00148542
		internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
		{
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x0014A354 File Offset: 0x00148554
		internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.typeInfos = this.MakeArray(paramInfos);
			this.name = name;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			for (int i = 0; i < this.typeInfos.Length; i++)
			{
				TraceLoggingTypeInfo traceLoggingTypeInfo = this.typeInfos[i];
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				string text = paramInfos[i].Name;
				if (Statics.ShouldOverrideFieldName(text))
				{
					text = traceLoggingTypeInfo.Name;
				}
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, text, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x0014A44C File Offset: 0x0014864C
		private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
		{
			if (defaultName == null)
			{
				throw new ArgumentNullException("defaultName");
			}
			this.typeInfos = typeInfos;
			this.name = defaultName;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			foreach (TraceLoggingTypeInfo traceLoggingTypeInfo in typeInfos)
			{
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, null, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x0014A51D File Offset: 0x0014871D
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x0014A525 File Offset: 0x00148725
		internal EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x0014A52D File Offset: 0x0014872D
		internal EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x0014A535 File Offset: 0x00148735
		internal EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x0014A53D File Offset: 0x0014873D
		internal EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x0014A548 File Offset: 0x00148748
		internal NameInfo GetNameInfo(string name, EventTags tags)
		{
			NameInfo nameInfo = this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags));
			if (nameInfo == null)
			{
				nameInfo = this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
			}
			return nameInfo;
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x0014A588 File Offset: 0x00148788
		private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
		{
			if (paramInfos == null)
			{
				throw new ArgumentNullException("paramInfos");
			}
			List<Type> list = new List<Type>(paramInfos.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(paramInfos[i].ParameterType, list);
			}
			return array;
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x0014A5D8 File Offset: 0x001487D8
		private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			List<Type> list = new List<Type>(types.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(types[i], list);
			}
			return array;
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x0014A620 File Offset: 0x00148820
		private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
		{
			if (typeInfos == null)
			{
				throw new ArgumentNullException("typeInfos");
			}
			return (TraceLoggingTypeInfo[])typeInfos.Clone();
		}

		// Token: 0x040031F2 RID: 12786
		internal readonly TraceLoggingTypeInfo[] typeInfos;

		// Token: 0x040031F3 RID: 12787
		internal readonly string name;

		// Token: 0x040031F4 RID: 12788
		internal readonly EventTags tags;

		// Token: 0x040031F5 RID: 12789
		internal readonly byte level;

		// Token: 0x040031F6 RID: 12790
		internal readonly byte opcode;

		// Token: 0x040031F7 RID: 12791
		internal readonly EventKeywords keywords;

		// Token: 0x040031F8 RID: 12792
		internal readonly byte[] typeMetadata;

		// Token: 0x040031F9 RID: 12793
		internal readonly int scratchSize;

		// Token: 0x040031FA RID: 12794
		internal readonly int dataCount;

		// Token: 0x040031FB RID: 12795
		internal readonly int pinCount;

		// Token: 0x040031FC RID: 12796
		private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;
	}
}
