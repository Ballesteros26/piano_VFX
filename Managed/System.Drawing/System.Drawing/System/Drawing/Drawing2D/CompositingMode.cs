using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how the source colors are combined with the background colors.</summary>
	// Token: 0x02000133 RID: 307
	public enum CompositingMode
	{
		/// <summary>Specifies that when a color is rendered, it is blended with the background color. The blend is determined by the alpha component of the color being rendered.</summary>
		// Token: 0x04000AA4 RID: 2724
		SourceOver,
		/// <summary>Specifies that when a color is rendered, it overwrites the background color.</summary>
		// Token: 0x04000AA5 RID: 2725
		SourceCopy
	}
}
