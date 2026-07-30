using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as text in a data-bound control.</summary>
	// Token: 0x0200033C RID: 828
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BoundField : DataControlField
	{
		/// <summary>Gets or sets a value indicating whether the formatting string specified by the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property is applied to field values when the data-bound control that contains the <see cref="T:System.Web.UI.WebControls.BoundField" /> object is in edit mode.</summary>
		/// <returns>true to apply the formatting string to field values in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00049440 File Offset: 0x00047640
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x00049453 File Offset: 0x00047653
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ApplyFormatInEditMode
		{
			get
			{
				return base.ViewState.GetBool("ApplyFormatInEditMode", false);
			}
			set
			{
				base.ViewState["ApplyFormatInEditMode"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether empty string values ("") are automatically converted to null values when the data field is updated in the data source.</summary>
		/// <returns>true to automatically convert empty string values to null values; otherwise, the false. The default is true.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0004946B File Offset: 0x0004766B
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0004947E File Offset: 0x0004767E
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				return base.ViewState.GetBool("ConvertEmptyStringToNull", true);
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0004949C File Offset: 0x0004769C
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x000494B3 File Offset: 0x000476B3
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataField
		{
			get
			{
				return base.ViewState.GetString("DataField", string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
		/// <returns>A formatting string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000494CC File Offset: 0x000476CC
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x000494E3 File Offset: 0x000476E3
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataFormatString
		{
			get
			{
				return base.ViewState.GetString("DataFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the text that is displayed in the header of a data control.</summary>
		/// <returns>The text displayed in the header of a data control. The default value is an empty string ("").</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x000494FC File Offset: 0x000476FC
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00049513 File Offset: 0x00047713
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		public override string HeaderText
		{
			get
			{
				return base.ViewState.GetString("HeaderText", string.Empty);
			}
			set
			{
				base.ViewState["HeaderText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption displayed for a field when the field's value is null.</summary>
		/// <returns>The caption displayed for a field when the field's value is null. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0004952C File Offset: 0x0004772C
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x00049543 File Offset: 0x00047743
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string NullDisplayText
		{
			get
			{
				return base.ViewState.GetString("NullDisplayText", string.Empty);
			}
			set
			{
				base.ViewState["NullDisplayText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the value of the field can be modified in edit mode.</summary>
		/// <returns>true to prevent the value of the field from being modified in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0004955C File Offset: 0x0004775C
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0004956F File Offset: 0x0004776F
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return base.ViewState.GetBool("ReadOnly", false);
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>true if field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object; otherwise, false. The default is true.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0004958D File Offset: 0x0004778D
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x000495A0 File Offset: 0x000477A0
		[WebCategory("HtmlEncode")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public virtual bool HtmlEncode
		{
			get
			{
				return base.ViewState.GetBool("HtmlEncode", true);
			}
			set
			{
				base.ViewState["HtmlEncode"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value that indicates whether text that is created by applying the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property to the <see cref="T:System.Web.UI.WebControls.BoundField" /> value should be HTML encoded when it is displayed.</summary>
		/// <returns>true if the text should be HTML-encoded; otherwise, false. The default is true.</returns>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x000495BE File Offset: 0x000477BE
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x000495D1 File Offset: 0x000477D1
		[DefaultValue(true)]
		public virtual bool HtmlEncodeFormatString
		{
			get
			{
				return base.ViewState.GetBool("HtmlEncodeFormatString", true);
			}
			set
			{
				base.ViewState["HtmlEncodeFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object.</summary>
		/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains the values to retrieve.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to include the values of read-only fields; otherwise, false.</param>
		// Token: 0x06001D63 RID: 7523 RVA: 0x000495F0 File Offset: 0x000477F0
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			if (this.IsEditable(rowState))
			{
				if (cell.Controls.Count > 0)
				{
					TextBox textBox = (TextBox)cell.Controls[0];
					dictionary[this.DataField] = textBox.Text;
					return;
				}
			}
			else if (includeReadOnly)
			{
				dictionary[this.DataField] = cell.Text;
			}
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="enableSorting">true if sorting is supported; otherwise, false.</param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.BoundField" />.</param>
		// Token: 0x06001D64 RID: 7524 RVA: 0x0004964F File Offset: 0x0004784F
		public override bool Initialize(bool enableSorting, Control control)
		{
			return base.Initialize(enableSorting, control);
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row.</param>
		// Token: 0x06001D65 RID: 7525 RVA: 0x00049659 File Offset: 0x00047859
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				this.InitializeDataCell(cell, rowState);
				if ((rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		// Token: 0x06001D66 RID: 7526 RVA: 0x0004968C File Offset: 0x0004788C
		protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			if (this.IsEditable(rowState))
			{
				TextBox textBox = new TextBox();
				cell.Controls.Add(textBox);
				textBox.ToolTip = this.HeaderText;
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000496C0 File Offset: 0x000478C0
		internal bool IsEditable(DataControlRowState rowState)
		{
			return ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && !this.ReadOnly) || ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.InsertVisible);
		}

		/// <summary>Gets a value indicating whether HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>Always returns true to indicate that HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool SupportsHtmlEncode
		{
			get
			{
				return true;
			}
		}

		/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The field value converted to the format specified by <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" />.</returns>
		/// <param name="dataValue">The field value to format.</param>
		/// <param name="encode">true to encode the value; otherwise, false.</param>
		// Token: 0x06001D69 RID: 7529 RVA: 0x000496E0 File Offset: 0x000478E0
		protected virtual string FormatDataValue(object dataValue, bool encode)
		{
			bool htmlEncodeFormatString = this.HtmlEncodeFormatString;
			string text = ((dataValue != null) ? dataValue.ToString() : string.Empty);
			string text2;
			if (dataValue == null || (text.Length == 0 && this.ConvertEmptyStringToNull))
			{
				if (this.NullDisplayText.Length == 0)
				{
					encode = false;
					text2 = "&nbsp;";
				}
				else
				{
					text2 = this.NullDisplayText;
				}
			}
			else
			{
				string dataFormatString = this.DataFormatString;
				if (!string.IsNullOrEmpty(dataFormatString))
				{
					if (!encode || htmlEncodeFormatString)
					{
						text2 = string.Format(dataFormatString, dataValue);
					}
					else
					{
						text2 = string.Format(dataFormatString, encode ? HttpUtility.HtmlEncode(text) : text);
					}
				}
				else
				{
					text2 = text;
				}
			}
			if (encode && htmlEncodeFormatString)
			{
				return HttpUtility.HtmlEncode(text2);
			}
			return text2;
		}

		/// <summary>Retrieves the value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		/// <param name="controlContainer">The container for the field value.</param>
		/// <exception cref="T:System.Web.HttpException">The container specified by the <paramref name="controlContainer" /> parameter is null.- or - The container specified by the <paramref name="controlContainer" /> parameter does not have a data item.- or - The data field was not found. </exception>
		// Token: 0x06001D6A RID: 7530 RVA: 0x0004977E File Offset: 0x0004797E
		protected virtual object GetValue(Control controlContainer)
		{
			if (base.DesignMode)
			{
				return this.GetDesignTimeValue();
			}
			return this.GetBoundValue(controlContainer);
		}

		/// <summary>Retrieves the value used for a field's value when rendering the <see cref="T:System.Web.UI.WebControls.BoundField" /> object in a designer.</summary>
		/// <returns>The value to display in the designer as the field's value.</returns>
		// Token: 0x06001D6B RID: 7531 RVA: 0x00049796 File Offset: 0x00047996
		protected virtual object GetDesignTimeValue()
		{
			return "Databound";
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000497A0 File Offset: 0x000479A0
		private object GetBoundValue(Control controlContainer)
		{
			object dataItem = DataBinder.GetDataItem(controlContainer);
			if (dataItem == null)
			{
				throw new HttpException("A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.");
			}
			if (this.DataField == BoundField.ThisExpression)
			{
				return dataItem;
			}
			if (this.DataField == string.Empty)
			{
				return null;
			}
			return DataBinder.GetPropertyValue(dataItem, this.DataField);
		}

		/// <summary>Restores the previously stored view-state information for this field.</summary>
		/// <param name="state">Represents the control state to be restored.</param>
		// Token: 0x06001D6D RID: 7533 RVA: 0x000497F6 File Offset: 0x000479F6
		protected override void LoadViewState(object state)
		{
			base.LoadViewState(state);
		}

		/// <summary>Binds the value of a field to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.TextBox" /> or a <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
		// Token: 0x06001D6E RID: 7534 RVA: 0x00049800 File Offset: 0x00047A00
		protected virtual void OnDataBindField(object sender, EventArgs e)
		{
			Control bindingContainer = ((Control)sender).BindingContainer;
			if (!(bindingContainer is INamingContainer))
			{
				throw new HttpException("A DataControlField must be within an INamingContainer.");
			}
			object value = this.GetValue(bindingContainer);
			TextBox textBox = sender as TextBox;
			if (textBox == null)
			{
				DataControlFieldCell dataControlFieldCell = sender as DataControlFieldCell;
				if (dataControlFieldCell != null)
				{
					ControlCollection controls = dataControlFieldCell.Controls;
					if (((controls != null) ? controls.Count : 0) == 1)
					{
						textBox = controls[0] as TextBox;
					}
					if (textBox == null)
					{
						dataControlFieldCell.Text = this.FormatDataValue(value, this.SupportsHtmlEncode && this.HtmlEncode);
						return;
					}
				}
			}
			if (textBox == null)
			{
				throw new HttpException("Bound field " + this.DataField + " contains a control that isn't a TextBox.  Override OnDataBindField to inherit from BoundField and add different controls.");
			}
			if (this.ApplyFormatInEditMode)
			{
				textBox.Text = this.FormatDataValue(value, this.SupportsHtmlEncode && this.HtmlEncode);
				return;
			}
			textBox.Text = ((value != null) ? value.ToString() : this.NullDisplayText);
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		// Token: 0x06001D6F RID: 7535 RVA: 0x000498ED File Offset: 0x00047AED
		protected override DataControlField CreateField()
		{
			return new BoundField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> to.</param>
		// Token: 0x06001D70 RID: 7536 RVA: 0x000498F4 File Offset: 0x00047AF4
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			BoundField boundField = (BoundField)newField;
			boundField.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			boundField.DataField = this.DataField;
			boundField.DataFormatString = this.DataFormatString;
			boundField.NullDisplayText = this.NullDisplayText;
			boundField.ReadOnly = this.ReadOnly;
			boundField.HtmlEncode = this.HtmlEncode;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object support callbacks.</summary>
		// Token: 0x06001D71 RID: 7537 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void ValidateSupportsCallback()
		{
		}

		/// <summary>Gets or sets a value that determines whether the control validates client input or not.</summary>
		/// <returns>A value that determines whether the control validates client input or not. The default is <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0004996C File Offset: 0x00047B6C
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public new ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Represents the "this" expression.</summary>
		// Token: 0x04001826 RID: 6182
		public static readonly string ThisExpression = "!";
	}
}
