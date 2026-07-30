using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an editor control that enables end users to edit custom properties on an associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control. This class cannot be inherited.</summary>
	// Token: 0x020007B6 RID: 1974
	public sealed class PropertyGridEditorPart : EditorPart
	{
		/// <summary>Creates a new instance of the class.</summary>
		// Token: 0x06004FB1 RID: 20401 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PropertyGridEditorPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the values from a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control to the corresponding properties in the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the action of saving values from the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> is successful; otherwise (if an error occurs), false.</returns>
		/// <exception cref="T:System.Exception">An error occurred when trying to set the value for a property on the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.</exception>
		// Token: 0x06004FB2 RID: 20402 RVA: 0x000CB8F0 File Offset: 0x000C9AF0
		public override bool ApplyChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves the values from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, and updates the corresponding controls used to edit those property values within a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		// Token: 0x06004FB3 RID: 20403 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void SyncChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
