using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Enables a non-control component to emulate the data-binding behavior of a Windows Forms control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C2 RID: 450
	public interface IBindableComponent : IDisposable, IComponent
	{
		/// <summary>Gets or sets the collection of currency managers for the <see cref="T:System.Windows.Forms.IBindableComponent" />. </summary>
		/// <returns>The collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001DC9 RID: 7625
		// (set) Token: 0x06001DCA RID: 7626
		BindingContext BindingContext { get; set; }

		/// <summary>Gets the collection of data-binding objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> for this <see cref="T:System.Windows.Forms.IBindableComponent" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001DCB RID: 7627
		ControlBindingsCollection DataBindings { get; }
	}
}
