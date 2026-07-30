using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DC RID: 988
	internal class Datatype_ENTITY : Datatype_NCName
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000E4B15 File Offset: 0x000E2D15
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x00004107 File Offset: 0x00002307
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}
	}
}
