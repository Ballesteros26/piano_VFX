using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents an individual cell in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	[TypeConverter(typeof(DataGridViewCellConverter))]
	public abstract class DataGridViewCell : DataGridViewElement, IDisposable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> class. </summary>
		// Token: 0x06001182 RID: 4482 RVA: 0x0004585C File Offset: 0x00043A5C
		protected DataGridViewCell()
		{
			this.columnIndex = -1;
			this.dataGridViewOwner = null;
			this.errorText = string.Empty;
		}

		/// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:System.Windows.Forms.DataGridViewCell" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06001183 RID: 4483 RVA: 0x00045880 File Offset: 0x00043A80
		~DataGridViewCell()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x000458BC File Offset: 0x00043ABC
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				if (this.accessibilityObject == null)
				{
					this.accessibilityObject = this.CreateAccessibilityInstance();
				}
				return this.accessibilityObject;
			}
		}

		/// <summary>Gets the column index for this cell. </summary>
		/// <returns>The index of the column that contains the cell; -1 if the cell is not contained within a column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000458DC File Offset: 0x00043ADC
		public int ColumnIndex
		{
			get
			{
				if (base.DataGridView == null)
				{
					return -1;
				}
				return this.columnIndex;
			}
		}

		/// <summary>Gets the bounding rectangle that encloses the cell's content area.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x000458F4 File Offset: 0x00043AF4
		[Browsable(false)]
		public Rectangle ContentBounds
		{
			get
			{
				return this.GetContentBounds(this.RowIndex);
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the cell. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the cell.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00045904 File Offset: 0x00043B04
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x0004590C File Offset: 0x00043B0C
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		/// <summary>Gets the default value for a cell in the row for new records.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00045918 File Offset: 0x00043B18
		[Browsable(false)]
		public virtual object DefaultNewRowValue
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the cell is currently displayed on-screen. </summary>
		/// <returns>true if the cell is on-screen or partially on-screen; otherwise, false.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004591C File Offset: 0x00043B1C
		[Browsable(false)]
		public virtual bool Displayed
		{
			get
			{
				return this.displayed;
			}
		}

		/// <summary>Gets the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed. </summary>
		/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell. </exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00045924 File Offset: 0x00043B24
		[Browsable(false)]
		[EditorBrowsable(2)]
		public object EditedFormattedValue
		{
			get
			{
				return this.GetEditedFormattedValue(this.RowIndex, DataGridViewDataErrorContexts.Formatting);
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control. </summary>
		/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00045934 File Offset: 0x00043B34
		[EditorBrowsable(2)]
		[Browsable(false)]
		public virtual Type EditType
		{
			get
			{
				return typeof(DataGridViewTextBoxEditingControl);
			}
		}

		/// <summary>Gets the bounds of the error icon for the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the error icon for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00045940 File Offset: 0x00043B40
		[EditorBrowsable(2)]
		[Browsable(false)]
		public Rectangle ErrorIconBounds
		{
			get
			{
				if (this is DataGridViewTopLeftHeaderCell)
				{
					return this.GetErrorIconBounds(null, null, this.RowIndex);
				}
				if (base.DataGridView == null || this.columnIndex < 0)
				{
					throw new InvalidOperationException();
				}
				if (this.RowIndex < 0 || this.RowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
				}
				return this.GetErrorIconBounds(null, null, this.RowIndex);
			}
		}

		/// <summary>Gets or sets the text describing an error condition associated with the cell. </summary>
		/// <returns>The text that describes an error condition associated with the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000459CC File Offset: 0x00043BCC
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00045A10 File Offset: 0x00043C10
		[Browsable(false)]
		public string ErrorText
		{
			get
			{
				if (this is DataGridViewTopLeftHeaderCell)
				{
					return this.GetErrorText(-1);
				}
				if (this.OwningRow == null)
				{
					return string.Empty;
				}
				return this.GetErrorText(this.OwningRow.Index);
			}
			set
			{
				if (this.errorText != value)
				{
					this.errorText = value;
					this.OnErrorTextChanged(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));
				}
			}
		}

		/// <summary>Gets the value of the cell as formatted for display.</summary>
		/// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00045A4C File Offset: 0x00043C4C
		[Browsable(false)]
		public object FormattedValue
		{
			get
			{
				if (base.DataGridView == null)
				{
					return null;
				}
				DataGridViewCellStyle inheritedStyle = this.InheritedStyle;
				return this.GetFormattedValue(this.Value, this.RowIndex, ref inheritedStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell. </summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00045A84 File Offset: 0x00043C84
		[Browsable(false)]
		public virtual Type FormattedValueType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the cell is frozen. </summary>
		/// <returns>true if the cell is frozen; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00045A88 File Offset: 0x00043C88
		[Browsable(false)]
		public virtual bool Frozen
		{
			get
			{
				return base.DataGridView != null && this.RowIndex >= 0 && this.OwningRow.Frozen && this.OwningColumn.Frozen;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00045AD0 File Offset: 0x00043CD0
		[Browsable(false)]
		public bool HasStyle
		{
			get
			{
				return this.style != null;
			}
		}

		/// <summary>Gets the current state of the cell as inherited from the state of its row and column.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
		/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is not -1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is -1.</exception>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00045AE0 File Offset: 0x00043CE0
		[Browsable(false)]
		public DataGridViewElementStates InheritedState
		{
			get
			{
				return this.GetInheritedState(this.RowIndex);
			}
		}

		/// <summary>Gets the style currently applied to the cell. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> currently applied to the cell.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00045AF0 File Offset: 0x00043CF0
		[Browsable(false)]
		public DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return this.GetInheritedStyle(null, this.RowIndex, true);
			}
		}

		/// <summary>Gets a value indicating whether this cell is currently being edited.</summary>
		/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.</exception>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00045B00 File Offset: 0x00043D00
		[Browsable(false)]
		public bool IsInEditMode
		{
			get
			{
				if (base.DataGridView == null)
				{
					return false;
				}
				if (this.RowIndex == -1)
				{
					throw new InvalidOperationException("Operation cannot be performed on a cell of a shared row.");
				}
				return this.isInEditMode;
			}
		}

		/// <summary>Gets the column that contains this cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that contains the cell, or null if the cell is not in a column.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00045B38 File Offset: 0x00043D38
		[EditorBrowsable(2)]
		[Browsable(false)]
		public DataGridViewColumn OwningColumn
		{
			get
			{
				if (base.DataGridView == null || this.columnIndex < 0 || this.columnIndex >= base.DataGridView.Columns.Count)
				{
					return null;
				}
				return base.DataGridView.Columns[this.columnIndex];
			}
		}

		/// <summary>Gets the row that contains this cell. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that contains the cell, or null if the cell is not in a row.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00045B90 File Offset: 0x00043D90
		[Browsable(false)]
		[EditorBrowsable(2)]
		public DataGridViewRow OwningRow
		{
			get
			{
				return this.owningRow;
			}
		}

		/// <summary>Gets the size, in pixels, of a rectangular area into which the cell can fit. </summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00045B98 File Offset: 0x00043D98
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				if (base.DataGridView == null)
				{
					return new Size(-1, -1);
				}
				return this.GetPreferredSize(Hwnd.GraphicsContext, this.InheritedStyle, this.RowIndex, Size.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell's data can be edited. </summary>
		/// <returns>true if the cell's data cannot be edited; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no owning row when setting this property. -or-The owning row is shared when setting this property.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00045BD4 File Offset: 0x00043DD4
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00045C60 File Offset: 0x00043E60
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual bool ReadOnly
		{
			get
			{
				if (base.DataGridView != null && base.DataGridView.ReadOnly)
				{
					return true;
				}
				if (this.readOnly != DataGridViewTriState.NotSet)
				{
					return this.readOnly == DataGridViewTriState.True;
				}
				return (this.OwningRow != null && !this.OwningRow.IsShared && this.OwningRow.ReadOnly) || (this.OwningColumn != null && this.OwningColumn.ReadOnly);
			}
			set
			{
				this.readOnly = ((!value) ? DataGridViewTriState.False : DataGridViewTriState.True);
				if (value)
				{
					this.SetState(DataGridViewElementStates.ReadOnly | this.State);
				}
				else
				{
					this.SetState(~DataGridViewElementStates.ReadOnly & this.State);
				}
			}
		}

		/// <summary>Gets a value indicating whether the cell can be resized. </summary>
		/// <returns>true if the cell can be resized; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00045CA8 File Offset: 0x00043EA8
		[Browsable(false)]
		public virtual bool Resizable
		{
			get
			{
				return base.DataGridView != null && this.RowIndex != -1 && this.columnIndex != -1 && (this.OwningRow.Resizable == DataGridViewTriState.True || this.OwningColumn.Resizable == DataGridViewTriState.True);
			}
		}

		/// <summary>Gets the index of the cell's parent row. </summary>
		/// <returns>The index of the row that contains the cell; -1 if there is no owning row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00045D00 File Offset: 0x00043F00
		[Browsable(false)]
		public int RowIndex
		{
			get
			{
				if (this.owningRow == null)
				{
					return -1;
				}
				return this.owningRow.Index;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell has been selected. </summary>
		/// <returns>true if the cell has been selected; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> when setting this property. -or-The owning row is shared when setting this property.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00045D1C File Offset: 0x00043F1C
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00045DD4 File Offset: 0x00043FD4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual bool Selected
		{
			get
			{
				if (this.selected)
				{
					return true;
				}
				if (base.DataGridView != null)
				{
					if (this.RowIndex >= 0 && this.RowIndex < base.DataGridView.Rows.Count && base.DataGridView.Rows[this.RowIndex].Selected)
					{
						return true;
					}
					if (this.ColumnIndex >= 0 && this.ColumnIndex < base.DataGridView.Columns.Count && base.DataGridView.Columns[this.ColumnIndex].Selected)
					{
						return true;
					}
				}
				return false;
			}
			set
			{
				bool flag = this.selected != value;
				this.selected = value;
				if (value != ((this.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None))
				{
					this.SetState(this.State ^ DataGridViewElementStates.Selected);
				}
				if (!this.selected && this.OwningRow != null && this.OwningRow.Selected)
				{
					this.OwningRow.Selected = false;
					if (this.columnIndex != 0 && this.OwningRow.Cells.Count > 0)
					{
						this.OwningRow.Cells[0].Selected = true;
					}
					else if (this.OwningRow.Cells.Count > 1)
					{
						this.OwningRow.Cells[1].Selected = true;
					}
				}
				if (flag && base.DataGridView != null && base.DataGridView.IsHandleCreated)
				{
					base.DataGridView.Invalidate();
				}
			}
		}

		/// <summary>Gets the size of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> set to the owning row's height and the owning column's width. </returns>
		/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00045EE4 File Offset: 0x000440E4
		[Browsable(false)]
		public Size Size
		{
			get
			{
				if (base.DataGridView == null)
				{
					return new Size(-1, -1);
				}
				return this.GetSize(this.RowIndex);
			}
		}

		/// <summary>Gets or sets the style for the cell. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00045F08 File Offset: 0x00044108
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00045F40 File Offset: 0x00044140
		[Browsable(true)]
		public DataGridViewCellStyle Style
		{
			get
			{
				if (this.style == null)
				{
					this.style = new DataGridViewCellStyle();
					this.style.StyleChanged += new EventHandler(this.OnStyleChanged);
				}
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets the object that contains supplemental data about the cell. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the cell. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00045F4C File Offset: 0x0004414C
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00045F54 File Offset: 0x00044154
		[Localizable(false)]
		[TypeConverter("System.ComponentModel.StringConverter, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Bindable(true, 0)]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text associated with this cell.</summary>
		/// <returns>The ToolTip text associated with the cell. The default is <see cref="F:System.String.Empty" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00045F60 File Offset: 0x00044160
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00045F80 File Offset: 0x00044180
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string ToolTipText
		{
			get
			{
				return (this.toolTipText != null) ? this.toolTipText : string.Empty;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		/// <summary>Gets or sets the value associated with this cell. </summary>
		/// <returns>Gets or sets the data to be displayed by the cell. The default is null.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00045F8C File Offset: 0x0004418C
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00045F9C File Offset: 0x0004419C
		[Browsable(false)]
		public object Value
		{
			get
			{
				return this.GetValue(this.RowIndex);
			}
			set
			{
				this.SetValue(this.RowIndex, value);
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell. </summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00045FAC File Offset: 0x000441AC
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00046008 File Offset: 0x00044208
		[Browsable(false)]
		public virtual Type ValueType
		{
			get
			{
				if (this.valueType == null)
				{
					if (this.DataProperty != null)
					{
						this.valueType = this.DataProperty.PropertyType;
					}
					else if (this.OwningColumn != null)
					{
						this.valueType = this.OwningColumn.ValueType;
					}
				}
				return this.valueType;
			}
			set
			{
				this.valueType = value;
			}
		}

		/// <summary>Gets a value indicating whether the cell is in a row or column that has been hidden. </summary>
		/// <returns>true if the cell is visible; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00046014 File Offset: 0x00044214
		[Browsable(false)]
		public virtual bool Visible
		{
			get
			{
				DataGridViewColumn owningColumn = this.OwningColumn;
				DataGridViewRow dataGridViewRow = this.OwningRow;
				bool flag = true;
				bool flag2 = true;
				if (dataGridViewRow == null && owningColumn == null)
				{
					return false;
				}
				if (dataGridViewRow != null)
				{
					flag = !dataGridViewRow.IsShared && dataGridViewRow.Visible;
				}
				if (owningColumn != null)
				{
					flag2 = owningColumn.Index >= 0 && owningColumn.Visible;
				}
				return flag && flag2;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00046084 File Offset: 0x00044284
		internal override void SetState(DataGridViewElementStates state)
		{
			base.SetState(state);
			if (base.DataGridView != null)
			{
				base.DataGridView.OnCellStateChangedInternal(new DataGridViewCellStateChangedEventArgs(this, state));
			}
		}

		/// <summary>Modifies the input cell border style according to the specified criteria. </summary>
		/// <returns>The modified <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
		/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the cell border style to modify.</param>
		/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the cell border style. </param>
		/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false. </param>
		/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false. </param>
		/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false. </param>
		/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011AD RID: 4525 RVA: 0x000460B8 File Offset: 0x000442B8
		[EditorBrowsable(2)]
		public virtual DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			return dataGridViewAdvancedBorderStyleInput;
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011AE RID: 4526 RVA: 0x000460BC File Offset: 0x000442BC
		public virtual object Clone()
		{
			DataGridViewCell dataGridViewCell = (DataGridViewCell)Activator.CreateInstance(base.GetType());
			dataGridViewCell.accessibilityObject = this.accessibilityObject;
			dataGridViewCell.columnIndex = this.columnIndex;
			dataGridViewCell.displayed = this.displayed;
			dataGridViewCell.errorText = this.errorText;
			dataGridViewCell.isInEditMode = this.isInEditMode;
			dataGridViewCell.owningRow = this.owningRow;
			dataGridViewCell.readOnly = this.readOnly;
			dataGridViewCell.selected = this.selected;
			dataGridViewCell.style = this.style;
			dataGridViewCell.tag = this.tag;
			dataGridViewCell.toolTipText = this.toolTipText;
			dataGridViewCell.valuex = this.valuex;
			dataGridViewCell.valueType = this.valueType;
			return dataGridViewCell;
		}

		/// <summary>Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">This cell is not associated with a <see cref="T:System.Windows.Forms.DataGridView" />.-or-The <see cref="P:System.Windows.Forms.DataGridView.EditingControl" /> property of the associated <see cref="T:System.Windows.Forms.DataGridView" /> has a value of null. This is the case, for example, when the control is not in edit mode.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011AF RID: 4527 RVA: 0x00046178 File Offset: 0x00044378
		[EditorBrowsable(2)]
		public virtual void DetachEditingControl()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011B0 RID: 4528 RVA: 0x0004617C File Offset: 0x0004437C
		public void Dispose()
		{
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area using a default <see cref="T:System.Drawing.Graphics" /> and cell style currently in effect for the cell.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x060011B1 RID: 4529 RVA: 0x00046180 File Offset: 0x00044380
		public Rectangle GetContentBounds(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			return this.GetContentBounds(Hwnd.GraphicsContext, this.InheritedStyle, rowIndex);
		}

		/// <summary>Returns the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.</summary>
		/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The row index of the cell.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x060011B2 RID: 4530 RVA: 0x000461B0 File Offset: 0x000443B0
		public object GetEditedFormattedValue(int rowIndex, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
			{
				throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
			}
			if (!this.IsInEditMode)
			{
				DataGridViewCellStyle inheritedStyle = this.InheritedStyle;
				return this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, null, null, context);
			}
			if (base.DataGridView.EditingControl != null)
			{
				return (base.DataGridView.EditingControl as IDataGridViewEditingControl).GetEditingControlFormattedValue(context);
			}
			return (this as IDataGridViewEditingCell).GetEditingCellFormattedValue(context);
		}

		/// <summary>Gets the inherited shortcut menu for the current cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> if the parent <see cref="T:System.Windows.Forms.DataGridView" />, <see cref="T:System.Windows.Forms.DataGridViewRow" />, or <see cref="T:System.Windows.Forms.DataGridViewColumn" /> has a <see cref="T:System.Windows.Forms.ContextMenuStrip" /> assigned; otherwise, null.</returns>
		/// <param name="rowIndex">The row index of the current cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and the specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x060011B3 RID: 4531 RVA: 0x0004624C File Offset: 0x0004444C
		public virtual ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.columnIndex < 0)
			{
				throw new InvalidOperationException("cannot perform this on a column header cell");
			}
			if (this.contextMenuStrip != null)
			{
				return this.contextMenuStrip;
			}
			if (this.OwningRow.ContextMenuStrip != null)
			{
				return this.OwningRow.ContextMenuStrip;
			}
			if (this.OwningColumn.ContextMenuStrip != null)
			{
				return this.OwningColumn.ContextMenuStrip;
			}
			return base.DataGridView.ContextMenuStrip;
		}

		/// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row and column.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is not -1.-or-<paramref name="rowIndex" /> is not the index of the row containing this cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
		// Token: 0x060011B4 RID: 4532 RVA: 0x000462FC File Offset: 0x000444FC
		public virtual DataGridViewElementStates GetInheritedState(int rowIndex)
		{
			if (base.DataGridView == null && rowIndex != -1)
			{
				throw new ArgumentException("msg?");
			}
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
			}
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ResizableSet | this.State;
			DataGridViewColumn owningColumn = this.OwningColumn;
			DataGridViewRow dataGridViewRow = this.OwningRow;
			if (base.DataGridView == null)
			{
				if (dataGridViewRow != null)
				{
					if (dataGridViewRow.Resizable == DataGridViewTriState.True)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Resizable;
					}
					if (dataGridViewRow.Visible)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Visible;
					}
					if (dataGridViewRow.ReadOnly)
					{
						dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
					}
					if (dataGridViewRow.Frozen)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Frozen;
					}
					if (dataGridViewRow.Displayed)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
					if (dataGridViewRow.Selected)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Selected;
					}
				}
				return dataGridViewElementStates;
			}
			if (owningColumn != null)
			{
				if (owningColumn.Resizable == DataGridViewTriState.True && dataGridViewRow.Resizable == DataGridViewTriState.True)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (owningColumn.Visible && dataGridViewRow.Visible)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
				}
				if (owningColumn.ReadOnly || dataGridViewRow.ReadOnly)
				{
					dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
				}
				if (owningColumn.Frozen || dataGridViewRow.Frozen)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				}
				if (owningColumn.Displayed && dataGridViewRow.Displayed)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Displayed;
				}
				if (owningColumn.Selected || dataGridViewRow.Selected)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Selected;
				}
			}
			return dataGridViewElementStates;
		}

		/// <summary>Gets the style applied to the cell. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">The cell has no associated <see cref="T:System.Windows.Forms.DataGridView" />.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0, or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		// Token: 0x060011B5 RID: 4533 RVA: 0x00046490 File Offset: 0x00044690
		public virtual DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException("Cell is not in a DataGridView. The cell cannot retrieve the inherited cell style.");
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle(base.DataGridView.DefaultCellStyle);
			if (this.OwningColumn != null)
			{
				dataGridViewCellStyle.ApplyStyle(this.OwningColumn.DefaultCellStyle);
			}
			dataGridViewCellStyle.ApplyStyle(base.DataGridView.RowsDefaultCellStyle);
			if (rowIndex % 2 == 1)
			{
				dataGridViewCellStyle.ApplyStyle(base.DataGridView.AlternatingRowsDefaultCellStyle);
			}
			dataGridViewCellStyle.ApplyStyle(base.DataGridView.Rows.SharedRow(rowIndex).DefaultCellStyle);
			if (this.HasStyle)
			{
				dataGridViewCellStyle.ApplyStyle(this.Style);
			}
			return dataGridViewCellStyle;
		}

		/// <summary>Initializes the control used to edit the cell.</summary>
		/// <param name="rowIndex">The zero-based row index of the cell's location.</param>
		/// <param name="initialFormattedValue">An <see cref="T:System.Object" /> that represents the value displayed by the cell when editing is started.</param>
		/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> or if one is present, it does not have an associated editing control. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011B6 RID: 4534 RVA: 0x00046568 File Offset: 0x00044768
		[EditorBrowsable(2)]
		public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (base.DataGridView == null || base.DataGridView.EditingControl == null)
			{
				throw new InvalidOperationException("No editing control defined");
			}
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <returns>true if edit mode should be started; otherwise, false. The default is false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011B7 RID: 4535 RVA: 0x0004659C File Offset: 0x0004479C
		public virtual bool KeyEntersEditMode(KeyEventArgs e)
		{
			return false;
		}

		/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics.</summary>
		/// <returns>The height, in pixels, of the text.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxWidth">The maximum width of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxWidth" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
		// Token: 0x060011B8 RID: 4536 RVA: 0x000465A0 File Offset: 0x000447A0
		[EditorBrowsable(2)]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("Graphics argument null");
			}
			if (font == null)
			{
				throw new ArgumentNullException("Font argument null");
			}
			if (maxWidth < 1)
			{
				throw new ArgumentOutOfRangeException("maxWidth is less than 1.");
			}
			return TextRenderer.MeasureText(graphics, text, font, new Size(maxWidth, 0), flags).Height;
		}

		/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics. Also indicates whether the required width is greater than the specified maximum width.</summary>
		/// <returns>The height, in pixels, of the text.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxWidth">The maximum width of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
		/// <param name="widthTruncated">Set to true if the required width of the text is greater than <paramref name="maxWidth" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxWidth" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
		// Token: 0x060011B9 RID: 4537 RVA: 0x000465FC File Offset: 0x000447FC
		[EditorBrowsable(2)]
		[MonoTODO("does not use widthTruncated parameter")]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
		{
			widthTruncated = false;
			return TextRenderer.MeasureText(graphics, text, font, new Size(maxWidth, 0), flags).Height;
		}

		/// <summary>Gets the ideal height and width of the specified text given the specified characteristics.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the preferred height and width of the text.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxRatio">The maximum width-to-height ratio of the block of text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxRatio" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
		// Token: 0x060011BA RID: 4538 RVA: 0x00046628 File Offset: 0x00044828
		[EditorBrowsable(2)]
		public static Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("Graphics argument null");
			}
			if (font == null)
			{
				throw new ArgumentNullException("Font argument null");
			}
			if (maxRatio <= 0f)
			{
				throw new ArgumentOutOfRangeException("maxRatio is less than or equals to 0.");
			}
			return DataGridViewCell.MeasureTextSize(graphics, text, font, flags);
		}

		/// <summary>Gets the height and width of the specified text given the specified characteristics.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the text.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
		// Token: 0x060011BB RID: 4539 RVA: 0x00046678 File Offset: 0x00044878
		[EditorBrowsable(2)]
		public static Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
		{
			return TextRenderer.MeasureText(graphics, text, font, Size.Empty, flags);
		}

		/// <summary>Gets the width, in pixels, of the specified text given the specified characteristics.</summary>
		/// <returns>The width, in pixels, of the text.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxHeight">The maximum height of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxHeight" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
		// Token: 0x060011BC RID: 4540 RVA: 0x00046688 File Offset: 0x00044888
		[EditorBrowsable(2)]
		public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("Graphics argument null");
			}
			if (font == null)
			{
				throw new ArgumentNullException("Font argument null");
			}
			if (maxHeight < 1)
			{
				throw new ArgumentOutOfRangeException("maxHeight is less than 1.");
			}
			return TextRenderer.MeasureText(graphics, text, font, new Size(0, maxHeight), flags).Width;
		}

		/// <summary>Converts a value formatted for display to an actual cell value.</summary>
		/// <returns>The cell value.</returns>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cellStyle" /> is null.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is null.-or-The <see cref="P:System.Windows.Forms.DataGridViewCell.ValueType" /> property value is null.-or-<paramref name="formattedValue" /> cannot be converted.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="formattedValue" /> is null.-or-The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property. </exception>
		// Token: 0x060011BD RID: 4541 RVA: 0x000466E4 File Offset: 0x000448E4
		public virtual object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle is null.");
			}
			if (this.FormattedValueType == null)
			{
				throw new FormatException("The System.Windows.Forms.DataGridViewCell.FormattedValueType property value is null.");
			}
			if (formattedValue == null)
			{
				throw new ArgumentException("formattedValue is null.");
			}
			if (this.ValueType == null)
			{
				throw new FormatException("valuetype is null");
			}
			if (!this.FormattedValueType.IsAssignableFrom(formattedValue.GetType()))
			{
				throw new ArgumentException("formattedValue is not of formattedValueType.");
			}
			if (formattedValueTypeConverter == null)
			{
				formattedValueTypeConverter = this.FormattedValueTypeConverter;
			}
			if (valueTypeConverter == null)
			{
				valueTypeConverter = this.ValueTypeConverter;
			}
			if (valueTypeConverter != null && valueTypeConverter.CanConvertFrom(this.FormattedValueType))
			{
				return valueTypeConverter.ConvertFrom(formattedValue);
			}
			if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertTo(this.ValueType))
			{
				return formattedValueTypeConverter.ConvertTo(formattedValue, this.ValueType);
			}
			return Convert.ChangeType(formattedValue, this.ValueType);
		}

		/// <summary>Sets the location and size of the editing control hosted by a cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
		/// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param>
		/// <param name="setSize">true to specify the size; false to allow the control to size itself. </param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
		/// <param name="cellClip">The area that will be used to paint the editing control.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
		/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
		/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param>
		/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011BE RID: 4542 RVA: 0x000467D0 File Offset: 0x000449D0
		[EditorBrowsable(2)]
		public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			if (base.DataGridView.EditingControl != null)
			{
				if (setLocation && setSize)
				{
					base.DataGridView.EditingControl.Bounds = cellBounds;
				}
				else if (setLocation)
				{
					base.DataGridView.EditingControl.Location = cellBounds.Location;
				}
				else if (setSize)
				{
					base.DataGridView.EditingControl.Size = cellBounds.Size;
				}
			}
		}

		/// <summary>Sets the location and size of the editing panel hosted by the cell, and returns the normal bounds of the editing control within the editing panel.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the normal bounds of the editing control within the editing panel.</returns>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
		/// <param name="cellClip">The area that will be used to paint the editing panel.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
		/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
		/// <param name="isFirstDisplayedColumn">true if the cell is in the first column currently displayed in the control; otherwise, false.</param>
		/// <param name="isFirstDisplayedRow">true if the cell is in the first row currently displayed in the control; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">The cell has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x060011BF RID: 4543 RVA: 0x00046850 File Offset: 0x00044A50
		[EditorBrowsable(2)]
		public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string that describes the current object. </summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C0 RID: 4544 RVA: 0x00046858 File Offset: 0x00044A58
		public override string ToString()
		{
			return string.Format("{0} {{ ColumnIndex={1}, RowIndex={2} }}", base.GetType().Name, this.ColumnIndex, this.RowIndex);
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins. </summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins.</returns>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that the margins are to be calculated for. </param>
		// Token: 0x060011C1 RID: 4545 RVA: 0x00046890 File Offset: 0x00044A90
		protected virtual Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			Rectangle empty = Rectangle.Empty;
			empty.X = this.BorderToWidth(advancedBorderStyle.Left);
			empty.Y = this.BorderToWidth(advancedBorderStyle.Top);
			empty.Width = this.BorderToWidth(advancedBorderStyle.Right);
			empty.Height = this.BorderToWidth(advancedBorderStyle.Bottom);
			if (this.OwningColumn != null)
			{
				empty.Width += this.OwningColumn.DividerWidth;
			}
			if (this.OwningRow != null)
			{
				empty.Height += this.OwningRow.DividerHeight;
			}
			return empty;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00046938 File Offset: 0x00044B38
		private int BorderToWidth(DataGridViewAdvancedCellBorderStyle style)
		{
			switch (style)
			{
			case DataGridViewAdvancedCellBorderStyle.None:
				return 0;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				return 2;
			}
			return 1;
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell is clicked.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x00046978 File Offset: 0x00044B78
		protected virtual bool ClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell's content is clicked.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0004697C File Offset: 0x00044B7C
		protected virtual bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell's content is double-clicked.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		// Token: 0x060011C5 RID: 4549 RVA: 0x00046980 File Offset: 0x00044B80
		protected virtual bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </returns>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00046984 File Offset: 0x00044B84
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewCell.DataGridViewCellAccessibleObject(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060011C7 RID: 4551 RVA: 0x0004698C File Offset: 0x00044B8C
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell is double-clicked.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		// Token: 0x060011C8 RID: 4552 RVA: 0x00046990 File Offset: 0x00044B90
		protected virtual bool DoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether the parent row will be unshared when the focus moves to the cell.</summary>
		/// <returns>true if the row will be unshared; otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
		// Token: 0x060011C9 RID: 4553 RVA: 0x00046994 File Offset: 0x00044B94
		protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
		/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
		/// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
		/// <param name="format">The current format string of the cell.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the cell's <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property is null.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x060011CA RID: 4554 RVA: 0x00046998 File Offset: 0x00044B98
		protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
			{
				throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
			}
			string text = null;
			if (this.Selected)
			{
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
				text = this.GetEditedFormattedValue(rowIndex, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.ClipboardContent) as string;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			if (format == DataFormats.UnicodeText || format == DataFormats.Text)
			{
				if (lastCell && !inLastRow)
				{
					text6 = Environment.NewLine;
				}
				else if (!lastCell)
				{
					text6 = "\t";
				}
			}
			else if (format == DataFormats.CommaSeparatedValue)
			{
				if (lastCell && !inLastRow)
				{
					text6 = Environment.NewLine;
				}
				else if (!lastCell)
				{
					text6 = ",";
				}
			}
			else
			{
				if (!(format == DataFormats.Html))
				{
					return text;
				}
				if (inFirstRow && firstCell)
				{
					text2 = "<TABLE>";
				}
				if (inLastRow && lastCell)
				{
					text5 = "</TABLE>";
				}
				if (firstCell)
				{
					text4 = "<TR>";
				}
				if (lastCell)
				{
					text7 = "</TR>";
				}
				text3 = "<TD>";
				text6 = "</TD>";
				if (!this.Selected)
				{
					text = "&nbsp;";
				}
			}
			return string.Concat(new string[] { text2, text4, text3, text, text6, text7, text5 });
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00046B64 File Offset: 0x00044D64
		internal object GetClipboardContentInternal(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			return this.GetClipboardContent(rowIndex, firstCell, lastCell, inFirstRow, inLastRow, format);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060011CC RID: 4556 RVA: 0x00046B78 File Offset: 0x00044D78
		protected virtual Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060011CD RID: 4557 RVA: 0x00046B80 File Offset: 0x00044D80
		protected virtual Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		/// <summary>Returns a string that represents the error for the cell.</summary>
		/// <returns>A string that describes the error for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The row index of the cell.</param>
		// Token: 0x060011CE RID: 4558 RVA: 0x00046B88 File Offset: 0x00044D88
		protected internal virtual string GetErrorText(int rowIndex)
		{
			return this.errorText;
		}

		/// <summary>Gets the value of the cell as formatted for display. </summary>
		/// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <param name="value">The value to be formatted. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x060011CF RID: 4559 RVA: 0x00046B90 File Offset: 0x00044D90
		protected virtual object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (!(this is DataGridViewRowHeaderCell))
			{
				DataGridViewCellFormattingEventArgs dataGridViewCellFormattingEventArgs = new DataGridViewCellFormattingEventArgs(this.ColumnIndex, rowIndex, value, this.FormattedValueType, cellStyle);
				base.DataGridView.OnCellFormattingInternal(dataGridViewCellFormattingEventArgs);
				if (dataGridViewCellFormattingEventArgs.FormattingApplied)
				{
					return dataGridViewCellFormattingEventArgs.Value;
				}
				cellStyle = dataGridViewCellFormattingEventArgs.CellStyle;
				value = dataGridViewCellFormattingEventArgs.Value;
			}
			if ((value == null || (cellStyle != null && value == cellStyle.DataSourceNullValue)) && this.FormattedValueType == typeof(string))
			{
				return string.Empty;
			}
			if (this.FormattedValueType == typeof(string) && value is IFormattable && !string.IsNullOrEmpty(cellStyle.Format))
			{
				return ((IFormattable)value).ToString(cellStyle.Format, cellStyle.FormatProvider);
			}
			if (value != null && this.FormattedValueType.IsAssignableFrom(value.GetType()))
			{
				return value;
			}
			if (formattedValueTypeConverter == null)
			{
				formattedValueTypeConverter = this.FormattedValueTypeConverter;
			}
			if (valueTypeConverter == null)
			{
				valueTypeConverter = this.ValueTypeConverter;
			}
			if (valueTypeConverter != null && valueTypeConverter.CanConvertTo(this.FormattedValueType))
			{
				return valueTypeConverter.ConvertTo(value, this.FormattedValueType);
			}
			if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertFrom(this.ValueType))
			{
				return formattedValueTypeConverter.ConvertFrom(value);
			}
			return Convert.ChangeType(value, this.FormattedValueType);
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x060011D0 RID: 4560 RVA: 0x00046D30 File Offset: 0x00044F30
		protected virtual Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			return new Size(-1, -1);
		}

		/// <summary>Gets the size of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the cell's dimensions.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="rowIndex" /> is -1</exception>
		// Token: 0x060011D1 RID: 4561 RVA: 0x00046D3C File Offset: 0x00044F3C
		protected virtual Size GetSize(int rowIndex)
		{
			if (this.RowIndex == -1)
			{
				throw new InvalidOperationException("Getting the Size property of a cell in a shared row is not a valid operation.");
			}
			return new Size(this.OwningColumn.Width, this.OwningRow.Height);
		}

		/// <summary>Gets the value of the cell. </summary>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x060011D2 RID: 4562 RVA: 0x00046D7C File Offset: 0x00044F7C
		protected virtual object GetValue(int rowIndex)
		{
			if (base.DataGridView != null && (this.RowIndex < 0 || this.RowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
			}
			if (this.OwningRow != null && this.OwningRow.Index == base.DataGridView.NewRowIndex)
			{
				return this.DefaultNewRowValue;
			}
			if (this.DataProperty != null && this.OwningRow.DataBoundItem != null)
			{
				return this.DataProperty.GetValue(this.OwningRow.DataBoundItem);
			}
			if (this.valuex != null)
			{
				return this.valuex;
			}
			DataGridViewCellValueEventArgs dataGridViewCellValueEventArgs = new DataGridViewCellValueEventArgs(this.columnIndex, rowIndex);
			if (base.DataGridView != null)
			{
				base.DataGridView.OnCellValueNeeded(dataGridViewCellValueEventArgs);
			}
			return dataGridViewCellValueEventArgs.Value;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00046E68 File Offset: 0x00045068
		private PropertyDescriptor DataProperty
		{
			get
			{
				if (this.OwningColumn != null && this.OwningColumn.DataColumnIndex != -1 && base.DataGridView != null && base.DataGridView.DataManager != null)
				{
					return base.DataGridView.DataManager.GetItemProperties()[this.OwningColumn.DataColumnIndex];
				}
				return null;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00046ED0 File Offset: 0x000450D0
		private TypeConverter FormattedValueTypeConverter
		{
			get
			{
				if (this.FormattedValueType != null)
				{
					return TypeDescriptor.GetConverter(this.FormattedValueType);
				}
				return null;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00046EEC File Offset: 0x000450EC
		private TypeConverter ValueTypeConverter
		{
			get
			{
				if (this.DataProperty != null && this.DataProperty.Converter != null)
				{
					return this.DataProperty.Converter;
				}
				if (this.Value != null)
				{
					return TypeDescriptor.GetConverter(this.Value);
				}
				if (this.ValueType != null)
				{
					return TypeDescriptor.GetConverter(this.ValueType);
				}
				return null;
			}
		}

		/// <summary>Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011D6 RID: 4566 RVA: 0x00046F50 File Offset: 0x00045150
		protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared if a key is pressed while a cell in the row has focus.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011D7 RID: 4567 RVA: 0x00046F54 File Offset: 0x00045154
		protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether the parent row is unshared when the user releases a key while the focus is on the cell.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011D8 RID: 4568 RVA: 0x00046F58 File Offset: 0x00045158
		protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the focus leaves a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
		// Token: 0x060011D9 RID: 4569 RVA: 0x00046F5C File Offset: 0x0004515C
		protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared if the user clicks a mouse button while the pointer is on a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011DA RID: 4570 RVA: 0x00046F60 File Offset: 0x00045160
		protected virtual bool MouseClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared if the user double-clicks a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060011DB RID: 4571 RVA: 0x00046F64 File Offset: 0x00045164
		protected virtual bool MouseDoubleClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is on a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011DC RID: 4572 RVA: 0x00046F68 File Offset: 0x00045168
		protected virtual bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011DD RID: 4573 RVA: 0x00046F6C File Offset: 0x0004516C
		protected virtual bool MouseEnterUnsharesRow(int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011DE RID: 4574 RVA: 0x00046F70 File Offset: 0x00045170
		protected virtual bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011DF RID: 4575 RVA: 0x00046F74 File Offset: 0x00045174
		protected virtual bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared when the user releases a mouse button while the pointer is on a cell in the row.</summary>
		/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011E0 RID: 4576 RVA: 0x00046F78 File Offset: 0x00045178
		protected virtual bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		/// <summary>Called when the cell is clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060011E1 RID: 4577 RVA: 0x00046F7C File Offset: 0x0004517C
		protected virtual void OnClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00046F80 File Offset: 0x00045180
		internal void OnClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnClick(e);
		}

		/// <summary>Called when the cell's contents are clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060011E3 RID: 4579 RVA: 0x00046F8C File Offset: 0x0004518C
		protected virtual void OnContentClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00046F90 File Offset: 0x00045190
		internal void OnContentClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentClick(e);
		}

		/// <summary>Called when the cell's contents are double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060011E5 RID: 4581 RVA: 0x00046F9C File Offset: 0x0004519C
		protected virtual void OnContentDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00046FA0 File Offset: 0x000451A0
		internal void OnContentDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentDoubleClick(e);
		}

		/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
		// Token: 0x060011E7 RID: 4583 RVA: 0x00046FAC File Offset: 0x000451AC
		protected override void OnDataGridViewChanged()
		{
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00046FB0 File Offset: 0x000451B0
		internal void OnDataGridViewChangedInternal()
		{
			this.OnDataGridViewChanged();
		}

		/// <summary>Called when the cell is double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060011E9 RID: 4585 RVA: 0x00046FB8 File Offset: 0x000451B8
		protected virtual void OnDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00046FBC File Offset: 0x000451BC
		internal void OnDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnDoubleClick(e);
		}

		/// <summary>Called when the focus moves to a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
		// Token: 0x060011EB RID: 4587 RVA: 0x00046FC8 File Offset: 0x000451C8
		protected virtual void OnEnter(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00046FCC File Offset: 0x000451CC
		internal void OnEnterInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnEnter(rowIndex, throughMouseClick);
		}

		/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011ED RID: 4589 RVA: 0x00046FD8 File Offset: 0x000451D8
		protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00046FDC File Offset: 0x000451DC
		internal void OnKeyDownInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyDown(e, rowIndex);
		}

		/// <summary>Called when a key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011EF RID: 4591 RVA: 0x00046FE8 File Offset: 0x000451E8
		protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)
		{
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00046FEC File Offset: 0x000451EC
		internal void OnKeyPressInternal(KeyPressEventArgs e, int rowIndex)
		{
			this.OnKeyPress(e, rowIndex);
		}

		/// <summary>Called when a character key is released while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011F1 RID: 4593 RVA: 0x00046FF8 File Offset: 0x000451F8
		protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00046FFC File Offset: 0x000451FC
		internal void OnKeyUpInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyUp(e, rowIndex);
		}

		/// <summary>Called when the focus moves from a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
		// Token: 0x060011F3 RID: 4595 RVA: 0x00047008 File Offset: 0x00045208
		protected virtual void OnLeave(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004700C File Offset: 0x0004520C
		internal void OnLeaveInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnLeave(rowIndex, throughMouseClick);
		}

		/// <summary>Called when the user clicks a mouse button while the pointer is on a cell.  </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011F5 RID: 4597 RVA: 0x00047018 File Offset: 0x00045218
		protected virtual void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0004701C File Offset: 0x0004521C
		internal void OnMouseClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseClick(e);
		}

		/// <summary>Called when the user double-clicks a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011F7 RID: 4599 RVA: 0x00047028 File Offset: 0x00045228
		protected virtual void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004702C File Offset: 0x0004522C
		internal void OnMouseDoubleClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseDoubleClick(e);
		}

		/// <summary>Called when the user holds down a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011F9 RID: 4601 RVA: 0x00047038 File Offset: 0x00045238
		protected virtual void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004703C File Offset: 0x0004523C
		internal void OnMouseDownInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseDown(e);
		}

		/// <summary>Called when the mouse pointer moves over a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011FB RID: 4603 RVA: 0x00047048 File Offset: 0x00045248
		protected virtual void OnMouseEnter(int rowIndex)
		{
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004704C File Offset: 0x0004524C
		internal void OnMouseEnterInternal(int rowIndex)
		{
			this.OnMouseEnter(rowIndex);
		}

		/// <summary>Called when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060011FD RID: 4605 RVA: 0x00047058 File Offset: 0x00045258
		protected virtual void OnMouseLeave(int rowIndex)
		{
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0004705C File Offset: 0x0004525C
		internal void OnMouseLeaveInternal(int e)
		{
			this.OnMouseLeave(e);
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060011FF RID: 4607 RVA: 0x00047068 File Offset: 0x00045268
		protected virtual void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004706C File Offset: 0x0004526C
		internal void OnMouseMoveInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseMove(e);
		}

		/// <summary>Called when the user releases a mouse button while the pointer is on a cell. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001201 RID: 4609 RVA: 0x00047078 File Offset: 0x00045278
		protected virtual void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004707C File Offset: 0x0004527C
		internal void OnMouseUpInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00047088 File Offset: 0x00045288
		internal void PaintInternal(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x06001204 RID: 4612 RVA: 0x000470B0 File Offset: 0x000452B0
		protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
			{
				this.PaintPartBackground(graphics, cellBounds, cellStyle);
			}
			if ((paintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground)
			{
				this.PaintPartSelectionBackground(graphics, cellBounds, cellState, cellStyle);
			}
			if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
			{
				this.PaintPartContent(graphics, cellBounds, rowIndex, cellState, cellStyle, formattedValue);
			}
			if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			if ((paintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
			{
				this.PaintPartFocus(graphics, cellBounds);
			}
			if ((paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon && !string.IsNullOrEmpty(this.ErrorText))
			{
				this.PaintErrorIcon(graphics, clipBounds, cellBounds, this.ErrorText);
			}
		}

		/// <summary>Paints the border of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the current cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
		// Token: 0x06001205 RID: 4613 RVA: 0x0004715C File Offset: 0x0004535C
		protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			Pen pen = new Pen(base.DataGridView.GridColor);
			switch (advancedBorderStyle.Left)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				if (base.DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.Single)
				{
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
				}
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			case DataGridViewAdvancedCellBorderStyle.Outset:
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
				graphics.DrawLine(pen, bounds.X + 2, bounds.Y, bounds.X + 2, bounds.Y + bounds.Height - 1);
				break;
			}
			switch (advancedBorderStyle.Right)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(pen, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.Outset:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				graphics.DrawLine(pen, bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height - 1);
				break;
			}
			switch (advancedBorderStyle.Top)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				if (base.DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.Single)
				{
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
				}
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.Outset:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
				break;
			}
			switch (advancedBorderStyle.Bottom)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
			case DataGridViewAdvancedCellBorderStyle.Inset:
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.Outset:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				graphics.DrawLine(pen, bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
				break;
			}
		}

		/// <summary>Paints the error icon of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellValueBounds">The bounding <see cref="T:System.Drawing.Rectangle" /> that encloses the cell's content area.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		// Token: 0x06001206 RID: 4614 RVA: 0x0004743C File Offset: 0x0004563C
		protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
		{
			Rectangle errorIconBounds = this.GetErrorIconBounds(graphics, null, this.RowIndex);
			if (errorIconBounds.IsEmpty)
			{
				return;
			}
			Point location = errorIconBounds.Location;
			location.X += cellValueBounds.Left;
			location.Y += cellValueBounds.Top;
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 1, location.Y + 2, 10, 7));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 2, location.Y + 1, 8, 9));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 4, location.Y, 4, 11));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X, location.Y + 4, 12, 3));
			graphics.FillRectangle(Brushes.White, new Rectangle(location.X + 5, location.Y + 2, 2, 4));
			graphics.FillRectangle(Brushes.White, new Rectangle(location.X + 5, location.Y + 7, 2, 2));
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00047570 File Offset: 0x00045770
		internal virtual void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
		{
			Color backColor = style.BackColor;
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(backColor), cellBounds);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004759C File Offset: 0x0004579C
		internal Pen GetBorderPen()
		{
			return ThemeEngine.Current.ResPool.GetPen(base.DataGridView.GridColor);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000475B8 File Offset: 0x000457B8
		internal virtual void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			if (this.IsInEditMode)
			{
				return;
			}
			Color color = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			textFormatFlags |= this.AlignmentToFlags(this.style.Alignment);
			cellBounds.Height -= 2;
			cellBounds.Width -= 2;
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, cellBounds, color, textFormatFlags);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00047648 File Offset: 0x00045848
		private void PaintPartFocus(Graphics graphics, Rectangle cellBounds)
		{
			cellBounds.Width--;
			cellBounds.Height--;
			if (base.DataGridView.ShowFocusCues && base.DataGridView.CurrentCell == this && base.DataGridView.Focused)
			{
				ControlPaint.DrawFocusRectangle(graphics, cellBounds);
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000476AC File Offset: 0x000458AC
		internal virtual void PaintPartSelectionBackground(Graphics graphics, Rectangle cellBounds, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle)
		{
			if ((cellState & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
			{
				return;
			}
			if (this.RowIndex >= 0 && this.IsInEditMode && this.EditType != null)
			{
				return;
			}
			Color selectionBackColor = cellStyle.SelectionBackColor;
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(selectionBackColor), cellBounds);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00047708 File Offset: 0x00045908
		internal void PaintWork(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			object obj;
			object obj2;
			if (this.RowIndex == -1 && !(this is DataGridViewColumnHeaderCell))
			{
				obj = null;
				obj2 = null;
			}
			else if (this.RowIndex == -1)
			{
				obj = this.Value;
				obj2 = this.Value;
			}
			else
			{
				obj = this.Value;
				obj2 = this.GetFormattedValue(this.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			}
			DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs = new DataGridViewCellPaintingEventArgs(base.DataGridView, graphics, clipBounds, cellBounds, rowIndex, this.columnIndex, cellState, obj, obj2, this.ErrorText, cellStyle, advancedBorderStyle, paintParts);
			base.DataGridView.OnCellPaintingInternal(dataGridViewCellPaintingEventArgs);
			if (dataGridViewCellPaintingEventArgs.Handled)
			{
				return;
			}
			dataGridViewCellPaintingEventArgs.Paint(dataGridViewCellPaintingEventArgs.ClipBounds, dataGridViewCellPaintingEventArgs.PaintParts);
		}

		/// <summary>Sets the value of the cell. </summary>
		/// <returns>true if the value has been set; otherwise, false.</returns>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="value">The cell value to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0.</exception>
		// Token: 0x0600120D RID: 4621 RVA: 0x000477C0 File Offset: 0x000459C0
		protected virtual bool SetValue(int rowIndex, object value)
		{
			object value2 = this.Value;
			if (this.DataProperty != null && !this.DataProperty.IsReadOnly)
			{
				this.DataProperty.SetValue(this.OwningRow.DataBoundItem, value);
			}
			else
			{
				this.valuex = value;
			}
			if (!object.ReferenceEquals(value2, value) || !object.Equals(value2, value))
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));
				if (this is IDataGridViewEditingCell)
				{
					(this as IDataGridViewEditingCell).EditingCellValueChanged = false;
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.InvalidateCell(this);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00047874 File Offset: 0x00045A74
		private void OnStyleChanged(object sender, EventArgs args)
		{
			if (base.DataGridView != null)
			{
				base.DataGridView.RaiseCellStyleChanged(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000478A8 File Offset: 0x00045AA8
		internal virtual Rectangle InternalErrorIconsBounds
		{
			get
			{
				return this.GetErrorIconBounds(null, null, -1);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000478B4 File Offset: 0x00045AB4
		internal void InternalPaint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000478DC File Offset: 0x00045ADC
		internal void SetOwningRow(DataGridViewRow row)
		{
			this.owningRow = row;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000478E8 File Offset: 0x00045AE8
		internal void SetOwningColumn(DataGridViewColumn col)
		{
			this.columnIndex = col.Index;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000478F8 File Offset: 0x00045AF8
		internal void SetColumnIndex(int index)
		{
			this.columnIndex = index;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00047904 File Offset: 0x00045B04
		internal void SetIsInEditMode(bool isInEditMode)
		{
			this.isInEditMode = isInEditMode;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00047910 File Offset: 0x00045B10
		internal void OnErrorTextChanged(DataGridViewCellEventArgs args)
		{
			if (base.DataGridView != null)
			{
				base.DataGridView.OnCellErrorTextChanged(args);
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004792C File Offset: 0x00045B2C
		internal TextFormatFlags AlignmentToFlags(DataGridViewContentAlignment align)
		{
			TextFormatFlags textFormatFlags = TextFormatFlags.Left;
			switch (align)
			{
			case DataGridViewContentAlignment.TopLeft:
				textFormatFlags |= TextFormatFlags.Left;
				break;
			case DataGridViewContentAlignment.TopCenter:
				textFormatFlags |= TextFormatFlags.HorizontalCenter;
				textFormatFlags |= TextFormatFlags.Left;
				break;
			default:
				if (align != DataGridViewContentAlignment.MiddleLeft)
				{
					if (align != DataGridViewContentAlignment.MiddleCenter)
					{
						if (align != DataGridViewContentAlignment.MiddleRight)
						{
							if (align != DataGridViewContentAlignment.BottomLeft)
							{
								if (align != DataGridViewContentAlignment.BottomCenter)
								{
									if (align == DataGridViewContentAlignment.BottomRight)
									{
										textFormatFlags |= TextFormatFlags.Bottom;
										textFormatFlags |= TextFormatFlags.Right;
									}
								}
								else
								{
									textFormatFlags |= TextFormatFlags.Bottom;
									textFormatFlags |= TextFormatFlags.HorizontalCenter;
								}
							}
							else
							{
								textFormatFlags |= TextFormatFlags.Bottom;
							}
						}
						else
						{
							textFormatFlags |= TextFormatFlags.VerticalCenter;
							textFormatFlags |= TextFormatFlags.Right;
						}
					}
					else
					{
						textFormatFlags |= TextFormatFlags.VerticalCenter;
						textFormatFlags |= TextFormatFlags.HorizontalCenter;
					}
				}
				else
				{
					textFormatFlags |= TextFormatFlags.VerticalCenter;
				}
				break;
			case DataGridViewContentAlignment.TopRight:
				textFormatFlags |= TextFormatFlags.Right;
				textFormatFlags |= TextFormatFlags.Left;
				break;
			}
			return textFormatFlags;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00047A00 File Offset: 0x00045C00
		internal Rectangle AlignInRectangle(Rectangle outer, Size inner, DataGridViewContentAlignment align)
		{
			int num = 0;
			int num2 = 0;
			if (align == DataGridViewContentAlignment.BottomLeft || align == DataGridViewContentAlignment.MiddleLeft || align == DataGridViewContentAlignment.TopLeft)
			{
				num = outer.X;
			}
			else if (align == DataGridViewContentAlignment.BottomCenter || align == DataGridViewContentAlignment.MiddleCenter || align == DataGridViewContentAlignment.TopCenter)
			{
				num = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			}
			else if (align == DataGridViewContentAlignment.BottomRight || align == DataGridViewContentAlignment.MiddleRight || align == DataGridViewContentAlignment.TopRight)
			{
				num = Math.Max(outer.Right - inner.Width, outer.X);
			}
			if (align == DataGridViewContentAlignment.TopCenter || align == DataGridViewContentAlignment.TopLeft || align == DataGridViewContentAlignment.TopRight)
			{
				num2 = outer.Y;
			}
			else if (align == DataGridViewContentAlignment.MiddleCenter || align == DataGridViewContentAlignment.MiddleLeft || align == DataGridViewContentAlignment.MiddleRight)
			{
				num2 = Math.Max(outer.Y + (outer.Height - inner.Height) / 2, outer.Y);
			}
			else if (align == DataGridViewContentAlignment.BottomCenter || align == DataGridViewContentAlignment.BottomRight || align == DataGridViewContentAlignment.BottomLeft)
			{
				num2 = Math.Max(outer.Bottom - inner.Height, outer.Y);
			}
			return new Rectangle(num, num2, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
		}

		// Token: 0x04000AD9 RID: 2777
		private DataGridView dataGridViewOwner;

		// Token: 0x04000ADA RID: 2778
		private AccessibleObject accessibilityObject;

		// Token: 0x04000ADB RID: 2779
		private int columnIndex;

		// Token: 0x04000ADC RID: 2780
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04000ADD RID: 2781
		private bool displayed;

		// Token: 0x04000ADE RID: 2782
		private string errorText;

		// Token: 0x04000ADF RID: 2783
		private bool isInEditMode;

		// Token: 0x04000AE0 RID: 2784
		private DataGridViewRow owningRow;

		// Token: 0x04000AE1 RID: 2785
		private DataGridViewTriState readOnly;

		// Token: 0x04000AE2 RID: 2786
		private bool selected;

		// Token: 0x04000AE3 RID: 2787
		private DataGridViewCellStyle style;

		// Token: 0x04000AE4 RID: 2788
		private object tag;

		// Token: 0x04000AE5 RID: 2789
		private string toolTipText;

		// Token: 0x04000AE6 RID: 2790
		private object valuex;

		// Token: 0x04000AE7 RID: 2791
		private Type valueType;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCell" /> to accessibility client applications.</summary>
		// Token: 0x020000E4 RID: 228
		[ComVisible(true)]
		protected class DataGridViewCellAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class without initializing the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property.</summary>
			// Token: 0x06001218 RID: 4632 RVA: 0x00047B84 File Offset: 0x00045D84
			public DataGridViewCellAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</param>
			// Token: 0x06001219 RID: 4633 RVA: 0x00047B8C File Offset: 0x00045D8C
			public DataGridViewCellAccessibleObject(DataGridViewCell owner)
			{
				this.dataGridViewCell = owner;
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x0600121A RID: 4634 RVA: 0x00047B9C File Offset: 0x00045D9C
			public override Rectangle Bounds
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
			/// <returns>The string "Edit".</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x0600121B RID: 4635 RVA: 0x00047BA4 File Offset: 0x00045DA4
			public override string DefaultAction
			{
				get
				{
					return "Edit";
				}
			}

			/// <summary>Gets the names of the owning cell's type and base type.</summary>
			/// <returns>The names of the owning cell's type and base type.</returns>
			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x0600121C RID: 4636 RVA: 0x00047BAC File Offset: 0x00045DAC
			public override string Help
			{
				get
				{
					return base.Help;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x0600121D RID: 4637 RVA: 0x00047BB4 File Offset: 0x00045DB4
			public override string Name
			{
				get
				{
					return this.dataGridViewCell.OwningColumn.HeaderText + ": " + this.dataGridViewCell.RowIndex.ToString();
				}
			}

			/// <summary>Gets or sets the cell that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has already been set.</exception>
			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x0600121E RID: 4638 RVA: 0x00047BF0 File Offset: 0x00045DF0
			// (set) Token: 0x0600121F RID: 4639 RVA: 0x00047BF8 File Offset: 0x00045DF8
			public DataGridViewCell Owner
			{
				get
				{
					return this.dataGridViewCell;
				}
				set
				{
					this.dataGridViewCell = value;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06001220 RID: 4640 RVA: 0x00047C04 File Offset: 0x00045E04
			public override AccessibleObject Parent
			{
				get
				{
					return this.dataGridViewCell.OwningRow.AccessibilityObject;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Cell" /> value.</returns>
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001221 RID: 4641 RVA: 0x00047C18 File Offset: 0x00045E18
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. </returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06001222 RID: 4642 RVA: 0x00047C1C File Offset: 0x00045E1C
			public override AccessibleStates State
			{
				get
				{
					if (this.dataGridViewCell.Selected)
					{
						return AccessibleStates.Selected;
					}
					return AccessibleStates.Focused;
				}
			}

			/// <summary>Gets or sets a string representing the formatted value of the owning cell. </summary>
			/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06001223 RID: 4643 RVA: 0x00047C34 File Offset: 0x00045E34
			// (set) Token: 0x06001224 RID: 4644 RVA: 0x00047C68 File Offset: 0x00045E68
			public override string Value
			{
				get
				{
					if (this.dataGridViewCell.FormattedValue == null)
					{
						return "(null)";
					}
					return this.dataGridViewCell.FormattedValue.ToString();
				}
				set
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException("owner is null");
					}
					throw new NotImplementedException();
				}
			}

			/// <summary>Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.-or-The value of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> property is not null and the <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is equal to -1.</exception>
			// Token: 0x06001225 RID: 4645 RVA: 0x00047C88 File Offset: 0x00045E88
			public override void DoDefaultAction()
			{
				if (this.dataGridViewCell.DataGridView.EditMode == DataGridViewEditMode.EditProgrammatically || this.dataGridViewCell.IsInEditMode)
				{
				}
			}

			/// <summary>Returns the accessible object corresponding to the specified index.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the child accessible object.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x06001226 RID: 4646 RVA: 0x00047CB8 File Offset: 0x00045EB8
			public override AccessibleObject GetChild(int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Returns the number of children that belong to the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The value 1 if the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> is being edited; otherwise, –1.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x06001227 RID: 4647 RVA: 0x00047CC0 File Offset: 0x00045EC0
			public override int GetChildCount()
			{
				if (this.dataGridViewCell.IsInEditMode)
				{
					return 1;
				}
				return -1;
			}

			/// <summary>Returns the child accessible object that has keyboard focus.</summary>
			/// <returns>null in all cases.</returns>
			// Token: 0x06001228 RID: 4648 RVA: 0x00047CD8 File Offset: 0x00045ED8
			public override AccessibleObject GetFocused()
			{
				return null;
			}

			/// <summary>Returns the child accessible object that is currently selected.</summary>
			/// <returns>null in all cases.</returns>
			// Token: 0x06001229 RID: 4649 RVA: 0x00047CDC File Offset: 0x00045EDC
			public override AccessibleObject GetSelected()
			{
				return null;
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation" /> value.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x0600122A RID: 4650 RVA: 0x00047CE0 File Offset: 0x00045EE0
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				switch (navigationDirection)
				{
				case AccessibleNavigation.Up:
					break;
				case AccessibleNavigation.Down:
					break;
				case AccessibleNavigation.Left:
					break;
				case AccessibleNavigation.Right:
					break;
				case AccessibleNavigation.Next:
					break;
				case AccessibleNavigation.Previous:
					break;
				default:
					return null;
				}
				return null;
			}

			/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
			/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x0600122B RID: 4651 RVA: 0x00047D38 File Offset: 0x00045F38
			public override void Select(AccessibleSelection flags)
			{
				if (flags != AccessibleSelection.TakeFocus)
				{
					if (flags != AccessibleSelection.TakeSelection)
					{
						if (flags != AccessibleSelection.AddSelection)
						{
							if (flags == AccessibleSelection.RemoveSelection)
							{
								this.dataGridViewCell.dataGridViewOwner.SelectedCells.InternalRemove(this.dataGridViewCell);
							}
						}
						else
						{
							this.dataGridViewCell.dataGridViewOwner.SelectedCells.InternalAdd(this.dataGridViewCell);
						}
					}
				}
				else
				{
					this.dataGridViewCell.dataGridViewOwner.Focus();
				}
			}

			// Token: 0x04000AE8 RID: 2792
			private DataGridViewCell dataGridViewCell;
		}
	}
}
