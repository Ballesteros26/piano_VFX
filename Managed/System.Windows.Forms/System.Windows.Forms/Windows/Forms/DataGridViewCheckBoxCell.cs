using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a check box user interface (UI) to use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F8 RID: 248
	public class DataGridViewCheckBoxCell : DataGridViewCell, IDataGridViewEditingCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class to its default state.</summary>
		// Token: 0x060012B6 RID: 4790 RVA: 0x00048F50 File Offset: 0x00047150
		public DataGridViewCheckBoxCell()
		{
			this.check_state = PushButtonState.Normal;
			this.editingCellFormattedValue = false;
			this.editingCellValueChanged = false;
			this.falseValue = null;
			this.flatStyle = FlatStyle.Standard;
			this.indeterminateValue = null;
			this.threeState = false;
			this.trueValue = null;
			this.ValueType = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class, enabling binary or ternary state.</summary>
		/// <param name="threeState">true to enable ternary state; false to enable binary state.</param>
		// Token: 0x060012B7 RID: 4791 RVA: 0x00048FA8 File Offset: 0x000471A8
		public DataGridViewCheckBoxCell(bool threeState)
			: this()
		{
			this.threeState = threeState;
			this.editingCellFormattedValue = CheckState.Unchecked;
		}

		/// <summary>Gets or sets the formatted value of the control hosted by the cell when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.-or-The assigned value is null or is not of the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property.-or- The assigned value is not of type <see cref="T:System.Boolean" /> nor of type <see cref="T:System.Windows.Forms.CheckState" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.</exception>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00048FC4 File Offset: 0x000471C4
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x00048FCC File Offset: 0x000471CC
		public virtual object EditingCellFormattedValue
		{
			get
			{
				return this.editingCellFormattedValue;
			}
			set
			{
				if (this.FormattedValueType == null || value == null || value.GetType() != this.FormattedValueType || !(value is bool) || !(value is CheckState))
				{
					throw new ArgumentException("Cannot set this property.");
				}
				this.editingCellFormattedValue = value;
			}
		}

		/// <summary>Gets or sets a flag indicating that the value has been changed for this cell.</summary>
		/// <returns>true if the cell's value has changed; otherwise, false.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00049024 File Offset: 0x00047224
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x0004902C File Offset: 0x0004722C
		public virtual bool EditingCellValueChanged
		{
			get
			{
				return this.editingCellValueChanged;
			}
			set
			{
				this.editingCellValueChanged = value;
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00049038 File Offset: 0x00047238
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of false.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of false. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004903C File Offset: 0x0004723C
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x00049044 File Offset: 0x00047244
		[DefaultValue(null)]
		public object FalseValue
		{
			get
			{
				return this.falseValue;
			}
			set
			{
				this.falseValue = value;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the check box user interface (UI).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00049050 File Offset: 0x00047250
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x00049058 File Offset: 0x00047258
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
				if (value == FlatStyle.Popup)
				{
					throw new Exception("FlatStyle cannot be set to Popup in this control.");
				}
			}
		}

		/// <summary>Gets the type of the cell display value. </summary>
		/// <returns>A <see cref="T:System.Type" /> representing the display type of the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00049094 File Offset: 0x00047294
		public override Type FormattedValueType
		{
			get
			{
				if (this.ThreeState)
				{
					return typeof(CheckState);
				}
				return typeof(bool);
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to an indeterminate or null cell value. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000490C4 File Offset: 0x000472C4
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x000490CC File Offset: 0x000472CC
		[DefaultValue(null)]
		public object IndeterminateValue
		{
			get
			{
				return this.indeterminateValue;
			}
			set
			{
				this.indeterminateValue = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether ternary mode has been enabled for the hosted check box control.</summary>
		/// <returns>true if ternary mode is enabled; false if binary mode is enabled. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x000490D8 File Offset: 0x000472D8
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x000490E0 File Offset: 0x000472E0
		[DefaultValue(false)]
		public bool ThreeState
		{
			get
			{
				return this.threeState;
			}
			set
			{
				this.threeState = value;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of true.</summary>
		/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of true. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000490EC File Offset: 0x000472EC
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x000490F4 File Offset: 0x000472F4
		[DefaultValue(null)]
		public object TrueValue
		{
			get
			{
				return this.trueValue;
			}
			set
			{
				this.trueValue = value;
			}
		}

		/// <summary>Gets the data type of the values in the cell.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying value of the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00049100 File Offset: 0x00047300
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00049168 File Offset: 0x00047368
		public override Type ValueType
		{
			get
			{
				if (base.ValueType != null)
				{
					return base.ValueType;
				}
				if (base.OwningColumn != null && base.OwningColumn.ValueType != null)
				{
					return base.OwningColumn.ValueType;
				}
				if (this.ThreeState)
				{
					return typeof(CheckState);
				}
				return typeof(bool);
			}
			set
			{
				base.ValueType = value;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012CA RID: 4810 RVA: 0x00049174 File Offset: 0x00047374
		public override object Clone()
		{
			DataGridViewCheckBoxCell dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)base.Clone();
			dataGridViewCheckBoxCell.editingCellValueChanged = this.editingCellValueChanged;
			dataGridViewCheckBoxCell.editingCellFormattedValue = this.editingCellFormattedValue;
			dataGridViewCheckBoxCell.falseValue = this.falseValue;
			dataGridViewCheckBoxCell.flatStyle = this.flatStyle;
			dataGridViewCheckBoxCell.indeterminateValue = this.indeterminateValue;
			dataGridViewCheckBoxCell.threeState = this.threeState;
			dataGridViewCheckBoxCell.trueValue = this.trueValue;
			dataGridViewCheckBoxCell.ValueType = this.ValueType;
			return dataGridViewCheckBoxCell;
		}

		/// <summary>Gets the formatted value of the cell while it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the formatted value of the editing cell. </returns>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that describes the context in which any formatting error occurs. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.</exception>
		// Token: 0x060012CB RID: 4811 RVA: 0x000491F0 File Offset: 0x000473F0
		public virtual object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
		{
			if (this.FormattedValueType == null)
			{
				throw new InvalidOperationException("FormattedValueType is null.");
			}
			if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
			{
				return Convert.ToString(base.Value);
			}
			if (this.editingCellFormattedValue != null)
			{
				return this.editingCellFormattedValue;
			}
			if (this.threeState)
			{
				return CheckState.Indeterminate;
			}
			return false;
		}

		/// <summary>Converts a value formatted for display to an actual cell value.</summary>
		/// <returns>The cell value.</returns>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cellStyle" /> is null.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="formattedValue" /> is null.- or -The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property. </exception>
		// Token: 0x060012CC RID: 4812 RVA: 0x00049258 File Offset: 0x00047458
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("CellStyle is null");
			}
			if (this.FormattedValueType == null)
			{
				throw new FormatException("FormattedValueType is null.");
			}
			if (formattedValue == null || formattedValue.GetType() != this.FormattedValueType)
			{
				throw new ArgumentException("FormattedValue is null or is not instance of FormattedValueType.");
			}
			return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		/// <summary>This method is not meaningful for this type.</summary>
		/// <param name="selectAll">This parameter is ignored.</param>
		// Token: 0x060012CD RID: 4813 RVA: 0x000492BC File Offset: 0x000474BC
		public virtual void PrepareEditingCellForEdit(bool selectAll)
		{
			this.editingCellFormattedValue = this.GetCurrentValue();
		}

		/// <summary>Returns the string representation of the cell.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012CE RID: 4814 RVA: 0x000492D0 File Offset: 0x000474D0
		public override string ToString()
		{
			return string.Format("DataGridViewCheckBoxCell {{ ColumnIndex={0}, RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is clicked.</summary>
		/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x060012CF RID: 4815 RVA: 0x00049300 File Offset: 0x00047500
		protected override bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return base.IsInEditMode;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is double-clicked.</summary>
		/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the double-click.</param>
		// Token: 0x060012D0 RID: 4816 RVA: 0x00049308 File Offset: 0x00047508
		protected override bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return base.IsInEditMode;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </returns>
		// Token: 0x060012D1 RID: 4817 RVA: 0x00049310 File Offset: 0x00047510
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject(this);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060012D2 RID: 4818 RVA: 0x00049318 File Offset: 0x00047518
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			return new Rectangle((base.Size.Width - 13) / 2, (base.Size.Height - 13) / 2, 13, 13);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060012D3 RID: 4819 RVA: 0x00049368 File Offset: 0x00047568
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

		/// <summary>Gets the formatted value of the cell's data. </summary>
		/// <returns>The value of the cell's data after formatting has been applied or null if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <param name="value">The value to be formatted. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		// Token: 0x060012D4 RID: 4820 RVA: 0x000493DC File Offset: 0x000475DC
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView != null && value != null)
			{
				return value;
			}
			if (this.threeState)
			{
				return CheckState.Indeterminate;
			}
			return false;
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x060012D5 RID: 4821 RVA: 0x0004940C File Offset: 0x0004760C
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			return new Size(21, 20);
		}

		/// <summary>Indicates whether the row containing the cell is unshared when a key is pressed while the cell has focus.</summary>
		/// <returns>true if the SPACE key is pressed and the CTRL, ALT, and SHIFT keys are all not pressed; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press. </param>
		/// <param name="rowIndex">The index of the row containing the cell. </param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x00049418 File Offset: 0x00047618
		protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyData == Keys.Space;
		}

		/// <summary>Indicates whether the row containing the cell is unshared when a key is released while the cell has focus.</summary>
		/// <returns>true if the SPACE key is released; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press. </param>
		/// <param name="rowIndex">The index of the row containing the cell. </param>
		// Token: 0x060012D7 RID: 4823 RVA: 0x00049424 File Offset: 0x00047624
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyData == Keys.Space;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
		/// <returns>Always true.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x060012D8 RID: 4824 RVA: 0x00049430 File Offset: 0x00047630
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
		/// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x060012D9 RID: 4825 RVA: 0x00049440 File Offset: 0x00047640
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return false;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
		/// <returns>true if the button is not in the normal state; false if the button is in the pressed state.</returns>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x060012DA RID: 4826 RVA: 0x00049444 File Offset: 0x00047644
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.check_state == PushButtonState.Pressed;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell.</summary>
		/// <returns>Always true.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x060012DB RID: 4827 RVA: 0x00049450 File Offset: 0x00047650
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060012DC RID: 4828 RVA: 0x00049460 File Offset: 0x00047660
		protected override void OnContentClick(DataGridViewCellEventArgs e)
		{
			if (this.ReadOnly)
			{
				return;
			}
			if (!base.IsInEditMode)
			{
				base.DataGridView.BeginEdit(false);
			}
			CheckState currentValue = this.GetCurrentValue();
			if (this.threeState)
			{
				if (currentValue == CheckState.Indeterminate)
				{
					this.editingCellFormattedValue = CheckState.Unchecked;
				}
				else if (currentValue == CheckState.Checked)
				{
					this.editingCellFormattedValue = CheckState.Indeterminate;
				}
				else
				{
					this.editingCellFormattedValue = CheckState.Checked;
				}
			}
			else if (currentValue == CheckState.Checked)
			{
				this.editingCellFormattedValue = false;
			}
			else
			{
				this.editingCellFormattedValue = true;
			}
			this.editingCellValueChanged = true;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
		// Token: 0x060012DD RID: 4829 RVA: 0x00049510 File Offset: 0x00047710
		protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x060012DE RID: 4830 RVA: 0x00049514 File Offset: 0x00047714
		protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
			if (!this.ReadOnly && (e.KeyData & Keys.Space) == Keys.Space)
			{
				this.check_state = PushButtonState.Pressed;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when a character key is released while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		// Token: 0x060012DF RID: 4831 RVA: 0x00049550 File Offset: 0x00047750
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if (!this.ReadOnly && (e.KeyData & Keys.Space) == Keys.Space)
			{
				this.check_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the focus moves from a cell.</summary>
		/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
		/// <param name="throughMouseClick">true if the cell was left as a result of user mouse click rather than a programmatic cell change; otherwise, false.</param>
		// Token: 0x060012E0 RID: 4832 RVA: 0x0004958C File Offset: 0x0004778C
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (!this.ReadOnly && this.check_state != PushButtonState.Normal)
			{
				this.check_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060012E1 RID: 4833 RVA: 0x000495C4 File Offset: 0x000477C4
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (!this.ReadOnly && (e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.check_state = PushButtonState.Pressed;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse pointer moves from a cell.</summary>
		/// <param name="rowIndex">The row index of the current cell or -1 if the cell is not owned by a row.</param>
		// Token: 0x060012E2 RID: 4834 RVA: 0x00049608 File Offset: 0x00047808
		protected override void OnMouseLeave(int rowIndex)
		{
			if (!this.ReadOnly && this.check_state != PushButtonState.Normal)
			{
				this.check_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060012E3 RID: 4835 RVA: 0x00049640 File Offset: 0x00047840
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (!this.ReadOnly && this.check_state != PushButtonState.Normal && this.check_state != PushButtonState.Hot)
			{
				this.check_state = PushButtonState.Hot;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <summary>Called when the mouse button is released while the pointer is on a cell. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060012E4 RID: 4836 RVA: 0x00049684 File Offset: 0x00047884
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (!this.ReadOnly && (e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.check_state = PushButtonState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
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
		// Token: 0x060012E5 RID: 4837 RVA: 0x000496C8 File Offset: 0x000478C8
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000496F0 File Offset: 0x000478F0
		internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			CheckState currentValue = this.GetCurrentValue();
			CheckBoxState checkBoxState;
			if (currentValue == CheckState.Unchecked)
			{
				checkBoxState = (CheckBoxState)this.check_state;
			}
			else if (currentValue == CheckState.Checked)
			{
				checkBoxState = (CheckBoxState)(this.check_state + 4);
			}
			else if (this.threeState)
			{
				checkBoxState = (CheckBoxState)(this.check_state + 8);
			}
			else
			{
				checkBoxState = (CheckBoxState)this.check_state;
			}
			Point point;
			point..ctor(cellBounds.X + (base.Size.Width - 13) / 2, cellBounds.Y + (base.Size.Height - 13) / 2);
			CheckBoxRenderer.DrawCheckBox(graphics, point, checkBoxState);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00049794 File Offset: 0x00047994
		private CheckState GetCurrentValue()
		{
			CheckState checkState = CheckState.Indeterminate;
			object value;
			if (this.editingCellValueChanged)
			{
				value = this.editingCellFormattedValue;
			}
			else
			{
				value = base.Value;
			}
			if (value == null)
			{
				checkState = CheckState.Indeterminate;
			}
			else if (value is bool)
			{
				if ((bool)value)
				{
					checkState = CheckState.Checked;
				}
				else if (!(bool)value)
				{
					checkState = CheckState.Unchecked;
				}
			}
			else if (value is CheckState)
			{
				checkState = (CheckState)((int)value);
			}
			return checkState;
		}

		// Token: 0x04000B37 RID: 2871
		private object editingCellFormattedValue;

		// Token: 0x04000B38 RID: 2872
		private bool editingCellValueChanged;

		// Token: 0x04000B39 RID: 2873
		private object falseValue;

		// Token: 0x04000B3A RID: 2874
		private FlatStyle flatStyle;

		// Token: 0x04000B3B RID: 2875
		private object indeterminateValue;

		// Token: 0x04000B3C RID: 2876
		private bool threeState;

		// Token: 0x04000B3D RID: 2877
		private object trueValue;

		// Token: 0x04000B3E RID: 2878
		private PushButtonState check_state;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> to accessibility client applications.</summary>
		// Token: 0x020000F9 RID: 249
		protected class DataGridViewCheckBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</param>
			// Token: 0x060012E8 RID: 4840 RVA: 0x00049810 File Offset: 0x00047A10
			public DataGridViewCheckBoxCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <returns>A description of the default action.</returns>
			// Token: 0x17000431 RID: 1073
			// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004981C File Offset: 0x00047A1C
			public override string DefaultAction
			{
				get
				{
					if (base.Owner.ReadOnly)
					{
						return string.Empty;
					}
					throw new NotImplementedException();
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a DataGridView control.-or-The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
			// Token: 0x060012EA RID: 4842 RVA: 0x0004983C File Offset: 0x00047A3C
			public override void DoDefaultAction()
			{
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
			/// <returns>The value –1.</returns>
			// Token: 0x060012EB RID: 4843 RVA: 0x00049840 File Offset: 0x00047A40
			public override int GetChildCount()
			{
				return -1;
			}
		}
	}
}
