using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D2 RID: 978
	internal class Datatype_normalizedString : Datatype_string
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000E4A88 File Offset: 0x000E2C88
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x00003242 File Offset: 0x00001442
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Replace;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
