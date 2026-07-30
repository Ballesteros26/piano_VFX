using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000300 RID: 768
	internal interface INameScope
	{
		// Token: 0x170005B5 RID: 1461
		object this[string name, string ns] { get; set; }
	}
}
