using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies values for navigating among accessible objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000037 RID: 55
	public enum AccessibleNavigation
	{
		/// <summary>Navigation to a sibling object located above the starting object.</summary>
		// Token: 0x04000521 RID: 1313
		Up = 1,
		/// <summary>Navigation to a sibling object located below the starting object.</summary>
		// Token: 0x04000522 RID: 1314
		Down,
		/// <summary>Navigation to the sibling object located to the left of the starting object.</summary>
		// Token: 0x04000523 RID: 1315
		Left,
		/// <summary>Navigation to the sibling object located to the right of the starting object.</summary>
		// Token: 0x04000524 RID: 1316
		Right,
		/// <summary>Navigation to the next logical object, typically from a sibling object to the starting object.</summary>
		// Token: 0x04000525 RID: 1317
		Next,
		/// <summary>Navigation to the previous logical object, typically from a sibling object to the starting object.</summary>
		// Token: 0x04000526 RID: 1318
		Previous,
		/// <summary>Navigation to the first child of the object.</summary>
		// Token: 0x04000527 RID: 1319
		FirstChild,
		/// <summary>Navigation to the last child of the object.</summary>
		// Token: 0x04000528 RID: 1320
		LastChild
	}
}
