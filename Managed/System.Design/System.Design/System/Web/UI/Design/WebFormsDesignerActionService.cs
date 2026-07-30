using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	/// <summary>Establishes a design-time service that manages the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects for a component.</summary>
	// Token: 0x020000B9 RID: 185
	public class WebFormsDesignerActionService : DesignerActionService
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebFormsDesignerActionService" /> class using the provided reference to the design host.</summary>
		/// <param name="serviceProvider">A reference to the design host.</param>
		// Token: 0x0600055B RID: 1371 RVA: 0x000095C7 File Offset: 0x000077C7
		[MonoTODO]
		public WebFormsDesignerActionService(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the smart-tag item lists that are associated with a component.</summary>
		/// <param name="component">A reference to the control associated with the designer.</param>
		/// <param name="actionLists">The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to add the associated smart tags to.</param>
		// Token: 0x0600055C RID: 1372 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void GetComponentDesignerActions(IComponent component, DesignerActionListCollection actionLists)
		{
			throw new NotImplementedException();
		}
	}
}
