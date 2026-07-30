using System;

namespace System.Xml.Schema
{
	// Token: 0x020003EB RID: 1003
	internal class Datatype_doubleXdr : Datatype_double
	{
		// Token: 0x0600271D RID: 10013 RVA: 0x000E538C File Offset: 0x000E358C
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			double num;
			try
			{
				num = XmlConvert.ToDouble(s);
			}
			catch (Exception ex)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex);
			}
			if (double.IsInfinity(num) || double.IsNaN(num))
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its data type.", s);
			}
			return num;
		}
	}
}
