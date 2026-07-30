using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F5 RID: 245
	internal class TextEditorEventHandler
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001D171 File Offset: 0x0001B371
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001D179 File Offset: 0x0001B379
		private protected TextEditorEngine editorEngine { protected get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001D182 File Offset: 0x0001B382
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001D18A File Offset: 0x0001B38A
		private protected ITextInputField textInputField { protected get; private set; }

		// Token: 0x06000704 RID: 1796 RVA: 0x0001D193 File Offset: 0x0001B393
		protected TextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField)
		{
			this.editorEngine = editorEngine;
			this.textInputField = textInputField;
			this.textInputField.SyncTextEngine();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000062F3 File Offset: 0x000044F3
		public virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		public virtual void ExecuteDefaultAction(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
			if (flag)
			{
				this.editorEngine.OnFocus();
				this.editorEngine.SelectAll();
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
					this.editorEngine.SelectNone();
				}
			}
		}
	}
}
