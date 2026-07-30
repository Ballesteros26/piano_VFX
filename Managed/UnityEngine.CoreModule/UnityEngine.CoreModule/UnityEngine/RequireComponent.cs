using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	[RequiredByNativeCode]
	[AttributeUsage(4, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0001F2B6 File Offset: 0x0001D4B6
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0001F2C7 File Offset: 0x0001D4C7
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0001F2DF File Offset: 0x0001D4DF
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x04000638 RID: 1592
		public Type m_Type0;

		// Token: 0x04000639 RID: 1593
		public Type m_Type1;

		// Token: 0x0400063A RID: 1594
		public Type m_Type2;
	}
}
