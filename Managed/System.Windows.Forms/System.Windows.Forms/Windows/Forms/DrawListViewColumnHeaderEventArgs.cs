using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000157 RID: 343
	public class DrawListViewColumnHeaderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> class. </summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="columnIndex">The index of the header's column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used for the header text.</param>
		// Token: 0x0600174E RID: 5966 RVA: 0x00056000 File Offset: 0x00054200
		public DrawListViewColumnHeaderEventArgs(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
		{
			this.backColor = backColor;
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.font = font;
			this.foreColor = foreColor;
			this.graphics = graphics;
			this.header = header;
			this.state = state;
		}

		/// <summary>Gets the background color of the header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the background color of the header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00056050 File Offset: 0x00054250
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
		}

		/// <summary>Gets the size and location of the column header to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the column header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00056058 File Offset: 0x00054258
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</summary>
		/// <returns>The index of the column header within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00056060 File Offset: 0x00054260
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets a value indicating whether the column header should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>true if the header should be drawn by the operating system; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00056068 File Offset: 0x00054268
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00056070 File Offset: 0x00054270
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		/// <summary>Gets the font used to draw the column header text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> representing the font of the header text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0005607C File Offset: 0x0005427C
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets the foreground color of the header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00056084 File Offset: 0x00054284
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0005608C File Offset: 0x0005428C
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00056094 File Offset: 0x00054294
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		/// <summary>Gets the current state of the column header.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0005609C File Offset: 0x0005429C
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Draws the background of the column header.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001759 RID: 5977 RVA: 0x000560A4 File Offset: 0x000542A4
		public void DrawBackground()
		{
			ThemeEngine.Current.CPDrawButton(this.graphics, this.bounds, ButtonState.Normal);
		}

		/// <summary>Draws the column header text using the default formatting.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600175A RID: 5978 RVA: 0x000560C0 File Offset: 0x000542C0
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
		}

		/// <summary>Draws the column header text, formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
		// Token: 0x0600175B RID: 5979 RVA: 0x000560D0 File Offset: 0x000542D0
		public void DrawText(TextFormatFlags flags)
		{
			Rectangle rectangle;
			rectangle..ctor(this.bounds.X + 8, this.bounds.Y, this.bounds.Width - 13, this.bounds.Height);
			TextRenderer.DrawText(this.graphics, this.header.Text, this.font, rectangle, this.foreColor, flags);
		}

		// Token: 0x04000CDB RID: 3291
		private Color backColor;

		// Token: 0x04000CDC RID: 3292
		private Rectangle bounds;

		// Token: 0x04000CDD RID: 3293
		private int columnIndex;

		// Token: 0x04000CDE RID: 3294
		private bool drawDefault;

		// Token: 0x04000CDF RID: 3295
		private Font font;

		// Token: 0x04000CE0 RID: 3296
		private Color foreColor;

		// Token: 0x04000CE1 RID: 3297
		private Graphics graphics;

		// Token: 0x04000CE2 RID: 3298
		private ColumnHeader header;

		// Token: 0x04000CE3 RID: 3299
		private ListViewItemStates state;
	}
}
