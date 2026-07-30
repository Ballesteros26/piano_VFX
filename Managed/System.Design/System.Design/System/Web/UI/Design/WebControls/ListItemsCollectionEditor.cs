using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a component editor for <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> objects in a control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> or a similar control.</summary>
	// Token: 0x02000196 RID: 406
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ListItemsCollectionEditor : CollectionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ListItemsCollectionEditor" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the collection to edit.</param>
		// Token: 0x06000B63 RID: 2915 RVA: 0x00009519 File Offset: 0x00007719
		public ListItemsCollectionEditor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
