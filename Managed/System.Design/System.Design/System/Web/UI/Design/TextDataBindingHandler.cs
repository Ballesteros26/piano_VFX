using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a data-binding handler for a data-bound control at design time.</summary>
	// Token: 0x020000AB RID: 171
	public class TextDataBindingHandler : DataBindingHandler
	{
		/// <summary>Data-binds the specified control.</summary>
		/// <param name="designerHost">An object implementing <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to data-bind. </param>
		// Token: 0x0600052A RID: 1322 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			throw new NotImplementedException();
		}
	}
}
