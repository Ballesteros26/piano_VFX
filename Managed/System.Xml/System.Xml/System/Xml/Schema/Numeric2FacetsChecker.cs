using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003F8 RID: 1016
	internal class Numeric2FacetsChecker : FacetsChecker
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x000E8118 File Offset: 0x000E6318
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			double num = datatype.ValueConverter.ToDouble(value);
			return this.CheckValueFacets(num, datatype);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000E813C File Offset: 0x000E633C
		internal override Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDouble(restriction.MaxInclusive))
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDouble(restriction.MaxExclusive))
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDouble(restriction.MinInclusive))
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDouble(restriction.MinExclusive))
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000E8228 File Offset: 0x000E6428
		internal override Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			double num = (double)value;
			return this.CheckValueFacets(num, datatype);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000E8240 File Offset: 0x000E6440
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDouble(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000E825C File Offset: 0x000E645C
		private bool MatchEnumeration(double value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDouble(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
