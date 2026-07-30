using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003FE RID: 1022
	internal class BinaryFacetsChecker : FacetsChecker
	{
		// Token: 0x060027AD RID: 10157 RVA: 0x000E895C File Offset: 0x000E6B5C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			byte[] array = (byte[])value;
			return this.CheckValueFacets(array, datatype);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000E8978 File Offset: 0x000E6B78
		internal override Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			int num = value.Length;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if (restrictionFlags != (RestrictionFlags)0)
			{
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != num)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && num < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < num)
				{
					return new XmlSchemaException("The actual length is greater than the MaxLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000E8A24 File Offset: 0x000E6C24
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((byte[])value, enumeration, datatype);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000E8A34 File Offset: 0x000E6C34
		private bool MatchEnumeration(byte[] value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (byte[])enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
