using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000431 RID: 1073
	internal struct HoverStruct
	{
		// Token: 0x040021D6 RID: 8662
		internal Timer Timer;

		// Token: 0x040021D7 RID: 8663
		internal IntPtr Window;

		// Token: 0x040021D8 RID: 8664
		internal int X;

		// Token: 0x040021D9 RID: 8665
		internal int Y;

		// Token: 0x040021DA RID: 8666
		internal Size Size;

		// Token: 0x040021DB RID: 8667
		internal int Interval;

		// Token: 0x040021DC RID: 8668
		internal IntPtr Atom;
	}
}
