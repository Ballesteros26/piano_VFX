using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Performs user-defined validation on an input control.</summary>
	// Token: 0x0200036A RID: 874
	[DefaultEvent("ServerValidate")]
	[ToolboxData("<{0}:CustomValidator runat=\"server\" ErrorMessage=\"CustomValidator\"></{0}:CustomValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CustomValidator : BaseValidator
	{
		/// <summary>Occurs when validation is performed on the server.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060020C7 RID: 8391 RVA: 0x0005449A File Offset: 0x0005269A
		// (remove) Token: 0x060020C8 RID: 8392 RVA: 0x000544AD File Offset: 0x000526AD
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event ServerValidateEventHandler ServerValidate
		{
			add
			{
				this.events.AddHandler(CustomValidator.serverValidateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(CustomValidator.serverValidateEvent, value);
			}
		}

		/// <summary>Gets or sets the name of the custom client-side script function used for validation.</summary>
		/// <returns>The name of the custom client script function used for validation. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.NoteThe function name should not include any parentheses or parameters.</returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000544D3 File Offset: 0x000526D3
		// (set) Token: 0x060020CB RID: 8395 RVA: 0x000544EA File Offset: 0x000526EA
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public string ClientValidationFunction
		{
			get
			{
				return this.ViewState.GetString("ClientValidationFunction", string.Empty);
			}
			set
			{
				this.ViewState["ClientValidationFunction"] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether empty text should be validated.</summary>
		/// <returns>true if empty text should be validated; otherwise, false.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000544FD File Offset: 0x000526FD
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x00054510 File Offset: 0x00052710
		[DefaultValue(false)]
		[Themeable(false)]
		public bool ValidateEmptyText
		{
			get
			{
				return this.ViewState.GetBool("ValidateEmptyText", false);
			}
			set
			{
				this.ViewState["ValidateEmptyText"] = value;
			}
		}

		/// <summary>Adds the properties of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x060020CE RID: 8398 RVA: 0x00054528 File Offset: 0x00052728
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CustomValidatorEvaluateIsValid");
				if (this.ValidateEmptyText)
				{
					base.RegisterExpandoAttribute(this.ClientID, "validateemptytext", "true");
				}
				string clientValidationFunction = this.ClientValidationFunction;
				if (!string.IsNullOrEmpty(clientValidationFunction))
				{
					base.RegisterExpandoAttribute(this.ClientID, "clientvalidationfunction", clientValidationFunction, true);
				}
			}
		}

		/// <summary>Checks the properties of the control for valid values.</summary>
		/// <returns>true if the control properties are valid; otherwise, false.</returns>
		// Token: 0x060020CF RID: 8399 RVA: 0x0005459A File Offset: 0x0005279A
		protected override bool ControlPropertiesValid()
		{
			return string.IsNullOrEmpty(base.ControlToValidate) || base.ControlPropertiesValid();
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.MobileControls.BaseValidator.EvaluateIsValid" /> method.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x060020D0 RID: 8400 RVA: 0x000545B4 File Offset: 0x000527B4
		protected override bool EvaluateIsValid()
		{
			string controlToValidate = base.ControlToValidate;
			if (!string.IsNullOrEmpty(controlToValidate))
			{
				string controlValidationValue = base.GetControlValidationValue(controlToValidate);
				return (string.IsNullOrEmpty(controlValidationValue) && !this.ValidateEmptyText) || this.OnServerValidate(controlValidationValue);
			}
			return this.OnServerValidate(string.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event for the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control.</summary>
		/// <returns>true if the value specified by the <paramref name="value" /> parameter passes validation; otherwise, false.</returns>
		/// <param name="value">The value to validate. </param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x00054600 File Offset: 0x00052800
		protected virtual bool OnServerValidate(string value)
		{
			ServerValidateEventHandler serverValidateEventHandler = this.events[CustomValidator.serverValidateEvent] as ServerValidateEventHandler;
			if (serverValidateEventHandler != null)
			{
				ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(value, true);
				serverValidateEventHandler(this, serverValidateEventArgs);
				return serverValidateEventArgs.IsValid;
			}
			return true;
		}

		// Token: 0x040018AD RID: 6317
		private static readonly object serverValidateEvent = new object();

		// Token: 0x040018AE RID: 6318
		private EventHandlerList events = new EventHandlerList();
	}
}
