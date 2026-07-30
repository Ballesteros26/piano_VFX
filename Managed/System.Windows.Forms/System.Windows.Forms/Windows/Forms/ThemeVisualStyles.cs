using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200032A RID: 810
	internal class ThemeVisualStyles : ThemeWin32Classic
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000D3E3C File Offset: 0x000D203C
		public ThemeVisualStyles()
		{
			ThemeVisualStyles.Update();
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000D3E68 File Offset: 0x000D2068
		public override void ResetDefaults()
		{
			base.ResetDefaults();
			ThemeVisualStyles.Update();
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000D3E78 File Offset: 0x000D2078
		private static void Update()
		{
			bool isEnabledByUser = VisualStyleInformation.IsEnabledByUser;
			ThemeVisualStyles.render_client_areas = isEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			ThemeVisualStyles.render_non_client_areas = isEnabledByUser && Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled;
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000D3EC8 File Offset: 0x000D20C8
		public static bool RenderClientAreas
		{
			get
			{
				return ThemeVisualStyles.render_client_areas;
			}
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000D3ED0 File Offset: 0x000D20D0
		public override void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button)
		{
			if (button.FlatStyle == FlatStyle.System)
			{
				ButtonRenderer.DrawButton(dc, new Rectangle(Point.Empty, button.Size), button.Text, button.Font, button.TextFormatFlags, null, Rectangle.Empty, ThemeWin32Classic.ShouldPaintFocusRectagle(button), ThemeVisualStyles.GetPushButtonState(button));
				return;
			}
			base.DrawButtonBase(dc, clip_area, button);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000D3F30 File Offset: 0x000D2130
		private static PushButtonState GetPushButtonState(ButtonBase button)
		{
			if (!button.Enabled)
			{
				return PushButtonState.Disabled;
			}
			if (button.Pressed)
			{
				return PushButtonState.Pressed;
			}
			if (button.Entered)
			{
				return PushButtonState.Hot;
			}
			if (button.IsDefault || button.Focused || button.paint_as_acceptbutton)
			{
				return PushButtonState.Default;
			}
			return PushButtonState.Normal;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D3F88 File Offset: 0x000D2188
		public override void DrawButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !button.UseVisualStyleBackColor)
			{
				base.DrawButtonBackground(g, button, clipArea);
				return;
			}
			ButtonRenderer.GetPushButtonRenderer(ThemeVisualStyles.GetPushButtonState(button)).DrawBackground(g, new Rectangle(Point.Empty, button.Size));
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D3FD8 File Offset: 0x000D21D8
		protected override void CheckBox_DrawCheckBox(Graphics dc, CheckBox checkbox, ButtonState state, Rectangle checkbox_rectangle)
		{
			if (checkbox.Appearance == Appearance.Normal && checkbox.FlatStyle == FlatStyle.System)
			{
				CheckBoxRenderer.DrawCheckBox(dc, new Point(checkbox_rectangle.Left, checkbox_rectangle.Top), ThemeVisualStyles.GetCheckBoxState(checkbox));
				return;
			}
			base.CheckBox_DrawCheckBox(dc, checkbox, state, checkbox_rectangle);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D4028 File Offset: 0x000D2228
		private static CheckBoxState GetCheckBoxState(CheckBox checkBox)
		{
			CheckState checkState = checkBox.CheckState;
			if (checkState != CheckState.Checked)
			{
				if (checkState != CheckState.Indeterminate)
				{
					if (!checkBox.Enabled)
					{
						return CheckBoxState.UncheckedDisabled;
					}
					if (checkBox.Pressed)
					{
						return CheckBoxState.UncheckedPressed;
					}
					if (checkBox.Entered)
					{
						return CheckBoxState.UncheckedHot;
					}
					return CheckBoxState.UncheckedNormal;
				}
				else
				{
					if (!checkBox.Enabled)
					{
						return CheckBoxState.MixedDisabled;
					}
					if (checkBox.Pressed)
					{
						return CheckBoxState.MixedPressed;
					}
					if (checkBox.Entered)
					{
						return CheckBoxState.MixedHot;
					}
					return CheckBoxState.MixedNormal;
				}
			}
			else
			{
				if (!checkBox.Enabled)
				{
					return CheckBoxState.CheckedDisabled;
				}
				if (checkBox.Pressed)
				{
					return CheckBoxState.CheckedPressed;
				}
				if (checkBox.Entered)
				{
					return CheckBoxState.CheckedHot;
				}
				return CheckBoxState.CheckedNormal;
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000D40D0 File Offset: 0x000D22D0
		private static VisualStyleElement ComboBoxGetVisualStyleElement(ComboBox comboBox, ButtonState state)
		{
			if (state == ButtonState.Inactive)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Disabled;
			}
			if (state == ButtonState.Pushed)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Pressed;
			}
			if (comboBox.DropDownButtonEntered)
			{
				return VisualStyleElement.ComboBox.DropDownButton.Hot;
			}
			return VisualStyleElement.ComboBox.DropDownButton.Normal;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000D4118 File Offset: 0x000D2318
		public override void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ComboBoxDrawNormalDropDownButton(comboBox, g, clippingArea, area, state);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ComboBoxGetVisualStyleElement(comboBox, state);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ComboBoxDrawNormalDropDownButton(comboBox, g, clippingArea, area, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000D4170 File Offset: 0x000D2370
		public override bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ComboBoxNormalDropDownButtonHasTransparentBackground(comboBox, state);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ComboBoxGetVisualStyleElement(comboBox, state);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.ComboBoxNormalDropDownButtonHasTransparentBackground(comboBox, state);
			}
			return new VisualStyleRenderer(visualStyleElement).IsBackgroundPartiallyTransparent();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000D41B8 File Offset: 0x000D23B8
		public override bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ComboBoxDropDownButtonHasHotElementStyle(comboBox);
			}
			FlatStyle flatStyle = comboBox.FlatStyle;
			return (flatStyle != FlatStyle.Flat && flatStyle != FlatStyle.Popup) || base.ComboBoxDropDownButtonHasHotElementStyle(comboBox);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000D41FC File Offset: 0x000D23FC
		private static bool ComboBoxShouldPaintBackground(ComboBox comboBox)
		{
			if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
			{
				return false;
			}
			FlatStyle flatStyle = comboBox.FlatStyle;
			return flatStyle != FlatStyle.Flat && flatStyle != FlatStyle.Popup;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000D4234 File Offset: 0x000D2434
		public override void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.ComboBoxShouldPaintBackground(comboBox))
			{
				base.ComboBoxDrawBackground(comboBox, g, clippingArea, style);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (!comboBox.Enabled)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Disabled;
			}
			else if (comboBox.Entered)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Hot;
			}
			else if (comboBox.Focused)
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Focused;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ComboBox.Border.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ComboBoxDrawBackground(comboBox, g, clippingArea, style);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, new Rectangle(Point.Empty, comboBox.Size), clippingArea);
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000D42E0 File Offset: 0x000D24E0
		public override bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox)
		{
			return (ThemeVisualStyles.RenderClientAreas && ThemeVisualStyles.ComboBoxShouldPaintBackground(comboBox) && comboBox.Enabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.Border.Hot)) || base.CombBoxBackgroundHasHotElementStyle(comboBox);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000D4328 File Offset: 0x000D2528
		public override void CPDrawButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawButton(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Disabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Pressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.PushButton.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawButton(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000D43CC File Offset: 0x000D25CC
		public override void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawCaptionButton(graphics, rectangle, button, state);
				return;
			}
			VisualStyleElement captionButtonVisualStyleElement = ThemeVisualStyles.GetCaptionButtonVisualStyleElement(button, state);
			if (!VisualStyleRenderer.IsElementDefined(captionButtonVisualStyleElement))
			{
				base.CPDrawCaptionButton(graphics, rectangle, button, state);
				return;
			}
			new VisualStyleRenderer(captionButtonVisualStyleElement).DrawBackground(graphics, rectangle);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000D4440 File Offset: 0x000D2640
		private static VisualStyleElement GetCaptionButtonVisualStyleElement(CaptionButton button, ButtonState state)
		{
			switch (button)
			{
			case CaptionButton.Close:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.CloseButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.CloseButton.Pressed;
				}
				return VisualStyleElement.Window.CloseButton.Normal;
			case CaptionButton.Minimize:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.MinButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.MinButton.Pressed;
				}
				return VisualStyleElement.Window.MinButton.Normal;
			case CaptionButton.Maximize:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.MaxButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.MaxButton.Pressed;
				}
				return VisualStyleElement.Window.MaxButton.Normal;
			case CaptionButton.Restore:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.RestoreButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.RestoreButton.Pressed;
				}
				return VisualStyleElement.Window.RestoreButton.Normal;
			default:
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					return VisualStyleElement.Window.HelpButton.Disabled;
				}
				if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					return VisualStyleElement.Window.HelpButton.Pressed;
				}
				return VisualStyleElement.Window.HelpButton.Normal;
			}
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000D4570 File Offset: 0x000D2770
		public override void CPDrawCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat)
			{
				base.CPDrawCheckBox(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedDisabled;
				}
				else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedPressed;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.CheckedNormal;
				}
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedDisabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedPressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawCheckBox(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D4658 File Offset: 0x000D2858
		public override void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawComboButton(graphics, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Disabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Pressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.ComboBox.DropDownButton.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawComboButton(graphics, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(graphics, rectangle);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000D46FC File Offset: 0x000D28FC
		public override void CPDrawMixedCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat)
			{
				base.CPDrawMixedCheckBox(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedDisabled;
				}
				else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedPressed;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Button.CheckBox.MixedNormal;
				}
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedDisabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedPressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.CheckBox.UncheckedNormal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawMixedCheckBox(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000D47E4 File Offset: 0x000D29E4
		public override void CPDrawRadioButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat)
			{
				base.CPDrawRadioButton(dc, rectangle, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					visualStyleElement = VisualStyleElement.Button.RadioButton.CheckedDisabled;
				}
				else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					visualStyleElement = VisualStyleElement.Button.RadioButton.CheckedPressed;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Button.RadioButton.CheckedNormal;
				}
			}
			else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
			{
				visualStyleElement = VisualStyleElement.Button.RadioButton.UncheckedDisabled;
			}
			else if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				visualStyleElement = VisualStyleElement.Button.RadioButton.UncheckedPressed;
			}
			else
			{
				visualStyleElement = VisualStyleElement.Button.RadioButton.UncheckedNormal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.CPDrawRadioButton(dc, rectangle, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000D48CC File Offset: 0x000D2ACC
		public override void CPDrawScrollButton(Graphics dc, Rectangle area, ScrollButton type, ButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas || (state & ButtonState.Flat) == ButtonState.Flat || (state & ButtonState.Checked) == ButtonState.Checked)
			{
				base.CPDrawScrollButton(dc, area, type, state);
				return;
			}
			VisualStyleElement scrollButtonVisualStyleElement = ThemeVisualStyles.GetScrollButtonVisualStyleElement(type, state);
			if (!VisualStyleRenderer.IsElementDefined(scrollButtonVisualStyleElement))
			{
				base.CPDrawScrollButton(dc, area, type, state);
				return;
			}
			new VisualStyleRenderer(scrollButtonVisualStyleElement).DrawBackground(dc, area);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000D4940 File Offset: 0x000D2B40
		private static VisualStyleElement GetScrollButtonVisualStyleElement(ScrollButton type, ButtonState state)
		{
			switch (type)
			{
			case ScrollButton.Min:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.UpDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.UpPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.UpNormal;
			case ScrollButton.Left:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.LeftPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.LeftNormal;
			case ScrollButton.Right:
				if (ThemeVisualStyles.IsDisabled(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.RightDisabled;
				}
				if (ThemeVisualStyles.IsPressed(state))
				{
					return VisualStyleElement.ScrollBar.ArrowButton.RightPressed;
				}
				return VisualStyleElement.ScrollBar.ArrowButton.RightNormal;
			}
			if (ThemeVisualStyles.IsDisabled(state))
			{
				return VisualStyleElement.ScrollBar.ArrowButton.DownDisabled;
			}
			if (ThemeVisualStyles.IsPressed(state))
			{
				return VisualStyleElement.ScrollBar.ArrowButton.DownPressed;
			}
			return VisualStyleElement.ScrollBar.ArrowButton.DownNormal;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D4A0C File Offset: 0x000D2C0C
		private static bool IsDisabled(ButtonState state)
		{
			return (state & ButtonState.Inactive) == ButtonState.Inactive;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D4A1C File Offset: 0x000D2C1C
		private static bool IsPressed(ButtonState state)
		{
			return (state & ButtonState.Pushed) == ButtonState.Pushed;
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D4A2C File Offset: 0x000D2C2C
		public override bool DataGridViewRowHeaderCellDrawBackground(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !cell.DataGridView.EnableHeadersVisualStyles)
			{
				return base.DataGridViewRowHeaderCellDrawBackground(cell, g, bounds);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.DataGridViewRowHeaderCellGetVisualStyleElement(cell);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.DataGridViewRowHeaderCellDrawBackground(cell, g, bounds);
			}
			bounds.Width--;
			Bitmap bitmap = new Bitmap(bounds.Height, bounds.Width);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, bitmap.Size);
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			if (!ThemeVisualStyles.AreEqual(visualStyleElement, VisualStyleElement.Header.Item.Normal) && visualStyleRenderer.IsBackgroundPartiallyTransparent())
			{
				new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal).DrawBackground(graphics, rectangle);
			}
			visualStyleRenderer.DrawBackground(graphics, rectangle);
			graphics.Dispose();
			g.Transform = new Matrix(0f, 1f, 1f, 0f, 0f, 0f);
			g.DrawImage(bitmap, bounds.Y, bounds.X);
			bitmap.Dispose();
			g.ResetTransform();
			return true;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D4B44 File Offset: 0x000D2D44
		public override bool DataGridViewRowHeaderCellDrawSelectionBackground(DataGridViewRowHeaderCell cell)
		{
			return (ThemeVisualStyles.RenderClientAreas && cell.DataGridView.EnableHeadersVisualStyles && VisualStyleRenderer.IsElementDefined(ThemeVisualStyles.DataGridViewRowHeaderCellGetVisualStyleElement(cell))) || base.DataGridViewRowHeaderCellDrawSelectionBackground(cell);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000D4B84 File Offset: 0x000D2D84
		public override bool DataGridViewRowHeaderCellDrawBorder(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !cell.DataGridView.EnableHeadersVisualStyles || !VisualStyleRenderer.IsElementDefined(ThemeVisualStyles.DataGridViewRowHeaderCellGetVisualStyleElement(cell)))
			{
				return base.DataGridViewRowHeaderCellDrawBorder(cell, g, bounds);
			}
			g.DrawLine(cell.GetBorderPen(), bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
			return true;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D4BF4 File Offset: 0x000D2DF4
		private static VisualStyleElement DataGridViewRowHeaderCellGetVisualStyleElement(DataGridViewRowHeaderCell cell)
		{
			if (cell.DataGridView.PressedHeaderCell == cell)
			{
				return VisualStyleElement.Header.Item.Pressed;
			}
			if (cell.DataGridView.EnteredHeaderCell == cell)
			{
				return VisualStyleElement.Header.Item.Hot;
			}
			if (cell.OwningRow.SelectedInternal)
			{
				return VisualStyleElement.Header.Item.Pressed;
			}
			return VisualStyleElement.Header.Item.Normal;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000D4C4C File Offset: 0x000D2E4C
		public override bool DataGridViewColumnHeaderCellDrawBackground(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !cell.DataGridView.EnableHeadersVisualStyles || cell is DataGridViewTopLeftHeaderCell)
			{
				return base.DataGridViewColumnHeaderCellDrawBackground(cell, g, bounds);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.DataGridViewColumnHeaderCellGetVisualStyleElement(cell);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.DataGridViewColumnHeaderCellDrawBackground(cell, g, bounds);
			}
			bounds.Height--;
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			if (!ThemeVisualStyles.AreEqual(visualStyleElement, VisualStyleElement.Header.Item.Normal) && visualStyleRenderer.IsBackgroundPartiallyTransparent())
			{
				new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal).DrawBackground(g, bounds);
			}
			visualStyleRenderer.DrawBackground(g, bounds);
			return true;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000D4CF0 File Offset: 0x000D2EF0
		public override bool DataGridViewColumnHeaderCellDrawBorder(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !cell.DataGridView.EnableHeadersVisualStyles || cell is DataGridViewTopLeftHeaderCell || !VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Normal))
			{
				return base.DataGridViewColumnHeaderCellDrawBorder(cell, g, bounds);
			}
			g.DrawLine(cell.GetBorderPen(), bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
			return true;
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D4D6C File Offset: 0x000D2F6C
		private static VisualStyleElement DataGridViewColumnHeaderCellGetVisualStyleElement(DataGridViewColumnHeaderCell cell)
		{
			if (cell.DataGridView.PressedHeaderCell == cell)
			{
				return VisualStyleElement.Header.Item.Pressed;
			}
			if (cell.DataGridView.EnteredHeaderCell == cell)
			{
				return VisualStyleElement.Header.Item.Hot;
			}
			return VisualStyleElement.Header.Item.Normal;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000D4DAC File Offset: 0x000D2FAC
		public override bool DataGridViewHeaderCellHasPressedStyle(DataGridView dataGridView)
		{
			return (ThemeVisualStyles.RenderClientAreas && dataGridView.EnableHeadersVisualStyles && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Pressed)) || base.DataGridViewHeaderCellHasPressedStyle(dataGridView);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D4DE8 File Offset: 0x000D2FE8
		public override bool DataGridViewHeaderCellHasHotStyle(DataGridView dataGridView)
		{
			return (ThemeVisualStyles.RenderClientAreas && dataGridView.EnableHeadersVisualStyles && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Hot)) || base.DataGridViewHeaderCellHasHotStyle(dataGridView);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D4E24 File Offset: 0x000D3024
		protected override void DateTimePickerDrawBorder(DateTimePicker dateTimePicker, Graphics g, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DateTimePickerDrawBorder(dateTimePicker, g, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (!dateTimePicker.Enabled)
			{
				visualStyleElement = VisualStyleElement.DatePicker.DateBorder.Disabled;
			}
			else if (dateTimePicker.Entered)
			{
				visualStyleElement = VisualStyleElement.DatePicker.DateBorder.Hot;
			}
			else if (dateTimePicker.Focused)
			{
				visualStyleElement = VisualStyleElement.DatePicker.DateBorder.Focused;
			}
			else
			{
				visualStyleElement = VisualStyleElement.DatePicker.DateBorder.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DateTimePickerDrawBorder(dateTimePicker, g, clippingArea);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, new Rectangle(Point.Empty, dateTimePicker.Size), clippingArea);
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000D4EC0 File Offset: 0x000D30C0
		public override bool DateTimePickerBorderHasHotElementStyle
		{
			get
			{
				return (ThemeVisualStyles.RenderClientAreas && VisualStyleRenderer.IsElementDefined(VisualStyleElement.DatePicker.DateBorder.Hot)) || base.DateTimePickerBorderHasHotElementStyle;
			}
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D4EE4 File Offset: 0x000D30E4
		protected override void DateTimePickerDrawDropDownButton(DateTimePicker dateTimePicker, Graphics g, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DateTimePickerDrawDropDownButton(dateTimePicker, g, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (!dateTimePicker.Enabled)
			{
				visualStyleElement = VisualStyleElement.DatePicker.ShowCalendarButtonRight.Disabled;
			}
			else if (dateTimePicker.is_drop_down_visible)
			{
				visualStyleElement = VisualStyleElement.DatePicker.ShowCalendarButtonRight.Pressed;
			}
			else if (dateTimePicker.DropDownButtonEntered)
			{
				visualStyleElement = VisualStyleElement.DatePicker.ShowCalendarButtonRight.Hot;
			}
			else
			{
				visualStyleElement = VisualStyleElement.DatePicker.ShowCalendarButtonRight.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DateTimePickerDrawDropDownButton(dateTimePicker, g, clippingArea);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, dateTimePicker.drop_down_arrow_rect, clippingArea);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D4F78 File Offset: 0x000D3178
		public override Rectangle DateTimePickerGetDropDownButtonArea(DateTimePicker dateTimePicker)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.DateTimePickerGetDropDownButtonArea(dateTimePicker);
			}
			VisualStyleElement pressed = VisualStyleElement.DatePicker.ShowCalendarButtonRight.Pressed;
			if (!VisualStyleRenderer.IsElementDefined(pressed))
			{
				return base.DateTimePickerGetDropDownButtonArea(dateTimePicker);
			}
			Size size;
			size..ctor(34, 20);
			return new Rectangle(dateTimePicker.Width - size.Width, 0, size.Width, size.Height);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000D4FE0 File Offset: 0x000D31E0
		public override Rectangle DateTimePickerGetDateArea(DateTimePicker dateTimePicker)
		{
			if (!ThemeVisualStyles.RenderClientAreas || dateTimePicker.ShowUpDown)
			{
				return base.DateTimePickerGetDateArea(dateTimePicker);
			}
			VisualStyleElement normal = VisualStyleElement.DatePicker.DateBorder.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				return base.DateTimePickerGetDateArea(dateTimePicker);
			}
			Graphics graphics = dateTimePicker.CreateGraphics();
			Rectangle backgroundContentRectangle = new VisualStyleRenderer(normal).GetBackgroundContentRectangle(graphics, dateTimePicker.ClientRectangle);
			graphics.Dispose();
			backgroundContentRectangle.Width -= 34;
			return backgroundContentRectangle;
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000D5054 File Offset: 0x000D3254
		public override bool DateTimePickerDropDownButtonHasHotElementStyle
		{
			get
			{
				return (ThemeVisualStyles.RenderClientAreas && VisualStyleRenderer.IsElementDefined(VisualStyleElement.DatePicker.ShowCalendarButtonRight.Hot)) || base.DateTimePickerDropDownButtonHasHotElementStyle;
			}
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000D5078 File Offset: 0x000D3278
		protected override void ListViewDrawColumnHeaderBackground(ListView listView, ColumnHeader columnHeader, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ListViewDrawColumnHeaderBackground(listView, columnHeader, g, area, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				if (columnHeader.Pressed)
				{
					visualStyleElement = VisualStyleElement.Header.Item.Pressed;
				}
				else if (columnHeader == listView.EnteredColumnHeader)
				{
					visualStyleElement = VisualStyleElement.Header.Item.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.Header.Item.Normal;
				}
			}
			else
			{
				visualStyleElement = VisualStyleElement.Header.Item.Normal;
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ListViewDrawColumnHeaderBackground(listView, columnHeader, g, area, clippingArea);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D5110 File Offset: 0x000D3310
		protected override void ListViewDrawUnusedHeaderBackground(ListView listView, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ListViewDrawUnusedHeaderBackground(listView, g, area, clippingArea);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Header.Item.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.ListViewDrawUnusedHeaderBackground(listView, g, area, clippingArea);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(g, area, clippingArea);
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000D5160 File Offset: 0x000D3360
		public override bool ListViewHasHotHeaderStyle
		{
			get
			{
				return (ThemeVisualStyles.RenderClientAreas && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Hot)) || base.ListViewHasHotHeaderStyle;
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000D5184 File Offset: 0x000D3384
		public override int ListViewGetHeaderHeight(ListView listView, Font font)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.ListViewGetHeaderHeight(listView, font);
			}
			VisualStyleElement normal = VisualStyleElement.Header.Item.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				return base.ListViewGetHeaderHeight(listView, font);
			}
			Control control = null;
			Graphics graphics;
			if (listView == null)
			{
				control = new Control();
				graphics = control.CreateGraphics();
			}
			else
			{
				graphics = listView.CreateGraphics();
			}
			int height = new VisualStyleRenderer(normal).GetPartSize(graphics, ThemeSizeType.True).Height;
			graphics.Dispose();
			if (listView == null)
			{
				control.Dispose();
			}
			return height;
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000D520C File Offset: 0x000D340C
		public override void DrawGroupBox(Graphics dc, Rectangle area, GroupBox box)
		{
			GroupBoxRenderer.DrawGroupBox(dc, new Rectangle(Point.Empty, box.Size), box.Text, box.Font, (!(box.ForeColor == Control.DefaultForeColor)) ? box.ForeColor : Color.Empty, (!box.Enabled) ? GroupBoxState.Disabled : GroupBoxState.Normal);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000D5274 File Offset: 0x000D3474
		private Rectangle ManagedWindowGetTitleBarRectangle(InternalWindowManager wm)
		{
			return new Rectangle(0, 0, wm.Form.Width, this.ManagedWindowTitleBarHeight(wm) + this.ManagedWindowBorderWidth(wm) * ((!wm.IsMinimized) ? 1 : 2));
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000D52B8 File Offset: 0x000D34B8
		private Region ManagedWindowGetWindowRegion(Form form)
		{
			if (form.WindowManager is MdiWindowManager && form.WindowManager.IsMaximized)
			{
				return null;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleBarVisualStyleElement(form.WindowManager);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return null;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			if (!visualStyleRenderer.IsBackgroundPartiallyTransparent())
			{
				return null;
			}
			IDeviceContext measurementDeviceContext = ThemeVisualStyles.GetMeasurementDeviceContext();
			Rectangle rectangle = this.ManagedWindowGetTitleBarRectangle(form.WindowManager);
			Region backgroundRegion = visualStyleRenderer.GetBackgroundRegion(measurementDeviceContext, rectangle);
			ThemeVisualStyles.ReleaseMeasurementDeviceContext(measurementDeviceContext);
			backgroundRegion.Union(new Rectangle(0, rectangle.Bottom, form.Width, form.Height));
			return backgroundRegion;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000D535C File Offset: 0x000D355C
		public override void ManagedWindowOnSizeInitializedOrChanged(Form form)
		{
			base.ManagedWindowOnSizeInitializedOrChanged(form);
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return;
			}
			form.Region = this.ManagedWindowGetWindowRegion(form);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000D5380 File Offset: 0x000D3580
		protected override Rectangle ManagedWindowDrawTitleBarAndBorders(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return base.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleBarVisualStyleElement(wm);
			VisualStyleElement visualStyleElement2;
			VisualStyleElement visualStyleElement3;
			VisualStyleElement visualStyleElement4;
			ThemeVisualStyles.ManagedWindowGetBorderVisualStyleElements(wm, out visualStyleElement2, out visualStyleElement3, out visualStyleElement4);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement) || (!wm.IsMinimized && (!VisualStyleRenderer.IsElementDefined(visualStyleElement2) || !VisualStyleRenderer.IsElementDefined(visualStyleElement3) || !VisualStyleRenderer.IsElementDefined(visualStyleElement4))))
			{
				return base.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
			Rectangle rectangle = this.ManagedWindowGetTitleBarRectangle(wm);
			visualStyleRenderer.DrawBackground(dc, rectangle, clip);
			if (!wm.IsMinimized)
			{
				int num = this.ManagedWindowBorderWidth(wm);
				visualStyleRenderer.SetParameters(visualStyleElement2);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(0, rectangle.Bottom, num, wm.Form.Height - rectangle.Bottom), clip);
				visualStyleRenderer.SetParameters(visualStyleElement3);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(wm.Form.Width - num, rectangle.Bottom, num, wm.Form.Height - rectangle.Bottom), clip);
				visualStyleRenderer.SetParameters(visualStyleElement4);
				visualStyleRenderer.DrawBackground(dc, new Rectangle(0, wm.Form.Height - num, wm.Form.Width, num), clip);
			}
			return rectangle;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000D54CC File Offset: 0x000D36CC
		private static FormWindowState ManagedWindowGetWindowState(InternalWindowManager wm)
		{
			return wm.GetWindowState();
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D54D4 File Offset: 0x000D36D4
		private static bool ManagedWindowIsDisabled(InternalWindowManager wm)
		{
			return !wm.Form.Enabled;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000D54E4 File Offset: 0x000D36E4
		private static bool ManagedWindowIsActive(InternalWindowManager wm)
		{
			return wm.IsActive;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D54EC File Offset: 0x000D36EC
		private static VisualStyleElement ManagedWindowGetTitleBarVisualStyleElement(InternalWindowManager wm)
		{
			if (wm.IsToolWindow)
			{
				FormWindowState formWindowState = ThemeVisualStyles.ManagedWindowGetWindowState(wm);
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState != FormWindowState.Maximized)
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.SmallCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.SmallCaption.Active;
						}
						return VisualStyleElement.Window.SmallCaption.Inactive;
					}
					else
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.SmallMaxCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.SmallMaxCaption.Active;
						}
						return VisualStyleElement.Window.SmallMaxCaption.Inactive;
					}
				}
				else
				{
					if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
					{
						return VisualStyleElement.Window.SmallMinCaption.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowIsActive(wm))
					{
						return VisualStyleElement.Window.SmallMinCaption.Active;
					}
					return VisualStyleElement.Window.SmallMinCaption.Inactive;
				}
			}
			else
			{
				FormWindowState formWindowState = ThemeVisualStyles.ManagedWindowGetWindowState(wm);
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState != FormWindowState.Maximized)
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.Caption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.Caption.Active;
						}
						return VisualStyleElement.Window.Caption.Inactive;
					}
					else
					{
						if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
						{
							return VisualStyleElement.Window.MaxCaption.Disabled;
						}
						if (ThemeVisualStyles.ManagedWindowIsActive(wm))
						{
							return VisualStyleElement.Window.MaxCaption.Active;
						}
						return VisualStyleElement.Window.MaxCaption.Inactive;
					}
				}
				else
				{
					if (ThemeVisualStyles.ManagedWindowIsDisabled(wm))
					{
						return VisualStyleElement.Window.MinCaption.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowIsActive(wm))
					{
						return VisualStyleElement.Window.MinCaption.Active;
					}
					return VisualStyleElement.Window.MinCaption.Inactive;
				}
			}
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D5628 File Offset: 0x000D3828
		private static void ManagedWindowGetBorderVisualStyleElements(InternalWindowManager wm, out VisualStyleElement left, out VisualStyleElement right, out VisualStyleElement bottom)
		{
			bool flag = !ThemeVisualStyles.ManagedWindowIsDisabled(wm) && ThemeVisualStyles.ManagedWindowIsActive(wm);
			if (wm.IsToolWindow)
			{
				if (flag)
				{
					left = VisualStyleElement.Window.SmallFrameLeft.Active;
					right = VisualStyleElement.Window.SmallFrameRight.Active;
					bottom = VisualStyleElement.Window.SmallFrameBottom.Active;
				}
				else
				{
					left = VisualStyleElement.Window.SmallFrameLeft.Inactive;
					right = VisualStyleElement.Window.SmallFrameRight.Inactive;
					bottom = VisualStyleElement.Window.SmallFrameBottom.Inactive;
				}
			}
			else if (flag)
			{
				left = VisualStyleElement.Window.FrameLeft.Active;
				right = VisualStyleElement.Window.FrameRight.Active;
				bottom = VisualStyleElement.Window.FrameBottom.Active;
			}
			else
			{
				left = VisualStyleElement.Window.FrameLeft.Inactive;
				right = VisualStyleElement.Window.FrameRight.Inactive;
				bottom = VisualStyleElement.Window.FrameBottom.Inactive;
			}
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000D56C4 File Offset: 0x000D38C4
		public override bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form)
		{
			if (ThemeVisualStyles.render_non_client_areas && (button.State & ButtonState.Inactive) != ButtonState.Inactive)
			{
				VisualStyleElement visualStyleElement;
				if (ThemeVisualStyles.ManagedWindowIsMaximizedMdiChild(form))
				{
					switch (button.Caption)
					{
					case CaptionButton.Close:
						visualStyleElement = VisualStyleElement.Window.MdiCloseButton.Hot;
						goto IL_007D;
					case CaptionButton.Minimize:
						visualStyleElement = VisualStyleElement.Window.MdiMinButton.Hot;
						goto IL_007D;
					case CaptionButton.Help:
						visualStyleElement = VisualStyleElement.Window.MdiHelpButton.Hot;
						goto IL_007D;
					}
					visualStyleElement = VisualStyleElement.Window.MdiRestoreButton.Hot;
					IL_007D:;
				}
				else if (form.WindowManager.IsToolWindow)
				{
					visualStyleElement = VisualStyleElement.Window.SmallCloseButton.Hot;
				}
				else
				{
					switch (button.Caption)
					{
					case CaptionButton.Close:
						visualStyleElement = VisualStyleElement.Window.CloseButton.Hot;
						goto IL_00FA;
					case CaptionButton.Minimize:
						visualStyleElement = VisualStyleElement.Window.MinButton.Hot;
						goto IL_00FA;
					case CaptionButton.Maximize:
						visualStyleElement = VisualStyleElement.Window.MaxButton.Hot;
						goto IL_00FA;
					case CaptionButton.Help:
						visualStyleElement = VisualStyleElement.Window.HelpButton.Hot;
						goto IL_00FA;
					}
					visualStyleElement = VisualStyleElement.Window.RestoreButton.Hot;
				}
				IL_00FA:
				if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
				{
					return true;
				}
			}
			return base.ManagedWindowTitleButtonHasHotElementStyle(button, form);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000D57E0 File Offset: 0x000D39E0
		private static bool ManagedWindowIsMaximizedMdiChild(Form form)
		{
			return form.WindowManager is MdiWindowManager && ThemeVisualStyles.ManagedWindowGetWindowState(form.WindowManager) == FormWindowState.Maximized;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000D5804 File Offset: 0x000D3A04
		private static bool ManagedWindowTitleButtonIsDisabled(TitleButton button, InternalWindowManager wm)
		{
			return (button.State & ButtonState.Inactive) == ButtonState.Inactive;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000D581C File Offset: 0x000D3A1C
		private static bool ManagedWindowTitleButtonIsPressed(TitleButton button)
		{
			return (button.State & ButtonState.Pushed) == ButtonState.Pushed;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000D5834 File Offset: 0x000D3A34
		private static VisualStyleElement ManagedWindowGetTitleButtonVisualStyleElement(TitleButton button, Form form)
		{
			if (form.WindowManager.IsToolWindow)
			{
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
				{
					return VisualStyleElement.Window.SmallCloseButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.SmallCloseButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.SmallCloseButton.Hot;
				}
				return VisualStyleElement.Window.SmallCloseButton.Normal;
			}
			else
			{
				switch (button.Caption)
				{
				case CaptionButton.Close:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.CloseButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.CloseButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.CloseButton.Hot;
					}
					return VisualStyleElement.Window.CloseButton.Normal;
				case CaptionButton.Minimize:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.MinButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.MinButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.MinButton.Hot;
					}
					return VisualStyleElement.Window.MinButton.Normal;
				case CaptionButton.Maximize:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.MaxButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.MaxButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.MaxButton.Hot;
					}
					return VisualStyleElement.Window.MaxButton.Normal;
				case CaptionButton.Help:
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
					{
						return VisualStyleElement.Window.HelpButton.Disabled;
					}
					if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
					{
						return VisualStyleElement.Window.HelpButton.Pressed;
					}
					if (button.Entered)
					{
						return VisualStyleElement.Window.HelpButton.Hot;
					}
					return VisualStyleElement.Window.HelpButton.Normal;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, form.WindowManager))
				{
					return VisualStyleElement.Window.RestoreButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.RestoreButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.RestoreButton.Hot;
				}
				return VisualStyleElement.Window.RestoreButton.Normal;
			}
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000D59F0 File Offset: 0x000D3BF0
		protected override void ManagedWindowDrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				base.ManagedWindowDrawTitleButton(dc, button, clip, form);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetTitleButtonVisualStyleElement(button, form);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ManagedWindowDrawTitleButton(dc, button, clip, form);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, button.Rectangle, clip);
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000D5A48 File Offset: 0x000D3C48
		public override Size ManagedWindowButtonSize(InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				return base.ManagedWindowButtonSize(wm);
			}
			VisualStyleElement visualStyleElement = ((!wm.IsToolWindow || wm.IsMinimized) ? VisualStyleElement.Window.CloseButton.Normal : VisualStyleElement.Window.SmallCloseButton.Normal);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.ManagedWindowButtonSize(wm);
			}
			IDeviceContext measurementDeviceContext = ThemeVisualStyles.GetMeasurementDeviceContext();
			Size partSize = new VisualStyleRenderer(visualStyleElement).GetPartSize(measurementDeviceContext, ThemeSizeType.True);
			ThemeVisualStyles.ReleaseMeasurementDeviceContext(measurementDeviceContext);
			return partSize;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000D5ABC File Offset: 0x000D3CBC
		public override void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm)
		{
			if (!ThemeVisualStyles.render_non_client_areas)
			{
				base.ManagedWindowDrawMenuButton(dc, button, clip, wm);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ManagedWindowGetMenuButtonVisualStyleElement(button, wm);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.ManagedWindowDrawMenuButton(dc, button, clip, wm);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, button.Rectangle, clip);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D5B14 File Offset: 0x000D3D14
		private static VisualStyleElement ManagedWindowGetMenuButtonVisualStyleElement(TitleButton button, InternalWindowManager wm)
		{
			switch (button.Caption)
			{
			case CaptionButton.Close:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiCloseButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiCloseButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiCloseButton.Hot;
				}
				return VisualStyleElement.Window.MdiCloseButton.Normal;
			case CaptionButton.Minimize:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiMinButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiMinButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiMinButton.Hot;
				}
				return VisualStyleElement.Window.MdiMinButton.Normal;
			case CaptionButton.Help:
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
				{
					return VisualStyleElement.Window.MdiHelpButton.Disabled;
				}
				if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
				{
					return VisualStyleElement.Window.MdiHelpButton.Pressed;
				}
				if (button.Entered)
				{
					return VisualStyleElement.Window.MdiHelpButton.Hot;
				}
				return VisualStyleElement.Window.MdiHelpButton.Normal;
			}
			if (ThemeVisualStyles.ManagedWindowTitleButtonIsDisabled(button, wm))
			{
				return VisualStyleElement.Window.MdiRestoreButton.Disabled;
			}
			if (ThemeVisualStyles.ManagedWindowTitleButtonIsPressed(button))
			{
				return VisualStyleElement.Window.MdiRestoreButton.Pressed;
			}
			if (button.Entered)
			{
				return VisualStyleElement.Window.MdiRestoreButton.Hot;
			}
			return VisualStyleElement.Window.MdiRestoreButton.Normal;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000D5C30 File Offset: 0x000D3E30
		public override void DrawProgressBar(Graphics dc, Rectangle clip_rect, ProgressBar ctrl)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !VisualStyleRenderer.IsElementDefined(VisualStyleElement.ProgressBar.Bar.Normal) || !VisualStyleRenderer.IsElementDefined(VisualStyleElement.ProgressBar.Chunk.Normal))
			{
				base.DrawProgressBar(dc, clip_rect, ctrl);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Bar.Normal);
			visualStyleRenderer.DrawBackground(dc, ctrl.ClientRectangle, clip_rect);
			Rectangle backgroundContentRectangle = visualStyleRenderer.GetBackgroundContentRectangle(dc, new Rectangle(Point.Empty, ctrl.Size));
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
			int num = int.MaxValue;
			int num2 = backgroundContentRectangle.X;
			int style = (int)ctrl.Style;
			int num3 = style;
			if (num3 != 1)
			{
				if (num3 == 2)
				{
					int num4 = (int)(DateTime.Now - ctrl.start).TotalMilliseconds;
					double num5 = (double)num4 % (double)ctrl.MarqueeAnimationSpeed / (double)ctrl.MarqueeAnimationSpeed;
					num = 5;
					num2 = backgroundContentRectangle.X + (int)((double)backgroundContentRectangle.Width * num5);
				}
				int num6 = visualStyleRenderer.GetInteger(IntegerProperty.ProgressChunkSize);
				num6 = Math.Max(num6, 0);
				int num7 = (int)((double)(ctrl.Value - ctrl.Minimum) * (double)backgroundContentRectangle.Width / (double)Math.Max(ctrl.Maximum - ctrl.Minimum, 1)) + backgroundContentRectangle.X;
				int num8 = 0;
				int num9 = num6 + visualStyleRenderer.GetInteger(IntegerProperty.ProgressSpaceSize);
				Rectangle rectangle;
				rectangle..ctor(num2, backgroundContentRectangle.Y, num6, backgroundContentRectangle.Height);
				for (;;)
				{
					if (num != 2147483647)
					{
						if (num8 == num)
						{
							break;
						}
						if (rectangle.Right >= backgroundContentRectangle.Width)
						{
							rectangle.X -= backgroundContentRectangle.Width;
						}
					}
					else
					{
						if (rectangle.X >= num7)
						{
							break;
						}
						if (rectangle.Right >= num7)
						{
							if (num7 != backgroundContentRectangle.Right)
							{
								break;
							}
							rectangle.Width = num7 - rectangle.X;
						}
					}
					if (clip_rect.IntersectsWith(rectangle))
					{
						visualStyleRenderer.DrawBackground(dc, rectangle, clip_rect);
					}
					rectangle.X += num9;
					num8++;
				}
			}
			else
			{
				backgroundContentRectangle.Width = (int)((double)backgroundContentRectangle.Width * ((double)(ctrl.Value - ctrl.Minimum) / (double)Math.Max(ctrl.Maximum - ctrl.Minimum, 1)));
				visualStyleRenderer.DrawBackground(dc, backgroundContentRectangle, clip_rect);
			}
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000D5EAC File Offset: 0x000D40AC
		protected override void RadioButton_DrawButton(RadioButton radio_button, Graphics dc, ButtonState state, Rectangle radiobutton_rectangle)
		{
			if (radio_button.Appearance == Appearance.Normal && radio_button.FlatStyle == FlatStyle.System)
			{
				RadioButtonRenderer.DrawRadioButton(dc, new Point(radiobutton_rectangle.Left, radiobutton_rectangle.Top), ThemeVisualStyles.GetRadioButtonState(radio_button));
				return;
			}
			base.RadioButton_DrawButton(radio_button, dc, state, radiobutton_rectangle);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000D5EFC File Offset: 0x000D40FC
		private static RadioButtonState GetRadioButtonState(RadioButton checkBox)
		{
			if (checkBox.Checked)
			{
				if (!checkBox.Enabled)
				{
					return RadioButtonState.CheckedDisabled;
				}
				if (checkBox.Pressed)
				{
					return RadioButtonState.CheckedPressed;
				}
				if (checkBox.Entered)
				{
					return RadioButtonState.CheckedHot;
				}
				return RadioButtonState.CheckedNormal;
			}
			else
			{
				if (!checkBox.Enabled)
				{
					return RadioButtonState.UncheckedDisabled;
				}
				if (checkBox.Pressed)
				{
					return RadioButtonState.UncheckedPressed;
				}
				if (checkBox.Entered)
				{
					return RadioButtonState.UncheckedHot;
				}
				return RadioButtonState.UncheckedNormal;
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000D5F68 File Offset: 0x000D4168
		public override void DrawScrollBar(Graphics dc, Rectangle clip, ScrollBar bar)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.ScrollBarAreElementsDefined)
			{
				base.DrawScrollBar(dc, clip, bar);
				return;
			}
			int scrollbutton_width = bar.scrollbutton_width;
			int scrollbutton_height = bar.scrollbutton_height;
			if (bar.vert)
			{
				bar.FirstArrowArea = new Rectangle(0, 0, bar.Width, scrollbutton_height);
				bar.SecondArrowArea = new Rectangle(0, bar.ClientRectangle.Height - scrollbutton_height, bar.Width, scrollbutton_height);
				Rectangle thumbPos = bar.ThumbPos;
				thumbPos.Width = bar.Width;
				bar.ThumbPos = thumbPos;
				VisualStyleElement visualStyleElement;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed;
				}
				else
				{
					visualStyleElement = ((!bar.Enabled) ? VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled : VisualStyleElement.ScrollBar.LowerTrackVertical.Normal);
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle;
				rectangle..ctor(0, 0, bar.ClientRectangle.Width, bar.ThumbPos.Top);
				if (clip.IntersectsWith(rectangle))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle, clip);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed;
				}
				else
				{
					visualStyleElement = ((!bar.Enabled) ? VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled : VisualStyleElement.ScrollBar.LowerTrackVertical.Normal);
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle2;
				rectangle2..ctor(0, bar.ThumbPos.Bottom, bar.ClientRectangle.Width, bar.ClientRectangle.Height - bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle2))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle2, clip);
				}
				if (clip.IntersectsWith(bar.FirstArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpDisabled;
					}
					else if (bar.firstbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpPressed;
					}
					else if (bar.FirstButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.UpNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.FirstArrowArea);
				}
				if (clip.IntersectsWith(bar.SecondArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownDisabled;
					}
					else if (bar.secondbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownPressed;
					}
					else if (bar.SecondButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.DownNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.SecondArrowArea);
				}
				if (!bar.Enabled)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled;
				}
				else if (bar.ThumbPressed)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed;
				}
				else if (bar.ThumbEntered)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal;
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
				if (bar.Enabled && bar.ThumbPos.Height >= 20)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.GripperVertical.Normal;
					if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
					{
						visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
						visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
					}
				}
			}
			else
			{
				bar.FirstArrowArea = new Rectangle(0, 0, scrollbutton_width, bar.Height);
				bar.SecondArrowArea = new Rectangle(bar.ClientRectangle.Width - scrollbutton_width, 0, scrollbutton_width, bar.Height);
				Rectangle thumbPos2 = bar.ThumbPos;
				thumbPos2.Height = bar.Height;
				bar.ThumbPos = thumbPos2;
				VisualStyleElement visualStyleElement;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed;
				}
				else
				{
					visualStyleElement = ((!bar.Enabled) ? VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled : VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal);
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle3;
				rectangle3..ctor(0, 0, bar.ThumbPos.Left, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle3))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle3, clip);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed;
				}
				else
				{
					visualStyleElement = ((!bar.Enabled) ? VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled : VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal);
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				Rectangle rectangle4;
				rectangle4..ctor(bar.ThumbPos.Right, 0, bar.ClientRectangle.Width - bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle4))
				{
					visualStyleRenderer.DrawBackground(dc, rectangle4, clip);
				}
				if (clip.IntersectsWith(bar.FirstArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled;
					}
					else if (bar.firstbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftPressed;
					}
					else if (bar.FirstButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.LeftNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.FirstArrowArea);
				}
				if (clip.IntersectsWith(bar.SecondArrowArea))
				{
					if (!bar.Enabled)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightDisabled;
					}
					else if (bar.secondbutton_state == ButtonState.Pushed)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightPressed;
					}
					else if (bar.SecondButtonEntered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightHot;
					}
					else if (ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles && bar.Entered)
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightHover;
					}
					else
					{
						visualStyleElement = VisualStyleElement.ScrollBar.ArrowButton.RightNormal;
					}
					visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
					visualStyleRenderer.DrawBackground(dc, bar.SecondArrowArea);
				}
				if (!bar.Enabled)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled;
				}
				else if (bar.ThumbPressed)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed;
				}
				else if (bar.ThumbEntered)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot;
				}
				else
				{
					visualStyleElement = VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal;
				}
				visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
				visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
				if (bar.Enabled && bar.ThumbPos.Height >= 20)
				{
					visualStyleElement = VisualStyleElement.ScrollBar.GripperHorizontal.Normal;
					if (VisualStyleRenderer.IsElementDefined(visualStyleElement))
					{
						visualStyleRenderer = new VisualStyleRenderer(visualStyleElement);
						visualStyleRenderer.DrawBackground(dc, bar.ThumbPos, clip);
					}
				}
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000D660C File Offset: 0x000D480C
		public override bool ScrollBarHasHotElementStyles
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ScrollBarHasHotElementStyles;
				}
				return ThemeVisualStyles.ScrollBarAreElementsDefined;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000D6624 File Offset: 0x000D4824
		public override bool ScrollBarHasPressedThumbStyle
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ScrollBarHasPressedThumbStyle;
				}
				return ThemeVisualStyles.ScrollBarAreElementsDefined;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000D663C File Offset: 0x000D483C
		public override bool ScrollBarHasHoverArrowButtonStyle
		{
			get
			{
				if (ThemeVisualStyles.RenderClientAreas && ThemeVisualStyles.ScrollBarHasHoverArrowButtonStyleVisualStyles)
				{
					return ThemeVisualStyles.ScrollBarAreElementsDefined;
				}
				return base.ScrollBarHasHoverArrowButtonStyle;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000D666C File Offset: 0x000D486C
		private static bool ScrollBarAreElementsDefined
		{
			get
			{
				return VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ArrowButton.DownDisabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.ThumbButtonVertical.Disabled) && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ScrollBar.UpperTrackVertical.Disabled);
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000D66E0 File Offset: 0x000D48E0
		protected override void DrawStatusBarBackground(Graphics dc, Rectangle clip, StatusBar sb)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawStatusBarBackground(dc, clip, sb);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Status.Bar.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.DrawStatusBarBackground(dc, clip, sb);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(dc, sb.ClientRectangle, clip);
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000D6730 File Offset: 0x000D4930
		protected override void DrawStatusBarSizingGrip(Graphics dc, Rectangle clip, StatusBar sb, Rectangle area)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawStatusBarSizingGrip(dc, clip, sb, area);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Status.Gripper.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.DrawStatusBarSizingGrip(dc, clip, sb, area);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(normal);
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, visualStyleRenderer.GetPartSize(dc, ThemeSizeType.True));
			rectangle.X = sb.Width - rectangle.Width;
			rectangle.Y = sb.Height - rectangle.Height;
			visualStyleRenderer.DrawBackground(dc, rectangle, clip);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000D67C0 File Offset: 0x000D49C0
		protected override void DrawStatusBarPanelBackground(Graphics dc, Rectangle area, StatusBarPanel panel)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawStatusBarPanelBackground(dc, area, panel);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.Status.Pane.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.DrawStatusBarPanelBackground(dc, area, panel);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(dc, area);
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000D680C File Offset: 0x000D4A0C
		private static bool TextBoxBaseShouldPaint(TextBoxBase textBoxBase)
		{
			return textBoxBase.BorderStyle == BorderStyle.Fixed3D;
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000D6818 File Offset: 0x000D4A18
		private static VisualStyleElement TextBoxBaseGetVisualStyleElement(TextBoxBase textBoxBase)
		{
			if (!textBoxBase.Enabled)
			{
				return VisualStyleElement.TextBox.TextEdit.Disabled;
			}
			if (textBoxBase.ReadOnly)
			{
				return VisualStyleElement.TextBox.TextEdit.ReadOnly;
			}
			if (textBoxBase.Entered)
			{
				return VisualStyleElement.TextBox.TextEdit.Hot;
			}
			if (textBoxBase.Focused)
			{
				return VisualStyleElement.TextBox.TextEdit.Focused;
			}
			return VisualStyleElement.TextBox.TextEdit.Normal;
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000D6870 File Offset: 0x000D4A70
		public override void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				base.TextBoxBaseFillBackground(textBoxBase, g, clippingArea);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TextBoxBaseFillBackground(textBoxBase, g, clippingArea);
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, textBoxBase.Size);
			rectangle.X -= (rectangle.Width - textBoxBase.ClientSize.Width) / 2;
			rectangle.Y -= (rectangle.Height - textBoxBase.ClientSize.Height) / 2;
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, rectangle, clippingArea);
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000D6924 File Offset: 0x000D4B24
		public override bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				return base.TextBoxBaseHandleWmNcPaint(textBoxBase, ref m);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.TextBoxBaseHandleWmNcPaint(textBoxBase, ref m);
			}
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, textBoxBase.Handle, false);
			new VisualStyleRenderer(visualStyleElement).DrawBackgroundExcludingArea(paintEventArgs.Graphics, new Rectangle(Point.Empty, textBoxBase.Size), new Rectangle(new Point((textBoxBase.Width - textBoxBase.ClientSize.Width) / 2, (textBoxBase.Height - textBoxBase.ClientSize.Height) / 2), textBoxBase.ClientSize));
			XplatUI.PaintEventEnd(ref m, textBoxBase.Handle, false);
			return true;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000D69E8 File Offset: 0x000D4BE8
		public override bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !ThemeVisualStyles.TextBoxBaseShouldPaint(textBoxBase))
			{
				return base.TextBoxBaseShouldPaintBackground(textBoxBase);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TextBoxBaseGetVisualStyleElement(textBoxBase);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.TextBoxBaseShouldPaintBackground(textBoxBase);
			}
			return new VisualStyleRenderer(visualStyleElement).IsBackgroundPartiallyTransparent();
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000D6A38 File Offset: 0x000D4C38
		private static bool ToolBarIsDisabled(ToolBarItem item)
		{
			return !item.Button.Enabled;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000D6A48 File Offset: 0x000D4C48
		private static bool ToolBarIsPressed(ToolBarItem item)
		{
			return item.Pressed;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000D6A50 File Offset: 0x000D4C50
		private static bool ToolBarIsChecked(ToolBarItem item)
		{
			return item.Button.Pushed;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000D6A60 File Offset: 0x000D4C60
		private static bool ToolBarIsHot(ToolBarItem item)
		{
			return item.Hilight;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000D6A68 File Offset: 0x000D4C68
		protected override void DrawToolBarButtonBorder(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarButtonBorder(dc, item, is_flat);
				return;
			}
			if (item.Button.Style == ToolBarButtonStyle.Separator)
			{
				return;
			}
			VisualStyleElement visualStyleElement;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton)
			{
				visualStyleElement = ThemeVisualStyles.ToolBarGetDropDownButtonVisualStyleElement(item);
			}
			else
			{
				visualStyleElement = ThemeVisualStyles.ToolBarGetButtonVisualStyleElement(item);
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarButtonBorder(dc, item, is_flat);
				return;
			}
			Rectangle rectangle = item.Rectangle;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton && item.Button.Parent.DropDownArrows)
			{
				rectangle.Width -= this.ToolBarDropDownWidth;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000D6B24 File Offset: 0x000D4D24
		private static VisualStyleElement ToolBarGetDropDownButtonVisualStyleElement(ToolBarItem item)
		{
			if (item.Button.Parent.DropDownArrows)
			{
				if (ThemeVisualStyles.ToolBarIsDisabled(item))
				{
					return VisualStyleElement.ToolBar.SplitButton.Disabled;
				}
				if (ThemeVisualStyles.ToolBarIsPressed(item))
				{
					return VisualStyleElement.ToolBar.SplitButton.Pressed;
				}
				if (ThemeVisualStyles.ToolBarIsChecked(item))
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.SplitButton.HotChecked;
					}
					return VisualStyleElement.ToolBar.SplitButton.Checked;
				}
				else
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.SplitButton.Hot;
					}
					return VisualStyleElement.ToolBar.SplitButton.Normal;
				}
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsDisabled(item))
				{
					return VisualStyleElement.ToolBar.DropDownButton.Disabled;
				}
				if (ThemeVisualStyles.ToolBarIsPressed(item))
				{
					return VisualStyleElement.ToolBar.DropDownButton.Pressed;
				}
				if (ThemeVisualStyles.ToolBarIsChecked(item))
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.DropDownButton.HotChecked;
					}
					return VisualStyleElement.ToolBar.DropDownButton.Checked;
				}
				else
				{
					if (ThemeVisualStyles.ToolBarIsHot(item))
					{
						return VisualStyleElement.ToolBar.DropDownButton.Hot;
					}
					return VisualStyleElement.ToolBar.DropDownButton.Normal;
				}
			}
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000D6BFC File Offset: 0x000D4DFC
		private static VisualStyleElement ToolBarGetButtonVisualStyleElement(ToolBarItem item)
		{
			if (ThemeVisualStyles.ToolBarIsDisabled(item))
			{
				return VisualStyleElement.ToolBar.Button.Disabled;
			}
			if (ThemeVisualStyles.ToolBarIsPressed(item))
			{
				return VisualStyleElement.ToolBar.Button.Pressed;
			}
			if (ThemeVisualStyles.ToolBarIsChecked(item))
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.Button.HotChecked;
				}
				return VisualStyleElement.ToolBar.Button.Checked;
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.Button.Hot;
				}
				return VisualStyleElement.ToolBar.Button.Normal;
			}
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000D6C64 File Offset: 0x000D4E64
		protected override void DrawToolBarSeparator(Graphics dc, ToolBarItem item)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarSeparator(dc, item);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ToolBarGetSeparatorVisualStyleElement(item);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarSeparator(dc, item);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, item.Rectangle);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000D6CB4 File Offset: 0x000D4EB4
		private static VisualStyleElement ToolBarGetSeparatorVisualStyleElement(ToolBarItem toolBarItem)
		{
			return (!toolBarItem.Button.Parent.Vertical) ? VisualStyleElement.ToolBar.SeparatorHorizontal.Normal : VisualStyleElement.ToolBar.SeparatorVertical.Normal;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000D6CE8 File Offset: 0x000D4EE8
		protected override void DrawToolBarToggleButtonBackground(Graphics dc, ToolBarItem item)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !VisualStyleRenderer.IsElementDefined(ThemeVisualStyles.ToolBarGetButtonVisualStyleElement(item)))
			{
				base.DrawToolBarToggleButtonBackground(dc, item);
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000D6D18 File Offset: 0x000D4F18
		protected override void DrawToolBarDropDownArrow(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.DrawToolBarDropDownArrow(dc, item, is_flat);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.ToolBarGetDropDownArrowVisualStyleElement(item);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.DrawToolBarDropDownArrow(dc, item, is_flat);
				return;
			}
			Rectangle rectangle = item.Rectangle;
			rectangle.X = item.Rectangle.Right - this.ToolBarDropDownWidth;
			rectangle.Width = this.ToolBarDropDownWidth;
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, rectangle);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000D6D94 File Offset: 0x000D4F94
		private static VisualStyleElement ToolBarGetDropDownArrowVisualStyleElement(ToolBarItem item)
		{
			if (ThemeVisualStyles.ToolBarIsDisabled(item))
			{
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Disabled;
			}
			if (ThemeVisualStyles.ToolBarIsPressed(item))
			{
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Pressed;
			}
			if (ThemeVisualStyles.ToolBarIsChecked(item))
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.SplitButtonDropDown.HotChecked;
				}
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Checked;
			}
			else
			{
				if (ThemeVisualStyles.ToolBarIsHot(item))
				{
					return VisualStyleElement.ToolBar.SplitButtonDropDown.Hot;
				}
				return VisualStyleElement.ToolBar.SplitButtonDropDown.Normal;
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000D6DFC File Offset: 0x000D4FFC
		public override bool ToolBarHasHotElementStyles(ToolBar toolBar)
		{
			return ThemeVisualStyles.RenderClientAreas || base.ToolBarHasHotElementStyles(toolBar);
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000D6E14 File Offset: 0x000D5014
		public override bool ToolBarHasHotCheckedElementStyles
		{
			get
			{
				return ThemeVisualStyles.RenderClientAreas || base.ToolBarHasHotCheckedElementStyles;
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000D6E28 File Offset: 0x000D5028
		protected override void ToolTipDrawBackground(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.ToolTipDrawBackground(dc, clip_rectangle, control);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.ToolTipDrawBackground(dc, clip_rectangle, control);
				return;
			}
			new VisualStyleRenderer(normal).DrawBackground(dc, control.ClientRectangle);
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000D6E78 File Offset: 0x000D5078
		public override bool ToolTipTransparentBackground
		{
			get
			{
				if (!ThemeVisualStyles.RenderClientAreas)
				{
					return base.ToolTipTransparentBackground;
				}
				VisualStyleElement normal = VisualStyleElement.ToolTip.Standard.Normal;
				if (!VisualStyleRenderer.IsElementDefined(normal))
				{
					return base.ToolTipTransparentBackground;
				}
				return new VisualStyleRenderer(normal).IsBackgroundPartiallyTransparent();
			}
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000D6EBC File Offset: 0x000D50BC
		protected override Size TrackBarGetThumbSize(TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				return base.TrackBarGetThumbSize(trackBar);
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetThumbVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				return base.TrackBarGetThumbSize(trackBar);
			}
			Graphics graphics = trackBar.CreateGraphics();
			Size partSize = new VisualStyleRenderer(visualStyleElement).GetPartSize(graphics, ThemeSizeType.True);
			graphics.Dispose();
			return (trackBar.Orientation != Orientation.Horizontal) ? ThemeVisualStyles.TrackBarRotateVerticalThumbSize(partSize) : partSize;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000D6F28 File Offset: 0x000D5128
		private static VisualStyleElement TrackBarGetThumbVisualStyleElement(TrackBar trackBar)
		{
			if (trackBar.Orientation == Orientation.Horizontal)
			{
				switch (trackBar.TickStyle)
				{
				case TickStyle.None:
				case TickStyle.BottomRight:
					return ThemeVisualStyles.TrackBarGetHorizontalThumbBottomVisualStyleElement(trackBar);
				case TickStyle.TopLeft:
					return ThemeVisualStyles.TrackBarGetHorizontalThumbTopVisualStyleElement(trackBar);
				default:
					return ThemeVisualStyles.TrackBarGetHorizontalThumbVisualStyleElement(trackBar);
				}
			}
			else
			{
				switch (trackBar.TickStyle)
				{
				case TickStyle.None:
				case TickStyle.BottomRight:
					return ThemeVisualStyles.TrackBarGetVerticalThumbRightVisualStyleElement(trackBar);
				case TickStyle.TopLeft:
					return ThemeVisualStyles.TrackBarGetVerticalThumbLeftVisualStyleElement(trackBar);
				default:
					return ThemeVisualStyles.TrackBarGetVerticalThumbVisualStyleElement(trackBar);
				}
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000D6FA8 File Offset: 0x000D51A8
		private static Size TrackBarRotateVerticalThumbSize(Size value)
		{
			int width = value.Width;
			value.Width = value.Height;
			value.Height = width;
			return value;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000D6FD4 File Offset: 0x000D51D4
		protected override void TrackBarDrawHorizontalTrack(Graphics dc, Rectangle thumb_area, Point channel_startpoint, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawHorizontalTrack(dc, thumb_area, channel_startpoint, clippingArea);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.TrackBar.Track.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.TrackBarDrawHorizontalTrack(dc, thumb_area, channel_startpoint, clippingArea);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(normal);
			visualStyleRenderer.DrawBackground(dc, new Rectangle(channel_startpoint, new Size(thumb_area.Width, visualStyleRenderer.GetPartSize(dc, ThemeSizeType.True).Height)), clippingArea);
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000D7048 File Offset: 0x000D5248
		protected override void TrackBarDrawVerticalTrack(Graphics dc, Rectangle thumb_area, Point channel_startpoint, Rectangle clippingArea)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawVerticalTrack(dc, thumb_area, channel_startpoint, clippingArea);
				return;
			}
			VisualStyleElement normal = VisualStyleElement.TrackBar.TrackVertical.Normal;
			if (!VisualStyleRenderer.IsElementDefined(normal))
			{
				base.TrackBarDrawVerticalTrack(dc, thumb_area, channel_startpoint, clippingArea);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(normal);
			visualStyleRenderer.DrawBackground(dc, new Rectangle(channel_startpoint, new Size(visualStyleRenderer.GetPartSize(dc, ThemeSizeType.True).Width, thumb_area.Height)), clippingArea);
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000D70BC File Offset: 0x000D52BC
		private static bool TrackBarIsDisabled(TrackBar trackBar)
		{
			return !trackBar.Enabled;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000D70C8 File Offset: 0x000D52C8
		private static bool TrackBarIsHot(TrackBar trackBar)
		{
			return trackBar.ThumbEntered;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000D70D0 File Offset: 0x000D52D0
		private static bool TrackBarIsPressed(TrackBar trackBar)
		{
			return trackBar.thumb_pressed;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000D70D8 File Offset: 0x000D52D8
		private static bool TrackBarIsFocused(TrackBar trackBar)
		{
			return trackBar.Focused;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000D70E0 File Offset: 0x000D52E0
		protected override void TrackBarDrawHorizontalThumbBottom(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawHorizontalThumbBottom(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetHorizontalThumbBottomVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawHorizontalThumbBottom(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, thumb_pos, clippingArea);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000D7138 File Offset: 0x000D5338
		private static VisualStyleElement TrackBarGetHorizontalThumbBottomVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbBottom.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbBottom.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbBottom.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbBottom.Focused;
			}
			return VisualStyleElement.TrackBar.ThumbBottom.Normal;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000D7190 File Offset: 0x000D5390
		protected override void TrackBarDrawHorizontalThumbTop(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawHorizontalThumbTop(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetHorizontalThumbTopVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawHorizontalThumbTop(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, thumb_pos, clippingArea);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000D71E8 File Offset: 0x000D53E8
		private static VisualStyleElement TrackBarGetHorizontalThumbTopVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbTop.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbTop.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbTop.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbTop.Focused;
			}
			return VisualStyleElement.TrackBar.ThumbTop.Normal;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000D7240 File Offset: 0x000D5440
		protected override void TrackBarDrawHorizontalThumb(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawHorizontalThumb(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetHorizontalThumbVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawHorizontalThumb(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, thumb_pos, clippingArea);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000D7298 File Offset: 0x000D5498
		private static VisualStyleElement TrackBarGetHorizontalThumbVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.Thumb.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.Thumb.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.Thumb.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.Thumb.Focused;
			}
			return VisualStyleElement.TrackBar.Thumb.Normal;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000D72F0 File Offset: 0x000D54F0
		private static Rectangle TrackBarRotateVerticalThumbSize(Rectangle value)
		{
			int width = value.Width;
			value.Width = value.Height;
			value.Height = width;
			return value;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000D731C File Offset: 0x000D551C
		protected override void TrackBarDrawVerticalThumbRight(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawVerticalThumbRight(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetVerticalThumbRightVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawVerticalThumbRight(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, ThemeVisualStyles.TrackBarRotateVerticalThumbSize(thumb_pos), clippingArea);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000D7378 File Offset: 0x000D5578
		private static VisualStyleElement TrackBarGetVerticalThumbRightVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbRight.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbRight.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbRight.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbRight.Focused;
			}
			return VisualStyleElement.TrackBar.ThumbRight.Normal;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000D73D0 File Offset: 0x000D55D0
		protected override void TrackBarDrawVerticalThumbLeft(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawVerticalThumbLeft(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetVerticalThumbLeftVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawVerticalThumbLeft(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, ThemeVisualStyles.TrackBarRotateVerticalThumbSize(thumb_pos), clippingArea);
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000D742C File Offset: 0x000D562C
		private static VisualStyleElement TrackBarGetVerticalThumbLeftVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbLeft.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbLeft.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbLeft.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbLeft.Focused;
			}
			return VisualStyleElement.TrackBar.ThumbLeft.Normal;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000D7484 File Offset: 0x000D5684
		protected override void TrackBarDrawVerticalThumb(Graphics dc, Rectangle thumb_pos, Brush br_thumb, Rectangle clippingArea, TrackBar trackBar)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TrackBarDrawVerticalThumb(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			VisualStyleElement visualStyleElement = ThemeVisualStyles.TrackBarGetVerticalThumbVisualStyleElement(trackBar);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TrackBarDrawVerticalThumb(dc, thumb_pos, br_thumb, clippingArea, trackBar);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, ThemeVisualStyles.TrackBarRotateVerticalThumbSize(thumb_pos), clippingArea);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000D74E0 File Offset: 0x000D56E0
		private static VisualStyleElement TrackBarGetVerticalThumbVisualStyleElement(TrackBar trackBar)
		{
			if (ThemeVisualStyles.TrackBarIsDisabled(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbVertical.Disabled;
			}
			if (ThemeVisualStyles.TrackBarIsPressed(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbVertical.Pressed;
			}
			if (ThemeVisualStyles.TrackBarIsHot(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbVertical.Hot;
			}
			if (ThemeVisualStyles.TrackBarIsFocused(trackBar))
			{
				return VisualStyleElement.TrackBar.ThumbVertical.Focused;
			}
			return VisualStyleElement.TrackBar.ThumbVertical.Normal;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000D7538 File Offset: 0x000D5738
		protected override ThemeWin32Classic.ITrackBarTickPainter TrackBarGetHorizontalTickPainter(Graphics g)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !VisualStyleRenderer.IsElementDefined(VisualStyleElement.TrackBar.Ticks.Normal))
			{
				return base.TrackBarGetHorizontalTickPainter(g);
			}
			return new ThemeVisualStyles.TrackBarHorizontalTickPainter(g);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000D7564 File Offset: 0x000D5764
		protected override ThemeWin32Classic.ITrackBarTickPainter TrackBarGetVerticalTickPainter(Graphics g)
		{
			if (!ThemeVisualStyles.RenderClientAreas || !VisualStyleRenderer.IsElementDefined(VisualStyleElement.TrackBar.TicksVertical.Normal))
			{
				return base.TrackBarGetVerticalTickPainter(g);
			}
			return new ThemeVisualStyles.TrackBarVerticalTickPainter(g);
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000D7590 File Offset: 0x000D5790
		public override bool TrackBarHasHotThumbStyle
		{
			get
			{
				return ThemeVisualStyles.RenderClientAreas || base.TrackBarHasHotThumbStyle;
			}
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000D75A4 File Offset: 0x000D57A4
		[MonoInternalNote("Use the sizing information provided by the VisualStyles API.")]
		public override void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.TreeViewDrawNodePlusMinus(treeView, node, dc, x, middle);
				return;
			}
			VisualStyleElement visualStyleElement = ((!node.IsExpanded) ? VisualStyleElement.TreeView.Glyph.Closed : VisualStyleElement.TreeView.Glyph.Opened);
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.TreeViewDrawNodePlusMinus(treeView, node, dc, x, middle);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(dc, new Rectangle(x, middle - 4, 9, 9));
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000D7618 File Offset: 0x000D5818
		public override void UpDownBaseDrawButton(Graphics g, Rectangle bounds, bool top, PushButtonState state)
		{
			if (!ThemeVisualStyles.RenderClientAreas)
			{
				base.UpDownBaseDrawButton(g, bounds, top, state);
				return;
			}
			VisualStyleElement visualStyleElement;
			if (top)
			{
				switch (state)
				{
				case PushButtonState.Hot:
					visualStyleElement = VisualStyleElement.Spin.Up.Hot;
					break;
				case PushButtonState.Pressed:
					visualStyleElement = VisualStyleElement.Spin.Up.Pressed;
					break;
				case PushButtonState.Disabled:
					visualStyleElement = VisualStyleElement.Spin.Up.Disabled;
					break;
				default:
					visualStyleElement = VisualStyleElement.Spin.Up.Normal;
					break;
				}
			}
			else
			{
				switch (state)
				{
				case PushButtonState.Hot:
					visualStyleElement = VisualStyleElement.Spin.Down.Hot;
					break;
				case PushButtonState.Pressed:
					visualStyleElement = VisualStyleElement.Spin.Down.Pressed;
					break;
				case PushButtonState.Disabled:
					visualStyleElement = VisualStyleElement.Spin.Down.Disabled;
					break;
				default:
					visualStyleElement = VisualStyleElement.Spin.Down.Normal;
					break;
				}
			}
			if (!VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				base.UpDownBaseDrawButton(g, bounds, top, state);
				return;
			}
			new VisualStyleRenderer(visualStyleElement).DrawBackground(g, bounds);
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000D76FC File Offset: 0x000D58FC
		public override bool UpDownBaseHasHotButtonStyle
		{
			get
			{
				return ThemeVisualStyles.RenderClientAreas || base.UpDownBaseHasHotButtonStyle;
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000D7710 File Offset: 0x000D5910
		private static bool AreEqual(VisualStyleElement value1, VisualStyleElement value2)
		{
			return value1.ClassName == value1.ClassName && value1.Part == value2.Part && value1.State == value2.State;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000D7758 File Offset: 0x000D5958
		private static IDeviceContext GetMeasurementDeviceContext()
		{
			if (ThemeVisualStyles.control == null)
			{
				ThemeVisualStyles.control = new Control();
			}
			return ThemeVisualStyles.control.CreateGraphics();
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000D7778 File Offset: 0x000D5978
		private static void ReleaseMeasurementDeviceContext(IDeviceContext dc)
		{
			dc.Dispose();
		}

		// Token: 0x04001993 RID: 6547
		private const int DateTimePickerDropDownWidthOnWindowsVista = 34;

		// Token: 0x04001994 RID: 6548
		private const int DateTimePickerDropDownHeightOnWindowsVista = 20;

		// Token: 0x04001995 RID: 6549
		private const int WindowsVistaMajorVersion = 6;

		// Token: 0x04001996 RID: 6550
		private const EdgeStyle TrackBarTickEdgeStyle = EdgeStyle.Bump;

		// Token: 0x04001997 RID: 6551
		private const EdgeEffects TrackBarTickEdgeEffects = EdgeEffects.None;

		// Token: 0x04001998 RID: 6552
		private static bool render_client_areas;

		// Token: 0x04001999 RID: 6553
		private static bool render_non_client_areas;

		// Token: 0x0400199A RID: 6554
		private static bool ScrollBarHasHoverArrowButtonStyleVisualStyles = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x0400199B RID: 6555
		private static Control control;

		// Token: 0x0200032B RID: 811
		private class TrackBarHorizontalTickPainter : ThemeWin32Classic.ITrackBarTickPainter
		{
			// Token: 0x06003793 RID: 14227 RVA: 0x000D7780 File Offset: 0x000D5980
			public TrackBarHorizontalTickPainter(Graphics g)
			{
				this.g = g;
				this.renderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Ticks.Normal);
			}

			// Token: 0x06003794 RID: 14228 RVA: 0x000D77A0 File Offset: 0x000D59A0
			public void Paint(float x1, float y1, float x2, float y2)
			{
				this.renderer.DrawEdge(this.g, new Rectangle((int)Math.Round((double)x1), (int)Math.Round((double)y1), 1, (int)Math.Round((double)(y2 - y1)) + 1), Edges.Left, EdgeStyle.Bump, EdgeEffects.None);
			}

			// Token: 0x0400199C RID: 6556
			private readonly Graphics g;

			// Token: 0x0400199D RID: 6557
			private readonly VisualStyleRenderer renderer;
		}

		// Token: 0x0200032C RID: 812
		private class TrackBarVerticalTickPainter : ThemeWin32Classic.ITrackBarTickPainter
		{
			// Token: 0x06003795 RID: 14229 RVA: 0x000D77E8 File Offset: 0x000D59E8
			public TrackBarVerticalTickPainter(Graphics g)
			{
				this.g = g;
				this.renderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal);
			}

			// Token: 0x06003796 RID: 14230 RVA: 0x000D7808 File Offset: 0x000D5A08
			public void Paint(float x1, float y1, float x2, float y2)
			{
				this.renderer.DrawEdge(this.g, new Rectangle((int)Math.Round((double)x1), (int)Math.Round((double)y1), (int)Math.Round((double)(x2 - x1)) + 1, 1), Edges.Top, EdgeStyle.Bump, EdgeEffects.None);
			}

			// Token: 0x0400199E RID: 6558
			private readonly Graphics g;

			// Token: 0x0400199F RID: 6559
			private readonly VisualStyleRenderer renderer;
		}
	}
}
