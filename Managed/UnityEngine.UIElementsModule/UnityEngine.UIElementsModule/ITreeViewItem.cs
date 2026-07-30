using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010B RID: 267
	internal interface ITreeViewItem
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000808 RID: 2056
		int id { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000809 RID: 2057
		ITreeViewItem parent { get; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600080A RID: 2058
		IEnumerable<ITreeViewItem> children { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600080B RID: 2059
		bool hasChildren { get; }

		// Token: 0x0600080C RID: 2060
		void AddChild(ITreeViewItem child);

		// Token: 0x0600080D RID: 2061
		void AddChildren(IList<ITreeViewItem> children);

		// Token: 0x0600080E RID: 2062
		void RemoveChild(ITreeViewItem child);
	}
}
