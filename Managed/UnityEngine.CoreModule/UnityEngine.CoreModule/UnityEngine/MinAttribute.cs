using System;

namespace UnityEngine
{
	// Token: 0x02000185 RID: 389
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06001293 RID: 4755 RVA: 0x0001E81F File Offset: 0x0001CA1F
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04000624 RID: 1572
		public readonly float min;
	}
}
