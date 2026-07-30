using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000159 RID: 345
	public class DrawListViewSubItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
		/// <param name="subItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
		/// <param name="itemIndex">The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
		/// <param name="columnIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection. </param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed. </param>
		/// <param name="itemState">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
		// Token: 0x06001768 RID: 5992 RVA: 0x00056280 File Offset: 0x00054480
		public DrawListViewSubItemEventArgs(Graphics graphics, Rectangle bounds, ListViewItem item, ListViewItem.ListViewSubItem subItem, int itemIndex, int columnIndex, ColumnHeader header, ListViewItemStates itemState)
		{
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.graphics = graphics;
			this.header = header;
			this.item = item;
			this.itemIndex = itemIndex;
			this.itemState = itemState;
			this.subItem = subItem;
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000562D0 File Offset: 0x000544D0
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000562D8 File Offset: 0x000544D8
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>true if the subitem should be drawn by the operating system; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000562E0 File Offset: 0x000544E0
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x000562E8 File Offset: 0x000544E8
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

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x000562F4 File Offset: 0x000544F4
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the header of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x000562FC File Offset: 0x000544FC
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00056304 File Offset: 0x00054504
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0005630C File Offset: 0x0005450C
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00056314 File Offset: 0x00054514
		public ListViewItemStates ItemState
		{
			get
			{
				return this.itemState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0005631C File Offset: 0x0005451C
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current background color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001773 RID: 6003 RVA: 0x00056324 File Offset: 0x00054524
		public void DrawBackground()
		{
			this.graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.subItem.BackColor), this.bounds);
		}

		/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> if the parent <see cref="T:System.Windows.Forms.ListViewItem" /> has focus.</summary>
		/// <param name="bounds">The area within which to draw the focus rectangle.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001774 RID: 6004 RVA: 0x0005635C File Offset: 0x0005455C
		public void DrawFocusRectangle(Rectangle bounds)
		{
			if ((this.itemState & ListViewItemStates.Focused) != (ListViewItemStates)0)
			{
				Rectangle rectangle;
				rectangle..ctor(bounds.X + 1, bounds.Y + 1, bounds.Width - 1, bounds.Height - 1);
				ThemeEngine.Current.CPDrawFocusRectangle(this.graphics, rectangle, this.subItem.ForeColor, this.subItem.BackColor);
			}
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001775 RID: 6005 RVA: 0x000563CC File Offset: 0x000545CC
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
		// Token: 0x06001776 RID: 6006 RVA: 0x000563DC File Offset: 0x000545DC
		public void DrawText(TextFormatFlags flags)
		{
			Rectangle rectangle;
			rectangle..ctor(this.bounds.X + 8, this.bounds.Y, this.bounds.Width - 13, this.bounds.Height);
			TextRenderer.DrawText(this.graphics, this.subItem.Text, this.subItem.Font, rectangle, this.subItem.ForeColor, flags);
		}

		// Token: 0x04000CEA RID: 3306
		private Rectangle bounds;

		// Token: 0x04000CEB RID: 3307
		private int columnIndex;

		// Token: 0x04000CEC RID: 3308
		private bool drawDefault;

		// Token: 0x04000CED RID: 3309
		private Graphics graphics;

		// Token: 0x04000CEE RID: 3310
		private ColumnHeader header;

		// Token: 0x04000CEF RID: 3311
		private ListViewItem item;

		// Token: 0x04000CF0 RID: 3312
		private int itemIndex;

		// Token: 0x04000CF1 RID: 3313
		private ListViewItemStates itemState;

		// Token: 0x04000CF2 RID: 3314
		private ListViewItem.ListViewSubItem subItem;
	}
}
