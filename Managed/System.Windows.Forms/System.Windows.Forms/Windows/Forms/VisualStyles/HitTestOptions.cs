using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the options that can be used when performing a hit test on the background specified by a visual style.</summary>
	// Token: 0x02000519 RID: 1305
	[Flags]
	public enum HitTestOptions
	{
		/// <summary>The hit test option for the background segment.</summary>
		// Token: 0x04002B58 RID: 11096
		BackgroundSegment = 0,
		/// <summary>The hit test option for the fixed border.</summary>
		// Token: 0x04002B59 RID: 11097
		FixedBorder = 2,
		/// <summary>The hit test option for the caption.</summary>
		// Token: 0x04002B5A RID: 11098
		Caption = 4,
		/// <summary>The hit test option for the left resizing border.</summary>
		// Token: 0x04002B5B RID: 11099
		ResizingBorderLeft = 16,
		/// <summary>The hit test option for the top resizing border.</summary>
		// Token: 0x04002B5C RID: 11100
		ResizingBorderTop = 32,
		/// <summary>The hit test option for the right resizing border.</summary>
		// Token: 0x04002B5D RID: 11101
		ResizingBorderRight = 64,
		/// <summary>The hit test option for the bottom resizing border.</summary>
		// Token: 0x04002B5E RID: 11102
		ResizingBorderBottom = 128,
		/// <summary>The hit test option for the resizing border.</summary>
		// Token: 0x04002B5F RID: 11103
		ResizingBorder = 240,
		/// <summary>The resizing border is specified as a template, not just window edges. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SystemSizingMargins" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
		// Token: 0x04002B60 RID: 11104
		SizingTemplate = 256,
		/// <summary>The system resizing border width is used instead of visual style content margins. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
		// Token: 0x04002B61 RID: 11105
		SystemSizingMargins = 512
	}
}
