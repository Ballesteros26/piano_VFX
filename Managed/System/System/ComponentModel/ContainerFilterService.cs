using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x0200024B RID: 587
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		/// <param name="components">The component collection to filter.</param>
		// Token: 0x060012F4 RID: 4852 RVA: 0x0000206B File Offset: 0x0000026B
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
