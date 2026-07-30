using System;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0001E830 File Offset: 0x0001CA30
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0001E841 File Offset: 0x0001CA41
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x04000625 RID: 1573
		public readonly int lines;
	}
}
