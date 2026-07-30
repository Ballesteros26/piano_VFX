using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x0200002E RID: 46
	public interface INavigation
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000122 RID: 290
		bool CanGoBack { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000123 RID: 291
		bool CanGoForward { get; }

		// Token: 0x06000124 RID: 292
		bool Back();

		// Token: 0x06000125 RID: 293
		bool Forward();

		// Token: 0x06000126 RID: 294
		void Home();

		// Token: 0x06000127 RID: 295
		void Reload();

		// Token: 0x06000128 RID: 296
		void Reload(ReloadOption option);

		// Token: 0x06000129 RID: 297
		void Stop();

		// Token: 0x0600012A RID: 298
		void Go(int index);

		// Token: 0x0600012B RID: 299
		void Go(int index, bool relative);

		// Token: 0x0600012C RID: 300
		void Go(string url);

		// Token: 0x0600012D RID: 301
		void Go(string url, LoadFlags flags);

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012E RID: 302
		int HistoryCount { get; }
	}
}
