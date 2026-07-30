using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E4 RID: 996
	internal class Datatype_byte : Datatype_short
	{
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x000E4F70 File Offset: 0x000E3170
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_byte.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x000E4F77 File Offset: 0x000E3177
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Byte;
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000E4F7C File Offset: 0x000E317C
		internal override int Compare(object value1, object value2)
		{
			return ((sbyte)value1).CompareTo(value2);
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000E4F98 File Offset: 0x000E3198
		public override Type ValueType
		{
			get
			{
				return Datatype_byte.atomicValueType;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000E4F9F File Offset: 0x000E319F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_byte.listValueType;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000E4FA8 File Offset: 0x000E31A8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_byte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				sbyte b;
				ex = XmlConvert.TryToSByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_byte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A08 RID: 6664
		private static readonly Type atomicValueType = typeof(sbyte);

		// Token: 0x04001A09 RID: 6665
		private static readonly Type listValueType = typeof(sbyte[]);

		// Token: 0x04001A0A RID: 6666
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-128m, 127m);
	}
}
