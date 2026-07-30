using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A3 RID: 163
	internal interface IVisualElementPanelActivatable
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004DC RID: 1244
		VisualElement element { get; }

		// Token: 0x060004DD RID: 1245
		bool CanBeActivated();

		// Token: 0x060004DE RID: 1246
		void OnPanelActivate();

		// Token: 0x060004DF RID: 1247
		void OnPanelDeactivate();
	}
}
