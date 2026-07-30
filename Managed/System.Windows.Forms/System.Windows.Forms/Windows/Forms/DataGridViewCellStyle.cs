using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents the formatting and style information applied to individual cells within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F1 RID: 241
	[Editor("System.Windows.Forms.Design.DataGridViewCellStyleEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(DataGridViewCellStyleConverter))]
	public class DataGridViewCellStyle : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using default property values.</summary>
		// Token: 0x0600127B RID: 4731 RVA: 0x00048674 File Offset: 0x00046874
		public DataGridViewCellStyle()
		{
			this.alignment = DataGridViewContentAlignment.NotSet;
			this.backColor = Color.Empty;
			this.dataSourceNullValue = DBNull.Value;
			this.font = null;
			this.foreColor = Color.Empty;
			this.format = string.Empty;
			this.nullValue = string.Empty;
			this.padding = Padding.Empty;
			this.selectionBackColor = Color.Empty;
			this.selectionForeColor = Color.Empty;
			this.tag = null;
			this.wrapMode = DataGridViewTriState.NotSet;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using the property values of the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used as a template to provide initial property values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCellStyle" /> is null.</exception>
		// Token: 0x0600127C RID: 4732 RVA: 0x000486FC File Offset: 0x000468FC
		public DataGridViewCellStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.alignment = dataGridViewCellStyle.alignment;
			this.backColor = dataGridViewCellStyle.backColor;
			this.dataSourceNullValue = dataGridViewCellStyle.dataSourceNullValue;
			this.font = dataGridViewCellStyle.font;
			this.foreColor = dataGridViewCellStyle.foreColor;
			this.format = dataGridViewCellStyle.format;
			this.formatProvider = dataGridViewCellStyle.formatProvider;
			this.nullValue = dataGridViewCellStyle.nullValue;
			this.padding = dataGridViewCellStyle.padding;
			this.selectionBackColor = dataGridViewCellStyle.selectionBackColor;
			this.selectionForeColor = dataGridViewCellStyle.selectionForeColor;
			this.tag = dataGridViewCellStyle.tag;
			this.wrapMode = dataGridViewCellStyle.wrapMode;
		}

		// Token: 0x1400016A RID: 362
		// (add) Token: 0x0600127D RID: 4733 RVA: 0x000487AC File Offset: 0x000469AC
		// (remove) Token: 0x0600127E RID: 4734 RVA: 0x000487C8 File Offset: 0x000469C8
		internal event EventHandler StyleChanged;

		/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
		// Token: 0x0600127F RID: 4735 RVA: 0x000487E4 File Offset: 0x000469E4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Gets or sets a value indicating the position of the cell content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewContentAlignment.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000487EC File Offset: 0x000469EC
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x000487F4 File Offset: 0x000469F4
		[DefaultValue(DataGridViewContentAlignment.NotSet)]
		public DataGridViewContentAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewContentAlignment), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewContentAlignment.");
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the background color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00048840 File Offset: 0x00046A40
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00048848 File Offset: 0x00046A48
		public Color BackColor
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
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the value saved to the data source when the user enters a null value into a cell.</summary>
		/// <returns>The value saved to the data source when the user specifies a null cell value. The default is <see cref="F:System.DBNull.Value" />.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00048868 File Offset: 0x00046A68
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00048870 File Offset: 0x00046A70
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public object DataSourceNullValue
		{
			get
			{
				return this.dataSourceNullValue;
			}
			set
			{
				if (this.dataSourceNullValue != value)
				{
					this.dataSourceNullValue = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the font applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> applied to the cell text. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004888C File Offset: 0x00046A8C
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00048894 File Offset: 0x00046A94
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				if (this.font != value)
				{
					this.font = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x000488B0 File Offset: 0x00046AB0
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x000488B8 File Offset: 0x00046AB8
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				if (this.foreColor != value)
				{
					this.foreColor = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the format string applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A string that indicates the format of the cell value. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x000488D8 File Offset: 0x00046AD8
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x000488E0 File Offset: 0x00046AE0
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[EditorBrowsable(2)]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (this.format != value)
				{
					this.format = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the object used to provide culture-specific formatting of <see cref="T:System.Windows.Forms.DataGridView" /> cell values.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> used for cell formatting. The default is <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00048900 File Offset: 0x00046B00
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x0004891C File Offset: 0x00046B1C
		[EditorBrowsable(2)]
		[Browsable(false)]
		public IFormatProvider FormatProvider
		{
			get
			{
				if (this.formatProvider == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.formatProvider;
			}
			set
			{
				if (this.formatProvider != value)
				{
					this.formatProvider = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property has been set.</summary>
		/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property is the default value; otherwise, false.</returns>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00048938 File Offset: 0x00046B38
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool IsDataSourceNullValueDefault
		{
			get
			{
				return this.dataSourceNullValue != null;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property has been set.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property is the default value; otherwise, false.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00048948 File Offset: 0x00046B48
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool IsFormatProviderDefault
		{
			get
			{
				return this.formatProvider == null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property has been set.</summary>
		/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property is the default value; otherwise, false.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00048954 File Offset: 0x00046B54
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsNullValueDefault
		{
			get
			{
				return this.nullValue is string && (string)this.nullValue == string.Empty;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> cell display value corresponding to a cell value of <see cref="F:System.DBNull.Value" /> or null.</summary>
		/// <returns>The object used to indicate a null value in a cell. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00048980 File Offset: 0x00046B80
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x00048988 File Offset: 0x00046B88
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue("")]
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (this.nullValue != value)
				{
					this.nullValue = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x000489A4 File Offset: 0x00046BA4
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x000489AC File Offset: 0x00046BAC
		public Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (this.padding != value)
				{
					this.padding = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the background color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000489CC File Offset: 0x00046BCC
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x000489D4 File Offset: 0x00046BD4
		public Color SelectionBackColor
		{
			get
			{
				return this.selectionBackColor;
			}
			set
			{
				if (this.selectionBackColor != value)
				{
					this.selectionBackColor = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets the foreground color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x000489F4 File Offset: 0x00046BF4
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x000489FC File Offset: 0x00046BFC
		public Color SelectionForeColor
		{
			get
			{
				return this.selectionForeColor;
			}
			set
			{
				if (this.selectionForeColor != value)
				{
					this.selectionForeColor = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets an object that contains additional data related to the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>An object that contains additional data. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00048A1C File Offset: 0x00046C1C
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00048A24 File Offset: 0x00046C24
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				if (this.tag != value)
				{
					this.tag = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether textual content in a <see cref="T:System.Windows.Forms.DataGridView" /> cell is wrapped to subsequent lines or truncated when it is too long to fit on a single line.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00048A40 File Offset: 0x00046C40
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00048A48 File Offset: 0x00046C48
		[DefaultValue(DataGridViewTriState.NotSet)]
		public DataGridViewTriState WrapMode
		{
			get
			{
				return this.wrapMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DataGridViewTriState), value))
				{
					throw new InvalidEnumArgumentException("Value is not valid DataGridViewTriState.");
				}
				if (this.wrapMode != value)
				{
					this.wrapMode = value;
					this.OnStyleChanged();
				}
			}
		}

		/// <summary>Applies the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to apply to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCellStyle" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600129D RID: 4765 RVA: 0x00048A94 File Offset: 0x00046C94
		public virtual void ApplyStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (dataGridViewCellStyle.alignment != DataGridViewContentAlignment.NotSet)
			{
				this.alignment = dataGridViewCellStyle.alignment;
			}
			if (dataGridViewCellStyle.backColor != Color.Empty)
			{
				this.backColor = dataGridViewCellStyle.backColor;
			}
			if (dataGridViewCellStyle.dataSourceNullValue != DBNull.Value)
			{
				this.dataSourceNullValue = dataGridViewCellStyle.dataSourceNullValue;
			}
			if (dataGridViewCellStyle.font != null)
			{
				this.font = dataGridViewCellStyle.font;
			}
			if (dataGridViewCellStyle.foreColor != Color.Empty)
			{
				this.foreColor = dataGridViewCellStyle.foreColor;
			}
			if (dataGridViewCellStyle.format != string.Empty)
			{
				this.format = dataGridViewCellStyle.format;
			}
			if (dataGridViewCellStyle.formatProvider != null)
			{
				this.formatProvider = dataGridViewCellStyle.formatProvider;
			}
			if (dataGridViewCellStyle.nullValue != null)
			{
				this.nullValue = dataGridViewCellStyle.nullValue;
			}
			if (dataGridViewCellStyle.padding != Padding.Empty)
			{
				this.padding = dataGridViewCellStyle.padding;
			}
			if (dataGridViewCellStyle.selectionBackColor != Color.Empty)
			{
				this.selectionBackColor = dataGridViewCellStyle.selectionBackColor;
			}
			if (dataGridViewCellStyle.selectionForeColor != Color.Empty)
			{
				this.selectionForeColor = dataGridViewCellStyle.selectionForeColor;
			}
			if (dataGridViewCellStyle.tag != null)
			{
				this.tag = dataGridViewCellStyle.tag;
			}
			if (dataGridViewCellStyle.wrapMode != DataGridViewTriState.NotSet)
			{
				this.wrapMode = dataGridViewCellStyle.wrapMode;
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
		// Token: 0x0600129E RID: 4766 RVA: 0x00048C10 File Offset: 0x00046E10
		public virtual DataGridViewCellStyle Clone()
		{
			return new DataGridViewCellStyle(this);
		}

		/// <summary>Returns a value indicating whether this instance is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> and has the same property values as this instance; otherwise, false.</returns>
		/// <param name="o">An object to compare with this instance, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600129F RID: 4767 RVA: 0x00048C18 File Offset: 0x00046E18
		public override bool Equals(object o)
		{
			if (o is DataGridViewCellStyle)
			{
				DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)o;
				return this.alignment == dataGridViewCellStyle.alignment && this.backColor == dataGridViewCellStyle.backColor && this.dataSourceNullValue == dataGridViewCellStyle.dataSourceNullValue && this.font == dataGridViewCellStyle.font && this.foreColor == dataGridViewCellStyle.foreColor && this.format == dataGridViewCellStyle.format && this.formatProvider == dataGridViewCellStyle.formatProvider && this.nullValue == dataGridViewCellStyle.nullValue && this.padding == dataGridViewCellStyle.padding && this.selectionBackColor == dataGridViewCellStyle.selectionBackColor && this.selectionForeColor == dataGridViewCellStyle.selectionForeColor && this.tag == dataGridViewCellStyle.tag && this.wrapMode == dataGridViewCellStyle.wrapMode;
			}
			return false;
		}

		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012A0 RID: 4768 RVA: 0x00048D34 File Offset: 0x00046F34
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012A1 RID: 4769 RVA: 0x00048D3C File Offset: 0x00046F3C
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00048D44 File Offset: 0x00046F44
		internal void OnStyleChanged()
		{
			if (this.StyleChanged != null)
			{
				this.StyleChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00048D64 File Offset: 0x00046F64
		internal StringFormat SetAlignment(StringFormat format)
		{
			DataGridViewContentAlignment dataGridViewContentAlignment = this.Alignment;
			switch (dataGridViewContentAlignment)
			{
			case DataGridViewContentAlignment.TopLeft:
			case DataGridViewContentAlignment.TopCenter:
			case DataGridViewContentAlignment.TopRight:
				format.LineAlignment = 2;
				break;
			default:
				if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleLeft && dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleCenter && dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomLeft || dataGridViewContentAlignment == DataGridViewContentAlignment.BottomCenter || dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
					{
						format.LineAlignment = 0;
					}
				}
				else
				{
					format.LineAlignment = 1;
				}
				break;
			}
			dataGridViewContentAlignment = this.Alignment;
			switch (dataGridViewContentAlignment)
			{
			case DataGridViewContentAlignment.TopLeft:
				goto IL_00EA;
			case DataGridViewContentAlignment.TopCenter:
				break;
			default:
				if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleLeft)
				{
					goto IL_00EA;
				}
				if (dataGridViewContentAlignment != DataGridViewContentAlignment.MiddleCenter)
				{
					if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleRight)
					{
						goto IL_00F6;
					}
					if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomLeft)
					{
						goto IL_00EA;
					}
					if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomCenter)
					{
						if (dataGridViewContentAlignment != DataGridViewContentAlignment.BottomRight)
						{
							return format;
						}
						goto IL_00F6;
					}
				}
				break;
			case DataGridViewContentAlignment.TopRight:
				goto IL_00F6;
			}
			format.Alignment = 1;
			return format;
			IL_00EA:
			format.Alignment = 0;
			return format;
			IL_00F6:
			format.Alignment = 2;
			return format;
		}

		// Token: 0x04000B16 RID: 2838
		private DataGridViewContentAlignment alignment;

		// Token: 0x04000B17 RID: 2839
		private Color backColor;

		// Token: 0x04000B18 RID: 2840
		private object dataSourceNullValue;

		// Token: 0x04000B19 RID: 2841
		private Font font;

		// Token: 0x04000B1A RID: 2842
		private Color foreColor;

		// Token: 0x04000B1B RID: 2843
		private string format;

		// Token: 0x04000B1C RID: 2844
		private IFormatProvider formatProvider;

		// Token: 0x04000B1D RID: 2845
		private object nullValue;

		// Token: 0x04000B1E RID: 2846
		private Padding padding;

		// Token: 0x04000B1F RID: 2847
		private Color selectionBackColor;

		// Token: 0x04000B20 RID: 2848
		private Color selectionForeColor;

		// Token: 0x04000B21 RID: 2849
		private object tag;

		// Token: 0x04000B22 RID: 2850
		private DataGridViewTriState wrapMode;
	}
}
