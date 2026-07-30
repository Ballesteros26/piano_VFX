using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Messaging.Design
{
	/// <summary>Provides basic design-time functionality for the <see cref="T:System.Messaging.Message" /> class.</summary>
	// Token: 0x020000E3 RID: 227
	public class MessageDesigner : ComponentDesigner
	{
		/// <summary>Modifies the set of properties that the designer exposes through the <see cref="T:System.ComponentModel.TypeDescriptor" /> class.</summary>
		/// <param name="properties">A <see cref="T:System.Collections.IDictionary" /> that contains the set of properties to filter for the component.</param>
		// Token: 0x06000699 RID: 1689 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
