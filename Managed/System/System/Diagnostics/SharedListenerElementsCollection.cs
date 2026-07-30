using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001B2 RID: 434
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00004240 File Offset: 0x00002440
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003E1C4 File Offset: 0x0003C3C4
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0003E1CC File Offset: 0x0003C3CC
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}
