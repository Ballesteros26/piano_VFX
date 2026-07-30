using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a group box control with or without visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AC RID: 428
	public sealed class GroupBoxRenderer
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x0006BDF8 File Offset: 0x00069FF8
		private GroupBoxRenderer()
		{
		}

		/// <summary>Draws a group box control in the specified state and bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001BF7 RID: 7159 RVA: 0x0006BE04 File Offset: 0x0006A004
		public static void DrawGroupBox(Graphics g, Rectangle bounds, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, string.Empty, null, Color.Empty, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text and font.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001BF8 RID: 7160 RVA: 0x0006BE1C File Offset: 0x0006A01C
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, Color.Empty, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, and color.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x0006BE30 File Offset: 0x0006A030
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, textColor, TextFormatFlags.Left, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x06001BFA RID: 7162 RVA: 0x0006BE40 File Offset: 0x0006A040
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, TextFormatFlags flags, GroupBoxState state)
		{
			GroupBoxRenderer.DrawGroupBox(g, bounds, groupBoxText, font, Color.Empty, flags, state);
		}

		/// <summary>Draws a group box control in the specified state and bounds, with the specified text, font, color, and text formatting.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the group box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the group box.</param>
		/// <param name="groupBoxText">The <see cref="T:System.String" /> to draw with the group box.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> to apply to <paramref name="groupBoxText" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		// Token: 0x06001BFB RID: 7163 RVA: 0x0006BE54 File Offset: 0x0006A054
		public static void DrawGroupBox(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color textColor, TextFormatFlags flags, GroupBoxState state)
		{
			Size size = TextRenderer.MeasureText(groupBoxText, font);
			if (Application.RenderWithVisualStyles || GroupBoxRenderer.always_use_visual_styles)
			{
				VisualStyleRenderer visualStyleRenderer;
				Rectangle rectangle;
				if (state == GroupBoxState.Normal || state != GroupBoxState.Disabled)
				{
					visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
					rectangle..ctor(bounds.Left, bounds.Top + size.Height / 2 - 1, bounds.Width, bounds.Height - size.Height / 2 + 1);
				}
				else
				{
					visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Disabled);
					rectangle..ctor(bounds.Left, bounds.Top + size.Height / 2 - 2, bounds.Width, bounds.Height - size.Height / 2 + 2);
				}
				if (groupBoxText == string.Empty)
				{
					visualStyleRenderer.DrawBackground(g, bounds);
				}
				else
				{
					visualStyleRenderer.DrawBackgroundExcludingArea(g, rectangle, new Rectangle(bounds.Left + 9, bounds.Top, size.Width - 3, size.Height));
				}
				if (textColor == Color.Empty)
				{
					textColor = visualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				if (groupBoxText != string.Empty)
				{
					TextRenderer.DrawText(g, groupBoxText, font, new Point(bounds.Left + 8, bounds.Top), textColor, flags);
				}
			}
			else
			{
				Rectangle rectangle2;
				rectangle2..ctor(bounds.Left, bounds.Top + size.Height / 2, bounds.Width, bounds.Height - size.Height / 2);
				Region clip = g.Clip;
				g.SetClip(new Rectangle(bounds.Left + 9, bounds.Top, size.Width - 3, size.Height), 4);
				ControlPaint.DrawBorder3D(g, rectangle2, Border3DStyle.Etched);
				g.Clip = clip;
				if (groupBoxText != string.Empty)
				{
					if (textColor == Color.Empty)
					{
						textColor = ((state != GroupBoxState.Normal) ? SystemColors.GrayText : SystemColors.ControlText);
					}
					TextRenderer.DrawText(g, groupBoxText, font, new Point(bounds.Left + 8, bounds.Top), textColor, flags);
				}
			}
		}

		/// <summary>Indicates whether the background of the group box has any semitransparent or alpha-blended pieces.</summary>
		/// <returns>true if the background of the group box has semitransparent or alpha-blended pieces; otherwise, false.</returns>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.GroupBoxState" /> values that specifies the visual state of the group box.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001BFC RID: 7164 RVA: 0x0006C0A4 File Offset: 0x0006A2A4
		public static bool IsBackgroundPartiallyTransparent(GroupBoxState state)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return false;
			}
			VisualStyleRenderer visualStyleRenderer;
			if (state == GroupBoxState.Normal || state != GroupBoxState.Disabled)
			{
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
			}
			else
			{
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Disabled);
			}
			return visualStyleRenderer.IsBackgroundPartiallyTransparent();
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
		// Token: 0x06001BFD RID: 7165 RVA: 0x0006C0F8 File Offset: 0x0006A2F8
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal);
			visualStyleRenderer.DrawParentBackground(g, bounds, childControl);
		}

		/// <summary>Gets or sets a value indicating whether the renderer uses the application state to determine rendering style.</summary>
		/// <returns>true if the application state is used to determine rendering style; otherwise, false. The default is true.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0006C124 File Offset: 0x0006A324
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0006C130 File Offset: 0x0006A330
		public static bool RenderMatchingApplicationState
		{
			get
			{
				return !GroupBoxRenderer.always_use_visual_styles;
			}
			set
			{
				GroupBoxRenderer.always_use_visual_styles = !value;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private static bool always_use_visual_styles;
	}
}
