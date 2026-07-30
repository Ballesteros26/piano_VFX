using System;

namespace UnityEngine
{
	// Token: 0x02000181 RID: 385
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x0001E7BF File Offset: 0x0001C9BF
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x0400061F RID: 1567
		public readonly string tooltip;
	}
}
