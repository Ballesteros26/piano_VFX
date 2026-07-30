using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance of a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018A RID: 394
	public enum FlatStyle
	{
		/// <summary>The control appears flat.</summary>
		// Token: 0x04000E3D RID: 3645
		Flat,
		/// <summary>A control appears flat until the mouse pointer moves over it, at which point it appears three-dimensional.</summary>
		// Token: 0x04000E3E RID: 3646
		Popup,
		/// <summary>The control appears three-dimensional.</summary>
		// Token: 0x04000E3F RID: 3647
		Standard,
		/// <summary>The appearance of the control is determined by the user's operating system.</summary>
		// Token: 0x04000E40 RID: 3648
		System
	}
}
