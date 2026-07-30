using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render an option button control (also known as a radio button) with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B2 RID: 690
	public sealed class RadioButtonRenderer
	{
		// Token: 0x06002E07 RID: 11783 RVA: 0x000B1804 File Offset: 0x000AFA04
		private RadioButtonRenderer()
		{
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E09 RID: 11785 RVA: 0x000B1810 File Offset: 0x000AFA10
		public static void DrawRadioButton(Graphics g, Point glyphLocation, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, Rectangle.Empty, string.Empty, null, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, false, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E0A RID: 11786 RVA: 0x000B1838 File Offset: 0x000AFA38
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x06002E0B RID: 11787 RVA: 0x000B185C File Offset: 0x000AFA5C
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, flags, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E0C RID: 11788 RVA: 0x000B1880 File Offset: 0x000AFA80
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			RadioButtonRenderer.DrawRadioButton(g, glyphLocation, textBounds, radioButtonText, font, TextFormatFlags.HorizontalCenter, image, imageBounds, focused, state);
		}

		/// <summary>Draws an option button control (also known as a radio button) in the specified state and location; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="glyphLocation">The <see cref="T:System.Drawing.Point" /> to draw the option button glyph at.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="radioButtonText" /> in.</param>
		/// <param name="radioButtonText">The <see cref="T:System.String" /> to draw with the option button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="radioButtonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw with the option button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> to draw <paramref name="image" /> in.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		// Token: 0x06002E0D RID: 11789 RVA: 0x000B18A4 File Offset: 0x000AFAA4
		public static void DrawRadioButton(Graphics g, Point glyphLocation, Rectangle textBounds, string radioButtonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, RadioButtonState state)
		{
			Rectangle rectangle;
			rectangle..ctor(glyphLocation, RadioButtonRenderer.GetGlyphSize(g, state));
			if (Application.RenderWithVisualStyles || RadioButtonRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer radioButtonRenderer = RadioButtonRenderer.GetRadioButtonRenderer(state);
				radioButtonRenderer.DrawBackground(g, rectangle);
				if (image != null)
				{
					radioButtonRenderer.DrawImage(g, imageBounds, image);
				}
				if (focused)
				{
					ControlPaint.DrawFocusRectangle(g, textBounds);
				}
				if (radioButtonText != string.Empty)
				{
					if (state == RadioButtonState.CheckedDisabled || state == RadioButtonState.UncheckedDisabled)
					{
						TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.GrayText, flags);
					}
					else
					{
						TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.ControlText, flags);
					}
				}
			}
			else
			{
				switch (state)
				{
				case RadioButtonState.UncheckedNormal:
				case RadioButtonState.UncheckedHot:
					ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Normal);
					break;
				case RadioButtonState.UncheckedPressed:
				case RadioButtonState.UncheckedDisabled:
					ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Inactive);
					break;
				case RadioButtonState.CheckedNormal:
				case RadioButtonState.CheckedHot:
					ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Checked);
					break;
				case RadioButtonState.CheckedPressed:
					ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Pushed | ButtonState.Checked);
					break;
				case RadioButtonState.CheckedDisabled:
					ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Inactive | ButtonState.Checked);
					break;
				}
				if (image != null)
				{
					g.DrawImage(image, imageBounds);
				}
				if (focused)
				{
					ControlPaint.DrawFocusRectangle(g, textBounds);
				}
				if (radioButtonText != string.Empty)
				{
					TextRenderer.DrawText(g, radioButtonText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
		}

		/// <summary>Indicates whether the background of the option button (also known as a radio button) has semitransparent or alpha-blended pieces.</summary>
		/// <returns>true if the background of the option button has semitransparent or alpha-blended pieces; otherwise, false.</returns>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E0E RID: 11790 RVA: 0x000B1A14 File Offset: 0x000AFC14
		public static bool IsBackgroundPartiallyTransparent(RadioButtonState state)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return false;
			}
			VisualStyleRenderer radioButtonRenderer = RadioButtonRenderer.GetRadioButtonRenderer(state);
			return radioButtonRenderer.IsBackgroundPartiallyTransparent();
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control’s bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E0F RID: 11791 RVA: 0x000B1A3C File Offset: 0x000AFC3C
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedNormal);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}

		/// <summary>Returns the size, in pixels, of the option button (also known as a radio button) glyph.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size, in pixels, of the option button glyph.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the option button.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.RadioButtonState" /> values that specifies the visual state of the option button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E10 RID: 11792 RVA: 0x000B1A68 File Offset: 0x000AFC68
		public static Size GetGlyphSize(Graphics g, RadioButtonState state)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return new Size(13, 13);
			}
			VisualStyleRenderer radioButtonRenderer = RadioButtonRenderer.GetRadioButtonRenderer(state);
			return radioButtonRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000B1A98 File Offset: 0x000AFC98
		private static VisualStyleRenderer GetRadioButtonRenderer(RadioButtonState state)
		{
			switch (state)
			{
			case RadioButtonState.UncheckedHot:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedHot);
			case RadioButtonState.UncheckedPressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedPressed);
			case RadioButtonState.UncheckedDisabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedDisabled);
			case RadioButtonState.CheckedNormal:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedNormal);
			case RadioButtonState.CheckedHot:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedHot);
			case RadioButtonState.CheckedPressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedPressed);
			case RadioButtonState.CheckedDisabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedDisabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedNormal);
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>true if the application state is used to determine rendering style; otherwise, false. The default is true.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000B1B2C File Offset: 0x000AFD2C
		// (set) Token: 0x06002E13 RID: 11795 RVA: 0x000B1B38 File Offset: 0x000AFD38
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return !RadioButtonRenderer.always_use_visual_styles;
			}
			set
			{
				RadioButtonRenderer.always_use_visual_styles = !value;
			}
		}

		// Token: 0x0400161C RID: 5660
		private static bool always_use_visual_styles;
	}
}
