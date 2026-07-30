using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E3 RID: 995
	internal class Datatype_short : Datatype_int
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000E4EA8 File Offset: 0x000E30A8
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_short.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000E4EAF File Offset: 0x000E30AF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Short;
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000E4EB4 File Offset: 0x000E30B4
		internal override int Compare(object value1, object value2)
		{
			return ((short)value1).CompareTo(value2);
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000E4ED0 File Offset: 0x000E30D0
		public override Type ValueType
		{
			get
			{
				return Datatype_short.atomicValueType;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000E4ED7 File Offset: 0x000E30D7
		internal override Type ListValueType
		{
			get
			{
				return Datatype_short.listValueType;
			}
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000E4EE0 File Offset: 0x000E30E0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_short.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				short num;
				ex = XmlConvert.TryToInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_short.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A05 RID: 6661
		private static readonly Type atomicValueType = typeof(short);

		// Token: 0x04001A06 RID: 6662
		private static readonly Type listValueType = typeof(short[]);

		// Token: 0x04001A07 RID: 6663
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-32768m, 32767m);
	}
}
