using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000104 RID: 260
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0001F8EE File Offset: 0x0001DAEE
		public TouchScreenTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField)
			: base(editorEngine, textInputField)
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001F904 File Offset: 0x0001DB04
		private void PollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = this.m_TouchKeyboardPoller == null;
				if (flag2)
				{
					VisualElement visualElement = base.textInputField as VisualElement;
					this.m_TouchKeyboardPoller = ((visualElement != null) ? visualElement.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001F980 File Offset: 0x0001DB80
		private void DoPollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = base.textInputField.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					base.textInputField.UpdateText(base.textInputField.CullString(base.textInputField.editorEngine.keyboardOnScreen.text));
					bool flag3 = !base.textInputField.isDelayed;
					if (flag3)
					{
						base.textInputField.UpdateValueFromText();
					}
					bool flag4 = base.textInputField.editorEngine.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
					if (flag4)
					{
						base.textInputField.editorEngine.keyboardOnScreen = null;
						this.m_TouchKeyboardPoller.Pause();
						bool isDelayed = base.textInputField.isDelayed;
						if (isDelayed)
						{
							base.textInputField.UpdateValueFromText();
						}
					}
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001FA6C File Offset: 0x0001DC6C
		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			long num = EventBase<MouseDownEvent>.TypeId();
			bool flag = !base.textInputField.isReadOnly && evt.eventTypeId == num && base.editorEngine.keyboardOnScreen == null;
			if (flag)
			{
				base.textInputField.SyncTextEngine();
				base.textInputField.UpdateText(base.editorEngine.text);
				base.editorEngine.keyboardOnScreen = TouchScreenKeyboard.Open(base.textInputField.text, TouchScreenKeyboardType.Default, true, base.editorEngine.multiline, base.textInputField.isPasswordField);
				bool flag2 = base.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					this.PollTouchScreenKeyboard();
				}
				base.editorEngine.UpdateScrollOffset();
				evt.StopPropagation();
			}
		}

		// Token: 0x0400037F RID: 895
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;
	}
}
