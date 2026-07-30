using System;

namespace UnityEngine
{
	// Token: 0x0200017F RID: 383
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0001E796 File Offset: 0x0001C996
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x0400061C RID: 1564
		public readonly string name;

		// Token: 0x0400061D RID: 1565
		public readonly string function;
	}
}
