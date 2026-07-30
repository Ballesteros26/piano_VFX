using System;

namespace UnityEngine
{
	// Token: 0x02000184 RID: 388
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x06001292 RID: 4754 RVA: 0x0001E807 File Offset: 0x0001CA07
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000622 RID: 1570
		public readonly float min;

		// Token: 0x04000623 RID: 1571
		public readonly float max;
	}
}
