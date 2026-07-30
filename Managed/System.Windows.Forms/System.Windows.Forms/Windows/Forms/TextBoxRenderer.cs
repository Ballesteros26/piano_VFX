using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a text box control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000314 RID: 788
	public sealed class TextBoxRenderer
	{
		// Token: 0x060034F0 RID: 13552 RVA: 0x000CA1B8 File Offset: 0x000C83B8
		private TextBoxRenderer()
		{
		}

		/// <summary>Draws a text box control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034F1 RID: 13553 RVA: 0x000CA1C0 File Offset: 0x000C83C0
		public static void DrawTextBox(Graphics g, Rectangle bounds, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, string.Empty, null, Rectangle.Empty, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034F2 RID: 13554 RVA: 0x000CA1D8 File Offset: 0x000C83D8
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, Rectangle.Empty, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034F3 RID: 13555 RVA: 0x000CA1EC File Offset: 0x000C83EC
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, textBounds, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060034F4 RID: 13556 RVA: 0x000CA1FC File Offset: 0x000C83FC
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, TextFormatFlags flags, TextBoxState state)
		{
			TextBoxRenderer.DrawTextBox(g, bounds, textBoxText, font, Rectangle.Empty, flags, state);
		}

		/// <summary>Draws a text box control in the specified state and bounds, and with the specified text, text bounds, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="textBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="textBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="textBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TextBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060034F5 RID: 13557 RVA: 0x000CA210 File Offset: 0x000C8410
		public static void DrawTextBox(Graphics g, Rectangle bounds, string textBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, TextBoxState state)
		{
			if (!TextBoxRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TextBoxState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot);
				goto IL_008C;
			case TextBoxState.Selected:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Selected);
				goto IL_008C;
			case TextBoxState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Disabled);
				goto IL_008C;
			case TextBoxState.Assist:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Assist);
				goto IL_008C;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
			IL_008C:
			visualStyleRenderer.DrawBackground(g, bounds);
			if (textBounds == Rectangle.Empty)
			{
				textBounds..ctor(bounds.Left + 3, bounds.Top + 3, bounds.Width - 6, bounds.Height - 6);
			}
			if (textBoxText != string.Empty)
			{
				if (state == TextBoxState.Disabled)
				{
					TextRenderer.DrawText(g, textBoxText, font, textBounds, SystemColors.GrayText, flags);
				}
				else
				{
					TextRenderer.DrawText(g, textBoxText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TextBoxRenderer" /> class can be used to draw a text box with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000CA32C File Offset: 0x000C852C
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}
	}
}
