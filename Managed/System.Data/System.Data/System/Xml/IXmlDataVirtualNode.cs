using System;
using System.Data;

namespace System.Xml
{
	// Token: 0x0200002E RID: 46
	internal interface IXmlDataVirtualNode
	{
		// Token: 0x0600012F RID: 303
		bool IsOnNode(XmlNode nodeToCheck);

		// Token: 0x06000130 RID: 304
		bool IsOnColumn(DataColumn col);

		// Token: 0x06000131 RID: 305
		bool IsInUse();

		// Token: 0x06000132 RID: 306
		void OnFoliated(XmlNode foliatedNode);
	}
}
