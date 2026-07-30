using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for nested containers, which logically contain zero or more other components and are owned by a parent component.</summary>
	// Token: 0x02000283 RID: 643
	public interface INestedContainer : IContainer, IDisposable
	{
		/// <summary>Gets the owning component for the nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns the nested container.</returns>
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600146C RID: 5228
		IComponent Owner { get; }
	}
}
