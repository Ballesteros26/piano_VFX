using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000400 RID: 1024
	internal class UnionFacetsChecker : FacetsChecker
	{
		// Token: 0x060027B5 RID: 10165 RVA: 0x000E8B58 File Offset: 0x000E6D58
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			if ((((restriction != null && restriction.Flags != (RestrictionFlags)0) ? 1 : 0) & 16) != 0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000E8BA0 File Offset: 0x000E6DA0
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
