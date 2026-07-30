using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000035 RID: 53
	public interface IWindow
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016F RID: 367
		IDocument Document { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000170 RID: 368
		IWindowCollection Frames { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000172 RID: 370
		string Name { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000173 RID: 371
		IWindow Parent { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000174 RID: 372
		string StatusText { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000175 RID: 373
		IWindow Top { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000176 RID: 374
		IHistory History { get; }

		// Token: 0x06000177 RID: 375
		void AttachEventHandler(string eventName, EventHandler handler);

		// Token: 0x06000178 RID: 376
		void DetachEventHandler(string eventName, EventHandler handler);

		// Token: 0x06000179 RID: 377
		void Focus();

		// Token: 0x0600017A RID: 378
		bool Equals(object obj);

		// Token: 0x0600017B RID: 379
		int GetHashCode();

		// Token: 0x0600017C RID: 380
		void Open(string url);

		// Token: 0x0600017D RID: 381
		void ScrollTo(int x, int y);

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600017E RID: 382
		// (remove) Token: 0x0600017F RID: 383
		event EventHandler Load;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000180 RID: 384
		// (remove) Token: 0x06000181 RID: 385
		event EventHandler Unload;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000182 RID: 386
		// (remove) Token: 0x06000183 RID: 387
		event EventHandler OnFocus;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000184 RID: 388
		// (remove) Token: 0x06000185 RID: 389
		event EventHandler OnBlur;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000186 RID: 390
		// (remove) Token: 0x06000187 RID: 391
		event EventHandler Error;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000188 RID: 392
		// (remove) Token: 0x06000189 RID: 393
		event EventHandler Scroll;
	}
}
