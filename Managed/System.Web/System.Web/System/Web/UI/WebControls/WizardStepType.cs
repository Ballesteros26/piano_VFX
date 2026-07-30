using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the types of navigation UI that can be displayed for a step in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	// Token: 0x0200032D RID: 813
	public enum WizardStepType
	{
		/// <summary>The navigation UI that is rendered for the step is determined automatically by the order in which the step is declared.</summary>
		// Token: 0x040017DF RID: 6111
		Auto,
		/// <summary>The step is the last one to appear. No navigation buttons are rendered.</summary>
		// Token: 0x040017E0 RID: 6112
		Complete,
		/// <summary>The step is the final data collection step. Finish and Previous buttons are rendered for navigation.</summary>
		// Token: 0x040017E1 RID: 6113
		Finish,
		/// <summary>The step is the first one to appear. A Next button is rendered but a Previous button is not rendered for this step.</summary>
		// Token: 0x040017E2 RID: 6114
		Start,
		/// <summary>The step is any step between the Start and the Finish steps. Previous and Next buttons are rendered for navigation. This step type is useful for overriding the <see cref="F:System.Web.UI.WebControls.WizardStepType.Auto" /> step type.</summary>
		// Token: 0x040017E3 RID: 6115
		Step
	}
}
