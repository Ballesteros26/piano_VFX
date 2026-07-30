using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000025 RID: 37
	public interface IAttributeCollection : INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x17000027 RID: 39
		IAttribute this[string name] { get; }

		// Token: 0x060000B5 RID: 181
		bool Exists(string name);

		// Token: 0x060000B6 RID: 182
		int GetHashCode();
	}
}
