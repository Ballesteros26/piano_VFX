using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013C RID: 316
	internal class History : DOMObject, IHistory
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x00007FFB File Offset: 0x000061FB
		public History(WebBrowser control, Navigation navigation)
			: base(control)
		{
			this.navigation = navigation;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000800B File Offset: 0x0000620B
		public int Count
		{
			get
			{
				return this.navigation.HistoryCount;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00008018 File Offset: 0x00006218
		public void Back(int count)
		{
			this.navigation.Go(count * -1, true);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00008029 File Offset: 0x00006229
		public void Forward(int count)
		{
			this.navigation.Go(count, true);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00008038 File Offset: 0x00006238
		public void GoToIndex(int index)
		{
			this.navigation.Go(index);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00008048 File Offset: 0x00006248
		public void GoToUrl(string url)
		{
			int num = -1;
			nsISHistory nsISHistory;
			this.navigation.navigation.getSessionHistory(out nsISHistory);
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				nsIHistoryEntry nsIHistoryEntry;
				nsISHistory.getEntryAtIndex(i, false, out nsIHistoryEntry);
				nsIURI nsIURI;
				nsIHistoryEntry.getURI(out nsIURI);
				AsciiString asciiString = new AsciiString(string.Empty);
				nsIURI.getSpec(asciiString.Handle);
				if (string.Compare(asciiString.ToString(), url, true) == 0)
				{
					num = i;
					break;
				}
			}
			if (num > -1)
			{
				this.GoToIndex(num);
			}
		}

		// Token: 0x04000121 RID: 289
		private Navigation navigation;
	}
}
