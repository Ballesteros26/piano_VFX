using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000AE RID: 174
	[ExecuteAlways]
	public class UIFoldout : Toggle
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00010ECD File Offset: 0x0000F0CD
		protected override void Start()
		{
			base.Start();
			this.onValueChanged.AddListener(new UnityAction<bool>(this.SetState));
			this.SetState(base.isOn);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		private void OnValidate()
		{
			this.SetState(base.isOn, false);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00010F07 File Offset: 0x0000F107
		public void SetState(bool state)
		{
			this.SetState(state, true);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00010F14 File Offset: 0x0000F114
		public void SetState(bool state, bool rebuildLayout)
		{
			if (this.arrowOpened == null || this.arrowClosed == null || this.content == null)
			{
				return;
			}
			if (this.arrowOpened.activeSelf != state)
			{
				this.arrowOpened.SetActive(state);
			}
			if (this.arrowClosed.activeSelf == state)
			{
				this.arrowClosed.SetActive(!state);
			}
			if (this.content.activeSelf != state)
			{
				this.content.SetActive(state);
			}
			if (rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent as RectTransform);
			}
		}

		// Token: 0x04000245 RID: 581
		public GameObject content;

		// Token: 0x04000246 RID: 582
		public GameObject arrowOpened;

		// Token: 0x04000247 RID: 583
		public GameObject arrowClosed;
	}
}
