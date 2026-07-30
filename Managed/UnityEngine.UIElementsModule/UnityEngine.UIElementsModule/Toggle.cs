using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000101 RID: 257
	public class Toggle : BaseField<bool>
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x0001F54E File Offset: 0x0001D74E
		public Toggle()
			: this(null)
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001F55C File Offset: 0x0001D75C
		public Toggle(string label)
			: base(label, null)
		{
			base.AddToClassList(Toggle.ussClassName);
			base.AddToClassList(Toggle.noTextVariantUssClassName);
			base.visualInput.AddToClassList(Toggle.inputUssClassName);
			base.labelElement.AddToClassList(Toggle.labelUssClassName);
			VisualElement visualElement = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			visualElement.AddToClassList(Toggle.checkmarkUssClassName);
			base.visualInput.Add(visualElement);
			base.visualInput.pickingMode = PickingMode.Position;
			this.text = null;
			this.AddManipulator(new Clickable(new Action<EventBase>(this.OnClickEvent)));
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001F610 File Offset: 0x0001D810
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001F634 File Offset: 0x0001D834
		public string text
		{
			get
			{
				Label label = this.m_Label;
				return (label != null) ? label.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_Label == null;
					if (flag2)
					{
						this.m_Label = new Label
						{
							pickingMode = PickingMode.Ignore
						};
						this.m_Label.AddToClassList(Toggle.textUssClassName);
						base.RemoveFromClassList(Toggle.noTextVariantUssClassName);
						base.visualInput.Add(this.m_Label);
					}
					this.m_Label.text = value;
				}
				else
				{
					bool flag3 = this.m_Label != null;
					if (flag3)
					{
						base.Remove(this.m_Label);
						base.AddToClassList(Toggle.noTextVariantUssClassName);
						this.m_Label = null;
					}
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001F74C File Offset: 0x0001D94C
		private void OnClickEvent(EventBase evt)
		{
			MouseUpEvent mouseUpEvent = evt as MouseUpEvent;
			bool flag = mouseUpEvent != null && mouseUpEvent.button == 0;
			if (flag)
			{
				MouseUpEvent mouseUpEvent2 = (MouseUpEvent)evt;
				bool flag2 = base.visualInput.ContainsPoint(base.visualInput.WorldToLocal(mouseUpEvent2.mousePosition));
				if (flag2)
				{
					this.OnClick();
				}
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001F7A5 File Offset: 0x0001D9A5
		private void OnClick()
		{
			this.value = !this.value;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				KeyDownEvent keyDownEvent = evt as KeyDownEvent;
				bool flag2;
				if (keyDownEvent == null || keyDownEvent.keyCode != KeyCode.KeypadEnter)
				{
					KeyDownEvent keyDownEvent2 = evt as KeyDownEvent;
					flag2 = keyDownEvent2 != null && keyDownEvent2.keyCode == KeyCode.Return;
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					this.OnClick();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x04000377 RID: 887
		public new static readonly string ussClassName = "unity-toggle";

		// Token: 0x04000378 RID: 888
		public new static readonly string labelUssClassName = Toggle.ussClassName + "__label";

		// Token: 0x04000379 RID: 889
		public new static readonly string inputUssClassName = Toggle.ussClassName + "__input";

		// Token: 0x0400037A RID: 890
		public static readonly string noTextVariantUssClassName = Toggle.ussClassName + "--no-text";

		// Token: 0x0400037B RID: 891
		public static readonly string checkmarkUssClassName = Toggle.ussClassName + "__checkmark";

		// Token: 0x0400037C RID: 892
		public static readonly string textUssClassName = Toggle.ussClassName + "__text";

		// Token: 0x0400037D RID: 893
		private Label m_Label;

		// Token: 0x02000102 RID: 258
		public new class UxmlFactory : UxmlFactory<Toggle, Toggle.UxmlTraits>
		{
		}

		// Token: 0x02000103 RID: 259
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x060007AE RID: 1966 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((Toggle)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400037E RID: 894
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
