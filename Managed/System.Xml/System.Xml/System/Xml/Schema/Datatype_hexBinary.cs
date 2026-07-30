using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CE RID: 974
	internal class Datatype_hexBinary : Datatype_anySimpleType
	{
		// Token: 0x0600266F RID: 9839 RVA: 0x000E4458 File Offset: 0x000E2658
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000E47DA File Offset: 0x000E29DA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x000E47E1 File Offset: 0x000E29E1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000E47E5 File Offset: 0x000E29E5
		public override Type ValueType
		{
			get
			{
				return Datatype_hexBinary.atomicValueType;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000E47EC File Offset: 0x000E29EC
		internal override Type ListValueType
		{
			get
			{
				return Datatype_hexBinary.listValueType;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000E47F3 File Offset: 0x000E29F3
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000E4808 File Offset: 0x000E2A08
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = XmlConvert.FromBinHexString(s, false);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
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

		// Token: 0x040019F3 RID: 6643
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x040019F4 RID: 6644
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
