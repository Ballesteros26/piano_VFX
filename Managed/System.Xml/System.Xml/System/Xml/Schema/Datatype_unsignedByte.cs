using System;

namespace System.Xml.Schema
{
	// Token: 0x020003E9 RID: 1001
	internal class Datatype_unsignedByte : Datatype_unsignedShort
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000E529F File Offset: 0x000E349F
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_unsignedByte.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x000E52A6 File Offset: 0x000E34A6
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000E52AC File Offset: 0x000E34AC
		internal override int Compare(object value1, object value2)
		{
			return ((byte)value1).CompareTo(value2);
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x000E52C8 File Offset: 0x000E34C8
		public override Type ValueType
		{
			get
			{
				return Datatype_unsignedByte.atomicValueType;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000E52CF File Offset: 0x000E34CF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_unsignedByte.listValueType;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000E52D8 File Offset: 0x000E34D8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte b;
				ex = XmlConvert.TryToByte(s, out b);
				if (ex == null)
				{
					ex = Datatype_unsignedByte.numeric10FacetsChecker.CheckValueFacets((short)b, this);
					if (ex == null)
					{
						typedValue = b;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04001A15 RID: 6677
		private static readonly Type atomicValueType = typeof(byte);

		// Token: 0x04001A16 RID: 6678
		private static readonly Type listValueType = typeof(byte[]);

		// Token: 0x04001A17 RID: 6679
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, 255m);
	}
}
