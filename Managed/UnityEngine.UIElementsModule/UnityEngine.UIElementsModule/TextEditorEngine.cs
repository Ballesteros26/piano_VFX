using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F6 RID: 246
	internal class TextEditorEngine : TextEditor
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0001D222 File Offset: 0x0001B422
		public TextEditorEngine(TextEditorEngine.OnDetectFocusChangeFunction detectFocusChange, TextEditorEngine.OnIndexChangeFunction indexChangeFunction)
		{
			this.m_DetectFocusChangeFunction = detectFocusChange;
			this.m_IndexChangeFunction = indexChangeFunction;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001D23C File Offset: 0x0001B43C
		internal override Rect localPosition
		{
			get
			{
				return new Rect(0f, 0f, base.position.width, base.position.height);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001D279 File Offset: 0x0001B479
		internal override void OnDetectFocusChange()
		{
			this.m_DetectFocusChangeFunction();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001D288 File Offset: 0x0001B488
		internal override void OnCursorIndexChange()
		{
			this.m_IndexChangeFunction();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001D288 File Offset: 0x0001B488
		internal override void OnSelectIndexChange()
		{
			this.m_IndexChangeFunction();
		}

		// Token: 0x04000351 RID: 849
		private TextEditorEngine.OnDetectFocusChangeFunction m_DetectFocusChangeFunction;

		// Token: 0x04000352 RID: 850
		private TextEditorEngine.OnIndexChangeFunction m_IndexChangeFunction;

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x0600070D RID: 1805
		internal delegate void OnDetectFocusChangeFunction();

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x06000711 RID: 1809
		internal delegate void OnIndexChangeFunction();
	}
}
