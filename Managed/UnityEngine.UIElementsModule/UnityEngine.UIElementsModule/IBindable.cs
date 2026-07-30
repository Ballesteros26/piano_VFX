using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BC RID: 188
	public interface IBindable
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600058C RID: 1420
		// (set) Token: 0x0600058D RID: 1421
		IBinding binding { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600058E RID: 1422
		// (set) Token: 0x0600058F RID: 1423
		string bindingPath { get; set; }
	}
}
