using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.Design
{
	/// <summary>Provides a UI handler for data binding values.</summary>
	// Token: 0x02000062 RID: 98
	public class DataBindingValueUIHandler
	{
		/// <summary>Adds a data binding for the specified property and the specified value item list, if the current control has data bindings and the current object does not already have a binding.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can provide additional context information. </param>
		/// <param name="propDesc">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property to add a data binding for. </param>
		/// <param name="valueUIItemList">An <see cref="T:System.Collections.ArrayList" /> of items that have data bindings. </param>
		// Token: 0x0600031C RID: 796 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public void OnGetUIValueItem(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList)
		{
			throw new NotImplementedException();
		}
	}
}
