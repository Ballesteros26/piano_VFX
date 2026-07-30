using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the image on the control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D7 RID: 471
	public enum ImageLayout
	{
		/// <summary>The image is left-aligned at the top across the control's client rectangle.</summary>
		// Token: 0x04000FC9 RID: 4041
		None,
		/// <summary>The image is tiled across the control's client rectangle.</summary>
		// Token: 0x04000FCA RID: 4042
		Tile,
		/// <summary>The image is centered within the control's client rectangle.</summary>
		// Token: 0x04000FCB RID: 4043
		Center,
		/// <summary>The image is streched across the control's client rectangle.</summary>
		// Token: 0x04000FCC RID: 4044
		Stretch,
		/// <summary>The image is enlarged within the control's client rectangle.</summary>
		// Token: 0x04000FCD RID: 4045
		Zoom
	}
}
