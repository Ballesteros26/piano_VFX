using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Diagnostics.Design
{
	/// <summary>Base designer class for extending the design-mode behavior of a process.</summary>
	// Token: 0x020000E5 RID: 229
	public class ProcessDesigner : ComponentDesigner
	{
		/// <summary>Adjusts the set of properties the process exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
		// Token: 0x060006A0 RID: 1696 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
