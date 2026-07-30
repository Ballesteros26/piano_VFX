using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for containers. Containers are objects that logically contain zero or more components.</summary>
	// Token: 0x0200027C RID: 636
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		// Token: 0x06001452 RID: 5202
		void Add(IComponent component);

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list, and assigns a name to the component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null that leaves the component unnamed. </param>
		// Token: 0x06001453 RID: 5203
		void Add(IComponent component, string name);

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.IComponent" /> objects that represents all the components in the <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001454 RID: 5204
		ComponentCollection Components { get; }

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove. </param>
		// Token: 0x06001455 RID: 5205
		void Remove(IComponent component);
	}
}
