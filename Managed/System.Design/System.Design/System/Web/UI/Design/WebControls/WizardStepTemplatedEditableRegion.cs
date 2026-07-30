using System;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Defines an editable region of content within the design-time markup of a template in a wizard step for a <see cref="T:System.Web.UI.Design.WebControls.WizardDesigner" />.</summary>
	// Token: 0x020001BA RID: 442
	public class WizardStepTemplatedEditableRegion : TemplatedEditableDesignerRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.WizardStepTemplatedEditableRegion" /> class.</summary>
		/// <param name="templateDefinition">A <see cref="T:System.Web.UI.Design.TemplateDefinition" /> object that defines a template element in a wizard step at design time.</param>
		/// <param name="wizardStep">A <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object that represents a step displayed in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</param>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x00009519 File Offset: 0x00007719
		public WizardStepTemplatedEditableRegion(TemplateDefinition templateDefinition, WizardStepBase wizardStep)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the associated design-time wizard step.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0000970B File Offset: 0x0000790B
		public WizardStepBase Step
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
