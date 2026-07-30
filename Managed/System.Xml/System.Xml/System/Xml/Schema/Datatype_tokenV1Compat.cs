using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D5 RID: 981
	internal class Datatype_tokenV1Compat : Datatype_normalizedStringV1Compat
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000E4A94 File Offset: 0x000E2C94
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}
	}
}
