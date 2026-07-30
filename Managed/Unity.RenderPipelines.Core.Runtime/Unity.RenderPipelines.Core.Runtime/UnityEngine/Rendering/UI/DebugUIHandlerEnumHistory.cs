using System;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009B RID: 155
	public class DebugUIHandlerEnumHistory : DebugUIHandlerEnumField
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000F17C File Offset: 0x0000D37C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryEnumField historyEnumField = widget as DebugUI.HistoryEnumField;
			int num = ((historyEnumField != null) ? historyEnumField.historyDepth : 0);
			this.historyValues = new Text[num];
			for (int i = 0; i < num; i++)
			{
				Text text = Object.Instantiate<Text>(this.valueLabel, base.transform);
				Vector3 position = text.transform.position;
				position.x += (float)(i + 1) * 60f;
				text.transform.position = position;
				Text component = text.GetComponent<Text>();
				component.color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyValues[i] = component;
			}
			base.SetWidget(widget);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F224 File Offset: 0x0000D424
		protected override void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			this.valueLabel.text = this.m_Field.enumNames[num].text;
			DebugUI.HistoryEnumField historyEnumField = this.m_Field as DebugUI.HistoryEnumField;
			int num2 = ((historyEnumField != null) ? historyEnumField.historyDepth : 0);
			for (int i = 0; i < num2; i++)
			{
				if (i < this.historyValues.Length && this.historyValues[i] != null)
				{
					this.historyValues[i].text = historyEnumField.enumNames[historyEnumField.GetHistoryValue(i)].text;
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.m_Field.currentIndex = this.m_Field.getIndex();
			this.valueLabel.text = this.m_Field.enumNames[this.m_Field.currentIndex].text;
			yield break;
		}

		// Token: 0x040001F2 RID: 498
		private Text[] historyValues;

		// Token: 0x040001F3 RID: 499
		private const float xDecal = 60f;
	}
}
