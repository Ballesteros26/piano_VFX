using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E8 RID: 1000
	internal class Datatype_unsignedShort : Datatype_unsignedInt
	{
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000E51DC File Offset: 0x000E33DC
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedShort.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000E51E3 File Offset: 0x000E33E3
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedShort;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000E51E8 File Offset: 0x000E33E8
		internal override int Compare(object value1, object value2)
		{
			return ((ushort)value1).CompareTo(value2);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000E5204 File Offset: 0x000E3404
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedShort.atomicValueType;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000E520B File Offset: 0x000E340B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedShort.listValueType;
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000E5214 File Offset: 0x000E3414
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ushort num;
				ex = XmlConvert.TryToUInt16(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedShort.numeric10FacetsChecker.CheckValueFacets((int)num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A12 RID: 6674
		private static readonly Type atomicValueType = typeof(ushort);

		// Token: 0x04001A13 RID: 6675
		private static readonly Type listValueType = typeof(ushort[]);

		// Token: 0x04001A14 RID: 6676
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 65535m);
	}
}
