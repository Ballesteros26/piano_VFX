using System;

namespace System.Xml.Schema
{
	// Token: 0x020003DE RID: 990
	internal class Datatype_integer : Datatype_decimal
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000E41CF File Offset: 0x000E23CF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Integer;
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000E4C5C File Offset: 0x000E2E5C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = this.FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToInteger(s, out num);
				if (ex == null)
				{
					ex = this.FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}
	}
}
