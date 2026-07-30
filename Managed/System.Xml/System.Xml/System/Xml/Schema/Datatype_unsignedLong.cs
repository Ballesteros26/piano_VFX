using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E6 RID: 998
	internal class Datatype_unsignedLong : Datatype_nonNegativeInteger
	{
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000E5058 File Offset: 0x000E3258
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedLong.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x000E505F File Offset: 0x000E325F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedLong;
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000E5064 File Offset: 0x000E3264
		internal override int Compare(object value1, object value2)
		{
			return ((ulong)value1).CompareTo(value2);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000E5080 File Offset: 0x000E3280
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedLong.atomicValueType;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x000E5087 File Offset: 0x000E3287
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedLong.listValueType;
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000E5090 File Offset: 0x000E3290
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ulong num;
				ex = XmlConvert.TryToUInt64(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedLong.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A0C RID: 6668
		private static readonly Type atomicValueType = typeof(ulong);

		// Token: 0x04001A0D RID: 6669
		private static readonly Type listValueType = typeof(ulong[]);

		// Token: 0x04001A0E RID: 6670
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 18446744073709551615m);
	}
}
