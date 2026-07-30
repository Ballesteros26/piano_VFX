using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BE RID: 190
	public static class IBindingExtensions
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00015744 File Offset: 0x00013944
		public static bool IsBound(this IBindable control)
		{
			return ((control != null) ? control.binding : null) != null;
		}
	}
}
