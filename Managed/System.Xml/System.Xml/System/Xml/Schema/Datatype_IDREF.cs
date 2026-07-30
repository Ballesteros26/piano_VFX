using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DB RID: 987
	internal class Datatype_IDREF : Datatype_NCName
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000E4B11 File Offset: 0x000E2D11
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000026AE File Offset: 0x000008AE
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}
	}
}
