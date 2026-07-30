using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define which mouse button was pressed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026F RID: 623
	[Flags]
	[ComVisible(true)]
	public enum MouseButtons
	{
		/// <summary>No mouse button was pressed.</summary>
		// Token: 0x04001457 RID: 5207
		None = 0,
		/// <summary>The left mouse button was pressed.</summary>
		// Token: 0x04001458 RID: 5208
		Left = 1048576,
		/// <summary>The right mouse button was pressed.</summary>
		// Token: 0x04001459 RID: 5209
		Right = 2097152,
		/// <summary>The middle mouse button was pressed.</summary>
		// Token: 0x0400145A RID: 5210
		Middle = 4194304,
		/// <summary>The first XButton was pressed.</summary>
		// Token: 0x0400145B RID: 5211
		XButton1 = 8388608,
		/// <summary>The second XButton was pressed.</summary>
		// Token: 0x0400145C RID: 5212
		XButton2 = 16777216
	}
}
