using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a button control with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006D RID: 109
	public sealed class ButtonRenderer
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0001676C File Offset: 0x0001496C
		private ButtonRenderer()
		{
		}

		/// <summary>Draws a button control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004EC RID: 1260 RVA: 0x00016778 File Offset: 0x00014978
		public static void DrawButton(Graphics g, Rectangle bounds, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, false, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004ED RID: 1261 RVA: 0x0001679C File Offset: 0x0001499C
		public static void DrawButton(Graphics g, Rectangle bounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004EE RID: 1262 RVA: 0x000167C0 File Offset: 0x000149C0
		public static void DrawButton(Graphics g, Rectangle bounds, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, string.Empty, null, TextFormatFlags.Left, image, imageBounds, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004EF RID: 1263 RVA: 0x000167E4 File Offset: 0x000149E4
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text and text formatting, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x060004F0 RID: 1264 RVA: 0x00016808 File Offset: 0x00014A08
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, flags, null, Rectangle.Empty, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds, with the specified text and image, and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001682C File Offset: 0x00014A2C
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			ButtonRenderer.DrawButton(g, bounds, buttonText, font, TextFormatFlags.HorizontalCenter, image, imageBounds, focused, state);
		}

		/// <summary>Draws a button control in the specified state and bounds; with the specified text, text formatting, and image; and with an optional focus rectangle.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the button.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the button.</param>
		/// <param name="buttonText">The <see cref="T:System.String" /> to draw on the button.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="buttonText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to <paramref name="buttonText" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw on the button.</param>
		/// <param name="imageBounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of <paramref name="image" />.</param>
		/// <param name="focused">true to draw a focus rectangle on the button; otherwise, false.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001684C File Offset: 0x00014A4C
		public static void DrawButton(Graphics g, Rectangle bounds, string buttonText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, PushButtonState state)
		{
			if (Application.RenderWithVisualStyles || ButtonRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer pushButtonRenderer = ButtonRenderer.GetPushButtonRenderer(state);
				pushButtonRenderer.DrawBackground(g, bounds);
				if (image != null)
				{
					pushButtonRenderer.DrawImage(g, imageBounds, image);
				}
			}
			else
			{
				if (state == PushButtonState.Pressed)
				{
					ControlPaint.DrawButton(g, bounds, ButtonState.Pushed);
				}
				else
				{
					ControlPaint.DrawButton(g, bounds, ButtonState.Normal);
				}
				if (image != null)
				{
					g.DrawImage(image, imageBounds);
				}
			}
			Rectangle rectangle = bounds;
			rectangle.Inflate(-3, -3);
			if (focused)
			{
				ControlPaint.DrawFocusRectangle(g, rectangle);
			}
			if (buttonText != string.Empty)
			{
				if (state == PushButtonState.Disabled)
				{
					TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.GrayText, flags);
				}
				else
				{
					TextRenderer.DrawText(g, buttonText, font, rectangle, SystemColors.ControlText, flags);
				}
			}
		}

		/// <summary>Indicates whether the background of the button has semitransparent or alpha-blended pieces.</summary>
		/// <returns>true if the background of the button has semitransparent or alpha-blended pieces; otherwise, false.</returns>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.PushButtonState" /> values that specifies the visual state of the button.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00016920 File Offset: 0x00014B20
		public static bool IsBackgroundPartiallyTransparent(PushButtonState state)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return false;
			}
			VisualStyleRenderer pushButtonRenderer = ButtonRenderer.GetPushButtonRenderer(state);
			return pushButtonRenderer.IsBackgroundPartiallyTransparent();
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the background of the parent of <paramref name="childControl" />.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control’s bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F4 RID: 1268 RVA: 0x00016948 File Offset: 0x00014B48
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016974 File Offset: 0x00014B74
		internal static VisualStyleRenderer GetPushButtonRenderer(PushButtonState state)
		{
			switch (state)
			{
			case PushButtonState.Normal:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
			case PushButtonState.Hot:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot);
			case PushButtonState.Pressed:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed);
			case PushButtonState.Disabled:
				return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Disabled);
			}
			return new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>true if the application state is used to determine rendering style; otherwise, false. The default is true.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000169DC File Offset: 0x00014BDC
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x000169E8 File Offset: 0x00014BE8
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return !ButtonRenderer.always_use_visual_styles;
			}
			set
			{
				ButtonRenderer.always_use_visual_styles = !value;
			}
		}

		// Token: 0x040006A3 RID: 1699
		private static bool always_use_visual_styles;
	}
}
