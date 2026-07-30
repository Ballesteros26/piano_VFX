using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E5 RID: 485
	public abstract class TypedUxmlAttributeDescription<T> : UxmlAttributeDescription
	{
		// Token: 0x06000F03 RID: 3843
		public abstract T GetValueFromBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00037C9B File Offset: 0x00035E9B
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00037CA3 File Offset: 0x00035EA3
		public T defaultValue { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00037CAC File Offset: 0x00035EAC
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = this.defaultValue;
				return defaultValue.ToString();
			}
		}
	}
}
