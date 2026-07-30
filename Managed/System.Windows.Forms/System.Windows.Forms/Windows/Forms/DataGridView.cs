using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays data in a customizable grid.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CF RID: 207
	[DefaultEvent("CellContentClick")]
	[ComVisible(true)]
	[ClassInterface(1)]
	[Designer("System.Windows.Forms.Design.DataGridViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Editor("System.Windows.Forms.Design.DataGridViewComponentEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor))]
	[ComplexBindingProperties("DataSource", "DataMember")]
	[Docking(DockingBehavior.Ask)]
	public class DataGridView : Control, IDisposable, IComponent, ISupportInitialize, IBindableComponent, IDropTarget
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView" /> class.</summary>
		// Token: 0x06000E2E RID: 3630 RVA: 0x000388AC File Offset: 0x00036AAC
		public DataGridView()
		{
			base.SetStyle(ControlStyles.Opaque, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.adjustedTopLeftHeaderBorderStyle = new DataGridViewAdvancedBorderStyle();
			this.adjustedTopLeftHeaderBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
			this.advancedCellBorderStyle = new DataGridViewAdvancedBorderStyle();
			this.advancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
			this.advancedColumnHeadersBorderStyle = new DataGridViewAdvancedBorderStyle();
			this.advancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
			this.advancedRowHeadersBorderStyle = new DataGridViewAdvancedBorderStyle();
			this.advancedRowHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
			this.alternatingRowsDefaultCellStyle = new DataGridViewCellStyle();
			this.allowUserToAddRows = true;
			this.allowUserToDeleteRows = true;
			this.allowUserToOrderColumns = false;
			this.allowUserToResizeColumns = true;
			this.allowUserToResizeRows = true;
			this.autoGenerateColumns = true;
			this.autoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			this.autoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			this.backColor = Control.DefaultBackColor;
			this.backgroundColor = SystemColors.AppWorkspace;
			this.borderStyle = BorderStyle.FixedSingle;
			this.cellBorderStyle = DataGridViewCellBorderStyle.Single;
			this.clipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
			this.columnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			this.columnHeadersDefaultCellStyle = new DataGridViewCellStyle();
			this.columnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
			this.columnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
			this.columnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			this.columnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			this.columnHeadersDefaultCellStyle.Font = this.Font;
			this.columnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.columnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
			this.columnHeadersHeight = 23;
			this.columnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			this.columnHeadersVisible = true;
			this.columns = this.CreateColumnsInstance();
			this.columns.CollectionChanged += new CollectionChangeEventHandler(this.OnColumnCollectionChanged);
			this.currentCellAddress = new Point(-1, -1);
			this.dataMember = string.Empty;
			this.defaultCellStyle = new DataGridViewCellStyle();
			this.defaultCellStyle.BackColor = SystemColors.Window;
			this.defaultCellStyle.ForeColor = SystemColors.ControlText;
			this.defaultCellStyle.SelectionBackColor = SystemColors.Highlight;
			this.defaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
			this.defaultCellStyle.Font = this.Font;
			this.defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.defaultCellStyle.WrapMode = DataGridViewTriState.False;
			this.editMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
			this.firstDisplayedScrollingColumnHiddenWidth = 0;
			this.isCurrentCellDirty = false;
			this.multiSelect = true;
			this.readOnly = false;
			this.rowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			this.rowHeadersDefaultCellStyle = this.columnHeadersDefaultCellStyle.Clone();
			this.rowHeadersVisible = true;
			this.rowHeadersWidth = 41;
			this.rowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
			this.rows = this.CreateRowsInstance();
			this.rowsDefaultCellStyle = new DataGridViewCellStyle();
			this.selectionMode = DataGridViewSelectionMode.RowHeaderSelect;
			this.showCellErrors = true;
			this.showEditingIcon = true;
			this.scrollBars = ScrollBars.Both;
			this.userSetCursor = Cursor.Current;
			this.virtualMode = false;
			this.horizontalScrollBar = new HScrollBar();
			this.horizontalScrollBar.Scroll += this.OnHScrollBarScroll;
			this.horizontalScrollBar.Visible = false;
			this.verticalScrollBar = new VScrollBar();
			this.verticalScrollBar.Scroll += this.OnVScrollBarScroll;
			this.verticalScrollBar.Visible = false;
			base.Controls.AddRange(new Control[] { this.horizontalScrollBar, this.verticalScrollBar });
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00038C1C File Offset: 0x00036E1C
		// Note: this type is marked as 'beforefieldinit'.
		static DataGridView()
		{
			DataGridView.AllowUserToAddRowsChangedEvent = new object();
			DataGridView.AllowUserToDeleteRowsChangedEvent = new object();
			DataGridView.AllowUserToOrderColumnsChangedEvent = new object();
			DataGridView.AllowUserToResizeColumnsChangedEvent = new object();
			DataGridView.AllowUserToResizeRowsChangedEvent = new object();
			DataGridView.AlternatingRowsDefaultCellStyleChangedEvent = new object();
			DataGridView.AutoGenerateColumnsChangedEvent = new object();
			DataGridView.AutoSizeColumnModeChangedEvent = new object();
			DataGridView.AutoSizeColumnsModeChangedEvent = new object();
			DataGridView.AutoSizeRowsModeChangedEvent = new object();
			DataGridView.BackgroundColorChangedEvent = new object();
			DataGridView.BorderStyleChangedEvent = new object();
			DataGridView.CancelRowEditEvent = new object();
			DataGridView.CellBeginEditEvent = new object();
			DataGridView.CellBorderStyleChangedEvent = new object();
			DataGridView.CellClickEvent = new object();
			DataGridView.CellContentClickEvent = new object();
			DataGridView.CellContentDoubleClickEvent = new object();
			DataGridView.CellContextMenuStripChangedEvent = new object();
			DataGridView.CellContextMenuStripNeededEvent = new object();
			DataGridView.CellDoubleClickEvent = new object();
			DataGridView.CellEndEditEvent = new object();
			DataGridView.CellEnterEvent = new object();
			DataGridView.CellErrorTextChangedEvent = new object();
			DataGridView.CellErrorTextNeededEvent = new object();
			DataGridView.CellFormattingEvent = new object();
			DataGridView.CellLeaveEvent = new object();
			DataGridView.CellMouseClickEvent = new object();
			DataGridView.CellMouseDoubleClickEvent = new object();
			DataGridView.CellMouseDownEvent = new object();
			DataGridView.CellMouseEnterEvent = new object();
			DataGridView.CellMouseLeaveEvent = new object();
			DataGridView.CellMouseMoveEvent = new object();
			DataGridView.CellMouseUpEvent = new object();
			DataGridView.CellPaintingEvent = new object();
			DataGridView.CellParsingEvent = new object();
			DataGridView.CellStateChangedEvent = new object();
			DataGridView.CellStyleChangedEvent = new object();
			DataGridView.CellStyleContentChangedEvent = new object();
			DataGridView.CellToolTipTextChangedEvent = new object();
			DataGridView.CellToolTipTextNeededEvent = new object();
			DataGridView.CellValidatedEvent = new object();
			DataGridView.CellValidatingEvent = new object();
			DataGridView.CellValueChangedEvent = new object();
			DataGridView.CellValueNeededEvent = new object();
			DataGridView.CellValuePushedEvent = new object();
			DataGridView.ColumnAddedEvent = new object();
			DataGridView.ColumnContextMenuStripChangedEvent = new object();
			DataGridView.ColumnDataPropertyNameChangedEvent = new object();
			DataGridView.ColumnDefaultCellStyleChangedEvent = new object();
			DataGridView.ColumnDisplayIndexChangedEvent = new object();
			DataGridView.ColumnDividerDoubleClickEvent = new object();
			DataGridView.ColumnDividerWidthChangedEvent = new object();
			DataGridView.ColumnHeaderCellChangedEvent = new object();
			DataGridView.ColumnHeaderMouseClickEvent = new object();
			DataGridView.ColumnHeaderMouseDoubleClickEvent = new object();
			DataGridView.ColumnHeadersBorderStyleChangedEvent = new object();
			DataGridView.ColumnHeadersDefaultCellStyleChangedEvent = new object();
			DataGridView.ColumnHeadersHeightChangedEvent = new object();
			DataGridView.ColumnHeadersHeightSizeModeChangedEvent = new object();
			DataGridView.ColumnMinimumWidthChangedEvent = new object();
			DataGridView.ColumnNameChangedEvent = new object();
			DataGridView.ColumnRemovedEvent = new object();
			DataGridView.ColumnSortModeChangedEvent = new object();
			DataGridView.ColumnStateChangedEvent = new object();
			DataGridView.ColumnToolTipTextChangedEvent = new object();
			DataGridView.ColumnWidthChangedEvent = new object();
			DataGridView.CurrentCellChangedEvent = new object();
			DataGridView.CurrentCellDirtyStateChangedEvent = new object();
			DataGridView.DataBindingCompleteEvent = new object();
			DataGridView.DataErrorEvent = new object();
			DataGridView.DataMemberChangedEvent = new object();
			DataGridView.DataSourceChangedEvent = new object();
			DataGridView.DefaultCellStyleChangedEvent = new object();
			DataGridView.DefaultValuesNeededEvent = new object();
			DataGridView.EditingControlShowingEvent = new object();
			DataGridView.EditModeChangedEvent = new object();
			DataGridView.GridColorChangedEvent = new object();
			DataGridView.MultiSelectChangedEvent = new object();
			DataGridView.NewRowNeededEvent = new object();
			DataGridView.ReadOnlyChangedEvent = new object();
			DataGridView.RowContextMenuStripChangedEvent = new object();
			DataGridView.RowContextMenuStripNeededEvent = new object();
			DataGridView.RowDefaultCellStyleChangedEvent = new object();
			DataGridView.RowDirtyStateNeededEvent = new object();
			DataGridView.RowDividerDoubleClickEvent = new object();
			DataGridView.RowDividerHeightChangedEvent = new object();
			DataGridView.RowEnterEvent = new object();
			DataGridView.RowErrorTextChangedEvent = new object();
			DataGridView.RowErrorTextNeededEvent = new object();
			DataGridView.RowHeaderCellChangedEvent = new object();
			DataGridView.RowHeaderMouseClickEvent = new object();
			DataGridView.RowHeaderMouseDoubleClickEvent = new object();
			DataGridView.RowHeadersBorderStyleChangedEvent = new object();
			DataGridView.RowHeadersDefaultCellStyleChangedEvent = new object();
			DataGridView.RowHeadersWidthChangedEvent = new object();
			DataGridView.RowHeadersWidthSizeModeChangedEvent = new object();
			DataGridView.RowHeightChangedEvent = new object();
			DataGridView.RowHeightInfoNeededEvent = new object();
			DataGridView.RowHeightInfoPushedEvent = new object();
			DataGridView.RowLeaveEvent = new object();
			DataGridView.RowMinimumHeightChangedEvent = new object();
			DataGridView.RowPostPaintEvent = new object();
			DataGridView.RowPrePaintEvent = new object();
			DataGridView.RowsAddedEvent = new object();
			DataGridView.RowsDefaultCellStyleChangedEvent = new object();
			DataGridView.RowsRemovedEvent = new object();
			DataGridView.RowStateChangedEvent = new object();
			DataGridView.RowUnsharedEvent = new object();
			DataGridView.RowValidatedEvent = new object();
			DataGridView.RowValidatingEvent = new object();
			DataGridView.ScrollEvent = new object();
			DataGridView.SelectionChangedEvent = new object();
			DataGridView.SortCompareEvent = new object();
			DataGridView.SortedEvent = new object();
			DataGridView.UserAddedRowEvent = new object();
			DataGridView.UserDeletedRowEvent = new object();
			DataGridView.UserDeletingRowEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06000E30 RID: 3632 RVA: 0x000390C8 File Offset: 0x000372C8
		// (remove) Token: 0x06000E31 RID: 3633 RVA: 0x000390DC File Offset: 0x000372DC
		public event EventHandler AllowUserToAddRowsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AllowUserToAddRowsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AllowUserToAddRowsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToDeleteRowsChanged" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06000E32 RID: 3634 RVA: 0x000390F0 File Offset: 0x000372F0
		// (remove) Token: 0x06000E33 RID: 3635 RVA: 0x00039104 File Offset: 0x00037304
		public event EventHandler AllowUserToDeleteRowsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AllowUserToDeleteRowsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AllowUserToDeleteRowsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToOrderColumns" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06000E34 RID: 3636 RVA: 0x00039118 File Offset: 0x00037318
		// (remove) Token: 0x06000E35 RID: 3637 RVA: 0x0003912C File Offset: 0x0003732C
		public event EventHandler AllowUserToOrderColumnsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AllowUserToOrderColumnsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AllowUserToOrderColumnsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeColumns" /> property changes.</summary>
		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06000E36 RID: 3638 RVA: 0x00039140 File Offset: 0x00037340
		// (remove) Token: 0x06000E37 RID: 3639 RVA: 0x00039154 File Offset: 0x00037354
		public event EventHandler AllowUserToResizeColumnsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AllowUserToResizeColumnsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AllowUserToResizeColumnsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property changes.</summary>
		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06000E38 RID: 3640 RVA: 0x00039168 File Offset: 0x00037368
		// (remove) Token: 0x06000E39 RID: 3641 RVA: 0x0003917C File Offset: 0x0003737C
		public event EventHandler AllowUserToResizeRowsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AllowUserToResizeRowsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AllowUserToResizeRowsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06000E3A RID: 3642 RVA: 0x00039190 File Offset: 0x00037390
		// (remove) Token: 0x06000E3B RID: 3643 RVA: 0x000391A4 File Offset: 0x000373A4
		public event EventHandler AlternatingRowsDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AlternatingRowsDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AlternatingRowsDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="E:System.Windows.Forms.DataGridView.AutoGenerateColumnsChanged" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06000E3C RID: 3644 RVA: 0x000391B8 File Offset: 0x000373B8
		// (remove) Token: 0x06000E3D RID: 3645 RVA: 0x000391CC File Offset: 0x000373CC
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler AutoGenerateColumnsChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AutoGenerateColumnsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AutoGenerateColumnsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of a column changes.</summary>
		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06000E3E RID: 3646 RVA: 0x000391E0 File Offset: 0x000373E0
		// (remove) Token: 0x06000E3F RID: 3647 RVA: 0x000391F4 File Offset: 0x000373F4
		public event DataGridViewAutoSizeColumnModeEventHandler AutoSizeColumnModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AutoSizeColumnModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AutoSizeColumnModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeColumnsMode" /> property changes.</summary>
		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06000E40 RID: 3648 RVA: 0x00039208 File Offset: 0x00037408
		// (remove) Token: 0x06000E41 RID: 3649 RVA: 0x0003921C File Offset: 0x0003741C
		public event DataGridViewAutoSizeColumnsModeEventHandler AutoSizeColumnsModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AutoSizeColumnsModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AutoSizeColumnsModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06000E42 RID: 3650 RVA: 0x00039230 File Offset: 0x00037430
		// (remove) Token: 0x06000E43 RID: 3651 RVA: 0x00039244 File Offset: 0x00037444
		public event DataGridViewAutoSizeModeEventHandler AutoSizeRowsModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.AutoSizeRowsModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.AutoSizeRowsModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x06000E44 RID: 3652 RVA: 0x00039258 File Offset: 0x00037458
		// (remove) Token: 0x06000E45 RID: 3653 RVA: 0x00039264 File Offset: 0x00037464
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.BackgroundColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06000E46 RID: 3654 RVA: 0x00039270 File Offset: 0x00037470
		// (remove) Token: 0x06000E47 RID: 3655 RVA: 0x00039284 File Offset: 0x00037484
		public event EventHandler BackgroundColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.BackgroundColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.BackgroundColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.BackgroundImage" /> property changes.</summary>
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06000E48 RID: 3656 RVA: 0x00039298 File Offset: 0x00037498
		// (remove) Token: 0x06000E49 RID: 3657 RVA: 0x000392A4 File Offset: 0x000374A4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06000E4A RID: 3658 RVA: 0x000392B0 File Offset: 0x000374B0
		// (remove) Token: 0x06000E4B RID: 3659 RVA: 0x000392BC File Offset: 0x000374BC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.BorderStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06000E4C RID: 3660 RVA: 0x000392C8 File Offset: 0x000374C8
		// (remove) Token: 0x06000E4D RID: 3661 RVA: 0x000392DC File Offset: 0x000374DC
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.BorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.BorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of a <see cref="T:System.Windows.Forms.DataGridView" /> control is true and the cancels edits in a row.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06000E4E RID: 3662 RVA: 0x000392F0 File Offset: 0x000374F0
		// (remove) Token: 0x06000E4F RID: 3663 RVA: 0x00039304 File Offset: 0x00037504
		public event QuestionEventHandler CancelRowEdit
		{
			add
			{
				base.Events.AddHandler(DataGridView.CancelRowEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CancelRowEditEvent, value);
			}
		}

		/// <summary>Occurs when edit mode starts for the selected cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06000E50 RID: 3664 RVA: 0x00039318 File Offset: 0x00037518
		// (remove) Token: 0x06000E51 RID: 3665 RVA: 0x0003932C File Offset: 0x0003752C
		public event DataGridViewCellCancelEventHandler CellBeginEdit
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellBeginEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellBeginEditEvent, value);
			}
		}

		/// <summary>Occurs when the border style of a cell changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06000E52 RID: 3666 RVA: 0x00039340 File Offset: 0x00037540
		// (remove) Token: 0x06000E53 RID: 3667 RVA: 0x00039354 File Offset: 0x00037554
		public event EventHandler CellBorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellBorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellBorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when any part of a cell is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06000E54 RID: 3668 RVA: 0x00039368 File Offset: 0x00037568
		// (remove) Token: 0x06000E55 RID: 3669 RVA: 0x0003937C File Offset: 0x0003757C
		public event DataGridViewCellEventHandler CellClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellClickEvent, value);
			}
		}

		/// <summary>Occurs when the content within a cell is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06000E56 RID: 3670 RVA: 0x00039390 File Offset: 0x00037590
		// (remove) Token: 0x06000E57 RID: 3671 RVA: 0x000393A4 File Offset: 0x000375A4
		public event DataGridViewCellEventHandler CellContentClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellContentClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellContentClickEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks a cell's contents.</summary>
		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06000E58 RID: 3672 RVA: 0x000393B8 File Offset: 0x000375B8
		// (remove) Token: 0x06000E59 RID: 3673 RVA: 0x000393CC File Offset: 0x000375CC
		public event DataGridViewCellEventHandler CellContentDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellContentDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellContentDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewCell.ContextMenuStrip" /> property changes. </summary>
		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06000E5A RID: 3674 RVA: 0x000393E0 File Offset: 0x000375E0
		// (remove) Token: 0x06000E5B RID: 3675 RVA: 0x000393F4 File Offset: 0x000375F4
		[EditorBrowsable(2)]
		public event DataGridViewCellEventHandler CellContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellContextMenuStripChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellContextMenuStripChangedEvent, value);
			}
		}

		/// <summary>Occurs when a cell's shortcut menu is needed. </summary>
		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06000E5C RID: 3676 RVA: 0x00039408 File Offset: 0x00037608
		// (remove) Token: 0x06000E5D RID: 3677 RVA: 0x0003941C File Offset: 0x0003761C
		[EditorBrowsable(2)]
		public event DataGridViewCellContextMenuStripNeededEventHandler CellContextMenuStripNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellContextMenuStripNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellContextMenuStripNeededEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks anywhere in a cell.</summary>
		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06000E5E RID: 3678 RVA: 0x00039430 File Offset: 0x00037630
		// (remove) Token: 0x06000E5F RID: 3679 RVA: 0x00039444 File Offset: 0x00037644
		public event DataGridViewCellEventHandler CellDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when edit mode stops for the currently selected cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000103 RID: 259
		// (add) Token: 0x06000E60 RID: 3680 RVA: 0x00039458 File Offset: 0x00037658
		// (remove) Token: 0x06000E61 RID: 3681 RVA: 0x0003946C File Offset: 0x0003766C
		public event DataGridViewCellEventHandler CellEndEdit
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellEndEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellEndEditEvent, value);
			}
		}

		/// <summary>Occurs when the current cell changes in the <see cref="T:System.Windows.Forms.DataGridView" /> control or when the control receives input focus. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06000E62 RID: 3682 RVA: 0x00039480 File Offset: 0x00037680
		// (remove) Token: 0x06000E63 RID: 3683 RVA: 0x00039494 File Offset: 0x00037694
		public event DataGridViewCellEventHandler CellEnter
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellEnterEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ErrorText" /> property of a cell changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06000E64 RID: 3684 RVA: 0x000394A8 File Offset: 0x000376A8
		// (remove) Token: 0x06000E65 RID: 3685 RVA: 0x000394BC File Offset: 0x000376BC
		public event DataGridViewCellEventHandler CellErrorTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellErrorTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellErrorTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when a cell's error text is needed.</summary>
		// Token: 0x14000106 RID: 262
		// (add) Token: 0x06000E66 RID: 3686 RVA: 0x000394D0 File Offset: 0x000376D0
		// (remove) Token: 0x06000E67 RID: 3687 RVA: 0x000394E4 File Offset: 0x000376E4
		[EditorBrowsable(2)]
		public event DataGridViewCellErrorTextNeededEventHandler CellErrorTextNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellErrorTextNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellErrorTextNeededEvent, value);
			}
		}

		/// <summary>Occurs when the contents of a cell need to be formatted for display.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06000E68 RID: 3688 RVA: 0x000394F8 File Offset: 0x000376F8
		// (remove) Token: 0x06000E69 RID: 3689 RVA: 0x0003950C File Offset: 0x0003770C
		public event DataGridViewCellFormattingEventHandler CellFormatting
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellFormattingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellFormattingEvent, value);
			}
		}

		/// <summary>Occurs when a cell loses input focus and is no longer the current cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000108 RID: 264
		// (add) Token: 0x06000E6A RID: 3690 RVA: 0x00039520 File Offset: 0x00037720
		// (remove) Token: 0x06000E6B RID: 3691 RVA: 0x00039534 File Offset: 0x00037734
		public event DataGridViewCellEventHandler CellLeave
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellLeaveEvent, value);
			}
		}

		/// <summary>Occurs whenever the user clicks anywhere on a cell with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06000E6C RID: 3692 RVA: 0x00039548 File Offset: 0x00037748
		// (remove) Token: 0x06000E6D RID: 3693 RVA: 0x0003955C File Offset: 0x0003775C
		public event DataGridViewCellMouseEventHandler CellMouseClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseClickEvent, value);
			}
		}

		/// <summary>Occurs when a cell within the <see cref="T:System.Windows.Forms.DataGridView" /> is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06000E6E RID: 3694 RVA: 0x00039570 File Offset: 0x00037770
		// (remove) Token: 0x06000E6F RID: 3695 RVA: 0x00039584 File Offset: 0x00037784
		public event DataGridViewCellMouseEventHandler CellMouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the user presses a mouse button while the mouse pointer is within the boundaries of a cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06000E70 RID: 3696 RVA: 0x00039598 File Offset: 0x00037798
		// (remove) Token: 0x06000E71 RID: 3697 RVA: 0x000395AC File Offset: 0x000377AC
		public event DataGridViewCellMouseEventHandler CellMouseDown
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseDownEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters a cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06000E72 RID: 3698 RVA: 0x000395C0 File Offset: 0x000377C0
		// (remove) Token: 0x06000E73 RID: 3699 RVA: 0x000395D4 File Offset: 0x000377D4
		public event DataGridViewCellEventHandler CellMouseEnter
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseEnterEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves a cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010D RID: 269
		// (add) Token: 0x06000E74 RID: 3700 RVA: 0x000395E8 File Offset: 0x000377E8
		// (remove) Token: 0x06000E75 RID: 3701 RVA: 0x000395FC File Offset: 0x000377FC
		public event DataGridViewCellEventHandler CellMouseLeave
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer moves over the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010E RID: 270
		// (add) Token: 0x06000E76 RID: 3702 RVA: 0x00039610 File Offset: 0x00037810
		// (remove) Token: 0x06000E77 RID: 3703 RVA: 0x00039624 File Offset: 0x00037824
		public event DataGridViewCellMouseEventHandler CellMouseMove
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseMoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseMoveEvent, value);
			}
		}

		/// <summary>Occurs when the user releases a mouse button while over a cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400010F RID: 271
		// (add) Token: 0x06000E78 RID: 3704 RVA: 0x00039638 File Offset: 0x00037838
		// (remove) Token: 0x06000E79 RID: 3705 RVA: 0x0003964C File Offset: 0x0003784C
		public event DataGridViewCellMouseEventHandler CellMouseUp
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellMouseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellMouseUpEvent, value);
			}
		}

		/// <summary>Occurs when a cell needs to be drawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000110 RID: 272
		// (add) Token: 0x06000E7A RID: 3706 RVA: 0x00039660 File Offset: 0x00037860
		// (remove) Token: 0x06000E7B RID: 3707 RVA: 0x00039674 File Offset: 0x00037874
		public event DataGridViewCellPaintingEventHandler CellPainting
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellPaintingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellPaintingEvent, value);
			}
		}

		/// <summary>Occurs when a cell leaves edit mode if the cell value has been modified.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000111 RID: 273
		// (add) Token: 0x06000E7C RID: 3708 RVA: 0x00039688 File Offset: 0x00037888
		// (remove) Token: 0x06000E7D RID: 3709 RVA: 0x0003969C File Offset: 0x0003789C
		public event DataGridViewCellParsingEventHandler CellParsing
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellParsingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellParsingEvent, value);
			}
		}

		/// <summary>Occurs when a cell state changes, such as when the cell loses or gains focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000112 RID: 274
		// (add) Token: 0x06000E7E RID: 3710 RVA: 0x000396B0 File Offset: 0x000378B0
		// (remove) Token: 0x06000E7F RID: 3711 RVA: 0x000396C4 File Offset: 0x000378C4
		public event DataGridViewCellStateChangedEventHandler CellStateChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellStateChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000113 RID: 275
		// (add) Token: 0x06000E80 RID: 3712 RVA: 0x000396D8 File Offset: 0x000378D8
		// (remove) Token: 0x06000E81 RID: 3713 RVA: 0x000396EC File Offset: 0x000378EC
		public event DataGridViewCellEventHandler CellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when one of the values of a cell style changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000114 RID: 276
		// (add) Token: 0x06000E82 RID: 3714 RVA: 0x00039700 File Offset: 0x00037900
		// (remove) Token: 0x06000E83 RID: 3715 RVA: 0x00039714 File Offset: 0x00037914
		public event DataGridViewCellStyleContentChangedEventHandler CellStyleContentChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellStyleContentChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellStyleContentChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewCell.ToolTipText" /> property value changes for a cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000115 RID: 277
		// (add) Token: 0x06000E84 RID: 3716 RVA: 0x00039728 File Offset: 0x00037928
		// (remove) Token: 0x06000E85 RID: 3717 RVA: 0x0003973C File Offset: 0x0003793C
		public event DataGridViewCellEventHandler CellToolTipTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellToolTipTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellToolTipTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when a cell's ToolTip text is needed.</summary>
		// Token: 0x14000116 RID: 278
		// (add) Token: 0x06000E86 RID: 3718 RVA: 0x00039750 File Offset: 0x00037950
		// (remove) Token: 0x06000E87 RID: 3719 RVA: 0x00039764 File Offset: 0x00037964
		[EditorBrowsable(2)]
		public event DataGridViewCellToolTipTextNeededEventHandler CellToolTipTextNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellToolTipTextNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellToolTipTextNeededEvent, value);
			}
		}

		/// <summary>Occurs after the cell has finished validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000117 RID: 279
		// (add) Token: 0x06000E88 RID: 3720 RVA: 0x00039778 File Offset: 0x00037978
		// (remove) Token: 0x06000E89 RID: 3721 RVA: 0x0003978C File Offset: 0x0003798C
		public event DataGridViewCellEventHandler CellValidated
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellValidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellValidatedEvent, value);
			}
		}

		/// <summary>Occurs when a cell loses input focus, enabling content validation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000118 RID: 280
		// (add) Token: 0x06000E8A RID: 3722 RVA: 0x000397A0 File Offset: 0x000379A0
		// (remove) Token: 0x06000E8B RID: 3723 RVA: 0x000397B4 File Offset: 0x000379B4
		public event DataGridViewCellValidatingEventHandler CellValidating
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellValidatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellValidatingEvent, value);
			}
		}

		/// <summary>Occurs when the value of a cell changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06000E8C RID: 3724 RVA: 0x000397C8 File Offset: 0x000379C8
		// (remove) Token: 0x06000E8D RID: 3725 RVA: 0x000397DC File Offset: 0x000379DC
		public event DataGridViewCellEventHandler CellValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> control is true and the <see cref="T:System.Windows.Forms.DataGridView" /> requires a value for a cell in order to format and display the cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400011A RID: 282
		// (add) Token: 0x06000E8E RID: 3726 RVA: 0x000397F0 File Offset: 0x000379F0
		// (remove) Token: 0x06000E8F RID: 3727 RVA: 0x00039804 File Offset: 0x00037A04
		[EditorBrowsable(2)]
		public event DataGridViewCellValueEventHandler CellValueNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellValueNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellValueNeededEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> control is true and a cell value has changed and requires storage in the underlying data source.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400011B RID: 283
		// (add) Token: 0x06000E90 RID: 3728 RVA: 0x00039818 File Offset: 0x00037A18
		// (remove) Token: 0x06000E91 RID: 3729 RVA: 0x0003982C File Offset: 0x00037A2C
		[EditorBrowsable(2)]
		public event DataGridViewCellValueEventHandler CellValuePushed
		{
			add
			{
				base.Events.AddHandler(DataGridView.CellValuePushedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CellValuePushedEvent, value);
			}
		}

		/// <summary>Occurs when a column is added to the control.</summary>
		// Token: 0x1400011C RID: 284
		// (add) Token: 0x06000E92 RID: 3730 RVA: 0x00039840 File Offset: 0x00037A40
		// (remove) Token: 0x06000E93 RID: 3731 RVA: 0x00039854 File Offset: 0x00037A54
		public event DataGridViewColumnEventHandler ColumnAdded
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnAddedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewColumn.ContextMenuStrip" /> property of a column changes.</summary>
		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06000E94 RID: 3732 RVA: 0x00039868 File Offset: 0x00037A68
		// (remove) Token: 0x06000E95 RID: 3733 RVA: 0x0003987C File Offset: 0x00037A7C
		public event DataGridViewColumnEventHandler ColumnContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnContextMenuStripChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnContextMenuStripChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.DataPropertyName" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400011E RID: 286
		// (add) Token: 0x06000E96 RID: 3734 RVA: 0x00039890 File Offset: 0x00037A90
		// (remove) Token: 0x06000E97 RID: 3735 RVA: 0x000398A4 File Offset: 0x00037AA4
		public event DataGridViewColumnEventHandler ColumnDataPropertyNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnDataPropertyNameChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnDataPropertyNameChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400011F RID: 287
		// (add) Token: 0x06000E98 RID: 3736 RVA: 0x000398B8 File Offset: 0x00037AB8
		// (remove) Token: 0x06000E99 RID: 3737 RVA: 0x000398CC File Offset: 0x00037ACC
		public event DataGridViewColumnEventHandler ColumnDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value the <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000120 RID: 288
		// (add) Token: 0x06000E9A RID: 3738 RVA: 0x000398E0 File Offset: 0x00037AE0
		// (remove) Token: 0x06000E9B RID: 3739 RVA: 0x000398F4 File Offset: 0x00037AF4
		public event DataGridViewColumnEventHandler ColumnDisplayIndexChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnDisplayIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnDisplayIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks a divider between two columns.</summary>
		// Token: 0x14000121 RID: 289
		// (add) Token: 0x06000E9C RID: 3740 RVA: 0x00039908 File Offset: 0x00037B08
		// (remove) Token: 0x06000E9D RID: 3741 RVA: 0x0003991C File Offset: 0x00037B1C
		public event DataGridViewColumnDividerDoubleClickEventHandler ColumnDividerDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnDividerDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnDividerDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewColumn.DividerWidth" /> property changes.</summary>
		// Token: 0x14000122 RID: 290
		// (add) Token: 0x06000E9E RID: 3742 RVA: 0x00039930 File Offset: 0x00037B30
		// (remove) Token: 0x06000E9F RID: 3743 RVA: 0x00039944 File Offset: 0x00037B44
		public event DataGridViewColumnEventHandler ColumnDividerWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnDividerWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnDividerWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the contents of a column header cell change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000123 RID: 291
		// (add) Token: 0x06000EA0 RID: 3744 RVA: 0x00039958 File Offset: 0x00037B58
		// (remove) Token: 0x06000EA1 RID: 3745 RVA: 0x0003996C File Offset: 0x00037B6C
		public event DataGridViewColumnEventHandler ColumnHeaderCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeaderCellChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeaderCellChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks a column header.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000124 RID: 292
		// (add) Token: 0x06000EA2 RID: 3746 RVA: 0x00039980 File Offset: 0x00037B80
		// (remove) Token: 0x06000EA3 RID: 3747 RVA: 0x00039994 File Offset: 0x00037B94
		public event DataGridViewCellMouseEventHandler ColumnHeaderMouseClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeaderMouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeaderMouseClickEvent, value);
			}
		}

		/// <summary>Occurs when a column header is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000125 RID: 293
		// (add) Token: 0x06000EA4 RID: 3748 RVA: 0x000399A8 File Offset: 0x00037BA8
		// (remove) Token: 0x06000EA5 RID: 3749 RVA: 0x000399BC File Offset: 0x00037BBC
		public event DataGridViewCellMouseEventHandler ColumnHeaderMouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeaderMouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeaderMouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersBorderStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000126 RID: 294
		// (add) Token: 0x06000EA6 RID: 3750 RVA: 0x000399D0 File Offset: 0x00037BD0
		// (remove) Token: 0x06000EA7 RID: 3751 RVA: 0x000399E4 File Offset: 0x00037BE4
		public event EventHandler ColumnHeadersBorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeadersBorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeadersBorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000127 RID: 295
		// (add) Token: 0x06000EA8 RID: 3752 RVA: 0x000399F8 File Offset: 0x00037BF8
		// (remove) Token: 0x06000EA9 RID: 3753 RVA: 0x00039A0C File Offset: 0x00037C0C
		public event EventHandler ColumnHeadersDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeadersDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeadersDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersHeight" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000128 RID: 296
		// (add) Token: 0x06000EAA RID: 3754 RVA: 0x00039A20 File Offset: 0x00037C20
		// (remove) Token: 0x06000EAB RID: 3755 RVA: 0x00039A34 File Offset: 0x00037C34
		public event EventHandler ColumnHeadersHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeadersHeightChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeadersHeightChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersHeightSizeMode" /> property changes.</summary>
		// Token: 0x14000129 RID: 297
		// (add) Token: 0x06000EAC RID: 3756 RVA: 0x00039A48 File Offset: 0x00037C48
		// (remove) Token: 0x06000EAD RID: 3757 RVA: 0x00039A5C File Offset: 0x00037C5C
		public event DataGridViewAutoSizeModeEventHandler ColumnHeadersHeightSizeModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnHeadersHeightSizeModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnHeadersHeightSizeModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.MinimumWidth" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400012A RID: 298
		// (add) Token: 0x06000EAE RID: 3758 RVA: 0x00039A70 File Offset: 0x00037C70
		// (remove) Token: 0x06000EAF RID: 3759 RVA: 0x00039A84 File Offset: 0x00037C84
		public event DataGridViewColumnEventHandler ColumnMinimumWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnMinimumWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnMinimumWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.Name" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400012B RID: 299
		// (add) Token: 0x06000EB0 RID: 3760 RVA: 0x00039A98 File Offset: 0x00037C98
		// (remove) Token: 0x06000EB1 RID: 3761 RVA: 0x00039AAC File Offset: 0x00037CAC
		public event DataGridViewColumnEventHandler ColumnNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnNameChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnNameChangedEvent, value);
			}
		}

		/// <summary>Occurs when a column is removed from the control.</summary>
		// Token: 0x1400012C RID: 300
		// (add) Token: 0x06000EB2 RID: 3762 RVA: 0x00039AC0 File Offset: 0x00037CC0
		// (remove) Token: 0x06000EB3 RID: 3763 RVA: 0x00039AD4 File Offset: 0x00037CD4
		public event DataGridViewColumnEventHandler ColumnRemoved
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnRemovedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06000EB4 RID: 3764 RVA: 0x00039AE8 File Offset: 0x00037CE8
		// (remove) Token: 0x06000EB5 RID: 3765 RVA: 0x00039AFC File Offset: 0x00037CFC
		public event DataGridViewColumnEventHandler ColumnSortModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnSortModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnSortModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when a column changes state, such as gaining or losing focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06000EB6 RID: 3766 RVA: 0x00039B10 File Offset: 0x00037D10
		// (remove) Token: 0x06000EB7 RID: 3767 RVA: 0x00039B24 File Offset: 0x00037D24
		public event DataGridViewColumnStateChangedEventHandler ColumnStateChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnStateChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewColumn.ToolTipText" /> property value changes for a column in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06000EB8 RID: 3768 RVA: 0x00039B38 File Offset: 0x00037D38
		// (remove) Token: 0x06000EB9 RID: 3769 RVA: 0x00039B4C File Offset: 0x00037D4C
		public event DataGridViewColumnEventHandler ColumnToolTipTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnToolTipTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnToolTipTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.Width" /> property for a column changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000130 RID: 304
		// (add) Token: 0x06000EBA RID: 3770 RVA: 0x00039B60 File Offset: 0x00037D60
		// (remove) Token: 0x06000EBB RID: 3771 RVA: 0x00039B74 File Offset: 0x00037D74
		public event DataGridViewColumnEventHandler ColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ColumnWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ColumnWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.CurrentCell" /> property changes.</summary>
		// Token: 0x14000131 RID: 305
		// (add) Token: 0x06000EBC RID: 3772 RVA: 0x00039B88 File Offset: 0x00037D88
		// (remove) Token: 0x06000EBD RID: 3773 RVA: 0x00039B9C File Offset: 0x00037D9C
		public event EventHandler CurrentCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CurrentCellChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CurrentCellChangedEvent, value);
			}
		}

		/// <summary>Occurs when the state of a cell changes in relation to a change in its contents.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000132 RID: 306
		// (add) Token: 0x06000EBE RID: 3774 RVA: 0x00039BB0 File Offset: 0x00037DB0
		// (remove) Token: 0x06000EBF RID: 3775 RVA: 0x00039BC4 File Offset: 0x00037DC4
		[EditorBrowsable(2)]
		public event EventHandler CurrentCellDirtyStateChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.CurrentCellDirtyStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.CurrentCellDirtyStateChangedEvent, value);
			}
		}

		/// <summary>Occurs after a data-binding operation has finished.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000133 RID: 307
		// (add) Token: 0x06000EC0 RID: 3776 RVA: 0x00039BD8 File Offset: 0x00037DD8
		// (remove) Token: 0x06000EC1 RID: 3777 RVA: 0x00039BEC File Offset: 0x00037DEC
		public event DataGridViewBindingCompleteEventHandler DataBindingComplete
		{
			add
			{
				base.Events.AddHandler(DataGridView.DataBindingCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DataBindingCompleteEvent, value);
			}
		}

		/// <summary>Occurs when an external data-parsing or validation operation throws an exception, or when an attempt to commit data to a data source fails.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000134 RID: 308
		// (add) Token: 0x06000EC2 RID: 3778 RVA: 0x00039C00 File Offset: 0x00037E00
		// (remove) Token: 0x06000EC3 RID: 3779 RVA: 0x00039C14 File Offset: 0x00037E14
		public event DataGridViewDataErrorEventHandler DataError
		{
			add
			{
				base.Events.AddHandler(DataGridView.DataErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DataErrorEvent, value);
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.DataGridView.DataMember" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000135 RID: 309
		// (add) Token: 0x06000EC4 RID: 3780 RVA: 0x00039C28 File Offset: 0x00037E28
		// (remove) Token: 0x06000EC5 RID: 3781 RVA: 0x00039C3C File Offset: 0x00037E3C
		public event EventHandler DataMemberChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.DataMemberChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DataMemberChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000136 RID: 310
		// (add) Token: 0x06000EC6 RID: 3782 RVA: 0x00039C50 File Offset: 0x00037E50
		// (remove) Token: 0x06000EC7 RID: 3783 RVA: 0x00039C64 File Offset: 0x00037E64
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.DataSourceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DataSourceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.DefaultCellStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000137 RID: 311
		// (add) Token: 0x06000EC8 RID: 3784 RVA: 0x00039C78 File Offset: 0x00037E78
		// (remove) Token: 0x06000EC9 RID: 3785 RVA: 0x00039C8C File Offset: 0x00037E8C
		public event EventHandler DefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.DefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user enters the row for new records so that it can be populated with default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000138 RID: 312
		// (add) Token: 0x06000ECA RID: 3786 RVA: 0x00039CA0 File Offset: 0x00037EA0
		// (remove) Token: 0x06000ECB RID: 3787 RVA: 0x00039CB4 File Offset: 0x00037EB4
		[EditorBrowsable(2)]
		public event DataGridViewRowEventHandler DefaultValuesNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.DefaultValuesNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.DefaultValuesNeededEvent, value);
			}
		}

		/// <summary>Occurs when a control for editing a cell is showing.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000139 RID: 313
		// (add) Token: 0x06000ECC RID: 3788 RVA: 0x00039CC8 File Offset: 0x00037EC8
		// (remove) Token: 0x06000ECD RID: 3789 RVA: 0x00039CDC File Offset: 0x00037EDC
		public event DataGridViewEditingControlShowingEventHandler EditingControlShowing
		{
			add
			{
				base.Events.AddHandler(DataGridView.EditingControlShowingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.EditingControlShowingEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.EditMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400013A RID: 314
		// (add) Token: 0x06000ECE RID: 3790 RVA: 0x00039CF0 File Offset: 0x00037EF0
		// (remove) Token: 0x06000ECF RID: 3791 RVA: 0x00039D04 File Offset: 0x00037F04
		public event EventHandler EditModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.EditModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.EditModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.Font" /> property value changes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400013B RID: 315
		// (add) Token: 0x06000ED0 RID: 3792 RVA: 0x00039D18 File Offset: 0x00037F18
		// (remove) Token: 0x06000ED1 RID: 3793 RVA: 0x00039D24 File Offset: 0x00037F24
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.ForeColor" /> property value changes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400013C RID: 316
		// (add) Token: 0x06000ED2 RID: 3794 RVA: 0x00039D30 File Offset: 0x00037F30
		// (remove) Token: 0x06000ED3 RID: 3795 RVA: 0x00039D3C File Offset: 0x00037F3C
		[EditorBrowsable(2)]
		[Browsable(false)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.Padding" /> property changes.</summary>
		// Token: 0x1400013D RID: 317
		// (add) Token: 0x06000ED4 RID: 3796 RVA: 0x00039D48 File Offset: 0x00037F48
		// (remove) Token: 0x06000ED5 RID: 3797 RVA: 0x00039D54 File Offset: 0x00037F54
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.GridColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400013E RID: 318
		// (add) Token: 0x06000ED6 RID: 3798 RVA: 0x00039D60 File Offset: 0x00037F60
		// (remove) Token: 0x06000ED7 RID: 3799 RVA: 0x00039D74 File Offset: 0x00037F74
		public event EventHandler GridColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.GridColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.GridColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.MultiSelect" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400013F RID: 319
		// (add) Token: 0x06000ED8 RID: 3800 RVA: 0x00039D88 File Offset: 0x00037F88
		// (remove) Token: 0x06000ED9 RID: 3801 RVA: 0x00039D9C File Offset: 0x00037F9C
		public event EventHandler MultiSelectChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.MultiSelectChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.MultiSelectChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is true and the user navigates to the new row at the bottom of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000140 RID: 320
		// (add) Token: 0x06000EDA RID: 3802 RVA: 0x00039DB0 File Offset: 0x00037FB0
		// (remove) Token: 0x06000EDB RID: 3803 RVA: 0x00039DC4 File Offset: 0x00037FC4
		public event DataGridViewRowEventHandler NewRowNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.NewRowNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.NewRowNeededEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.ReadOnly" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000141 RID: 321
		// (add) Token: 0x06000EDC RID: 3804 RVA: 0x00039DD8 File Offset: 0x00037FD8
		// (remove) Token: 0x06000EDD RID: 3805 RVA: 0x00039DEC File Offset: 0x00037FEC
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ReadOnlyChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewRow.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x14000142 RID: 322
		// (add) Token: 0x06000EDE RID: 3806 RVA: 0x00039E00 File Offset: 0x00038000
		// (remove) Token: 0x06000EDF RID: 3807 RVA: 0x00039E14 File Offset: 0x00038014
		public event DataGridViewRowEventHandler RowContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowContextMenuStripChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowContextMenuStripChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row's shortcut menu is needed.</summary>
		// Token: 0x14000143 RID: 323
		// (add) Token: 0x06000EE0 RID: 3808 RVA: 0x00039E28 File Offset: 0x00038028
		// (remove) Token: 0x06000EE1 RID: 3809 RVA: 0x00039E3C File Offset: 0x0003803C
		[EditorBrowsable(2)]
		public event DataGridViewRowContextMenuStripNeededEventHandler RowContextMenuStripNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowContextMenuStripNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowContextMenuStripNeededEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property for a row changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000144 RID: 324
		// (add) Token: 0x06000EE2 RID: 3810 RVA: 0x00039E50 File Offset: 0x00038050
		// (remove) Token: 0x06000EE3 RID: 3811 RVA: 0x00039E64 File Offset: 0x00038064
		public event DataGridViewRowEventHandler RowDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> control is true and the <see cref="T:System.Windows.Forms.DataGridView" /> needs to determine whether the current row has uncommitted changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000145 RID: 325
		// (add) Token: 0x06000EE4 RID: 3812 RVA: 0x00039E78 File Offset: 0x00038078
		// (remove) Token: 0x06000EE5 RID: 3813 RVA: 0x00039E8C File Offset: 0x0003808C
		[EditorBrowsable(2)]
		public event QuestionEventHandler RowDirtyStateNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowDirtyStateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowDirtyStateNeededEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the divider between two rows.</summary>
		// Token: 0x14000146 RID: 326
		// (add) Token: 0x06000EE6 RID: 3814 RVA: 0x00039EA0 File Offset: 0x000380A0
		// (remove) Token: 0x06000EE7 RID: 3815 RVA: 0x00039EB4 File Offset: 0x000380B4
		public event DataGridViewRowDividerDoubleClickEventHandler RowDividerDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowDividerDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowDividerDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewRow.DividerHeight" /> property changes. </summary>
		// Token: 0x14000147 RID: 327
		// (add) Token: 0x06000EE8 RID: 3816 RVA: 0x00039EC8 File Offset: 0x000380C8
		// (remove) Token: 0x06000EE9 RID: 3817 RVA: 0x00039EDC File Offset: 0x000380DC
		public event DataGridViewRowEventHandler RowDividerHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowDividerHeightChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowDividerHeightChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row receives input focus but before it becomes the current row.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000148 RID: 328
		// (add) Token: 0x06000EEA RID: 3818 RVA: 0x00039EF0 File Offset: 0x000380F0
		// (remove) Token: 0x06000EEB RID: 3819 RVA: 0x00039F04 File Offset: 0x00038104
		public event DataGridViewCellEventHandler RowEnter
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowEnterEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridViewRow.ErrorText" /> property of a row changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000149 RID: 329
		// (add) Token: 0x06000EEC RID: 3820 RVA: 0x00039F18 File Offset: 0x00038118
		// (remove) Token: 0x06000EED RID: 3821 RVA: 0x00039F2C File Offset: 0x0003812C
		public event DataGridViewRowEventHandler RowErrorTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowErrorTextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowErrorTextChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row's error text is needed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014A RID: 330
		// (add) Token: 0x06000EEE RID: 3822 RVA: 0x00039F40 File Offset: 0x00038140
		// (remove) Token: 0x06000EEF RID: 3823 RVA: 0x00039F54 File Offset: 0x00038154
		[EditorBrowsable(2)]
		public event DataGridViewRowErrorTextNeededEventHandler RowErrorTextNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowErrorTextNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowErrorTextNeededEvent, value);
			}
		}

		/// <summary>Occurs when the user changes the contents of a row header cell.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014B RID: 331
		// (add) Token: 0x06000EF0 RID: 3824 RVA: 0x00039F68 File Offset: 0x00038168
		// (remove) Token: 0x06000EF1 RID: 3825 RVA: 0x00039F7C File Offset: 0x0003817C
		public event DataGridViewRowEventHandler RowHeaderCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeaderCellChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeaderCellChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks within the boundaries of a row header.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014C RID: 332
		// (add) Token: 0x06000EF2 RID: 3826 RVA: 0x00039F90 File Offset: 0x00038190
		// (remove) Token: 0x06000EF3 RID: 3827 RVA: 0x00039FA4 File Offset: 0x000381A4
		public event DataGridViewCellMouseEventHandler RowHeaderMouseClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeaderMouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeaderMouseClickEvent, value);
			}
		}

		/// <summary>Occurs when a row header is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014D RID: 333
		// (add) Token: 0x06000EF4 RID: 3828 RVA: 0x00039FB8 File Offset: 0x000381B8
		// (remove) Token: 0x06000EF5 RID: 3829 RVA: 0x00039FCC File Offset: 0x000381CC
		public event DataGridViewCellMouseEventHandler RowHeaderMouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeaderMouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeaderMouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersBorderStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014E RID: 334
		// (add) Token: 0x06000EF6 RID: 3830 RVA: 0x00039FE0 File Offset: 0x000381E0
		// (remove) Token: 0x06000EF7 RID: 3831 RVA: 0x00039FF4 File Offset: 0x000381F4
		public event EventHandler RowHeadersBorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeadersBorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeadersBorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400014F RID: 335
		// (add) Token: 0x06000EF8 RID: 3832 RVA: 0x0003A008 File Offset: 0x00038208
		// (remove) Token: 0x06000EF9 RID: 3833 RVA: 0x0003A01C File Offset: 0x0003821C
		public event EventHandler RowHeadersDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeadersDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeadersDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidth" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000150 RID: 336
		// (add) Token: 0x06000EFA RID: 3834 RVA: 0x0003A030 File Offset: 0x00038230
		// (remove) Token: 0x06000EFB RID: 3835 RVA: 0x0003A044 File Offset: 0x00038244
		public event EventHandler RowHeadersWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeadersWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeadersWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property changes.</summary>
		// Token: 0x14000151 RID: 337
		// (add) Token: 0x06000EFC RID: 3836 RVA: 0x0003A058 File Offset: 0x00038258
		// (remove) Token: 0x06000EFD RID: 3837 RVA: 0x0003A06C File Offset: 0x0003826C
		public event DataGridViewAutoSizeModeEventHandler RowHeadersWidthSizeModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeadersWidthSizeModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeadersWidthSizeModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewRow.Height" /> property for a row changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000152 RID: 338
		// (add) Token: 0x06000EFE RID: 3838 RVA: 0x0003A080 File Offset: 0x00038280
		// (remove) Token: 0x06000EFF RID: 3839 RVA: 0x0003A094 File Offset: 0x00038294
		public event DataGridViewRowEventHandler RowHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeightChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeightChangedEvent, value);
			}
		}

		/// <summary>Occurs when information about row height is requested. </summary>
		// Token: 0x14000153 RID: 339
		// (add) Token: 0x06000F00 RID: 3840 RVA: 0x0003A0A8 File Offset: 0x000382A8
		// (remove) Token: 0x06000F01 RID: 3841 RVA: 0x0003A0BC File Offset: 0x000382BC
		[EditorBrowsable(2)]
		public event DataGridViewRowHeightInfoNeededEventHandler RowHeightInfoNeeded
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeightInfoNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeightInfoNeededEvent, value);
			}
		}

		/// <summary>Occurs when the user changes the height of a row.</summary>
		// Token: 0x14000154 RID: 340
		// (add) Token: 0x06000F02 RID: 3842 RVA: 0x0003A0D0 File Offset: 0x000382D0
		// (remove) Token: 0x06000F03 RID: 3843 RVA: 0x0003A0E4 File Offset: 0x000382E4
		[EditorBrowsable(2)]
		public event DataGridViewRowHeightInfoPushedEventHandler RowHeightInfoPushed
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowHeightInfoPushedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowHeightInfoPushedEvent, value);
			}
		}

		/// <summary>Occurs when a row loses input focus and is no longer the current row.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000155 RID: 341
		// (add) Token: 0x06000F04 RID: 3844 RVA: 0x0003A0F8 File Offset: 0x000382F8
		// (remove) Token: 0x06000F05 RID: 3845 RVA: 0x0003A10C File Offset: 0x0003830C
		public event DataGridViewCellEventHandler RowLeave
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridViewRow.MinimumHeight" /> property for a row changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000156 RID: 342
		// (add) Token: 0x06000F06 RID: 3846 RVA: 0x0003A120 File Offset: 0x00038320
		// (remove) Token: 0x06000F07 RID: 3847 RVA: 0x0003A134 File Offset: 0x00038334
		public event DataGridViewRowEventHandler RowMinimumHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowMinimumHeightChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowMinimumHeightChangedEvent, value);
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Windows.Forms.DataGridViewRow" /> is painted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000157 RID: 343
		// (add) Token: 0x06000F08 RID: 3848 RVA: 0x0003A148 File Offset: 0x00038348
		// (remove) Token: 0x06000F09 RID: 3849 RVA: 0x0003A15C File Offset: 0x0003835C
		public event DataGridViewRowPostPaintEventHandler RowPostPaint
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowPostPaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowPostPaintEvent, value);
			}
		}

		/// <summary>Occurs before a <see cref="T:System.Windows.Forms.DataGridViewRow" /> is painted</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000158 RID: 344
		// (add) Token: 0x06000F0A RID: 3850 RVA: 0x0003A170 File Offset: 0x00038370
		// (remove) Token: 0x06000F0B RID: 3851 RVA: 0x0003A184 File Offset: 0x00038384
		public event DataGridViewRowPrePaintEventHandler RowPrePaint
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowPrePaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowPrePaintEvent, value);
			}
		}

		/// <summary>Occurs after a new row is added to the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x14000159 RID: 345
		// (add) Token: 0x06000F0C RID: 3852 RVA: 0x0003A198 File Offset: 0x00038398
		// (remove) Token: 0x06000F0D RID: 3853 RVA: 0x0003A1AC File Offset: 0x000383AC
		public event DataGridViewRowsAddedEventHandler RowsAdded
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowsAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowsAddedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.RowsDefaultCellStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400015A RID: 346
		// (add) Token: 0x06000F0E RID: 3854 RVA: 0x0003A1C0 File Offset: 0x000383C0
		// (remove) Token: 0x06000F0F RID: 3855 RVA: 0x0003A1D4 File Offset: 0x000383D4
		public event EventHandler RowsDefaultCellStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowsDefaultCellStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowsDefaultCellStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row or rows are deleted from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x1400015B RID: 347
		// (add) Token: 0x06000F10 RID: 3856 RVA: 0x0003A1E8 File Offset: 0x000383E8
		// (remove) Token: 0x06000F11 RID: 3857 RVA: 0x0003A1FC File Offset: 0x000383FC
		public event DataGridViewRowsRemovedEventHandler RowsRemoved
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowsRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowsRemovedEvent, value);
			}
		}

		/// <summary>Occurs when a row changes state, such as losing or gaining input focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400015C RID: 348
		// (add) Token: 0x06000F12 RID: 3858 RVA: 0x0003A210 File Offset: 0x00038410
		// (remove) Token: 0x06000F13 RID: 3859 RVA: 0x0003A224 File Offset: 0x00038424
		public event DataGridViewRowStateChangedEventHandler RowStateChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowStateChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row's state changes from shared to unshared.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400015D RID: 349
		// (add) Token: 0x06000F14 RID: 3860 RVA: 0x0003A238 File Offset: 0x00038438
		// (remove) Token: 0x06000F15 RID: 3861 RVA: 0x0003A24C File Offset: 0x0003844C
		[EditorBrowsable(2)]
		public event DataGridViewRowEventHandler RowUnshared
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowUnsharedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowUnsharedEvent, value);
			}
		}

		/// <summary>Occurs after a row has finished validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400015E RID: 350
		// (add) Token: 0x06000F16 RID: 3862 RVA: 0x0003A260 File Offset: 0x00038460
		// (remove) Token: 0x06000F17 RID: 3863 RVA: 0x0003A274 File Offset: 0x00038474
		public event DataGridViewCellEventHandler RowValidated
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowValidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowValidatedEvent, value);
			}
		}

		/// <summary>Occurs when a row is validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400015F RID: 351
		// (add) Token: 0x06000F18 RID: 3864 RVA: 0x0003A288 File Offset: 0x00038488
		// (remove) Token: 0x06000F19 RID: 3865 RVA: 0x0003A29C File Offset: 0x0003849C
		public event DataGridViewCellCancelEventHandler RowValidating
		{
			add
			{
				base.Events.AddHandler(DataGridView.RowValidatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.RowValidatingEvent, value);
			}
		}

		/// <summary>Occurs when the user scrolls through the control contents.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000160 RID: 352
		// (add) Token: 0x06000F1A RID: 3866 RVA: 0x0003A2B0 File Offset: 0x000384B0
		// (remove) Token: 0x06000F1B RID: 3867 RVA: 0x0003A2C4 File Offset: 0x000384C4
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(DataGridView.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the current selection changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000161 RID: 353
		// (add) Token: 0x06000F1C RID: 3868 RVA: 0x0003A2D8 File Offset: 0x000384D8
		// (remove) Token: 0x06000F1D RID: 3869 RVA: 0x0003A2EC File Offset: 0x000384EC
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(DataGridView.SelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.SelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.DataGridView" /> compares two cell values to perform a sort operation.</summary>
		// Token: 0x14000162 RID: 354
		// (add) Token: 0x06000F1E RID: 3870 RVA: 0x0003A300 File Offset: 0x00038500
		// (remove) Token: 0x06000F1F RID: 3871 RVA: 0x0003A314 File Offset: 0x00038514
		[EditorBrowsable(2)]
		public event DataGridViewSortCompareEventHandler SortCompare
		{
			add
			{
				base.Events.AddHandler(DataGridView.SortCompareEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.SortCompareEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.DataGridView" /> control completes a sorting operation.</summary>
		// Token: 0x14000163 RID: 355
		// (add) Token: 0x06000F20 RID: 3872 RVA: 0x0003A328 File Offset: 0x00038528
		// (remove) Token: 0x06000F21 RID: 3873 RVA: 0x0003A33C File Offset: 0x0003853C
		public event EventHandler Sorted
		{
			add
			{
				base.Events.AddHandler(DataGridView.SortedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.SortedEvent, value);
			}
		}

		/// <summary>Occurs when the user has finished adding a row to the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000164 RID: 356
		// (add) Token: 0x06000F22 RID: 3874 RVA: 0x0003A350 File Offset: 0x00038550
		// (remove) Token: 0x06000F23 RID: 3875 RVA: 0x0003A364 File Offset: 0x00038564
		public event DataGridViewRowEventHandler UserAddedRow
		{
			add
			{
				base.Events.AddHandler(DataGridView.UserAddedRowEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.UserAddedRowEvent, value);
			}
		}

		/// <summary>Occurs when the user has finished deleting a row from the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000165 RID: 357
		// (add) Token: 0x06000F24 RID: 3876 RVA: 0x0003A378 File Offset: 0x00038578
		// (remove) Token: 0x06000F25 RID: 3877 RVA: 0x0003A38C File Offset: 0x0003858C
		public event DataGridViewRowEventHandler UserDeletedRow
		{
			add
			{
				base.Events.AddHandler(DataGridView.UserDeletedRowEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.UserDeletedRowEvent, value);
			}
		}

		/// <summary>Occurs when the user deletes a row from the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000166 RID: 358
		// (add) Token: 0x06000F26 RID: 3878 RVA: 0x0003A3A0 File Offset: 0x000385A0
		// (remove) Token: 0x06000F27 RID: 3879 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public event DataGridViewRowCancelEventHandler UserDeletingRow
		{
			add
			{
				base.Events.AddHandler(DataGridView.UserDeletingRowEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridView.UserDeletingRowEvent, value);
			}
		}

		/// <summary>Occurs when the control style changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000167 RID: 359
		// (add) Token: 0x06000F28 RID: 3880 RVA: 0x0003A3C8 File Offset: 0x000385C8
		// (remove) Token: 0x06000F29 RID: 3881 RVA: 0x0003A3D4 File Offset: 0x000385D4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler StyleChanged
		{
			add
			{
				base.StyleChanged += value;
			}
			remove
			{
				base.StyleChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGridView.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000168 RID: 360
		// (add) Token: 0x06000F2A RID: 3882 RVA: 0x0003A3E0 File Offset: 0x000385E0
		// (remove) Token: 0x06000F2B RID: 3883 RVA: 0x0003A3EC File Offset: 0x000385EC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ISupportInitialize.BeginInit" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method has already been called for this control.</exception>
		// Token: 0x06000F2C RID: 3884 RVA: 0x0003A3F8 File Offset: 0x000385F8
		void ISupportInitialize.BeginInit()
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ISupportInitialize.EndInit" />.</summary>
		// Token: 0x06000F2D RID: 3885 RVA: 0x0003A3FC File Offset: 0x000385FC
		void ISupportInitialize.EndInit()
		{
		}

		/// <summary>Gets the border style for the upper-left cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the style of the border of the upper-left cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0003A400 File Offset: 0x00038600
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public virtual DataGridViewAdvancedBorderStyle AdjustedTopLeftHeaderBorderStyle
		{
			get
			{
				return this.adjustedTopLeftHeaderBorderStyle;
			}
		}

		/// <summary>Gets the border style of the cells in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the cells in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0003A408 File Offset: 0x00038608
		[EditorBrowsable(2)]
		[Browsable(false)]
		public DataGridViewAdvancedBorderStyle AdvancedCellBorderStyle
		{
			get
			{
				return this.advancedCellBorderStyle;
			}
		}

		/// <summary>Gets the border style of the column header cells in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> objects in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0003A410 File Offset: 0x00038610
		[EditorBrowsable(2)]
		[Browsable(false)]
		public DataGridViewAdvancedBorderStyle AdvancedColumnHeadersBorderStyle
		{
			get
			{
				return this.advancedColumnHeadersBorderStyle;
			}
		}

		/// <summary>Gets the border style of the row header cells in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> objects in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0003A418 File Offset: 0x00038618
		[EditorBrowsable(2)]
		[Browsable(false)]
		public DataGridViewAdvancedBorderStyle AdvancedRowHeadersBorderStyle
		{
			get
			{
				return this.advancedRowHeadersBorderStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the option to add rows is displayed to the user.</summary>
		/// <returns>true if the add-row option is displayed to the user; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0003A420 File Offset: 0x00038620
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0003A458 File Offset: 0x00038658
		[DefaultValue(true)]
		public bool AllowUserToAddRows
		{
			get
			{
				if (this.allowUserToAddRows && this.DataManager != null)
				{
					return this.DataManager.AllowNew;
				}
				return this.allowUserToAddRows;
			}
			set
			{
				if (this.allowUserToAddRows != value)
				{
					this.allowUserToAddRows = value;
					if (!value)
					{
						if (this.new_row_editing)
						{
							this.CancelEdit();
						}
						this.RemoveEditingRow();
					}
					else
					{
						this.PrepareEditingRow(false, false);
					}
					this.OnAllowUserToAddRowsChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user is allowed to delete rows from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the user can delete rows; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003A4B4 File Offset: 0x000386B4
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0003A4EC File Offset: 0x000386EC
		[DefaultValue(true)]
		public bool AllowUserToDeleteRows
		{
			get
			{
				if (this.allowUserToDeleteRows && this.DataManager != null)
				{
					return this.DataManager.AllowRemove;
				}
				return this.allowUserToDeleteRows;
			}
			set
			{
				if (this.allowUserToDeleteRows != value)
				{
					this.allowUserToDeleteRows = value;
					this.OnAllowUserToDeleteRowsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether manual column repositioning is enabled.</summary>
		/// <returns>true if the user can change the column order; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0003A50C File Offset: 0x0003870C
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0003A514 File Offset: 0x00038714
		[DefaultValue(false)]
		public bool AllowUserToOrderColumns
		{
			get
			{
				return this.allowUserToOrderColumns;
			}
			set
			{
				if (this.allowUserToOrderColumns != value)
				{
					this.allowUserToOrderColumns = value;
					this.OnAllowUserToOrderColumnsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether users can resize columns.</summary>
		/// <returns>true if users can resize columns; otherwise, false. The default is true.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0003A534 File Offset: 0x00038734
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0003A53C File Offset: 0x0003873C
		[DefaultValue(true)]
		public bool AllowUserToResizeColumns
		{
			get
			{
				return this.allowUserToResizeColumns;
			}
			set
			{
				if (this.allowUserToResizeColumns != value)
				{
					this.allowUserToResizeColumns = value;
					this.OnAllowUserToResizeColumnsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether users can resize rows.</summary>
		/// <returns>true if all the rows are resizable; otherwise, false. The default is true.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003A55C File Offset: 0x0003875C
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0003A564 File Offset: 0x00038764
		[DefaultValue(true)]
		public bool AllowUserToResizeRows
		{
			get
			{
				return this.allowUserToResizeRows;
			}
			set
			{
				if (this.allowUserToResizeRows != value)
				{
					this.allowUserToResizeRows = value;
					this.OnAllowUserToResizeRowsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the default cell style applied to odd-numbered rows of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to apply to the odd-numbered rows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0003A584 File Offset: 0x00038784
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0003A58C File Offset: 0x0003878C
		public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
		{
			get
			{
				return this.alternatingRowsDefaultCellStyle;
			}
			set
			{
				if (this.alternatingRowsDefaultCellStyle != value)
				{
					this.alternatingRowsDefaultCellStyle = value;
					this.OnAlternatingRowsDefaultCellStyleChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether columns are created automatically when the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> or <see cref="P:System.Windows.Forms.DataGridView.DataMember" /> properties are set.</summary>
		/// <returns>true if the columns should be created automatically; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003A5C0 File Offset: 0x000387C0
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0003A5C8 File Offset: 0x000387C8
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool AutoGenerateColumns
		{
			get
			{
				return this.autoGenerateColumns;
			}
			set
			{
				if (this.autoGenerateColumns != value)
				{
					this.autoGenerateColumns = value;
					this.OnAutoGenerateColumnsChanged(EventArgs.Empty);
				}
			}
		}

		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0003A5E8 File Offset: 0x000387E8
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0003A5F0 File Offset: 0x000387F0
		public override bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (this.autoSize != value)
				{
					this.autoSize = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating how column widths are determined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> value. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None" />. </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader" />, column headers are hidden, and at least one visible column has an <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill" /> and at least one visible column with an <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" /> is frozen.</exception>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003A608 File Offset: 0x00038808
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0003A610 File Offset: 0x00038810
		[DefaultValue(DataGridViewAutoSizeColumnsMode.None)]
		public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
		{
			get
			{
				return this.autoSizeColumnsMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewAutoSizeColumnsMode), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewAutoSizeColumnsMode.");
				}
				if (value == DataGridViewAutoSizeColumnsMode.ColumnHeader && !this.columnHeadersVisible)
				{
					foreach (object obj in this.columns)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
						if (dataGridViewColumn.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
						{
							throw new InvalidOperationException("Cant set this property to ColumnHeader in this DataGridView.");
						}
					}
				}
				if (value == DataGridViewAutoSizeColumnsMode.Fill)
				{
					foreach (object obj2 in this.columns)
					{
						DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)obj2;
						if (dataGridViewColumn2.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet && dataGridViewColumn2.Frozen)
						{
							throw new InvalidOperationException("Cant set this property to Fill in this DataGridView.");
						}
					}
				}
				this.autoSizeColumnsMode = value;
				this.AutoResizeColumns(value);
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating how row heights are determined. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value indicating the sizing mode. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders" /> and row headers are hidden. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0003A760 File Offset: 0x00038960
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0003A768 File Offset: 0x00038968
		[DefaultValue(DataGridViewAutoSizeRowsMode.None)]
		public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
		{
			get
			{
				return this.autoSizeRowsMode;
			}
			set
			{
				if (this.autoSizeRowsMode != value)
				{
					if (!Enum.IsDefined(typeof(DataGridViewAutoSizeRowsMode), value))
					{
						throw new InvalidEnumArgumentException("Value is not valid DataGridViewRowsMode.");
					}
					if ((value == DataGridViewAutoSizeRowsMode.AllHeaders || value == DataGridViewAutoSizeRowsMode.DisplayedHeaders) && !this.rowHeadersVisible)
					{
						throw new InvalidOperationException("Cant set this property to AllHeaders or DisplayedHeaders in this DataGridView.");
					}
					this.autoSizeRowsMode = value;
					if (value == DataGridViewAutoSizeRowsMode.None)
					{
						foreach (object obj in this.Rows)
						{
							DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
							dataGridViewRow.ResetToExplicitHeight();
						}
					}
					else
					{
						this.AutoResizeRows(value);
					}
					this.OnAutoSizeRowsModeChanged(new DataGridViewAutoSizeModeEventArgs(false));
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0003A858 File Offset: 0x00038A58
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0003A860 File Offset: 0x00038A60
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public override Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				if (this.backColor != value)
				{
					this.backColor = value;
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.DataGridView" />. The default is <see cref="P:System.Drawing.SystemColors.AppWorkspace" />. </returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Drawing.Color.Empty" />. -or-The specified value when setting this property has a <see cref="P:System.Drawing.Color.A" /> property value that is less that 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003A888 File Offset: 0x00038A88
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0003A890 File Offset: 0x00038A90
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				if (this.backgroundColor != value)
				{
					if (value == Color.Empty)
					{
						throw new ArgumentException("Cant set an Empty color.");
					}
					this.backgroundColor = value;
					this.OnBackgroundColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0003A8DC File Offset: 0x00038ADC
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x0003A8E4 File Offset: 0x00038AE4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				return this.backgroundImage;
			}
			set
			{
				if (this.backgroundImage != value)
				{
					this.backgroundImage = value;
					this.OnBackgroundImageChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value indicating the background image layout. The default is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003A904 File Offset: 0x00038B04
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0003A90C File Offset: 0x00038B0C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets the border style for the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.FixedSingle" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003A918 File Offset: 0x00038B18
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0003A920 File Offset: 0x00038B20
		[DefaultValue(BorderStyle.FixedSingle)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!Enum.IsDefined(typeof(BorderStyle), value))
					{
						throw new InvalidEnumArgumentException("Invalid border style.");
					}
					this.borderStyle = value;
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003A970 File Offset: 0x00038B70
		internal int BorderWidth
		{
			get
			{
				switch (this.BorderStyle)
				{
				case BorderStyle.FixedSingle:
					return 1;
				case BorderStyle.Fixed3D:
					return 2;
				}
				return 0;
			}
		}

		/// <summary>Gets the cell border style for the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellBorderStyle" /> that represents the border style of the cells contained in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewCellBorderStyle" /> value.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewCellBorderStyle.Custom" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		[DefaultValue(DataGridViewCellBorderStyle.Single)]
		[Browsable(true)]
		public DataGridViewCellBorderStyle CellBorderStyle
		{
			get
			{
				return this.cellBorderStyle;
			}
			set
			{
				if (this.cellBorderStyle != value)
				{
					if (value == DataGridViewCellBorderStyle.Custom)
					{
						throw new ArgumentException("CellBorderStyle cannot be set to Custom.");
					}
					this.cellBorderStyle = value;
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
					switch (this.cellBorderStyle)
					{
					case DataGridViewCellBorderStyle.Single:
						dataGridViewAdvancedBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
						break;
					case DataGridViewCellBorderStyle.Raised:
					case DataGridViewCellBorderStyle.RaisedVertical:
						dataGridViewAdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Outset;
						break;
					case DataGridViewCellBorderStyle.Sunken:
						dataGridViewAdvancedBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Inset;
						break;
					case DataGridViewCellBorderStyle.None:
						dataGridViewAdvancedBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
						break;
					case DataGridViewCellBorderStyle.SingleVertical:
						dataGridViewAdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
						break;
					case DataGridViewCellBorderStyle.SunkenVertical:
						dataGridViewAdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Inset;
						break;
					case DataGridViewCellBorderStyle.SingleHorizontal:
					case DataGridViewCellBorderStyle.SunkenHorizontal:
						dataGridViewAdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
						break;
					case DataGridViewCellBorderStyle.RaisedHorizontal:
						dataGridViewAdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
						break;
					}
					this.advancedCellBorderStyle = dataGridViewAdvancedBorderStyle;
					this.OnCellBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether users can copy cell text values to the <see cref="T:System.Windows.Forms.Clipboard" /> and whether row and column header text is included.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewClipboardCopyMode" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithAutoHeaderText" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewClipboardCopyMode" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0003AB00 File Offset: 0x00038D00
		[DefaultValue(DataGridViewClipboardCopyMode.EnableWithAutoHeaderText)]
		[Browsable(true)]
		public DataGridViewClipboardCopyMode ClipboardCopyMode
		{
			get
			{
				return this.clipboardCopyMode;
			}
			set
			{
				this.clipboardCopyMode = value;
			}
		}

		/// <summary>Gets or sets the number of columns displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>The number of columns displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property has been set. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003AB0C File Offset: 0x00038D0C
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0003AB1C File Offset: 0x00038D1C
		[EditorBrowsable(2)]
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int ColumnCount
		{
			get
			{
				return this.columns.Count;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ColumnCount", "ColumnCount must be >= 0.");
				}
				if (this.dataSource != null)
				{
					throw new InvalidOperationException("Cant change column count if DataSource is set.");
				}
				if (value < this.columns.Count)
				{
					for (int i = this.columns.Count - 1; i >= value; i--)
					{
						this.columns.RemoveAt(i);
					}
				}
				else if (value > this.columns.Count)
				{
					for (int j = this.columns.Count; j < value; j++)
					{
						DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
						this.columns.Add(dataGridViewTextBoxColumn);
					}
				}
			}
		}

		/// <summary>Gets the border style applied to the column headers.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle" /> value.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003ABD8 File Offset: 0x00038DD8
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003ABE0 File Offset: 0x00038DE0
		[Browsable(true)]
		[DefaultValue(DataGridViewHeaderBorderStyle.Raised)]
		public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle
		{
			get
			{
				return this.columnHeadersBorderStyle;
			}
			set
			{
				if (this.columnHeadersBorderStyle != value)
				{
					this.columnHeadersBorderStyle = value;
					this.OnColumnHeadersBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the default column header style.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the default column header style.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003AC00 File Offset: 0x00038E00
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0003AC08 File Offset: 0x00038E08
		[AmbientValue(null)]
		public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
		{
			get
			{
				return this.columnHeadersDefaultCellStyle;
			}
			set
			{
				if (this.columnHeadersDefaultCellStyle != value)
				{
					this.columnHeadersDefaultCellStyle = value;
					this.OnColumnHeadersDefaultCellStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the height, in pixels, of the column headers row </summary>
		/// <returns>The height, in pixels, of the row that contains the column headers. The default is 23.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than the minimum height of 4 pixels or is greater than the maximum height of 32768 pixels.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003AC28 File Offset: 0x00038E28
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0003AC30 File Offset: 0x00038E30
		[Localizable(true)]
		public int ColumnHeadersHeight
		{
			get
			{
				return this.columnHeadersHeight;
			}
			set
			{
				if (this.columnHeadersHeight != value)
				{
					if (value < 4)
					{
						throw new ArgumentOutOfRangeException("ColumnHeadersHeight", "Column headers height cant be less than 4.");
					}
					if (value > 32768)
					{
						throw new ArgumentOutOfRangeException("ColumnHeadersHeight", "Column headers height cannot be more than 32768.");
					}
					this.columnHeadersHeight = value;
					this.OnColumnHeadersHeightChanged(EventArgs.Empty);
					if (this.columnHeadersVisible)
					{
						base.Invalidate();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the height of the column headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode" /> value indicating the mode by which the height of the column headers row can be adjusted. The default is <see cref="F:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode" /> value.</exception>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		[RefreshProperties(1)]
		[DefaultValue(DataGridViewColumnHeadersHeightSizeMode.EnableResizing)]
		public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
		{
			get
			{
				return this.columnHeadersHeightSizeMode;
			}
			set
			{
				if (this.columnHeadersHeightSizeMode != value)
				{
					if (!Enum.IsDefined(typeof(DataGridViewColumnHeadersHeightSizeMode), value))
					{
						throw new InvalidEnumArgumentException("Value is not a valid DataGridViewColumnHeadersHeightSizeMode.");
					}
					this.columnHeadersHeightSizeMode = value;
					this.OnColumnHeadersHeightSizeModeChanged(new DataGridViewAutoSizeModeEventArgs(false));
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the column header row is displayed.</summary>
		/// <returns>true if the column headers are displayed; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and one or more columns have an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003ACFC File Offset: 0x00038EFC
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0003AD04 File Offset: 0x00038F04
		[DefaultValue(true)]
		public bool ColumnHeadersVisible
		{
			get
			{
				return this.columnHeadersVisible;
			}
			set
			{
				if (this.columnHeadersVisible != value)
				{
					this.columnHeadersVisible = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets a collection that contains all the columns in the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> that contains all the columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003AD20 File Offset: 0x00038F20
		[MergableProperty(false)]
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.DataGridViewColumnCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public DataGridViewColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		/// <summary>Gets or sets the currently active cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that represents the current cell, or null if there is no current cell. The default is the first cell in the first column or null if there are no cells in the control.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of this property cannot be set because changes to the current cell cannot be committed or canceled.-or-The specified cell when setting this property is in a hidden row or column. </exception>
		/// <exception cref="T:System.ArgumentException">The specified cell when setting this property is not in the <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003AD28 File Offset: 0x00038F28
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0003AD30 File Offset: 0x00038F30
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public DataGridViewCell CurrentCell
		{
			get
			{
				return this.currentCell;
			}
			set
			{
				if (value == null)
				{
					this.MoveCurrentCell(-1, -1, true, false, false, true);
				}
				else
				{
					if (value.DataGridView != this)
					{
						throw new ArgumentException("The cell is not in this DataGridView.");
					}
					this.MoveCurrentCell(value.OwningColumn.Index, value.OwningRow.Index, true, false, false, true);
				}
			}
		}

		/// <summary>Gets the row and column indexes of the currently active cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the row and column indexes of the currently active cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003AD8C File Offset: 0x00038F8C
		[Browsable(false)]
		public Point CurrentCellAddress
		{
			get
			{
				return this.currentCellAddress;
			}
		}

		/// <summary>Gets the row containing the current cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that represents the row containing the current cell, or null if there is no current cell.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003AD94 File Offset: 0x00038F94
		[Browsable(false)]
		public DataGridViewRow CurrentRow
		{
			get
			{
				if (this.currentCell != null)
				{
					return this.currentCell.OwningRow;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the name of the list or table in the data source for which the <see cref="T:System.Windows.Forms.DataGridView" /> is displaying data.</summary>
		/// <returns>The name of the table or list in the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> for which the <see cref="T:System.Windows.Forms.DataGridView" /> is displaying data. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Exception">An error occurred in the data source and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003ADB0 File Offset: 0x00038FB0
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0003ADB8 File Offset: 0x00038FB8
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (this.dataMember != value)
				{
					this.dataMember = value;
					if (this.BindingContext != null)
					{
						this.ReBind();
					}
					this.OnDataMemberChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data source that the <see cref="T:System.Windows.Forms.DataGridView" /> is displaying data for.</summary>
		/// <returns>The object that contains data for the <see cref="T:System.Windows.Forms.DataGridView" /> to display.</returns>
		/// <exception cref="T:System.Exception">An error occurred in the data source and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003ADFC File Offset: 0x00038FFC
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0003AE04 File Offset: 0x00039004
		[AttributeProvider(typeof(IListSource))]
		[RefreshProperties(2)]
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource) && !(value is IBindingList) && !(value is IBindingListView))
				{
					throw new NotSupportedException("Type cannot be bound.");
				}
				this.ClearBinding();
				if (this.BindingContext != null)
				{
					this.dataSource = value;
					this.ReBind();
				}
				else
				{
					this.dataSource = value;
				}
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003AE84 File Offset: 0x00039084
		internal CurrencyManager DataManager
		{
			get
			{
				if (this.DataSource != null && this.BindingContext != null)
				{
					string empty = this.DataMember;
					if (empty == null)
					{
						empty = string.Empty;
					}
					return (CurrencyManager)this.BindingContext[this.DataSource, empty];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the default cell style to be applied to the cells in the <see cref="T:System.Windows.Forms.DataGridView" /> if no other cell style properties are set.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0003AED4 File Offset: 0x000390D4
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0003AEDC File Offset: 0x000390DC
		[AmbientValue(null)]
		public DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return this.defaultCellStyle;
			}
			set
			{
				if (this.defaultCellStyle != value)
				{
					this.defaultCellStyle = value;
					this.OnDefaultCellStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the rectangle that represents the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003AEFC File Offset: 0x000390FC
		public override Rectangle DisplayRectangle
		{
			get
			{
				return base.DisplayRectangle;
			}
		}

		/// <summary>Gets the control hosted by the current cell, if a cell with an editing control is in edit mode.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> hosted by the current cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003AF04 File Offset: 0x00039104
		[Browsable(false)]
		[EditorBrowsable(2)]
		public Control EditingControl
		{
			get
			{
				return this.editingControl;
			}
		}

		/// <summary>Gets the panel that contains the <see cref="P:System.Windows.Forms.DataGridView.EditingControl" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Panel" /> that contains the <see cref="P:System.Windows.Forms.DataGridView.EditingControl" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003AF0C File Offset: 0x0003910C
		[Browsable(false)]
		[EditorBrowsable(2)]
		public Panel EditingPanel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating how to begin editing a cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewEditMode" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewEditMode" /> value.</exception>
		/// <exception cref="T:System.Exception">The specified value when setting this property would cause the control to enter edit mode, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003AF14 File Offset: 0x00039114
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0003AF1C File Offset: 0x0003911C
		[DefaultValue(DataGridViewEditMode.EditOnKeystrokeOrF2)]
		public DataGridViewEditMode EditMode
		{
			get
			{
				return this.editMode;
			}
			set
			{
				if (this.editMode != value)
				{
					this.editMode = value;
					this.OnEditModeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether row and column headers use the visual styles of the user's current theme if visual styles are enabled for the application.</summary>
		/// <returns>true if visual styles are enabled for the headers; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003AF3C File Offset: 0x0003913C
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003AF44 File Offset: 0x00039144
		[DefaultValue(true)]
		public bool EnableHeadersVisualStyles
		{
			get
			{
				return this.enableHeadersVisualStyles;
			}
			set
			{
				this.enableHeadersVisualStyles = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003AF50 File Offset: 0x00039150
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x0003AF58 File Offset: 0x00039158
		internal DataGridViewHeaderCell EnteredHeaderCell
		{
			get
			{
				return this.entered_header_cell;
			}
			set
			{
				if (this.entered_header_cell == value)
				{
					return;
				}
				if (ThemeEngine.Current.DataGridViewHeaderCellHasHotStyle(this))
				{
					Region region = new Region();
					region.MakeEmpty();
					if (this.entered_header_cell != null)
					{
						region.Union(this.GetHeaderCellBounds(this.entered_header_cell));
					}
					this.entered_header_cell = value;
					if (this.entered_header_cell != null)
					{
						region.Union(this.GetHeaderCellBounds(this.entered_header_cell));
					}
					base.Invalidate(region);
					region.Dispose();
				}
				else
				{
					this.entered_header_cell = value;
				}
			}
		}

		/// <summary>Gets or sets the first cell currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; typically, this cell is in the upper left corner.</summary>
		/// <returns>The first <see cref="T:System.Windows.Forms.DataGridViewCell" /> currently displayed in the control.</returns>
		/// <exception cref="T:System.ArgumentException">The specified cell when setting this property is not is not in the <see cref="T:System.Windows.Forms.DataGridView" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property has a <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> or <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property value of -1, indicating that it is a header cell or a shared cell. -or-The specified cell when setting this property has a <see cref="P:System.Windows.Forms.DataGridViewCell.Visible" /> property value of false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0003AFE8 File Offset: 0x000391E8
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x0003AFF0 File Offset: 0x000391F0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewCell FirstDisplayedCell
		{
			get
			{
				return this.firstDisplayedCell;
			}
			set
			{
				if (value.DataGridView != this)
				{
					throw new ArgumentException("The cell is not in this DataGridView.");
				}
				this.firstDisplayedCell = value;
			}
		}

		/// <summary>Gets the width of the portion of the column that is currently scrolled out of view..</summary>
		/// <returns>The width of the portion of the column that is scrolled out of view.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0003B010 File Offset: 0x00039210
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public int FirstDisplayedScrollingColumnHiddenWidth
		{
			get
			{
				return this.firstDisplayedScrollingColumnHiddenWidth;
			}
		}

		/// <summary>Gets or sets the index of the column that is the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>The index of the column that is the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0 or greater than the number of columns in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewColumn.Visible" /> property value of false.-or-The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003B018 File Offset: 0x00039218
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0003B020 File Offset: 0x00039220
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int FirstDisplayedScrollingColumnIndex
		{
			get
			{
				return this.firstDisplayedScrollingColumnIndex;
			}
			set
			{
				this.firstDisplayedScrollingColumnIndex = value;
			}
		}

		/// <summary>Gets or sets the index of the row that is the first row displayed on the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>The index of the row that is the first row displayed on the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0 or greater than the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property indicates a row with a <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property value of false.-or-The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewRow.Frozen" /> property value of true.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003B02C File Offset: 0x0003922C
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0003B034 File Offset: 0x00039234
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int FirstDisplayedScrollingRowIndex
		{
			get
			{
				return this.firstDisplayedScrollingRowIndex;
			}
			set
			{
				this.firstDisplayedScrollingRowIndex = value;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003B040 File Offset: 0x00039240
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x0003B048 File Offset: 0x00039248
		[EditorBrowsable(2)]
		[Browsable(false)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the <see cref="T:System.Windows.Forms.DataGridView" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003B054 File Offset: 0x00039254
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x0003B05C File Offset: 0x0003925C
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the color of the grid lines separating the cells of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> or <see cref="T:System.Drawing.SystemColors" /> that represents the color of the grid lines. The default is <see cref="F:System.Drawing.KnownColor.ControlDarkDark" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Drawing.Color.Empty" />. -or-The specified value when setting this property has a <see cref="P:System.Drawing.Color.A" /> property value that is less that 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003B068 File Offset: 0x00039268
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x0003B070 File Offset: 0x00039270
		public Color GridColor
		{
			get
			{
				return this.gridColor;
			}
			set
			{
				if (this.gridColor != value)
				{
					if (value == Color.Empty)
					{
						throw new ArgumentException("Cant set an Empty color.");
					}
					this.gridColor = value;
					this.OnGridColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the number of pixels by which the control is scrolled horizontally. </summary>
		/// <returns>The number of pixels by which the control is scrolled horizontally.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003B0BC File Offset: 0x000392BC
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x0003B0C4 File Offset: 0x000392C4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int HorizontalScrollingOffset
		{
			get
			{
				return this.horizontalScrollingOffset;
			}
			set
			{
				this.horizontalScrollingOffset = value;
			}
		}

		/// <summary>Gets a value indicating whether the current cell has uncommitted changes.</summary>
		/// <returns>true if the current cell has uncommitted changes; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003B0D0 File Offset: 0x000392D0
		[Browsable(false)]
		public bool IsCurrentCellDirty
		{
			get
			{
				return this.isCurrentCellDirty;
			}
		}

		/// <summary>Gets a value indicating whether the currently active cell is being edited.</summary>
		/// <returns>true if the current cell is being edited; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003B0D8 File Offset: 0x000392D8
		[Browsable(false)]
		public bool IsCurrentCellInEditMode
		{
			get
			{
				return this.currentCell != null && this.currentCell.IsInEditMode;
			}
		}

		/// <summary>Gets a value indicating whether the current row has uncommitted changes.</summary>
		/// <returns>true if the current row has uncommitted changes; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003B0F4 File Offset: 0x000392F4
		[Browsable(false)]
		public bool IsCurrentRowDirty
		{
			get
			{
				if (!this.virtualMode)
				{
					return this.IsCurrentCellDirty;
				}
				QuestionEventArgs questionEventArgs = new QuestionEventArgs();
				this.OnRowDirtyStateNeeded(questionEventArgs);
				return questionEventArgs.Response;
			}
		}

		/// <summary>Provides an indexer to get or set the cell located at the intersection of the column and row with the specified indexes. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified location.</returns>
		/// <param name="columnIndex">The index of the column containing the cell.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x17000361 RID: 865
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewCell this[int columnIndex, int rowIndex]
		{
			get
			{
				return this.rows[rowIndex].Cells[columnIndex];
			}
			set
			{
				this.rows[rowIndex].Cells[columnIndex] = value;
			}
		}

		/// <summary>Provides an indexer to get or set the cell located at the intersection of the row with the specified index and the column with the specified name. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified location.</returns>
		/// <param name="columnName">The name of the column containing the cell.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x17000362 RID: 866
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewCell this[string columnName, int rowIndex]
		{
			get
			{
				int num = -1;
				foreach (object obj in this.columns)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
					if (dataGridViewColumn.Name == columnName)
					{
						num = dataGridViewColumn.Index;
						break;
					}
				}
				return this[num, rowIndex];
			}
			set
			{
				int num = -1;
				foreach (object obj in this.columns)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
					if (dataGridViewColumn.Name == columnName)
					{
						num = dataGridViewColumn.Index;
						break;
					}
				}
				this[num, rowIndex] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user is allowed to select more than one cell, row, or column of the <see cref="T:System.Windows.Forms.DataGridView" /> at a time.</summary>
		/// <returns>true if the user can select more than one cell, row, or column at a time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003B280 File Offset: 0x00039480
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x0003B288 File Offset: 0x00039488
		[DefaultValue(true)]
		public bool MultiSelect
		{
			get
			{
				return this.multiSelect;
			}
			set
			{
				if (this.multiSelect != value)
				{
					this.multiSelect = value;
					this.OnMultiSelectChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the index of the row for new records.</summary>
		/// <returns>The index of the row for new records, or -1 if <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is false.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003B2A8 File Offset: 0x000394A8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int NewRowIndex
		{
			get
			{
				if (!this.AllowUserToAddRows || this.ColumnCount == 0)
				{
					return -1;
				}
				return this.rows.Count - 1;
			}
		}

		/// <summary>This property is not relevant for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> instance.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003B2DC File Offset: 0x000394DC
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x0003B2E4 File Offset: 0x000394E4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return Padding.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003B2E8 File Offset: 0x000394E8
		internal DataGridViewHeaderCell PressedHeaderCell
		{
			get
			{
				return this.pressed_header_cell;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the cells of the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>true if the user cannot edit the cells of the <see cref="T:System.Windows.Forms.DataGridView" /> control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, the current cell is in edit mode, and the current cell contains changes that cannot be committed. </exception>
		/// <exception cref="T:System.Exception">The specified value when setting this property would cause the control to enter edit mode, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0003B2F0 File Offset: 0x000394F0
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0003B2F8 File Offset: 0x000394F8
		[Browsable(true)]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the number of rows displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>The number of rows to display in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is less than 0.-or-The specified value is less than 1 and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is set to true. </exception>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property is set. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003B318 File Offset: 0x00039518
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x0003B328 File Offset: 0x00039528
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[DefaultValue(0)]
		public int RowCount
		{
			get
			{
				return this.rows.Count;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("RowCount must be >= 0.");
				}
				if (value < 1 && this.AllowUserToAddRows)
				{
					throw new ArgumentException("RowCount must be >= 1 if AllowUserToAddRows is true.");
				}
				if (this.dataSource != null)
				{
					throw new InvalidOperationException("Cant change row count if DataSource is set.");
				}
				if (value < this.rows.Count)
				{
					int num = this.rows.Count - 1;
					if (this.AllowUserToAddRows)
					{
						num--;
					}
					int num2 = value - 1;
					if (this.AllowUserToAddRows)
					{
						num2--;
					}
					for (int i = num; i > num2; i--)
					{
						this.rows.RemoveAt(i);
					}
				}
				else if (value > this.rows.Count)
				{
					if (this.ColumnCount == 0)
					{
						this.ColumnCount = 1;
					}
					List<DataGridViewRow> list = new List<DataGridViewRow>(value - this.rows.Count);
					for (int j = this.rows.Count; j < value; j++)
					{
						list.Add(this.RowTemplateFull);
					}
					this.rows.AddRange(list.ToArray());
				}
			}
		}

		/// <summary>Gets or sets the border style of the row header cells.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle" /> value.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003B454 File Offset: 0x00039654
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0003B45C File Offset: 0x0003965C
		[DefaultValue(DataGridViewHeaderBorderStyle.Raised)]
		[Browsable(true)]
		public DataGridViewHeaderBorderStyle RowHeadersBorderStyle
		{
			get
			{
				return this.rowHeadersBorderStyle;
			}
			set
			{
				if (this.rowHeadersBorderStyle != value)
				{
					this.rowHeadersBorderStyle = value;
					this.OnRowHeadersBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the default style applied to the row header cells.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the default style applied to the row header cells.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0003B47C File Offset: 0x0003967C
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0003B484 File Offset: 0x00039684
		[AmbientValue(null)]
		public DataGridViewCellStyle RowHeadersDefaultCellStyle
		{
			get
			{
				return this.rowHeadersDefaultCellStyle;
			}
			set
			{
				if (this.rowHeadersDefaultCellStyle != value)
				{
					this.rowHeadersDefaultCellStyle = value;
					this.OnRowHeadersDefaultCellStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the column that contains row headers is displayed.</summary>
		/// <returns>true if the column that contains row headers is displayed; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property is set to <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003B4A4 File Offset: 0x000396A4
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0003B4AC File Offset: 0x000396AC
		[DefaultValue(true)]
		public bool RowHeadersVisible
		{
			get
			{
				return this.rowHeadersVisible;
			}
			set
			{
				if (this.rowHeadersVisible != value)
				{
					this.rowHeadersVisible = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the width, in pixels, of the column that contains the row headers.</summary>
		/// <returns>The width, in pixels, of the column that contains row headers. The default is 43.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than the minimum width of 4 pixels or is greater than the maximum width of 32768 pixels.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003B4C8 File Offset: 0x000396C8
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003B4D0 File Offset: 0x000396D0
		[Localizable(true)]
		public int RowHeadersWidth
		{
			get
			{
				return this.rowHeadersWidth;
			}
			set
			{
				if (this.rowHeadersWidth != value)
				{
					if (value < 4)
					{
						throw new ArgumentOutOfRangeException("RowHeadersWidth", "Row headers width cant be less than 4.");
					}
					if (value > 32768)
					{
						throw new ArgumentOutOfRangeException("RowHeadersWidth", "Row headers width cannot be more than 32768.");
					}
					this.rowHeadersWidth = value;
					this.OnRowHeadersWidthChanged(EventArgs.Empty);
					if (this.rowHeadersVisible)
					{
						base.Invalidate();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the width of the row headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value indicating the mode by which the width of the row headers can be adjusted. The default is <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value.</exception>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003B540 File Offset: 0x00039740
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0003B548 File Offset: 0x00039748
		[DefaultValue(DataGridViewRowHeadersWidthSizeMode.EnableResizing)]
		[RefreshProperties(1)]
		public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
		{
			get
			{
				return this.rowHeadersWidthSizeMode;
			}
			set
			{
				if (this.rowHeadersWidthSizeMode != value)
				{
					if (!Enum.IsDefined(typeof(DataGridViewRowHeadersWidthSizeMode), value))
					{
						throw new InvalidEnumArgumentException("Value is not valid DataGridViewRowHeadersWidthSizeMode.");
					}
					this.rowHeadersWidthSizeMode = value;
					this.OnRowHeadersWidthSizeModeChanged(new DataGridViewAutoSizeModeEventArgs(false));
				}
			}
		}

		/// <summary>Gets a collection that contains all the rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that contains all the rows in the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003B59C File Offset: 0x0003979C
		[Browsable(false)]
		public DataGridViewRowCollection Rows
		{
			get
			{
				return this.rows;
			}
		}

		/// <summary>Gets or sets the default style applied to the row cells of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to apply to the row cells of the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003B5A4 File Offset: 0x000397A4
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0003B5AC File Offset: 0x000397AC
		public DataGridViewCellStyle RowsDefaultCellStyle
		{
			get
			{
				return this.rowsDefaultCellStyle;
			}
			set
			{
				if (this.rowsDefaultCellStyle != value)
				{
					this.rowsDefaultCellStyle = value;
					this.OnRowsDefaultCellStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the row that represents the template for all the rows in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRow" /> representing the row template.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified row when setting this property has its <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property set.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003B5CC File Offset: 0x000397CC
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0003B5EC File Offset: 0x000397EC
		[Browsable(true)]
		[DesignerSerializationVisibility(2)]
		public DataGridViewRow RowTemplate
		{
			get
			{
				if (this.rowTemplate == null)
				{
					this.rowTemplate = new DataGridViewRow();
				}
				return this.rowTemplate;
			}
			set
			{
				this.rowTemplate = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003B5F8 File Offset: 0x000397F8
		internal DataGridViewRow RowTemplateFull
		{
			get
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.RowTemplate.Clone();
				for (int i = dataGridViewRow.Cells.Count; i < this.Columns.Count; i++)
				{
					DataGridViewCell cellTemplate = this.columns[i].CellTemplate;
					if (cellTemplate == null)
					{
						throw new InvalidOperationException("At least one of the DataGridView control's columns has no cell template.");
					}
					dataGridViewRow.Cells.Add((DataGridViewCell)cellTemplate.Clone());
				}
				return dataGridViewRow;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0003B678 File Offset: 0x00039878
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the type of scroll bars to display for the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollBars" /> values. The default is <see cref="F:System.Windows.Forms.ScrollBars.Both" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.ScrollBars" /> value. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value of this property cannot be set because the <see cref="T:System.Windows.Forms.DataGridView" /> is unable to scroll due to a cell change that cannot be committed or canceled. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0003B67C File Offset: 0x0003987C
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0003B684 File Offset: 0x00039884
		[DefaultValue(ScrollBars.Both)]
		[Localizable(true)]
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ScrollBars), value))
				{
					throw new InvalidEnumArgumentException("Invalid ScrollBars value.");
				}
				this.scrollBars = value;
				base.PerformLayout();
				base.Invalidate();
			}
		}

		/// <summary>Gets the collection of cells selected by the user.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" /> that represents the cells selected by the user.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003B6CC File Offset: 0x000398CC
		[Browsable(false)]
		public DataGridViewSelectedCellCollection SelectedCells
		{
			get
			{
				DataGridViewSelectedCellCollection dataGridViewSelectedCellCollection = new DataGridViewSelectedCellCollection();
				foreach (object obj in this.rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					foreach (object obj2 in dataGridViewRow.Cells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj2;
						if (dataGridViewCell.Selected)
						{
							dataGridViewSelectedCellCollection.InternalAdd(dataGridViewCell);
						}
					}
				}
				return dataGridViewSelectedCellCollection;
			}
		}

		/// <summary>Gets the collection of columns selected by the user.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" /> that represents the columns selected by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0003B7B0 File Offset: 0x000399B0
		[Browsable(false)]
		public DataGridViewSelectedColumnCollection SelectedColumns
		{
			get
			{
				DataGridViewSelectedColumnCollection dataGridViewSelectedColumnCollection = new DataGridViewSelectedColumnCollection();
				if (this.selectionMode != DataGridViewSelectionMode.FullColumnSelect && this.selectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
				{
					return dataGridViewSelectedColumnCollection;
				}
				dataGridViewSelectedColumnCollection.InternalAddRange(this.selected_columns);
				return dataGridViewSelectedColumnCollection;
			}
		}

		/// <summary>Gets the collection of rows selected by the user.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection" /> that contains the rows selected by the user.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003B7EC File Offset: 0x000399EC
		[Browsable(false)]
		public DataGridViewSelectedRowCollection SelectedRows
		{
			get
			{
				DataGridViewSelectedRowCollection dataGridViewSelectedRowCollection = new DataGridViewSelectedRowCollection(this);
				if (this.selectionMode != DataGridViewSelectionMode.FullRowSelect && this.selectionMode != DataGridViewSelectionMode.RowHeaderSelect)
				{
					return dataGridViewSelectedRowCollection;
				}
				dataGridViewSelectedRowCollection.InternalAddRange(this.selected_rows);
				return dataGridViewSelectedRowCollection;
			}
		}

		/// <summary>Gets or sets a value indicating how the cells of the <see cref="T:System.Windows.Forms.DataGridView" /> can be selected.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewSelectionMode" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewSelectionMode" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" /> and the <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property of one or more columns is set to <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0003B828 File Offset: 0x00039A28
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x0003B830 File Offset: 0x00039A30
		[Browsable(true)]
		[DefaultValue(DataGridViewSelectionMode.RowHeaderSelect)]
		public DataGridViewSelectionMode SelectionMode
		{
			get
			{
				return this.selectionMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewSelectionMode), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewSelectionMode.");
				}
				if (value == DataGridViewSelectionMode.ColumnHeaderSelect || value == DataGridViewSelectionMode.FullColumnSelect)
				{
					foreach (object obj in this.Columns)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
						if (dataGridViewColumn.SortMode == DataGridViewColumnSortMode.Automatic)
						{
							throw new InvalidOperationException(string.Format("Cannot set SelectionMode to {0} because there are Automatic sort columns.", value));
						}
					}
				}
				this.selectionMode = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to show cell errors.</summary>
		/// <returns>true if a red glyph will appear in a cell that fails validation; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0003B8F4 File Offset: 0x00039AF4
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x0003B8FC File Offset: 0x00039AFC
		[DefaultValue(true)]
		public bool ShowCellErrors
		{
			get
			{
				return this.showCellErrors;
			}
			set
			{
				this.showCellErrors = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not ToolTips will show when the mouse pointer pauses on a cell.</summary>
		/// <returns>true if cell ToolTips are enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0003B908 File Offset: 0x00039B08
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x0003B910 File Offset: 0x00039B10
		[DefaultValue(true)]
		public bool ShowCellToolTips
		{
			get
			{
				return this.showCellToolTips;
			}
			set
			{
				this.showCellToolTips = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not the editing glyph is visible in the row header of the cell being edited.</summary>
		/// <returns>true if the editing glyph is visible; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0003B91C File Offset: 0x00039B1C
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x0003B924 File Offset: 0x00039B24
		[DefaultValue(true)]
		public bool ShowEditingIcon
		{
			get
			{
				return this.showEditingIcon;
			}
			set
			{
				this.showEditingIcon = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether row headers will display error glyphs for each row that contains a data entry error. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.DataGridViewRow" /> indicates there is an error; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0003B930 File Offset: 0x00039B30
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0003B938 File Offset: 0x00039B38
		[DefaultValue(true)]
		public bool ShowRowErrors
		{
			get
			{
				return this.showRowErrors;
			}
			set
			{
				this.showRowErrors = value;
			}
		}

		/// <summary>Gets the column by which the <see cref="T:System.Windows.Forms.DataGridView" /> contents are currently sorted.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> by which the <see cref="T:System.Windows.Forms.DataGridView" /> contents are currently sorted.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0003B944 File Offset: 0x00039B44
		[Browsable(false)]
		public DataGridViewColumn SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		/// <summary>Gets a value indicating whether the items in the <see cref="T:System.Windows.Forms.DataGridView" /> control are sorted in ascending or descending order, or are not sorted.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SortOrder" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0003B94C File Offset: 0x00039B4C
		[Browsable(false)]
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		/// <summary>Gets or sets a value indicating whether the TAB key moves the focus to the next control in the tab order rather than moving focus to the next cell in the control.</summary>
		/// <returns>true if the TAB key moves the focus to the next control in the tab order; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0003B954 File Offset: 0x00039B54
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x0003B95C File Offset: 0x00039B5C
		[DefaultValue(false)]
		[EditorBrowsable(2)]
		public bool StandardTab
		{
			get
			{
				return this.standardTab;
			}
			set
			{
				this.standardTab = value;
			}
		}

		/// <summary>Gets or sets the text associated with the control.</summary>
		/// <returns>The text associated with the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003B968 File Offset: 0x00039B68
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x0003B970 File Offset: 0x00039B70
		[Bindable(false)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the header cell located in the upper left corner of the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> located at the upper left corner of the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003B97C File Offset: 0x00039B7C
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewHeaderCell TopLeftHeaderCell
		{
			get
			{
				if (this.topLeftHeaderCell == null)
				{
					this.topLeftHeaderCell = new DataGridViewTopLeftHeaderCell();
					this.topLeftHeaderCell.SetDataGridView(this);
				}
				return this.topLeftHeaderCell;
			}
			set
			{
				if (this.topLeftHeaderCell == value)
				{
					return;
				}
				if (this.topLeftHeaderCell != null)
				{
					this.topLeftHeaderCell.SetDataGridView(null);
				}
				this.topLeftHeaderCell = value;
				if (this.topLeftHeaderCell != null)
				{
					this.topLeftHeaderCell.SetDataGridView(this);
				}
			}
		}

		/// <summary>Gets the default or user-specified value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> representing the normal value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003BA04 File Offset: 0x00039C04
		[EditorBrowsable(2)]
		[Browsable(false)]
		public Cursor UserSetCursor
		{
			get
			{
				return this.userSetCursor;
			}
		}

		/// <summary>Gets the number of pixels by which the control is scrolled vertically.</summary>
		/// <returns>The number of pixels by which the control is scrolled vertically.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003BA0C File Offset: 0x00039C0C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int VerticalScrollingOffset
		{
			get
			{
				return this.verticalScrollingOffset;
			}
		}

		/// <summary>Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.DataGridView" /> uses data-management operations that you provide; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0003BA14 File Offset: 0x00039C14
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0003BA1C File Offset: 0x00039C1C
		[DefaultValue(false)]
		[MonoTODO("VirtualMode is not supported.")]
		[EditorBrowsable(2)]
		public bool VirtualMode
		{
			get
			{
				return this.virtualMode;
			}
			set
			{
				this.virtualMode = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003BA28 File Offset: 0x00039C28
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0003BA30 File Offset: 0x00039C30
		internal Control EditingControlInternal
		{
			get
			{
				return this.editingControl;
			}
			set
			{
				if (value == this.editingControl)
				{
					return;
				}
				if (this.editingControl != null)
				{
					DataGridView.DataGridViewControlCollection dataGridViewControlCollection = base.Controls as DataGridView.DataGridViewControlCollection;
					if (dataGridViewControlCollection != null)
					{
						dataGridViewControlCollection.RemoveInternal(this.editingControl);
					}
					else
					{
						base.Controls.Remove(this.editingControl);
					}
				}
				if (value != null)
				{
					value.Visible = false;
					base.Controls.Add(value);
				}
				this.editingControl = value;
			}
		}

		/// <summary>Adjusts the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> for a column header cell of a <see cref="T:System.Windows.Forms.DataGridView" /> that is currently being painted.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style for the current column header.</returns>
		/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that that represents the column header border style to modify.</param>
		/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the column header border style.</param>
		/// <param name="isFirstDisplayedColumn">true to indicate that the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is currently being painted is in the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
		/// <param name="isLastVisibleColumn">true to indicate that the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is currently being painted is in the last column in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewColumn.Visible" /> property set to true; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003BAAC File Offset: 0x00039CAC
		[EditorBrowsable(2)]
		public virtual DataGridViewAdvancedBorderStyle AdjustColumnHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool isFirstDisplayedColumn, bool isLastVisibleColumn)
		{
			return (DataGridViewAdvancedBorderStyle)dataGridViewAdvancedBorderStyleInput.Clone();
		}

		/// <summary>Returns a value indicating whether all the <see cref="T:System.Windows.Forms.DataGridView" /> cells are currently selected.</summary>
		/// <returns>true if all cells (or all visible cells) are selected or if there are no cells (or no visible cells); otherwise, false.</returns>
		/// <param name="includeInvisibleCells">true to include the rows and columns with <see cref="P:System.Windows.Forms.DataGridViewBand.Visible" /> property values of false; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003BABC File Offset: 0x00039CBC
		public bool AreAllCellsSelected(bool includeInvisibleCells)
		{
			foreach (object obj in this.rows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				foreach (object obj2 in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj2;
					if (includeInvisibleCells || dataGridViewCell.Visible)
					{
						if (!dataGridViewCell.Selected)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		/// <summary>Adjusts the width of the specified column to fit the contents of all its cells, including the header cell. </summary>
		/// <param name="columnIndex">The index of the column to resize.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		public void AutoResizeColumn(int columnIndex)
		{
			this.AutoResizeColumn(columnIndex, DataGridViewAutoSizeColumnMode.AllCells);
		}

		/// <summary>Adjusts the width of the specified column using the specified size mode.</summary>
		/// <param name="columnIndex">The index of the column to resize. </param>
		/// <param name="autoSizeColumnMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeColumnMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None" />, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value.</exception>
		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003BBBC File Offset: 0x00039DBC
		public void AutoResizeColumn(int columnIndex, DataGridViewAutoSizeColumnMode autoSizeColumnMode)
		{
			this.AutoResizeColumnInternal(columnIndex, autoSizeColumnMode);
		}

		/// <summary>Adjusts the height of the column headers to fit the contents of the largest column header.</summary>
		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003BBC8 File Offset: 0x00039DC8
		public void AutoResizeColumnHeadersHeight()
		{
			int num = 0;
			foreach (object obj in this.Columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				num = Math.Max(num, dataGridViewColumn.HeaderCell.PreferredSize.Height);
			}
			if (this.ColumnHeadersHeight != num)
			{
				this.ColumnHeadersHeight = num;
			}
		}

		/// <summary>Adjusts the height of the column headers based on changes to the contents of the header in the specified column.</summary>
		/// <param name="columnIndex">The index of the column containing the header with the changed content.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1.</exception>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003BC64 File Offset: 0x00039E64
		[MonoTODO("columnIndex parameter is not used")]
		public void AutoResizeColumnHeadersHeight(int columnIndex)
		{
			this.AutoResizeColumnHeadersHeight();
		}

		/// <summary>Adjusts the width of all columns to fit the contents of all their cells, including the header cells.</summary>
		// Token: 0x06000FCA RID: 4042 RVA: 0x0003BC6C File Offset: 0x00039E6C
		public void AutoResizeColumns()
		{
			this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		/// <summary>Adjusts the width of all columns using the specified size mode.</summary>
		/// <param name="autoSizeColumnsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeColumnsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill" />. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnsMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> value.</exception>
		// Token: 0x06000FCB RID: 4043 RVA: 0x0003BC78 File Offset: 0x00039E78
		public void AutoResizeColumns(DataGridViewAutoSizeColumnsMode autoSizeColumnsMode)
		{
			this.AutoResizeColumns(autoSizeColumnsMode, true);
		}

		/// <summary>Adjusts the height of the specified row to fit the contents of all its cells including the header cell.</summary>
		/// <param name="rowIndex">The index of the row to resize.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1. </exception>
		// Token: 0x06000FCC RID: 4044 RVA: 0x0003BC84 File Offset: 0x00039E84
		public void AutoResizeRow(int rowIndex)
		{
			this.AutoResizeRow(rowIndex, DataGridViewAutoSizeRowMode.AllCells, true);
		}

		/// <summary>Adjusts the height of the specified row using the specified size mode.</summary>
		/// <param name="rowIndex">The index of the row to resize. </param>
		/// <param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeRowMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowMode.RowHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1.</exception>
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003BC90 File Offset: 0x00039E90
		public void AutoResizeRow(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode)
		{
			this.AutoResizeRow(rowIndex, autoSizeRowMode, true);
		}

		/// <summary>Adjusts the width of the row headers using the specified size mode.</summary>
		/// <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value. </exception>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003BC9C File Offset: 0x00039E9C
		public void AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)
		{
			if (rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader)
			{
				this.RowHeadersWidth = this.GetRowInternal(0).HeaderCell.PreferredSize.Width;
				return;
			}
			int num = 0;
			if (rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders)
			{
				foreach (object obj in this.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					if (dataGridViewRow.Displayed)
					{
						num = Math.Max(num, dataGridViewRow.HeaderCell.PreferredSize.Width);
					}
				}
				if (this.RowHeadersWidth != num)
				{
					this.RowHeadersWidth = num;
				}
				return;
			}
			if (rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders)
			{
				foreach (object obj2 in this.Rows)
				{
					DataGridViewRow dataGridViewRow2 = (DataGridViewRow)obj2;
					num = Math.Max(num, dataGridViewRow2.HeaderCell.PreferredSize.Width);
				}
				if (this.RowHeadersWidth != num)
				{
					this.RowHeadersWidth = num;
				}
				return;
			}
		}

		/// <summary>Adjusts the width of the row headers based on changes to the contents of the header in the specified row and using the specified size mode.</summary>
		/// <param name="rowIndex">The index of the row header with the changed content.</param>
		/// <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /></exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value. </exception>
		// Token: 0x06000FCF RID: 4047 RVA: 0x0003BE08 File Offset: 0x0003A008
		[MonoTODO("Does not use rowIndex parameter.")]
		public void AutoResizeRowHeadersWidth(int rowIndex, DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)
		{
			this.AutoResizeRowHeadersWidth(rowHeadersWidthSizeMode);
		}

		/// <summary>Adjusts the heights of all rows to fit the contents of all their cells, including the header cells.</summary>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003BE14 File Offset: 0x0003A014
		public void AutoResizeRows()
		{
			this.AutoResizeRows(0, this.Rows.Count, DataGridViewAutoSizeRowMode.AllCells, false);
		}

		/// <summary>Adjusts the heights of the rows using the specified size mode value.</summary>
		/// <param name="autoSizeRowsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders" />, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowsMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" />.</exception>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003BE38 File Offset: 0x0003A038
		public void AutoResizeRows(DataGridViewAutoSizeRowsMode autoSizeRowsMode)
		{
			if (!Enum.IsDefined(typeof(DataGridViewAutoSizeRowsMode), autoSizeRowsMode))
			{
				throw new InvalidEnumArgumentException("Parameter autoSizeRowsMode is not a valid DataGridViewRowsMode.");
			}
			if ((autoSizeRowsMode == DataGridViewAutoSizeRowsMode.AllHeaders || autoSizeRowsMode == DataGridViewAutoSizeRowsMode.DisplayedHeaders) && !this.rowHeadersVisible)
			{
				throw new InvalidOperationException("Parameter autoSizeRowsMode cannot be AllHeaders or DisplayedHeaders in this DataGridView.");
			}
			if (autoSizeRowsMode == DataGridViewAutoSizeRowsMode.None)
			{
				throw new ArgumentException("Parameter autoSizeRowsMode cannot be None.");
			}
			this.AutoResizeRows(autoSizeRowsMode, false);
		}

		/// <summary>Puts the current cell in edit mode.</summary>
		/// <returns>true if the current cell is already in edit mode or successfully enters edit mode; otherwise, false.</returns>
		/// <param name="selectAll">true to select all the cell's contents; false to not select any contents.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridView.CurrentCell" /> is not set to a valid cell.-or-This method was called in a handler for the <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit" /> event.</exception>
		/// <exception cref="T:System.InvalidCastException">The type indicated by the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property does not derive from the <see cref="T:System.Windows.Forms.Control" /> type.-or-The type indicated by the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property does not implement the <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" /> interface.</exception>
		/// <exception cref="T:System.Exception">Initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
		public virtual bool BeginEdit(bool selectAll)
		{
			if (this.currentCell == null || this.currentCell.IsInEditMode)
			{
				return false;
			}
			if (this.currentCell.RowIndex >= 0 && (this.currentCell.InheritedState & DataGridViewElementStates.ReadOnly) == DataGridViewElementStates.ReadOnly)
			{
				return false;
			}
			DataGridViewCell dataGridViewCell = this.currentCell;
			Type editType = dataGridViewCell.EditType;
			if (editType == null && !(dataGridViewCell is IDataGridViewEditingCell))
			{
				return false;
			}
			DataGridViewCellCancelEventArgs dataGridViewCellCancelEventArgs = new DataGridViewCellCancelEventArgs(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex);
			this.OnCellBeginEdit(dataGridViewCellCancelEventArgs);
			if (dataGridViewCellCancelEventArgs.Cancel)
			{
				return false;
			}
			dataGridViewCell.SetIsInEditMode(true);
			if (editType != null)
			{
				Control control = this.EditingControlInternal;
				if (control == null || control.GetType() != editType)
				{
					control = null;
				}
				if (control == null)
				{
					control = (Control)Activator.CreateInstance(editType);
					this.EditingControlInternal = control;
				}
				DataGridViewCellStyle dataGridViewCellStyle = ((dataGridViewCell.RowIndex != -1) ? dataGridViewCell.InheritedStyle : this.DefaultCellStyle);
				dataGridViewCell.InitializeEditingControl(dataGridViewCell.RowIndex, dataGridViewCell.FormattedValue, dataGridViewCellStyle);
				dataGridViewCell.PositionEditingControl(true, true, this.GetCellDisplayRectangle(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, false), this.bounds, dataGridViewCellStyle, false, false, this.columns[dataGridViewCell.ColumnIndex].DisplayIndex == 0, dataGridViewCell.RowIndex == 0);
				if (this.EditingControlInternal != null)
				{
					this.EditingControlInternal.Visible = true;
				}
				IDataGridViewEditingControl dataGridViewEditingControl = (IDataGridViewEditingControl)this.EditingControlInternal;
				if (dataGridViewEditingControl != null)
				{
					dataGridViewEditingControl.EditingControlDataGridView = this;
					dataGridViewEditingControl.EditingControlRowIndex = this.currentCell.OwningRow.Index;
					dataGridViewEditingControl.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
					dataGridViewEditingControl.PrepareEditingControlForEdit(selectAll);
					dataGridViewEditingControl.EditingControlFormattedValue = this.currentCell.EditedFormattedValue;
				}
				return true;
			}
			(dataGridViewCell as IDataGridViewEditingCell).PrepareEditingCellForEdit(selectAll);
			return true;
		}

		/// <summary>Cancels edit mode for the currently selected cell and discards any changes.</summary>
		/// <returns>true if the cancel was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003C080 File Offset: 0x0003A280
		public bool CancelEdit()
		{
			if (this.currentCell != null)
			{
				if (this.currentCell.IsInEditMode)
				{
					this.currentCell.SetIsInEditMode(false);
					this.currentCell.DetachEditingControl();
				}
				if (this.currentCell.RowIndex == this.NewRowIndex)
				{
					if (this.DataManager != null)
					{
						this.DataManager.CancelCurrentEdit();
					}
					this.new_row_editing = false;
					this.PrepareEditingRow(false, false);
					this.MoveCurrentCell(this.currentCell.ColumnIndex, this.NewRowIndex, true, false, false, true);
					this.OnUserDeletedRow(new DataGridViewRowEventArgs(this.EditingRow));
				}
			}
			return true;
		}

		/// <summary>Clears the current selection by unselecting all selected cells.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003C128 File Offset: 0x0003A328
		public void ClearSelection()
		{
			foreach (object obj in this.SelectedColumns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				dataGridViewColumn.Selected = false;
			}
			foreach (object obj2 in this.SelectedRows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj2;
				dataGridViewRow.Selected = false;
			}
			foreach (object obj3 in this.SelectedCells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj3;
				dataGridViewCell.Selected = false;
			}
		}

		/// <summary>Commits changes in the current cell to the data cache without ending edit mode.</summary>
		/// <returns>true if the changes were committed; otherwise false.</returns>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which an error can occur. </param>
		/// <exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003C264 File Offset: 0x0003A464
		public bool CommitEdit(DataGridViewDataErrorContexts context)
		{
			if (this.currentCell == null)
			{
				return true;
			}
			try
			{
				object obj = this.currentCell.ParseFormattedValue(this.currentCell.EditedFormattedValue, this.currentCell.InheritedStyle, null, null);
				DataGridViewCellValidatingEventArgs dataGridViewCellValidatingEventArgs = new DataGridViewCellValidatingEventArgs(this.currentCell.ColumnIndex, this.currentCell.RowIndex, obj);
				this.OnCellValidating(dataGridViewCellValidatingEventArgs);
				if (dataGridViewCellValidatingEventArgs.Cancel)
				{
					return false;
				}
				this.OnCellValidated(new DataGridViewCellEventArgs(this.currentCell.ColumnIndex, this.currentCell.RowIndex));
				this.currentCell.Value = obj;
			}
			catch (Exception ex)
			{
				DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs = new DataGridViewDataErrorEventArgs(ex, this.currentCell.ColumnIndex, this.currentCell.RowIndex, DataGridViewDataErrorContexts.Commit);
				this.OnDataError(false, dataGridViewDataErrorEventArgs);
				if (dataGridViewDataErrorEventArgs.ThrowException)
				{
					throw ex;
				}
				return false;
			}
			return true;
		}

		/// <summary>Returns the number of columns displayed to the user.</summary>
		/// <returns>The number of columns displayed to the user.</returns>
		/// <param name="includePartialColumns">true to include partial columns in the displayed column count; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003C370 File Offset: 0x0003A570
		public int DisplayedColumnCount(bool includePartialColumns)
		{
			int num = 0;
			int num2 = 0;
			if (this.RowHeadersVisible)
			{
				num2 += this.RowHeadersWidth;
			}
			Size clientSize = base.ClientSize;
			if (this.verticalScrollBar.Visible)
			{
				clientSize.Width -= this.verticalScrollBar.Width;
			}
			if (this.horizontalScrollBar.Visible)
			{
				clientSize.Height -= this.horizontalScrollBar.Height;
			}
			for (int i = this.first_col_index; i < this.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.Columns[this.ColumnDisplayIndexToIndex(i)];
				if (num2 + dataGridViewColumn.Width > clientSize.Width)
				{
					if (includePartialColumns)
					{
						num++;
					}
					break;
				}
				num++;
				num2 += dataGridViewColumn.Width;
			}
			return num;
		}

		/// <summary>Returns the number of rows displayed to the user.</summary>
		/// <returns>The number of rows displayed to the user.</returns>
		/// <param name="includePartialRow">true to include partial rows in the displayed row count; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003C45C File Offset: 0x0003A65C
		public int DisplayedRowCount(bool includePartialRow)
		{
			int num = 0;
			int num2 = 0;
			if (this.ColumnHeadersVisible)
			{
				num2 += this.ColumnHeadersHeight;
			}
			Size clientSize = base.ClientSize;
			if (this.verticalScrollBar.Visible)
			{
				clientSize.Width -= this.verticalScrollBar.Width;
			}
			if (this.horizontalScrollBar.Visible)
			{
				clientSize.Height -= this.horizontalScrollBar.Height;
			}
			for (int i = this.first_row_index; i < this.Rows.Count; i++)
			{
				DataGridViewRow rowInternal = this.GetRowInternal(i);
				if (num2 + rowInternal.Height > clientSize.Height)
				{
					if (includePartialRow)
					{
						num++;
					}
					break;
				}
				num++;
				num2 += rowInternal.Height;
			}
			return num;
		}

		/// <summary>Commits and ends the edit operation on the current cell using the default error context.</summary>
		/// <returns>true if the edit operation is committed and ended; otherwise, false.</returns>
		/// <exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003C53C File Offset: 0x0003A73C
		public bool EndEdit()
		{
			return this.EndEdit(DataGridViewDataErrorContexts.Commit);
		}

		/// <summary>Commits and ends the edit operation on the current cell using the specified error context.</summary>
		/// <returns>true if the edit operation is committed and ended; otherwise, false.</returns>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which an error can occur. </param>
		/// <exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003C54C File Offset: 0x0003A74C
		[MonoTODO("Does not use context parameter")]
		public bool EndEdit(DataGridViewDataErrorContexts context)
		{
			if (this.currentCell == null || !this.currentCell.IsInEditMode)
			{
				return true;
			}
			if (!this.CommitEdit(context))
			{
				if (this.DataManager != null)
				{
					this.DataManager.EndCurrentEdit();
				}
				if (this.EditingControl != null)
				{
					this.EditingControl.Focus();
				}
				return false;
			}
			this.currentCell.SetIsInEditMode(false);
			this.currentCell.DetachEditingControl();
			this.OnCellEndEdit(new DataGridViewCellEventArgs(this.currentCell.ColumnIndex, this.currentCell.RowIndex));
			base.Focus();
			if (this.currentCell.RowIndex == this.NewRowIndex)
			{
				this.new_row_editing = false;
				this.editing_row = null;
				this.PrepareEditingRow(true, false);
				this.MoveCurrentCell(this.currentCell.ColumnIndex, this.NewRowIndex, true, false, false, true);
			}
			return true;
		}

		/// <summary>Gets the number of cells that satisfy the provided filter.</summary>
		/// <returns>The number of cells that match the <paramref name="includeFilter" /> parameter.</returns>
		/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values specifying the cells to count.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="includeFilter" /> includes the value <see cref="F:System.Windows.Forms.DataGridViewElementStates.ResizableSet" />.</exception>
		// Token: 0x06000FDA RID: 4058 RVA: 0x0003C638 File Offset: 0x0003A838
		public int GetCellCount(DataGridViewElementStates includeFilter)
		{
			int num = 0;
			foreach (object obj in this.rows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				foreach (object obj2 in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj2;
					if ((dataGridViewCell.State & includeFilter) != DataGridViewElementStates.None)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003C718 File Offset: 0x0003A918
		internal DataGridViewRow GetRowInternal(int rowIndex)
		{
			return this.Rows.SharedRow(rowIndex);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003C728 File Offset: 0x0003A928
		internal DataGridViewCell GetCellInternal(int colIndex, int rowIndex)
		{
			return this.GetRowInternal(rowIndex).Cells.GetCellInternal(colIndex);
		}

		/// <summary>Returns the rectangle that represents the display area for a cell.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the display rectangle of the cell.</returns>
		/// <param name="columnIndex">The column index for the desired cell. </param>
		/// <param name="rowIndex">The row index for the desired cell. </param>
		/// <param name="cutOverflow">true to return the displayed portion of the cell only; false to return the entire cell bounds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex" /> is less than -1 or greater than the number of rows in the control minus 1. </exception>
		// Token: 0x06000FDD RID: 4061 RVA: 0x0003C73C File Offset: 0x0003A93C
		public Rectangle GetCellDisplayRectangle(int columnIndex, int rowIndex, bool cutOverflow)
		{
			if (columnIndex < 0 || columnIndex >= this.columns.Count)
			{
				throw new ArgumentOutOfRangeException("Column index is out of range.");
			}
			int num = 0;
			int num2 = 0;
			int num3 = this.BorderWidth;
			int num4 = this.BorderWidth;
			if (this.ColumnHeadersVisible)
			{
				num4 += this.ColumnHeadersHeight;
			}
			if (this.RowHeadersVisible)
			{
				num3 += this.RowHeadersWidth;
			}
			List<DataGridViewColumn> columnDisplayIndexSortedArrayList = this.columns.ColumnDisplayIndexSortedArrayList;
			for (int i = this.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
			{
				if (columnDisplayIndexSortedArrayList[i].Visible)
				{
					if (columnDisplayIndexSortedArrayList[i].Index == columnIndex)
					{
						num = columnDisplayIndexSortedArrayList[i].Width;
						break;
					}
					num3 += columnDisplayIndexSortedArrayList[i].Width;
				}
			}
			for (int j = this.first_row_index; j < this.Rows.Count; j++)
			{
				if (this.rows[j].Visible)
				{
					if (this.rows[j].Index == rowIndex)
					{
						num2 = this.rows[j].Height;
						break;
					}
					num4 += this.rows[j].Height;
				}
			}
			return new Rectangle(num3, num4, num, num2);
		}

		/// <summary>Retrieves the formatted values that represent the contents of the selected cells for copying to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataObject" /> that represents the contents of the selected cells.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.Windows.Forms.DataGridView.ClipboardCopyMode" /> is set to <see cref="F:System.Windows.Forms.DataGridViewClipboardCopyMode.Disable" />.</exception>
		// Token: 0x06000FDE RID: 4062 RVA: 0x0003C8B8 File Offset: 0x0003AAB8
		public virtual DataObject GetClipboardContent()
		{
			if (this.clipboardCopyMode == DataGridViewClipboardCopyMode.Disable)
			{
				throw new InvalidOperationException("Generating Clipboard content is not supported when the ClipboardCopyMode property is Disable.");
			}
			int num = int.MaxValue;
			int num2 = int.MinValue;
			int num3 = int.MaxValue;
			int num4 = int.MinValue;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			switch (this.ClipboardCopyMode)
			{
			case DataGridViewClipboardCopyMode.EnableWithAutoHeaderText:
				flag4 = this.selectionMode != DataGridViewSelectionMode.CellSelect;
				break;
			case DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText:
				flag = (flag2 = true);
				break;
			}
			BitArray bitArray = new BitArray(this.RowCount);
			BitArray bitArray2 = new BitArray(this.ColumnCount);
			if (flag4 && !flag2)
			{
				for (int i = 0; i < this.ColumnCount; i++)
				{
					if (this.Columns[i].Selected)
					{
						flag2 = true;
						break;
					}
				}
			}
			for (int j = 0; j < this.RowCount; j++)
			{
				DataGridViewRow dataGridViewRow = this.Rows[j];
				if (flag4 && !flag && dataGridViewRow.Selected)
				{
					flag = true;
				}
				for (int k = 0; k < this.ColumnCount; k++)
				{
					DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[k];
					if (dataGridViewCell != null && dataGridViewCell.Selected)
					{
						bitArray2[k] = true;
						bitArray[j] = true;
						num = Math.Min(num, j);
						num3 = Math.Min(num3, k);
						num2 = Math.Max(num2, j);
						num4 = Math.Max(num4, k);
					}
				}
			}
			switch (this.selectionMode)
			{
			case DataGridViewSelectionMode.CellSelect:
			case DataGridViewSelectionMode.RowHeaderSelect:
			case DataGridViewSelectionMode.ColumnHeaderSelect:
				if (this.selectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
				{
					for (int l = num; l <= num2; l++)
					{
						bitArray.Set(l, true);
					}
				}
				else if (num <= num2)
				{
					bitArray.SetAll(true);
				}
				if (this.selectionMode != DataGridViewSelectionMode.RowHeaderSelect)
				{
					for (int m = num3; m <= num4; m++)
					{
						bitArray2.Set(m, true);
					}
				}
				break;
			case DataGridViewSelectionMode.FullRowSelect:
			case DataGridViewSelectionMode.FullColumnSelect:
				flag3 = true;
				break;
			}
			if (num > num2)
			{
				return null;
			}
			if (num3 > num4)
			{
				return null;
			}
			DataObject dataObject = new DataObject();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			int num5 = num;
			int num6 = num3;
			if (flag2)
			{
				num5 = -1;
			}
			int n = num5;
			while (n <= num2)
			{
				DataGridViewRow dataGridViewRow2 = null;
				if (n < 0)
				{
					goto IL_02BF;
				}
				if (bitArray[n])
				{
					dataGridViewRow2 = this.Rows[n];
					goto IL_02BF;
				}
				IL_045C:
				n++;
				continue;
				IL_02BF:
				if (flag)
				{
					num6 = -1;
				}
				for (int num7 = num6; num7 <= num4; num7++)
				{
					if (num7 < 0 || !flag3 || bitArray2[num7])
					{
						DataGridViewCell dataGridViewCell2;
						if (dataGridViewRow2 == null)
						{
							if (num7 == -1)
							{
								dataGridViewCell2 = this.TopLeftHeaderCell;
							}
							else
							{
								dataGridViewCell2 = this.Columns[num7].HeaderCell;
							}
						}
						else if (num7 == -1)
						{
							dataGridViewCell2 = dataGridViewRow2.HeaderCell;
						}
						else
						{
							dataGridViewCell2 = dataGridViewRow2.Cells[num7];
						}
						bool flag5 = num7 == num6;
						bool flag6 = num7 == num4;
						bool flag7 = n == num5;
						bool flag8 = n == num2;
						string text;
						string text2;
						string text3;
						string text4;
						if (dataGridViewCell2 == null)
						{
							text = string.Empty;
							text2 = string.Empty;
							text3 = string.Empty;
							text4 = string.Empty;
						}
						else
						{
							text = dataGridViewCell2.GetClipboardContentInternal(n, flag5, flag6, flag7, flag8, DataFormats.Text) as string;
							text2 = dataGridViewCell2.GetClipboardContentInternal(n, flag5, flag6, flag7, flag8, DataFormats.UnicodeText) as string;
							text3 = dataGridViewCell2.GetClipboardContentInternal(n, flag5, flag6, flag7, flag8, DataFormats.Html) as string;
							text4 = dataGridViewCell2.GetClipboardContentInternal(n, flag5, flag6, flag7, flag8, DataFormats.CommaSeparatedValue) as string;
						}
						stringBuilder.Append(text);
						stringBuilder2.Append(text2);
						stringBuilder3.Append(text3);
						stringBuilder4.Append(text4);
						if (num7 == -1)
						{
							num7 = num3 - 1;
						}
					}
				}
				if (n == -1)
				{
					n = num - 1;
					goto IL_045C;
				}
				goto IL_045C;
			}
			int num8 = 135 + stringBuilder3.Length;
			int num9 = num8 + 36;
			string text5 = "Version:1.0{0}StartHTML:00000097{0}EndHTML:{1:00000000}{0}StartFragment:00000133{0}EndFragment:{2:00000000}{0}<HTML>{0}<BODY>{0}<!--StartFragment-->";
			text5 = string.Format(text5, "\r\n", num9, num8);
			stringBuilder3.Insert(0, text5);
			stringBuilder3.AppendFormat("{0}<!--EndFragment-->{0}</BODY>{0}</HTML>", "\r\n");
			dataObject.SetData(DataFormats.CommaSeparatedValue, false, stringBuilder4.ToString());
			dataObject.SetData(DataFormats.Html, false, stringBuilder3.ToString());
			dataObject.SetData(DataFormats.UnicodeText, false, stringBuilder2.ToString());
			dataObject.SetData(DataFormats.Text, false, stringBuilder.ToString());
			return dataObject;
		}

		/// <summary>Returns the rectangle that represents the display area for a column, as determined by the column index.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the display rectangle of the column.</returns>
		/// <param name="columnIndex">The column index for the desired cell. </param>
		/// <param name="cutOverflow">true to return the column rectangle visible in the <see cref="T:System.Windows.Forms.DataGridView" /> bounds; false to return the entire column rectangle. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		// Token: 0x06000FDF RID: 4063 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		[MonoTODO("Does not use cutOverflow parameter")]
		public Rectangle GetColumnDisplayRectangle(int columnIndex, bool cutOverflow)
		{
			if (columnIndex < 0 || columnIndex > this.Columns.Count - 1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			int num = 0;
			int num2 = this.BorderWidth;
			if (this.RowHeadersVisible)
			{
				num2 += this.RowHeadersWidth;
			}
			List<DataGridViewColumn> columnDisplayIndexSortedArrayList = this.columns.ColumnDisplayIndexSortedArrayList;
			for (int i = this.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
			{
				if (columnDisplayIndexSortedArrayList[i].Visible)
				{
					if (columnDisplayIndexSortedArrayList[i].Index == columnIndex)
					{
						num = columnDisplayIndexSortedArrayList[i].Width;
						break;
					}
					num2 += columnDisplayIndexSortedArrayList[i].Width;
				}
			}
			return new Rectangle(num2, 0, num, base.Height);
		}

		/// <summary>Returns the rectangle that represents the display area for a row, as determined by the row index.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the display rectangle of the row.</returns>
		/// <param name="rowIndex">The row index for the desired cell. </param>
		/// <param name="cutOverflow">true to return the row rectangle visible in the <see cref="T:System.Windows.Forms.DataGridView" /> bounds; false to return the entire row rectangle. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1. </exception>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		[MonoTODO("Does not use cutOverflow parameter")]
		public Rectangle GetRowDisplayRectangle(int rowIndex, bool cutOverflow)
		{
			if (rowIndex < 0 || rowIndex > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			int num = 0;
			int num2 = this.BorderWidth;
			if (this.ColumnHeadersVisible)
			{
				num2 += this.ColumnHeadersHeight;
			}
			for (int i = this.first_row_index; i < this.Rows.Count; i++)
			{
				if (this.rows[i].Visible)
				{
					if (this.rows[i].Index == rowIndex)
					{
						num = this.rows[i].Height;
						break;
					}
					num2 += this.rows[i].Height;
				}
			}
			return new Rectangle(0, num2, base.Width, num);
		}

		/// <summary>Returns location information, such as row and column indices, given x- and y-coordinates.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" /> that contains the location information. </returns>
		/// <param name="x">The x-coordinate. </param>
		/// <param name="y">The y-coordinate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003CF88 File Offset: 0x0003B188
		public DataGridView.HitTestInfo HitTest(int x, int y)
		{
			bool flag = this.columnHeadersVisible && y >= 0 && y <= this.ColumnHeadersHeight;
			bool flag2 = this.rowHeadersVisible && x >= 0 && x <= this.RowHeadersWidth;
			if (flag && flag2)
			{
				return new DataGridView.HitTestInfo(-1, x, -1, y, DataGridViewHitTestType.TopLeftHeader);
			}
			if (this.horizontalScrollBar.Visible && this.horizontalScrollBar.Bounds.Contains(x, y))
			{
				return new DataGridView.HitTestInfo(-1, x, -1, y, DataGridViewHitTestType.HorizontalScrollBar);
			}
			if (this.verticalScrollBar.Visible && this.verticalScrollBar.Bounds.Contains(x, y))
			{
				return new DataGridView.HitTestInfo(-1, x, -1, y, DataGridViewHitTestType.VerticalScrollBar);
			}
			if (this.verticalScrollBar.Visible && this.horizontalScrollBar.Visible)
			{
				Rectangle rectangle;
				rectangle..ctor(this.verticalScrollBar.Left, this.horizontalScrollBar.Top, this.verticalScrollBar.Width, this.horizontalScrollBar.Height);
				if (rectangle.Contains(x, y))
				{
					return new DataGridView.HitTestInfo(-1, x, -1, y, DataGridViewHitTestType.None);
				}
			}
			int num = -1;
			int num2 = -1;
			int num3 = ((!this.columnHeadersVisible) ? 0 : this.columnHeadersHeight);
			for (int i = this.first_row_index; i < this.Rows.Count; i++)
			{
				DataGridViewRow dataGridViewRow = this.Rows[i];
				if (dataGridViewRow.Visible)
				{
					if (y > num3 && y <= num3 + dataGridViewRow.Height)
					{
						num = i;
						break;
					}
					num3 += dataGridViewRow.Height;
				}
			}
			int num4 = ((!this.rowHeadersVisible) ? 0 : this.RowHeadersWidth);
			List<DataGridViewColumn> columnDisplayIndexSortedArrayList = this.columns.ColumnDisplayIndexSortedArrayList;
			for (int j = this.first_col_index; j < columnDisplayIndexSortedArrayList.Count; j++)
			{
				if (columnDisplayIndexSortedArrayList[j].Visible)
				{
					if (x > num4 && x <= num4 + columnDisplayIndexSortedArrayList[j].Width)
					{
						num2 = columnDisplayIndexSortedArrayList[j].Index;
						break;
					}
					num4 += columnDisplayIndexSortedArrayList[j].Width;
				}
			}
			if (num2 >= 0 && num >= 0)
			{
				return new DataGridView.HitTestInfo(num2, x, num, y, DataGridViewHitTestType.Cell);
			}
			if (flag && num2 > -1)
			{
				return new DataGridView.HitTestInfo(num2, x, num, y, DataGridViewHitTestType.ColumnHeader);
			}
			if (flag2 && num > -1)
			{
				return new DataGridView.HitTestInfo(num2, x, num, y, DataGridViewHitTestType.RowHeader);
			}
			return new DataGridView.HitTestInfo(-1, x, -1, y, DataGridViewHitTestType.None);
		}

		/// <summary>Invalidates the specified cell of the <see cref="T:System.Windows.Forms.DataGridView" />, forcing it to be repainted.</summary>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to invalidate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dataGridViewCell" /> does not belong to the <see cref="T:System.Windows.Forms.DataGridView" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCell" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003D250 File Offset: 0x0003B450
		public void InvalidateCell(DataGridViewCell dataGridViewCell)
		{
			if (dataGridViewCell == null)
			{
				throw new ArgumentNullException("Cell is null");
			}
			if (dataGridViewCell.DataGridView != this)
			{
				throw new ArgumentException("The specified cell does not belong to this DataGridView.");
			}
			this.InvalidateCell(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex);
		}

		/// <summary>Invalidates the cell with the specified row and column indexes, forcing it to be repainted.</summary>
		/// <param name="columnIndex">The column index of the cell to invalidate.</param>
		/// <param name="rowIndex">The row index of the cell to invalidate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex" /> is less than -1 or greater than the number of rows in the control minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003D298 File Offset: 0x0003B498
		public void InvalidateCell(int columnIndex, int rowIndex)
		{
			if (columnIndex < 0 || columnIndex >= this.columns.Count)
			{
				throw new ArgumentOutOfRangeException("Column index is out of range.");
			}
			if (rowIndex < 0 || rowIndex >= this.rows.Count)
			{
				throw new ArgumentOutOfRangeException("Row index is out of range.");
			}
			if (!this.is_binding)
			{
				base.Invalidate(this.GetCellDisplayRectangle(columnIndex, rowIndex, true));
			}
		}

		/// <summary>Invalidates the specified column of the <see cref="T:System.Windows.Forms.DataGridView" />, forcing it to be repainted.</summary>
		/// <param name="columnIndex">The index of the column to invalidate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003D308 File Offset: 0x0003B508
		public void InvalidateColumn(int columnIndex)
		{
			if (columnIndex < 0 || columnIndex >= this.columns.Count)
			{
				throw new ArgumentOutOfRangeException("Column index is out of range.");
			}
			if (!this.is_binding)
			{
				base.Invalidate(this.GetColumnDisplayRectangle(columnIndex, true));
			}
		}

		/// <summary>Invalidates the specified row of the <see cref="T:System.Windows.Forms.DataGridView" />, forcing it to be repainted.</summary>
		/// <param name="rowIndex">The index of the row to invalidate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003D354 File Offset: 0x0003B554
		public void InvalidateRow(int rowIndex)
		{
			if (rowIndex < 0 || rowIndex >= this.rows.Count)
			{
				throw new ArgumentOutOfRangeException("Row index is out of range.");
			}
			if (!this.is_binding)
			{
				base.Invalidate(this.GetRowDisplayRectangle(rowIndex, true));
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.DataGridView" /> that the current cell has uncommitted changes.</summary>
		/// <param name="dirty">true to indicate the cell has uncommitted changes; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		public virtual void NotifyCurrentCellDirty(bool dirty)
		{
			if (this.currentCell != null)
			{
				this.InvalidateCell(this.currentCell);
			}
		}

		/// <summary>Refreshes the value of the current cell with the underlying cell value when the cell is in edit mode, discarding any previous value.</summary>
		/// <returns>true if successful; false if a <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event occurred.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		public bool RefreshEdit()
		{
			if (this.IsCurrentCellInEditMode)
			{
				this.currentCell.InitializeEditingControl(this.currentCell.RowIndex, this.currentCell.FormattedValue, this.currentCell.InheritedStyle);
				return true;
			}
			return false;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridView.Text" /> property to its default value.</summary>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003D404 File Offset: 0x0003B604
		[EditorBrowsable(1)]
		public override void ResetText()
		{
			this.Text = string.Empty;
		}

		/// <summary>Selects all the cells in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003D414 File Offset: 0x0003B614
		public void SelectAll()
		{
			DataGridViewSelectionMode dataGridViewSelectionMode = this.selectionMode;
			if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullRowSelect)
			{
				if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullColumnSelect)
				{
					foreach (object obj in this.rows)
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
						foreach (object obj2 in dataGridViewRow.Cells)
						{
							DataGridViewCell dataGridViewCell = (DataGridViewCell)obj2;
							dataGridViewCell.Selected = true;
						}
					}
				}
				else
				{
					foreach (object obj3 in this.columns)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj3;
						dataGridViewColumn.Selected = true;
					}
				}
			}
			else
			{
				foreach (object obj4 in this.rows)
				{
					DataGridViewRow dataGridViewRow2 = (DataGridViewRow)obj4;
					dataGridViewRow2.Selected = true;
				}
			}
			base.Invalidate();
		}

		/// <summary>Sorts the contents of the <see cref="T:System.Windows.Forms.DataGridView" /> control using an implementation of the <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">An implementation of <see cref="T:System.Collections.IComparer" /> that performs the custom sorting operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> is set to true.-or- <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> is not null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FEA RID: 4074 RVA: 0x0003D5E0 File Offset: 0x0003B7E0
		public virtual void Sort(IComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (this.VirtualMode || this.DataSource != null)
			{
				throw new InvalidOperationException();
			}
			if (this.SortedColumn != null)
			{
				this.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
			}
			this.EndEdit();
			this.Rows.Sort(comparer);
			this.sortedColumn = null;
			this.sortOrder = SortOrder.None;
			this.currentCell = null;
			base.Invalidate();
			this.OnSorted(EventArgs.Empty);
		}

		/// <summary>Sorts the contents of the <see cref="T:System.Windows.Forms.DataGridView" /> control in ascending or descending order based on the contents of the specified column.</summary>
		/// <param name="dataGridViewColumn">The column by which to sort the contents of the <see cref="T:System.Windows.Forms.DataGridView" />. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The specified column is not part of this <see cref="T:System.Windows.Forms.DataGridView" />.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property has been set and the <see cref="P:System.Windows.Forms.DataGridViewColumn.IsDataBound" /> property of the specified column returns false.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property is set to true and the <see cref="P:System.Windows.Forms.DataGridViewColumn.IsDataBound" /> property of the specified column returns false.-or-The object specified by the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property does not implement the <see cref="T:System.ComponentModel.IBindingList" /> interface.-or-The object specified by the <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property has a <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> property value of false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FEB RID: 4075 RVA: 0x0003D670 File Offset: 0x0003B870
		public virtual void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
		{
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			if (dataGridViewColumn.DataGridView != this)
			{
				throw new ArgumentException("dataGridViewColumn");
			}
			if (!this.EndEdit())
			{
				return;
			}
			if (this.SortedColumn != null)
			{
				this.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
			}
			this.sortedColumn = dataGridViewColumn;
			this.sortOrder = ((direction != null) ? SortOrder.Descending : SortOrder.Ascending);
			if (this.Rows.Count == 0)
			{
				return;
			}
			if (dataGridViewColumn.IsDataBound)
			{
				IBindingList bindingList = this.DataManager.List as IBindingList;
				if (bindingList != null && bindingList.SupportsSorting)
				{
					bindingList.ApplySort(this.DataManager.GetItemProperties()[dataGridViewColumn.DataPropertyName], direction);
					dataGridViewColumn.HeaderCell.SortGlyphDirection = this.sortOrder;
				}
			}
			else
			{
				bool flag = true;
				foreach (object obj in this.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					object value = dataGridViewRow.Cells[dataGridViewColumn.Index].Value;
					double num;
					if (value != null && !double.TryParse(value.ToString(), ref num))
					{
						flag = false;
						break;
					}
				}
				DataGridView.ColumnSorter columnSorter = new DataGridView.ColumnSorter(dataGridViewColumn, direction, flag);
				this.Rows.Sort(columnSorter);
				dataGridViewColumn.HeaderCell.SortGlyphDirection = this.sortOrder;
			}
			base.Invalidate();
			this.OnSorted(EventArgs.Empty);
		}

		/// <summary>Forces the cell at the specified location to update its error text.</summary>
		/// <param name="columnIndex">The column index of the cell to update, or -1 to indicate a row header cell.</param>
		/// <param name="rowIndex">The row index of the cell to update, or -1 to indicate a column header cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex" /> is less than -1 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x06000FEC RID: 4076 RVA: 0x0003D830 File Offset: 0x0003BA30
		public void UpdateCellErrorText(int columnIndex, int rowIndex)
		{
			if (columnIndex < 0 || columnIndex > this.Columns.Count - 1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < 0 || rowIndex > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.InvalidateCell(columnIndex, rowIndex);
		}

		/// <summary>Forces the control to update its display of the cell at the specified location based on its new value, applying any automatic sizing modes currently in effect. </summary>
		/// <param name="columnIndex">The zero-based column index of the cell with the new value.</param>
		/// <param name="rowIndex">The zero-based row index of the cell with the new value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than zero or greater than the number of columns in the control minus one.-or-<paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x06000FED RID: 4077 RVA: 0x0003D890 File Offset: 0x0003BA90
		public void UpdateCellValue(int columnIndex, int rowIndex)
		{
			if (columnIndex < 0 || columnIndex > this.Columns.Count - 1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < 0 || rowIndex > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.InvalidateCell(columnIndex, rowIndex);
		}

		/// <summary>Forces the row at the given row index to update its error text.</summary>
		/// <param name="rowIndex">The zero-based index of the row to update.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
		// Token: 0x06000FEE RID: 4078 RVA: 0x0003D8F0 File Offset: 0x0003BAF0
		public void UpdateRowErrorText(int rowIndex)
		{
			if (rowIndex < 0 || rowIndex > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.InvalidateRow(rowIndex);
		}

		/// <summary>Forces the rows in the given range to update their error text.</summary>
		/// <param name="rowIndexStart">The zero-based index of the first row in the set of rows to update.</param>
		/// <param name="rowIndexEnd">The zero-based index of the last row in the set of rows to update.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndexStart" /> is not in the valid range of 0 to the number of rows in the control minus 1.-or-<paramref name="rowIndexEnd" /> is not in the valid range of 0 to the number of rows in the control minus 1.-or-<paramref name="rowIndexEnd" /> is less than <paramref name="rowIndexStart" />.</exception>
		// Token: 0x06000FEF RID: 4079 RVA: 0x0003D92C File Offset: 0x0003BB2C
		public void UpdateRowErrorText(int rowIndexStart, int rowIndexEnd)
		{
			if (rowIndexStart < 0 || rowIndexStart > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndexStart");
			}
			if (rowIndexEnd < 0 || rowIndexEnd > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndexEnd");
			}
			if (rowIndexEnd < rowIndexStart)
			{
				throw new ArgumentOutOfRangeException("rowIndexEnd", "rowIndexEnd must be greater than rowIndexStart");
			}
			for (int i = rowIndexStart; i <= rowIndexEnd; i++)
			{
				this.InvalidateRow(i);
			}
		}

		/// <summary>Forces the specified row or rows to update their height information.</summary>
		/// <param name="rowIndex">The zero-based index of the first row to update.</param>
		/// <param name="updateToEnd">true to update the specified row and all subsequent rows.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 and <paramref name="updateToEnd" /> is true.-or-<paramref name="rowIndex" /> is less than -1 and <paramref name="updateToEnd" /> is false.-or-<paramref name="rowIndex" /> is greater than the highest row index in the <see cref="P:System.Windows.Forms.DataGridView.Rows" /> collection.</exception>
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
		public void UpdateRowHeightInfo(int rowIndex, bool updateToEnd)
		{
			if (rowIndex < 0 && updateToEnd)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (rowIndex < -1 && !updateToEnd)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (rowIndex >= this.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (!this.VirtualMode && this.DataManager == null)
			{
				return;
			}
			if (rowIndex == -1)
			{
				updateToEnd = true;
				rowIndex = 0;
			}
			if (updateToEnd)
			{
				for (int i = rowIndex; i < this.Rows.Count; i++)
				{
					DataGridViewRow dataGridViewRow = this.Rows[i];
					if (dataGridViewRow.Visible)
					{
						DataGridViewRowHeightInfoNeededEventArgs dataGridViewRowHeightInfoNeededEventArgs = new DataGridViewRowHeightInfoNeededEventArgs(dataGridViewRow.Index, dataGridViewRow.Height, dataGridViewRow.MinimumHeight);
						this.OnRowHeightInfoNeeded(dataGridViewRowHeightInfoNeededEventArgs);
						if (dataGridViewRow.Height != dataGridViewRowHeightInfoNeededEventArgs.Height || dataGridViewRow.MinimumHeight != dataGridViewRowHeightInfoNeededEventArgs.MinimumHeight)
						{
							dataGridViewRow.Height = dataGridViewRowHeightInfoNeededEventArgs.Height;
							dataGridViewRow.MinimumHeight = dataGridViewRowHeightInfoNeededEventArgs.MinimumHeight;
							this.OnRowHeightInfoPushed(new DataGridViewRowHeightInfoPushedEventArgs(dataGridViewRow.Index, dataGridViewRowHeightInfoNeededEventArgs.Height, dataGridViewRowHeightInfoNeededEventArgs.MinimumHeight));
						}
					}
				}
			}
			else
			{
				DataGridViewRow dataGridViewRow2 = this.Rows[rowIndex];
				DataGridViewRowHeightInfoNeededEventArgs dataGridViewRowHeightInfoNeededEventArgs2 = new DataGridViewRowHeightInfoNeededEventArgs(dataGridViewRow2.Index, dataGridViewRow2.Height, dataGridViewRow2.MinimumHeight);
				this.OnRowHeightInfoNeeded(dataGridViewRowHeightInfoNeededEventArgs2);
				if (dataGridViewRow2.Height != dataGridViewRowHeightInfoNeededEventArgs2.Height || dataGridViewRow2.MinimumHeight != dataGridViewRowHeightInfoNeededEventArgs2.MinimumHeight)
				{
					dataGridViewRow2.Height = dataGridViewRowHeightInfoNeededEventArgs2.Height;
					dataGridViewRow2.MinimumHeight = dataGridViewRowHeightInfoNeededEventArgs2.MinimumHeight;
					this.OnRowHeightInfoPushed(new DataGridViewRowHeightInfoPushedEventArgs(dataGridViewRow2.Index, dataGridViewRowHeightInfoNeededEventArgs2.Height, dataGridViewRowHeightInfoNeededEventArgs2.MinimumHeight));
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>true if there is an editable cell selected; otherwise, false.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0003DB78 File Offset: 0x0003BD78
		protected override bool CanEnableIme
		{
			get
			{
				return this.CurrentCell != null && this.CurrentCell.EditType != null;
			}
		}

		/// <summary>Gets the default initial size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the initial size of the control, which is 240 pixels wide by 150 pixels high.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0003DB98 File Offset: 0x0003BD98
		protected override Size DefaultSize
		{
			get
			{
				return new Size(240, 150);
			}
		}

		/// <summary>Gets the horizontal scroll bar of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollBar" /> representing the horizontal scroll bar.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0003DBAC File Offset: 0x0003BDAC
		protected ScrollBar HorizontalScrollBar
		{
			get
			{
				return this.horizontalScrollBar;
			}
		}

		/// <summary>Gets the vertical scroll bar of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollBar" /> representing the vertical scroll bar.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		protected ScrollBar VerticalScrollBar
		{
			get
			{
				return this.verticalScrollBar;
			}
		}

		/// <summary>Notifies the accessible client applications when a new cell becomes the current cell. </summary>
		/// <param name="cellAddress">A <see cref="T:System.Drawing.Point" /> indicating the row and column indexes of the new current cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Drawing.Point.X" /> property of <paramref name="cellAddress" /> is less than 0 or greater than the number of columns in the control minus 1. -or-The value of the <see cref="P:System.Drawing.Point.Y" /> property of <paramref name="cellAddress" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		protected virtual void AccessibilityNotifyCurrentCellChanged(Point cellAddress)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adjusts the width of the specified column using the specified size mode, optionally calculating the width with the expectation that row heights will subsequently be adjusted. </summary>
		/// <param name="columnIndex">The index of the column to resize. </param>
		/// <param name="autoSizeColumnMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values. </param>
		/// <param name="fixedHeight">true to calculate the new width based on the current row heights; false to calculate the width with the expectation that the row heights will also be adjusted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeColumnMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None" />, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value.</exception>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		[MonoTODO("Does not use fixedHeight parameter")]
		protected void AutoResizeColumn(int columnIndex, DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
		{
			this.AutoResizeColumn(columnIndex, autoSizeColumnMode);
		}

		/// <summary>Adjusts the height of the column headers to fit their contents, optionally calculating the height with the expectation that the column and/or row header widths will subsequently be adjusted.</summary>
		/// <param name="fixedRowHeadersWidth">true to calculate the new height based on the current width of the row headers; false to calculate the height with the expectation that the row headers width will also be adjusted. </param>
		/// <param name="fixedColumnsWidth">true to calculate the new height based on the current column widths; false to calculate the height with the expectation that the column widths will also be adjusted.</param>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		[MonoTODO("Does not use fixedRowHeadersWidth or fixedColumnsWidth parameters")]
		protected void AutoResizeColumnHeadersHeight(bool fixedRowHeadersWidth, bool fixedColumnsWidth)
		{
			this.AutoResizeColumnHeadersHeight();
		}

		/// <summary>Adjusts the height of the column headers based on changes to the contents of the header in the specified column, optionally calculating the height with the expectation that the column and/or row header widths will subsequently be adjusted.</summary>
		/// <param name="columnIndex">The index of the column header whose contents should be used to determine new height.</param>
		/// <param name="fixedRowHeadersWidth">true to calculate the new height based on the current width of the row headers; false to calculate the height with the expectation that the row headers width will also be adjusted.</param>
		/// <param name="fixedColumnWidth">true to calculate the new height based on the current width of the specified column; false to calculate the height with the expectation that the column width will also be adjusted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is not in the valid range of 0 to the number of columns minus 1. </exception>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		[MonoTODO("Does not use columnIndex or fixedRowHeadersWidth or fixedColumnsWidth parameters")]
		protected void AutoResizeColumnHeadersHeight(int columnIndex, bool fixedRowHeadersWidth, bool fixedColumnWidth)
		{
			this.AutoResizeColumnHeadersHeight(columnIndex);
		}

		/// <summary>Adjusts the width of all columns using the specified size mode, optionally calculating the widths with the expectation that row heights will subsequently be adjusted. </summary>
		/// <param name="autoSizeColumnsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> values. </param>
		/// <param name="fixedHeight">true to calculate the new widths based on the current row heights; false to calculate the widths with the expectation that the row heights will also be adjusted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeColumnsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeColumnsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill" />. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeColumnsMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode" /> value.</exception>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		protected void AutoResizeColumns(DataGridViewAutoSizeColumnsMode autoSizeColumnsMode, bool fixedHeight)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				this.AutoResizeColumn(i, (DataGridViewAutoSizeColumnMode)autoSizeColumnsMode, fixedHeight);
			}
		}

		/// <summary>Adjusts the height of the specified row using the specified size mode, optionally calculating the height with the expectation that column widths will subsequently be adjusted. </summary>
		/// <param name="rowIndex">The index of the row to resize. </param>
		/// <param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> values. </param>
		/// <param name="fixedWidth">true to calculate the new height based on the current width of the columns; false to calculate the height with the expectation that the column widths will also be adjusted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeRowMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowMode.RowHeader" /> and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1.</exception>
		// Token: 0x06000FFA RID: 4090 RVA: 0x0003DC18 File Offset: 0x0003BE18
		[MonoTODO("Does not use fixedWidth parameter")]
		protected void AutoResizeRow(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
		{
			if (autoSizeRowMode == DataGridViewAutoSizeRowMode.RowHeader && !this.rowHeadersVisible)
			{
				throw new InvalidOperationException("row headers are not visible");
			}
			if (rowIndex < 0 || rowIndex > this.Rows.Count - 1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewRow rowInternal = this.GetRowInternal(rowIndex);
			int preferredHeight = rowInternal.GetPreferredHeight(rowIndex, autoSizeRowMode, true);
			if (rowInternal.Height != preferredHeight)
			{
				rowInternal.SetAutoSizeHeight(preferredHeight);
			}
		}

		/// <summary>Adjusts the width of the row headers using the specified size mode, optionally calculating the width with the expectation that the row and/or column header widths will subsequently be adjusted.</summary>
		/// <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> values.</param>
		/// <param name="fixedColumnHeadersHeight">true to calculate the new width based on the current height of the column headers; false to calculate the width with the expectation that the height of the column headers will also be adjusted.</param>
		/// <param name="fixedRowsHeight">true to calculate the new width based on the current row heights; false to calculate the width with the expectation that the row heights will also be adjusted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value. </exception>
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003DC8C File Offset: 0x0003BE8C
		[MonoTODO("Does not use fixedColumnHeadersHeight or fixedRowsHeight parameter")]
		protected void AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode, bool fixedColumnHeadersHeight, bool fixedRowsHeight)
		{
			this.AutoResizeRowHeadersWidth(rowHeadersWidthSizeMode);
		}

		/// <summary>Adjusts the width of the row headers based on changes to the contents of the header in the specified row and using the specified size mode, optionally calculating the width with the expectation that the row and/or column header widths will subsequently be adjusted.</summary>
		/// <param name="rowIndex">The index of the row containing the header with the changed content.</param>
		/// <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> values.</param>
		/// <param name="fixedColumnHeadersHeight">true to calculate the new width based on the current height of the column headers; false to calculate the width with the expectation that the height of the column headers will also be adjusted.</param>
		/// <param name="fixedRowHeight">true to calculate the new width based on the current height of the specified row; false to calculate the width with the expectation that the row height will also be adjusted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows minus 1. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="rowHeadersWidthSizeMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value. </exception>
		// Token: 0x06000FFC RID: 4092 RVA: 0x0003DC98 File Offset: 0x0003BE98
		[MonoTODO("Does not use rowIndex or fixedColumnHeadersHeight or fixedRowsHeight parameter")]
		protected void AutoResizeRowHeadersWidth(int rowIndex, DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode, bool fixedColumnHeadersHeight, bool fixedRowHeight)
		{
			this.AutoResizeRowHeadersWidth(rowHeadersWidthSizeMode);
		}

		/// <summary>Adjusts the heights of all rows using the specified size mode, optionally calculating the heights with the expectation that column widths will subsequently be adjusted. </summary>
		/// <param name="autoSizeRowsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> values.</param>
		/// <param name="fixedWidth">true to calculate the new heights based on the current column widths; false to calculate the heights with the expectation that the column widths will also be adjusted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders" />, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowsMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" />.</exception>
		// Token: 0x06000FFD RID: 4093 RVA: 0x0003DCA4 File Offset: 0x0003BEA4
		[MonoTODO("Does not use fixedWidth parameter")]
		protected void AutoResizeRows(DataGridViewAutoSizeRowsMode autoSizeRowsMode, bool fixedWidth)
		{
			if (autoSizeRowsMode == DataGridViewAutoSizeRowsMode.None)
			{
				return;
			}
			bool flag = false;
			DataGridViewAutoSizeRowMode dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.AllCells;
			switch (autoSizeRowsMode)
			{
			case DataGridViewAutoSizeRowsMode.AllHeaders:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.RowHeader;
				break;
			case DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.AllCellsExceptHeader;
				break;
			case DataGridViewAutoSizeRowsMode.AllCells:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.AllCells;
				break;
			case DataGridViewAutoSizeRowsMode.DisplayedHeaders:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.RowHeader;
				flag = true;
				break;
			case DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.AllCellsExceptHeader;
				flag = true;
				break;
			case DataGridViewAutoSizeRowsMode.DisplayedCells:
				dataGridViewAutoSizeRowMode = DataGridViewAutoSizeRowMode.AllCells;
				flag = true;
				break;
			}
			foreach (object obj in this.Rows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if (dataGridViewRow.Visible)
				{
					if (!flag || dataGridViewRow.Displayed)
					{
						int preferredHeight = dataGridViewRow.GetPreferredHeight(dataGridViewRow.Index, dataGridViewAutoSizeRowMode, fixedWidth);
						if (dataGridViewRow.Height != preferredHeight)
						{
							dataGridViewRow.SetAutoSizeHeight(preferredHeight);
						}
					}
				}
			}
		}

		/// <summary>Adjusts the heights of the specified rows using the specified size mode, optionally calculating the heights with the expectation that column widths will subsequently be adjusted. </summary>
		/// <param name="rowIndexStart">The index of the first row to resize. </param>
		/// <param name="rowsCount">The number of rows to resize. </param>
		/// <param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> values. </param>
		/// <param name="fixedWidth">true to calculate the new heights based on the current column widths; false to calculate the heights with the expectation that the column widths will also be adjusted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders" /> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders" />, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> is false. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowsMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="autoSizeRowsMode" /> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndexStart" /> is less than 0.-or-<paramref name="rowsCount" /> is less than 0.</exception>
		// Token: 0x06000FFE RID: 4094 RVA: 0x0003DDC0 File Offset: 0x0003BFC0
		[MonoTODO("Does not use fixedMode parameter")]
		protected void AutoResizeRows(int rowIndexStart, int rowsCount, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
		{
			for (int i = rowIndexStart; i < rowIndexStart + rowsCount; i++)
			{
				this.AutoResizeRow(i, autoSizeRowMode, fixedWidth);
			}
		}

		/// <summary>Cancels the selection of all currently selected cells except the one indicated, optionally ensuring that the indicated cell is selected. </summary>
		/// <param name="columnIndexException">The column index to exclude.</param>
		/// <param name="rowIndexException">The row index to exclude.</param>
		/// <param name="selectExceptionElement">true to select the excluded cell, row, or column; false to retain its original state.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndexException" /> is greater than the highest column index.-or-<paramref name="columnIndexException" /> is less than -1 when <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect" />; otherwise, <paramref name="columnIndexException" /> is less than 0.-or- <paramref name="rowIndexException" /> is greater than the highest row index.-or-<paramref name="rowIndexException" /> is less than -1 when <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" />; otherwise, <paramref name="rowIndexException" /> is less than 0.</exception>
		// Token: 0x06000FFF RID: 4095 RVA: 0x0003DDEC File Offset: 0x0003BFEC
		protected void ClearSelection(int columnIndexException, int rowIndexException, bool selectExceptionElement)
		{
			if (columnIndexException >= this.columns.Count)
			{
				throw new ArgumentOutOfRangeException("ColumnIndexException is greater than the highest column index.");
			}
			if (this.selectionMode == DataGridViewSelectionMode.FullRowSelect)
			{
				if (columnIndexException < -1)
				{
					throw new ArgumentOutOfRangeException("ColumnIndexException is less than -1.");
				}
			}
			else if (columnIndexException < 0)
			{
				throw new ArgumentOutOfRangeException("ColumnIndexException is less than 0.");
			}
			if (rowIndexException >= this.rows.Count)
			{
				throw new ArgumentOutOfRangeException("RowIndexException is greater than the highest row index.");
			}
			if (this.selectionMode == DataGridViewSelectionMode.FullColumnSelect)
			{
				if (rowIndexException < -1)
				{
					throw new ArgumentOutOfRangeException("RowIndexException is less than -1.");
				}
			}
			else if (rowIndexException < 0)
			{
				throw new ArgumentOutOfRangeException("RowIndexException is less than 0.");
			}
			DataGridViewSelectionMode dataGridViewSelectionMode = this.selectionMode;
			if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullRowSelect)
			{
				if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullColumnSelect)
				{
					foreach (object obj in this.SelectedCells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
						if (!selectExceptionElement || dataGridViewCell.RowIndex != rowIndexException || dataGridViewCell.ColumnIndex != columnIndexException)
						{
							this.SetSelectedCellCore(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, false);
						}
					}
				}
				else
				{
					foreach (object obj2 in this.columns)
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj2;
						if (!selectExceptionElement || dataGridViewColumn.Index != columnIndexException)
						{
							this.SetSelectedColumnCore(dataGridViewColumn.Index, false);
						}
					}
				}
			}
			else
			{
				foreach (object obj3 in this.rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj3;
					if (!selectExceptionElement || dataGridViewRow.Index != rowIndexException)
					{
						this.SetSelectedRowCore(dataGridViewRow.Index, false);
					}
				}
			}
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x06001000 RID: 4096 RVA: 0x0003E060 File Offset: 0x0003C260
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridView.DataGridViewAccessibleObject(this);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />.</summary>
		/// <returns>An empty <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />.</returns>
		// Token: 0x06001001 RID: 4097 RVA: 0x0003E068 File Offset: 0x0003C268
		[EditorBrowsable(2)]
		protected virtual DataGridViewColumnCollection CreateColumnsInstance()
		{
			return new DataGridViewColumnCollection(this);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Windows.Forms.Control.ControlCollection" /> that can be cast to type <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</summary>
		/// <returns>An empty <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		// Token: 0x06001002 RID: 4098 RVA: 0x0003E070 File Offset: 0x0003C270
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new DataGridView.DataGridViewControlCollection(this);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</summary>
		/// <returns>An empty <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
		// Token: 0x06001003 RID: 4099 RVA: 0x0003E078 File Offset: 0x0003C278
		[EditorBrowsable(2)]
		protected virtual DataGridViewRowCollection CreateRowsInstance()
		{
			return new DataGridViewRowCollection(this);
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001004 RID: 4100 RVA: 0x0003E080 File Offset: 0x0003C280
		protected override void Dispose(bool disposing)
		{
		}

		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" />.</returns>
		/// <param name="objectId">An Int32 that identifies the <see cref="T:System.Windows.Forms.AccessibleObject" /> to retrieve.</param>
		// Token: 0x06001005 RID: 4101 RVA: 0x0003E084 File Offset: 0x0003C284
		protected override AccessibleObject GetAccessibilityObjectById(int objectId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether a character is an input character that the <see cref="T:System.Windows.Forms.DataGridView" /> recognizes.</summary>
		/// <returns>true if the character is recognized as an input character; otherwise, false.</returns>
		/// <param name="charCode">The character to test.</param>
		// Token: 0x06001006 RID: 4102 RVA: 0x0003E08C File Offset: 0x0003C28C
		protected override bool IsInputChar(char charCode)
		{
			return true;
		}

		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x06001007 RID: 4103 RVA: 0x0003E090 File Offset: 0x0003C290
		protected override bool IsInputKey(Keys keyData)
		{
			keyData &= Keys.KeyCode;
			Keys keys = keyData;
			switch (keys)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
			case Keys.Delete:
			case Keys.D0:
				break;
			default:
				if (keys != Keys.Return && keys != Keys.NumPad0 && keys != Keys.F2)
				{
					return false;
				}
				break;
			}
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToAddRowsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001008 RID: 4104 RVA: 0x0003E114 File Offset: 0x0003C314
		protected virtual void OnAllowUserToAddRowsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AllowUserToAddRowsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToDeleteRowsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001009 RID: 4105 RVA: 0x0003E148 File Offset: 0x0003C348
		protected virtual void OnAllowUserToDeleteRowsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AllowUserToDeleteRowsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToOrderColumnsChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600100A RID: 4106 RVA: 0x0003E17C File Offset: 0x0003C37C
		protected virtual void OnAllowUserToOrderColumnsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AllowUserToOrderColumnsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToResizeColumnsChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600100B RID: 4107 RVA: 0x0003E1B0 File Offset: 0x0003C3B0
		protected virtual void OnAllowUserToResizeColumnsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AllowUserToResizeColumnsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToResizeRowsChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600100C RID: 4108 RVA: 0x0003E1E4 File Offset: 0x0003C3E4
		protected virtual void OnAllowUserToResizeRowsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AllowUserToResizeRowsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600100D RID: 4109 RVA: 0x0003E218 File Offset: 0x0003C418
		protected virtual void OnAlternatingRowsDefaultCellStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AlternatingRowsDefaultCellStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoGenerateColumnsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600100E RID: 4110 RVA: 0x0003E24C File Offset: 0x0003C44C
		protected virtual void OnAutoGenerateColumnsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.AutoGenerateColumnsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column" /> property of <paramref name="e" /> is null.</exception>
		// Token: 0x0600100F RID: 4111 RVA: 0x0003E280 File Offset: 0x0003C480
		protected internal virtual void OnAutoSizeColumnModeChanged(DataGridViewAutoSizeColumnModeEventArgs e)
		{
			DataGridViewAutoSizeColumnModeEventHandler dataGridViewAutoSizeColumnModeEventHandler = (DataGridViewAutoSizeColumnModeEventHandler)base.Events[DataGridView.AutoSizeColumnModeChangedEvent];
			if (dataGridViewAutoSizeColumnModeEventHandler != null)
			{
				dataGridViewAutoSizeColumnModeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs.PreviousModes" /> property of <paramref name="e" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of entries in the array returned by the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs.PreviousModes" /> property of <paramref name="e" /> is not equal to the number of columns in the control.</exception>
		// Token: 0x06001010 RID: 4112 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
		protected virtual void OnAutoSizeColumnsModeChanged(DataGridViewAutoSizeColumnsModeEventArgs e)
		{
			DataGridViewAutoSizeColumnsModeEventHandler dataGridViewAutoSizeColumnsModeEventHandler = (DataGridViewAutoSizeColumnsModeEventHandler)base.Events[DataGridView.AutoSizeColumnsModeChangedEvent];
			if (dataGridViewAutoSizeColumnsModeEventHandler != null)
			{
				dataGridViewAutoSizeColumnsModeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06001011 RID: 4113 RVA: 0x0003E2E8 File Offset: 0x0003C4E8
		protected virtual void OnAutoSizeRowsModeChanged(DataGridViewAutoSizeModeEventArgs e)
		{
			DataGridViewAutoSizeModeEventHandler dataGridViewAutoSizeModeEventHandler = (DataGridViewAutoSizeModeEventHandler)base.Events[DataGridView.AutoSizeRowsModeChangedEvent];
			if (dataGridViewAutoSizeModeEventHandler != null)
			{
				dataGridViewAutoSizeModeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.BackgroundColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001012 RID: 4114 RVA: 0x0003E31C File Offset: 0x0003C51C
		protected virtual void OnBackgroundColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.BackgroundColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001013 RID: 4115 RVA: 0x0003E350 File Offset: 0x0003C550
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			this.ReBind();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001014 RID: 4116 RVA: 0x0003E360 File Offset: 0x0003C560
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.BorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CancelRowEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.QuestionEventArgs" /> that contains the event data. </param>
		// Token: 0x06001015 RID: 4117 RVA: 0x0003E394 File Offset: 0x0003C594
		protected virtual void OnCancelRowEdit(QuestionEventArgs e)
		{
			QuestionEventHandler questionEventHandler = (QuestionEventHandler)base.Events[DataGridView.CancelRowEditEvent];
			if (questionEventHandler != null)
			{
				questionEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellCancelEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellCancelEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001016 RID: 4118 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		protected virtual void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
		{
			DataGridViewCellCancelEventHandler dataGridViewCellCancelEventHandler = (DataGridViewCellCancelEventHandler)base.Events[DataGridView.CellBeginEditEvent];
			if (dataGridViewCellCancelEventHandler != null)
			{
				dataGridViewCellCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellBorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001017 RID: 4119 RVA: 0x0003E3FC File Offset: 0x0003C5FC
		protected virtual void OnCellBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.CellBorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001018 RID: 4120 RVA: 0x0003E430 File Offset: 0x0003C630
		protected virtual void OnCellClick(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnClickInternal(e);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellClickEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains information regarding the cell whose content was clicked.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001019 RID: 4121 RVA: 0x0003E47C File Offset: 0x0003C67C
		protected virtual void OnCellContentClick(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnContentClickInternal(e);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellContentClickEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101A RID: 4122 RVA: 0x0003E4C8 File Offset: 0x0003C6C8
		protected virtual void OnCellContentDoubleClick(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnContentDoubleClickInternal(e);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellContentDoubleClickEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101B RID: 4123 RVA: 0x0003E514 File Offset: 0x0003C714
		protected virtual void OnCellContextMenuStripChanged(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellContextMenuStripChangedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101C RID: 4124 RVA: 0x0003E548 File Offset: 0x0003C748
		protected virtual void OnCellContextMenuStripNeeded(DataGridViewCellContextMenuStripNeededEventArgs e)
		{
			DataGridViewCellContextMenuStripNeededEventHandler dataGridViewCellContextMenuStripNeededEventHandler = (DataGridViewCellContextMenuStripNeededEventHandler)base.Events[DataGridView.CellContextMenuStripNeededEvent];
			if (dataGridViewCellContextMenuStripNeededEventHandler != null)
			{
				dataGridViewCellContextMenuStripNeededEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101D RID: 4125 RVA: 0x0003E57C File Offset: 0x0003C77C
		protected virtual void OnCellDoubleClick(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnDoubleClickInternal(e);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellDoubleClickEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellEndEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101E RID: 4126 RVA: 0x0003E5C8 File Offset: 0x0003C7C8
		protected virtual void OnCellEndEdit(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellEndEditEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellEnter" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600101F RID: 4127 RVA: 0x0003E5FC File Offset: 0x0003C7FC
		protected virtual void OnCellEnter(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnEnterInternal(e.RowIndex, true);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellEnterEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is less than -1 or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is less than -1 or greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001020 RID: 4128 RVA: 0x0003E650 File Offset: 0x0003C850
		protected internal virtual void OnCellErrorTextChanged(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellErrorTextChangedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001021 RID: 4129 RVA: 0x0003E684 File Offset: 0x0003C884
		protected virtual void OnCellErrorTextNeeded(DataGridViewCellErrorTextNeededEventArgs e)
		{
			DataGridViewCellErrorTextNeededEventHandler dataGridViewCellErrorTextNeededEventHandler = (DataGridViewCellErrorTextNeededEventHandler)base.Events[DataGridView.CellErrorTextNeededEvent];
			if (dataGridViewCellErrorTextNeededEventHandler != null)
			{
				dataGridViewCellErrorTextNeededEventHandler(this, e);
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003E6B8 File Offset: 0x0003C8B8
		internal void OnCellFormattingInternal(DataGridViewCellFormattingEventArgs e)
		{
			this.OnCellFormatting(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellFormattingEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellFormattingEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001023 RID: 4131 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
		protected virtual void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
		{
			DataGridViewCellFormattingEventHandler dataGridViewCellFormattingEventHandler = (DataGridViewCellFormattingEventHandler)base.Events[DataGridView.CellFormattingEvent];
			if (dataGridViewCellFormattingEventHandler != null)
			{
				dataGridViewCellFormattingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001024 RID: 4132 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		protected virtual void OnCellLeave(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnLeaveInternal(e.RowIndex, true);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellLeaveEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001025 RID: 4133 RVA: 0x0003E74C File Offset: 0x0003C94C
		protected virtual void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseClickInternal(e);
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.CellMouseClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001026 RID: 4134 RVA: 0x0003E798 File Offset: 0x0003C998
		protected virtual void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseDoubleClickInternal(e);
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.CellMouseDoubleClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x06001027 RID: 4135 RVA: 0x0003E7E4 File Offset: 0x0003C9E4
		protected virtual void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseDownInternal(e);
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.CellMouseDownEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseEnter" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001028 RID: 4136 RVA: 0x0003E830 File Offset: 0x0003CA30
		protected virtual void OnCellMouseEnter(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseEnterInternal(e.RowIndex);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellMouseEnterEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001029 RID: 4137 RVA: 0x0003E880 File Offset: 0x0003CA80
		protected virtual void OnCellMouseLeave(DataGridViewCellEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseLeaveInternal(e.RowIndex);
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellMouseLeaveEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600102A RID: 4138 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		protected virtual void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseMoveInternal(e);
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.CellMouseMoveEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600102B RID: 4139 RVA: 0x0003E91C File Offset: 0x0003CB1C
		protected virtual void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCell cellInternal = this.GetCellInternal(e.ColumnIndex, e.RowIndex);
			cellInternal.OnMouseUpInternal(e);
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.CellMouseUpEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003E968 File Offset: 0x0003CB68
		internal void OnCellPaintingInternal(DataGridViewCellPaintingEventArgs e)
		{
			this.OnCellPainting(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellPainting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600102D RID: 4141 RVA: 0x0003E974 File Offset: 0x0003CB74
		protected virtual void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			DataGridViewCellPaintingEventHandler dataGridViewCellPaintingEventHandler = (DataGridViewCellPaintingEventHandler)base.Events[DataGridView.CellPaintingEvent];
			if (dataGridViewCellPaintingEventHandler != null)
			{
				dataGridViewCellPaintingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellParsing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellParsingEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellParsingEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x0600102E RID: 4142 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		protected internal virtual void OnCellParsing(DataGridViewCellParsingEventArgs e)
		{
			DataGridViewCellParsingEventHandler dataGridViewCellParsingEventHandler = (DataGridViewCellParsingEventHandler)base.Events[DataGridView.CellParsingEvent];
			if (dataGridViewCellParsingEventHandler != null)
			{
				dataGridViewCellParsingEventHandler(this, e);
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003E9DC File Offset: 0x0003CBDC
		internal void OnCellStateChangedInternal(DataGridViewCellStateChangedEventArgs e)
		{
			this.OnCellStateChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001030 RID: 4144 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		protected virtual void OnCellStateChanged(DataGridViewCellStateChangedEventArgs e)
		{
			DataGridViewCellStateChangedEventHandler dataGridViewCellStateChangedEventHandler = (DataGridViewCellStateChangedEventHandler)base.Events[DataGridView.CellStateChangedEvent];
			if (dataGridViewCellStateChangedEventHandler != null)
			{
				dataGridViewCellStateChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStyleChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001031 RID: 4145 RVA: 0x0003EA1C File Offset: 0x0003CC1C
		protected virtual void OnCellStyleChanged(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellStyleChangedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStyleContentChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellStyleContentChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001032 RID: 4146 RVA: 0x0003EA50 File Offset: 0x0003CC50
		protected virtual void OnCellStyleContentChanged(DataGridViewCellStyleContentChangedEventArgs e)
		{
			DataGridViewCellStyleContentChangedEventHandler dataGridViewCellStyleContentChangedEventHandler = (DataGridViewCellStyleContentChangedEventHandler)base.Events[DataGridView.CellStyleContentChangedEvent];
			if (dataGridViewCellStyleContentChangedEventHandler != null)
			{
				dataGridViewCellStyleContentChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains information about the cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001033 RID: 4147 RVA: 0x0003EA84 File Offset: 0x0003CC84
		protected virtual void OnCellToolTipTextChanged(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellToolTipTextChangedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001034 RID: 4148 RVA: 0x0003EAB8 File Offset: 0x0003CCB8
		protected virtual void OnCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
		{
			DataGridViewCellToolTipTextNeededEventHandler dataGridViewCellToolTipTextNeededEventHandler = (DataGridViewCellToolTipTextNeededEventHandler)base.Events[DataGridView.CellToolTipTextNeededEvent];
			if (dataGridViewCellToolTipTextNeededEventHandler != null)
			{
				dataGridViewCellToolTipTextNeededEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValidated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001035 RID: 4149 RVA: 0x0003EAEC File Offset: 0x0003CCEC
		protected virtual void OnCellValidated(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellValidatedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValidating" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValidatingEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValidatingEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValidatingEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001036 RID: 4150 RVA: 0x0003EB20 File Offset: 0x0003CD20
		protected virtual void OnCellValidating(DataGridViewCellValidatingEventArgs e)
		{
			DataGridViewCellValidatingEventHandler dataGridViewCellValidatingEventHandler = (DataGridViewCellValidatingEventHandler)base.Events[DataGridView.CellValidatingEvent];
			if (dataGridViewCellValidatingEventHandler != null)
			{
				dataGridViewCellValidatingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex" /> property of <paramref name="e" /> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex" /> property of <paramref name="e" /> is greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001037 RID: 4151 RVA: 0x0003EB54 File Offset: 0x0003CD54
		protected virtual void OnCellValueChanged(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.CellValueChangedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.ColumnIndex" /> property of <paramref name="e" /> is less than zero or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.RowIndex" /> property of <paramref name="e" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001038 RID: 4152 RVA: 0x0003EB88 File Offset: 0x0003CD88
		protected internal virtual void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
		{
			DataGridViewCellValueEventHandler dataGridViewCellValueEventHandler = (DataGridViewCellValueEventHandler)base.Events[DataGridView.CellValueNeededEvent];
			if (dataGridViewCellValueEventHandler != null)
			{
				dataGridViewCellValueEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.ColumnIndex" /> property of <paramref name="e" /> is less than zero or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.RowIndex" /> property of <paramref name="e" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x06001039 RID: 4153 RVA: 0x0003EBBC File Offset: 0x0003CDBC
		protected virtual void OnCellValuePushed(DataGridViewCellValueEventArgs e)
		{
			DataGridViewCellValueEventHandler dataGridViewCellValueEventHandler = (DataGridViewCellValueEventHandler)base.Events[DataGridView.CellValuePushedEvent];
			if (dataGridViewCellValueEventHandler != null)
			{
				dataGridViewCellValueEventHandler(this, e);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		internal void OnColumnAddedInternal(DataGridViewColumnEventArgs e)
		{
			if (e.Column.CellTemplate != null)
			{
				if (!this.is_autogenerating_columns && this.columns.Count == 1)
				{
					this.ReBind();
				}
				foreach (object obj in this.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.Cells.Add((DataGridViewCell)e.Column.CellTemplate.Clone());
				}
			}
			e.Column.DataColumnIndex = this.FindDataColumnIndex(e.Column);
			this.AutoResizeColumnsInternal();
			this.OnColumnAdded(e);
			this.PrepareEditingRow(false, true);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003ECD8 File Offset: 0x0003CED8
		private int FindDataColumnIndex(DataGridViewColumn column)
		{
			if (column != null && this.DataManager != null)
			{
				PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
				for (int i = 0; i < itemProperties.Count; i++)
				{
					if (string.Compare(column.DataPropertyName, itemProperties[i].Name, true) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnAdded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600103C RID: 4156 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		protected virtual void OnColumnAdded(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnAddedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnContextMenuStripChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600103D RID: 4157 RVA: 0x0003ED70 File Offset: 0x0003CF70
		protected internal virtual void OnColumnContextMenuStripChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnContextMenuStripChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDataPropertyNameChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600103E RID: 4158 RVA: 0x0003EDA4 File Offset: 0x0003CFA4
		protected internal virtual void OnColumnDataPropertyNameChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnDataPropertyNameChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600103F RID: 4159 RVA: 0x0003EDD8 File Offset: 0x0003CFD8
		protected internal virtual void OnColumnDefaultCellStyleChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnDefaultCellStyleChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDisplayIndexChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001040 RID: 4160 RVA: 0x0003EE0C File Offset: 0x0003D00C
		protected internal virtual void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnDisplayIndexChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs" /> that contains the event data. </param>
		// Token: 0x06001041 RID: 4161 RVA: 0x0003EE40 File Offset: 0x0003D040
		protected virtual void OnColumnDividerDoubleClick(DataGridViewColumnDividerDoubleClickEventArgs e)
		{
			DataGridViewColumnDividerDoubleClickEventHandler dataGridViewColumnDividerDoubleClickEventHandler = (DataGridViewColumnDividerDoubleClickEventHandler)base.Events[DataGridView.ColumnDividerDoubleClickEvent];
			if (dataGridViewColumnDividerDoubleClickEventHandler != null)
			{
				dataGridViewColumnDividerDoubleClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerWidthChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001042 RID: 4162 RVA: 0x0003EE74 File Offset: 0x0003D074
		protected internal virtual void OnColumnDividerWidthChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnDividerWidthChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderCellChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001043 RID: 4163 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		protected internal virtual void OnColumnHeaderCellChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnHeaderCellChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderMouseClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex" /> property of <paramref name="e" /> is less than zero or greater than the number of columns in the control minus one.</exception>
		// Token: 0x06001044 RID: 4164 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		protected virtual void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewColumn dataGridViewColumn = this.Columns[e.ColumnIndex];
			if (dataGridViewColumn.SortMode == DataGridViewColumnSortMode.Automatic)
			{
				ListSortDirection listSortDirection;
				if (this.SortedColumn != dataGridViewColumn || this.sortOrder != SortOrder.Ascending)
				{
					listSortDirection = 0;
				}
				else
				{
					listSortDirection = 1;
				}
				this.Sort(dataGridViewColumn, listSortDirection);
			}
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.ColumnHeaderMouseClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderMouseDoubleClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the cell and the position of the mouse pointer.</param>
		// Token: 0x06001045 RID: 4165 RVA: 0x0003EF54 File Offset: 0x0003D154
		protected virtual void OnColumnHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.ColumnHeaderMouseDoubleClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersBorderStyleChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001046 RID: 4166 RVA: 0x0003EF88 File Offset: 0x0003D188
		protected virtual void OnColumnHeadersBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.ColumnHeadersBorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001047 RID: 4167 RVA: 0x0003EFBC File Offset: 0x0003D1BC
		protected virtual void OnColumnHeadersDefaultCellStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.ColumnHeadersDefaultCellStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersHeightChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001048 RID: 4168 RVA: 0x0003EFF0 File Offset: 0x0003D1F0
		protected virtual void OnColumnHeadersHeightChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.ColumnHeadersHeightChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersHeightSizeModeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06001049 RID: 4169 RVA: 0x0003F024 File Offset: 0x0003D224
		protected virtual void OnColumnHeadersHeightSizeModeChanged(DataGridViewAutoSizeModeEventArgs e)
		{
			DataGridViewAutoSizeModeEventHandler dataGridViewAutoSizeModeEventHandler = (DataGridViewAutoSizeModeEventHandler)base.Events[DataGridView.ColumnHeadersHeightSizeModeChangedEvent];
			if (dataGridViewAutoSizeModeEventHandler != null)
			{
				dataGridViewAutoSizeModeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnMinimumWidthChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600104A RID: 4170 RVA: 0x0003F058 File Offset: 0x0003D258
		protected internal virtual void OnColumnMinimumWidthChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnMinimumWidthChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnNameChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600104B RID: 4171 RVA: 0x0003F08C File Offset: 0x0003D28C
		protected internal virtual void OnColumnNameChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnNameChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003F0C0 File Offset: 0x0003D2C0
		internal void OnColumnPreRemovedInternal(DataGridViewColumnEventArgs e)
		{
			if (this.Columns.Count - 1 == 0)
			{
				this.MoveCurrentCell(-1, -1, true, false, false, true);
				this.rows.ClearInternal();
			}
			else if (this.currentCell != null && this.CurrentCell.ColumnIndex == e.Column.Index)
			{
				int num = e.Column.Index;
				if (num >= this.Columns.Count - 1)
				{
					num = this.Columns.Count - 1 - 1;
				}
				this.MoveCurrentCell(num, this.currentCell.RowIndex, true, false, false, true);
				if (this.hover_cell != null && this.hover_cell.ColumnIndex >= e.Column.Index)
				{
					this.hover_cell = null;
				}
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003F194 File Offset: 0x0003D394
		private void OnColumnPostRemovedInternal(DataGridViewColumnEventArgs e)
		{
			if (e.Column.CellTemplate != null)
			{
				int index = e.Column.Index;
				foreach (object obj in this.Rows)
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					dataGridViewRow.Cells.RemoveAt(index);
				}
			}
			this.AutoResizeColumnsInternal();
			this.PrepareEditingRow(false, true);
			this.OnColumnRemoved(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnRemoved" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		// Token: 0x0600104E RID: 4174 RVA: 0x0003F23C File Offset: 0x0003D43C
		protected virtual void OnColumnRemoved(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnRemovedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnSortModeChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600104F RID: 4175 RVA: 0x0003F270 File Offset: 0x0003D470
		protected internal virtual void OnColumnSortModeChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnSortModeChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidCastException">The column changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		// Token: 0x06001050 RID: 4176 RVA: 0x0003F2A4 File Offset: 0x0003D4A4
		protected internal virtual void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
		{
			DataGridViewColumnStateChangedEventHandler dataGridViewColumnStateChangedEventHandler = (DataGridViewColumnStateChangedEventHandler)base.Events[DataGridView.ColumnStateChangedEvent];
			if (dataGridViewColumnStateChangedEventHandler != null)
			{
				dataGridViewColumnStateChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnToolTipTextChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains information about the column.</param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001051 RID: 4177 RVA: 0x0003F2D8 File Offset: 0x0003D4D8
		protected internal virtual void OnColumnToolTipTextChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnToolTipTextChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnWidthChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001052 RID: 4178 RVA: 0x0003F30C File Offset: 0x0003D50C
		protected internal virtual void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
		{
			DataGridViewColumnEventHandler dataGridViewColumnEventHandler = (DataGridViewColumnEventHandler)base.Events[DataGridView.ColumnWidthChangedEvent];
			if (dataGridViewColumnEventHandler != null)
			{
				dataGridViewColumnEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CurrentCellChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001053 RID: 4179 RVA: 0x0003F340 File Offset: 0x0003D540
		protected virtual void OnCurrentCellChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.CurrentCellChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CurrentCellDirtyStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001054 RID: 4180 RVA: 0x0003F374 File Offset: 0x0003D574
		protected virtual void OnCurrentCellDirtyStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.CurrentCellDirtyStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event and updates the <see cref="P:System.Windows.Forms.DataGridView.UserSetCursor" /> property if the cursor was changed in user code.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001055 RID: 4181 RVA: 0x0003F3A8 File Offset: 0x0003D5A8
		protected override void OnCursorChanged(EventArgs e)
		{
			base.OnCursorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs" /> that contains the event data.</param>
		// Token: 0x06001056 RID: 4182 RVA: 0x0003F3B4 File Offset: 0x0003D5B4
		protected virtual void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
		{
			DataGridViewBindingCompleteEventHandler dataGridViewBindingCompleteEventHandler = (DataGridViewBindingCompleteEventHandler)base.Events[DataGridView.DataBindingCompleteEvent];
			if (dataGridViewBindingCompleteEventHandler != null)
			{
				dataGridViewBindingCompleteEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event. </summary>
		/// <param name="displayErrorDialogIfNoHandler">true to display an error dialog box if there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x06001057 RID: 4183 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
		protected virtual void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
		{
			DataGridViewDataErrorEventHandler dataGridViewDataErrorEventHandler = (DataGridViewDataErrorEventHandler)base.Events[DataGridView.DataErrorEvent];
			if (dataGridViewDataErrorEventHandler != null)
			{
				dataGridViewDataErrorEventHandler(this, e);
			}
			else if (displayErrorDialogIfNoHandler)
			{
				MessageBox.Show(e.ToString());
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001058 RID: 4184 RVA: 0x0003F430 File Offset: 0x0003D630
		protected virtual void OnDataMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.DataMemberChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001059 RID: 4185 RVA: 0x0003F464 File Offset: 0x0003D664
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.DataSourceChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600105A RID: 4186 RVA: 0x0003F498 File Offset: 0x0003D698
		protected virtual void OnDefaultCellStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.DefaultCellStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DefaultValuesNeeded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		// Token: 0x0600105B RID: 4187 RVA: 0x0003F4CC File Offset: 0x0003D6CC
		protected virtual void OnDefaultValuesNeeded(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.DefaultValuesNeededEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600105C RID: 4188 RVA: 0x0003F500 File Offset: 0x0003D700
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			Point point = base.PointToClient(Control.MousePosition);
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(point.X, point.Y);
			if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
			{
				this.OnCellDoubleClick(new DataGridViewCellEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs" /> that contains information about the editing control.</param>
		// Token: 0x0600105D RID: 4189 RVA: 0x0003F558 File Offset: 0x0003D758
		protected virtual void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewEditingControlShowingEventHandler dataGridViewEditingControlShowingEventHandler = (DataGridViewEditingControlShowingEventHandler)base.Events[DataGridView.EditingControlShowingEvent];
			if (dataGridViewEditingControlShowingEventHandler != null)
			{
				dataGridViewEditingControlShowingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.EditModeChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidCastException">When entering edit mode, the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		// Token: 0x0600105E RID: 4190 RVA: 0x0003F58C File Offset: 0x0003D78C
		protected virtual void OnEditModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.EditModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600105F RID: 4191 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidCastException">The control is configured to enter edit mode when it receives focus, but upon entering focus, the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001060 RID: 4192 RVA: 0x0003F5CC File Offset: 0x0003D7CC
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001061 RID: 4193 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.currentCell != null && this.ShowFocusCues)
			{
				this.InvalidateCell(this.currentCell);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001062 RID: 4194 RVA: 0x0003F604 File Offset: 0x0003D804
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001063 RID: 4195 RVA: 0x0003F610 File Offset: 0x0003D810
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.GridColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001064 RID: 4196 RVA: 0x0003F61C File Offset: 0x0003D81C
		protected virtual void OnGridColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.GridColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001065 RID: 4197 RVA: 0x0003F650 File Offset: 0x0003D850
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.ReBind();
			if (this.DataManager == null && this.CurrentCell == null && this.Rows.Count > 0 && this.Columns.Count > 0)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), 0, true, false, false, false);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001066 RID: 4198 RVA: 0x0003F6B4 File Offset: 0x0003D8B4
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Exception">This action would cause the control to enter edit mode but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001067 RID: 4199 RVA: 0x0003F6C0 File Offset: 0x0003D8C0
		[EditorBrowsable(2)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			e.Handled = this.ProcessDataGridViewKey(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x06001068 RID: 4200 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		[EditorBrowsable(2)]
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06001069 RID: 4201 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		[EditorBrowsable(2)]
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x0600106A RID: 4202 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (this.horizontalScrollBar.Visible && this.verticalScrollBar.Visible)
			{
				this.horizontalScrollBar.Bounds = new Rectangle(this.BorderWidth, base.Height - this.BorderWidth - this.horizontalScrollBar.Height, base.Width - 2 * this.BorderWidth - this.verticalScrollBar.Width, this.horizontalScrollBar.Height);
				this.verticalScrollBar.Bounds = new Rectangle(base.Width - this.BorderWidth - this.verticalScrollBar.Width, this.BorderWidth, this.verticalScrollBar.Width, base.Height - 2 * this.BorderWidth - this.horizontalScrollBar.Height);
			}
			else if (this.horizontalScrollBar.Visible)
			{
				this.horizontalScrollBar.Bounds = new Rectangle(this.BorderWidth, base.Height - this.BorderWidth - this.horizontalScrollBar.Height, base.Width - 2 * this.BorderWidth, this.horizontalScrollBar.Height);
			}
			else if (this.verticalScrollBar.Visible)
			{
				this.verticalScrollBar.Bounds = new Rectangle(base.Width - this.BorderWidth - this.verticalScrollBar.Width, this.BorderWidth, this.verticalScrollBar.Width, base.Height - 2 * this.BorderWidth);
			}
			this.AutoResizeColumnsInternal();
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600106B RID: 4203 RVA: 0x0003F890 File Offset: 0x0003DA90
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600106C RID: 4204 RVA: 0x0003F89C File Offset: 0x0003DA9C
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (this.currentCell != null && this.ShowFocusCues)
			{
				this.InvalidateCell(this.currentCell);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x0600106D RID: 4205 RVA: 0x0003F8C8 File Offset: 0x0003DAC8
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (this.column_resize_active || this.row_resize_active)
			{
				return;
			}
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			DataGridViewHitTestType type = hitTestInfo.Type;
			if (type != DataGridViewHitTestType.Cell)
			{
				if (type == DataGridViewHitTestType.ColumnHeader)
				{
					Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
					Point point;
					point..ctor(e.X - cellDisplayRectangle.X, e.Y - cellDisplayRectangle.Y);
					this.OnColumnHeaderMouseClick(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, point.X, point.Y, e));
				}
			}
			else
			{
				Rectangle cellDisplayRectangle2 = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
				Point point2;
				point2..ctor(e.X - cellDisplayRectangle2.X, e.Y - cellDisplayRectangle2.Y);
				this.OnCellMouseClick(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, point2.X, point2.Y, e));
				DataGridViewCell cellInternal = this.GetCellInternal(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex);
				if (cellInternal.GetContentBounds(hitTestInfo.RowIndex).Contains(point2))
				{
					DataGridViewCellEventArgs dataGridViewCellEventArgs = new DataGridViewCellEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex);
					this.OnCellContentClick(dataGridViewCellEventArgs);
				}
			}
		}

		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600106E RID: 4206 RVA: 0x0003FA34 File Offset: 0x0003DC34
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
			{
				this.OnCellMouseDoubleClick(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, hitTestInfo.ColumnX, hitTestInfo.RowY, e));
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		private void DoSelectionOnMouseDown(DataGridView.HitTestInfo hitTest)
		{
			Keys modifierKeys = Control.ModifierKeys;
			bool flag = (modifierKeys & Keys.Control) != Keys.None;
			bool flag2 = (modifierKeys & Keys.Shift) != Keys.None;
			DataGridViewSelectionMode dataGridViewSelectionMode;
			switch (hitTest.Type)
			{
			case DataGridViewHitTestType.Cell:
				dataGridViewSelectionMode = this.selectionMode;
				break;
			case DataGridViewHitTestType.ColumnHeader:
				dataGridViewSelectionMode = ((this.selectionMode != DataGridViewSelectionMode.ColumnHeaderSelect) ? this.selectionMode : DataGridViewSelectionMode.FullColumnSelect);
				if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullColumnSelect)
				{
					return;
				}
				break;
			case DataGridViewHitTestType.RowHeader:
				dataGridViewSelectionMode = ((this.selectionMode != DataGridViewSelectionMode.RowHeaderSelect) ? this.selectionMode : DataGridViewSelectionMode.FullRowSelect);
				if (dataGridViewSelectionMode != DataGridViewSelectionMode.FullRowSelect)
				{
					return;
				}
				break;
			default:
				return;
			}
			if (!flag)
			{
				if (!flag2)
				{
					this.selected_row = hitTest.RowIndex;
					this.selected_column = hitTest.ColumnIndex;
				}
				if (!flag2)
				{
					if (this.selected_row != -1)
					{
						this.selected_row = hitTest.RowIndex;
					}
					if (this.selected_column != -1)
					{
						this.selected_column = hitTest.ColumnIndex;
					}
				}
				int num;
				int num2;
				if (this.selected_row >= hitTest.RowIndex)
				{
					num = hitTest.RowIndex;
					num2 = ((!flag2) ? num : this.selected_row);
				}
				else
				{
					num2 = hitTest.RowIndex;
					num = ((!flag2) ? num2 : this.selected_row);
				}
				int num3;
				int num4;
				if (this.selected_column >= hitTest.ColumnIndex)
				{
					num3 = hitTest.ColumnIndex;
					num4 = ((!flag2) ? num3 : this.selected_column);
				}
				else
				{
					num4 = hitTest.ColumnIndex;
					num3 = ((!flag2) ? num4 : this.selected_column);
				}
				switch (dataGridViewSelectionMode)
				{
				case DataGridViewSelectionMode.CellSelect:
				case DataGridViewSelectionMode.RowHeaderSelect:
				case DataGridViewSelectionMode.ColumnHeaderSelect:
				{
					if (!flag2)
					{
						for (int i = 0; i < this.ColumnCount; i++)
						{
							if (this.columns[i].Selected)
							{
								this.SetSelectedColumnCore(i, false);
							}
						}
						for (int j = 0; j < this.RowCount; j++)
						{
							if (this.rows[j].Selected)
							{
								this.SetSelectedRowCore(j, false);
							}
						}
					}
					for (int k = 0; k < this.RowCount; k++)
					{
						for (int l = 0; l < this.ColumnCount; l++)
						{
							bool flag3 = k >= num && k <= num2 && l >= num3 && l <= num4;
							if (flag3 != this.Rows[k].Cells[l].Selected)
							{
								this.SetSelectedCellCore(l, k, flag3);
							}
						}
					}
					break;
				}
				case DataGridViewSelectionMode.FullRowSelect:
				{
					for (int m = 0; m < this.RowCount; m++)
					{
						bool flag4 = m >= num && m <= num2;
						if (!flag4)
						{
							for (int n = 0; n < this.ColumnCount; n++)
							{
								if (this.Rows[m].Cells[n].Selected)
								{
									this.SetSelectedCellCore(n, m, false);
								}
							}
						}
						if (flag4 != this.Rows[m].Selected)
						{
							this.SetSelectedRowCore(m, flag4);
						}
					}
					break;
				}
				case DataGridViewSelectionMode.FullColumnSelect:
				{
					for (int num5 = 0; num5 < this.ColumnCount; num5++)
					{
						bool flag5 = num5 >= num3 && num5 <= num4;
						if (!flag5)
						{
							for (int num6 = 0; num6 < this.RowCount; num6++)
							{
								if (this.Rows[num6].Cells[num5].Selected)
								{
									this.SetSelectedCellCore(num5, num6, false);
								}
							}
						}
						if (flag5 != this.Columns[num5].Selected)
						{
							this.SetSelectedColumnCore(num5, flag5);
						}
					}
					break;
				}
				}
			}
			else if (flag)
			{
				switch (dataGridViewSelectionMode)
				{
				case DataGridViewSelectionMode.CellSelect:
				case DataGridViewSelectionMode.RowHeaderSelect:
				case DataGridViewSelectionMode.ColumnHeaderSelect:
					if (hitTest.ColumnIndex >= 0 && hitTest.RowIndex >= 0)
					{
						this.SetSelectedCellCore(hitTest.ColumnIndex, hitTest.RowIndex, !this.Rows[hitTest.RowIndex].Cells[hitTest.ColumnIndex].Selected);
					}
					break;
				case DataGridViewSelectionMode.FullRowSelect:
					this.SetSelectedRowCore(hitTest.RowIndex, !this.rows[hitTest.RowIndex].Selected);
					break;
				case DataGridViewSelectionMode.FullColumnSelect:
					this.SetSelectedColumnCore(hitTest.ColumnIndex, !this.columns[hitTest.ColumnIndex].Selected);
					break;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001070 RID: 4208 RVA: 0x0003FF98 File Offset: 0x0003E198
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!this.EndEdit())
			{
				return;
			}
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if ((hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader || (hitTestInfo.Type == DataGridViewHitTestType.Cell && !this.ColumnHeadersVisible)) && this.MouseOverColumnResize(hitTestInfo.ColumnIndex, e.X))
			{
				if (e.Clicks == 2)
				{
					this.AutoResizeColumn(hitTestInfo.ColumnIndex);
					return;
				}
				this.resize_band = hitTestInfo.ColumnIndex;
				this.column_resize_active = true;
				this.resize_band_start = e.X;
				this.resize_band_delta = 0;
				this.DrawVerticalResizeLine(this.resize_band_start);
				return;
			}
			else if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader && this.MouseOverRowResize(hitTestInfo.RowIndex, e.Y))
			{
				if (e.Clicks == 2)
				{
					this.AutoResizeRow(hitTestInfo.RowIndex);
					return;
				}
				this.resize_band = hitTestInfo.RowIndex;
				this.row_resize_active = true;
				this.resize_band_start = e.Y;
				this.resize_band_delta = 0;
				this.DrawHorizontalResizeLine(this.resize_band_start);
				return;
			}
			else
			{
				if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
				{
					DataGridViewRow dataGridViewRow = this.rows[hitTestInfo.RowIndex];
					DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[hitTestInfo.ColumnIndex];
					this.SetCurrentCellAddressCore(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, false, true, true);
					Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
					this.OnCellMouseDown(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, e.X - cellDisplayRectangle.X, e.Y - cellDisplayRectangle.Y, e));
					this.OnCellClick(new DataGridViewCellEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex));
				}
				this.DoSelectionOnMouseDown(hitTestInfo);
				if (hitTestInfo.Type != DataGridViewHitTestType.Cell)
				{
					if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
					{
						this.pressed_header_cell = this.columns[hitTestInfo.ColumnIndex].HeaderCell;
					}
					else if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
					{
						this.pressed_header_cell = this.rows[hitTestInfo.RowIndex].HeaderCell;
					}
					base.Invalidate();
					return;
				}
				base.Invalidate();
				return;
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000401E0 File Offset: 0x0003E3E0
		private void UpdateBindingPosition(int position)
		{
			if (this.DataManager != null)
			{
				this.DataManager.Position = position;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001072 RID: 4210 RVA: 0x000401FC File Offset: 0x0003E3FC
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001073 RID: 4211 RVA: 0x00040208 File Offset: 0x0003E408
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.hover_cell != null)
			{
				this.OnCellMouseLeave(new DataGridViewCellEventArgs(this.hover_cell.ColumnIndex, this.hover_cell.RowIndex));
				this.hover_cell = null;
			}
			this.EnteredHeaderCell = null;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001074 RID: 4212 RVA: 0x00040258 File Offset: 0x0003E458
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.column_resize_active)
			{
				this.DrawVerticalResizeLine(this.resize_band_start + this.resize_band_delta);
				this.resize_band_delta = e.X - this.resize_band_start;
				this.DrawVerticalResizeLine(this.resize_band_start + this.resize_band_delta);
				return;
			}
			if (this.row_resize_active)
			{
				this.DrawHorizontalResizeLine(this.resize_band_start + this.resize_band_delta);
				this.resize_band_delta = e.Y - this.resize_band_start;
				this.DrawHorizontalResizeLine(this.resize_band_start + this.resize_band_delta);
				return;
			}
			Cursor cursor = Cursors.Default;
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader || (!this.ColumnHeadersVisible && hitTestInfo.Type == DataGridViewHitTestType.Cell && this.MouseOverColumnResize(hitTestInfo.ColumnIndex, e.X)))
			{
				this.EnteredHeaderCell = this.Columns[hitTestInfo.ColumnIndex].HeaderCell;
				if (this.MouseOverColumnResize(hitTestInfo.ColumnIndex, e.X))
				{
					cursor = Cursors.VSplit;
				}
			}
			else if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
			{
				this.EnteredHeaderCell = null;
				DataGridViewCell cellInternal = this.GetCellInternal(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex);
				Rectangle errorIconBounds = cellInternal.ErrorIconBounds;
				if (!errorIconBounds.IsEmpty)
				{
					Point location = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false).Location;
					errorIconBounds.X += location.X;
					errorIconBounds.Y += location.Y;
					if (errorIconBounds.Contains(e.X, e.Y))
					{
						if (this.tooltip_currently_showing != cellInternal)
						{
							this.MouseEnteredErrorIcon(cellInternal);
						}
					}
					else
					{
						this.MouseLeftErrorIcon(cellInternal);
					}
				}
				this.Cursor = cursor;
				if (this.hover_cell == null)
				{
					this.hover_cell = cellInternal;
					this.OnCellMouseEnter(new DataGridViewCellEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex));
					Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
					this.OnCellMouseMove(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, e.X - cellDisplayRectangle.X, e.Y - cellDisplayRectangle.Y, e));
					return;
				}
				if (this.hover_cell.RowIndex == hitTestInfo.RowIndex && this.hover_cell.ColumnIndex == hitTestInfo.ColumnIndex)
				{
					Rectangle cellDisplayRectangle2 = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
					this.OnCellMouseMove(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, e.X - cellDisplayRectangle2.X, e.Y - cellDisplayRectangle2.Y, e));
					return;
				}
				this.OnCellMouseLeave(new DataGridViewCellEventArgs(this.hover_cell.ColumnIndex, this.hover_cell.RowIndex));
				this.hover_cell = cellInternal;
				this.OnCellMouseEnter(new DataGridViewCellEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex));
				Rectangle cellDisplayRectangle3 = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
				this.OnCellMouseMove(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, e.X - cellDisplayRectangle3.X, e.Y - cellDisplayRectangle3.Y, e));
				return;
			}
			else if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
			{
				DataGridViewRowHeaderCell headerCell = this.Rows[hitTestInfo.RowIndex].HeaderCell;
				this.EnteredHeaderCell = headerCell;
				if (this.MouseOverRowResize(hitTestInfo.RowIndex, e.Y))
				{
					cursor = Cursors.HSplit;
				}
				Rectangle internalErrorIconsBounds = headerCell.InternalErrorIconsBounds;
				if (!internalErrorIconsBounds.IsEmpty)
				{
					Point location2 = this.GetCellDisplayRectangle(0, hitTestInfo.RowIndex, false).Location;
					internalErrorIconsBounds.X += this.BorderWidth;
					internalErrorIconsBounds.Y += location2.Y;
					if (internalErrorIconsBounds.Contains(e.X, e.Y))
					{
						if (this.tooltip_currently_showing != headerCell)
						{
							this.MouseEnteredErrorIcon(headerCell);
						}
					}
					else
					{
						this.MouseLeftErrorIcon(headerCell);
					}
				}
			}
			else if (hitTestInfo.Type == DataGridViewHitTestType.TopLeftHeader)
			{
				this.EnteredHeaderCell = null;
				DataGridViewTopLeftHeaderCell dataGridViewTopLeftHeaderCell = (DataGridViewTopLeftHeaderCell)this.TopLeftHeaderCell;
				Rectangle internalErrorIconsBounds2 = dataGridViewTopLeftHeaderCell.InternalErrorIconsBounds;
				if (!internalErrorIconsBounds2.IsEmpty)
				{
					Point empty = Point.Empty;
					internalErrorIconsBounds2.X += this.BorderWidth;
					internalErrorIconsBounds2.Y += empty.Y;
					if (internalErrorIconsBounds2.Contains(e.X, e.Y))
					{
						if (this.tooltip_currently_showing != dataGridViewTopLeftHeaderCell)
						{
							this.MouseEnteredErrorIcon(dataGridViewTopLeftHeaderCell);
						}
					}
					else
					{
						this.MouseLeftErrorIcon(dataGridViewTopLeftHeaderCell);
					}
				}
			}
			else
			{
				this.EnteredHeaderCell = null;
				if (this.hover_cell != null)
				{
					this.OnCellMouseLeave(new DataGridViewCellEventArgs(this.hover_cell.ColumnIndex, this.hover_cell.RowIndex));
					this.hover_cell = null;
				}
			}
			this.Cursor = cursor;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001075 RID: 4213 RVA: 0x0004077C File Offset: 0x0003E97C
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.column_resize_active)
			{
				this.column_resize_active = false;
				if (this.resize_band_delta + this.Columns[this.resize_band].Width < 0)
				{
					this.resize_band_delta = -this.Columns[this.resize_band].Width;
				}
				this.Columns[this.resize_band].Width = Math.Max(this.resize_band_delta + this.Columns[this.resize_band].Width, this.Columns[this.resize_band].MinimumWidth);
				base.Invalidate();
				return;
			}
			if (this.row_resize_active)
			{
				this.row_resize_active = false;
				if (this.resize_band_delta + this.Rows[this.resize_band].Height < 0)
				{
					this.resize_band_delta = -this.Rows[this.resize_band].Height;
				}
				this.Rows[this.resize_band].Height = Math.Max(this.resize_band_delta + this.Rows[this.resize_band].Height, this.Rows[this.resize_band].MinimumHeight);
				base.Invalidate();
				return;
			}
			DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
			{
				Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, false);
				this.OnCellMouseUp(new DataGridViewCellMouseEventArgs(hitTestInfo.ColumnIndex, hitTestInfo.RowIndex, e.X - cellDisplayRectangle.X, e.Y - cellDisplayRectangle.Y, e));
			}
			if (this.pressed_header_cell != null)
			{
				DataGridViewHeaderCell dataGridViewHeaderCell = this.pressed_header_cell;
				this.pressed_header_cell = null;
				if (ThemeEngine.Current.DataGridViewHeaderCellHasPressedStyle(this))
				{
					base.Invalidate(this.GetHeaderCellBounds(dataGridViewHeaderCell));
				}
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001076 RID: 4214 RVA: 0x0004097C File Offset: 0x0003EB7C
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			int num = SystemInformation.MouseWheelScrollLines * this.verticalScrollBar.SmallChange;
			if (e.Delta < 0)
			{
				this.verticalScrollBar.SafeValueSet(this.verticalScrollBar.Value + num);
			}
			else
			{
				this.verticalScrollBar.SafeValueSet(this.verticalScrollBar.Value - num);
			}
			this.OnVScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.verticalScrollBar.Value));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.MultiSelectChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001077 RID: 4215 RVA: 0x000409FC File Offset: 0x0003EBFC
		protected virtual void OnMultiSelectChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.MultiSelectChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.NewRowNeeded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001078 RID: 4216 RVA: 0x00040A30 File Offset: 0x0003EC30
		protected virtual void OnNewRowNeeded(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.NewRowNeededEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Exception">Any exceptions that occur during this method are ignored unless they are one of the following:<see cref="T:System.NullReferenceException" /><see cref="T:System.StackOverflowException" /><see cref="T:System.OutOfMemoryException" /><see cref="T:System.Threading.ThreadAbortException" /><see cref="T:System.ExecutionEngineException" /><see cref="T:System.IndexOutOfRangeException" /><see cref="T:System.AccessViolationException" /></exception>
		// Token: 0x06001079 RID: 4217 RVA: 0x00040A64 File Offset: 0x0003EC64
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Rectangle rectangle = base.ClientRectangle;
			this.PaintBackground(graphics, e.ClipRectangle, rectangle);
			List<DataGridViewColumn> columnDisplayIndexSortedArrayList = this.columns.ColumnDisplayIndexSortedArrayList;
			rectangle.Inflate(-this.BorderWidth, -this.BorderWidth);
			if (this.rowHeadersVisible && this.columnHeadersVisible && this.ColumnCount > 0)
			{
				Rectangle rectangle2;
				rectangle2..ctor(rectangle.X, rectangle.Y, this.rowHeadersWidth, this.columnHeadersHeight);
				this.TopLeftHeaderCell.PaintWork(graphics, e.ClipRectangle, rectangle2, -1, this.TopLeftHeaderCell.State, this.ColumnHeadersDefaultCellStyle, this.AdvancedColumnHeadersBorderStyle, DataGridViewPaintParts.All);
			}
			if (this.columnHeadersVisible)
			{
				Rectangle rectangle3 = rectangle;
				rectangle3.Height = this.columnHeadersHeight;
				if (this.rowHeadersVisible)
				{
					rectangle3.X += this.rowHeadersWidth;
				}
				for (int i = this.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = columnDisplayIndexSortedArrayList[i];
					if (dataGridViewColumn.Visible)
					{
						rectangle3.Width = dataGridViewColumn.Width;
						DataGridViewCell headerCell = dataGridViewColumn.HeaderCell;
						DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = (DataGridViewAdvancedBorderStyle)this.AdvancedColumnHeadersBorderStyle.Clone();
						DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustColumnHeaderBorderStyle(this.AdvancedColumnHeadersBorderStyle, dataGridViewAdvancedBorderStyle, headerCell.ColumnIndex == 0, headerCell.ColumnIndex == this.columns.Count - 1);
						headerCell.PaintWork(graphics, e.ClipRectangle, rectangle3, -1, headerCell.State, headerCell.InheritedStyle, dataGridViewAdvancedBorderStyle2, DataGridViewPaintParts.All);
						rectangle3.X += dataGridViewColumn.Width;
					}
				}
				rectangle.Y += this.columnHeadersHeight;
			}
			for (int j = 0; j < this.first_col_index; j++)
			{
				this.Columns[j].DisplayedInternal = false;
			}
			int num = ((!this.rowHeadersVisible) ? 0 : this.rowHeadersWidth);
			for (int k = this.first_col_index; k < this.Columns.Count; k++)
			{
				DataGridViewColumn dataGridViewColumn2 = this.Columns.ColumnDisplayIndexSortedArrayList[k];
				if (dataGridViewColumn2.Visible)
				{
					dataGridViewColumn2.DisplayedInternal = true;
					num += dataGridViewColumn2.Width;
					if (num >= base.Width)
					{
						break;
					}
				}
			}
			for (int l = 0; l < this.first_row_index; l++)
			{
				this.GetRowInternal(l).DisplayedInternal = false;
			}
			for (int m = this.first_row_index; m < this.Rows.Count; m++)
			{
				DataGridViewRow dataGridViewRow = this.Rows[m];
				if (dataGridViewRow.Visible)
				{
					this.GetRowInternal(m).DisplayedInternal = true;
					rectangle.Height = dataGridViewRow.Height;
					bool flag = dataGridViewRow.Index == 0;
					bool flag2 = dataGridViewRow.Index == this.rows.Count - 1;
					dataGridViewRow.Paint(graphics, e.ClipRectangle, rectangle, dataGridViewRow.Index, dataGridViewRow.GetState(dataGridViewRow.Index), flag, flag2);
					rectangle.Y += rectangle.Height;
					rectangle.X = this.BorderWidth;
					if (rectangle.Y >= base.ClientSize.Height - ((!this.horizontalScrollBar.Visible) ? 0 : this.horizontalScrollBar.Height))
					{
						break;
					}
				}
			}
			this.RefreshScrollBars();
			if (this.horizontalScrollBar.Visible && this.verticalScrollBar.Visible)
			{
				graphics.FillRectangle(SystemBrushes.Control, new Rectangle(this.horizontalScrollBar.Right, this.verticalScrollBar.Bottom, this.verticalScrollBar.Width, this.horizontalScrollBar.Height));
			}
			rectangle = base.ClientRectangle;
			BorderStyle borderStyle = this.BorderStyle;
			if (borderStyle != BorderStyle.FixedSingle)
			{
				if (borderStyle == BorderStyle.Fixed3D)
				{
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.Sunken);
				}
			}
			else
			{
				graphics.DrawRectangle(Pens.Black, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1));
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00040EFC File Offset: 0x0003F0FC
		private void RefreshScrollBars()
		{
			int num = 0;
			int num2 = 0;
			foreach (DataGridViewColumn dataGridViewColumn in this.columns.ColumnDisplayIndexSortedArrayList)
			{
				if (dataGridViewColumn.Visible)
				{
					num += dataGridViewColumn.Width;
				}
			}
			foreach (object obj in this.Rows)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				if (dataGridViewRow.Visible)
				{
					num2 += dataGridViewRow.Height;
				}
			}
			if (this.rowHeadersVisible)
			{
				num += this.rowHeadersWidth;
			}
			if (this.columnHeadersVisible)
			{
				num2 += this.columnHeadersHeight;
			}
			bool flag = false;
			bool flag2 = false;
			if (this.AutoSize)
			{
				if (num > base.Size.Width || num2 > base.Size.Height)
				{
					base.Size = new Size(num, num2);
				}
			}
			else
			{
				if (num > base.Size.Width)
				{
					flag = true;
				}
				if (num2 > base.Size.Height)
				{
					flag2 = true;
				}
				if (this.horizontalScrollBar.Visible && num2 + this.horizontalScrollBar.Height > base.Size.Height)
				{
					flag2 = true;
				}
				if (this.verticalScrollBar.Visible && num + this.verticalScrollBar.Width > base.Size.Width)
				{
					flag = true;
				}
				if (this.scrollBars != ScrollBars.Vertical && this.scrollBars != ScrollBars.Both)
				{
					flag2 = false;
				}
				if (this.scrollBars != ScrollBars.Horizontal && this.scrollBars != ScrollBars.Both)
				{
					flag = false;
				}
				if (this.RowCount <= 1)
				{
					flag2 = false;
				}
				if (flag)
				{
					this.horizontalScrollBar.Minimum = 0;
					this.horizontalScrollBar.Maximum = num;
					this.horizontalScrollBar.SmallChange = this.Columns[this.first_col_index].Width;
					int num3 = base.ClientSize.Width - this.rowHeadersWidth - this.horizontalScrollBar.Height;
					if (num3 <= 0)
					{
						num3 = base.ClientSize.Width;
					}
					this.horizontalScrollBar.LargeChange = num3;
				}
				if (flag2)
				{
					this.verticalScrollBar.Minimum = 0;
					this.verticalScrollBar.Maximum = num2;
					int num4 = ((this.Rows.Count <= 0) ? 0 : this.Rows[Math.Min(this.Rows.Count - 1, this.first_row_index)].Height);
					this.verticalScrollBar.SmallChange = num4 + 1;
					int num5 = base.ClientSize.Height - this.columnHeadersHeight - this.verticalScrollBar.Width;
					if (num5 <= 0)
					{
						num5 = base.ClientSize.Height;
					}
					this.verticalScrollBar.LargeChange = num5;
				}
			}
			this.horizontalScrollBar.Visible = flag;
			this.verticalScrollBar.Visible = flag2;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidCastException">The control changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		// Token: 0x0600107B RID: 4219 RVA: 0x0004129C File Offset: 0x0003F49C
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600107C RID: 4220 RVA: 0x000412D0 File Offset: 0x0003F4D0
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.AutoResizeColumnsInternal();
			this.OnVScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.verticalScrollBar.Value));
			this.OnHScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.horizontalScrollBar.Value));
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600107D RID: 4221 RVA: 0x0004131C File Offset: 0x0003F51C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600107E RID: 4222 RVA: 0x00041328 File Offset: 0x0003F528
		protected internal virtual void OnRowContextMenuStripChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowContextMenuStripChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs" /> that contains the event data. </param>
		// Token: 0x0600107F RID: 4223 RVA: 0x0004135C File Offset: 0x0003F55C
		protected virtual void OnRowContextMenuStripNeeded(DataGridViewRowContextMenuStripNeededEventArgs e)
		{
			DataGridViewRowContextMenuStripNeededEventHandler dataGridViewRowContextMenuStripNeededEventHandler = (DataGridViewRowContextMenuStripNeededEventHandler)base.Events[DataGridView.RowContextMenuStripNeededEvent];
			if (dataGridViewRowContextMenuStripNeededEventHandler != null)
			{
				dataGridViewRowContextMenuStripNeededEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001080 RID: 4224 RVA: 0x00041390 File Offset: 0x0003F590
		protected internal virtual void OnRowDefaultCellStyleChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowDefaultCellStyleChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDirtyStateNeeded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.QuestionEventArgs" /> that contains the event data. </param>
		// Token: 0x06001081 RID: 4225 RVA: 0x000413C4 File Offset: 0x0003F5C4
		protected virtual void OnRowDirtyStateNeeded(QuestionEventArgs e)
		{
			QuestionEventHandler questionEventHandler = (QuestionEventHandler)base.Events[DataGridView.RowDirtyStateNeededEvent];
			if (questionEventHandler != null)
			{
				questionEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDividerDoubleClick" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs" /> that contains the event data. </param>
		// Token: 0x06001082 RID: 4226 RVA: 0x000413F8 File Offset: 0x0003F5F8
		protected virtual void OnRowDividerDoubleClick(DataGridViewRowDividerDoubleClickEventArgs e)
		{
			DataGridViewRowDividerDoubleClickEventHandler dataGridViewRowDividerDoubleClickEventHandler = (DataGridViewRowDividerDoubleClickEventHandler)base.Events[DataGridView.RowDividerDoubleClickEvent];
			if (dataGridViewRowDividerDoubleClickEventHandler != null)
			{
				dataGridViewRowDividerDoubleClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDividerHeightChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001083 RID: 4227 RVA: 0x0004142C File Offset: 0x0003F62C
		protected virtual void OnRowDividerHeightChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowDividerHeightChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowEnter" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001084 RID: 4228 RVA: 0x00041460 File Offset: 0x0003F660
		protected virtual void OnRowEnter(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.RowEnterEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001085 RID: 4229 RVA: 0x00041494 File Offset: 0x0003F694
		protected internal virtual void OnRowErrorTextChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowErrorTextChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowErrorTextNeededEventArgs" /> that contains the event data. </param>
		// Token: 0x06001086 RID: 4230 RVA: 0x000414C8 File Offset: 0x0003F6C8
		protected virtual void OnRowErrorTextNeeded(DataGridViewRowErrorTextNeededEventArgs e)
		{
			DataGridViewRowErrorTextNeededEventHandler dataGridViewRowErrorTextNeededEventHandler = (DataGridViewRowErrorTextNeededEventHandler)base.Events[DataGridView.RowErrorTextNeededEvent];
			if (dataGridViewRowErrorTextNeededEventHandler != null)
			{
				dataGridViewRowErrorTextNeededEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderCellChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001087 RID: 4231 RVA: 0x000414FC File Offset: 0x0003F6FC
		protected internal virtual void OnRowHeaderCellChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowHeaderCellChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderMouseClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse and the header cell that was clicked.</param>
		// Token: 0x06001088 RID: 4232 RVA: 0x00041530 File Offset: 0x0003F730
		protected virtual void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.RowHeaderMouseClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderMouseDoubleClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse and the header cell that was double-clicked.</param>
		// Token: 0x06001089 RID: 4233 RVA: 0x00041564 File Offset: 0x0003F764
		protected virtual void OnRowHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
			DataGridViewCellMouseEventHandler dataGridViewCellMouseEventHandler = (DataGridViewCellMouseEventHandler)base.Events[DataGridView.RowHeaderMouseDoubleClickEvent];
			if (dataGridViewCellMouseEventHandler != null)
			{
				dataGridViewCellMouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersBorderStyleChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600108A RID: 4234 RVA: 0x00041598 File Offset: 0x0003F798
		protected virtual void OnRowHeadersBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.RowHeadersBorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600108B RID: 4235 RVA: 0x000415CC File Offset: 0x0003F7CC
		protected virtual void OnRowHeadersDefaultCellStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.RowHeadersDefaultCellStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600108C RID: 4236 RVA: 0x00041600 File Offset: 0x0003F800
		protected virtual void OnRowHeadersWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.RowHeadersWidthChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600108D RID: 4237 RVA: 0x00041634 File Offset: 0x0003F834
		protected virtual void OnRowHeadersWidthSizeModeChanged(DataGridViewAutoSizeModeEventArgs e)
		{
			DataGridViewAutoSizeModeEventHandler dataGridViewAutoSizeModeEventHandler = (DataGridViewAutoSizeModeEventHandler)base.Events[DataGridView.RowHeadersWidthSizeModeChangedEvent];
			if (dataGridViewAutoSizeModeEventHandler != null)
			{
				dataGridViewAutoSizeModeEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600108E RID: 4238 RVA: 0x00041668 File Offset: 0x0003F868
		protected internal virtual void OnRowHeightChanged(DataGridViewRowEventArgs e)
		{
			this.UpdateRowHeightInfo(e.Row.Index, false);
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowHeightChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoNeeded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs" /> that contains the event data. </param>
		// Token: 0x0600108F RID: 4239 RVA: 0x000416AC File Offset: 0x0003F8AC
		protected virtual void OnRowHeightInfoNeeded(DataGridViewRowHeightInfoNeededEventArgs e)
		{
			DataGridViewRowHeightInfoNeededEventHandler dataGridViewRowHeightInfoNeededEventHandler = (DataGridViewRowHeightInfoNeededEventHandler)base.Events[DataGridView.RowHeightInfoNeededEvent];
			if (dataGridViewRowHeightInfoNeededEventHandler != null)
			{
				dataGridViewRowHeightInfoNeededEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoPushed" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoPushedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001090 RID: 4240 RVA: 0x000416E0 File Offset: 0x0003F8E0
		protected virtual void OnRowHeightInfoPushed(DataGridViewRowHeightInfoPushedEventArgs e)
		{
			DataGridViewRowHeightInfoPushedEventHandler dataGridViewRowHeightInfoPushedEventHandler = (DataGridViewRowHeightInfoPushedEventHandler)base.Events[DataGridView.RowHeightInfoPushedEvent];
			if (dataGridViewRowHeightInfoPushedEventHandler != null)
			{
				dataGridViewRowHeightInfoPushedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x06001091 RID: 4241 RVA: 0x00041714 File Offset: 0x0003F914
		protected virtual void OnRowLeave(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.RowLeaveEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowMinimumHeightChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001092 RID: 4242 RVA: 0x00041748 File Offset: 0x0003F948
		protected internal virtual void OnRowMinimumHeightChanged(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowMinimumHeightChangedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowPostPaint" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowPostPaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06001093 RID: 4243 RVA: 0x0004177C File Offset: 0x0003F97C
		protected internal virtual void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
		{
			DataGridViewRowPostPaintEventHandler dataGridViewRowPostPaintEventHandler = (DataGridViewRowPostPaintEventHandler)base.Events[DataGridView.RowPostPaintEvent];
			if (dataGridViewRowPostPaintEventHandler != null)
			{
				dataGridViewRowPostPaintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowPrePaint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowPrePaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06001094 RID: 4244 RVA: 0x000417B0 File Offset: 0x0003F9B0
		protected internal virtual void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
		{
			DataGridViewRowPrePaintEventHandler dataGridViewRowPrePaintEventHandler = (DataGridViewRowPrePaintEventHandler)base.Events[DataGridView.RowPrePaintEvent];
			if (dataGridViewRowPrePaintEventHandler != null)
			{
				dataGridViewRowPrePaintEventHandler(this, e);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000417E4 File Offset: 0x0003F9E4
		internal void OnRowsAddedInternal(DataGridViewRowsAddedEventArgs e)
		{
			if (this.hover_cell != null && this.hover_cell.RowIndex >= e.RowIndex)
			{
				this.hover_cell = null;
			}
			if (base.IsHandleCreated && this.DataManager == null && this.CurrentCell == null && this.Rows.Count > 0 && this.Columns.Count > 0)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), 0, true, false, false, true);
			}
			this.AutoResizeColumnsInternal();
			base.Invalidate();
			this.OnRowsAdded(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsAdded" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowsAddedEventArgs" /> that contains information about the added rows. </param>
		// Token: 0x06001096 RID: 4246 RVA: 0x00041884 File Offset: 0x0003FA84
		protected virtual void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
		{
			DataGridViewRowsAddedEventHandler dataGridViewRowsAddedEventHandler = (DataGridViewRowsAddedEventHandler)base.Events[DataGridView.RowsAddedEvent];
			if (dataGridViewRowsAddedEventHandler != null)
			{
				dataGridViewRowsAddedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsDefaultCellStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001097 RID: 4247 RVA: 0x000418B8 File Offset: 0x0003FAB8
		protected virtual void OnRowsDefaultCellStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.RowsDefaultCellStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000418EC File Offset: 0x0003FAEC
		internal void OnRowsPreRemovedInternal(DataGridViewRowsRemovedEventArgs e)
		{
			if (this.selected_rows != null)
			{
				this.selected_rows.InternalClear();
			}
			if (this.selected_columns != null)
			{
				this.selected_columns.InternalClear();
			}
			if (this.Rows.Count - e.RowCount <= 0)
			{
				this.MoveCurrentCell(-1, -1, true, false, false, true);
				this.hover_cell = null;
			}
			else if (this.Columns.Count == 0)
			{
				this.MoveCurrentCell(-1, -1, true, false, false, true);
				this.hover_cell = null;
			}
			else if (this.currentCell != null && this.currentCell.RowIndex == e.RowIndex)
			{
				int num = e.RowIndex;
				if (num >= this.Rows.Count - e.RowCount)
				{
					num = this.Rows.Count - 1 - e.RowCount;
				}
				this.MoveCurrentCell((this.currentCell == null) ? 0 : this.currentCell.ColumnIndex, num, true, false, false, true);
				if (this.hover_cell != null && this.hover_cell.RowIndex >= e.RowIndex)
				{
					this.hover_cell = null;
				}
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00041A24 File Offset: 0x0003FC24
		internal void OnRowsPostRemovedInternal(DataGridViewRowsRemovedEventArgs e)
		{
			base.Invalidate();
			this.OnRowsRemoved(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsRemoved" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs" /> that contains information about the deleted rows. </param>
		// Token: 0x0600109A RID: 4250 RVA: 0x00041A34 File Offset: 0x0003FC34
		protected virtual void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
		{
			DataGridViewRowsRemovedEventHandler dataGridViewRowsRemovedEventHandler = (DataGridViewRowsRemovedEventHandler)base.Events[DataGridView.RowsRemovedEvent];
			if (dataGridViewRowsRemovedEventHandler != null)
			{
				dataGridViewRowsRemovedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowStateChanged" /> event.</summary>
		/// <param name="rowIndex">The index of the row that is changing state.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowStateChangedEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidCastException">The row changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		// Token: 0x0600109B RID: 4251 RVA: 0x00041A68 File Offset: 0x0003FC68
		protected internal virtual void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
		{
			DataGridViewRowStateChangedEventHandler dataGridViewRowStateChangedEventHandler = (DataGridViewRowStateChangedEventHandler)base.Events[DataGridView.RowStateChangedEvent];
			if (dataGridViewRowStateChangedEventHandler != null)
			{
				dataGridViewRowStateChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowUnshared" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x0600109C RID: 4252 RVA: 0x00041A9C File Offset: 0x0003FC9C
		protected virtual void OnRowUnshared(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.RowUnsharedEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowValidated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x0600109D RID: 4253 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		protected virtual void OnRowValidated(DataGridViewCellEventArgs e)
		{
			DataGridViewCellEventHandler dataGridViewCellEventHandler = (DataGridViewCellEventHandler)base.Events[DataGridView.RowValidatedEvent];
			if (dataGridViewCellEventHandler != null)
			{
				dataGridViewCellEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.RowValidating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600109E RID: 4254 RVA: 0x00041B04 File Offset: 0x0003FD04
		protected virtual void OnRowValidating(DataGridViewCellCancelEventArgs e)
		{
			DataGridViewCellCancelEventHandler dataGridViewCellCancelEventHandler = (DataGridViewCellCancelEventHandler)base.Events[DataGridView.RowValidatingEvent];
			if (dataGridViewCellCancelEventHandler != null)
			{
				dataGridViewCellCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.Scroll" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x0600109F RID: 4255 RVA: 0x00041B38 File Offset: 0x0003FD38
		protected virtual void OnScroll(ScrollEventArgs e)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[DataGridView.ScrollEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.SelectionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains information about the event.</param>
		// Token: 0x060010A0 RID: 4256 RVA: 0x00041B6C File Offset: 0x0003FD6C
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.SelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.SortCompare" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs" /> that contains the event data. </param>
		// Token: 0x060010A1 RID: 4257 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		protected virtual void OnSortCompare(DataGridViewSortCompareEventArgs e)
		{
			DataGridViewSortCompareEventHandler dataGridViewSortCompareEventHandler = (DataGridViewSortCompareEventHandler)base.Events[DataGridView.SortCompareEvent];
			if (dataGridViewSortCompareEventHandler != null)
			{
				dataGridViewSortCompareEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.Sorted" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060010A2 RID: 4258 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		protected virtual void OnSorted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataGridView.SortedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.UserAddedRow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row" /> property of <paramref name="e" /> does not belong to this <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x060010A3 RID: 4259 RVA: 0x00041C08 File Offset: 0x0003FE08
		protected virtual void OnUserAddedRow(DataGridViewRowEventArgs e)
		{
			this.PrepareEditingRow(false, false);
			this.new_row_editing = true;
			if (this.DataManager != null)
			{
				if (this.editing_row != null)
				{
					this.Rows.RemoveInternal(this.editing_row);
					this.editing_row = null;
				}
				this.DataManager.AddNew();
			}
			e = new DataGridViewRowEventArgs(this.Rows[this.NewRowIndex]);
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.UserAddedRowEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.UserDeletedRow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> that contains the event data. </param>
		// Token: 0x060010A4 RID: 4260 RVA: 0x00041C9C File Offset: 0x0003FE9C
		protected virtual void OnUserDeletedRow(DataGridViewRowEventArgs e)
		{
			DataGridViewRowEventHandler dataGridViewRowEventHandler = (DataGridViewRowEventHandler)base.Events[DataGridView.UserDeletedRowEvent];
			if (dataGridViewRowEventHandler != null)
			{
				dataGridViewRowEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.UserDeletingRow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060010A5 RID: 4261 RVA: 0x00041CD0 File Offset: 0x0003FED0
		protected virtual void OnUserDeletingRow(DataGridViewRowCancelEventArgs e)
		{
			DataGridViewRowCancelEventHandler dataGridViewRowCancelEventHandler = (DataGridViewRowCancelEventHandler)base.Events[DataGridView.UserDeletingRowEvent];
			if (dataGridViewRowCancelEventHandler != null)
			{
				dataGridViewRowCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Exception">Validation failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x060010A6 RID: 4262 RVA: 0x00041D04 File Offset: 0x0003FF04
		protected override void OnValidating(CancelEventArgs e)
		{
			base.OnValidating(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060010A7 RID: 4263 RVA: 0x00041D10 File Offset: 0x0003FF10
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <summary>Paints the background of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the background.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="gridBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area in which cells are drawn.</param>
		// Token: 0x060010A8 RID: 4264 RVA: 0x00041D1C File Offset: 0x0003FF1C
		protected virtual void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
		{
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.backgroundColor), gridBounds);
		}

		/// <summary>Processes the A key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		// Token: 0x060010A9 RID: 4265 RVA: 0x00041D3C File Offset: 0x0003FF3C
		protected bool ProcessAKey(Keys keyData)
		{
			if (!this.MultiSelect)
			{
				return false;
			}
			if ((keyData & Keys.Control) == Keys.Control)
			{
				this.SelectAll();
				return true;
			}
			return false;
		}

		/// <summary>Processes keys used for navigating in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="e">Contains information about the key that was pressed.</param>
		/// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true.-or-The DELETE key would delete one or more rows, but an error in the data source prevents the deletion and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AA RID: 4266 RVA: 0x00041D68 File Offset: 0x0003FF68
		protected virtual bool ProcessDataGridViewKey(KeyEventArgs e)
		{
			Keys keys = e.KeyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.Escape:
				return this.ProcessEscapeKey(e.KeyData);
			default:
				if (keys == Keys.Tab)
				{
					return this.ProcessTabKey(e.KeyData);
				}
				if (keys == Keys.Return)
				{
					return this.ProcessEnterKey(e.KeyData);
				}
				if (keys == Keys.A)
				{
					return this.ProcessAKey(e.KeyData);
				}
				if (keys != Keys.NumPad0)
				{
					return keys == Keys.F2 && this.ProcessF2Key(e.KeyData);
				}
				break;
			case Keys.Space:
				return this.ProcessSpaceKey(e.KeyData);
			case Keys.PageUp:
				return this.ProcessPriorKey(e.KeyData);
			case Keys.PageDown:
				return this.ProcessNextKey(e.KeyData);
			case Keys.End:
				return this.ProcessEndKey(e.KeyData);
			case Keys.Home:
				return this.ProcessHomeKey(e.KeyData);
			case Keys.Left:
				return this.ProcessLeftKey(e.KeyData);
			case Keys.Up:
				return this.ProcessUpKey(e.KeyData);
			case Keys.Right:
				return this.ProcessRightKey(e.KeyData);
			case Keys.Down:
				return this.ProcessDownKey(e.KeyData);
			case Keys.Delete:
				return this.ProcessDeleteKey(e.KeyData);
			case Keys.D0:
				break;
			}
			return this.ProcessZeroKey(e.KeyData);
		}

		/// <summary>Processes the DELETE key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.Exception">The DELETE key would delete one or more rows, but an error in the data source prevents the deletion and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AB RID: 4267 RVA: 0x00041EE4 File Offset: 0x000400E4
		protected bool ProcessDeleteKey(Keys keyData)
		{
			if (!this.AllowUserToDeleteRows || this.SelectedRows.Count == 0)
			{
				return false;
			}
			int num = Math.Max(this.selected_row - this.SelectedRows.Count + 1, 0);
			for (int i = this.SelectedRows.Count - 1; i >= 0; i--)
			{
				DataGridViewRow dataGridViewRow = this.SelectedRows[i];
				if (!dataGridViewRow.IsNewRow)
				{
					if (this.hover_cell != null && this.hover_cell.OwningRow == dataGridViewRow)
					{
						this.hover_cell = null;
					}
					if (this.DataManager != null)
					{
						this.DataManager.RemoveAt(dataGridViewRow.Index);
					}
					else
					{
						this.Rows.RemoveAt(dataGridViewRow.Index);
					}
				}
			}
			return true;
		}

		/// <summary>Processes keys, such as the TAB, ESCAPE, ENTER, and ARROW keys, used to control dialog boxes.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AC RID: 4268 RVA: 0x00041FBC File Offset: 0x000401BC
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData != Keys.Tab)
			{
				if (keyData != Keys.Return && keyData != Keys.Escape)
				{
					if (keyData != (Keys.LButton | Keys.Back | Keys.Shift))
					{
						if (keyData != (Keys.LButton | Keys.Back | Keys.Control) && keyData != (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
						{
							goto IL_00BA;
						}
						if (!this.standardTab)
						{
							return base.ProcessDialogKey(keyData & ~Keys.Control);
						}
						if (this.ProcessDataGridViewKey(new KeyEventArgs(keyData)))
						{
							return true;
						}
						goto IL_00BA;
					}
				}
				else
				{
					if (this.ProcessDataGridViewKey(new KeyEventArgs(keyData)))
					{
						return true;
					}
					goto IL_00BA;
				}
			}
			if (this.standardTab)
			{
				return base.ProcessDialogKey(keyData & ~Keys.Control);
			}
			if (this.ProcessDataGridViewKey(new KeyEventArgs(keyData)))
			{
				return true;
			}
			IL_00BA:
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes the DOWN ARROW key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The DOWN ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AD RID: 4269 RVA: 0x0004208C File Offset: 0x0004028C
		protected bool ProcessDownKey(Keys keyData)
		{
			int y = this.CurrentCellAddress.Y;
			if (y < this.Rows.Count - 1)
			{
				if ((keyData & Keys.Control) == Keys.Control)
				{
					this.MoveCurrentCell(this.CurrentCellAddress.X, this.Rows.Count - 1, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				else
				{
					this.MoveCurrentCell(this.CurrentCellAddress.X, y + 1, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes the END key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The END key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AE RID: 4270 RVA: 0x00042148 File Offset: 0x00040348
		protected bool ProcessEndKey(Keys keyData)
		{
			int num = this.ColumnIndexToDisplayIndex(this.currentCellAddress.X);
			if ((keyData & Keys.Control) == Keys.Control)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(this.Columns.Count - 1), this.Rows.Count - 1, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			if (num < this.Columns.Count - 1)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(this.Columns.Count - 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			return false;
		}

		/// <summary>Processes the ENTER key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The ENTER key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010AF RID: 4271 RVA: 0x00042218 File Offset: 0x00040418
		protected bool ProcessEnterKey(Keys keyData)
		{
			if (this.ProcessDownKey(keyData))
			{
				return true;
			}
			this.EndEdit();
			return true;
		}

		/// <summary>Processes the ESC key.</summary>
		/// <returns>true if the key was processed; otherwise, false. </returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		// Token: 0x060010B0 RID: 4272 RVA: 0x00042230 File Offset: 0x00040430
		protected bool ProcessEscapeKey(Keys keyData)
		{
			if (!this.IsCurrentCellInEditMode)
			{
				return false;
			}
			this.CancelEdit();
			return true;
		}

		/// <summary>Processes the F2 key.</summary>
		/// <returns>true if the key was processed; otherwise, false. </returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The F2 key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">The F2 key would cause the control to enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00042248 File Offset: 0x00040448
		protected bool ProcessF2Key(Keys keyData)
		{
			if (this.editMode == DataGridViewEditMode.EditOnF2 || this.editMode == DataGridViewEditMode.EditOnKeystrokeOrF2)
			{
				this.BeginEdit(true);
				return true;
			}
			return false;
		}

		/// <summary>Processes the HOME key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">The key that was pressed.</param>
		/// <exception cref="T:System.InvalidCastException">The HOME key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B2 RID: 4274 RVA: 0x00042270 File Offset: 0x00040470
		protected bool ProcessHomeKey(Keys keyData)
		{
			int num = this.ColumnIndexToDisplayIndex(this.currentCellAddress.X);
			if ((keyData & Keys.Control) == Keys.Control)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), 0, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			if (num > 0)
			{
				this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			return false;
		}

		/// <summary>Processes the INSERT key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00042310 File Offset: 0x00040510
		[MonoInternalNote("What does insert do?")]
		protected bool ProcessInsertKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <returns>true if the message was processed; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00042314 File Offset: 0x00040514
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			DataGridViewCell dataGridViewCell = this.CurrentCell;
			if (dataGridViewCell != null)
			{
				if (dataGridViewCell.KeyEntersEditMode(new KeyEventArgs((Keys)m.WParam.ToInt32())))
				{
					this.BeginEdit(true);
				}
				if (this.EditingControl != null && (m.Msg == 256 || m.Msg == 258))
				{
					XplatUI.SendMessage(this.EditingControl.Handle, (Msg)m.Msg, m.WParam, m.LParam);
				}
			}
			return base.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <returns>true if the message was processed; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B5 RID: 4277 RVA: 0x000423AC File Offset: 0x000405AC
		protected override bool ProcessKeyPreview(ref Message m)
		{
			if (m.Msg == 256 && (this.IsCurrentCellInEditMode || m.HWnd == this.horizontalScrollBar.Handle || m.HWnd == this.verticalScrollBar.Handle))
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)m.WParam.ToInt32());
				IDataGridViewEditingControl dataGridViewEditingControl = (IDataGridViewEditingControl)this.EditingControlInternal;
				if (dataGridViewEditingControl != null && dataGridViewEditingControl.EditingControlWantsInputKey(keyEventArgs.KeyData, false))
				{
					return false;
				}
				Keys keyData = keyEventArgs.KeyData;
				switch (keyData)
				{
				case Keys.Escape:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					break;
				default:
					if (keyData != Keys.Tab)
					{
						goto IL_00E8;
					}
					break;
				}
				return this.ProcessDataGridViewKey(keyEventArgs);
			}
			IL_00E8:
			return base.ProcessKeyPreview(ref m);
		}

		/// <summary>Processes the LEFT ARROW key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The LEFT ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B6 RID: 4278 RVA: 0x000424A8 File Offset: 0x000406A8
		protected bool ProcessLeftKey(Keys keyData)
		{
			int num = this.ColumnIndexToDisplayIndex(this.currentCellAddress.X);
			if (num > 0)
			{
				if ((keyData & Keys.Control) == Keys.Control)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				else
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(num - 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes the PAGE DOWN key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The PAGE DOWN key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B7 RID: 4279 RVA: 0x00042558 File Offset: 0x00040758
		protected bool ProcessNextKey(Keys keyData)
		{
			int y = this.CurrentCellAddress.Y;
			if (y < this.Rows.Count - 1)
			{
				int num = Math.Min(this.Rows.Count - 1, y + this.DisplayedRowCount(false));
				this.MoveCurrentCell(this.CurrentCellAddress.X, num, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			return false;
		}

		/// <summary>Processes the PAGE UP key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The PAGE UP key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B8 RID: 4280 RVA: 0x000425DC File Offset: 0x000407DC
		protected bool ProcessPriorKey(Keys keyData)
		{
			int y = this.CurrentCellAddress.Y;
			if (y > 0)
			{
				int num = Math.Max(0, y - this.DisplayedRowCount(false));
				this.MoveCurrentCell(this.CurrentCellAddress.X, num, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				return true;
			}
			return false;
		}

		/// <summary>Processes the RIGHT ARROW key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The RIGHT ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00042648 File Offset: 0x00040848
		protected bool ProcessRightKey(Keys keyData)
		{
			int num = this.ColumnIndexToDisplayIndex(this.currentCellAddress.X);
			if (num < this.Columns.Count - 1)
			{
				if ((keyData & Keys.Control) == Keys.Control)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(this.Columns.Count - 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				else
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(num + 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes the SPACEBAR.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x060010BA RID: 4282 RVA: 0x00042710 File Offset: 0x00040910
		protected bool ProcessSpaceKey(Keys keyData)
		{
			if ((keyData & Keys.Shift) == Keys.Shift)
			{
				if (this.selectionMode == DataGridViewSelectionMode.RowHeaderSelect)
				{
					this.SetSelectedRowCore(this.CurrentCellAddress.Y, true);
					this.InvalidateRow(this.CurrentCellAddress.Y);
					return true;
				}
				if (this.selectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
				{
					this.SetSelectedColumnCore(this.CurrentCellAddress.X, true);
					this.InvalidateColumn(this.CurrentCellAddress.X);
					return true;
				}
			}
			if (this.CurrentCell is DataGridViewButtonCell || this.CurrentCell is DataGridViewLinkCell || this.CurrentCell is DataGridViewCheckBoxCell)
			{
				DataGridViewCellEventArgs dataGridViewCellEventArgs = new DataGridViewCellEventArgs(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex);
				this.OnCellClick(dataGridViewCellEventArgs);
				this.OnCellContentClick(dataGridViewCellEventArgs);
				if (this.CurrentCell is DataGridViewButtonCell)
				{
					(this.CurrentCell as DataGridViewButtonCell).OnClickInternal(dataGridViewCellEventArgs);
				}
				if (this.CurrentCell is DataGridViewCheckBoxCell)
				{
					(this.CurrentCell as DataGridViewCheckBoxCell).OnClickInternal(dataGridViewCellEventArgs);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes the TAB key.</summary>
		/// <returns>true if the key was processed; otherwise, false. </returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The TAB key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010BB RID: 4283 RVA: 0x0004283C File Offset: 0x00040A3C
		protected bool ProcessTabKey(Keys keyData)
		{
			Form form = base.FindForm();
			if (form != null)
			{
				form.ActivateFocusCues();
			}
			int num = this.ColumnIndexToDisplayIndex(this.currentCellAddress.X);
			if ((keyData & Keys.Shift) == Keys.Shift)
			{
				if (num > 0)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(num - 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, false, true);
					return true;
				}
				if (this.currentCellAddress.Y > 0)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(this.Columns.Count - 1), this.currentCellAddress.Y - 1, true, false, false, true);
					return true;
				}
			}
			else
			{
				if (num < this.Columns.Count - 1)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(num + 1), this.currentCellAddress.Y, true, (keyData & Keys.Control) == Keys.Control, false, true);
					return true;
				}
				if (this.currentCellAddress.Y < this.Rows.Count - 1)
				{
					this.MoveCurrentCell(this.ColumnDisplayIndexToIndex(0), this.currentCellAddress.Y + 1, true, false, false, true);
					return true;
				}
			}
			return false;
		}

		/// <summary>Processes the UP ARROW key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The UP ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010BC RID: 4284 RVA: 0x00042974 File Offset: 0x00040B74
		protected bool ProcessUpKey(Keys keyData)
		{
			int y = this.CurrentCellAddress.Y;
			if (y > 0)
			{
				if ((keyData & Keys.Control) == Keys.Control)
				{
					this.MoveCurrentCell(this.CurrentCellAddress.X, 0, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				else
				{
					this.MoveCurrentCell(this.CurrentCellAddress.X, y - 1, true, (keyData & Keys.Control) == Keys.Control, (keyData & Keys.Shift) == Keys.Shift, true);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes the 0 key.</summary>
		/// <returns>true if the key was processed; otherwise, false.</returns>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key or keys to process.</param>
		/// <exception cref="T:System.InvalidCastException">The 0 key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		/// <exception cref="T:System.Exception">This action would cause the control to enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. </exception>
		// Token: 0x060010BD RID: 4285 RVA: 0x00042A18 File Offset: 0x00040C18
		protected bool ProcessZeroKey(Keys keyData)
		{
			if ((keyData & Keys.Control) == Keys.Control && this.CurrentCell.EditType != null)
			{
				this.CurrentCell.Value = DBNull.Value;
				this.InvalidateCell(this.CurrentCell);
				return true;
			}
			return false;
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" />.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or both of the width or height values exceeds the maximum value of 8,388,607. </exception>
		// Token: 0x060010BE RID: 4286 RVA: 0x00042A68 File Offset: 0x00040C68
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Sets the currently active cell.</summary>
		/// <returns>true if the current cell was successfully set; otherwise, false.</returns>
		/// <param name="columnIndex">The index of the column containing the cell.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <param name="setAnchorCellAddress">true to make the new current cell the anchor cell for a subsequent multicell selection using the SHIFT key; otherwise, false.</param>
		/// <param name="validateCurrentCell">true to validate the value in the old current cell and cancel the change if validation fails; otherwise, false.</param>
		/// <param name="throughMouseClick">true if the current cell is being set as a result of a mouse click; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0 or greater than the number of columns in the control minus 1, and <paramref name="rowIndex" /> is not -1.-or-<paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1, and <paramref name="columnIndex" /> is not -1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified cell has a <see cref="P:System.Windows.Forms.DataGridViewCell.Visible" /> property value of false.-or-This method was called for a reason other than the underlying data source being reset, and another thread is currently executing this method.</exception>
		/// <exception cref="T:System.InvalidCastException">The new current cell tried to enter edit mode, but its <see cref="P:System.Windows.Forms.DataGridViewCell.EditType" /> property does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control" /> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl" />.</exception>
		// Token: 0x060010BF RID: 4287 RVA: 0x00042A78 File Offset: 0x00040C78
		[MonoTODO("Does not use validateCurrentCell")]
		protected virtual bool SetCurrentCellAddressCore(int columnIndex, int rowIndex, bool setAnchorCellAddress, bool validateCurrentCell, bool throughMouseClick)
		{
			if ((columnIndex < 0 || columnIndex > this.Columns.Count - 1) && rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if ((rowIndex < 0 || rowIndex > this.Rows.Count - 1) && columnIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCell dataGridViewCell;
			if (columnIndex == -1 && rowIndex == -1)
			{
				dataGridViewCell = null;
			}
			else
			{
				dataGridViewCell = this.Rows.SharedRow(rowIndex).Cells[columnIndex];
			}
			if (dataGridViewCell != null && !dataGridViewCell.Visible)
			{
				throw new InvalidOperationException("cell is not visible");
			}
			if (this.currentCell != null)
			{
				if (setAnchorCellAddress)
				{
					this.anchor_cell.X = this.currentCell.ColumnIndex;
					this.anchor_cell.Y = this.currentCell.RowIndex;
				}
				this.currentCellAddress.X = this.currentCell.ColumnIndex;
				this.currentCellAddress.Y = this.currentCell.RowIndex;
			}
			if (dataGridViewCell != this.currentCell)
			{
				if (this.currentCell != null)
				{
					if (this.currentCell.IsInEditMode)
					{
						if (!this.EndEdit())
						{
							return false;
						}
						if (this.currentCell.RowIndex == this.NewRowIndex && this.new_row_editing)
						{
							this.CancelEdit();
						}
					}
					else if (this.new_row_editing && this.currentCell.RowIndex == this.NewRowIndex)
					{
						this.CancelEdit();
					}
					this.OnCellLeave(new DataGridViewCellEventArgs(this.currentCell.ColumnIndex, this.currentCell.RowIndex));
					this.OnRowLeave(new DataGridViewCellEventArgs(this.currentCell.ColumnIndex, this.currentCell.RowIndex));
				}
				this.currentCell = dataGridViewCell;
				if (setAnchorCellAddress)
				{
					this.anchor_cell = new Point(columnIndex, rowIndex);
				}
				this.currentCellAddress = new Point(columnIndex, rowIndex);
				if (dataGridViewCell != null)
				{
					this.UpdateBindingPosition(dataGridViewCell.RowIndex);
					this.OnRowEnter(new DataGridViewCellEventArgs(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex));
					this.OnCellEnter(new DataGridViewCellEventArgs(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex));
				}
				this.OnCurrentCellChanged(EventArgs.Empty);
				if (dataGridViewCell != null)
				{
					if (this.AllowUserToAddRows && dataGridViewCell.RowIndex == this.NewRowIndex && !this.is_binding && !this.new_row_editing)
					{
						this.OnUserAddedRow(new DataGridViewRowEventArgs(this.Rows[this.NewRowIndex]));
					}
					else if (this.editMode == DataGridViewEditMode.EditOnEnter)
					{
						this.BeginEdit(true);
					}
				}
			}
			else if (dataGridViewCell != null && throughMouseClick)
			{
				this.BeginEdit(true);
			}
			return true;
		}

		/// <summary>Changes the selection state of the cell with the specified row and column indexes.</summary>
		/// <param name="columnIndex">The index of the column containing the cell.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <param name="selected">true to select the cell; false to cancel the selection of the cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x060010C0 RID: 4288 RVA: 0x00042D54 File Offset: 0x00040F54
		protected virtual void SetSelectedCellCore(int columnIndex, int rowIndex, bool selected)
		{
			this.rows[rowIndex].Cells[columnIndex].Selected = selected;
			this.OnSelectionChanged(EventArgs.Empty);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00042D8C File Offset: 0x00040F8C
		internal void SetSelectedColumnCoreInternal(int columnIndex, bool selected)
		{
			this.SetSelectedColumnCore(columnIndex, selected);
		}

		/// <summary>Changes the selection state of the column with the specified index.</summary>
		/// <param name="columnIndex">The index of the column.</param>
		/// <param name="selected">true to select the column; false to cancel the selection of the column.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0 or greater than the number of columns in the control minus 1.</exception>
		// Token: 0x060010C2 RID: 4290 RVA: 0x00042D98 File Offset: 0x00040F98
		protected virtual void SetSelectedColumnCore(int columnIndex, bool selected)
		{
			if (this.selectionMode != DataGridViewSelectionMode.ColumnHeaderSelect && this.selectionMode != DataGridViewSelectionMode.FullColumnSelect)
			{
				return;
			}
			DataGridViewColumn dataGridViewColumn = this.columns[columnIndex];
			dataGridViewColumn.SelectedInternal = selected;
			if (this.selected_columns == null)
			{
				this.selected_columns = new DataGridViewSelectedColumnCollection();
			}
			if (!selected && this.selected_columns.Contains(dataGridViewColumn))
			{
				this.selected_columns.InternalRemove(dataGridViewColumn);
			}
			else if (selected && !this.selected_columns.Contains(dataGridViewColumn))
			{
				this.selected_columns.InternalAdd(dataGridViewColumn);
			}
			base.Invalidate();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00042E3C File Offset: 0x0004103C
		internal void SetSelectedRowCoreInternal(int rowIndex, bool selected)
		{
			if (rowIndex >= 0 && rowIndex < this.Rows.Count)
			{
				this.SetSelectedRowCore(rowIndex, selected);
			}
		}

		/// <summary>Changes the selection state of the row with the specified index.</summary>
		/// <param name="rowIndex">The index of the row.</param>
		/// <param name="selected">true to select the row; false to cancel the selection of the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		// Token: 0x060010C4 RID: 4292 RVA: 0x00042E6C File Offset: 0x0004106C
		protected virtual void SetSelectedRowCore(int rowIndex, bool selected)
		{
			DataGridViewRow dataGridViewRow = this.rows[rowIndex];
			dataGridViewRow.SelectedInternal = selected;
			if (this.selected_rows == null)
			{
				this.selected_rows = new DataGridViewSelectedRowCollection(this);
			}
			if (!selected && this.selected_rows.Contains(dataGridViewRow))
			{
				this.selected_rows.InternalRemove(dataGridViewRow);
			}
			else if (selected && !this.selected_rows.Contains(dataGridViewRow))
			{
				this.selected_rows.InternalAdd(dataGridViewRow);
			}
			base.Invalidate();
		}

		/// <summary>Processes window messages.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		// Token: 0x060010C5 RID: 4293 RVA: 0x00042EF8 File Offset: 0x000410F8
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00042F04 File Offset: 0x00041104
		internal void InternalOnCellClick(DataGridViewCellEventArgs e)
		{
			this.OnCellClick(e);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00042F10 File Offset: 0x00041110
		internal void InternalOnCellContentClick(DataGridViewCellEventArgs e)
		{
			this.OnCellContentClick(e);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00042F1C File Offset: 0x0004111C
		internal void InternalOnCellContentDoubleClick(DataGridViewCellEventArgs e)
		{
			this.OnCellContentDoubleClick(e);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00042F28 File Offset: 0x00041128
		internal void InternalOnCellValueChanged(DataGridViewCellEventArgs e)
		{
			this.OnCellValueChanged(e);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00042F34 File Offset: 0x00041134
		internal void InternalOnDataError(DataGridViewDataErrorEventArgs e)
		{
			this.OnDataError(false, e);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00042F40 File Offset: 0x00041140
		internal void InternalOnMouseWheel(MouseEventArgs e)
		{
			this.OnMouseWheel(e);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00042F4C File Offset: 0x0004114C
		internal void OnHScrollBarScroll(object sender, ScrollEventArgs e)
		{
			int num = this.Columns.Count - this.DisplayedColumnCount(false);
			this.horizontalScrollingOffset = e.NewValue;
			int num2 = 0;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.Columns[i];
				if (dataGridViewColumn.Index >= num)
				{
					this.first_col_index = num;
					base.Invalidate();
					this.OnScroll(e);
				}
				else if (e.NewValue < num2 + dataGridViewColumn.Width)
				{
					if (this.first_col_index != i)
					{
						this.first_col_index = i;
						base.Invalidate();
						this.OnScroll(e);
					}
					return;
				}
				num2 += dataGridViewColumn.Width;
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00043008 File Offset: 0x00041208
		internal void OnVScrollBarScroll(object sender, ScrollEventArgs e)
		{
			this.verticalScrollingOffset = e.NewValue;
			if (this.Rows.Count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = this.Rows.Count - this.DisplayedRowCount(false);
			for (int i = 0; i < this.Rows.Count; i++)
			{
				DataGridViewRow dataGridViewRow = this.Rows[i];
				if (dataGridViewRow.Visible)
				{
					if (dataGridViewRow.Index >= num2)
					{
						this.first_row_index = num2;
						base.Invalidate();
						this.OnScroll(e);
					}
					else if (e.NewValue < num + dataGridViewRow.Height)
					{
						if (this.first_row_index != i)
						{
							this.first_row_index = i;
							base.Invalidate();
							this.OnScroll(e);
						}
						return;
					}
					num += dataGridViewRow.Height;
				}
			}
			this.first_row_index = num2;
			base.Invalidate();
			this.OnScroll(e);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000430F8 File Offset: 0x000412F8
		internal void RaiseCellStyleChanged(DataGridViewCellEventArgs e)
		{
			this.OnCellStyleChanged(e);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00043104 File Offset: 0x00041304
		internal void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			switch (e.Action)
			{
			case 1:
				this.OnColumnAddedInternal(new DataGridViewColumnEventArgs(e.Element as DataGridViewColumn));
				break;
			case 2:
				this.OnColumnPostRemovedInternal(new DataGridViewColumnEventArgs(e.Element as DataGridViewColumn));
				break;
			case 3:
				this.hover_cell = null;
				this.MoveCurrentCell(-1, -1, true, false, false, true);
				break;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00043180 File Offset: 0x00041380
		internal void AutoResizeColumnsInternal()
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				this.AutoResizeColumnInternal(i, this.Columns[i].InheritedAutoSizeMode);
			}
			this.AutoFillColumnsInternal();
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000431C8 File Offset: 0x000413C8
		internal void AutoFillColumnsInternal()
		{
			float num = 0f;
			int num2 = 0;
			int num3 = base.ClientSize.Width - ((!this.verticalScrollBar.VisibleInternal) ? 0 : this.verticalScrollBar.Width);
			if (this.RowHeadersVisible)
			{
				num3 -= this.RowHeadersWidth;
			}
			num3 -= this.BorderWidth * 2;
			int[] array = new int[this.Columns.Count];
			int[] array2 = new int[this.Columns.Count];
			for (int i = 0; i < this.Columns.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = this.Columns[i];
				if (dataGridViewColumn.Visible)
				{
					DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = dataGridViewColumn.InheritedAutoSizeMode;
					switch (inheritedAutoSizeMode)
					{
					case DataGridViewAutoSizeColumnMode.NotSet:
					case DataGridViewAutoSizeColumnMode.None:
					case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
					case DataGridViewAutoSizeColumnMode.AllCells:
					case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
					case DataGridViewAutoSizeColumnMode.DisplayedCells:
						num3 -= this.Columns[i].Width;
						break;
					default:
						if (inheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
						{
							num2++;
							num += dataGridViewColumn.FillWeight;
						}
						break;
					}
				}
			}
			num3 = Math.Max(0, num3);
			bool flag;
			do
			{
				flag = false;
				for (int j = 0; j < this.columns.Count; j++)
				{
					DataGridViewColumn dataGridViewColumn2 = this.Columns[j];
					if (dataGridViewColumn2.InheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
					{
						if (dataGridViewColumn2.Visible)
						{
							if (array[j] == 0)
							{
								int num4 = ((num != 0f) ? ((int)Math.Round((double)((float)num3 * (dataGridViewColumn2.FillWeight / num)), 0)) : 0);
								if (num4 < 0)
								{
									num4 = 0;
								}
								if (num4 < dataGridViewColumn2.MinimumWidth)
								{
									num4 = dataGridViewColumn2.MinimumWidth;
									array[j] = num4;
									flag = true;
									num3 -= num4;
									num -= dataGridViewColumn2.FillWeight;
								}
								array2[j] = num4;
							}
						}
					}
				}
			}
			while (flag);
			for (int k = 0; k < this.columns.Count; k++)
			{
				if (this.Columns[k].InheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
				{
					if (this.Columns[k].Visible)
					{
						this.Columns[k].Width = array2[k];
					}
				}
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00043458 File Offset: 0x00041658
		internal void AutoResizeColumnInternal(int columnIndex, DataGridViewAutoSizeColumnMode mode)
		{
			DataGridViewColumn dataGridViewColumn = this.Columns[columnIndex];
			int num;
			switch (mode)
			{
			case DataGridViewAutoSizeColumnMode.ColumnHeader:
				num = dataGridViewColumn.HeaderCell.ContentBounds.Width;
				break;
			default:
				if (mode == DataGridViewAutoSizeColumnMode.Fill)
				{
					return;
				}
				num = dataGridViewColumn.Width;
				break;
			case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.AllCells:
			case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.DisplayedCells:
				num = Math.Max(this.CalculateColumnCellWidth(columnIndex, dataGridViewColumn.InheritedAutoSizeMode), dataGridViewColumn.HeaderCell.ContentBounds.Width);
				break;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (num < dataGridViewColumn.MinimumWidth)
			{
				num = dataGridViewColumn.MinimumWidth;
			}
			dataGridViewColumn.Width = num;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00043524 File Offset: 0x00041724
		internal int CalculateColumnCellWidth(int index, DataGridViewAutoSizeColumnMode mode)
		{
			int num = 0;
			int num2 = this.Rows.Count;
			int num3 = 0;
			if (mode == DataGridViewAutoSizeColumnMode.DisplayedCells || mode == DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader)
			{
				num = this.first_row_index;
				num2 = this.DisplayedRowCount(true);
			}
			for (int i = num; i < num2; i++)
			{
				if (this.Rows[i].Visible)
				{
					int width = this.Rows[i].Cells[index].PreferredSize.Width;
					num3 = Math.Max(num3, width);
				}
			}
			return num3;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000435C0 File Offset: 0x000417C0
		private Rectangle GetHeaderCellBounds(DataGridViewHeaderCell cell)
		{
			Rectangle rectangle;
			rectangle..ctor(base.ClientRectangle.Location, cell.Size);
			if (cell is DataGridViewColumnHeaderCell)
			{
				if (this.RowHeadersVisible)
				{
					rectangle.X += this.RowHeadersWidth;
				}
				List<DataGridViewColumn> columnDisplayIndexSortedArrayList = this.columns.ColumnDisplayIndexSortedArrayList;
				for (int i = this.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = columnDisplayIndexSortedArrayList[i];
					if (dataGridViewColumn.Index == cell.ColumnIndex)
					{
						break;
					}
					rectangle.X += dataGridViewColumn.Width;
				}
			}
			else
			{
				if (this.ColumnHeadersVisible)
				{
					rectangle.Y += this.ColumnHeadersHeight;
				}
				for (int j = this.first_row_index; j < this.Rows.Count; j++)
				{
					DataGridViewRow rowInternal = this.GetRowInternal(j);
					if (rowInternal.HeaderCell == cell)
					{
						break;
					}
					rectangle.Y += rowInternal.Height;
				}
			}
			return rectangle;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000436E8 File Offset: 0x000418E8
		private void PrepareEditingRow(bool cell_changed, bool column_changed)
		{
			if (this.new_row_editing)
			{
				return;
			}
			bool flag = this.ColumnCount > 0 && this.AllowUserToAddRows;
			if (!flag)
			{
				this.RemoveEditingRow();
			}
			else if (flag)
			{
				if (this.editing_row != null && (cell_changed || column_changed))
				{
					this.RemoveEditingRow();
				}
				if (this.editing_row == null)
				{
					this.editing_row = this.RowTemplateFull;
					this.Rows.AddInternal(this.editing_row, false);
				}
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00043778 File Offset: 0x00041978
		internal void RemoveEditingRow()
		{
			if (this.editing_row != null)
			{
				if (this.Rows.Contains(this.editing_row))
				{
					this.Rows.RemoveInternal(this.editing_row);
				}
				this.editing_row = null;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000437C0 File Offset: 0x000419C0
		internal DataGridViewRow EditingRow
		{
			get
			{
				return this.editing_row;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000437C8 File Offset: 0x000419C8
		private void AddBoundRow(object element)
		{
			if (this.ColumnCount == 0)
			{
				return;
			}
			DataGridViewRow rowTemplateFull = this.RowTemplateFull;
			this.rows.AddInternal(rowTemplateFull, false);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000437F8 File Offset: 0x000419F8
		private bool IsColumnAlreadyBound(string name)
		{
			foreach (object obj in this.Columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (string.Compare(dataGridViewColumn.DataPropertyName, name, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004387C File Offset: 0x00041A7C
		private DataGridViewColumn CreateColumnByType(Type type)
		{
			if (type == typeof(bool))
			{
				return new DataGridViewCheckBoxColumn();
			}
			if (typeof(Bitmap).IsAssignableFrom(type))
			{
				return new DataGridViewImageColumn();
			}
			return new DataGridViewTextBoxColumn();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000438C0 File Offset: 0x00041AC0
		private void ClearBinding()
		{
			if (this.IsCurrentCellInEditMode && !this.EndEdit())
			{
				this.CancelEdit();
			}
			this.MoveCurrentCell(-1, -1, false, false, false, true);
			if (this.DataManager != null)
			{
				this.DataManager.ListChanged -= new ListChangedEventHandler(this.OnListChanged);
				this.DataManager.PositionChanged -= new EventHandler(this.OnListPositionChanged);
				this.columns.ClearAutoGeneratedColumns();
				this.rows.Clear();
				this.RemoveEditingRow();
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004394C File Offset: 0x00041B4C
		private void ResetRows()
		{
			this.rows.Clear();
			this.RemoveEditingRow();
			if (this.DataManager != null)
			{
				foreach (object obj in this.DataManager.List)
				{
					this.AddBoundRow(obj);
				}
			}
			this.PrepareEditingRow(false, true);
			this.OnListPositionChanged(this, EventArgs.Empty);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000439EC File Offset: 0x00041BEC
		private void DoBinding()
		{
			if (this.dataSource != null && this.DataManager != null)
			{
				if (this.autoGenerateColumns)
				{
					this.is_autogenerating_columns = true;
					foreach (object obj in this.DataManager.GetItemProperties())
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
						if (!typeof(ICollection).IsAssignableFrom(propertyDescriptor.PropertyType))
						{
							if (propertyDescriptor.IsBrowsable)
							{
								if (!this.IsColumnAlreadyBound(propertyDescriptor.Name))
								{
									DataGridViewColumn dataGridViewColumn = this.CreateColumnByType(propertyDescriptor.PropertyType);
									dataGridViewColumn.Name = propertyDescriptor.DisplayName;
									dataGridViewColumn.DataPropertyName = propertyDescriptor.Name;
									dataGridViewColumn.ReadOnly = !this.DataManager.AllowEdit || propertyDescriptor.IsReadOnly;
									dataGridViewColumn.SetIsDataBound(true);
									dataGridViewColumn.ValueType = propertyDescriptor.PropertyType;
									dataGridViewColumn.AutoGenerated = true;
									this.columns.Add(dataGridViewColumn);
								}
							}
						}
					}
					this.is_autogenerating_columns = false;
				}
				foreach (object obj2 in this.columns)
				{
					DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)obj2;
					dataGridViewColumn2.DataColumnIndex = this.FindDataColumnIndex(dataGridViewColumn2);
					if (dataGridViewColumn2.DataColumnIndex != -1)
					{
						dataGridViewColumn2.SetIsDataBound(true);
					}
				}
				foreach (object obj3 in this.DataManager.List)
				{
					this.AddBoundRow(obj3);
				}
				this.DataManager.ListChanged += new ListChangedEventHandler(this.OnListChanged);
				this.DataManager.PositionChanged += new EventHandler(this.OnListPositionChanged);
				this.OnDataBindingComplete(new DataGridViewBindingCompleteEventArgs(0));
				this.OnListPositionChanged(this, EventArgs.Empty);
			}
			else if (this.Rows.Count > 0 && this.Columns.Count > 0)
			{
				this.MoveCurrentCell(0, 0, true, false, false, false);
			}
			this.PrepareEditingRow(false, true);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00043CA4 File Offset: 0x00041EA4
		private void MoveCurrentCell(int x, int y, bool select, bool isControl, bool isShift, bool scroll)
		{
			if (x == -1 || y == -1)
			{
				y = (x = -1);
			}
			else
			{
				if (x < 0 || x > this.Columns.Count - 1)
				{
					throw new ArgumentOutOfRangeException("x");
				}
				if (y < 0 || y > this.Rows.Count - 1)
				{
					throw new ArgumentOutOfRangeException("y");
				}
				if (!this.Rows[y].Visible)
				{
					for (int i = y; i < this.Rows.Count; i++)
					{
						if (this.Rows[i].Visible)
						{
							y = i;
							break;
						}
					}
				}
				if (!this.Columns[x].Visible)
				{
					for (int j = x; j < this.Columns.Count; j++)
					{
						if (this.Columns[j].Visible)
						{
							x = j;
							break;
						}
					}
				}
				if (!this.Rows[y].Visible || !this.Columns[x].Visible)
				{
					y = (x = -1);
				}
			}
			if (!this.SetCurrentCellAddressCore(x, y, true, false, false))
			{
				this.ClearSelection();
				return;
			}
			if (x == -1 && y == -1)
			{
				this.ClearSelection();
				return;
			}
			bool selected = this.Rows.SharedRow(this.CurrentCellAddress.Y).Selected;
			bool selected2 = this.Columns[this.CurrentCellAddress.X].Selected;
			DataGridViewSelectionMode dataGridViewSelectionMode = this.selectionMode;
			if (dataGridViewSelectionMode == DataGridViewSelectionMode.RowHeaderSelect && (x == -1 || (selected && this.CurrentCellAddress.X == x)))
			{
				dataGridViewSelectionMode = DataGridViewSelectionMode.FullRowSelect;
			}
			else if (dataGridViewSelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
			{
				dataGridViewSelectionMode = DataGridViewSelectionMode.CellSelect;
			}
			if (dataGridViewSelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect && (y == -1 || (selected2 && this.CurrentCellAddress.Y == y)))
			{
				dataGridViewSelectionMode = DataGridViewSelectionMode.FullColumnSelect;
			}
			else if (dataGridViewSelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
			{
				dataGridViewSelectionMode = DataGridViewSelectionMode.CellSelect;
			}
			if (scroll)
			{
				int num = this.ColumnIndexToDisplayIndex(x);
				bool flag = false;
				int num2 = this.DisplayedColumnCount(false);
				int num3 = 0;
				if (num < this.first_col_index)
				{
					this.RefreshScrollBars();
					flag = true;
					if (num == 0)
					{
						num3 = this.horizontalScrollBar.Value;
					}
					else
					{
						if (this.first_col_index >= this.ColumnCount)
						{
							this.first_col_index = this.ColumnCount - 1;
						}
						for (int k = num; k < this.first_col_index; k++)
						{
							num3 += this.Columns[this.ColumnDisplayIndexToIndex(k)].Width;
						}
					}
					this.horizontalScrollBar.SafeValueSet(this.horizontalScrollBar.Value - num3);
					this.OnHScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.horizontalScrollBar.Value));
				}
				else if (num > this.first_col_index + num2 - 1)
				{
					this.RefreshScrollBars();
					flag = true;
					if (num == this.Columns.Count - 1)
					{
						num3 = this.horizontalScrollBar.Maximum - this.horizontalScrollBar.Value;
					}
					else
					{
						for (int l = this.first_col_index + num2 - 1; l < num; l++)
						{
							num3 += this.Columns[this.ColumnDisplayIndexToIndex(l)].Width;
						}
					}
					this.horizontalScrollBar.SafeValueSet(this.horizontalScrollBar.Value + num3);
					this.OnHScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.horizontalScrollBar.Value));
				}
				int num4 = y;
				int num5 = this.DisplayedRowCount(false);
				int num6 = 0;
				if (num4 < this.first_row_index)
				{
					if (!flag)
					{
						this.RefreshScrollBars();
					}
					if (num4 == 0)
					{
						num6 = this.verticalScrollBar.Value;
					}
					else
					{
						if (this.first_row_index >= this.RowCount)
						{
							this.first_row_index = this.RowCount - 1;
						}
						for (int m = num4; m < this.first_row_index; m++)
						{
							num6 += this.GetRowInternal(m).Height;
						}
					}
					this.verticalScrollBar.SafeValueSet(this.verticalScrollBar.Value - num6);
					this.OnVScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.verticalScrollBar.Value));
				}
				else if (num4 > this.first_row_index + num5 - 1)
				{
					if (!flag)
					{
						this.RefreshScrollBars();
					}
					if (num4 == this.Rows.Count - 1)
					{
						num6 = this.verticalScrollBar.Maximum - this.verticalScrollBar.Value;
					}
					else
					{
						for (int n = this.first_row_index + num5 - 1; n < num4; n++)
						{
							num6 += this.GetRowInternal(n).Height;
						}
					}
					this.verticalScrollBar.SafeValueSet(this.verticalScrollBar.Value + num6);
					this.OnVScrollBarScroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.verticalScrollBar.Value));
				}
			}
			if (!select)
			{
				return;
			}
			if (!isShift)
			{
				this.ClearSelection();
			}
			switch (dataGridViewSelectionMode)
			{
			case DataGridViewSelectionMode.CellSelect:
				this.SetSelectedCellCore(x, y, true);
				break;
			case DataGridViewSelectionMode.FullRowSelect:
				this.SetSelectedRowCore(y, true);
				break;
			case DataGridViewSelectionMode.FullColumnSelect:
				this.SetSelectedColumnCore(x, true);
				break;
			}
			base.Invalidate();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00044244 File Offset: 0x00042444
		private int ColumnIndexToDisplayIndex(int index)
		{
			if (index == -1)
			{
				return index;
			}
			return this.Columns[index].DisplayIndex;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00044260 File Offset: 0x00042460
		private int ColumnDisplayIndexToIndex(int index)
		{
			return this.Columns.ColumnDisplayIndexSortedArrayList[index].Index;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00044284 File Offset: 0x00042484
		private void OnListChanged(object sender, ListChangedEventArgs args)
		{
			switch (args.ListChangedType)
			{
			case 1:
				this.AddBoundRow(this.DataManager[args.NewIndex]);
				goto IL_0066;
			case 2:
				this.Rows.RemoveAtInternal(args.NewIndex);
				goto IL_0066;
			case 4:
				goto IL_0066;
			}
			this.ResetRows();
			IL_0066:
			base.Invalidate();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00044300 File Offset: 0x00042500
		private void OnListPositionChanged(object sender, EventArgs args)
		{
			if (this.Rows.Count > 0 && this.Columns.Count > 0 && this.DataManager.Position != -1)
			{
				this.MoveCurrentCell((this.currentCell == null) ? 0 : this.currentCell.ColumnIndex, this.DataManager.Position, true, false, false, true);
			}
			else
			{
				this.MoveCurrentCell(-1, -1, true, false, false, true);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00044384 File Offset: 0x00042584
		private void ReBind()
		{
			if (!this.is_binding)
			{
				base.SuspendLayout();
				this.is_binding = true;
				this.ClearBinding();
				this.DoBinding();
				this.is_binding = false;
				base.ResumeLayout(true);
				base.Invalidate();
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000443CC File Offset: 0x000425CC
		private bool MouseOverColumnResize(int col, int mousex)
		{
			if (!this.allowUserToResizeColumns)
			{
				return false;
			}
			Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(col, 0, false);
			return mousex >= cellDisplayRectangle.Right - 4 && mousex <= cellDisplayRectangle.Right;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00044410 File Offset: 0x00042610
		private bool MouseOverRowResize(int row, int mousey)
		{
			if (!this.allowUserToResizeRows)
			{
				return false;
			}
			Rectangle cellDisplayRectangle = this.GetCellDisplayRectangle(0, row, false);
			return mousey >= cellDisplayRectangle.Bottom - 4 && mousey <= cellDisplayRectangle.Bottom;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00044454 File Offset: 0x00042654
		private void DrawVerticalResizeLine(int x)
		{
			Rectangle rectangle;
			rectangle..ctor(x, this.Bounds.Y + 3 + ((!this.ColumnHeadersVisible) ? 0 : this.ColumnHeadersHeight), 1, this.Bounds.Height - 3 - ((!this.ColumnHeadersVisible) ? 0 : this.ColumnHeadersHeight));
			XplatUI.DrawReversibleRectangle(this.Handle, rectangle, 2);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000444C8 File Offset: 0x000426C8
		private void DrawHorizontalResizeLine(int y)
		{
			Rectangle rectangle;
			rectangle..ctor(this.Bounds.X + 3 + ((!this.RowHeadersVisible) ? 0 : this.RowHeadersWidth), y, this.Bounds.Width - 3 + ((!this.RowHeadersVisible) ? 0 : this.RowHeadersWidth), 1);
			XplatUI.DrawReversibleRectangle(this.Handle, rectangle, 2);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004453C File Offset: 0x0004273C
		private void MouseEnteredErrorIcon(DataGridViewCell item)
		{
			this.tooltip_currently_showing = item;
			this.ToolTipTimer.Start();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00044550 File Offset: 0x00042750
		private void MouseLeftErrorIcon(DataGridViewCell item)
		{
			this.ToolTipTimer.Stop();
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0004457C File Offset: 0x0004277C
		private Timer ToolTipTimer
		{
			get
			{
				if (this.tooltip_timer == null)
				{
					this.tooltip_timer = new Timer();
					this.tooltip_timer.Enabled = false;
					this.tooltip_timer.Interval = 500;
					this.tooltip_timer.Tick += new EventHandler(this.ToolTipTimer_Tick);
				}
				return this.tooltip_timer;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x000445D8 File Offset: 0x000427D8
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000445F8 File Offset: 0x000427F8
		private void ToolTipTimer_Tick(object o, EventArgs args)
		{
			string errorText = this.tooltip_currently_showing.ErrorText;
			if (!string.IsNullOrEmpty(errorText))
			{
				this.ToolTipWindow.Present(this, errorText);
			}
			this.ToolTipTimer.Stop();
		}

		// Token: 0x040009BE RID: 2494
		private DataGridViewAdvancedBorderStyle adjustedTopLeftHeaderBorderStyle;

		// Token: 0x040009BF RID: 2495
		private DataGridViewAdvancedBorderStyle advancedCellBorderStyle;

		// Token: 0x040009C0 RID: 2496
		private DataGridViewAdvancedBorderStyle advancedColumnHeadersBorderStyle;

		// Token: 0x040009C1 RID: 2497
		private DataGridViewAdvancedBorderStyle advancedRowHeadersBorderStyle;

		// Token: 0x040009C2 RID: 2498
		private bool allowUserToAddRows;

		// Token: 0x040009C3 RID: 2499
		private bool allowUserToDeleteRows;

		// Token: 0x040009C4 RID: 2500
		private bool allowUserToOrderColumns;

		// Token: 0x040009C5 RID: 2501
		private bool allowUserToResizeColumns;

		// Token: 0x040009C6 RID: 2502
		private bool allowUserToResizeRows;

		// Token: 0x040009C7 RID: 2503
		private DataGridViewCellStyle alternatingRowsDefaultCellStyle;

		// Token: 0x040009C8 RID: 2504
		private Point anchor_cell;

		// Token: 0x040009C9 RID: 2505
		private bool autoGenerateColumns;

		// Token: 0x040009CA RID: 2506
		private bool autoSize;

		// Token: 0x040009CB RID: 2507
		private DataGridViewAutoSizeColumnsMode autoSizeColumnsMode;

		// Token: 0x040009CC RID: 2508
		private DataGridViewAutoSizeRowsMode autoSizeRowsMode;

		// Token: 0x040009CD RID: 2509
		private Color backColor;

		// Token: 0x040009CE RID: 2510
		private Color backgroundColor;

		// Token: 0x040009CF RID: 2511
		private Image backgroundImage;

		// Token: 0x040009D0 RID: 2512
		private BorderStyle borderStyle;

		// Token: 0x040009D1 RID: 2513
		private DataGridViewCellBorderStyle cellBorderStyle;

		// Token: 0x040009D2 RID: 2514
		private DataGridViewClipboardCopyMode clipboardCopyMode;

		// Token: 0x040009D3 RID: 2515
		private DataGridViewHeaderBorderStyle columnHeadersBorderStyle;

		// Token: 0x040009D4 RID: 2516
		private DataGridViewCellStyle columnHeadersDefaultCellStyle;

		// Token: 0x040009D5 RID: 2517
		private int columnHeadersHeight;

		// Token: 0x040009D6 RID: 2518
		private DataGridViewColumnHeadersHeightSizeMode columnHeadersHeightSizeMode;

		// Token: 0x040009D7 RID: 2519
		private bool columnHeadersVisible;

		// Token: 0x040009D8 RID: 2520
		private DataGridViewColumnCollection columns;

		// Token: 0x040009D9 RID: 2521
		private DataGridViewCell currentCell;

		// Token: 0x040009DA RID: 2522
		private Point currentCellAddress;

		// Token: 0x040009DB RID: 2523
		private DataGridViewRow currentRow;

		// Token: 0x040009DC RID: 2524
		private string dataMember;

		// Token: 0x040009DD RID: 2525
		private object dataSource;

		// Token: 0x040009DE RID: 2526
		private DataGridViewCellStyle defaultCellStyle;

		// Token: 0x040009DF RID: 2527
		private DataGridViewEditMode editMode;

		// Token: 0x040009E0 RID: 2528
		private bool enableHeadersVisualStyles = true;

		// Token: 0x040009E1 RID: 2529
		private DataGridViewCell firstDisplayedCell;

		// Token: 0x040009E2 RID: 2530
		private int firstDisplayedScrollingColumnHiddenWidth;

		// Token: 0x040009E3 RID: 2531
		private int firstDisplayedScrollingColumnIndex;

		// Token: 0x040009E4 RID: 2532
		private int firstDisplayedScrollingRowIndex;

		// Token: 0x040009E5 RID: 2533
		private Color gridColor = Color.FromKnownColor(6);

		// Token: 0x040009E6 RID: 2534
		private int horizontalScrollingOffset;

		// Token: 0x040009E7 RID: 2535
		private DataGridViewCell hover_cell;

		// Token: 0x040009E8 RID: 2536
		private bool isCurrentCellDirty;

		// Token: 0x040009E9 RID: 2537
		private bool multiSelect;

		// Token: 0x040009EA RID: 2538
		private bool readOnly;

		// Token: 0x040009EB RID: 2539
		private DataGridViewHeaderBorderStyle rowHeadersBorderStyle;

		// Token: 0x040009EC RID: 2540
		private DataGridViewCellStyle rowHeadersDefaultCellStyle;

		// Token: 0x040009ED RID: 2541
		private bool rowHeadersVisible;

		// Token: 0x040009EE RID: 2542
		private int rowHeadersWidth;

		// Token: 0x040009EF RID: 2543
		private DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode;

		// Token: 0x040009F0 RID: 2544
		private DataGridViewRowCollection rows;

		// Token: 0x040009F1 RID: 2545
		private DataGridViewCellStyle rowsDefaultCellStyle;

		// Token: 0x040009F2 RID: 2546
		private DataGridViewRow rowTemplate;

		// Token: 0x040009F3 RID: 2547
		private ScrollBars scrollBars;

		// Token: 0x040009F4 RID: 2548
		private DataGridViewSelectionMode selectionMode;

		// Token: 0x040009F5 RID: 2549
		private bool showCellErrors;

		// Token: 0x040009F6 RID: 2550
		private bool showCellToolTips;

		// Token: 0x040009F7 RID: 2551
		private bool showEditingIcon;

		// Token: 0x040009F8 RID: 2552
		private bool showRowErrors;

		// Token: 0x040009F9 RID: 2553
		private DataGridViewColumn sortedColumn;

		// Token: 0x040009FA RID: 2554
		private SortOrder sortOrder;

		// Token: 0x040009FB RID: 2555
		private bool standardTab;

		// Token: 0x040009FC RID: 2556
		private DataGridViewHeaderCell topLeftHeaderCell;

		// Token: 0x040009FD RID: 2557
		private Cursor userSetCursor;

		// Token: 0x040009FE RID: 2558
		private int verticalScrollingOffset;

		// Token: 0x040009FF RID: 2559
		private bool virtualMode;

		// Token: 0x04000A00 RID: 2560
		private HScrollBar horizontalScrollBar;

		// Token: 0x04000A01 RID: 2561
		private VScrollBar verticalScrollBar;

		// Token: 0x04000A02 RID: 2562
		private Control editingControl;

		// Token: 0x04000A03 RID: 2563
		private bool is_autogenerating_columns;

		// Token: 0x04000A04 RID: 2564
		private bool is_binding;

		// Token: 0x04000A05 RID: 2565
		private bool new_row_editing;

		// Token: 0x04000A06 RID: 2566
		private int selected_row = -1;

		// Token: 0x04000A07 RID: 2567
		private int selected_column = -1;

		// Token: 0x04000A08 RID: 2568
		private Timer tooltip_timer;

		// Token: 0x04000A09 RID: 2569
		private ToolTip tooltip_window;

		// Token: 0x04000A0A RID: 2570
		private DataGridViewCell tooltip_currently_showing;

		// Token: 0x04000A0B RID: 2571
		private DataGridViewSelectedRowCollection selected_rows;

		// Token: 0x04000A0C RID: 2572
		private DataGridViewSelectedColumnCollection selected_columns;

		// Token: 0x04000A0D RID: 2573
		private DataGridViewRow editing_row;

		// Token: 0x04000A0E RID: 2574
		private DataGridViewHeaderCell pressed_header_cell;

		// Token: 0x04000A0F RID: 2575
		private DataGridViewHeaderCell entered_header_cell;

		// Token: 0x04000A10 RID: 2576
		private bool column_resize_active;

		// Token: 0x04000A11 RID: 2577
		private bool row_resize_active;

		// Token: 0x04000A12 RID: 2578
		private int resize_band = -1;

		// Token: 0x04000A13 RID: 2579
		private int resize_band_start;

		// Token: 0x04000A14 RID: 2580
		private int resize_band_delta;

		// Token: 0x04000A8B RID: 2699
		private int first_row_index;

		// Token: 0x04000A8C RID: 2700
		internal int first_col_index;

		// Token: 0x020000D0 RID: 208
		private class ColumnSorter : IComparer
		{
			// Token: 0x060010ED RID: 4333 RVA: 0x00044634 File Offset: 0x00042834
			public ColumnSorter(DataGridViewColumn column, ListSortDirection direction, bool numeric)
			{
				this.column = column.Index;
				this.numeric_sort = numeric;
				if (direction == 1)
				{
					this.direction = -1;
				}
			}

			// Token: 0x060010EE RID: 4334 RVA: 0x00044670 File Offset: 0x00042870
			public int Compare(object x, object y)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)x;
				DataGridViewRow dataGridViewRow2 = (DataGridViewRow)y;
				if (dataGridViewRow.Cells[this.column].ValueType == typeof(DateTime) && dataGridViewRow2.Cells[this.column].ValueType == typeof(DateTime))
				{
					return DateTime.Compare((DateTime)dataGridViewRow.Cells[this.column].Value, (DateTime)dataGridViewRow2.Cells[this.column].Value) * this.direction;
				}
				object formattedValue = dataGridViewRow.Cells[this.column].FormattedValue;
				object formattedValue2 = dataGridViewRow2.Cells[this.column].FormattedValue;
				object nullValue = dataGridViewRow.Cells[this.column].InheritedStyle.NullValue;
				object nullValue2 = dataGridViewRow2.Cells[this.column].InheritedStyle.NullValue;
				if (formattedValue == nullValue && formattedValue2 == nullValue2)
				{
					return 0;
				}
				if (formattedValue == nullValue)
				{
					return this.direction;
				}
				if (formattedValue2 == nullValue2)
				{
					return -1 * this.direction;
				}
				if (this.numeric_sort)
				{
					return (int)(double.Parse(formattedValue.ToString()) - double.Parse(formattedValue2.ToString())) * this.direction;
				}
				return string.Compare(formattedValue.ToString(), formattedValue2.ToString()) * this.direction;
			}

			// Token: 0x04000A8D RID: 2701
			private int column;

			// Token: 0x04000A8E RID: 2702
			private int direction = 1;

			// Token: 0x04000A8F RID: 2703
			private bool numeric_sort;
		}

		/// <summary>Contains information, such as the row and column indexes, about a specific coordinate pair in the <see cref="T:System.Windows.Forms.DataGridView" /> control. This class cannot be inherited. </summary>
		// Token: 0x020000D1 RID: 209
		public sealed class HitTestInfo
		{
			// Token: 0x060010EF RID: 4335 RVA: 0x000447F8 File Offset: 0x000429F8
			internal HitTestInfo(int columnIndex, int columnX, int rowIndex, int rowY, DataGridViewHitTestType type)
			{
				this.columnIndex = columnIndex;
				this.columnX = columnX;
				this.rowIndex = rowIndex;
				this.rowY = rowY;
				this.type = type;
			}

			/// <summary>Gets the index of the column that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</summary>
			/// <returns>The index of the column in the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x1700038C RID: 908
			// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004483C File Offset: 0x00042A3C
			public int ColumnIndex
			{
				get
				{
					return this.columnIndex;
				}
			}

			/// <summary>Gets the x-coordinate of the beginning of the column that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</summary>
			/// <returns>The x-coordinate of the column in the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x1700038D RID: 909
			// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00044844 File Offset: 0x00042A44
			public int ColumnX
			{
				get
				{
					return this.columnX;
				}
			}

			/// <summary>Gets the index of the row that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</summary>
			/// <returns>The index of the row in the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x1700038E RID: 910
			// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004484C File Offset: 0x00042A4C
			public int RowIndex
			{
				get
				{
					return this.rowIndex;
				}
			}

			/// <summary>Gets the y-coordinate of the top of the row that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</summary>
			/// <returns>The y-coordinate of the row in the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x1700038F RID: 911
			// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00044854 File Offset: 0x00042A54
			public int RowY
			{
				get
				{
					return this.rowY;
				}
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewHitTestType" /> that indicates which part of the <see cref="T:System.Windows.Forms.DataGridView" /> the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" /> belong to.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewHitTestType" /> value that indicates the control part at the coordinates described by the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x17000390 RID: 912
			// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004485C File Offset: 0x00042A5C
			public DataGridViewHitTestType Type
			{
				get
				{
					return this.type;
				}
			}

			/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />. </summary>
			/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" /> in which the values of the <see cref="P:System.Windows.Forms.DataGridView.HitTestInfo.Type" />, <see cref="P:System.Windows.Forms.DataGridView.HitTestInfo.RowIndex" />, and <see cref="P:System.Windows.Forms.DataGridView.HitTestInfo.ColumnIndex" /> properties are the same as the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</param>
			// Token: 0x060010F6 RID: 4342 RVA: 0x00044864 File Offset: 0x00042A64
			public override bool Equals(object value)
			{
				if (value is DataGridView.HitTestInfo)
				{
					DataGridView.HitTestInfo hitTestInfo = (DataGridView.HitTestInfo)value;
					if (hitTestInfo.columnIndex == this.columnIndex && hitTestInfo.columnX == this.columnX && hitTestInfo.rowIndex == this.rowIndex && hitTestInfo.rowY == this.rowY && hitTestInfo.type == this.type)
					{
						return true;
					}
				}
				return false;
			}

			/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
			// Token: 0x060010F7 RID: 4343 RVA: 0x000448DC File Offset: 0x00042ADC
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Returns a string that represents a <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</summary>
			/// <returns>A string that represents a <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo" />.</returns>
			// Token: 0x060010F8 RID: 4344 RVA: 0x000448E4 File Offset: 0x00042AE4
			public override string ToString()
			{
				return string.Format("Type:{0}, Column:{1}, Row:{2}", this.type, this.columnIndex, this.rowIndex);
			}

			/// <summary>Specifies that the point is not on a cell or cell header. This field is read-only.</summary>
			// Token: 0x04000A90 RID: 2704
			public static readonly DataGridView.HitTestInfo Nowhere = new DataGridView.HitTestInfo(-1, -1, -1, -1, DataGridViewHitTestType.None);

			// Token: 0x04000A91 RID: 2705
			private int columnIndex;

			// Token: 0x04000A92 RID: 2706
			private int columnX;

			// Token: 0x04000A93 RID: 2707
			private int rowIndex;

			// Token: 0x04000A94 RID: 2708
			private int rowY;

			// Token: 0x04000A95 RID: 2709
			private DataGridViewHitTestType type;
		}

		/// <summary>Represents a collection of controls contained on a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x020000D2 RID: 210
		[ComVisible(false)]
		public class DataGridViewControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</param>
			// Token: 0x060010F9 RID: 4345 RVA: 0x00044914 File Offset: 0x00042B14
			public DataGridViewControlCollection(DataGridView owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Removes all controls from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
			// Token: 0x060010FA RID: 4346 RVA: 0x00044924 File Offset: 0x00042B24
			public override void Clear()
			{
				for (int i = 0; i < this.Count; i++)
				{
					this.Remove(this[i]);
				}
			}

			/// <summary>Copies the contents of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" /> into a <see cref="T:System.Windows.Forms.Control" /> array, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Windows.Forms.Control" /> array that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
			// Token: 0x060010FB RID: 4347 RVA: 0x00044958 File Offset: 0x00042B58
			public void CopyTo(Control[] array, int index)
			{
				base.CopyTo(array, index);
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.Control" /> into the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" /> at the specified index.</summary>
			/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to insert into the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero or greater than or equal to the current number of controls in the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</exception>
			// Token: 0x060010FC RID: 4348 RVA: 0x00044964 File Offset: 0x00042B64
			public void Insert(int index, Control value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the specified control from the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove from the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection" />.</param>
			// Token: 0x060010FD RID: 4349 RVA: 0x0004496C File Offset: 0x00042B6C
			public override void Remove(Control value)
			{
				if (value == this.owner.horizontalScrollBar)
				{
					return;
				}
				if (value == this.owner.verticalScrollBar)
				{
					return;
				}
				if (value == this.owner.editingControl)
				{
					return;
				}
				base.Remove(value);
			}

			// Token: 0x060010FE RID: 4350 RVA: 0x000449AC File Offset: 0x00042BAC
			internal void RemoveInternal(Control value)
			{
				base.Remove(value);
			}

			// Token: 0x04000A96 RID: 2710
			private DataGridView owner;
		}

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DataGridView" /> control to accessibility client applications.</summary>
		// Token: 0x020000D3 RID: 211
		[ComVisible(true)]
		protected class DataGridViewAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" />.</param>
			// Token: 0x060010FF RID: 4351 RVA: 0x000449B8 File Offset: 0x00042BB8
			public DataGridViewAccessibleObject(DataGridView owner)
				: base(owner)
			{
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Table" /> value.</returns>
			// Token: 0x17000391 RID: 913
			// (get) Token: 0x06001100 RID: 4352 RVA: 0x000449C4 File Offset: 0x00042BC4
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <returns>The accessible object name.</returns>
			// Token: 0x17000392 RID: 914
			// (get) Token: 0x06001101 RID: 4353 RVA: 0x000449CC File Offset: 0x00042BCC
			public override string Name
			{
				get
				{
					return base.Name;
				}
			}

			/// <summary>Returns the child accessible object corresponding to the specified index.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child accessible object corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the child accessible object.</param>
			// Token: 0x06001102 RID: 4354 RVA: 0x000449D4 File Offset: 0x00042BD4
			public override AccessibleObject GetChild(int index)
			{
				return base.GetChild(index);
			}

			/// <summary>Returns the number of child objects belonging to an accessible object.</summary>
			/// <returns>The number of child objects belonging to the accessible object.</returns>
			// Token: 0x06001103 RID: 4355 RVA: 0x000449E0 File Offset: 0x00042BE0
			public override int GetChildCount()
			{
				return base.GetChildCount();
			}

			/// <summary>Returns the accessible object of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has the keyboard focus.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has the current focus, or null if the <see cref="T:System.Windows.Forms.DataGridView" /> does not have focus.</returns>
			// Token: 0x06001104 RID: 4356 RVA: 0x000449E8 File Offset: 0x00042BE8
			public override AccessibleObject GetFocused()
			{
				return base.GetFocused();
			}

			/// <summary>Returns an <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the selected cells in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected cells, or null if no cells are selected or if the object itself does not have focus.</returns>
			// Token: 0x06001105 RID: 4357 RVA: 0x000449F0 File Offset: 0x00042BF0
			public override AccessibleObject GetSelected()
			{
				return base.GetSelected();
			}

			/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object at the given screen coordinates, or null if no object is at the specified location.</returns>
			/// <param name="x">The horizontal screen coordinate.</param>
			/// <param name="y">The vertical screen coordinate.</param>
			// Token: 0x06001106 RID: 4358 RVA: 0x000449F8 File Offset: 0x00042BF8
			public override AccessibleObject HitTest(int x, int y)
			{
				return base.HitTest(x, y);
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the object positioned at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation" /> value.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			// Token: 0x06001107 RID: 4359 RVA: 0x00044A04 File Offset: 0x00042C04
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				return base.Navigate(navigationDirection);
			}
		}

		/// <summary>Provides information about a row of <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> objects to accessibility client applications.</summary>
		// Token: 0x020000D4 RID: 212
		[ComVisible(true)]
		protected class DataGridViewTopRowAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject" /> class without setting the <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property. </summary>
			// Token: 0x06001108 RID: 4360 RVA: 0x00044A10 File Offset: 0x00042C10
			public DataGridViewTopRowAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property to the specified value.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject" /></param>
			// Token: 0x06001109 RID: 4361 RVA: 0x00044A18 File Offset: 0x00042C18
			public DataGridViewTopRowAccessibleObject(DataGridView owner)
			{
				this.owner = owner;
			}

			/// <summary>Returns the child accessible object corresponding to the specified index.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the column header at the specified index.</returns>
			/// <param name="index">The zero-based index of the accessible child.</param>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property is not set.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			// Token: 0x0600110A RID: 4362 RVA: 0x00044A28 File Offset: 0x00042C28
			public override AccessibleObject GetChild(int index)
			{
				return base.GetChild(index);
			}

			/// <summary>Returns the number of children belonging to the accessible object.</summary>
			/// <returns>The number of child accessible objects belonging to the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property is not set.</exception>
			// Token: 0x0600110B RID: 4363 RVA: 0x00044A34 File Offset: 0x00042C34
			public override int GetChildCount()
			{
				return base.GetChildCount();
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the object at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation" /> value.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property is not set.</exception>
			// Token: 0x0600110C RID: 4364 RVA: 0x00044A3C File Offset: 0x00042C3C
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				return base.Navigate(navigationDirection);
			}

			/// <summary>Gets the location and size of the accessible object. </summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property is not set.</exception>
			// Token: 0x17000393 RID: 915
			// (get) Token: 0x0600110D RID: 4365 RVA: 0x00044A48 File Offset: 0x00042C48
			public override Rectangle Bounds
			{
				get
				{
					return base.Bounds;
				}
			}

			/// <summary>Gets the name of the accessible object.</summary>
			/// <returns>The string "Top Row".</returns>
			// Token: 0x17000394 RID: 916
			// (get) Token: 0x0600110E RID: 4366 RVA: 0x00044A50 File Offset: 0x00042C50
			public override string Name
			{
				get
				{
					return base.Name;
				}
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the row of column headers.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the row of column headers.</returns>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property has already been set.</exception>
			// Token: 0x17000395 RID: 917
			// (get) Token: 0x0600110F RID: 4367 RVA: 0x00044A58 File Offset: 0x00042C58
			// (set) Token: 0x06001110 RID: 4368 RVA: 0x00044A68 File Offset: 0x00042C68
			public DataGridView Owner
			{
				get
				{
					return (DataGridView)this.owner;
				}
				set
				{
					if (this.owner != null)
					{
						throw new InvalidOperationException("owner has already been set");
					}
					this.owner = value;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject" />.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.DataGridViewTopRowAccessibleObject.Owner" /> property is not set.</exception>
			// Token: 0x17000396 RID: 918
			// (get) Token: 0x06001111 RID: 4369 RVA: 0x00044A88 File Offset: 0x00042C88
			public override AccessibleObject Parent
			{
				get
				{
					return base.Parent;
				}
			}

			/// <summary>Gets the role of the accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Row" /> value.</returns>
			// Token: 0x17000397 RID: 919
			// (get) Token: 0x06001112 RID: 4370 RVA: 0x00044A90 File Offset: 0x00042C90
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the value of an accessible object.</summary>
			/// <returns>The string "Top Row".</returns>
			// Token: 0x17000398 RID: 920
			// (get) Token: 0x06001113 RID: 4371 RVA: 0x00044A98 File Offset: 0x00042C98
			public override string Value
			{
				get
				{
					return base.Value;
				}
			}
		}
	}
}
