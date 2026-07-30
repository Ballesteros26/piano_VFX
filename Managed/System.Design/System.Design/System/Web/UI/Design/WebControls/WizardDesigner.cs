using System;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	// Token: 0x02000186 RID: 390
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WizardDesigner : CompositeControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.WizardDesigner" /> class.</summary>
		// Token: 0x06000B31 RID: 2865 RVA: 0x00009519 File Offset: 0x00007719
		public WizardDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a property in the associated wizard control indicating whether to display a sidebar.</summary>
		/// <returns>true to display the sidebar; otherwise, false.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x000166B4 File Offset: 0x000148B4
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00009519 File Offset: 0x00007719
		protected bool DisplaySideBar
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Adds the provided designer region collection to the current designer regions.</summary>
		/// <param name="regions">A <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object containing the regions to add.</param>
		// Token: 0x06000B34 RID: 2868 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void AddDesignerRegions(DesignerRegionCollection regions)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a navigation template from the active step and saves it in the <see cref="P:System.Web.UI.WebControls.TemplatedWizardStep.CustomNavigationTemplate" /> property of the active step.</summary>
		// Token: 0x06000B35 RID: 2869 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void ConvertToCustomNavigationTemplate()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Converts a selection of controls to a template in the associated control.</summary>
		/// <param name="description">A description of the effect of allowing the transaction to complete, which is used by the design host to give the user an opportunity to cancel the operation.</param>
		/// <param name="component">The wizard control associated with this designer.</param>
		/// <param name="templateName">The name of the template to convert to.</param>
		/// <param name="keys">An array of IDs for the controls that are to be included in the template.</param>
		// Token: 0x06000B36 RID: 2870 RVA: 0x00009519 File Offset: 0x00007719
		protected void ConvertToTemplate(string description, IComponent component, string templateName, string[] keys)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Resets the specified template to its default value.</summary>
		/// <param name="description">A description of the effect of allowing the transaction to complete, which is used by the design host to give the user an opportunity to cancel the operation.</param>
		/// <param name="component">The <see cref="T:System.Web.UI.WebControls.Wizard" /> control associated with this designer.</param>
		/// <param name="templateName">The name of the template to reset.</param>
		// Token: 0x06000B37 RID: 2871 RVA: 0x00009519 File Offset: 0x00007719
		protected void ResetTemplate(string description, IComponent component, string templateName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
