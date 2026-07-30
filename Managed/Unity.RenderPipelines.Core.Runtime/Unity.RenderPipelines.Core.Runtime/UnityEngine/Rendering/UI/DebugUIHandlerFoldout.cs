using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009D RID: 157
	public class DebugUIHandlerFoldout : DebugUIHandlerWidget
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000F420 File Offset: 0x0000D620
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Foldout>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			string[] columnLabels = this.m_Field.columnLabels;
			int num = ((columnLabels != null) ? columnLabels.Length : 0);
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.nameLabel.gameObject, base.GetComponent<DebugUIHandlerContainer>().contentHolder);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				RectTransform rectTransform = gameObject.transform as RectTransform;
				RectTransform rectTransform2 = this.nameLabel.transform as RectTransform;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform.anchorMin = vector;
				rectTransform.anchorMax = vector;
				rectTransform.sizeDelta = new Vector2(100f, 26f);
				Vector3 vector2 = rectTransform2.anchoredPosition;
				vector2.x += (float)(i + 1) * 60f + 230f;
				rectTransform.anchoredPosition = vector2;
				rectTransform.pivot = new Vector2(0f, 0.5f);
				rectTransform.eulerAngles = new Vector3(0f, 0f, 13f);
				Text component = gameObject.GetComponent<Text>();
				component.fontSize = 15;
				component.text = this.m_Field.columnLabels[i];
			}
			this.UpdateValue();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000F588 File Offset: 0x0000D788
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000F5FF File Offset: 0x0000D7FF
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000F612 File Offset: 0x0000D812
		public override void OnIncrement(bool fast)
		{
			this.m_Field.SetValue(true);
			this.UpdateValue();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F626 File Offset: 0x0000D826
		public override void OnDecrement(bool fast)
		{
			this.m_Field.SetValue(false);
			this.UpdateValue();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000F63C File Offset: 0x0000D83C
		public override void OnAction()
		{
			bool flag = !this.m_Field.GetValue();
			this.m_Field.SetValue(flag);
			this.UpdateValue();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F66A File Offset: 0x0000D86A
		private void UpdateValue()
		{
			this.valueToggle.isOn = this.m_Field.GetValue();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F684 File Offset: 0x0000D884
		public override DebugUIHandlerWidget Next()
		{
			if (!this.m_Field.GetValue() || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x040001F7 RID: 503
		public Text nameLabel;

		// Token: 0x040001F8 RID: 504
		public UIFoldout valueToggle;

		// Token: 0x040001F9 RID: 505
		private DebugUI.Foldout m_Field;

		// Token: 0x040001FA RID: 506
		private DebugUIHandlerContainer m_Container;

		// Token: 0x040001FB RID: 507
		private const float xDecal = 60f;

		// Token: 0x040001FC RID: 508
		private const float xDecalInit = 230f;
	}
}
