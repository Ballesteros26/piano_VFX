using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Displays a combo box in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000108 RID: 264
	public class DataGridViewComboBoxCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> class.</summary>
		// Token: 0x06001390 RID: 5008 RVA: 0x0004B2B8 File Offset: 0x000494B8
		public DataGridViewComboBoxCell()
		{
			this.autoComplete = true;
			this.dataSource = null;
			this.displayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
			this.displayStyleForCurrentCellOnly = false;
			this.dropDownWidth = 1;
			this.flatStyle = FlatStyle.Standard;
			this.items = new DataGridViewComboBoxCell.ObjectCollection(this);
			this.maxDropDownItems = 8;
			this.sorted = false;
			this.owningColumnTemlate = null;
		}

		/// <summary>Gets or sets a value indicating whether the cell will match the characters being entered in the cell with a selection from the drop-down list. </summary>
		/// <returns>true if automatic completion is activated; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0004B318 File Offset: 0x00049518
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0004B320 File Offset: 0x00049520
		[DefaultValue(true)]
		public virtual bool AutoComplete
		{
			get
			{
				return this.autoComplete;
			}
			set
			{
				this.autoComplete = value;
			}
		}

		/// <summary>Gets or sets the data source whose data contains the possible selections shown in the drop-down list.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to the drop-down list. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not null and is not of type <see cref="T:System.Collections.IList" /> nor <see cref="T:System.ComponentModel.IListSource" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0004B32C File Offset: 0x0004952C
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0004B334 File Offset: 0x00049534
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value is IList || value is IListSource || value == null)
				{
					this.dataSource = value;
					return;
				}
				throw new Exception("Value is no IList, IListSource or null.");
			}
		}

		/// <summary>Gets or sets a string that specifies where to gather selections to display in the drop-down list.</summary>
		/// <returns>A string specifying the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property. The default value is <see cref="F:System.String.Empty" />, which indicates that the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property will not be used.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the specified value when setting this property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0004B370 File Offset: 0x00049570
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x0004B378 File Offset: 0x00049578
		[DefaultValue("")]
		public virtual string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
			set
			{
				this.displayMember = value;
			}
		}

		/// <summary>Gets or sets a value that determines how the combo box is displayed when it is not in edit mode.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value.</exception>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004B384 File Offset: 0x00049584
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0004B38C File Offset: 0x0004958C
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				return this.displayStyle;
			}
			set
			{
				this.displayStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayStyle" /> property value applies to the cell only when it is the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>true if the display style applies to the cell only when it is the current cell; otherwise false. The default is false.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0004B398 File Offset: 0x00049598
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0004B3A0 File Offset: 0x000495A0
		[DefaultValue(false)]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				return this.displayStyleForCurrentCellOnly;
			}
			set
			{
				this.displayStyleForCurrentCellOnly = value;
			}
		}

		/// <summary>Gets or sets the width of the of the drop-down list portion of a combo box.</summary>
		/// <returns>The width, in pixels, of the drop-down list. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than one.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0004B3AC File Offset: 0x000495AC
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0004B3B4 File Offset: 0x000495B4
		[DefaultValue(1)]
		public virtual int DropDownWidth
		{
			get
			{
				return this.dropDownWidth;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Value is less than 1.");
				}
				this.dropDownWidth = value;
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. This property always returns <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0004B3D0 File Offset: 0x000495D0
		public override Type EditType
		{
			get
			{
				return typeof(DataGridViewComboBoxEditingControl);
			}
		}

		/// <summary>Gets or sets the flat style appearance of the cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0004B3DC File Offset: 0x000495DC
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0004B3E4 File Offset: 0x000495E4
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid FlatStyle.");
				}
				this.flatStyle = value;
			}
		}

		/// <summary>Gets the class type of the formatted value associated with the cell.</summary>
		/// <returns>The type of the cell's formatted value. This property always returns <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004B420 File Offset: 0x00049620
		public override Type FormattedValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>Gets the objects that represent the selection displayed in the drop-down list. </summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> containing the selection. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0004B42C File Offset: 0x0004962C
		[Browsable(false)]
		public virtual DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				if (base.DataGridView != null && base.DataGridView.BindingContext != null && this.DataSource != null && !string.IsNullOrEmpty(this.ValueMember))
				{
					this.items.ClearInternal();
					CurrencyManager currencyManager = (CurrencyManager)base.DataGridView.BindingContext[this.DataSource];
					if (currencyManager != null && currencyManager.Count > 0)
					{
						foreach (object obj in currencyManager.List)
						{
							this.items.AddInternal(obj);
						}
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the maximum number of items shown in the drop-down list.</summary>
		/// <returns>The number of drop-down list items to allow. The minimum is 1 and the maximum is 100; the default is 8.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 1 or greater than 100 when setting this property.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0004B514 File Offset: 0x00049714
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0004B51C File Offset: 0x0004971C
		[DefaultValue(8)]
		public virtual int MaxDropDownItems
		{
			get
			{
				return this.maxDropDownItems;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("Value is less than 1 or greater than 100.");
				}
				this.maxDropDownItems = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are automatically sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to sort a cell that is attached to a data source.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0004B540 File Offset: 0x00049740
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0004B548 File Offset: 0x00049748
		[DefaultValue(false)]
		public virtual bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				this.sorted = value;
			}
		}

		/// <summary>Gets or sets a string that specifies where to gather the underlying values used in the drop-down list.</summary>
		/// <returns>A string specifying the name of a property or column. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is ignored.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the specified value when setting this property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0004B554 File Offset: 0x00049754
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0004B55C File Offset: 0x0004975C
		[DefaultValue("")]
		public virtual string ValueMember
		{
			get
			{
				return this.valueMember;
			}
			set
			{
				this.valueMember = value;
			}
		}

		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0004B568 File Offset: 0x00049768
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004B574 File Offset: 0x00049774
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x0004B57C File Offset: 0x0004977C
		internal DataGridViewComboBoxColumn OwningColumnTemplate
		{
			get
			{
				return this.owningColumnTemlate;
			}
			set
			{
				this.owningColumnTemlate = value;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013AB RID: 5035 RVA: 0x0004B588 File Offset: 0x00049788
		public override object Clone()
		{
			DataGridViewComboBoxCell dataGridViewComboBoxCell = (DataGridViewComboBoxCell)base.Clone();
			dataGridViewComboBoxCell.autoComplete = this.autoComplete;
			dataGridViewComboBoxCell.dataSource = this.dataSource;
			dataGridViewComboBoxCell.displayStyle = this.displayStyle;
			dataGridViewComboBoxCell.displayMember = this.displayMember;
			dataGridViewComboBoxCell.valueMember = this.valueMember;
			dataGridViewComboBoxCell.displayStyleForCurrentCellOnly = this.displayStyleForCurrentCellOnly;
			dataGridViewComboBoxCell.dropDownWidth = this.dropDownWidth;
			dataGridViewComboBoxCell.flatStyle = this.flatStyle;
			dataGridViewComboBoxCell.items.AddRangeInternal(this.items);
			dataGridViewComboBoxCell.maxDropDownItems = this.maxDropDownItems;
			dataGridViewComboBoxCell.sorted = this.sorted;
			return dataGridViewComboBoxCell;
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013AC RID: 5036 RVA: 0x0004B62C File Offset: 0x0004982C
		public override void DetachEditingControl()
		{
			base.DataGridView.EditingControlInternal = null;
		}

		/// <summary>Attaches and initializes the hosted editing control.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
		/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that determines the appearance of the hosted control.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013AD RID: 5037 RVA: 0x0004B63C File Offset: 0x0004983C
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			ComboBox comboBox = base.DataGridView.EditingControl as ComboBox;
			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox.Sorted = this.Sorted;
			comboBox.DataSource = null;
			comboBox.ValueMember = null;
			comboBox.DisplayMember = null;
			comboBox.Items.Clear();
			comboBox.SelectedIndex = -1;
			if (this.DataSource != null)
			{
				comboBox.DataSource = this.DataSource;
				comboBox.ValueMember = this.ValueMember;
				comboBox.DisplayMember = this.DisplayMember;
			}
			else
			{
				comboBox.Items.AddRange(this.Items);
				if (base.FormattedValue != null && comboBox.Items.IndexOf(base.FormattedValue) != -1)
				{
					comboBox.SelectedItem = base.FormattedValue;
				}
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004B710 File Offset: 0x00049910
		internal void SyncItems()
		{
			if (this.DataSource != null || this.OwningColumnTemplate == null)
			{
				return;
			}
			if (this.OwningColumnTemplate.DataGridView != null)
			{
				DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = this.OwningColumnTemplate.DataGridView.EditingControl as DataGridViewComboBoxEditingControl;
				if (dataGridViewComboBoxEditingControl != null)
				{
					object selectedItem = dataGridViewComboBoxEditingControl.SelectedItem;
					dataGridViewComboBoxEditingControl.Items.Clear();
					dataGridViewComboBoxEditingControl.Items.AddRange(this.items);
					if (dataGridViewComboBoxEditingControl.Items.IndexOf(selectedItem) != -1)
					{
						dataGridViewComboBoxEditingControl.SelectedItem = selectedItem;
					}
				}
			}
			this.OwningColumnTemplate.SyncItems(this.Items);
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <returns>true if edit mode should be started; otherwise, false. </returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013AF RID: 5039 RVA: 0x0004B7B0 File Offset: 0x000499B0
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return e.KeyCode == Keys.Space || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode == Keys.BrowserSearch || e.KeyCode == Keys.SelectMedia) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.ProcessKey) || (e.KeyCode == Keys.Attn || e.KeyCode == Keys.Packet) || (e.KeyCode >= Keys.Exsel && e.KeyCode <= Keys.OemClear) || e.KeyCode == Keys.F4 || (e.Modifiers == Keys.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up));
		}

		/// <returns>The cell value.</returns>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
		// Token: 0x060013B0 RID: 5040 RVA: 0x0004B8C8 File Offset: 0x00049AC8
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B1 RID: 5041 RVA: 0x0004B8D8 File Offset: 0x00049AD8
		public override string ToString()
		{
			return string.Format("DataGridViewComboBoxCell {{ ColumnIndex={0}, RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060013B2 RID: 5042 RVA: 0x0004B908 File Offset: 0x00049B08
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			object formattedValue = base.FormattedValue;
			Size size = Size.Empty;
			if (formattedValue != null)
			{
				size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			}
			return new Rectangle(1, (base.OwningRow.Height - size.Height) / 2, size.Width - 3, size.Height);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060013B3 RID: 5043 RVA: 0x0004B978 File Offset: 0x00049B78
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(12, 11);
			return new Rectangle(new Point(base.Size.Width - size.Width - 23, (base.Size.Height - size.Height) / 2), size);
		}

		/// <summary>Gets the formatted value of the cell's data. </summary>
		/// <returns>The value of the cell's data after formatting has been applied or null if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <param name="value">The value to be formatted. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" /> for type conversion errors or to type <see cref="T:System.ArgumentException" /> if <paramref name="value" /> cannot be found in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.Items" /> collection. </exception>
		// Token: 0x060013B4 RID: 5044 RVA: 0x0004B9F0 File Offset: 0x00049BF0
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x060013B5 RID: 5045 RVA: 0x0004BA04 File Offset: 0x00049C04
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object formattedValue = base.FormattedValue;
			if (formattedValue != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 22);
				size.Width += 25;
				return size;
			}
			return new Size(39, 22);
		}

		/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property is not null and the value of either the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DisplayMember" /> property or the <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.ValueMember" /> property is not null or <see cref="F:System.String.Empty" /> and does not name a valid property or column in the data source.</exception>
		// Token: 0x060013B6 RID: 5046 RVA: 0x0004BA64 File Offset: 0x00049C64
		protected override void OnDataGridViewChanged()
		{
			base.OnDataGridViewChanged();
		}

		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
		// Token: 0x060013B7 RID: 5047 RVA: 0x0004BA6C File Offset: 0x00049C6C
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
			base.OnEnter(rowIndex, throughMouseClick);
		}

		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
		// Token: 0x060013B8 RID: 5048 RVA: 0x0004BA78 File Offset: 0x00049C78
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			base.OnLeave(rowIndex, throughMouseClick);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060013B9 RID: 5049 RVA: 0x0004BA84 File Offset: 0x00049C84
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			base.OnMouseClick(e);
		}

		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060013BA RID: 5050 RVA: 0x0004BA90 File Offset: 0x00049C90
		protected override void OnMouseEnter(int rowIndex)
		{
			base.OnMouseEnter(rowIndex);
		}

		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x060013BB RID: 5051 RVA: 0x0004BA9C File Offset: 0x00049C9C
		protected override void OnMouseLeave(int rowIndex)
		{
			base.OnMouseLeave(rowIndex);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060013BC RID: 5052 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			base.OnMouseMove(e);
		}

		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="elementState"></param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x060013BD RID: 5053 RVA: 0x0004BAB4 File Offset: 0x00049CB4
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004BADC File Offset: 0x00049CDC
		internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			Color color = ((!this.Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			Rectangle contentBounds = base.ContentBounds;
			contentBounds.X += cellBounds.X;
			contentBounds.Y += cellBounds.Y;
			Rectangle rectangle = this.CalculateButtonArea(cellBounds);
			graphics.FillRectangle(SystemBrushes.Control, rectangle);
			ThemeEngine.Current.CPDrawComboButton(graphics, rectangle, ButtonState.Normal);
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, contentBounds, color, textFormatFlags);
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004BB80 File Offset: 0x00049D80
		private Rectangle CalculateButtonArea(Rectangle cellBounds)
		{
			int width = ThemeEngine.Current.Border3DSize.Width;
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = cellBounds;
			rectangle2.X = rectangle.Right - 16 - width;
			rectangle2.Y = rectangle.Y + width;
			rectangle2.Width = 16;
			rectangle2.Height = rectangle.Height - 2 * width;
			return rectangle2;
		}

		// Token: 0x04000B70 RID: 2928
		private bool autoComplete;

		// Token: 0x04000B71 RID: 2929
		private object dataSource;

		// Token: 0x04000B72 RID: 2930
		private string displayMember;

		// Token: 0x04000B73 RID: 2931
		private DataGridViewComboBoxDisplayStyle displayStyle;

		// Token: 0x04000B74 RID: 2932
		private bool displayStyleForCurrentCellOnly;

		// Token: 0x04000B75 RID: 2933
		private int dropDownWidth;

		// Token: 0x04000B76 RID: 2934
		private FlatStyle flatStyle;

		// Token: 0x04000B77 RID: 2935
		private DataGridViewComboBoxCell.ObjectCollection items;

		// Token: 0x04000B78 RID: 2936
		private int maxDropDownItems;

		// Token: 0x04000B79 RID: 2937
		private bool sorted;

		// Token: 0x04000B7A RID: 2938
		private string valueMember;

		// Token: 0x04000B7B RID: 2939
		private DataGridViewComboBoxColumn owningColumnTemlate;

		/// <summary>Represents the collection of selection choices in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
		// Token: 0x02000109 RID: 265
		[ListBindable(false)]
		public class ObjectCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that owns the collection.</param>
			// Token: 0x060013C0 RID: 5056 RVA: 0x0004BBE4 File Offset: 0x00049DE4
			public ObjectCollection(DataGridViewComboBoxCell owner)
			{
				this.owner = owner;
				this.list = new ArrayList();
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000478 RID: 1144
			// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0004BC00 File Offset: 0x00049E00
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000479 RID: 1145
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0004BC10 File Offset: 0x00049E10
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.list.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</returns>
			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0004BC20 File Offset: 0x00049E20
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destination" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="destination" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="destination" /> is multidimensional.</exception>
			// Token: 0x060013C4 RID: 5060 RVA: 0x0004BC30 File Offset: 0x00049E30
			void ICollection.CopyTo(Array destination, int index)
			{
				this.CopyTo((object[])destination, index);
			}

			/// <summary>Adds an object to the collection.</summary>
			/// <returns>The position in which to insert the new element.</returns>
			/// <param name="item">The object to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013C5 RID: 5061 RVA: 0x0004BC40 File Offset: 0x00049E40
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0004BC4C File Offset: 0x00049E4C
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0004BC5C File Offset: 0x00049E5C
			public bool IsReadOnly
			{
				get
				{
					return this.list.IsReadOnly;
				}
			}

			/// <summary>Gets or sets the item at the current index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> class.</summary>
			/// <returns>The <see cref="T:System.Object" /> stored at the given index.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one. </exception>
			/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
			/// <exception cref="T:System.ArgumentException">When setting this property, the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the cell is in a shared row.</exception>
			// Token: 0x1700047D RID: 1149
			public virtual object this[int index]
			{
				get
				{
					return this.list[index];
				}
				set
				{
					this.ThrowIfOwnerIsDataBound();
					this.list[index] = value;
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013CA RID: 5066 RVA: 0x0004BC94 File Offset: 0x00049E94
			public int Add(object item)
			{
				this.ThrowIfOwnerIsDataBound();
				int num = this.AddInternal(item);
				this.SyncOwnerItems();
				return num;
			}

			// Token: 0x060013CB RID: 5067 RVA: 0x0004BCB8 File Offset: 0x00049EB8
			internal int AddInternal(object item)
			{
				return this.list.Add(item);
			}

			// Token: 0x060013CC RID: 5068 RVA: 0x0004BCC8 File Offset: 0x00049EC8
			internal void AddRangeInternal(ICollection items)
			{
				this.list.AddRange(items);
			}

			/// <summary>Adds the items of an existing <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to the list of items in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> to load into this collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="value" /> collection is null.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013CD RID: 5069 RVA: 0x0004BCD8 File Offset: 0x00049ED8
			public void AddRange(DataGridViewComboBoxCell.ObjectCollection value)
			{
				this.ThrowIfOwnerIsDataBound();
				this.AddRangeInternal(value);
				this.SyncOwnerItems();
			}

			// Token: 0x060013CE RID: 5070 RVA: 0x0004BCF0 File Offset: 0x00049EF0
			private void SyncOwnerItems()
			{
				this.ThrowIfOwnerIsDataBound();
				if (this.owner != null)
				{
					this.owner.SyncItems();
				}
			}

			// Token: 0x060013CF RID: 5071 RVA: 0x0004BD10 File Offset: 0x00049F10
			public void ThrowIfOwnerIsDataBound()
			{
				if (this.owner != null && this.owner.DataGridView != null && this.owner.DataSource != null)
				{
					throw new ArgumentException("Cannot modify collection if the cell is data bound.");
				}
			}

			/// <summary>Adds one or more items to the list of items for a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
			/// <param name="items">One or more objects that represent items for the drop-down list.-or-An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is null.</exception>
			/// <exception cref="T:System.InvalidOperationException">One or more of the items in the <paramref name="items" /> array is null.</exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013D0 RID: 5072 RVA: 0x0004BD54 File Offset: 0x00049F54
			public void AddRange(params object[] items)
			{
				this.ThrowIfOwnerIsDataBound();
				this.AddRangeInternal(items);
				this.SyncOwnerItems();
			}

			/// <summary>Clears all items from the collection.</summary>
			/// <exception cref="T:System.ArgumentException">The collection contains at least one item and the cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The collection contains at least one item and the cell is in a shared row.</exception>
			// Token: 0x060013D1 RID: 5073 RVA: 0x0004BD6C File Offset: 0x00049F6C
			public void Clear()
			{
				this.ThrowIfOwnerIsDataBound();
				this.ClearInternal();
				this.SyncOwnerItems();
			}

			// Token: 0x060013D2 RID: 5074 RVA: 0x0004BD80 File Offset: 0x00049F80
			internal void ClearInternal()
			{
				this.list.Clear();
			}

			/// <summary>Determines whether the specified item is contained in the collection.</summary>
			/// <returns>true if the <paramref name="item" /> is in the collection; otherwise, false.</returns>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x060013D3 RID: 5075 RVA: 0x0004BD90 File Offset: 0x00049F90
			public bool Contains(object value)
			{
				return this.list.Contains(value);
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The destination array to which the contents will be copied.</param>
			/// <param name="arrayIndex">The index of the element in <paramref name="dest" /> at which to start copying.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destination" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0 or equal to or greater than the length of <paramref name="destination" />.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of <paramref name="destination" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="destination" /> is multidimensional.</exception>
			// Token: 0x060013D4 RID: 5076 RVA: 0x0004BDA0 File Offset: 0x00049FA0
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.list.CopyTo(destination, arrayIndex);
			}

			/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" />.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x060013D5 RID: 5077 RVA: 0x0004BDB0 File Offset: 0x00049FB0
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Returns the index of the specified item in the collection.</summary>
			/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x060013D6 RID: 5078 RVA: 0x0004BDC0 File Offset: 0x00049FC0
			public int IndexOf(object value)
			{
				return this.list.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index. </summary>
			/// <param name="index">The zero-based index at which to place <paramref name="item" /> within an unsorted <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</param>
			/// <param name="item">An object representing the item to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection. </exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013D7 RID: 5079 RVA: 0x0004BDD0 File Offset: 0x00049FD0
			public void Insert(int index, object item)
			{
				this.ThrowIfOwnerIsDataBound();
				this.InsertInternal(index, item);
				this.SyncOwnerItems();
			}

			// Token: 0x060013D8 RID: 5080 RVA: 0x0004BDE8 File Offset: 0x00049FE8
			internal void InsertInternal(int index, object item)
			{
				this.list.Insert(index, item);
			}

			/// <summary>Removes the specified object from the collection.</summary>
			/// <param name="value">An object representing the item to remove from the collection.</param>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013D9 RID: 5081 RVA: 0x0004BDF8 File Offset: 0x00049FF8
			public void Remove(object value)
			{
				this.ThrowIfOwnerIsDataBound();
				this.RemoveInternal(value);
				this.SyncOwnerItems();
			}

			// Token: 0x060013DA RID: 5082 RVA: 0x0004BE10 File Offset: 0x0004A010
			internal void RemoveInternal(object value)
			{
				this.list.Remove(value);
			}

			/// <summary>Removes the object at the specified index.</summary>
			/// <param name="index">The zero-based index of the object to be removed.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than the number of items in the collection minus one. </exception>
			/// <exception cref="T:System.ArgumentException">The cell's <see cref="P:System.Windows.Forms.DataGridViewComboBoxCell.DataSource" /> property value is not null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The cell is in a shared row.</exception>
			// Token: 0x060013DB RID: 5083 RVA: 0x0004BE20 File Offset: 0x0004A020
			public void RemoveAt(int index)
			{
				this.ThrowIfOwnerIsDataBound();
				this.RemoveAtInternal(index);
				this.SyncOwnerItems();
			}

			// Token: 0x060013DC RID: 5084 RVA: 0x0004BE38 File Offset: 0x0004A038
			internal void RemoveAtInternal(int index)
			{
				this.list.RemoveAt(index);
			}

			// Token: 0x04000B7C RID: 2940
			private ArrayList list;

			// Token: 0x04000B7D RID: 2941
			private DataGridViewComboBoxCell owner;
		}
	}
}
