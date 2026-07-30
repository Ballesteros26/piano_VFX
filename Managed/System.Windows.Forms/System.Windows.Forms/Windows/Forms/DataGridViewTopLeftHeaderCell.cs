using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the cell in the top left corner of the <see cref="T:System.Windows.Forms.DataGridView" /> that sits above the row headers and to the left of the column headers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013B RID: 315
	public class DataGridViewTopLeftHeaderCell : DataGridViewColumnHeaderCell
	{
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001608 RID: 5640 RVA: 0x000518D0 File Offset: 0x0004FAD0
		public override string ToString()
		{
			return base.GetType().Name;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />. </returns>
		// Token: 0x06001609 RID: 5641 RVA: 0x000518E0 File Offset: 0x0004FAE0
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> object and cell style.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600160A RID: 5642 RVA: 0x000518E8 File Offset: 0x0004FAE8
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(36, 13);
			return new Rectangle(2, (base.DataGridView.ColumnHeadersHeight - size.Height) / 2, size.Width, size.Height);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600160B RID: 5643 RVA: 0x0005193C File Offset: 0x0004FB3C
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(12, 11);
			return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> does not equal -1.</exception>
		// Token: 0x0600160C RID: 5644 RVA: 0x000519B0 File Offset: 0x0004FBB0
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object value = base.Value;
			if (value != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, value.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 17);
				size.Width += 29;
				return size;
			}
			return new Size(39, 17);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00051A10 File Offset: 0x0004FC10
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the current cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
		// Token: 0x0600160E RID: 5646 RVA: 0x00051A38 File Offset: 0x0004FC38
		protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
		}

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> to accessibility client applications.</summary>
		// Token: 0x0200013C RID: 316
		protected class DataGridViewTopLeftHeaderCellAccessibleObject : DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</param>
			// Token: 0x0600160F RID: 5647 RVA: 0x00051A48 File Offset: 0x0004FC48
			public DataGridViewTopLeftHeaderCellAccessibleObject(DataGridViewTopLeftHeaderCell owner)
				: base(owner)
			{
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x06001610 RID: 5648 RVA: 0x00051A54 File Offset: 0x0004FC54
			public override Rectangle Bounds
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets a description of the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>The string "Press to Select All" if the <see cref="P:System.Windows.Forms.DataGridView.MultiSelect" /> property is true; otherwise, an empty string ("").</returns>
			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06001611 RID: 5649 RVA: 0x00051A5C File Offset: 0x0004FC5C
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.DataGridView != null && base.Owner.DataGridView.MultiSelect)
					{
						return "Press to Select All";
					}
					return string.Empty;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</returns>
			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06001612 RID: 5650 RVA: 0x00051A9C File Offset: 0x0004FC9C
			public override string Name
			{
				get
				{
					return base.Name;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" />.</returns>
			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06001613 RID: 5651 RVA: 0x00051AA4 File Offset: 0x0004FCA4
			public override AccessibleStates State
			{
				get
				{
					return base.State;
				}
			}

			/// <summary>The value of the containing <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
			/// <returns>Always returns <see cref="F:System.String.Empty" />.</returns>
			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06001614 RID: 5652 RVA: 0x00051AAC File Offset: 0x0004FCAC
			public override string Value
			{
				get
				{
					return base.Value;
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewTopLeftHeaderCell" />.</summary>
			// Token: 0x06001615 RID: 5653 RVA: 0x00051AB4 File Offset: 0x0004FCB4
			public override void DoDefaultAction()
			{
				if (base.Owner.DataGridView != null)
				{
					base.Owner.DataGridView.SelectAll();
				}
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			// Token: 0x06001616 RID: 5654 RVA: 0x00051AE4 File Offset: 0x0004FCE4
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				throw new NotImplementedException();
			}

			/// <summary>Modifies the selection in the <see cref="T:System.Windows.Forms.DataGridView" /> control or sets input focus to the control. </summary>
			/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values. </param>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property value is null.</exception>
			// Token: 0x06001617 RID: 5655 RVA: 0x00051AEC File Offset: 0x0004FCEC
			public override void Select(AccessibleSelection flags)
			{
				base.Select(flags);
			}
		}
	}
}
