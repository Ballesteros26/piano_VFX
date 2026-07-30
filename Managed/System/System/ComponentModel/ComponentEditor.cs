using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the base class for a custom component editor.</summary>
	// Token: 0x02000233 RID: 563
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ComponentEditor
	{
		/// <summary>Edits the component and returns a value indicating whether the component was modified.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x06001232 RID: 4658 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		/// <summary>Edits the component and returns a value indicating whether the component was modified based upon a given context.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="context">An optional context object that can be used to obtain further information about the edit. </param>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x06001233 RID: 4659
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);
	}
}
