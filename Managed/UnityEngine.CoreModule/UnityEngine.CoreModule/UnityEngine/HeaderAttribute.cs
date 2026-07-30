using System;

namespace UnityEngine
{
	// Token: 0x02000183 RID: 387
	[AttributeUsage(256, Inherited = true, AllowMultiple = true)]
	public class HeaderAttribute : PropertyAttribute
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x0001E7F6 File Offset: 0x0001C9F6
		public HeaderAttribute(string header)
		{
			this.header = header;
		}

		// Token: 0x04000621 RID: 1569
		public readonly string header;
	}
}
