using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the layout for an image contained in a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000119 RID: 281
	public enum DataGridViewImageCellLayout
	{
		/// <summary>The layout specification has not been set.</summary>
		// Token: 0x04000BCC RID: 3020
		NotSet,
		/// <summary>The graphic is displayed centered using its native resolution.</summary>
		// Token: 0x04000BCD RID: 3021
		Normal,
		/// <summary>The graphic is stretched by the percentages required to fit the width and height of the containing cell.</summary>
		// Token: 0x04000BCE RID: 3022
		Stretch,
		/// <summary>The graphic is uniformly enlarged until it fills the width or height of the containing cell.</summary>
		// Token: 0x04000BCF RID: 3023
		Zoom
	}
}
