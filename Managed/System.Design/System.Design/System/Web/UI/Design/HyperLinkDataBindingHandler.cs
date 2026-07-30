using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Provides a data-binding handler for a hyperlink property.</summary>
	// Token: 0x02000081 RID: 129
	public class HyperLinkDataBindingHandler : DataBindingHandler
	{
		/// <summary>Resolves design-time data-binding for the specified control.</summary>
		/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control. </param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to data bind. </param>
		// Token: 0x06000423 RID: 1059 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			throw new NotImplementedException();
		}
	}
}
