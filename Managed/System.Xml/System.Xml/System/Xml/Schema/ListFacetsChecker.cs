using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003FF RID: 1023
	internal class ListFacetsChecker : FacetsChecker
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x000E8A6C File Offset: 0x000E6C6C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			Array array = value as Array;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if ((restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength)) != (RestrictionFlags)0)
			{
				int length = array.Length;
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != length)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && length < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < length)
				{
					return new XmlSchemaException("The actual length is greater than the MaxLength value.", string.Empty);
				}
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000E8B24 File Offset: 0x000E6D24
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
