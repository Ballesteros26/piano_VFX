using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CF RID: 975
	internal class Datatype_base64Binary : Datatype_anySimpleType
	{
		// Token: 0x0600267A RID: 9850 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000E47DA File Offset: 0x000E29DA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000E4890 File Offset: 0x000E2A90
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000E4894 File Offset: 0x000E2A94
		public override Type ValueType
		{
			get
			{
				return Datatype_base64Binary.atomicValueType;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x000E489B File Offset: 0x000E2A9B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_base64Binary.listValueType;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000E47F3 File Offset: 0x000E29F3
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000E48A4 File Offset: 0x000E2AA4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = Convert.FromBase64String(s);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (FormatException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.binaryFacetsChecker.CheckValueFacets(array, this);
				if (ex == null)
				{
					typedValue = array;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x040019F5 RID: 6645
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x040019F6 RID: 6646
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
