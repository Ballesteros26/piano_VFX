using System;

namespace System.Xml
{
	// Token: 0x02000036 RID: 54
	internal sealed class XmlDataImplementation : XmlImplementation
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000D004 File Offset: 0x0000B204
		public override XmlDocument CreateDocument()
		{
			return new XmlDataDocument(this);
		}
	}
}
