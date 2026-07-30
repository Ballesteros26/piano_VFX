using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003F9 RID: 1017
	internal class DurationFacetsChecker : FacetsChecker
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x000E8298 File Offset: 0x000E6498
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			TimeSpan timeSpan = (TimeSpan)datatype.ValueConverter.ChangeType(value, typeof(TimeSpan));
			return this.CheckValueFacets(timeSpan, datatype);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000E82CC File Offset: 0x000E64CC
		internal override Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000E83C4 File Offset: 0x000E65C4
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((TimeSpan)value, enumeration);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000E83D4 File Offset: 0x000E65D4
		private bool MatchEnumeration(TimeSpan value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (TimeSpan.Compare(value, (TimeSpan)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
