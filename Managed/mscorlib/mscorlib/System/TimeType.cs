using System;

namespace System
{
	// Token: 0x0200023C RID: 572
	internal class TimeType
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x00066F16 File Offset: 0x00065116
		public TimeType(int offset, bool is_dst, string abbrev)
		{
			this.Offset = offset;
			this.IsDst = is_dst;
			this.Name = abbrev;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00066F34 File Offset: 0x00065134
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"offset: ",
				this.Offset,
				"s, is_dst: ",
				this.IsDst.ToString(),
				", zone name: ",
				this.Name
			});
		}

		// Token: 0x04000F3C RID: 3900
		public readonly int Offset;

		// Token: 0x04000F3D RID: 3901
		public readonly bool IsDst;

		// Token: 0x04000F3E RID: 3902
		public string Name;
	}
}
