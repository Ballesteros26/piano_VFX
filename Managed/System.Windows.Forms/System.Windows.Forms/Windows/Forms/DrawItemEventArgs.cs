using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the DrawItem event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	public class DrawItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
		// Token: 0x06001743 RID: 5955 RVA: 0x00055F44 File Offset: 0x00054144
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
			: this(graphics, font, rect, index, state, Control.DefaultForeColor, Control.DefaultBackColor)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, foreground color, background color, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
		// Token: 0x06001744 RID: 5956 RVA: 0x00055F68 File Offset: 0x00054168
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.fore_color = foreColor;
			this.back_color = backColor;
		}

		/// <summary>Gets the graphics surface to draw the item on.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> surface to draw the item on.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00055FA8 File Offset: 0x000541A8
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the font assigned to the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> assigned to the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00055FB0 File Offset: 0x000541B0
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets the rectangle that represents the bounds of the item that is being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the item that is being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00055FB8 File Offset: 0x000541B8
		public Rectangle Bounds
		{
			get
			{
				return this.rect;
			}
		}

		/// <summary>Gets the index value of the item that is being drawn.</summary>
		/// <returns>The numeric value that represents the <see cref="P:System.Windows.Forms.Control.ControlCollection.Item(System.Int32)" /> value of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00055FC0 File Offset: 0x000541C0
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the state of the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DrawItemState" /> that represents the state of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00055FC8 File Offset: 0x000541C8
		public DrawItemState State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets the background color of the item that is being drawn.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the item that is being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00055FD0 File Offset: 0x000541D0
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets the foreground color of the of the item being drawn.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00055FD8 File Offset: 0x000541D8
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
		}

		/// <summary>Draws the background within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor and with the appropriate color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600174C RID: 5964 RVA: 0x00055FE0 File Offset: 0x000541E0
		public virtual void DrawBackground()
		{
			ThemeEngine.Current.DrawOwnerDrawBackground(this);
		}

		/// <summary>Draws a focus rectangle within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600174D RID: 5965 RVA: 0x00055FF0 File Offset: 0x000541F0
		public virtual void DrawFocusRectangle()
		{
			ThemeEngine.Current.DrawOwnerDrawFocusRectangle(this);
		}

		// Token: 0x04000CC7 RID: 3271
		private Graphics graphics;

		// Token: 0x04000CC8 RID: 3272
		private Font font;

		// Token: 0x04000CC9 RID: 3273
		private Rectangle rect;

		// Token: 0x04000CCA RID: 3274
		private int index;

		// Token: 0x04000CCB RID: 3275
		private DrawItemState state;

		// Token: 0x04000CCC RID: 3276
		private Color fore_color;

		// Token: 0x04000CCD RID: 3277
		private Color back_color;
	}
}
