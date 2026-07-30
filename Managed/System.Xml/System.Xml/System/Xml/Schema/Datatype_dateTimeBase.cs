using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C1 RID: 961
	internal class Datatype_dateTimeBase : Datatype_anySimpleType
	{
		// Token: 0x06002650 RID: 9808 RVA: 0x000E4614 File Offset: 0x000E2814
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlDateTimeConverter.Create(schemaType);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000E461C File Offset: 0x000E281C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.dateTimeFacetsChecker;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000E4623 File Offset: 0x000E2823
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000E4160 File Offset: 0x000E2360
		internal Datatype_dateTimeBase()
		{
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000E4627 File Offset: 0x000E2827
		internal Datatype_dateTimeBase(XsdDateTimeFlags dateTimeFlags)
		{
			this.dateTimeFlags = dateTimeFlags;
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000E4636 File Offset: 0x000E2836
		public override Type ValueType
		{
			get
			{
				return Datatype_dateTimeBase.atomicValueType;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000E463D File Offset: 0x000E283D
		internal override Type ListValueType
		{
			get
			{
				return Datatype_dateTimeBase.listValueType;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000026AE File Offset: 0x000008AE
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000E426B File Offset: 0x000E246B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000E4644 File Offset: 0x000E2844
		internal override int Compare(object value1, object value2)
		{
			DateTime dateTime = (DateTime)value1;
			DateTime dateTime2 = (DateTime)value2;
			if (dateTime.Kind == DateTimeKind.Unspecified || dateTime2.Kind == DateTimeKind.Unspecified)
			{
				return dateTime.CompareTo(dateTime2);
			}
			return dateTime.ToUniversalTime().CompareTo(dateTime2.ToUniversalTime());
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000E4690 File Offset: 0x000E2890
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.dateTimeFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDateTime xsdDateTime;
				if (!XsdDateTime.TryParse(s, this.dateTimeFlags, out xsdDateTime))
				{
					ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
					{
						s,
						this.dateTimeFlags.ToString()
					}));
				}
				else
				{
					DateTime dateTime = DateTime.MinValue;
					try
					{
						dateTime = xsdDateTime;
					}
					catch (ArgumentException ex)
					{
						return ex;
					}
					ex = DatatypeImplementation.dateTimeFacetsChecker.CheckValueFacets(dateTime, this);
					if (ex == null)
					{
						typedValue = dateTime;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019F0 RID: 6640
		private static readonly Type atomicValueType = typeof(DateTime);

		// Token: 0x040019F1 RID: 6641
		private static readonly Type listValueType = typeof(DateTime[]);

		// Token: 0x040019F2 RID: 6642
		private XsdDateTimeFlags dateTimeFlags;
	}
}
