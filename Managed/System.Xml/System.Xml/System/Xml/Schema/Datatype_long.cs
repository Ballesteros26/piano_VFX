using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E1 RID: 993
	internal class Datatype_long : Datatype_integer
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000E4D0C File Offset: 0x000E2F0C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_long.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000E4D13 File Offset: 0x000E2F13
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Long;
			}
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000E4D18 File Offset: 0x000E2F18
		internal override int Compare(object value1, object value2)
		{
			return ((long)value1).CompareTo(value2);
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000E4D34 File Offset: 0x000E2F34
		public override Type ValueType
		{
			get
			{
				return Datatype_long.atomicValueType;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x000E4D3B File Offset: 0x000E2F3B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_long.listValueType;
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000E4D44 File Offset: 0x000E2F44
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_long.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				long num;
				ex = XmlConvert.TryToInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_long.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x040019FF RID: 6655
		private static readonly Type atomicValueType = typeof(long);

		// Token: 0x04001A00 RID: 6656
		private static readonly Type listValueType = typeof(long[]);

		// Token: 0x04001A01 RID: 6657
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-9223372036854775808m, 9223372036854775807m);
	}
}
