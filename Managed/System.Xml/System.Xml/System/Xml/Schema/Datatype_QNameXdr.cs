using System;

namespace System.Xml.Schema
{
	// Token: 0x020003ED RID: 1005
	internal class Datatype_QNameXdr : Datatype_anySimpleType
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000E5464 File Offset: 0x000E3664
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (s == null || s.Length == 0)
			{
				throw new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			if (nsmgr == null)
			{
				throw new ArgumentNullException("nsmgr");
			}
			object obj;
			try
			{
				string text;
				obj = XmlQualifiedName.Parse(s.Trim(), nsmgr, out text);
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

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000E54E8 File Offset: 0x000E36E8
		public override Type ValueType
		{
			get
			{
				return Datatype_QNameXdr.atomicValueType;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000E54EF File Offset: 0x000E36EF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QNameXdr.listValueType;
			}
		}

		// Token: 0x04001A19 RID: 6681
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04001A1A RID: 6682
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
