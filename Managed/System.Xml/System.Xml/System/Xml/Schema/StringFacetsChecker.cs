using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x020003FB RID: 1019
	internal class StringFacetsChecker : FacetsChecker
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000E85AC File Offset: 0x000E67AC
		private static Regex LanguagePattern
		{
			get
			{
				if (StringFacetsChecker.languagePattern == null)
				{
					Regex regex = new Regex("^([a-zA-Z]{1,8})(-[a-zA-Z0-9]{1,8})*$", RegexOptions.None);
					Interlocked.CompareExchange<Regex>(ref StringFacetsChecker.languagePattern, regex, null);
				}
				return StringFacetsChecker.languagePattern;
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000E85E0 File Offset: 0x000E67E0
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			string text = datatype.ValueConverter.ToString(value);
			return this.CheckValueFacets(text, datatype, true);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000E8603 File Offset: 0x000E6803
		internal override Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return this.CheckValueFacets(value, datatype, true);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000E8610 File Offset: 0x000E6810
		internal Exception CheckValueFacets(string value, XmlSchemaDatatype datatype, bool verifyUri)
		{
			int length = value.Length;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			Exception ex = this.CheckBuiltInFacets(value, datatype.TypeCode, verifyUri);
			if (ex != null)
			{
				return ex;
			}
			if (restrictionFlags != (RestrictionFlags)0)
			{
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000E86D3 File Offset: 0x000E68D3
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToString(value), enumeration, datatype);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000E86EC File Offset: 0x000E68EC
		private bool MatchEnumeration(string value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			if (datatype.TypeCode == XmlTypeCode.AnyUri)
			{
				for (int i = 0; i < enumeration.Count; i++)
				{
					if (value.Equals(((Uri)enumeration[i]).OriginalString))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < enumeration.Count; j++)
				{
					if (value.Equals((string)enumeration[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000E875C File Offset: 0x000E695C
		private Exception CheckBuiltInFacets(string s, XmlTypeCode typeCode, bool verifyUri)
		{
			Exception ex = null;
			switch (typeCode)
			{
			case XmlTypeCode.AnyUri:
				if (verifyUri)
				{
					Uri uri;
					ex = XmlConvert.TryToUri(s, out uri);
				}
				break;
			case XmlTypeCode.NormalizedString:
				ex = XmlConvert.TryVerifyNormalizedString(s);
				break;
			case XmlTypeCode.Token:
				ex = XmlConvert.TryVerifyTOKEN(s);
				break;
			case XmlTypeCode.Language:
				if (s == null || s.Length == 0)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				if (!StringFacetsChecker.LanguagePattern.IsMatch(s))
				{
					return new XmlSchemaException("'{0}' is an invalid language identifier.", string.Empty);
				}
				break;
			case XmlTypeCode.NmToken:
				ex = XmlConvert.TryVerifyNMTOKEN(s);
				break;
			case XmlTypeCode.Name:
				ex = XmlConvert.TryVerifyName(s);
				break;
			case XmlTypeCode.NCName:
			case XmlTypeCode.Id:
			case XmlTypeCode.Idref:
			case XmlTypeCode.Entity:
				ex = XmlConvert.TryVerifyNCName(s);
				break;
			}
			return ex;
		}

		// Token: 0x04001A38 RID: 6712
		private static Regex languagePattern;
	}
}
