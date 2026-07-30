using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Displays a graphic in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000117 RID: 279
	public class DataGridViewImageCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
		/// <param name="valueIsIcon">The cell will display an <see cref="T:System.Drawing.Icon" /> value.</param>
		// Token: 0x0600143A RID: 5178 RVA: 0x0004C880 File Offset: 0x0004AA80
		public DataGridViewImageCell(bool valueIsIcon)
		{
			this.valueIsIcon = valueIsIcon;
			this.imageLayout = DataGridViewImageCellLayout.NotSet;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, configuring it for use with cell values other than <see cref="T:System.Drawing.Icon" /> objects.</summary>
		// Token: 0x0600143B RID: 5179 RVA: 0x0004C898 File Offset: 0x0004AA98
		public DataGridViewImageCell()
			: this(false)
		{
		}

		/// <summary>Gets the default value that is used when creating a new row.</summary>
		/// <returns>An object containing a default image placeholder, or null to display an empty cell.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004C8B8 File Offset: 0x0004AAB8
		public override object DefaultNewRowValue
		{
			get
			{
				return DataGridViewImageCell.missing_image;
			}
		}

		/// <summary>Gets or sets the text associated with the image.</summary>
		/// <returns>The text associated with the image displayed in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0004C8C8 File Offset: 0x0004AAC8
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. As implemented in this class, this property is always null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004C8D4 File Offset: 0x0004AAD4
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing display value type of the cell, which is the <see cref="T:System.Drawing.Image" /> type if the <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to false or the <see cref="T:System.Drawing.Icon" /> type otherwise.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0004C8D8 File Offset: 0x0004AAD8
		public override Type FormattedValueType
		{
			get
			{
				return (!this.valueIsIcon) ? typeof(Image) : typeof(Icon);
			}
		}

		/// <summary>Gets or sets the graphics layout for the cell. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> for this cell. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The supplied <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> value is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004C90C File Offset: 0x0004AB0C
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0004C914 File Offset: 0x0004AB14
		[DefaultValue(DataGridViewImageCellLayout.NotSet)]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				return this.imageLayout;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewImageCellLayout), value))
				{
					throw new InvalidEnumArgumentException("Value is invalid image cell layout.");
				}
				this.imageLayout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether this cell displays an <see cref="T:System.Drawing.Icon" /> value.</summary>
		/// <returns>true if this cell displays an <see cref="T:System.Drawing.Icon" /> value; otherwise, false.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0004C950 File Offset: 0x0004AB50
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0004C958 File Offset: 0x0004AB58
		[DefaultValue(false)]
		public bool ValueIsIcon
		{
			get
			{
				return this.valueIsIcon;
			}
			set
			{
				this.valueIsIcon = value;
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the cell's value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004C964 File Offset: 0x0004AB64
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0004C9CC File Offset: 0x0004ABCC
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
				if (this.valueIsIcon)
				{
					return typeof(Icon);
				}
				return typeof(Image);
			}
			set
			{
				base.ValueType = value;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001448 RID: 5192 RVA: 0x0004C9D8 File Offset: 0x0004ABD8
		public override object Clone()
		{
			DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell)base.Clone();
			dataGridViewImageCell.defaultNewRowValue = this.defaultNewRowValue;
			dataGridViewImageCell.description = this.description;
			dataGridViewImageCell.valueIsIcon = this.valueIsIcon;
			return dataGridViewImageCell;
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001449 RID: 5193 RVA: 0x0004CA18 File Offset: 0x0004AC18
		public override string ToString()
		{
			return base.GetType().Name;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </returns>
		// Token: 0x0600144A RID: 5194 RVA: 0x0004CA28 File Offset: 0x0004AC28
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewImageCell.DataGridViewImageCellAccessibleObject(this);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600144B RID: 5195 RVA: 0x0004CA30 File Offset: 0x0004AC30
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			Rectangle empty = Rectangle.Empty;
			Image image = (Image)this.GetFormattedValue(base.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.PreferredSize);
			if (image == null)
			{
				image = DataGridViewImageCell.missing_image;
			}
			switch (this.imageLayout)
			{
			case DataGridViewImageCellLayout.NotSet:
			case DataGridViewImageCellLayout.Normal:
				empty..ctor((base.Size.Width - image.Width) / 2, (base.Size.Height - image.Height) / 2, image.Width, image.Height);
				break;
			case DataGridViewImageCellLayout.Stretch:
				empty..ctor(Point.Empty, base.Size);
				break;
			case DataGridViewImageCellLayout.Zoom:
			{
				Size size;
				if ((float)image.Width / (float)image.Height >= (float)base.Size.Width / (float)base.Size.Height)
				{
					size..ctor(base.Size.Width, image.Height * base.Size.Width / image.Width);
				}
				else
				{
					size..ctor(image.Width * base.Size.Height / image.Height, base.Size.Height);
				}
				empty..ctor((base.Size.Width - size.Width) / 2, (base.Size.Height - size.Height) / 2, size.Width, size.Height);
				break;
			}
			}
			return empty;
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600144C RID: 5196 RVA: 0x0004CBE8 File Offset: 0x0004ADE8
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

		/// <summary>Returns a graphic as it would be displayed in the cell.</summary>
		/// <returns>An object that represents the formatted image.</returns>
		/// <param name="value">The value to be formatted. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell. </param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed. </param>
		// Token: 0x0600144D RID: 5197 RVA: 0x0004CC5C File Offset: 0x0004AE5C
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x0600144E RID: 5198 RVA: 0x0004CC70 File Offset: 0x0004AE70
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			Image image = (Image)base.FormattedValue;
			if (image == null)
			{
				return new Size(21, 20);
			}
			if (image != null)
			{
				return new Size(image.Width + 1, image.Height + 1);
			}
			return new Size(21, 20);
		}

		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600144F RID: 5199 RVA: 0x0004CCC0 File Offset: 0x0004AEC0
		protected override object GetValue(int rowIndex)
		{
			return base.GetValue(rowIndex);
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
		// Token: 0x06001450 RID: 5200 RVA: 0x0004CCCC File Offset: 0x0004AECC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004CCF4 File Offset: 0x0004AEF4
		internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			Image image;
			if (formattedValue == null)
			{
				image = DataGridViewImageCell.missing_image;
			}
			else
			{
				image = (Image)formattedValue;
			}
			Rectangle rectangle = Rectangle.Empty;
			switch (this.imageLayout)
			{
			case DataGridViewImageCellLayout.NotSet:
			case DataGridViewImageCellLayout.Normal:
				rectangle = base.AlignInRectangle(new Rectangle(2, 2, cellBounds.Width - 4, cellBounds.Height - 4), image.Size, cellStyle.Alignment);
				break;
			case DataGridViewImageCellLayout.Stretch:
				rectangle..ctor(Point.Empty, cellBounds.Size);
				break;
			case DataGridViewImageCellLayout.Zoom:
			{
				Size size;
				if ((float)image.Width / (float)image.Height >= (float)base.Size.Width / (float)base.Size.Height)
				{
					size..ctor(base.Size.Width, image.Height * base.Size.Width / image.Width);
				}
				else
				{
					size..ctor(image.Width * base.Size.Height / image.Height, base.Size.Height);
				}
				rectangle..ctor((base.Size.Width - size.Width) / 2, (base.Size.Height - size.Height) / 2, size.Width, size.Height);
				break;
			}
			}
			rectangle.X += cellBounds.Left;
			rectangle.Y += cellBounds.Top;
			graphics.DrawImage(image, rectangle);
		}

		// Token: 0x04000BC6 RID: 3014
		private object defaultNewRowValue;

		// Token: 0x04000BC7 RID: 3015
		private string description;

		// Token: 0x04000BC8 RID: 3016
		private DataGridViewImageCellLayout imageLayout;

		// Token: 0x04000BC9 RID: 3017
		private bool valueIsIcon;

		// Token: 0x04000BCA RID: 3018
		private static Image missing_image = ResourceImageLoader.Get("image-missing.png");

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> to accessibility client applications.</summary>
		// Token: 0x02000118 RID: 280
		protected class DataGridViewImageCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</param>
			// Token: 0x06001452 RID: 5202 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
			public DataGridViewImageCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</summary>
			/// <returns>An empty string ("").</returns>
			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x06001453 RID: 5203 RVA: 0x0004CEBC File Offset: 0x0004B0BC
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			/// <summary>Gets the text associated with the image in the image cell.</summary>
			/// <returns>The text associated with the image in the image cell.</returns>
			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x06001454 RID: 5204 RVA: 0x0004CEC4 File Offset: 0x0004B0C4
			public override string Description
			{
				get
				{
					return (base.Owner as DataGridViewImageCell).Description;
				}
			}

			/// <summary>Gets a string representing the formatted value of the owning cell. </summary>
			/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06001455 RID: 5205 RVA: 0x0004CED8 File Offset: 0x0004B0D8
			// (set) Token: 0x06001456 RID: 5206 RVA: 0x0004CEE0 File Offset: 0x0004B0E0
			public override string Value
			{
				get
				{
					return base.Value;
				}
				set
				{
					base.Value = value;
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
			// Token: 0x06001457 RID: 5207 RVA: 0x0004CEEC File Offset: 0x0004B0EC
			public override void DoDefaultAction()
			{
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
			/// <returns>The value –1.</returns>
			// Token: 0x06001458 RID: 5208 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
			public override int GetChildCount()
			{
				return -1;
			}
		}
	}
}
