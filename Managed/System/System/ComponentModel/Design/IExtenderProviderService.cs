using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x02000330 RID: 816
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add. </param>
		// Token: 0x060019D3 RID: 6611
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove. </param>
		// Token: 0x060019D4 RID: 6612
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
