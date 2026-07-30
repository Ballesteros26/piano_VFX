using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as a button in a data-bound control.</summary>
	// Token: 0x02000342 RID: 834
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ButtonField : ButtonFieldBase
	{
		/// <summary>Gets or sets a string that represents the action to perform when a button in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object is clicked.</summary>
		/// <returns>The name of the action to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> is clicked.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0004A64B File Offset: 0x0004884B
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0004A662 File Offset: 0x00048862
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("Raised when a Button Command is executed.")]
		public virtual string CommandName
		{
			get
			{
				return base.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				base.ViewState["CommandName"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the data field for which the value is bound to the <see cref="P:System.Web.UI.WebControls.Button.Text" /> property of the <see cref="T:System.Web.UI.WebControls.Button" /> control that is rendered by the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextField" /> property is not set.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0004A67B File Offset: 0x0004887B
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0004A692 File Offset: 0x00048892
		[WebCategory("Data")]
		[WebSysDescription("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				return base.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
		/// <returns>A format string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0004A6AB File Offset: 0x000488AB
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0004A6C2 File Offset: 0x000488C2
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("")]
		public virtual string DataTextFormatString
		{
			get
			{
				return base.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.ImageUrl" /> property is not set.</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0004A6DB File Offset: 0x000488DB
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0004A6F2 File Offset: 0x000488F2
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebSysDescription("")]
		public virtual string ImageUrl
		{
			get
			{
				return base.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the static caption that is displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The caption displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string ("").</returns>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0004A70B File Offset: 0x0004890B
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0004A722 File Offset: 0x00048922
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return base.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>false, which indicates the control does not need to rebind to the data.</returns>
		/// <param name="sortingEnabled">true to enable sorting; otherwise, false. </param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.ButtonField" />. </param>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x0004964F File Offset: 0x0004784F
		public override bool Initialize(bool sortingEnabled, Control control)
		{
			return base.Initialize(sortingEnabled, control);
		}

		/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The field value converted to the format specified by the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextFormatString" /> property.</returns>
		/// <param name="dataTextValue">The field value to format. </param>
		// Token: 0x06001DD7 RID: 7639 RVA: 0x0004A73B File Offset: 0x0004893B
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

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="cellType">A <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> value that indicates the type of row (header, footer, or data).</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row.</param>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x0004A768 File Offset: 0x00048968
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			string text = rowIndex.ToString();
			if (cellType == DataControlCellType.DataCell)
			{
				IDataControlButton dataControlButton = DataControlButton.CreateButton(this.ButtonType, base.Control, this.Text, this.ImageUrl, this.CommandName, text, false);
				if (this.CausesValidation)
				{
					dataControlButton.Container = null;
					dataControlButton.CausesValidation = true;
					dataControlButton.ValidationGroup = this.ValidationGroup;
				}
				if (!string.IsNullOrEmpty(this.DataTextField) && (rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
				cell.Controls.Add((Control)dataControlButton);
				return;
			}
			base.InitializeCell(cell, cellType, rowState, rowIndex);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0004A80C File Offset: 0x00048A0C
		private void OnDataBindField(object sender, EventArgs e)
		{
			DataControlFieldCell dataControlFieldCell = (DataControlFieldCell)sender;
			((IDataControlButton)dataControlFieldCell.Controls[0]).Text = this.FormatDataTextValue(this.GetBoundValue(dataControlFieldCell.BindingContainer));
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0004A848 File Offset: 0x00048A48
		private object GetBoundValue(Control controlContainer)
		{
			IDataItemContainer dataItemContainer = controlContainer as IDataItemContainer;
			if (this.boundProperty == null)
			{
				this.boundProperty = TypeDescriptor.GetProperties(dataItemContainer.DataItem)[this.DataTextField];
				if (this.boundProperty == null)
				{
					throw new InvalidOperationException(string.Concat(new object[]
					{
						"Property '",
						this.DataTextField,
						"' not found in object of type ",
						dataItemContainer.DataItem.GetType()
					}));
				}
			}
			return this.boundProperty.GetValue(dataItemContainer.DataItem);
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</summary>
		/// <returns>A new instance of the  <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</returns>
		// Token: 0x06001DDB RID: 7643 RVA: 0x0004A8D1 File Offset: 0x00048AD1
		protected override DataControlField CreateField()
		{
			return new ButtonField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" />.</param>
		// Token: 0x06001DDC RID: 7644 RVA: 0x0004A8D8 File Offset: 0x00048AD8
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			ButtonField buttonField = (ButtonField)newField;
			buttonField.CommandName = this.CommandName;
			buttonField.DataTextField = this.DataTextField;
			buttonField.DataTextFormatString = this.DataTextFormatString;
			buttonField.ImageUrl = this.ImageUrl;
			buttonField.Text = this.Text;
		}

		/// <summary>Determines whether the controls that are contained in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object support callbacks.</summary>
		// Token: 0x06001DDD RID: 7645 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x0400183C RID: 6204
		private PropertyDescriptor boundProperty;
	}
}
