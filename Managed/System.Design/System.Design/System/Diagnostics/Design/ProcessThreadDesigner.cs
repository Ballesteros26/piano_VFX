using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Diagnostics.Design
{
	/// <summary>Base designer class for extending the design-mode behavior of a process thread.</summary>
	// Token: 0x020000E7 RID: 231
	public class ProcessThreadDesigner : ComponentDesigner
	{
		/// <summary>Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
		// Token: 0x060006A4 RID: 1700 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
