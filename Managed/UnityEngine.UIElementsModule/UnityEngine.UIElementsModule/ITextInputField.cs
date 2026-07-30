using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FD RID: 253
	internal interface ITextInputField : IEventHandler, ITextElement
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600072F RID: 1839
		bool hasFocus { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000730 RID: 1840
		bool doubleClickSelectsWord { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000731 RID: 1841
		bool tripleClickSelectsLine { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000732 RID: 1842
		bool isReadOnly { get; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000733 RID: 1843
		bool isDelayed { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000734 RID: 1844
		bool isPasswordField { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000735 RID: 1845
		TextEditorEngine editorEngine { get; }

		// Token: 0x06000736 RID: 1846
		void SyncTextEngine();

		// Token: 0x06000737 RID: 1847
		bool AcceptCharacter(char c);

		// Token: 0x06000738 RID: 1848
		string CullString(string s);

		// Token: 0x06000739 RID: 1849
		void UpdateText(string value);

		// Token: 0x0600073A RID: 1850
		void UpdateValueFromText();
	}
}
