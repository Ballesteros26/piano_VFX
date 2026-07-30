using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x0200002D RID: 45
	public interface INamedNodeMap : IList, ICollection, IEnumerable
	{
		// Token: 0x1700005D RID: 93
		INode this[string name] { get; set; }

		// Token: 0x0600011C RID: 284
		INode RemoveNamedItem(string name);

		// Token: 0x1700005E RID: 94
		INode this[int index] { get; set; }

		// Token: 0x1700005F RID: 95
		INode this[string namespaceURI, string localName] { get; set; }

		// Token: 0x06000121 RID: 289
		INode RemoveNamedItemNS(string namespaceURI, string localName);
	}
}
