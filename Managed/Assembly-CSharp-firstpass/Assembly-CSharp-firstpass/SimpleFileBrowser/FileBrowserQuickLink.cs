using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleFileBrowser
{
	// Token: 0x0200000A RID: 10
	public class FileBrowserQuickLink : FileBrowserItem, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004187 File Offset: 0x00002387
		public string TargetPath
		{
			get
			{
				return this.m_targetPath;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000418F File Offset: 0x0000238F
		public void SetQuickLink(Sprite icon, string name, string targetPath)
		{
			base.SetFile(icon, name, true);
			this.m_targetPath = targetPath;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000041A1 File Offset: 0x000023A1
		public new void OnPointerClick(PointerEventData eventData)
		{
			this.fileBrowser.OnQuickLinkSelected(this);
		}

		// Token: 0x0400005E RID: 94
		private string m_targetPath;
	}
}
