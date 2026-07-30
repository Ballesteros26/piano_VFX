using System;

namespace UnityEngine
{
	// Token: 0x02000187 RID: 391
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x0001E852 File Offset: 0x0001CA52
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0001E86A File Offset: 0x0001CA6A
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x04000626 RID: 1574
		public readonly int minLines;

		// Token: 0x04000627 RID: 1575
		public readonly int maxLines;
	}
}
