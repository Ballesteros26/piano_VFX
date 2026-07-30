using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000027 RID: 39
	internal interface IOleDragClient
	{
		// Token: 0x06000153 RID: 339
		bool AddComponent(IComponent component, string name, bool firstAdd);

		// Token: 0x06000154 RID: 340
		Control GetControlForComponent(object component);

		// Token: 0x06000155 RID: 341
		Control GetDesignerControl();

		// Token: 0x06000156 RID: 342
		bool IsDropOk(IComponent component);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000157 RID: 343
		bool CanModifyComponents { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000158 RID: 344
		IComponent Component { get; }
	}
}
