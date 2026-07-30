using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011F RID: 287
	[TypeConverter(typeof(DataGridViewRowConverter))]
	public class DataGridViewRow : DataGridViewBand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> class without using a template.</summary>
		// Token: 0x060014A7 RID: 5287 RVA: 0x0004DD5C File Offset: 0x0004BF5C
		public DataGridViewRow()
		{
			this.cells = new DataGridViewCellCollection(this);
			this.minimumHeight = 3;
			this.height = -1;
			this.explicit_height = -1;
			this.headerCell = new DataGridViewRowHeaderCell();
			this.headerCell.SetOwningRow(this);
			this.accessibilityObject = new AccessibleObject();
			this.SetState(DataGridViewElementStates.Visible);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0004DDBC File Offset: 0x0004BFBC
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				return this.accessibilityObject;
			}
		}

		/// <summary>Gets the collection of cells that populate the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> that contains all of the cells in the row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
		[Browsable(false)]
		[DesignerSerializationVisibility(2)]
		public DataGridViewCellCollection Cells
		{
			get
			{
				return this.cells;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the row.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewRow" />. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0004DDCC File Offset: 0x0004BFCC
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0004DDEC File Offset: 0x0004BFEC
		[DefaultValue(null)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Operation cannot be performed on a shared row.");
				}
				return this.contextMenuStrip;
			}
			set
			{
				if (this.contextMenuStrip != value)
				{
					this.contextMenuStrip = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnRowContextMenuStripChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets the data-bound object that populated the row.</summary>
		/// <returns>The data-bound <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0004DE20 File Offset: 0x0004C020
		[Browsable(false)]
		[EditorBrowsable(2)]
		public object DataBoundItem
		{
			get
			{
				if (base.DataGridView != null && base.DataGridView.DataManager != null && base.DataGridView.DataManager.Count > base.Index)
				{
					return base.DataGridView.DataManager[base.Index];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the default styles for the row, which are used to render cells in the row unless the styles are overridden. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004DE7C File Offset: 0x0004C07C
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x0004DE84 File Offset: 0x0004C084
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(2)]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				if (this.DefaultCellStyle != value)
				{
					base.DefaultCellStyle = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnRowDefaultCellStyleChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether this row is displayed on the screen.</summary>
		/// <returns>true if the row is currently displayed on the screen; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004DEC0 File Offset: 0x0004C0C0
		[Browsable(false)]
		public override bool Displayed
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the Displayed property of a shared row is not a valid operation.");
				}
				return base.Displayed;
			}
		}

		/// <summary>Gets or sets the height, in pixels, of the row divider.</summary>
		/// <returns>The height, in pixels, of the divider (the row's bottom margin). </returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0004DEE0 File Offset: 0x0004C0E0
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0004DEE8 File Offset: 0x0004C0E8
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int DividerHeight
		{
			get
			{
				return this.dividerHeight;
			}
			set
			{
				this.dividerHeight = value;
			}
		}

		/// <summary>Gets or sets the error message text for row-level errors.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the error message.</returns>
		/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is a shared row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004DEF4 File Offset: 0x0004C0F4
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x0004DF28 File Offset: 0x0004C128
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ErrorText
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Operation cannot be performed on a shared row.");
				}
				return (this.errorText != null) ? this.errorText : string.Empty;
			}
			set
			{
				if (this.errorText != value)
				{
					this.errorText = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnRowErrorTextChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is frozen. </summary>
		/// <returns>true if the row is frozen; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0004DF6C File Offset: 0x0004C16C
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0004DF8C File Offset: 0x0004C18C
		[Browsable(false)]
		public override bool Frozen
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the Frozen property of a shared row is not a valid operation.");
				}
				return base.Frozen;
			}
			set
			{
				base.Frozen = value;
			}
		}

		/// <summary>Gets or sets the row's header cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> that represents the header cell of row.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0004DF98 File Offset: 0x0004C198
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DataGridViewRowHeaderCell HeaderCell
		{
			get
			{
				return this.headerCell;
			}
			set
			{
				if (this.headerCell != value)
				{
					this.headerCell = value;
					this.headerCell.SetOwningRow(this);
					if (base.DataGridView != null)
					{
						this.headerCell.SetDataGridView(base.DataGridView);
						base.DataGridView.OnRowHeaderCellChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets the current height of the row.</summary>
		/// <returns>The height, in pixels, of the row. The default is the height of the default font plus 9 pixels.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004DFFC File Offset: 0x0004C1FC
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0004E094 File Offset: 0x0004C294
		[DefaultValue(22)]
		[NotifyParentProperty(true)]
		public int Height
		{
			get
			{
				if (this.height >= 0)
				{
					return this.height;
				}
				if (this.DefaultCellStyle != null && this.DefaultCellStyle.Font != null)
				{
					return this.DefaultCellStyle.Font.Height + 9;
				}
				if (base.Index >= 0 && this.InheritedStyle != null && this.InheritedStyle.Font != null)
				{
					return this.InheritedStyle.Font.Height + 9;
				}
				return Control.DefaultFont.Height + 9;
			}
			set
			{
				this.explicit_height = value;
				if (this.height != value)
				{
					if (value < this.minimumHeight)
					{
						throw new ArgumentOutOfRangeException("Height can't be less than MinimumHeight.");
					}
					this.height = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.Invalidate();
						base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets the cell style in effect for the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that specifies the formatting and style information for the cells in the row.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0004E0FC File Offset: 0x0004C2FC
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				if (base.Index == -1)
				{
					throw new InvalidOperationException("Getting the InheritedStyle property of a shared row is not a valid operation.");
				}
				if (base.DataGridView == null)
				{
					return this.DefaultCellStyle;
				}
				if (this.DefaultCellStyle == null)
				{
					return base.DataGridView.DefaultCellStyle;
				}
				return this.DefaultCellStyle.Clone();
			}
		}

		/// <summary>Gets a value indicating whether the row is the row for new records.</summary>
		/// <returns>true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" />, which is used for the entry of a new row of data; otherwise, false.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0004E158 File Offset: 0x0004C358
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool IsNewRow
		{
			get
			{
				return base.DataGridView != null && base.DataGridView.Rows[base.DataGridView.Rows.Count - 1] == this && base.DataGridView.NewRowIndex == base.Index;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0004E1B4 File Offset: 0x0004C3B4
		internal bool IsShared
		{
			get
			{
				return base.Index == -1 && base.DataGridView != null;
			}
		}

		/// <summary>Gets or sets the minimum height of the row.</summary>
		/// <returns>The minimum row height in pixels, ranging from 2 to <see cref="F:System.Int32.MaxValue" />. The default is 3.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x0004E1DC File Offset: 0x0004C3DC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
			set
			{
				if (this.minimumHeight != value)
				{
					if (value < 2 || value > 2147483647)
					{
						throw new ArgumentOutOfRangeException("MinimumHeight should be between 2 and Int32.MaxValue.");
					}
					this.minimumHeight = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnRowMinimumHeightChanged(new DataGridViewRowEventArgs(this));
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is read-only.</summary>
		/// <returns>true if the row is read-only; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0004E238 File Offset: 0x0004C438
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x0004E280 File Offset: 0x0004C480
		[Browsable(true)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public override bool ReadOnly
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the ReadOnly property of a shared row is not a valid operation.");
				}
				return (base.DataGridView != null && base.DataGridView.ReadOnly) || base.ReadOnly;
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether users can resize the row or indicating that the behavior is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value that indicates whether the row can be resized or whether it can be resized only when the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property is set to true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0004E28C File Offset: 0x0004C48C
		// (set) Token: 0x060014C2 RID: 5314 RVA: 0x0004E2AC File Offset: 0x0004C4AC
		[NotifyParentProperty(true)]
		public override DataGridViewTriState Resizable
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the Resizable property of a shared row is not a valid operation.");
				}
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is selected. </summary>
		/// <returns>true if the row is selected; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0004E2B8 File Offset: 0x0004C4B8
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x0004E2D8 File Offset: 0x0004C4D8
		public override bool Selected
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the Selected property of a shared row is not a valid operation.");
				}
				return base.Selected;
			}
			set
			{
				if (base.Index == -1)
				{
					throw new InvalidOperationException("The row is a shared row.");
				}
				if (base.DataGridView == null)
				{
					throw new InvalidOperationException("The row has not been added to a DataGridView control.");
				}
				base.Selected = value;
			}
		}

		/// <summary>Gets the current state of the row.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0004E31C File Offset: 0x0004C51C
		public override DataGridViewElementStates State
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the State property of a shared row is not a valid operation.");
				}
				return base.State;
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is visible. </summary>
		/// <returns>true if the row is visible; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0004E33C File Offset: 0x0004C53C
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0004E35C File Offset: 0x0004C55C
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				if (this.IsShared)
				{
					throw new InvalidOperationException("Getting the Visible property of a shared row is not a valid operation.");
				}
				return base.Visible;
			}
			set
			{
				if (this.IsNewRow && !value)
				{
					throw new InvalidOperationException("Cant make invisible a new row.");
				}
				if (!value && base.DataGridView != null && base.DataGridView.DataManager != null && base.DataGridView.DataManager.Position == base.Index)
				{
					throw new InvalidOperationException("Row associated with the currency manager's position cannot be made invisible.");
				}
				base.Visible = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.Invalidate();
				}
			}
		}

		/// <summary>Modifies an input row header border style according to the specified criteria.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the new border style used.</returns>
		/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the row header border style to modify. </param>
		/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the row header border style.</param>
		/// <param name="singleVerticalBorderAdded">true to add a single vertical border to the result; otherwise, false. </param>
		/// <param name="singleHorizontalBorderAdded">true to add a single horizontal border to the result; otherwise, false. </param>
		/// <param name="isFirstDisplayedRow">true if the row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false. </param>
		/// <param name="isLastVisibleRow">true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has its <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014C8 RID: 5320 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		[EditorBrowsable(2)]
		public virtual DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an exact copy of this row.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014C9 RID: 5321 RVA: 0x0004E3F4 File Offset: 0x0004C5F4
		public override object Clone()
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)base.MemberwiseClone();
			dataGridViewRow.HeaderCell = (DataGridViewRowHeaderCell)this.HeaderCell.Clone();
			dataGridViewRow.SetIndex(-1);
			dataGridViewRow.cells = new DataGridViewCellCollection(dataGridViewRow);
			foreach (object obj in this.cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewRow.cells.Add(dataGridViewCell.Clone() as DataGridViewCell);
			}
			dataGridViewRow.SetDataGridView(null);
			return dataGridViewRow;
		}

		/// <summary>Clears the existing cells and sets their template according to the supplied <see cref="T:System.Windows.Forms.DataGridView" /> template.</summary>
		/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added. -or-A column that has no cell template was added.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014CA RID: 5322 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		public void CreateCells(DataGridView dataGridView)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("DataGridView is null.");
			}
			if (dataGridView.Rows.Contains(this))
			{
				throw new InvalidOperationException("The row already exists in the DataGridView.");
			}
			DataGridViewCellCollection dataGridViewCellCollection = new DataGridViewCellCollection(this);
			foreach (object obj in dataGridView.Columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.CellTemplate == null)
				{
					throw new InvalidOperationException("Cell template not set in column: " + dataGridViewColumn.Index.ToString() + ".");
				}
				dataGridViewCellCollection.Add((DataGridViewCell)dataGridViewColumn.CellTemplate.Clone());
			}
			this.cells = dataGridViewCellCollection;
		}

		/// <summary>Clears the existing cells and sets their template and values.</summary>
		/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles. </param>
		/// <param name="values">An array of objects that initialize the reset cells. </param>
		/// <exception cref="T:System.ArgumentNullException">Either of the parameters is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added. -or-A column that has no cell template was added.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014CB RID: 5323 RVA: 0x0004E59C File Offset: 0x0004C79C
		public void CreateCells(DataGridView dataGridView, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values is null");
			}
			this.CreateCells(dataGridView);
			for (int i = 0; i < values.Length; i++)
			{
				this.cells[i].Value = values[i];
			}
		}

		/// <summary>Gets the shortcut menu for the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
		/// <param name="rowIndex">The index of the current row.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="rowIndex" /> is -1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control minus one.</exception>
		// Token: 0x060014CC RID: 5324 RVA: 0x0004E5EC File Offset: 0x0004C7EC
		public ContextMenuStrip GetContextMenuStrip(int rowIndex)
		{
			if (rowIndex == -1)
			{
				throw new InvalidOperationException("rowIndex is -1");
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex is out of range");
			}
			return null;
		}

		/// <summary>Gets the error text for the row at the specified index.</summary>
		/// <returns>A string that describes the error of the row at the specified index.</returns>
		/// <param name="rowIndex">The index of the row that contains the error.</param>
		/// <exception cref="T:System.InvalidOperationException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one. </exception>
		// Token: 0x060014CD RID: 5325 RVA: 0x0004E62C File Offset: 0x0004C82C
		public string GetErrorText(int rowIndex)
		{
			return string.Empty;
		}

		/// <summary>Calculates the ideal height of the specified row based on the specified criteria.</summary>
		/// <returns>The ideal height of the row, in pixels.</returns>
		/// <param name="rowIndex">The index of the row whose preferred height is calculated.</param>
		/// <param name="autoSizeRowMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> that specifies an automatic sizing mode.</param>
		/// <param name="fixedWidth">true to calculate the preferred height for a fixed cell width; otherwise, false.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows in the control minus 1. </exception>
		// Token: 0x060014CE RID: 5326 RVA: 0x0004E634 File Offset: 0x0004C834
		public virtual int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
		{
			DataGridViewRow dataGridViewRow;
			if (base.DataGridView != null)
			{
				dataGridViewRow = base.DataGridView.Rows.SharedRow(rowIndex);
			}
			else
			{
				dataGridViewRow = this;
			}
			int num = 0;
			if (autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCells || autoSizeRowMode == DataGridViewAutoSizeRowMode.RowHeader)
			{
				num = Math.Max(num, dataGridViewRow.HeaderCell.PreferredSize.Height);
			}
			if (autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCells || autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCellsExceptHeader)
			{
				foreach (object obj in dataGridViewRow.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					num = Math.Max(num, dataGridViewCell.PreferredSize.Height);
				}
			}
			return num;
		}

		/// <summary>Returns a value indicating the current state of the row.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
		/// <param name="rowIndex">The index of the row.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row has been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.ArgumentException">The row is not a shared row, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.-or-The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.</exception>
		// Token: 0x060014CF RID: 5327 RVA: 0x0004E714 File Offset: 0x0004C914
		[EditorBrowsable(2)]
		public virtual DataGridViewElementStates GetState(int rowIndex)
		{
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.None;
			if (rowIndex == -1)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Displayed;
				if (base.DataGridView.ReadOnly)
				{
					dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
				}
				if (base.DataGridView.AllowUserToResizeRows)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.DataGridView.Visible)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
				}
				return dataGridViewElementStates;
			}
			DataGridViewRow dataGridViewRow = base.DataGridView.Rows[rowIndex];
			if (dataGridViewRow.Displayed)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Displayed;
			}
			if (dataGridViewRow.Frozen)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Frozen;
			}
			if (dataGridViewRow.ReadOnly)
			{
				dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
			}
			if (dataGridViewRow.Resizable == DataGridViewTriState.True || (dataGridViewRow.Resizable == DataGridViewTriState.NotSet && base.DataGridView.AllowUserToResizeRows))
			{
				dataGridViewElementStates |= DataGridViewElementStates.Resizable;
			}
			if (dataGridViewRow.Resizable == DataGridViewTriState.True)
			{
				dataGridViewElementStates |= DataGridViewElementStates.ResizableSet;
			}
			if (dataGridViewRow.Selected)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Selected;
			}
			if (dataGridViewRow.Visible)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Visible;
			}
			return dataGridViewElementStates;
		}

		/// <summary>Sets the values of the row's cells.</summary>
		/// <returns>true if all values have been set; otherwise, false.</returns>
		/// <param name="values">One or more objects that represent the cell values in the row.-or-An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called when the associated <see cref="T:System.Windows.Forms.DataGridView" /> is operating in virtual mode. -or-This row is a shared row.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014D0 RID: 5328 RVA: 0x0004E80C File Offset: 0x0004CA0C
		public bool SetValues(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("vues is null");
			}
			if (base.DataGridView != null && base.DataGridView.VirtualMode)
			{
				throw new InvalidOperationException("DataGridView is operating in virtual mode");
			}
			for (int i = 0; i < values.Length; i++)
			{
				DataGridViewCell dataGridViewCell;
				if (this.cells.Count > i)
				{
					dataGridViewCell = this.cells[i];
				}
				else
				{
					dataGridViewCell = new DataGridViewTextBoxCell();
					this.cells.Add(dataGridViewCell);
				}
				dataGridViewCell.Value = values[i];
			}
			return true;
		}

		/// <summary>Gets a human-readable string that describes the row.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014D1 RID: 5329 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		public override string ToString()
		{
			return base.GetType().Name + ", Band Index: " + base.Index.ToString();
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewRow" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewRow" />. </returns>
		// Token: 0x060014D2 RID: 5330 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewRow.DataGridViewRowAccessibleObject(this);
		}

		/// <summary>Constructs a new collection of cells based on this row.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
		// Token: 0x060014D3 RID: 5331 RVA: 0x0004E8DC File Offset: 0x0004CADC
		[EditorBrowsable(2)]
		protected virtual DataGridViewCellCollection CreateCellsInstance()
		{
			this.cells = new DataGridViewCellCollection(this);
			return this.cells;
		}

		/// <summary>Draws a focus rectangle around the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used to paint the focus rectangle.</param>
		/// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is null.-or-<paramref name="cellStyle" /> is null.</exception>
		// Token: 0x060014D4 RID: 5332 RVA: 0x0004E8F0 File Offset: 0x0004CAF0
		[EditorBrowsable(2)]
		protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, DataGridViewElementStates rowState, DataGridViewCellStyle cellStyle, bool cellsPaintSelectionBackground)
		{
		}

		/// <summary>Paints the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
		/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x060014D5 RID: 5333 RVA: 0x0004E8F4 File Offset: 0x0004CAF4
		protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			DataGridViewCellStyle dataGridViewCellStyle;
			if (base.Index == -1)
			{
				dataGridViewCellStyle = base.DataGridView.RowsDefaultCellStyle;
			}
			else
			{
				dataGridViewCellStyle = this.InheritedStyle;
			}
			DataGridViewRowPrePaintEventArgs dataGridViewRowPrePaintEventArgs = new DataGridViewRowPrePaintEventArgs(base.DataGridView, graphics, clipBounds, rowBounds, rowIndex, rowState, string.Empty, dataGridViewCellStyle, isFirstDisplayedRow, isLastVisibleRow);
			dataGridViewRowPrePaintEventArgs.PaintParts = DataGridViewPaintParts.All;
			base.DataGridView.OnRowPrePaint(dataGridViewRowPrePaintEventArgs);
			if (dataGridViewRowPrePaintEventArgs.Handled)
			{
				return;
			}
			if (base.DataGridView.RowHeadersVisible)
			{
				this.PaintHeader(graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, dataGridViewRowPrePaintEventArgs.PaintParts);
			}
			this.PaintCells(graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, dataGridViewRowPrePaintEventArgs.PaintParts);
			DataGridViewRowPostPaintEventArgs dataGridViewRowPostPaintEventArgs = new DataGridViewRowPostPaintEventArgs(base.DataGridView, graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, dataGridViewRowPrePaintEventArgs.ErrorText, dataGridViewCellStyle, isFirstDisplayedRow, isLastVisibleRow);
			base.DataGridView.OnRowPostPaint(dataGridViewRowPostPaintEventArgs);
		}

		/// <summary>Paints the cells in the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
		/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x060014D6 RID: 5334 RVA: 0x0004E9DC File Offset: 0x0004CBDC
		[EditorBrowsable(2)]
		protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			List<DataGridViewColumn> columnDisplayIndexSortedArrayList = base.DataGridView.Columns.ColumnDisplayIndexSortedArrayList;
			Rectangle rectangle = rowBounds;
			if (base.DataGridView.RowHeadersVisible)
			{
				rectangle.X += base.DataGridView.RowHeadersWidth;
				rectangle.Width -= base.DataGridView.RowHeadersWidth;
			}
			for (int i = base.DataGridView.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = columnDisplayIndexSortedArrayList[i];
				if (dataGridViewColumn.Visible)
				{
					if (!dataGridViewColumn.Displayed)
					{
						break;
					}
					rectangle.Width = dataGridViewColumn.Width;
					DataGridViewCell dataGridViewCell = this.Cells[dataGridViewColumn.Index];
					if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
					{
						graphics.FillRectangle(Brushes.White, rectangle);
					}
					DataGridViewCellStyle dataGridViewCellStyle;
					if (dataGridViewCell.RowIndex == -1)
					{
						dataGridViewCellStyle = this.DefaultCellStyle;
					}
					else
					{
						dataGridViewCellStyle = dataGridViewCell.InheritedStyle;
					}
					object obj;
					DataGridViewElementStates dataGridViewElementStates;
					if (dataGridViewCell.RowIndex == -1)
					{
						obj = null;
						dataGridViewElementStates = dataGridViewCell.State;
					}
					else
					{
						obj = dataGridViewCell.Value;
						object formattedValue = dataGridViewCell.FormattedValue;
						string text = dataGridViewCell.ErrorText;
						dataGridViewElementStates = dataGridViewCell.InheritedState;
					}
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = (DataGridViewAdvancedBorderStyle)base.DataGridView.AdvancedCellBorderStyle.Clone();
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = dataGridViewCell.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, true, true, dataGridViewCell.ColumnIndex == 0, dataGridViewCell.RowIndex == 0);
					base.DataGridView.OnCellFormattingInternal(new DataGridViewCellFormattingEventArgs(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, obj, dataGridViewCell.FormattedValueType, dataGridViewCellStyle));
					dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, dataGridViewElementStates, dataGridViewCellStyle, dataGridViewAdvancedBorderStyle2, paintParts);
					rectangle.X += rectangle.Width;
				}
			}
		}

		/// <summary>Paints the header cell of the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">true to indicate that the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
		/// <param name="isLastVisibleRow">true to indicate that the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x060014D7 RID: 5335 RVA: 0x0004EBC0 File Offset: 0x0004CDC0
		[EditorBrowsable(2)]
		protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			rowBounds.Width = base.DataGridView.RowHeadersWidth;
			graphics.FillRectangle(Brushes.White, rowBounds);
			this.HeaderCell.PaintWork(graphics, clipBounds, rowBounds, rowIndex, rowState, this.HeaderCell.InheritedStyle, base.DataGridView.AdvancedRowHeadersBorderStyle, paintParts);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0004EC18 File Offset: 0x0004CE18
		internal override void SetDataGridView(DataGridView dataGridView)
		{
			base.SetDataGridView(dataGridView);
			this.headerCell.SetDataGridView(dataGridView);
			foreach (object obj in this.cells)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.SetDataGridView(dataGridView);
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0004EC9C File Offset: 0x0004CE9C
		internal override void SetState(DataGridViewElementStates state)
		{
			if (this.State != state)
			{
				base.SetState(state);
				if (base.DataGridView != null)
				{
					base.DataGridView.OnRowStateChanged(base.Index, new DataGridViewRowStateChangedEventArgs(this, state));
				}
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0004ECE0 File Offset: 0x0004CEE0
		internal void SetAutoSizeHeight(int height)
		{
			this.height = height;
			if (base.DataGridView != null)
			{
				base.DataGridView.Invalidate();
				base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004ED1C File Offset: 0x0004CF1C
		internal void ResetToExplicitHeight()
		{
			this.height = this.explicit_height;
			if (base.DataGridView != null)
			{
				base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
			}
		}

		// Token: 0x04000BE7 RID: 3047
		private AccessibleObject accessibilityObject;

		// Token: 0x04000BE8 RID: 3048
		private DataGridViewCellCollection cells;

		// Token: 0x04000BE9 RID: 3049
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04000BEA RID: 3050
		private int dividerHeight;

		// Token: 0x04000BEB RID: 3051
		private string errorText;

		// Token: 0x04000BEC RID: 3052
		private DataGridViewRowHeaderCell headerCell;

		// Token: 0x04000BED RID: 3053
		private int height;

		// Token: 0x04000BEE RID: 3054
		private int minimumHeight;

		// Token: 0x04000BEF RID: 3055
		private int explicit_height;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to accessibility client applications.</summary>
		// Token: 0x02000120 RID: 288
		[ComVisible(true)]
		protected class DataGridViewRowAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class without setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property.</summary>
			// Token: 0x060014DC RID: 5340 RVA: 0x0004ED54 File Offset: 0x0004CF54
			public DataGridViewRowAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /></param>
			// Token: 0x060014DD RID: 5341 RVA: 0x0004ED5C File Offset: 0x0004CF5C
			public DataGridViewRowAccessibleObject(DataGridViewRow owner)
			{
				this.dataGridViewRow = owner;
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x060014DE RID: 5342 RVA: 0x0004ED6C File Offset: 0x0004CF6C
			public override Rectangle Bounds
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004ED74 File Offset: 0x0004CF74
			public override string Name
			{
				get
				{
					return "Index: " + this.dataGridViewRow.Index.ToString();
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to which this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> applies.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property has already been set.</exception>
			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
			// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0004EDA8 File Offset: 0x0004CFA8
			public DataGridViewRow Owner
			{
				get
				{
					return this.dataGridViewRow;
				}
				set
				{
					this.dataGridViewRow = value;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0004EDB4 File Offset: 0x0004CFB4
			public override AccessibleObject Parent
			{
				get
				{
					return this.dataGridViewRow.AccessibilityObject;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Row" /> value.</returns>
			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0004EDC4 File Offset: 0x0004CFC4
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is the bitwise combination of the <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" /> and <see cref="F:System.Windows.Forms.AccessibleStates.Focusable" /> values.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0004EDC8 File Offset: 0x0004CFC8
			public override AccessibleStates State
			{
				get
				{
					if (this.dataGridViewRow.Selected)
					{
						return AccessibleStates.Selected;
					}
					return AccessibleStates.Focused;
				}
			}

			/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0004EDE0 File Offset: 0x0004CFE0
			public override string Value
			{
				get
				{
					if (this.dataGridViewRow.Cells.Count == 0)
					{
						return "(Create New)";
					}
					string text = string.Empty;
					foreach (object obj in this.dataGridViewRow.Cells)
					{
						DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
						text += dataGridViewCell.AccessibilityObject.Value;
					}
					return text;
				}
			}

			/// <summary>Returns the accessible child corresponding to the specified index.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the accessible child.</param>
			/// <exception cref="T:System.InvalidOperationException">
			///   <paramref name="index" /> is less than 0.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014E6 RID: 5350 RVA: 0x0004EE84 File Offset: 0x0004D084
			public override AccessibleObject GetChild(int index)
			{
				throw new NotImplementedException();
			}

			/// <summary>Returns the number of children belonging to the accessible object.</summary>
			/// <returns>The number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> corresponds to the number of visible columns in the <see cref="T:System.Windows.Forms.DataGridView" />. If the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> property is true, the <see cref="M:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.GetChildCount" /> method includes the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> in the count of child accessible objects.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014E7 RID: 5351 RVA: 0x0004EE8C File Offset: 0x0004D08C
			public override int GetChildCount()
			{
				throw new NotImplementedException();
			}

			/// <summary>Returns the accessible object that has keyboard focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> if the cell indicated by the <see cref="P:System.Windows.Forms.DataGridView.CurrentCell" /> property has keyboard focus and is in the current <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, null.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014E8 RID: 5352 RVA: 0x0004EE94 File Offset: 0x0004D094
			public override AccessibleObject GetFocused()
			{
				return null;
			}

			/// <summary>Gets an accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects.</summary>
			/// <returns>An accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014E9 RID: 5353 RVA: 0x0004EE98 File Offset: 0x0004D098
			public override AccessibleObject GetSelected()
			{
				return null;
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014EA RID: 5354 RVA: 0x0004EE9C File Offset: 0x0004D09C
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
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x060014EB RID: 5355 RVA: 0x0004EEF4 File Offset: 0x0004D0F4
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
								this.dataGridViewRow.DataGridView.SelectedRows.InternalRemove(this.dataGridViewRow);
							}
						}
						else
						{
							this.dataGridViewRow.DataGridView.SelectedRows.InternalAdd(this.dataGridViewRow);
						}
					}
				}
				else
				{
					this.dataGridViewRow.DataGridView.Focus();
				}
			}

			// Token: 0x04000BF0 RID: 3056
			private DataGridViewRow dataGridViewRow;
		}
	}
}
