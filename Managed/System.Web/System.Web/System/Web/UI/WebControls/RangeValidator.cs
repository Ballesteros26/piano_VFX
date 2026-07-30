using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Checks whether the value of an input control is within a specified range of values.</summary>
	// Token: 0x020003FA RID: 1018
	[ToolboxData("<{0}:RangeValidator runat=\"server\" ErrorMessage=\"RangeValidator\"></{0}:RangeValidator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RangeValidator : BaseCompareValidator
	{
		/// <summary>Gets or sets the maximum value of the validation range.</summary>
		/// <returns>The maximum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000774C7 File Offset: 0x000756C7
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x000774DE File Offset: 0x000756DE
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public string MaximumValue
		{
			get
			{
				return this.ViewState.GetString("MaximumValue", string.Empty);
			}
			set
			{
				this.ViewState["MaximumValue"] = value;
			}
		}

		/// <summary>Gets or sets the minimum value of the validation range.</summary>
		/// <returns>The minimum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000774F1 File Offset: 0x000756F1
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x00077508 File Offset: 0x00075708
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Themeable(false)]
		public string MinimumValue
		{
			get
			{
				return this.ViewState.GetString("MinimumValue", string.Empty);
			}
			set
			{
				this.ViewState["MinimumValue"] = value;
			}
		}

		/// <summary>Adds the HTML attributes and styles for the control that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002D05 RID: 11525 RVA: 0x0007751C File Offset: 0x0007571C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RangeValidatorEvaluateIsValid");
				base.RegisterExpandoAttribute(this.ClientID, "minimumvalue", this.MinimumValue, true);
				base.RegisterExpandoAttribute(this.ClientID, "maximumvalue", this.MaximumValue, true);
			}
		}

		/// <summary>This is a check of properties to determine any errors made by the developer. </summary>
		/// <returns>true if the control properties are valid; otherwise, false.</returns>
		// Token: 0x06002D06 RID: 11526 RVA: 0x00077580 File Offset: 0x00075780
		protected override bool ControlPropertiesValid()
		{
			if (!BaseCompareValidator.CanConvert(this.MinimumValue, base.Type))
			{
				throw new HttpException("Minimum value cannot be converterd to type " + base.Type.ToString());
			}
			if (!BaseCompareValidator.CanConvert(this.MaximumValue, base.Type))
			{
				throw new HttpException("Maximum value cannot be converterd to type " + base.Type.ToString());
			}
			if (base.Type != ValidationDataType.String && BaseCompareValidator.Compare(this.MinimumValue, this.MaximumValue, ValidationCompareOperator.GreaterThan, base.Type))
			{
				throw new HttpException("Maximum value must be equal or bigger than Minimum value");
			}
			return base.ControlPropertiesValid();
		}

		/// <summary>Determines whether the content in the input control is valid.</summary>
		/// <returns>true if the control is valid; otherwise, false.</returns>
		// Token: 0x06002D07 RID: 11527 RVA: 0x00077630 File Offset: 0x00075830
		protected override bool EvaluateIsValid()
		{
			string text = base.GetControlValidationValue(base.ControlToValidate);
			if (text == null)
			{
				return true;
			}
			text = text.Trim();
			return text.Length == 0 || (BaseCompareValidator.Compare(text, this.MinimumValue, ValidationCompareOperator.GreaterThanEqual, base.Type) && BaseCompareValidator.Compare(text, this.MaximumValue, ValidationCompareOperator.LessThanEqual, base.Type));
		}
	}
}
