using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Validates whether the value of an associated input control matches the pattern specified by a regular expression.</summary>
	// Token: 0x020003FC RID: 1020
	[ToolboxData("<{0}:RegularExpressionValidator runat=\"server\" ErrorMessage=\"RegularExpressionValidator\"></{0}:RegularExpressionValidator>")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RegularExpressionValidator : BaseValidator
	{
		/// <summary>Adds to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object the HTML attributes and styles that need to be rendered for the control. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002D14 RID: 11540 RVA: 0x00077810 File Offset: 0x00075A10
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel)
			{
				base.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "RegularExpressionValidatorEvaluateIsValid");
				if (this.ValidationExpression.Length > 0)
				{
					base.RegisterExpandoAttribute(this.ClientID, "validationexpression", this.ValidationExpression, true);
				}
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Indicates whether the value in the input control is valid.</summary>
		/// <returns>true if the value in the input control is valid; otherwise, false.</returns>
		// Token: 0x06002D15 RID: 11541 RVA: 0x00077868 File Offset: 0x00075A68
		protected override bool EvaluateIsValid()
		{
			if (base.GetControlValidationValue(base.ControlToValidate).Trim() == "")
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(this.ValidationExpression);
			if (stringBuilder.Length == 0 || stringBuilder[0] != '^')
			{
				stringBuilder.Insert(0, '^');
			}
			if (stringBuilder[stringBuilder.Length - 1] != '$')
			{
				stringBuilder.Append('$');
			}
			return Regex.IsMatch(base.GetControlValidationValue(base.ControlToValidate), stringBuilder.ToString());
		}

		/// <summary>Gets or sets the regular expression that determines the pattern used to validate a field.</summary>
		/// <returns>A string that specifies the regular expression used to validate a field for format. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Web.HttpException">The regular expression is not properly formed. </exception>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000778EF File Offset: 0x00075AEF
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x00077906 File Offset: 0x00075B06
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string ValidationExpression
		{
			get
			{
				return this.ViewState.GetString("ValidationExpression", "");
			}
			set
			{
				this.ViewState["ValidationExpression"] = value;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x0007791C File Offset: 0x00075B1C
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int? MatchTimeout
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
