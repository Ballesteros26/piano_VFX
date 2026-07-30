using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base class for a data-binding handler.</summary>
	// Token: 0x02000061 RID: 97
	public abstract class DataBindingHandler
	{
		/// <summary>Binds the specified control.</summary>
		/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to bind. </param>
		// Token: 0x0600031A RID: 794
		public abstract void DataBindControl(IDesignerHost designerHost, Control control);
	}
}
