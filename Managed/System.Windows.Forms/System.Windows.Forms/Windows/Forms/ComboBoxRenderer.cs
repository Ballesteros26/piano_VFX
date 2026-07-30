using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a combo box control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000099 RID: 153
	public sealed class ComboBoxRenderer
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x000218E0 File Offset: 0x0001FAE0
		private ComboBoxRenderer()
		{
		}

		/// <summary>Draws a drop-down arrow with the current visual style of the operating system.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the drop-down arrow.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the drop-down arrow.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the drop-down arrow.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000774 RID: 1908 RVA: 0x000218E8 File Offset: 0x0001FAE8
		public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			if (!ComboBoxRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			ComboBoxRenderer.GetComboRenderer(state).DrawBackground(g, bounds);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text, text formatting, and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000775 RID: 1909 RVA: 0x00021908 File Offset: 0x0001FB08
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, ComboBoxState state)
		{
			if (!ComboBoxRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			ComboBoxRenderer.GetTextBoxRenderer(state).DrawBackground(g, bounds);
			if (textBounds == Rectangle.Empty)
			{
				textBounds..ctor(bounds.Left + 3, bounds.Top, bounds.Width - 4, bounds.Height);
			}
			if (comboBoxText != string.Empty)
			{
				if (state == ComboBoxState.Disabled)
				{
					TextRenderer.DrawText(g, comboBoxText, font, textBounds, SystemColors.GrayText, flags);
				}
				else
				{
					TextRenderer.DrawText(g, comboBoxText, font, textBounds, SystemColors.ControlText, flags);
				}
			}
		}

		/// <summary>Draws a text box in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000776 RID: 1910 RVA: 0x000219AC File Offset: 0x0001FBAC
		public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, string.Empty, null, Rectangle.Empty, TextFormatFlags.VerticalCenter, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000777 RID: 1911 RVA: 0x000219C4 File Offset: 0x0001FBC4
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, Rectangle.Empty, TextFormatFlags.VerticalCenter, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text and text bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="textBounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds in which to draw <paramref name="comboBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000778 RID: 1912 RVA: 0x000219D8 File Offset: 0x0001FBD8
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, textBounds, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a text box in the specified state and bounds, with the specified text and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the text box.</param>
		/// <param name="comboBoxText">The <see cref="T:System.String" /> to draw in the text box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="comboBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ComboBoxState" /> values that specifies the visual state of the text box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06000779 RID: 1913 RVA: 0x000219E8 File Offset: 0x0001FBE8
		public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, TextFormatFlags flags, ComboBoxState state)
		{
			ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, Rectangle.Empty, flags |= TextFormatFlags.VerticalCenter, state);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00021A04 File Offset: 0x0001FC04
		private static VisualStyleRenderer GetComboRenderer(ComboBoxState state)
		{
			switch (state)
			{
			case ComboBoxState.Hot:
				return new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Hot);
			case ComboBoxState.Pressed:
				return new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Pressed);
			case ComboBoxState.Disabled:
				return new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Disabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Normal);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00021A5C File Offset: 0x0001FC5C
		private static VisualStyleRenderer GetTextBoxRenderer(ComboBoxState state)
		{
			switch (state)
			{
			case ComboBoxState.Hot:
				return new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot);
			case ComboBoxState.Disabled:
				return new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Disabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBoxRenderer" /> class can be used to draw a combo box with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}
	}
}
