using System;
using UnityEngine;

namespace SimpleFileBrowser
{
	// Token: 0x0200000D RID: 13
	[RequireComponent(typeof(RectTransform))]
	public class ListItem : MonoBehaviour
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000041B7 File Offset: 0x000023B7
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000041BF File Offset: 0x000023BF
		public object Tag { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000041C8 File Offset: 0x000023C8
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000041D0 File Offset: 0x000023D0
		public int Position { get; set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x000041D9 File Offset: 0x000023D9
		internal void SetAdapter(IListViewAdapter listView)
		{
			this.adapter = listView;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000041E2 File Offset: 0x000023E2
		public void OnClick()
		{
			if (this.adapter.OnItemClicked != null)
			{
				this.adapter.OnItemClicked(this);
			}
		}

		// Token: 0x04000061 RID: 97
		private IListViewAdapter adapter;
	}
}
