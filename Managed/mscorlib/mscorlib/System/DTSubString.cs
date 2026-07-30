using System;

namespace System
{
	// Token: 0x02000173 RID: 371
	internal struct DTSubString
	{
		// Token: 0x170001F7 RID: 503
		internal char this[int relativeIndex]
		{
			get
			{
				return this.s[this.index + relativeIndex];
			}
		}

		// Token: 0x04000995 RID: 2453
		internal string s;

		// Token: 0x04000996 RID: 2454
		internal int index;

		// Token: 0x04000997 RID: 2455
		internal int length;

		// Token: 0x04000998 RID: 2456
		internal DTSubStringType type;

		// Token: 0x04000999 RID: 2457
		internal int value;
	}
}
