using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000034 RID: 52
	public interface IStylesheetList : IEnumerable
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600016C RID: 364
		int Count { get; }

		// Token: 0x17000078 RID: 120
		IStylesheet this[int index] { get; set; }
	}
}
