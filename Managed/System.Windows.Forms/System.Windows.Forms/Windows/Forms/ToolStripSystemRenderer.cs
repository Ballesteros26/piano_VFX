using System;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, using system colors and a flat visual style.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037F RID: 895
	public class ToolStripSystemRenderer : ToolStripRenderer
	{
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06004076 RID: 16502 RVA: 0x001007C4 File Offset: 0x000FE9C4
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderButtonBackground(e);
			base.OnRenderButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06004077 RID: 16503 RVA: 0x001007E0 File Offset: 0x000FE9E0
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderDropDownButtonBackground(e);
			base.OnRenderDropDownButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06004078 RID: 16504 RVA: 0x001007FC File Offset: 0x000FE9FC
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderGrip(e);
			base.OnRenderGrip(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06004079 RID: 16505 RVA: 0x00100818 File Offset: 0x000FEA18
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			base.OnRenderImageMargin(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x0600407A RID: 16506 RVA: 0x00100824 File Offset: 0x000FEA24
		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderItemBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600407B RID: 16507 RVA: 0x00100830 File Offset: 0x000FEA30
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderLabelBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600407C RID: 16508 RVA: 0x0010083C File Offset: 0x000FEA3C
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderMenuItemBackground(e);
			base.OnRenderMenuItemBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x0600407D RID: 16509 RVA: 0x00100858 File Offset: 0x000FEA58
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderOverflowButtonBackground(e);
			base.OnRenderOverflowButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x0600407E RID: 16510 RVA: 0x00100874 File Offset: 0x000FEA74
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderSeparator(e);
			base.OnRenderSeparator(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600407F RID: 16511 RVA: 0x00100890 File Offset: 0x000FEA90
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderSplitButtonBackground(e);
			base.OnRenderSplitButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06004080 RID: 16512 RVA: 0x001008AC File Offset: 0x000FEAAC
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderToolStripBackground(e);
			base.OnRenderToolStripBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004081 RID: 16513 RVA: 0x001008C8 File Offset: 0x000FEAC8
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			ThemeElements.CurrentTheme.ToolStripPainter.OnRenderToolStripBorder(e);
			base.OnRenderToolStripBorder(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004082 RID: 16514 RVA: 0x001008E4 File Offset: 0x000FEAE4
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderToolStripStatusLabelBackground(e);
		}
	}
}
