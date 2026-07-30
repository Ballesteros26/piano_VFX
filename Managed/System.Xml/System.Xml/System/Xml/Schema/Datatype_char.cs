using System;

namespace System.Xml.Schema
{
	// Token: 0x020003EF RID: 1007
	internal class Datatype_char : Datatype_anySimpleType
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000E551E File Offset: 0x000E371E
		public override Type ValueType
		{
			get
			{
				return Datatype_char.atomicValueType;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000E5525 File Offset: 0x000E3725
		internal override Type ListValueType
		{
			get
			{
				return Datatype_char.listValueType;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0000226C File Offset: 0x0000046C
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000E552C File Offset: 0x000E372C
		internal override int Compare(object value1, object value2)
		{
			return ((char)value1).CompareTo(value2);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000E5548 File Offset: 0x000E3748
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			try
			{
				obj = XmlConvert.ToChar(s);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex2);
			}
			return obj;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000E55A0 File Offset: 0x000E37A0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			char c;
			Exception ex = XmlConvert.TryToChar(s, out c);
			if (ex == null)
			{
				typedValue = c;
				return null;
			}
			return ex;
		}

		// Token: 0x04001A1B RID: 6683
		private static readonly Type atomicValueType = typeof(char);

		// Token: 0x04001A1C RID: 6684
		private static readonly Type listValueType = typeof(char[]);
	}
}
