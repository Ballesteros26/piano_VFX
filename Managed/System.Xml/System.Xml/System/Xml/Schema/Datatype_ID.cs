using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DA RID: 986
	internal class Datatype_ID : Datatype_NCName
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000E4B05 File Offset: 0x000E2D05
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x00003242 File Offset: 0x00001442
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}
	}
}
