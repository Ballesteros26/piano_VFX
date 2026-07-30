using System;

namespace TMPro
{
	// Token: 0x0200005A RID: 90
	public struct CaretInfo
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0002139F File Offset: 0x0001F59F
		public CaretInfo(int index, CaretPosition position)
		{
			this.index = index;
			this.position = position;
		}

		// Token: 0x0400043A RID: 1082
		public int index;

		// Token: 0x0400043B RID: 1083
		public CaretPosition position;
	}
}
