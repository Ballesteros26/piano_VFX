using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	// Token: 0x02000008 RID: 8
	public class FileBrowserItem : ListItem, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003D4B File Offset: 0x00001F4B
		public RectTransform TransformComponent
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = (RectTransform)base.transform;
				}
				return this.m_transform;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003D72 File Offset: 0x00001F72
		public string Name
		{
			get
			{
				return this.nameText.text;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003D7F File Offset: 0x00001F7F
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003D87 File Offset: 0x00001F87
		public bool IsDirectory { get; private set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003D90 File Offset: 0x00001F90
		public void SetFileBrowser(FileBrowser fileBrowser)
		{
			this.fileBrowser = fileBrowser;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D99 File Offset: 0x00001F99
		public void SetFile(Sprite icon, string name, bool isDirectory)
		{
			this.icon.sprite = icon;
			this.nameText.text = name;
			this.IsDirectory = isDirectory;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003DBC File Offset: 0x00001FBC
		public void OnPointerClick(PointerEventData eventData)
		{
			if (FileBrowser.SingleClickMode)
			{
				this.fileBrowser.OnItemSelected(this);
				this.fileBrowser.OnItemOpened(this);
				return;
			}
			if (Time.realtimeSinceStartup - this.prevTouchTime < 0.5f)
			{
				if (this.fileBrowser.SelectedFilePosition == base.Position)
				{
					this.fileBrowser.OnItemOpened(this);
				}
				this.prevTouchTime = float.NegativeInfinity;
				return;
			}
			this.fileBrowser.OnItemSelected(this);
			this.prevTouchTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003E3E File Offset: 0x0000203E
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.fileBrowser.SelectedFilePosition != base.Position)
			{
				this.background.color = this.fileBrowser.hoveredFileColor;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E69 File Offset: 0x00002069
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.fileBrowser.SelectedFilePosition != base.Position)
			{
				this.background.color = this.fileBrowser.normalFileColor;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E94 File Offset: 0x00002094
		public void Select()
		{
			this.background.color = this.fileBrowser.selectedFileColor;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003EAC File Offset: 0x000020AC
		public void Deselect()
		{
			this.background.color = this.fileBrowser.normalFileColor;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003EC4 File Offset: 0x000020C4
		public void SetHidden(bool isHidden)
		{
			Color color = this.icon.color;
			color.a = (isHidden ? 0.5f : 1f);
			this.icon.color = color;
			color = this.nameText.color;
			color.a = (isHidden ? 0.55f : 1f);
			this.nameText.color = color;
		}

		// Token: 0x0400004E RID: 78
		private const float DOUBLE_CLICK_TIME = 0.5f;

		// Token: 0x0400004F RID: 79
		protected FileBrowser fileBrowser;

		// Token: 0x04000050 RID: 80
		[SerializeField]
		private Image background;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private Image icon;

		// Token: 0x04000052 RID: 82
		[SerializeField]
		private Text nameText;

		// Token: 0x04000053 RID: 83
		private float prevTouchTime = float.NegativeInfinity;

		// Token: 0x04000054 RID: 84
		private RectTransform m_transform;
	}
}
