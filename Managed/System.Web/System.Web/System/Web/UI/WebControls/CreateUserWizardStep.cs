using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Contains basic functionality for creating a user in a step that can be templated. This class cannot be inherited.</summary>
	// Token: 0x02000369 RID: 873
	[Browsable(false)]
	public sealed class CreateUserWizardStep : TemplatedWizardStep
	{
		/// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>true if the user is allowed to return to the <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> step; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the property.</exception>
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0005443D File Offset: 0x0005263D
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x00054450 File Offset: 0x00052650
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowReturn
		{
			get
			{
				return this.ViewState.GetBool("AllowReturn", false);
			}
			set
			{
				this.ViewState["AllowReturn"] = value;
			}
		}

		/// <summary>Gets or sets the title to use for the user-account-creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. </summary>
		/// <returns>The title to use for the user-account-creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Sign up for your new account." The default text for the control is localized based on the server's current locale.</returns>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00054468 File Offset: 0x00052668
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x00050366 File Offset: 0x0004E566
		[Localizable(true)]
		public override string Title
		{
			get
			{
				object obj = this.ViewState["TitleText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Sign Up for Your New Account");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("TitleText");
					return;
				}
				this.ViewState["TitleText"] = value;
			}
		}

		/// <summary>Gets or sets the type of user interface (UI) to display for the <see cref="T:System.Web.UI.WebControls.CreateUserWizardStep" /> step of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>The <see cref="F:System.Web.UI.WebControls.WizardStepType.Auto" /> enumeration value of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.CreateUserWizardStep.StepType" /> property to a value other than the WizardStepType.Auto enumeration value.</exception>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0005032B File Offset: 0x0004E52B
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Themeable(false)]
		public override WizardStepType StepType
		{
			get
			{
				return WizardStepType.Auto;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}
	}
}
