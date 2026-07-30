using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003FC RID: 1020
	internal class QNameFacetsChecker : FacetsChecker
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x000E8818 File Offset: 0x000E6A18
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName));
			return this.CheckValueFacets(xmlQualifiedName, datatype);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000E884C File Offset: 0x000E6A4C
		internal override Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if (restrictionFlags != (RestrictionFlags)0)
			{
				int length = value.ToString().Length;
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000E88FF File Offset: 0x000E6AFF
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName)), enumeration);
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000E8924 File Offset: 0x000E6B24
		private bool MatchEnumeration(XmlQualifiedName value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value.Equals((XmlQualifiedName)enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
