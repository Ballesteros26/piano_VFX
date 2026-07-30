using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D9 RID: 985
	internal class Datatype_NCName : Datatype_Name
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000E4AB8 File Offset: 0x000E2CB8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NCName;
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000E4ABC File Offset: 0x000E2CBC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					nameTable.Add(s);
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}
