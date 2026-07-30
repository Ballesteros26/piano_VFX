using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Makes the associated input control a required field.</summary>
	// Token: 0x02000401 RID: 1025
	[ToolboxData("<{0}:RequiredFieldValidator runat=\"server\" ErrorMessage=\"RequiredFieldValidator\"></{0}:RequiredFieldValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RequiredFieldValidator : BaseValidator
	{
		/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002D83 RID: 11651 RVA: 0x00078B36 File Offset: 0x00076D36
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RequiredFieldValidatorEvaluateIsValid");
				base.RegisterExpandoAttribute(this.ClientID, "initialvalue", this.InitialValue, true);
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Called during the validation stage when ASP.NET processes a Web Form.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06002D84 RID: 11652 RVA: 0x00078B75 File Offset: 0x00076D75
		protected override bool EvaluateIsValid()
		{
			return base.GetControlValidationValue(base.ControlToValidate) != this.InitialValue;
		}

		/// <summary>Gets or sets the initial value of the associated input control.</summary>
		/// <returns>A string that specifies the initial value of the associated input control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x00078B8E File Offset: 0x00076D8E
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x00078BA5 File Offset: 0x00076DA5
		[Themeable(false)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public string InitialValue
		{
			get
			{
				return this.ViewState.GetString("InitialValue", "");
			}
			set
			{
				this.ViewState["InitialValue"] = value;
			}
		}
	}
}
