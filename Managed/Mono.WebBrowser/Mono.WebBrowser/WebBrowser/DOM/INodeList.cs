using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000032 RID: 50
	public interface INodeList : IList, ICollection, IEnumerable
	{
		// Token: 0x1700006F RID: 111
		INode this[int index] { get; set; }

		// Token: 0x06000163 RID: 355
		int GetHashCode();
	}
}
