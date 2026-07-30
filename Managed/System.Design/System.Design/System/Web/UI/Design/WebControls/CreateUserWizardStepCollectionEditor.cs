using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides an editor in a design host such as Visual Studio 2005 to edit the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> object of a <see cref="T:System.Web.UI.WebControls.CreateUserWizard" /> Web server control.</summary>
	// Token: 0x02000187 RID: 391
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CreateUserWizardStepCollectionEditor : WizardStepCollectionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.CreateUserWizardStepCollectionEditor" /> class using the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the items in the collection associated with the collection editor.</param>
		// Token: 0x06000B38 RID: 2872 RVA: 0x00009519 File Offset: 0x00007719
		public CreateUserWizardStepCollectionEditor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
