using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015B RID: 347
	public class DrawToolTipEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawToolTipEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> context used to draw the ToolTip. </param>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that outlines the area where the ToolTip is to be displayed.</param>
		/// <param name="toolTipText">A <see cref="T:System.String" /> containing the text for the ToolTip.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip background.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> of the ToolTip text. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to draw the ToolTip text.</param>
		// Token: 0x06001777 RID: 6007 RVA: 0x00056450 File Offset: 0x00054650
		public DrawToolTipEventArgs(Graphics graphics, IWin32Window associatedWindow, Control associatedControl, Rectangle bounds, string toolTipText, Color backColor, Color foreColor, Font font)
		{
			this.graphics = graphics;
			this.associated_window = associatedWindow;
			this.associated_control = associatedControl;
			this.bounds = bounds;
			this.tooltip_text = toolTipText;
			this.back_color = backColor;
			this.fore_color = foreColor;
			this.font = font;
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system background color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001778 RID: 6008 RVA: 0x000564A0 File Offset: 0x000546A0
		public void DrawBackground()
		{
			this.graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.back_color), this.bounds);
		}

		/// <summary>Draws the border of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system border color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001779 RID: 6009 RVA: 0x000564D4 File Offset: 0x000546D4
		public void DrawBorder()
		{
			ControlPaint.DrawBorder(this.graphics, this.bounds, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600177A RID: 6010 RVA: 0x000564F0 File Offset: 0x000546F0
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.HidePrefix);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ToolTip" /> using the system text color and font, and the specified text layout.</summary>
		/// <param name="flags">A <see cref="T:System.Windows.Forms.TextFormatFlags" /> containing a bitwise combination of values that specifies the display and layout for the <see cref="P:System.Windows.Forms.DrawToolTipEventArgs.ToolTipText" />.</param>
		// Token: 0x0600177B RID: 6011 RVA: 0x00056500 File Offset: 0x00054700
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(this.graphics, this.tooltip_text, this.font, this.bounds, this.fore_color, flags, false);
		}

		/// <summary>Gets the control for which the <see cref="T:System.Windows.Forms.ToolTip" /> is being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs. The return value will be null if the ToolTip is not associated with a control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00056528 File Offset: 0x00054728
		public Control AssociatedControl
		{
			get
			{
				return this.associated_control;
			}
		}

		/// <summary>Gets the window to which this <see cref="T:System.Windows.Forms.ToolTip" /> is bound.</summary>
		/// <returns>The window which owns the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00056530 File Offset: 0x00054730
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associated_window;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ToolTip" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00056538 File Offset: 0x00054738
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the font used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x00056540 File Offset: 0x00054740
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets the graphics surface used to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> on which to draw the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00056548 File Offset: 0x00054748
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the text for the <see cref="T:System.Windows.Forms.ToolTip" /> that is being drawn.</summary>
		/// <returns>The text that is associated with the <see cref="T:System.Windows.Forms.ToolTip" /> when the <see cref="E:System.Windows.Forms.ToolTip.Draw" /> event occurs.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00056550 File Offset: 0x00054750
		public string ToolTipText
		{
			get
			{
				return this.tooltip_text;
			}
		}

		// Token: 0x04000CF7 RID: 3319
		private Control associated_control;

		// Token: 0x04000CF8 RID: 3320
		private IWin32Window associated_window;

		// Token: 0x04000CF9 RID: 3321
		private Color back_color;

		// Token: 0x04000CFA RID: 3322
		private Font font;

		// Token: 0x04000CFB RID: 3323
		private Rectangle bounds;

		// Token: 0x04000CFC RID: 3324
		private Color fore_color;

		// Token: 0x04000CFD RID: 3325
		private Graphics graphics;

		// Token: 0x04000CFE RID: 3326
		private string tooltip_text;
	}
}
