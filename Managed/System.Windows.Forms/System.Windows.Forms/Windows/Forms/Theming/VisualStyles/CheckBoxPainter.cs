using System;
using System.Drawing;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.VisualStyles
{
	// Token: 0x020004CD RID: 1229
	internal class CheckBoxPainter : CheckBoxPainter
	{
		// Token: 0x06004CA1 RID: 19617 RVA: 0x00132EB8 File Offset: 0x001310B8
		public override void DrawNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedNormal;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedNormal;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedNormal;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x00132EFC File Offset: 0x001310FC
		public override void DrawHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedHot;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedHot;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedHot;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x00132F40 File Offset: 0x00131140
		public override void DrawPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedPressed;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedPressed;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedPressed;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x00132F84 File Offset: 0x00131184
		public override void DrawDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			CheckBoxState checkBoxState;
			if (state != CheckState.Checked)
			{
				if (state != CheckState.Indeterminate)
				{
					checkBoxState = CheckBoxState.UncheckedDisabled;
				}
				else
				{
					checkBoxState = CheckBoxState.MixedDisabled;
				}
			}
			else
			{
				checkBoxState = CheckBoxState.CheckedDisabled;
			}
			CheckBoxPainter.DrawCheckBox(g, bounds, checkBoxState);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x00132FC8 File Offset: 0x001311C8
		private static void DrawCheckBox(Graphics g, Rectangle bounds, CheckBoxState state)
		{
			CheckBoxRenderer.DrawCheckBox(g, bounds.Location, state);
		}
	}
}
