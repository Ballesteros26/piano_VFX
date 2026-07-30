using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F9 RID: 249
	public class TextField : TextInputBaseField<string>
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001D297 File Offset: 0x0001B497
		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001D2C1 File Offset: 0x0001B4C1
		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				this.textInput.multiline = value;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001D2D1 File Offset: 0x0001B4D1
		public void SelectRange(int rangeCursorIndex, int selectionIndex)
		{
			this.textInput.SelectRange(rangeCursorIndex, selectionIndex);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001D2E2 File Offset: 0x0001B4E2
		public TextField()
			: this(null)
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001D2ED File Offset: 0x0001B4ED
		public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: this(null, maxLength, multiline, isPasswordField, maskChar)
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001D2FD File Offset: 0x0001B4FD
		public TextField(string label)
			: this(label, -1, false, false, '*')
		{
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001D310 File Offset: 0x0001B510
		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar)
			: base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.isPasswordField = isPasswordField;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001D384 File Offset: 0x0001B584
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001D39C File Offset: 0x0001B59C
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.text = base.rawValue;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			base.text = base.rawValue;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001D3CC File Offset: 0x0001B5CC
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			base.text = base.rawValue;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001D400 File Offset: 0x0001B600
		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool multiline = this.multiline;
			if (multiline)
			{
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
				long num2 = EventBase<FocusInEvent>.TypeId();
				bool flag;
				if (!((num.GetValueOrDefault() == num2) & (num != null)) || ((evt != null) ? evt.leafTarget : null) != this)
				{
					num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
					num2 = EventBase<FocusInEvent>.TypeId();
					flag = ((num.GetValueOrDefault() == num2) & (num != null)) && ((evt != null) ? evt.leafTarget : null) == base.labelElement;
				}
				else
				{
					flag = true;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this.m_VisualInputTabIndex = base.visualInput.tabIndex;
					base.visualInput.tabIndex = -1;
				}
				else
				{
					num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
					num2 = EventBase<BlurEvent>.TypeId();
					bool flag3;
					if (!((num.GetValueOrDefault() == num2) & (num != null)) || ((evt != null) ? evt.leafTarget : null) != this)
					{
						num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
						num2 = EventBase<BlurEvent>.TypeId();
						flag3 = ((num.GetValueOrDefault() == num2) & (num != null)) && ((evt != null) ? evt.leafTarget : null) == base.labelElement;
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						base.visualInput.tabIndex = this.m_VisualInputTabIndex;
					}
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private int m_VisualInputTabIndex;

		// Token: 0x04000354 RID: 852
		public new static readonly string ussClassName = "unity-text-field";

		// Token: 0x04000355 RID: 853
		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		// Token: 0x04000356 RID: 854
		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		// Token: 0x020000FA RID: 250
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
		}

		// Token: 0x020000FB RID: 251
		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			// Token: 0x06000723 RID: 1827 RVA: 0x0001D5CC File Offset: 0x0001B7CC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField textField = (TextField)ve;
				textField.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000357 RID: 855
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};
		}

		// Token: 0x020000FC RID: 252
		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001D61F File Offset: 0x0001B81F
			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001D62C File Offset: 0x0001B82C
			// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001D644 File Offset: 0x0001B844
			public bool multiline
			{
				get
				{
					return this.m_Multiline;
				}
				set
				{
					this.m_Multiline = value;
					bool flag = !value;
					if (flag)
					{
						base.text = base.text.Replace("\n", "");
					}
				}
			}

			// Token: 0x1700019E RID: 414
			// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001D680 File Offset: 0x0001B880
			public override bool isPasswordField
			{
				set
				{
					base.isPasswordField = value;
					if (value)
					{
						this.multiline = false;
					}
				}
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
			protected override string StringToValue(string str)
			{
				return str;
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
			public void SelectRange(int cursorIndex, int selectionIndex)
			{
				bool flag = base.editorEngine != null;
				if (flag)
				{
					base.editorEngine.cursorIndex = cursorIndex;
					base.editorEngine.selectIndex = selectionIndex;
				}
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
			internal override void SyncTextEngine()
			{
				bool flag = this.parentTextField != null;
				if (flag)
				{
					base.editorEngine.multiline = this.multiline;
					base.editorEngine.isPasswordField = this.isPasswordField;
				}
				base.SyncTextEngine();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0001D738 File Offset: 0x0001B938
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = evt == null;
				if (!flag)
				{
					bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
					if (flag2)
					{
						KeyDownEvent keyDownEvent = evt as KeyDownEvent;
						bool flag3 = !this.parentTextField.isDelayed || (!this.multiline && ((keyDownEvent != null && keyDownEvent.keyCode == KeyCode.KeypadEnter) || (keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Return)));
						if (flag3)
						{
							this.parentTextField.value = base.text;
						}
						bool multiline = this.multiline;
						if (multiline)
						{
							char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
							int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
							int num2 = 9;
							bool flag4 = ((num.GetValueOrDefault() == num2) & (num != null)) && keyDownEvent.modifiers == EventModifiers.None;
							if (flag4)
							{
								if (keyDownEvent != null)
								{
									keyDownEvent.StopPropagation();
								}
								if (keyDownEvent != null)
								{
									keyDownEvent.PreventDefault();
								}
							}
							else
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
								num2 = 3;
								bool flag5;
								if (!((num.GetValueOrDefault() == num2) & (num != null)) || keyDownEvent == null || !keyDownEvent.shiftKey)
								{
									c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
									num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
									num2 = 10;
									flag5 = ((num.GetValueOrDefault() == num2) & (num != null)) && keyDownEvent != null && keyDownEvent.shiftKey;
								}
								else
								{
									flag5 = true;
								}
								bool flag6 = flag5;
								if (flag6)
								{
									base.parent.Focus();
								}
							}
						}
						else
						{
							char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
							int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
							int num2 = 3;
							bool flag7;
							if (!((num.GetValueOrDefault() == num2) & (num != null)))
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
								num2 = 10;
								flag7 = (num.GetValueOrDefault() == num2) & (num != null);
							}
							else
							{
								flag7 = true;
							}
							bool flag8 = flag7;
							if (flag8)
							{
								base.parent.Focus();
							}
						}
					}
					else
					{
						bool flag9 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
						if (flag9)
						{
							ExecuteCommandEvent executeCommandEvent = evt as ExecuteCommandEvent;
							string commandName = executeCommandEvent.commandName;
							bool flag10 = !this.parentTextField.isDelayed && (commandName == "Paste" || commandName == "Cut");
							if (flag10)
							{
								this.parentTextField.value = base.text;
							}
						}
					}
				}
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x0001DA84 File Offset: 0x0001BC84
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				bool flag;
				if (this.parentTextField.isDelayed)
				{
					long? num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
					long num2 = EventBase<BlurEvent>.TypeId();
					flag = (num.GetValueOrDefault() == num2) & (num != null);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this.parentTextField.value = base.text;
				}
			}

			// Token: 0x04000358 RID: 856
			private bool m_Multiline;
		}
	}
}
