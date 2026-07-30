using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a tab control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002FF RID: 767
	public sealed class TabRenderer
	{
		// Token: 0x0600335C RID: 13148 RVA: 0x000C26B8 File Offset: 0x000C08B8
		private TabRenderer()
		{
		}

		/// <summary>Draws a tab in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600335D RID: 13149 RVA: 0x000C26C0 File Offset: 0x000C08C0
		public static void DrawTabItem(Graphics g, Rectangle bounds, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, false, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600335E RID: 13150 RVA: 0x000C26E4 File Offset: 0x000C08E4
		public static void DrawTabItem(Graphics g, Rectangle bounds, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, and with the specified text.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600335F RID: 13151 RVA: 0x000C2708 File Offset: 0x000C0908
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, false, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003360 RID: 13152 RVA: 0x000C2728 File Offset: 0x000C0928
		public static void DrawTabItem(Graphics g, Rectangle bounds, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, string.Empty, null, TextFormatFlags.HorizontalCenter, image, imageRectangle, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003361 RID: 13153 RVA: 0x000C274C File Offset: 0x000C094C
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003362 RID: 13154 RVA: 0x000C2770 File Offset: 0x000C0970
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, flags, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003363 RID: 13155 RVA: 0x000C2794 File Offset: 0x000C0994
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			TabRenderer.DrawTabItem(g, bounds, tabItemText, font, TextFormatFlags.HorizontalCenter, image, imageRectangle, focused, state);
		}

		/// <summary>Draws a tab in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab.</param>
		/// <param name="tabItemText">The <see cref="T:System.String" /> to draw in the tab.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="tabItemText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw in the tab.</param>
		/// <param name="imageRectangle">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TabItemState" /> values that specifies the visual state of the tab.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06003364 RID: 13156 RVA: 0x000C27B4 File Offset: 0x000C09B4
		public static void DrawTabItem(Graphics g, Rectangle bounds, string tabItemText, Font font, TextFormatFlags flags, Image image, Rectangle imageRectangle, bool focused, TabItemState state)
		{
			if (!TabRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TabItemState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Hot);
				goto IL_0070;
			case TabItemState.Selected:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Pressed);
				goto IL_0070;
			case TabItemState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Disabled);
				goto IL_0070;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Normal);
			IL_0070:
			visualStyleRenderer.DrawBackground(g, bounds);
			if (image != null)
			{
				visualStyleRenderer.DrawImage(g, imageRectangle, image);
			}
			bounds.Offset(3, 3);
			bounds.Height -= 6;
			bounds.Width -= 6;
			if (tabItemText != string.Empty)
			{
				TextRenderer.DrawText(g, tabItemText, font, bounds, SystemColors.ControlText, flags);
			}
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, bounds);
			}
		}

		/// <summary>Draws a tab page in the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the tab page.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the tab page.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003365 RID: 13157 RVA: 0x000C28A0 File Offset: 0x000C0AA0
		public static void DrawTabPage(Graphics g, Rectangle bounds)
		{
			if (!TabRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Tab.Pane.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TabRenderer" /> class can be used to draw a tab control with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000C28D0 File Offset: 0x000C0AD0
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}
	}
}
