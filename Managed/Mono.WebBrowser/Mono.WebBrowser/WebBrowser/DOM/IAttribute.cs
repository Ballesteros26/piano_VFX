using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000024 RID: 36
	public interface IAttribute : INode
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B2 RID: 178
		string Name { get; }

		// Token: 0x060000B3 RID: 179
		int GetHashCode();
	}
}
