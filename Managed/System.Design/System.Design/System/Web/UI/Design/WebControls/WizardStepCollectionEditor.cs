using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time editor in a design host such as Visual Studio 2005 for a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" />.</summary>
	// Token: 0x02000188 RID: 392
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WizardStepCollectionEditor : CollectionEditor
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.WizardStepCollectionEditor" /> class using the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of items in the collection.</param>
		// Token: 0x06000B39 RID: 2873 RVA: 0x00009519 File Offset: 0x00007719
		public WizardStepCollectionEditor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
