using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.CurrentStepIndex" /> property and the <see cref="P:System.Web.UI.WebControls.WizardNavigationEventArgs.NextStepIndex" /> property for navigation in wizard controls.</summary>
	// Token: 0x0200044C RID: 1100
	public class WizardNavigationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> class.</summary>
		/// <param name="currentStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</param>
		/// <param name="nextStepIndex">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</param>
		// Token: 0x060032FB RID: 13051 RVA: 0x000893A9 File Offset: 0x000875A9
		public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
		{
			this.curStepIndex = currentStepIndex;
			this.nxtStepIndex = nextStepIndex;
			this.cancel = false;
		}

		/// <summary>Gets or sets a value indicating whether the navigation to the next step in the wizard should be canceled.</summary>
		/// <returns>true if the navigation to the next step should be canceled; otherwise, false. The default value is false.</returns>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000893C6 File Offset: 0x000875C6
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000893CE File Offset: 0x000875CE
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000893D7 File Offset: 0x000875D7
		public int CurrentStepIndex
		{
			get
			{
				return this.curStepIndex;
			}
		}

		/// <summary>Gets a value that represents the index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</summary>
		/// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object that the <see cref="T:System.Web.UI.WebControls.Wizard" /> control will display next.</returns>
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000893DF File Offset: 0x000875DF
		public int NextStepIndex
		{
			get
			{
				return this.nxtStepIndex;
			}
		}

		// Token: 0x04001CBC RID: 7356
		private int curStepIndex;

		// Token: 0x04001CBD RID: 7357
		private int nxtStepIndex;

		// Token: 0x04001CBE RID: 7358
		private bool cancel;
	}
}
