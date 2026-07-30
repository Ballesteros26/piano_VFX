using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the visual effects that can be applied to the edges of a visual style element.</summary>
	// Token: 0x020004D8 RID: 1240
	[Flags]
	public enum EdgeEffects
	{
		/// <summary>The border is drawn without any effects.</summary>
		// Token: 0x04002A48 RID: 10824
		None = 0,
		/// <summary>The area within the element borders is filled.</summary>
		// Token: 0x04002A49 RID: 10825
		FillInterior = 2048,
		/// <summary>The border is flat.</summary>
		// Token: 0x04002A4A RID: 10826
		Flat = 4096,
		/// <summary>The border is soft.</summary>
		// Token: 0x04002A4B RID: 10827
		Soft = 16384,
		/// <summary>The border is one-dimensional.</summary>
		// Token: 0x04002A4C RID: 10828
		Mono = 32768
	}
}
