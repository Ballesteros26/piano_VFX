using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how the elements of a control are drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015A RID: 346
	public enum DrawMode
	{
		/// <summary>All the elements in a control are drawn by the operating system and are of the same size.</summary>
		// Token: 0x04000CF4 RID: 3316
		Normal,
		/// <summary>All the elements in the control are drawn manually and are of the same size.</summary>
		// Token: 0x04000CF5 RID: 3317
		OwnerDrawFixed,
		/// <summary>All the elements in the control are drawn manually and can differ in size.</summary>
		// Token: 0x04000CF6 RID: 3318
		OwnerDrawVariable
	}
}
