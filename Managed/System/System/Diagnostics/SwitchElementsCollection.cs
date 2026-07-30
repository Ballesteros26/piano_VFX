using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001BD RID: 445
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000257 RID: 599
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000027E2 File Offset: 0x000009E2
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003F48C File Offset: 0x0003D68C
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003F493 File Offset: 0x0003D693
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}
	}
}
