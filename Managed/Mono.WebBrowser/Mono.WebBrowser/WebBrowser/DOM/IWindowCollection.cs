using System;
using System.Collections;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000036 RID: 54
	public interface IWindowCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000080 RID: 128
		IWindow this[int index] { get; set; }
	}
}
