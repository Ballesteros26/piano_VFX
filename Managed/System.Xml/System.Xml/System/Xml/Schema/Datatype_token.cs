using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D4 RID: 980
	internal class Datatype_token : Datatype_normalizedString
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000E4A94 File Offset: 0x000E2C94
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}
	}
}
