using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Configuration;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for validation controls.</summary>
	// Token: 0x0200033A RID: 826
	[Designer("System.Web.UI.Design.WebControls.BaseValidatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("ErrorMessage")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseValidator : Label, IValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> class.</summary>
		// Token: 0x06001D18 RID: 7448 RVA: 0x00048963 File Offset: 0x00046B63
		protected BaseValidator()
		{
			this.valid = true;
			this.ForeColor = Color.Red;
		}

		/// <summary>This property is not supported.</summary>
		/// <returns>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0004897D File Offset: 0x00046B7D
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x00048985 File Offset: 0x00046B85
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string AssociatedControlID
		{
			get
			{
				return base.AssociatedControlID;
			}
			set
			{
				base.AssociatedControlID = value;
			}
		}

		/// <summary>Gets or sets the name of the validation group to which this validation control belongs.</summary>
		/// <returns>The name of the validation group to which this validation control belongs. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x000419CA File Offset: 0x0003FBCA
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether focus is set to the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property when validation fails.</summary>
		/// <returns>true to set focus on the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> when validation fails; otherwise, false. The default is false.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0004898E File Offset: 0x00046B8E
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x000489A1 File Offset: 0x00046BA1
		[DefaultValue(false)]
		[Themeable(false)]
		public bool SetFocusOnError
		{
			get
			{
				return this.ViewState.GetBool("SetFocusOnError", false);
			}
			set
			{
				this.ViewState["SetFocusOnError"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the validation control when validation fails.</summary>
		/// <returns>The text displayed in the validation control when validation fails. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000489B9 File Offset: 0x00046BB9
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x000489C1 File Offset: 0x00046BC1
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[global::System.MonoTODO("Why override?")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the input control to validate.</summary>
		/// <returns>The input control to validate. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000489CA File Offset: 0x00046BCA
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x000489E1 File Offset: 0x00046BE1
		[Themeable(false)]
		[IDReferenceProperty(typeof(Control))]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[TypeConverter(typeof(ValidatedControlConverter))]
		public string ControlToValidate
		{
			get
			{
				return this.ViewState.GetString("ControlToValidate", string.Empty);
			}
			set
			{
				this.ViewState["ControlToValidate"] = value;
			}
		}

		/// <summary>Gets or sets the display behavior of the error message in a validation control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. The default value is Static.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. </exception>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x000489F4 File Offset: 0x00046BF4
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00048A07 File Offset: 0x00046C07
		[Themeable(false)]
		[DefaultValue(ValidatorDisplay.Static)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public ValidatorDisplay Display
		{
			get
			{
				return (ValidatorDisplay)this.ViewState.GetInt("Display", 1);
			}
			set
			{
				this.ViewState["Display"] = (int)value;
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side validation is enabled.</summary>
		/// <returns>true if client-side validation is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00048A1F File Offset: 0x00046C1F
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00048A32 File Offset: 0x00046C32
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		public bool EnableClientScript
		{
			get
			{
				return this.ViewState.GetBool("EnableClientScript", true);
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the validation control is enabled.</summary>
		/// <returns>true if the validation control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00048A4A File Offset: 0x00046C4A
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00048A5D File Offset: 0x00046C5D
		public override bool Enabled
		{
			get
			{
				return this.ViewState.GetBool("BaseValidatorEnabled", true);
			}
			set
			{
				this.ViewState["BaseValidatorEnabled"] = value;
			}
		}

		/// <summary>Gets or sets the text for the error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails.</summary>
		/// <returns>The error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00048A75 File Offset: 0x00046C75
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x00048A8C File Offset: 0x00046C8C
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string ErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ErrorMessage", string.Empty);
			}
			set
			{
				this.ViewState["ErrorMessage"] = value;
			}
		}

		/// <summary>Gets or sets the color of the message displayed when validation fails.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the message displayed when validation fails. The default is <see cref="P:System.Drawing.Color.Red" />.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x00048A9F File Offset: 0x00046C9F
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x00048AA7 File Offset: 0x00046CA7
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return this.forecolor;
			}
			set
			{
				this.forecolor = value;
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated input control passes validation.</summary>
		/// <returns>true if the associated input control passes validation; otherwise, false. The default value is true.</returns>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x00048AB7 File Offset: 0x00046CB7
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x00048ABF File Offset: 0x00046CBF
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[DefaultValue(true)]
		[Browsable(false)]
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
		/// <returns>true if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">No value is specified in the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page. </exception>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00048AC8 File Offset: 0x00046CC8
		protected bool PropertiesValid
		{
			get
			{
				return this.NamingContainer.FindControl(this.ControlToValidate) != null;
			}
		}

		/// <summary>Gets a value that indicates whether the client's browser supports "uplevel" rendering.</summary>
		/// <returns>true if the browser supports "uplevel" rendering; otherwise, false.</returns>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x00048AE0 File Offset: 0x00046CE0
		protected bool RenderUplevel
		{
			get
			{
				return this.render_uplevel;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00048AE0 File Offset: 0x00046CE0
		internal bool GetRenderUplevel()
		{
			return this.render_uplevel;
		}

		/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001D32 RID: 7474 RVA: 0x00048AE8 File Offset: 0x00046CE8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.render_uplevel)
			{
				if (this.ID == null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				}
				if (this.ControlToValidate != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "controltovalidate", this.GetControlRenderID(this.ControlToValidate));
				}
				if (this.ErrorMessage != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "errormessage", this.ErrorMessage, true);
				}
				if (this.ValidationGroup != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "validationGroup", this.ValidationGroup, true);
				}
				if (this.SetFocusOnError)
				{
					this.RegisterExpandoAttribute(this.ClientID, "focusOnError", "t");
				}
				bool isEnabled = base.IsEnabled;
				if (!isEnabled)
				{
					this.RegisterExpandoAttribute(this.ClientID, "enabled", "False");
				}
				if (isEnabled && !this.IsValid)
				{
					this.RegisterExpandoAttribute(this.ClientID, "isvalid", "False");
				}
				else if (this.Display == ValidatorDisplay.Static)
				{
					writer.AddStyleAttribute("visibility", "hidden");
				}
				else
				{
					writer.AddStyleAttribute("display", "none");
				}
				if (this.Display != ValidatorDisplay.Static)
				{
					this.RegisterExpandoAttribute(this.ClientID, "display", this.Display.ToString());
				}
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00048C58 File Offset: 0x00046E58
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, false);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00048C64 File Offset: 0x00046E64
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			Page page = this.Page;
			if (page.ScriptManager != null)
			{
				page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
				return;
			}
			page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}

		/// <summary>Verifies whether the specified control is on the page and contains validation properties.</summary>
		/// <param name="name">The control to verify. </param>
		/// <param name="propertyName">Additional text to describe the source of the exception, if an exception is thrown from using this method. </param>
		/// <exception cref="T:System.Web.HttpException">The specified control is not found.- or -The specified control does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control. </exception>
		// Token: 0x06001D35 RID: 7477 RVA: 0x00048CA2 File Offset: 0x00046EA2
		protected void CheckControlValidationProperty(string name, string propertyName)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				throw new HttpException(string.Format("Unable to find control id '{0}'.", name));
			}
			if (BaseValidator.GetValidationProperty(control) == null)
			{
				throw new HttpException(string.Format("Unable to find ValidationProperty attribute '{0}' on control '{1}'", propertyName, name));
			}
		}

		/// <summary>Determines whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
		/// <returns>true if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">No value is specified for the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control.</exception>
		// Token: 0x06001D36 RID: 7478 RVA: 0x00048CDD File Offset: 0x00046EDD
		protected virtual bool ControlPropertiesValid()
		{
			if (this.ControlToValidate.Length == 0)
			{
				throw new HttpException(string.Format("ControlToValidate property of '{0}' cannot be blank.", this.ID));
			}
			this.CheckControlValidationProperty(this.ControlToValidate, string.Empty);
			return true;
		}

		/// <summary>Determines whether the validation control can perform client-side validation.</summary>
		/// <returns>true if the validation control can perform client-side validation; otherwise, false.</returns>
		// Token: 0x06001D37 RID: 7479 RVA: 0x00048D14 File Offset: 0x00046F14
		protected virtual bool DetermineRenderUplevel()
		{
			return this.EnableClientScript && UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request));
		}

		/// <summary>When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06001D38 RID: 7480
		protected abstract bool EvaluateIsValid();

		/// <summary>Gets the client ID of the specified control.</summary>
		/// <returns>The client ID of the specified control.</returns>
		/// <param name="name">The name of the control to get the client ID from. </param>
		// Token: 0x06001D39 RID: 7481 RVA: 0x00048D34 File Offset: 0x00046F34
		protected string GetControlRenderID(string name)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				return null;
			}
			return control.ClientID;
		}

		/// <summary>Gets the value associated with the specified input control.</summary>
		/// <returns>The value associated with the specified input control.</returns>
		/// <param name="name">The name of the input control to get the value from. </param>
		// Token: 0x06001D3A RID: 7482 RVA: 0x00048D5C File Offset: 0x00046F5C
		protected string GetControlValidationValue(string name)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				return null;
			}
			PropertyDescriptor validationProperty = BaseValidator.GetValidationProperty(control);
			if (validationProperty == null)
			{
				return null;
			}
			object value = validationProperty.GetValue(control);
			if (value == null)
			{
				return string.Empty;
			}
			if (value is ListItem)
			{
				return ((ListItem)value).Value;
			}
			return value.ToString();
		}

		/// <summary>Determines the validation property of a control (if it exists).</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the validation property of the control.</returns>
		/// <param name="component">A <see cref="T:System.Object" /> that represents the control to get the validation property of. </param>
		// Token: 0x06001D3B RID: 7483 RVA: 0x00048DB4 File Offset: 0x00046FB4
		public static PropertyDescriptor GetValidationProperty(object component)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			foreach (object obj in TypeDescriptor.GetAttributes(component))
			{
				ValidationPropertyAttribute validationPropertyAttribute = ((Attribute)obj) as ValidationPropertyAttribute;
				if (validationPropertyAttribute != null && validationPropertyAttribute.Name != null)
				{
					return properties[validationPropertyAttribute.Name];
				}
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001D3C RID: 7484 RVA: 0x00048E34 File Offset: 0x00047034
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.Validators.Add(this);
				page.GetValidators(this.ValidationGroup).Add(this);
			}
			base.OnInit(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x00048E70 File Offset: 0x00047070
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.pre_render_called = true;
			this.ControlPropertiesValid();
			this.render_uplevel = this.DetermineRenderUplevel();
			if (this.render_uplevel)
			{
				this.RegisterValidatorCommonScript();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001D3E RID: 7486 RVA: 0x00048EA4 File Offset: 0x000470A4
		protected internal override void OnUnload(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.Validators.Remove(this);
				if (!string.IsNullOrEmpty(this.ValidationGroup))
				{
					page.GetValidators(this.ValidationGroup).Remove(this);
				}
			}
			base.OnUnload(e);
		}

		/// <summary>Registers code on the page for client-side validation.</summary>
		// Token: 0x06001D3F RID: 7487 RVA: 0x00048EF0 File Offset: 0x000470F0
		protected void RegisterValidatorCommonScript()
		{
			Page page = this.Page;
			if (page != null)
			{
				if (page.ScriptManager != null)
				{
					page.ScriptManager.RegisterClientScriptResourceExternal(this, typeof(BaseValidator), "WebUIValidation_2.0.js");
					page.ScriptManager.RegisterClientScriptBlockExternal(this, typeof(BaseValidator), "ValidationInitializeScript", page.ValidationInitializeScript, true);
					page.ScriptManager.RegisterOnSubmitStatementExternal(this, typeof(BaseValidator), "ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
					page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), "ValidationStartupScript", page.ValidationStartupScript, true);
					return;
				}
				if (!page.ClientScript.IsClientScriptIncludeRegistered(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock"))
				{
					page.ClientScript.RegisterClientScriptInclude(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock", page.ClientScript.GetWebResourceUrl(typeof(BaseValidator), "WebUIValidation_2.0.js"));
					page.ClientScript.RegisterClientScriptBlock(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock.Initialize", page.ValidationInitializeScript, true);
					page.ClientScript.RegisterOnSubmitStatement(typeof(BaseValidator), "Mono-System.Web-ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
					page.ClientScript.RegisterStartupScript(typeof(BaseValidator), "Mono-System.Web-ValidationStartupScript", page.ValidationStartupScript, true);
				}
			}
		}

		/// <summary>Registers an ECMAScript array declaration using the array name Page_Validators.</summary>
		// Token: 0x06001D40 RID: 7488 RVA: 0x0004904C File Offset: 0x0004724C
		protected virtual void RegisterValidatorDeclaration()
		{
			Page page = this.Page;
			if (page != null)
			{
				if (page.ScriptManager != null)
				{
					page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_Validators", "document.getElementById ('" + this.ClientID + "')");
					page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), this.ClientID + "DisposeScript", string.Concat(new string[] { "\ndocument.getElementById('", this.ClientID, "').dispose = function() {\n    Array.remove(Page_Validators, document.getElementById('", this.ClientID, "'));\n}\n" }), true);
					return;
				}
				page.ClientScript.RegisterArrayDeclaration("Page_Validators", "document.getElementById ('" + this.ClientID + "')");
			}
		}

		/// <summary>Displays the control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001D41 RID: 7489 RVA: 0x0004911C File Offset: 0x0004731C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.IsEnabled && !this.EnableClientScript)
			{
				return;
			}
			if (this.render_uplevel)
			{
				this.RegisterValidatorDeclaration();
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool isValid = this.IsValid;
			if (!this.pre_render_called)
			{
				flag = true;
				flag2 = true;
			}
			else if (this.render_uplevel)
			{
				flag = true;
				flag2 = this.Display > ValidatorDisplay.None;
			}
			else if (this.Display != ValidatorDisplay.None)
			{
				flag = !isValid;
				flag2 = !isValid;
				flag3 = isValid && this.Display == ValidatorDisplay.Static;
			}
			if (flag)
			{
				this.AddAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			if (flag2 || flag3)
			{
				string text;
				if (flag2)
				{
					text = this.Text;
					if (string.IsNullOrEmpty(text))
					{
						text = this.ErrorMessage;
					}
				}
				else
				{
					text = "&nbsp;";
				}
				writer.Write(text);
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		/// <summary>Performs validation on the associated input control and updates the <see cref="P:System.Web.UI.WebControls.BaseValidator.IsValid" /> property.</summary>
		// Token: 0x06001D42 RID: 7490 RVA: 0x000491E8 File Offset: 0x000473E8
		public void Validate()
		{
			if (base.IsEnabled && this.Visible)
			{
				this.IsValid = this.ControlPropertiesValid() && this.EvaluateIsValid();
				return;
			}
			this.IsValid = true;
		}

		/// <summary>Gets a value that indicates whether the control generates unobtrusive JavaScript.</summary>
		/// <returns>true if the control generates unobtrusive JavaScript; otherwise, false.</returns>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0004921C File Offset: 0x0004741C
		protected bool IsUnobtrusive
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x04001820 RID: 6176
		private bool render_uplevel;

		// Token: 0x04001821 RID: 6177
		private bool valid;

		// Token: 0x04001822 RID: 6178
		private Color forecolor;

		// Token: 0x04001823 RID: 6179
		private bool pre_render_called;
	}
}
