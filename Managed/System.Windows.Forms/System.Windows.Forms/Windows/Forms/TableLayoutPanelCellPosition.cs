using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a cell in a <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	// Token: 0x02000306 RID: 774
	[TypeConverter(typeof(TableLayoutPanelCellPositionTypeConverter))]
	public struct TableLayoutPanelCellPosition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> class.</summary>
		/// <param name="column">The column position of the cell.</param>
		/// <param name="row">The row position of the cell.</param>
		// Token: 0x060033A7 RID: 13223 RVA: 0x000C3B70 File Offset: 0x000C1D70
		public TableLayoutPanelCellPosition(int column, int row)
		{
			this.column = column;
			this.row = row;
		}

		/// <summary>Gets or sets the column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>The column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000C3B80 File Offset: 0x000C1D80
		// (set) Token: 0x060033A9 RID: 13225 RVA: 0x000C3B88 File Offset: 0x000C1D88
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		/// <summary>Gets or sets the row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>The row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000C3B94 File Offset: 0x000C1D94
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x000C3B9C File Offset: 0x000C1D9C
		public int Row
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		/// <summary>Converts this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to a human readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x060033AC RID: 13228 RVA: 0x000C3BA8 File Offset: 0x000C1DA8
		public override string ToString()
		{
			return this.column.ToString() + "," + this.row.ToString();
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
		// Token: 0x060033AD RID: 13229 RVA: 0x000C3BD8 File Offset: 0x000C1DD8
		public override int GetHashCode()
		{
			return this.column.GetHashCode() ^ this.row.GetHashCode();
		}

		/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> contains the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
		/// <returns>true if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> and has the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to test.</param>
		// Token: 0x060033AE RID: 13230 RVA: 0x000C3BF4 File Offset: 0x000C1DF4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (!(other is TableLayoutPanelCellPosition))
			{
				return false;
			}
			TableLayoutPanelCellPosition tableLayoutPanelCellPosition = (TableLayoutPanelCellPosition)other;
			return tableLayoutPanelCellPosition.column == this.column && tableLayoutPanelCellPosition.row == this.row;
		}

		/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are equal.</summary>
		/// <returns>true if <paramref name="p1" /> and <paramref name="p2" /> are equal; otherwise, false.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		// Token: 0x060033AF RID: 13231 RVA: 0x000C3C44 File Offset: 0x000C1E44
		public static bool operator ==(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return p1.column == p2.column && p1.row == p2.row;
		}

		/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are unequal.</summary>
		/// <returns>true if <paramref name="p1" /> and <paramref name="p2" /> differ; otherwise, false.</returns>
		/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
		// Token: 0x060033B0 RID: 13232 RVA: 0x000C3C78 File Offset: 0x000C1E78
		public static bool operator !=(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return p1.column != p2.column || p1.row != p2.row;
		}

		// Token: 0x04001871 RID: 6257
		private int column;

		// Token: 0x04001872 RID: 6258
		private int row;
	}
}
