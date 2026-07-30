using System;
using System.Drawing;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.VisualStyles
{
	// Token: 0x020004CE RID: 1230
	internal class RadioButtonPainter : RadioButtonPainter
	{
		// Token: 0x06004CA7 RID: 19623 RVA: 0x00132FE0 File Offset: 0x001311E0
		public override void DrawNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			RadioButtonPainter.DrawRadioButton(g, bounds, (!isChecked) ? RadioButtonState.UncheckedNormal : RadioButtonState.CheckedNormal);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00132FF8 File Offset: 0x001311F8
		public override void DrawHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			RadioButtonPainter.DrawRadioButton(g, bounds, (!isChecked) ? RadioButtonState.UncheckedHot : RadioButtonState.CheckedHot);
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00133010 File Offset: 0x00131210
		public override void DrawPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			RadioButtonPainter.DrawRadioButton(g, bounds, (!isChecked) ? RadioButtonState.UncheckedPressed : RadioButtonState.CheckedPressed);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00133028 File Offset: 0x00131228
		public override void DrawDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
		{
			RadioButtonPainter.DrawRadioButton(g, bounds, (!isChecked) ? RadioButtonState.UncheckedDisabled : RadioButtonState.CheckedDisabled);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x00133040 File Offset: 0x00131240
		private static void DrawRadioButton(Graphics g, Rectangle bounds, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, bounds.Location, state);
		}
	}
}
