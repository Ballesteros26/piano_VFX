using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a Boolean field that is displayed as a check box in a data-bound control.</summary>
	// Token: 0x0200034D RID: 845
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxField : BoundField
	{
		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ApplyFormatInEditMode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0004F096 File Offset: 0x0004D296
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x0004F096 File Offset: 0x0004D296
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool ApplyFormatInEditMode
		{
			get
			{
				throw base.GetNotSupportedPropException("ApplyFormatInEditMode");
			}
			set
			{
				throw base.GetNotSupportedPropException("ApplyFormatInEditMode");
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ConvertEmptyStringToNull" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0004F0A3 File Offset: 0x0004D2A3
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x0004F0A3 File Offset: 0x0004D2A3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ConvertEmptyStringToNull
		{
			get
			{
				throw base.GetNotSupportedPropException("ConvertEmptyStringToNull");
			}
			set
			{
				throw base.GetNotSupportedPropException("ConvertEmptyStringToNull");
			}
		}

		/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0004F0B0 File Offset: 0x0004D2B0
		// (set) Token: 0x06001F28 RID: 7976 RVA: 0x0004F0B8 File Offset: 0x0004D2B8
		[TypeConverter("System.Web.UI.Design.DataSourceBooleanViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string DataField
		{
			get
			{
				return base.DataField;
			}
			set
			{
				base.DataField = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>A formatting string that specifies the display format for the value of the field. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0004F0C1 File Offset: 0x0004D2C1
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x0004F0C1 File Offset: 0x0004D2C1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string DataFormatString
		{
			get
			{
				throw base.GetNotSupportedPropException("DataFormatString");
			}
			set
			{
				throw base.GetNotSupportedPropException("DataFormatString");
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.HtmlEncode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0004F0CE File Offset: 0x0004D2CE
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0004F0CE File Offset: 0x0004D2CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool HtmlEncode
		{
			get
			{
				throw base.GetNotSupportedPropException("HtmlEncode");
			}
			set
			{
				throw base.GetNotSupportedPropException("HtmlEncode");
			}
		}

		/// <summary>Gets or sets a value that indicates whether the formatted text should be HTML encoded before it is displayed.</summary>
		/// <returns>true if the text should be HTML encoded; otherwise, false. The default is true.</returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0004F0DB File Offset: 0x0004D2DB
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0004F0E3 File Offset: 0x0004D2E3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HtmlEncodeFormatString
		{
			get
			{
				return base.HtmlEncodeFormatString;
			}
			set
			{
				base.HtmlEncodeFormatString = value;
			}
		}

		/// <summary>Gets or sets the text displayed for a field when the field's value is null. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>The text displayed for a field with a value of null. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string NullDisplayText
		{
			get
			{
				throw base.GetNotSupportedPropException("NullDisplayText");
			}
			set
			{
				throw base.GetNotSupportedPropException("NullDisplayText");
			}
		}

		/// <summary>Gets a Boolean value indicating whether the control supports HTML encoding.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00008A69 File Offset: 0x00006C69
		protected override bool SupportsHtmlEncode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the caption to display next to each check box in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>The caption displayed next to each check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string ("").</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0004A70B File Offset: 0x0004890B
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x0004A722 File Offset: 0x00048922
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
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

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		// Token: 0x06001F34 RID: 7988 RVA: 0x0004F0FC File Offset: 0x0004D2FC
		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			bool flag = base.IsEditable(rowState);
			CheckBox checkBox = new CheckBox();
			checkBox.Enabled = flag;
			if (flag)
			{
				checkBox.ToolTip = this.HeaderText;
			}
			checkBox.Text = this.Text;
			cell.Controls.Add(checkBox);
		}

		/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object.</summary>
		/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the values to retrieve.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to include the values of read-only fields; otherwise, false.</param>
		// Token: 0x06001F35 RID: 7989 RVA: 0x0004F148 File Offset: 0x0004D348
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			if (base.IsEditable(rowState) || includeReadOnly)
			{
				CheckBox checkBox = (CheckBox)cell.Controls[0];
				dictionary[this.DataField] = checkBox.Checked;
			}
		}

		/// <summary>Binds the value of a field to a check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.- or -The field value cannot be converted to a Boolean value. </exception>
		// Token: 0x06001F36 RID: 7990 RVA: 0x0004F18C File Offset: 0x0004D38C
		protected override void OnDataBindField(object sender, EventArgs e)
		{
			try
			{
				Control control = (Control)sender;
				object value = this.GetValue(control.NamingContainer);
				CheckBox checkBox = sender as CheckBox;
				if (checkBox == null)
				{
					DataControlFieldCell dataControlFieldCell = sender as DataControlFieldCell;
					if (dataControlFieldCell != null)
					{
						ControlCollection controls = dataControlFieldCell.Controls;
						if (((controls != null) ? controls.Count : 0) == 1)
						{
							checkBox = controls[0] as CheckBox;
						}
						if (checkBox == null)
						{
							return;
						}
					}
				}
				if (checkBox == null)
				{
					throw new HttpException("CheckBox field '" + this.DataField + "' contains a control that isn't a CheckBox.  Override OnDataBindField to inherit from CheckBoxField and add different controls.");
				}
				if (value != null && value != DBNull.Value)
				{
					checkBox.Checked = (bool)value;
				}
				else if (string.IsNullOrEmpty(this.DataField))
				{
					checkBox.Visible = false;
					return;
				}
				if (!checkBox.Visible)
				{
					checkBox.Visible = true;
				}
			}
			catch (HttpException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new HttpException(ex.Message, ex);
			}
		}

		/// <summary>Retrieves the value used for the field's value when rendering the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object in a designer.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x06001F37 RID: 7991 RVA: 0x0004F27C File Offset: 0x0004D47C
		protected override object GetDesignTimeValue()
		{
			return true;
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" />.</returns>
		// Token: 0x06001F38 RID: 7992 RVA: 0x0004F284 File Offset: 0x0004D484
		protected override DataControlField CreateField()
		{
			return new CheckBoxField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> to.</param>
		// Token: 0x06001F39 RID: 7993 RVA: 0x0004F28B File Offset: 0x0004D48B
		protected override void CopyProperties(DataControlField newField)
		{
			CheckBoxField checkBoxField = (CheckBoxField)newField;
			checkBoxField.DataField = this.DataField;
			checkBoxField.ReadOnly = this.ReadOnly;
			checkBoxField.Text = this.Text;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object support callbacks.</summary>
		// Token: 0x06001F3A RID: 7994 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void ValidateSupportsCallback()
		{
		}
	}
}
