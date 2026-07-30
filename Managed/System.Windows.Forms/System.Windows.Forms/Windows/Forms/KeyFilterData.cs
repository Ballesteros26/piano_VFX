using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000463 RID: 1123
	[StructLayout(0, CharSet = 3)]
	internal struct KeyFilterData
	{
		// Token: 0x04002584 RID: 9604
		internal bool Down;

		// Token: 0x04002585 RID: 9605
		internal int keycode;

		// Token: 0x04002586 RID: 9606
		internal int keysym;

		// Token: 0x04002587 RID: 9607
		internal Keys ModifierKeys;

		// Token: 0x04002588 RID: 9608
		internal string str;
	}
}
