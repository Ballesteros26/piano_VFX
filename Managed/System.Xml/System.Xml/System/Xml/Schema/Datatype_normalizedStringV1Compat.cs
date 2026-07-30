using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D3 RID: 979
	internal class Datatype_normalizedStringV1Compat : Datatype_string
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000E4A88 File Offset: 0x000E2C88
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
