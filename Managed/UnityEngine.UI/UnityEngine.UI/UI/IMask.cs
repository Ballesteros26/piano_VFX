using System;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000015 RID: 21
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Not supported anymore.", true)]
	public interface IMask
	{
		// Token: 0x06000102 RID: 258
		bool Enabled();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259
		RectTransform rectTransform { get; }
	}
}
