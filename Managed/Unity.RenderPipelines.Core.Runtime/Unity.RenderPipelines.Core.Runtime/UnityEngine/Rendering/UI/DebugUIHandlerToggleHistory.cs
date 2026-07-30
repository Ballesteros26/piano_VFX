using System;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A6 RID: 166
	public class DebugUIHandlerToggleHistory : DebugUIHandlerToggle
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryBoolField historyBoolField = widget as DebugUI.HistoryBoolField;
			int num = ((historyBoolField != null) ? historyBoolField.historyDepth : 0);
			this.historyToggles = new Toggle[num];
			for (int i = 0; i < num; i++)
			{
				Toggle toggle = Object.Instantiate<Toggle>(this.valueToggle, base.transform);
				Vector3 position = toggle.transform.position;
				position.x += (float)(i + 1) * 60f;
				toggle.transform.position = position;
				Image component = toggle.transform.GetChild(0).GetComponent<Image>();
				component.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(-1f, -1f, 2f, 2f), Vector2.zero);
				component.color = new Color32(50, 50, 50, 120);
				component.transform.GetChild(0).GetComponent<Image>().color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyToggles[i] = toggle.GetComponent<Toggle>();
			}
			base.SetWidget(widget);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		protected override void UpdateValueLabel()
		{
			base.UpdateValueLabel();
			DebugUI.HistoryBoolField historyBoolField = this.m_Field as DebugUI.HistoryBoolField;
			int num = ((historyBoolField != null) ? historyBoolField.historyDepth : 0);
			for (int i = 0; i < num; i++)
			{
				if (i < this.historyToggles.Length && this.historyToggles[i] != null)
				{
					this.historyToggles[i].isOn = historyBoolField.GetHistoryValue(i);
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010058 File Offset: 0x0000E258
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.valueToggle.isOn = this.m_Field.getter();
			yield break;
		}

		// Token: 0x04000220 RID: 544
		private Toggle[] historyToggles;

		// Token: 0x04000221 RID: 545
		private const float xDecal = 60f;
	}
}
