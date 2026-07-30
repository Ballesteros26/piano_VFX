using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the template of the final step for creating a user account with the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
	// Token: 0x02000355 RID: 853
	[Browsable(false)]
	public sealed class CompleteWizardStep : TemplatedWizardStep
	{
		/// <summary>Gets or sets the type of user interface (UI) to display for the <see cref="T:System.Web.UI.WebControls.CompleteWizardStep" /> page of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>The <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> enumeration value for the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.CompleteWizardStep.StepType" /> property.</exception>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00008B66 File Offset: 0x00006D66
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x0005032B File Offset: 0x0004E52B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Filterable(false)]
		[Browsable(false)]
		[Themeable(false)]
		public override WizardStepType StepType
		{
			get
			{
				return WizardStepType.Complete;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the title to display for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control.</summary>
		/// <returns>The title to use for the final user account creation step of the <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> control. The default value is "Complete".</returns>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x00050334 File Offset: 0x0004E534
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x00050366 File Offset: 0x0004E566
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
				return global::Locale.GetText("Complete");
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
	}
}
