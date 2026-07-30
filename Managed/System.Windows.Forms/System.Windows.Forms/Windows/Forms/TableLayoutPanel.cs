using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel that dynamically lays out its contents in a grid composed of rows and columns.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000304 RID: 772
	[ProvideProperty("RowSpan", typeof(Control))]
	[ClassInterface(1)]
	[Designer("System.Windows.Forms.Design.TableLayoutPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("ColumnCount")]
	[ComVisible(true)]
	[ProvideProperty("Row", typeof(Control))]
	[ProvideProperty("ColumnSpan", typeof(Control))]
	[ProvideProperty("CellPosition", typeof(Control))]
	[DesignerSerializer("System.Windows.Forms.Design.TableLayoutPanelCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ProvideProperty("Column", typeof(Control))]
	[Docking(DockingBehavior.Never)]
	public class TableLayoutPanel : Panel, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> class.</summary>
		// Token: 0x06003376 RID: 13174 RVA: 0x000C29FC File Offset: 0x000C0BFC
		public TableLayoutPanel()
		{
			this.settings = new TableLayoutSettings(this);
			this.cell_border_style = TableLayoutPanelCellBorderStyle.None;
			this.column_widths = new int[0];
			this.row_heights = new int[0];
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000C2A30 File Offset: 0x000C0C30
		// Note: this type is marked as 'beforefieldinit'.
		static TableLayoutPanel()
		{
			TableLayoutPanel.CellPaintEvent = new object();
		}

		/// <summary>Occurs when the cell is redrawn.</summary>
		// Token: 0x1400032C RID: 812
		// (add) Token: 0x06003378 RID: 13176 RVA: 0x000C2A48 File Offset: 0x000C0C48
		// (remove) Token: 0x06003379 RID: 13177 RVA: 0x000C2A5C File Offset: 0x000C0C5C
		public event TableLayoutCellPaintEventHandler CellPaint
		{
			add
			{
				base.Events.AddHandler(TableLayoutPanel.CellPaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TableLayoutPanel.CellPaintEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		// Token: 0x0600337A RID: 13178 RVA: 0x000C2A70 File Offset: 0x000C0C70
		bool IExtenderProvider.CanExtend(object obj)
		{
			return obj is Control && (obj as Control).Parent == this;
		}

		/// <summary>Gets or sets the border style for the panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values describing the style of the border of the panel. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000C2A94 File Offset: 0x000C0C94
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000C2A9C File Offset: 0x000C0C9C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[Localizable(true)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the style of the cell borders.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellBorderStyle" /> values describing the style of all the cell borders in the table. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelCellBorderStyle.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000C2AA8 File Offset: 0x000C0CA8
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x000C2AB0 File Offset: 0x000C0CB0
		[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
		[Localizable(true)]
		public TableLayoutPanelCellBorderStyle CellBorderStyle
		{
			get
			{
				return this.cell_border_style;
			}
			set
			{
				if (this.cell_border_style != value)
				{
					this.cell_border_style = value;
					base.PerformLayout(this, "CellBorderStyle");
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the number of columns in the table.</summary>
		/// <returns>The number of columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000C2AD8 File Offset: 0x000C0CD8
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x000C2AE8 File Offset: 0x000C0CE8
		[DefaultValue(0)]
		[Localizable(true)]
		public int ColumnCount
		{
			get
			{
				return this.settings.ColumnCount;
			}
			set
			{
				this.settings.ColumnCount = value;
			}
		}

		/// <summary>Gets a collection of column styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> containing a <see cref="T:System.Windows.Forms.ColumnStyle" /> for each column in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
		[DesignerSerializationVisibility(2)]
		[MergableProperty(false)]
		[DisplayName("Columns")]
		[Browsable(false)]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				return this.settings.ColumnStyles;
			}
		}

		/// <summary>Gets the collection of controls contained within the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutControlCollection" /> containing the controls associated with the current <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000C2B08 File Offset: 0x000C0D08
		[DesignerSerializationVisibility(2)]
		[Browsable(false)]
		public new TableLayoutControlCollection Controls
		{
			get
			{
				return (TableLayoutControlCollection)base.Controls;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control should expand to accommodate new cells when all existing cells are occupied.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> indicating the growth scheme. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is invalid for the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> enumeration.</exception>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000C2B18 File Offset: 0x000C0D18
		// (set) Token: 0x06003384 RID: 13188 RVA: 0x000C2B28 File Offset: 0x000C0D28
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return this.settings.GrowStyle;
			}
			set
			{
				this.settings.GrowStyle = value;
			}
		}

		/// <summary>Gets a cached instance of the panel's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000C2B38 File Offset: 0x000C0D38
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return TableLayoutPanel.layout_engine;
			}
		}

		/// <summary>Gets or sets a value representing the table layout settings.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutSettings" /> containing the table layout settings.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value is null, or an attempt was made to set <see cref="T:System.Windows.Forms.TableLayoutSettings" />  directly, which is not supported; instead, set individual properties.</exception>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000C2B40 File Offset: 0x000C0D40
		// (set) Token: 0x06003387 RID: 13191 RVA: 0x000C2B48 File Offset: 0x000C0D48
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public TableLayoutSettings LayoutSettings
		{
			get
			{
				return this.settings;
			}
			set
			{
				if (value.isSerialized)
				{
					value.ColumnCount = value.ColumnStyles.Count;
					value.RowCount = value.RowStyles.Count;
					value.panel = this;
					this.settings = value;
					value.isSerialized = false;
					return;
				}
				throw new NotSupportedException("LayoutSettings value cannot be set directly.");
			}
		}

		/// <summary>Gets or sets the number of rows in the table.</summary>
		/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000C2BA8 File Offset: 0x000C0DA8
		// (set) Token: 0x06003389 RID: 13193 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
		[DefaultValue(0)]
		[Localizable(true)]
		public int RowCount
		{
			get
			{
				return this.settings.RowCount;
			}
			set
			{
				this.settings.RowCount = value;
			}
		}

		/// <summary>Gets a collection of row styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> containing a <see cref="T:System.Windows.Forms.RowStyle" /> for each row in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000C2BC8 File Offset: 0x000C0DC8
		[DisplayName("Rows")]
		[DesignerSerializationVisibility(2)]
		[MergableProperty(false)]
		[Browsable(false)]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				return this.settings.RowStyles;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		/// <param name="control">A control contained within a cell.</param>
		// Token: 0x0600338B RID: 13195 RVA: 0x000C2BD8 File Offset: 0x000C0DD8
		[DesignerSerializationVisibility(0)]
		[DefaultValue(-1)]
		[DisplayName("Cell")]
		public TableLayoutPanelCellPosition GetCellPosition(Control control)
		{
			return this.settings.GetCellPosition(control);
		}

		/// <summary>Returns the column position of the specified child control.</summary>
		/// <returns>The column position of the specified child control, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600338C RID: 13196 RVA: 0x000C2BE8 File Offset: 0x000C0DE8
		[DesignerSerializationVisibility(0)]
		[DisplayName("Column")]
		[DefaultValue(-1)]
		public int GetColumn(Control control)
		{
			return this.settings.GetColumn(control);
		}

		/// <summary>Returns the number of columns spanned by the specified child control.</summary>
		/// <returns>The number of columns spanned by the child control. The default is 1.</returns>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600338D RID: 13197 RVA: 0x000C2BF8 File Offset: 0x000C0DF8
		[DefaultValue(1)]
		[DisplayName("ColumnSpan")]
		public int GetColumnSpan(Control control)
		{
			return this.settings.GetColumnSpan(control);
		}

		/// <summary>Returns an array representing the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>An array of type <see cref="T:System.Int32" /> that contains the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		// Token: 0x0600338E RID: 13198 RVA: 0x000C2C08 File Offset: 0x000C0E08
		[Browsable(false)]
		[EditorBrowsable(1)]
		public int[] GetColumnWidths()
		{
			return this.column_widths;
		}

		/// <summary>Returns the child control occupying the specified position.</summary>
		/// <returns>The child control occupying the specified cell; otherwise, null if no control exists at the specified column and row, or if the control has its <see cref="P:System.Windows.Forms.Control.Visible" /> property set to false.</returns>
		/// <param name="column">The column position of the control to retrieve.</param>
		/// <param name="row">The row position of the control to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">Either <paramref name="column" /> or <paramref name="row" /> (or both) is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600338F RID: 13199 RVA: 0x000C2C10 File Offset: 0x000C0E10
		public Control GetControlFromPosition(int column, int row)
		{
			if (column < 0 || row < 0)
			{
				throw new ArgumentException();
			}
			TableLayoutPanelCellPosition tableLayoutPanelCellPosition = new TableLayoutPanelCellPosition(column, row);
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (this.settings.GetCellPosition(control) == tableLayoutPanelCellPosition)
				{
					return control;
				}
			}
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell that contains the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		/// <param name="control">A control contained within a cell.</param>
		// Token: 0x06003390 RID: 13200 RVA: 0x000C2CBC File Offset: 0x000C0EBC
		public TableLayoutPanelCellPosition GetPositionFromControl(Control control)
		{
			for (int i = 0; i < this.actual_positions.GetLength(0); i++)
			{
				for (int j = 0; j < this.actual_positions.GetLength(1); j++)
				{
					if (this.actual_positions[i, j] == control)
					{
						return new TableLayoutPanelCellPosition(i, j);
					}
				}
			}
			return new TableLayoutPanelCellPosition(-1, -1);
		}

		/// <summary>Returns the row position of the specified child control.</summary>
		/// <returns>The row position of <paramref name="control" />, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003391 RID: 13201 RVA: 0x000C2D28 File Offset: 0x000C0F28
		[DefaultValue("-1")]
		[DesignerSerializationVisibility(0)]
		[DisplayName("Row")]
		public int GetRow(Control control)
		{
			return this.settings.GetRow(control);
		}

		/// <summary>Returns an array representing the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>An array of type <see cref="T:System.Int32" /> that contains the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		// Token: 0x06003392 RID: 13202 RVA: 0x000C2D38 File Offset: 0x000C0F38
		[EditorBrowsable(1)]
		[Browsable(false)]
		public int[] GetRowHeights()
		{
			return this.row_heights;
		}

		/// <summary>Returns the number of rows spanned by the specified child control.</summary>
		/// <returns>The number of rows spanned by the child control. The default is 1.</returns>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003393 RID: 13203 RVA: 0x000C2D40 File Offset: 0x000C0F40
		[DisplayName("RowSpan")]
		[DefaultValue(1)]
		public int GetRowSpan(Control control)
		{
			return this.settings.GetRowSpan(control);
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="position">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</param>
		// Token: 0x06003394 RID: 13204 RVA: 0x000C2D50 File Offset: 0x000C0F50
		public void SetCellPosition(Control control, TableLayoutPanelCellPosition position)
		{
			this.settings.SetCellPosition(control, position);
		}

		/// <summary>Sets the column position of the specified child control.</summary>
		/// <param name="control">The control to move to another column.</param>
		/// <param name="column">The column to which <paramref name="control" /> will be moved.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003395 RID: 13205 RVA: 0x000C2D60 File Offset: 0x000C0F60
		public void SetColumn(Control control, int column)
		{
			this.settings.SetColumn(control, column);
		}

		/// <summary>Sets the number of columns spanned by the child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <param name="value">The number of columns to span.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003396 RID: 13206 RVA: 0x000C2D70 File Offset: 0x000C0F70
		public void SetColumnSpan(Control control, int value)
		{
			this.settings.SetColumnSpan(control, value);
		}

		/// <summary>Sets the row position of the specified child control.</summary>
		/// <param name="control">The control to move to another row.</param>
		/// <param name="row">The row to which <paramref name="control" /> will be moved.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003397 RID: 13207 RVA: 0x000C2D80 File Offset: 0x000C0F80
		public void SetRow(Control control, int row)
		{
			this.settings.SetRow(control, row);
		}

		/// <summary>Sets the number of rows spanned by the child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <param name="value">The number of rows to span.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003398 RID: 13208 RVA: 0x000C2D90 File Offset: 0x000C0F90
		public void SetRowSpan(Control control, int value)
		{
			this.settings.SetRowSpan(control, value);
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003399 RID: 13209 RVA: 0x000C2DA0 File Offset: 0x000C0FA0
		[EditorBrowsable(2)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TableLayoutControlCollection(this);
		}

		/// <summary>Receives a call when the cell should be refreshed.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> that provides data for the event.</param>
		// Token: 0x0600339A RID: 13210 RVA: 0x000C2DA8 File Offset: 0x000C0FA8
		protected virtual void OnCellPaint(TableLayoutCellPaintEventArgs e)
		{
			TableLayoutCellPaintEventHandler tableLayoutCellPaintEventHandler = (TableLayoutCellPaintEventHandler)base.Events[TableLayoutPanel.CellPaintEvent];
			if (tableLayoutCellPaintEventHandler != null)
			{
				tableLayoutCellPaintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x0600339B RID: 13211 RVA: 0x000C2DDC File Offset: 0x000C0FDC
		[EditorBrowsable(2)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			base.Invalidate();
		}

		/// <summary>Paints the background of the panel.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />  that contains information about the panel to paint.</param>
		// Token: 0x0600339C RID: 13212 RVA: 0x000C2DEC File Offset: 0x000C0FEC
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			this.DrawCellBorders(e);
			int cellBorderWidth = TableLayoutPanel.GetCellBorderWidth(this.CellBorderStyle);
			int num = cellBorderWidth;
			int num2 = cellBorderWidth;
			for (int i = 0; i < this.column_widths.Length; i++)
			{
				for (int j = 0; j < this.row_heights.Length; j++)
				{
					this.OnCellPaint(new TableLayoutCellPaintEventArgs(e.Graphics, e.ClipRectangle, new Rectangle(num, num2, this.column_widths[i] + cellBorderWidth, this.row_heights[j] + cellBorderWidth), i, j));
					num2 += this.row_heights[j] + cellBorderWidth;
				}
				num += this.column_widths[i] + cellBorderWidth;
				num2 = cellBorderWidth;
			}
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" />  that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x0600339D RID: 13213 RVA: 0x000C2EA0 File Offset: 0x000C10A0
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>Performs the work of scaling the entire panel and any child controls.</summary>
		/// <param name="dx">The ratio by which to scale the control horizontally.</param>
		/// <param name="dy">The ratio by which to scale the control vertically</param>
		// Token: 0x0600339E RID: 13214 RVA: 0x000C2EAC File Offset: 0x000C10AC
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000C2EB8 File Offset: 0x000C10B8
		internal static int GetCellBorderWidth(TableLayoutPanelCellBorderStyle style)
		{
			switch (style)
			{
			case TableLayoutPanelCellBorderStyle.Single:
				return 1;
			case TableLayoutPanelCellBorderStyle.Inset:
			case TableLayoutPanelCellBorderStyle.Outset:
				return 2;
			case TableLayoutPanelCellBorderStyle.InsetDouble:
			case TableLayoutPanelCellBorderStyle.OutsetDouble:
			case TableLayoutPanelCellBorderStyle.OutsetPartial:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000C2EF4 File Offset: 0x000C10F4
		private void DrawCellBorders(PaintEventArgs e)
		{
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, base.Size);
			switch (this.CellBorderStyle)
			{
			case TableLayoutPanelCellBorderStyle.Single:
				this.DrawSingleBorder(e.Graphics, rectangle);
				break;
			case TableLayoutPanelCellBorderStyle.Inset:
				this.DrawInsetBorder(e.Graphics, rectangle);
				break;
			case TableLayoutPanelCellBorderStyle.InsetDouble:
				this.DrawInsetDoubleBorder(e.Graphics, rectangle);
				break;
			case TableLayoutPanelCellBorderStyle.Outset:
				this.DrawOutsetBorder(e.Graphics, rectangle);
				break;
			case TableLayoutPanelCellBorderStyle.OutsetDouble:
			case TableLayoutPanelCellBorderStyle.OutsetPartial:
				this.DrawOutsetDoubleBorder(e.Graphics, rectangle);
				break;
			}
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C2F9C File Offset: 0x000C119C
		private void DrawSingleBorder(Graphics g, Rectangle rect)
		{
			ControlPaint.DrawBorder(g, rect, SystemColors.ControlDark, ButtonBorderStyle.Solid);
			int num = this.DisplayRectangle.X;
			int num2 = this.DisplayRectangle.Y;
			for (int i = 0; i < this.column_widths.Length - 1; i++)
			{
				num += this.column_widths[i] + 1;
				g.DrawLine(SystemPens.ControlDark, new Point(num, 1), new Point(num, base.Bottom - 2));
			}
			for (int j = 0; j < this.row_heights.Length - 1; j++)
			{
				num2 += this.row_heights[j] + 1;
				g.DrawLine(SystemPens.ControlDark, new Point(1, num2), new Point(base.Right - 2, num2));
			}
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000C3068 File Offset: 0x000C1268
		private void DrawInsetBorder(Graphics g, Rectangle rect)
		{
			ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Etched);
			int num = this.DisplayRectangle.X;
			int num2 = this.DisplayRectangle.Y;
			for (int i = 0; i < this.column_widths.Length - 1; i++)
			{
				num += this.column_widths[i] + 2;
				g.DrawLine(SystemPens.ControlDark, new Point(num, 1), new Point(num, base.Bottom - 3));
				g.DrawLine(Pens.White, new Point(num + 1, 1), new Point(num + 1, base.Bottom - 3));
			}
			for (int j = 0; j < this.row_heights.Length - 1; j++)
			{
				num2 += this.row_heights[j] + 2;
				g.DrawLine(SystemPens.ControlDark, new Point(1, num2), new Point(base.Right - 3, num2));
				g.DrawLine(Pens.White, new Point(1, num2 + 1), new Point(base.Right - 3, num2 + 1));
			}
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C3178 File Offset: 0x000C1378
		private void DrawOutsetBorder(Graphics g, Rectangle rect)
		{
			g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2));
			g.DrawRectangle(Pens.White, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
			int num = this.DisplayRectangle.X;
			int num2 = this.DisplayRectangle.Y;
			for (int i = 0; i < this.column_widths.Length - 1; i++)
			{
				num += this.column_widths[i] + 2;
				g.DrawLine(Pens.White, new Point(num, 1), new Point(num, base.Bottom - 3));
				g.DrawLine(SystemPens.ControlDark, new Point(num + 1, 1), new Point(num + 1, base.Bottom - 3));
			}
			for (int j = 0; j < this.row_heights.Length - 1; j++)
			{
				num2 += this.row_heights[j] + 2;
				g.DrawLine(Pens.White, new Point(1, num2), new Point(base.Right - 3, num2));
				g.DrawLine(SystemPens.ControlDark, new Point(1, num2 + 1), new Point(base.Right - 3, num2 + 1));
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C32E4 File Offset: 0x000C14E4
		private void DrawOutsetDoubleBorder(Graphics g, Rectangle rect)
		{
			rect.Width--;
			rect.Height--;
			g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left + 2, rect.Top + 2, rect.Width - 2, rect.Height - 2));
			g.DrawRectangle(Pens.White, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
			int num = this.DisplayRectangle.X;
			int num2 = this.DisplayRectangle.Y;
			for (int i = 0; i < this.column_widths.Length - 1; i++)
			{
				num += this.column_widths[i] + 3;
				g.DrawLine(Pens.White, new Point(num, 3), new Point(num, base.Bottom - 5));
				g.DrawLine(SystemPens.ControlDark, new Point(num + 2, 3), new Point(num + 2, base.Bottom - 5));
			}
			for (int j = 0; j < this.row_heights.Length - 1; j++)
			{
				num2 += this.row_heights[j] + 3;
				g.DrawLine(Pens.White, new Point(3, num2), new Point(base.Right - 4, num2));
				g.DrawLine(SystemPens.ControlDark, new Point(3, num2 + 2), new Point(base.Right - 4, num2 + 2));
			}
			num = this.DisplayRectangle.X;
			num2 = this.DisplayRectangle.Y;
			for (int k = 0; k < this.column_widths.Length - 1; k++)
			{
				num += this.column_widths[k] + 3;
				g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.BackColor), new Point(num + 1, 3), new Point(num + 1, base.Bottom - 5));
			}
			for (int l = 0; l < this.row_heights.Length - 1; l++)
			{
				num2 += this.row_heights[l] + 3;
				g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.BackColor), new Point(3, num2 + 1), new Point(base.Right - 4, num2 + 1));
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000C3550 File Offset: 0x000C1750
		private void DrawInsetDoubleBorder(Graphics g, Rectangle rect)
		{
			rect.Width--;
			rect.Height--;
			g.DrawRectangle(Pens.White, new Rectangle(rect.Left + 2, rect.Top + 2, rect.Width - 2, rect.Height - 2));
			g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
			int num = this.DisplayRectangle.X;
			int num2 = this.DisplayRectangle.Y;
			for (int i = 0; i < this.column_widths.Length - 1; i++)
			{
				num += this.column_widths[i] + 3;
				g.DrawLine(SystemPens.ControlDark, new Point(num, 3), new Point(num, base.Bottom - 5));
				g.DrawLine(Pens.White, new Point(num + 2, 3), new Point(num + 2, base.Bottom - 5));
			}
			for (int j = 0; j < this.row_heights.Length - 1; j++)
			{
				num2 += this.row_heights[j] + 3;
				g.DrawLine(SystemPens.ControlDark, new Point(3, num2), new Point(base.Right - 4, num2));
				g.DrawLine(Pens.White, new Point(3, num2 + 2), new Point(base.Right - 4, num2 + 2));
			}
			num = this.DisplayRectangle.X;
			num2 = this.DisplayRectangle.Y;
			for (int k = 0; k < this.column_widths.Length - 1; k++)
			{
				num += this.column_widths[k] + 3;
				g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.BackColor), new Point(num + 1, 3), new Point(num + 1, base.Bottom - 5));
			}
			for (int l = 0; l < this.row_heights.Length - 1; l++)
			{
				num2 += this.row_heights[l] + 3;
				g.DrawLine(ThemeEngine.Current.ResPool.GetPen(this.BackColor), new Point(3, num2 + 1), new Point(base.Right - 4, num2 + 1));
			}
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000C37BC File Offset: 0x000C19BC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			this.actual_positions = (this.LayoutEngine as TableLayout).CalculateControlPositions(this, Math.Max(this.ColumnCount, 1), Math.Max(this.RowCount, 1));
			int length = this.actual_positions.GetLength(0);
			int length2 = this.actual_positions.GetLength(1);
			int[] array = new int[length];
			float num = 0f;
			for (int i = 0; i < length; i++)
			{
				if (i < this.ColumnStyles.Count && this.ColumnStyles[i].SizeType == SizeType.Percent)
				{
					num += this.ColumnStyles[i].Width;
				}
				int num2 = 0;
				for (int j = 0; j < length2; j++)
				{
					Control control = this.actual_positions[i, j];
					if (control != null)
					{
						if (!control.AutoSize)
						{
							num2 = Math.Max(num2, control.ExplicitBounds.Width + control.Margin.Horizontal + base.Padding.Horizontal);
						}
						else
						{
							num2 = Math.Max(num2, control.PreferredSize.Width + control.Margin.Horizontal + base.Padding.Horizontal);
						}
					}
				}
				array[i] = num2;
			}
			int num3 = 0;
			int num4 = 0;
			for (int k = 0; k < length; k++)
			{
				if (k < this.ColumnStyles.Count && this.ColumnStyles[k].SizeType == SizeType.Percent)
				{
					num4 = Math.Max(num4, (int)((float)array[k] / (this.ColumnStyles[k].Width / num)));
				}
				else
				{
					num3 += array[k];
				}
			}
			int[] array2 = new int[length2];
			float num5 = 0f;
			for (int l = 0; l < length2; l++)
			{
				if (l < this.RowStyles.Count && this.RowStyles[l].SizeType == SizeType.Percent)
				{
					num5 += this.RowStyles[l].Height;
				}
				int num6 = 0;
				for (int m = 0; m < length; m++)
				{
					Control control2 = this.actual_positions[m, l];
					if (control2 != null)
					{
						if (!control2.AutoSize)
						{
							num6 = Math.Max(num6, control2.ExplicitBounds.Height + control2.Margin.Vertical + base.Padding.Vertical);
						}
						else
						{
							num6 = Math.Max(num6, control2.PreferredSize.Height + control2.Margin.Vertical + base.Padding.Vertical);
						}
					}
				}
				array2[l] = num6;
			}
			int num7 = 0;
			int num8 = 0;
			for (int n = 0; n < length2; n++)
			{
				if (n < this.RowStyles.Count && this.RowStyles[n].SizeType == SizeType.Percent)
				{
					num8 = Math.Max(num8, (int)((float)array2[n] / (this.RowStyles[n].Height / num5)));
				}
				else
				{
					num7 += array2[n];
				}
			}
			int cellBorderWidth = TableLayoutPanel.GetCellBorderWidth(this.CellBorderStyle);
			return new Size(num3 + num4 + cellBorderWidth * (length + 1), num7 + num8 + cellBorderWidth * (length2 + 1));
		}

		// Token: 0x04001862 RID: 6242
		private TableLayoutSettings settings;

		// Token: 0x04001863 RID: 6243
		private static TableLayout layout_engine = new TableLayout();

		// Token: 0x04001864 RID: 6244
		private TableLayoutPanelCellBorderStyle cell_border_style;

		// Token: 0x04001865 RID: 6245
		internal Control[,] actual_positions;

		// Token: 0x04001866 RID: 6246
		internal int[] column_widths;

		// Token: 0x04001867 RID: 6247
		internal int[] row_heights;
	}
}
