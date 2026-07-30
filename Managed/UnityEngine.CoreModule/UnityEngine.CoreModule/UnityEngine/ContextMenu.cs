using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	[AttributeUsage(64, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x0001F393 File Offset: 0x0001D593
		public ContextMenu(string itemName)
			: this(itemName, false)
		{
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0001F39F File Offset: 0x0001D59F
		public ContextMenu(string itemName, bool isValidateFunction)
			: this(itemName, isValidateFunction, 1000000)
		{
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		public ContextMenu(string itemName, bool isValidateFunction, int priority)
		{
			this.menuItem = itemName;
			this.validate = isValidateFunction;
			this.priority = priority;
		}

		// Token: 0x04000640 RID: 1600
		public readonly string menuItem;

		// Token: 0x04000641 RID: 1601
		public readonly bool validate;

		// Token: 0x04000642 RID: 1602
		public readonly int priority;
	}
}
