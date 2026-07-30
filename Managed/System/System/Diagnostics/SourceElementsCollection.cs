using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001B6 RID: 438
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000242 RID: 578
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0003E98F File Offset: 0x0003CB8F
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00004240 File Offset: 0x00002440
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003E996 File Offset: 0x0003CB96
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}
	}
}
