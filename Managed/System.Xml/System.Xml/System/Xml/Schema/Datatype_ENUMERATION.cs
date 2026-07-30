using System;

namespace System.Xml.Schema
{
	// Token: 0x020003EE RID: 1006
	internal class Datatype_ENUMERATION : Datatype_NMTOKEN
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x000735E6 File Offset: 0x000717E6
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENUMERATION;
			}
		}
	}
}
