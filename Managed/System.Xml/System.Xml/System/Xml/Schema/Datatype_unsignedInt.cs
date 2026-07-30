using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E7 RID: 999
	internal class Datatype_unsignedInt : Datatype_unsignedLong
	{
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000E511D File Offset: 0x000E331D
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedInt.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x000E5124 File Offset: 0x000E3324
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedInt;
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000E5128 File Offset: 0x000E3328
		internal override int Compare(object value1, object value2)
		{
			return ((uint)value1).CompareTo(value2);
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x000E5144 File Offset: 0x000E3344
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedInt.atomicValueType;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000E514B File Offset: 0x000E334B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedInt.listValueType;
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000E5154 File Offset: 0x000E3354
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				uint num;
				ex = XmlConvert.TryToUInt32(s, out num);
				if (ex == null)
				{
					ex = Datatype_unsignedInt.numeric10FacetsChecker.CheckValueFacets((long)((ulong)num), this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A0F RID: 6671
		private static readonly Type atomicValueType = typeof(uint);

		// Token: 0x04001A10 RID: 6672
		private static readonly Type listValueType = typeof(uint[]);

		// Token: 0x04001A11 RID: 6673
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 4294967295m);
	}
}
