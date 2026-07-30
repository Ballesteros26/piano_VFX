using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x0200002B RID: 43
	public interface IHistory
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000115 RID: 277
		int Count { get; }

		// Token: 0x06000116 RID: 278
		void Back(int count);

		// Token: 0x06000117 RID: 279
		void Forward(int count);

		// Token: 0x06000118 RID: 280
		void GoToIndex(int index);

		// Token: 0x06000119 RID: 281
		void GoToUrl(string url);
	}
}
