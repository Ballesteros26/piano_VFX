using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x0200002A RID: 42
	public interface IElementCollection : INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x1700005B RID: 91
		IElement this[int index] { get; set; }

		// Token: 0x06000114 RID: 276
		int GetHashCode();
	}
}
