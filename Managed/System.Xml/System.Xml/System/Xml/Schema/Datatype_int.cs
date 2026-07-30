using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E2 RID: 994
	internal class Datatype_int : Datatype_long
	{
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x000E4DE1 File Offset: 0x000E2FE1
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_int.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000E4DE8 File Offset: 0x000E2FE8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Int;
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000E4DEC File Offset: 0x000E2FEC
		internal override int Compare(object value1, object value2)
		{
			return ((int)value1).CompareTo(value2);
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000E4E08 File Offset: 0x000E3008
		public override Type ValueType
		{
			get
			{
				return Datatype_int.atomicValueType;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000E4E0F File Offset: 0x000E300F
		internal override Type ListValueType
		{
			get
			{
				return Datatype_int.listValueType;
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000E4E18 File Offset: 0x000E3018
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_int.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				int num;
				ex = XmlConvert.TryToInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_int.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A02 RID: 6658
		private static readonly Type atomicValueType = typeof(int);

		// Token: 0x04001A03 RID: 6659
		private static readonly Type listValueType = typeof(int[]);

		// Token: 0x04001A04 RID: 6660
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(-2147483648m, 2147483647m);
	}
}
