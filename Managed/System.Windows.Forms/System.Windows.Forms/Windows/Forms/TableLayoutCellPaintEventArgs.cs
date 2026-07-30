using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TableLayoutPanel.CellPaint" /> event.</summary>
	// Token: 0x02000301 RID: 769
	public class TableLayoutCellPaintEventArgs : PaintEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> class.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
		/// <param name="clipRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint.</param>
		/// <param name="cellBounds">The bounds of the cell.</param>
		/// <param name="column">The column of the cell.</param>
		/// <param name="row">The row of the cell.</param>
		// Token: 0x06003367 RID: 13159 RVA: 0x000C28F8 File Offset: 0x000C0AF8
		public TableLayoutCellPaintEventArgs(Graphics g, Rectangle clipRectangle, Rectangle cellBounds, int column, int row)
			: base(g, clipRectangle)
		{
			this.cell_bounds = cellBounds;
			this.column = column;
			this.row = row;
		}

		/// <summary>Gets the size and location of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the cell.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000C291C File Offset: 0x000C0B1C
		public Rectangle CellBounds
		{
			get
			{
				return this.cell_bounds;
			}
		}

		/// <summary>Gets the column of the cell.</summary>
		/// <returns>The column position of the cell.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000C2924 File Offset: 0x000C0B24
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		/// <summary>Gets the row of the cell.</summary>
		/// <returns>The row position of the cell.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000C292C File Offset: 0x000C0B2C
		public int Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x0400185E RID: 6238
		private Rectangle cell_bounds;

		// Token: 0x0400185F RID: 6239
		private int column;

		// Token: 0x04001860 RID: 6240
		private int row;
	}
}
