using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200003C RID: 60
	public interface IPanel : IDisposable
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000136 RID: 310
		VisualElement visualTree { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000137 RID: 311
		EventDispatcher dispatcher { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000138 RID: 312
		ContextType contextType { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000139 RID: 313
		FocusController focusController { get; }

		// Token: 0x0600013A RID: 314
		VisualElement Pick(Vector2 point);

		// Token: 0x0600013B RID: 315
		VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013C RID: 316
		ContextualMenuManager contextualMenuManager { get; }
	}
}
