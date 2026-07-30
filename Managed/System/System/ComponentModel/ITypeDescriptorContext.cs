using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides contextual information about a component, such as its container and property descriptor.</summary>
	// Token: 0x0200028E RID: 654
	[ComVisible(true)]
	public interface ITypeDescriptorContext : IServiceProvider
	{
		/// <summary>Gets the container representing this <see cref="T:System.ComponentModel.TypeDescriptor" /> request.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IContainer" /> with the set of objects for this <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no container or if the <see cref="T:System.ComponentModel.TypeDescriptor" /> does not use outside objects.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001486 RID: 5254
		IContainer Container { get; }

		/// <summary>Gets the object that is connected with this type descriptor request.</summary>
		/// <returns>The object that invokes the method on the <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no object responsible for the call.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001487 RID: 5255
		object Instance { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with the given context item.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the given context item; otherwise, null if there is no <see cref="T:System.ComponentModel.PropertyDescriptor" /> responsible for the call.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001488 RID: 5256
		PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
		/// <returns>true if this object can be changed; otherwise, false.</returns>
		// Token: 0x06001489 RID: 5257
		bool OnComponentChanging();

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
		// Token: 0x0600148A RID: 5258
		void OnComponentChanged();
	}
}
