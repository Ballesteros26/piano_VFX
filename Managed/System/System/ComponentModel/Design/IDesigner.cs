using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the basic framework for building a custom designer.</summary>
	// Token: 0x02000327 RID: 807
	[ComVisible(true)]
	public interface IDesigner : IDisposable
	{
		/// <summary>Gets the base component that this designer is designing.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> indicating the base component that this designer is designing.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001994 RID: 6548
		IComponent Component { get; }

		/// <summary>Gets a collection of the design-time verbs supported by the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the verbs supported by the designer, or null if the component has no verbs.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001995 RID: 6549
		DesignerVerbCollection Verbs { get; }

		/// <summary>Performs the default action for this designer.</summary>
		// Token: 0x06001996 RID: 6550
		void DoDefaultAction();

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The component to associate with this designer. </param>
		// Token: 0x06001997 RID: 6551
		void Initialize(IComponent component);
	}
}
