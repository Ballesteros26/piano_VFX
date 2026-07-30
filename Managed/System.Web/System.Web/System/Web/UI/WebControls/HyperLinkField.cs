using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as a hyperlink in a data-bound control.</summary>
	// Token: 0x020003B4 RID: 948
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkField : DataControlField
	{
		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="enableSorting">true if sorting is supported; otherwise, false.</param>
		/// <param name="control">The data control that acts as the parent for the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		// Token: 0x060026CD RID: 9933 RVA: 0x0004964F File Offset: 0x0004784F
		public override bool Initialize(bool enableSorting, Control control)
		{
			return base.Initialize(enableSorting, control);
		}

		/// <summary>Gets or sets the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>An array containing the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty array, indicating that <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFields" /> is not set.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000653D8 File Offset: 0x000635D8
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x00065417 File Offset: 0x00063617
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Data")]
		[DefaultValue(null)]
		public virtual string[] DataNavigateUrlFields
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				if (HyperLinkField.emptyFields == null)
				{
					HyperLinkField.emptyFields = new string[0];
				}
				return HyperLinkField.emptyFields;
			}
			set
			{
				base.ViewState["DataNavigateUrlFields"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are rendered.</summary>
		/// <returns>A string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are rendered. The default is an empty string (""), which indicates that no special formatting is applied to the URL values.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x00065430 File Offset: 0x00063630
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0006545D File Offset: 0x0006365D
		[DefaultValue("")]
		[WebCategory("Data")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the field from the data source containing the text to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>The name of the field from the data source containing the values to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x00065478 File Offset: 0x00063678
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0004A692 File Offset: 0x00048892
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Get or sets the string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are displayed.</summary>
		/// <returns>A string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are displayed. The default is an empty string (""), which indicates that no special formatting is applied to the hyperlink captions.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000654A8 File Offset: 0x000636A8
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x0004A6C2 File Offset: 0x000488C2
		[WebCategory("Data")]
		[DefaultValue("")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
		/// <returns>The URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000654D8 File Offset: 0x000636D8
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x00065505 File Offset: 0x00063705
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebCategory("Behavior")]
		public virtual string NavigateUrl
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x00065520 File Offset: 0x00063720
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0006554D File Offset: 0x0006374D
		[WebCategory("Behavior")]
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				object obj = base.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Target"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>The text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x00065568 File Offset: 0x00063768
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x0004A722 File Offset: 0x00048922
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes a cell in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values that specifies the state of the row containing the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		/// <param name="rowIndex">The index of the row in the table.</param>
		// Token: 0x060026DC RID: 9948 RVA: 0x00065598 File Offset: 0x00063798
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				HyperLink hyperLink = new HyperLink();
				bool flag = false;
				if (this.Target.Length > 0)
				{
					hyperLink.Target = this.Target;
				}
				if (this.DataTextField.Length > 0)
				{
					flag = true;
				}
				else
				{
					hyperLink.Text = this.Text;
				}
				if (this.DataNavigateUrlFields.Length != 0)
				{
					flag = true;
				}
				else
				{
					hyperLink.NavigateUrl = this.NavigateUrl;
				}
				if (flag && cellType == DataControlCellType.DataCell && (rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
				hyperLink.ControlStyle.CopyFrom(base.ControlStyle);
				cell.Controls.Add(hyperLink);
			}
		}

		/// <summary>Formats the navigation URL using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFormatString" /> property.</summary>
		/// <returns>The formatted URL value.</returns>
		/// <param name="dataUrlValues">An array of values to combine with the format string.</param>
		// Token: 0x060026DD RID: 9949 RVA: 0x0006564B File Offset: 0x0006384B
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			if (dataUrlValues == null || dataUrlValues.Length == 0)
			{
				return string.Empty;
			}
			if (this.DataNavigateUrlFormatString.Length > 0)
			{
				return string.Format(this.DataNavigateUrlFormatString, dataUrlValues);
			}
			return dataUrlValues[0].ToString();
		}

		/// <summary>Formats the caption text using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataTextFormatString" /> property.</summary>
		/// <returns>The formatted text value.</returns>
		/// <param name="dataTextValue">The text value to format. </param>
		// Token: 0x060026DE RID: 9950 RVA: 0x0006567D File Offset: 0x0006387D
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			if (this.DataTextFormatString.Length > 0)
			{
				return string.Format(this.DataTextFormatString, dataTextValue);
			}
			if (dataTextValue == null)
			{
				return string.Empty;
			}
			return dataTextValue.ToString();
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000656AC File Offset: 0x000638AC
		private void OnDataBindField(object sender, EventArgs e)
		{
			DataControlFieldCell dataControlFieldCell = (DataControlFieldCell)sender;
			HyperLink hyperLink = (HyperLink)dataControlFieldCell.Controls[0];
			object bindingContainer = dataControlFieldCell.BindingContainer;
			object dataItem = DataBinder.GetDataItem(bindingContainer);
			if (this.DataTextField.Length > 0)
			{
				if (this.textProperty == null)
				{
					this.SetupProperties(bindingContainer);
				}
				hyperLink.Text = this.FormatDataTextValue(this.textProperty.GetValue(dataItem));
			}
			string[] dataNavigateUrlFields = this.DataNavigateUrlFields;
			if (dataNavigateUrlFields.Length != 0)
			{
				if (this.urlProperties == null)
				{
					this.SetupProperties(bindingContainer);
				}
				object[] array = new object[dataNavigateUrlFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.urlProperties[i].GetValue(dataItem);
				}
				hyperLink.NavigateUrl = this.FormatDataNavigateUrlValue(array);
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0006576C File Offset: 0x0006396C
		private void SetupProperties(object controlContainer)
		{
			object dataItem = DataBinder.GetDataItem(controlContainer);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			if (this.DataTextField.Length > 0)
			{
				this.textProperty = properties.Find(this.DataTextField, true);
				if (this.textProperty == null)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"Property '",
						this.DataTextField,
						"' not found in object of type ",
						dataItem.GetType()
					}));
				}
			}
			string[] dataNavigateUrlFields = this.DataNavigateUrlFields;
			if (dataNavigateUrlFields.Length != 0)
			{
				this.urlProperties = new PropertyDescriptor[dataNavigateUrlFields.Length];
				for (int i = 0; i < dataNavigateUrlFields.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = properties.Find(dataNavigateUrlFields[i], true);
					if (propertyDescriptor == null)
					{
						throw new InvalidOperationException(string.Concat(new object[]
						{
							"Property '",
							dataNavigateUrlFields[i],
							"' not found in object of type ",
							dataItem.GetType()
						}));
					}
					this.urlProperties[i] = propertyDescriptor;
				}
			}
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</returns>
		// Token: 0x060026E1 RID: 9953 RVA: 0x00065854 File Offset: 0x00063A54
		protected override DataControlField CreateField()
		{
			return new HyperLinkField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object to the specified object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object that receives the copy.</param>
		// Token: 0x060026E2 RID: 9954 RVA: 0x0006585C File Offset: 0x00063A5C
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			HyperLinkField hyperLinkField = (HyperLinkField)newField;
			hyperLinkField.DataNavigateUrlFields = this.DataNavigateUrlFields;
			hyperLinkField.DataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
			hyperLinkField.DataTextField = this.DataTextField;
			hyperLinkField.DataTextFormatString = this.DataTextFormatString;
			hyperLinkField.NavigateUrl = this.NavigateUrl;
			hyperLinkField.Target = this.Target;
			hyperLinkField.Text = this.Text;
		}

		/// <summary>Indicates that the controls contained by the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object support callbacks.</summary>
		// Token: 0x060026E3 RID: 9955 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001A52 RID: 6738
		private PropertyDescriptor textProperty;

		// Token: 0x04001A53 RID: 6739
		private PropertyDescriptor[] urlProperties;

		// Token: 0x04001A54 RID: 6740
		private static string[] emptyFields;
	}
}
