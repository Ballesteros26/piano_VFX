using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003FA RID: 1018
	internal class DateTimeFacetsChecker : FacetsChecker
	{
		// Token: 0x0600279A RID: 10138 RVA: 0x000E840C File Offset: 0x000E660C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			DateTime dateTime = datatype.ValueConverter.ToDateTime(value);
			return this.CheckValueFacets(dateTime, datatype);
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000E8430 File Offset: 0x000E6630
		internal override Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000E8555 File Offset: 0x000E6755
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDateTime(value), enumeration, datatype);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000E856C File Offset: 0x000E676C
		private bool MatchEnumeration(DateTime value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (DateTime)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
