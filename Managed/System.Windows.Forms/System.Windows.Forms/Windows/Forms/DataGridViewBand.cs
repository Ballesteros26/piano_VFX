using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a linear collection of elements in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DE RID: 222
	public class DataGridViewBand : DataGridViewElement, IDisposable, ICloneable
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00044DD4 File Offset: 0x00042FD4
		internal DataGridViewBand()
		{
			this.defaultHeaderCellType = typeof(DataGridViewHeaderCell);
			this.isRow = this is DataGridViewRow;
		}

		/// <summary>Releases the resources associated with the band.</summary>
		// Token: 0x0600112B RID: 4395 RVA: 0x00044E0C File Offset: 0x0004300C
		~DataGridViewBand()
		{
			this.Dispose();
		}

		/// <summary>Gets or sets the shortcut menu for the band.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is null.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00044E48 File Offset: 0x00043048
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00044E50 File Offset: 0x00043050
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

		/// <summary>Gets or sets the default cell style of the band.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00044E5C File Offset: 0x0004305C
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00044E7C File Offset: 0x0004307C
		[Browsable(false)]
		public virtual DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				if (this.defaultCellStyle == null)
				{
					this.defaultCellStyle = new DataGridViewCellStyle();
				}
				return this.defaultCellStyle;
			}
			set
			{
				this.defaultCellStyle = value;
			}
		}

		/// <summary>Gets or sets the run-time type of the default header cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the object used as the default header cell.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Type" /> representing <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> or a derived type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00044E88 File Offset: 0x00043088
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00044E90 File Offset: 0x00043090
		[Browsable(false)]
		public Type DefaultHeaderCellType
		{
			get
			{
				return this.defaultHeaderCellType;
			}
			set
			{
				if (!value.IsSubclassOf(typeof(DataGridViewHeaderCell)))
				{
					throw new ArgumentException("Type is not DataGridViewHeaderCell or a derived type.");
				}
				this.defaultHeaderCellType = value;
			}
		}

		/// <summary>Gets a value indicating whether the band is currently displayed onscreen. </summary>
		/// <returns>true if the band is currently onscreen; otherwise, false.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00044EBC File Offset: 0x000430BC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual bool Displayed
		{
			get
			{
				return this.displayed;
			}
		}

		/// <summary>Gets or sets a value indicating whether the band will move when a user scrolls through the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>true if the band cannot be scrolled from view; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00044EC4 File Offset: 0x000430C4
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00044ECC File Offset: 0x000430CC
		[DefaultValue(false)]
		public virtual bool Frozen
		{
			get
			{
				return this.frozen;
			}
			set
			{
				if (this.frozen != value)
				{
					this.frozen = value;
					if (this.frozen)
					{
						this.SetState(this.State | DataGridViewElementStates.Frozen);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.Frozen);
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set. </summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00044F1C File Offset: 0x0004311C
		[Browsable(false)]
		public bool HasDefaultCellStyle
		{
			get
			{
				return this.defaultCellStyle != null;
			}
		}

		/// <summary>Gets the relative position of the band within the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>The zero-based position of the band in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> or <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> that it is contained within. The default is -1, indicating that there is no associated <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00044F2C File Offset: 0x0004312C
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the cell style in effect for the current band, taking into account style inheritance.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is null.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00044F34 File Offset: 0x00043134
		[Browsable(false)]
		public virtual DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return this.inheritedStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the band's cells.</summary>
		/// <returns>true if the user cannot edit the band's cells; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00044F3C File Offset: 0x0004313C
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00044F44 File Offset: 0x00043144
		[DefaultValue(false)]
		public virtual bool ReadOnly
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
					if (this.readOnly)
					{
						this.SetState(this.State | DataGridViewElementStates.ReadOnly);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.ReadOnly);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the band can be resized in the user interface (UI).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00044F94 File Offset: 0x00043194
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00044FD8 File Offset: 0x000431D8
		[Browsable(true)]
		public virtual DataGridViewTriState Resizable
		{
			get
			{
				if (this.resizable == DataGridViewTriState.NotSet && base.DataGridView != null)
				{
					return (!base.DataGridView.AllowUserToResizeColumns) ? DataGridViewTriState.False : DataGridViewTriState.True;
				}
				return this.resizable;
			}
			set
			{
				if (value != this.resizable)
				{
					this.resizable = value;
					if (this.resizable == DataGridViewTriState.True)
					{
						this.SetState(this.State | DataGridViewElementStates.Resizable);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.Resizable);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the band is in a selected user interface (UI) state.</summary>
		/// <returns>true if the band is selected; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, but the band has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control. -or-This property is being set on a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00045028 File Offset: 0x00043228
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x00045030 File Offset: 0x00043230
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				if (base.DataGridView == null)
				{
					throw new InvalidOperationException("Cant select a row non associated with a DataGridView.");
				}
				if (this.isRow)
				{
					base.DataGridView.SetSelectedRowCoreInternal(this.Index, value);
				}
				else
				{
					base.DataGridView.SetSelectedColumnCoreInternal(this.Index, value);
				}
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00045088 File Offset: 0x00043288
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x00045090 File Offset: 0x00043290
		internal bool SelectedInternal
		{
			get
			{
				return this.selected;
			}
			set
			{
				if (this.selected != value)
				{
					this.selected = value;
					if (this.selected)
					{
						this.SetState(this.State | DataGridViewElementStates.Selected);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.Selected);
					}
				}
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000450E0 File Offset: 0x000432E0
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x000450E8 File Offset: 0x000432E8
		internal bool DisplayedInternal
		{
			get
			{
				return this.displayed;
			}
			set
			{
				if (value != this.displayed)
				{
					this.displayed = value;
					if (this.displayed)
					{
						this.SetState(this.State | DataGridViewElementStates.Displayed);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.Displayed);
					}
				}
			}
		}

		/// <summary>Gets or sets the object that contains data to associate with the band.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains information associated with the band. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00045138 File Offset: 0x00043338
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00045140 File Offset: 0x00043340
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether the band is visible to the user.</summary>
		/// <returns>true if the band is visible; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the band is the row for new records.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004514C File Offset: 0x0004334C
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00045154 File Offset: 0x00043354
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					if (this.visible)
					{
						this.SetState(this.State | DataGridViewElementStates.Visible);
					}
					else
					{
						this.SetState(this.State & ~DataGridViewElementStates.Visible);
					}
				}
			}
		}

		/// <summary>Creates an exact copy of this band.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001146 RID: 4422 RVA: 0x000451A4 File Offset: 0x000433A4
		public virtual object Clone()
		{
			return new DataGridViewBand();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" />.  </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001147 RID: 4423 RVA: 0x000451B8 File Offset: 0x000433B8
		public void Dispose()
		{
		}

		/// <summary>Returns a string that represents the current band.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001148 RID: 4424 RVA: 0x000451BC File Offset: 0x000433BC
		public override string ToString()
		{
			return base.GetType().Name + ": " + this.index.ToString() + ".";
		}

		/// <summary>Gets or sets the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> representing the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewRow" />.-or-The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x000451F0 File Offset: 0x000433F0
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x000451F8 File Offset: 0x000433F8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		protected DataGridViewHeaderCell HeaderCellCore
		{
			get
			{
				return this.headerCellCore;
			}
			set
			{
				this.headerCellCore = value;
			}
		}

		/// <summary>Gets a value indicating whether the band represents a row.</summary>
		/// <returns>true if the band represents a <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, false.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00045204 File Offset: 0x00043404
		protected bool IsRow
		{
			get
			{
				return this.isRow;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" /> and optionally releases the managed resources.  </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600114C RID: 4428 RVA: 0x0004520C File Offset: 0x0004340C
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Called when the band is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x0600114D RID: 4429 RVA: 0x00045210 File Offset: 0x00043410
		protected override void OnDataGridViewChanged()
		{
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00045214 File Offset: 0x00043414
		internal virtual void SetIndex(int index)
		{
			this.index = index;
		}

		// Token: 0x04000AC5 RID: 2757
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04000AC6 RID: 2758
		private DataGridViewCellStyle defaultCellStyle;

		// Token: 0x04000AC7 RID: 2759
		private Type defaultHeaderCellType;

		// Token: 0x04000AC8 RID: 2760
		private bool displayed;

		// Token: 0x04000AC9 RID: 2761
		private bool frozen;

		// Token: 0x04000ACA RID: 2762
		private int index = -1;

		// Token: 0x04000ACB RID: 2763
		private bool readOnly;

		// Token: 0x04000ACC RID: 2764
		private DataGridViewTriState resizable;

		// Token: 0x04000ACD RID: 2765
		private bool selected;

		// Token: 0x04000ACE RID: 2766
		private object tag;

		// Token: 0x04000ACF RID: 2767
		private bool visible = true;

		// Token: 0x04000AD0 RID: 2768
		private DataGridViewHeaderCell headerCellCore;

		// Token: 0x04000AD1 RID: 2769
		private bool isRow;

		// Token: 0x04000AD2 RID: 2770
		private DataGridViewCellStyle inheritedStyle;
	}
}
