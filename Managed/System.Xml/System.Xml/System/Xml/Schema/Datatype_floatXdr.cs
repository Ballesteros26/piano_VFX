using System;

namespace System.Xml.Schema
{
	// Token: 0x020003EC RID: 1004
	internal class Datatype_floatXdr : Datatype_float
	{
		// Token: 0x0600271F RID: 10015 RVA: 0x000E53F8 File Offset: 0x000E35F8
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			float num;
			try
			{
				num = XmlConvert.ToSingle(s);
			}
			catch (Exception ex)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex);
			}
			if (float.IsInfinity(num) || float.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}
	}
}
