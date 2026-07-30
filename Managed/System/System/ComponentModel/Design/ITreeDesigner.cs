using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for building a set of related custom designers.</summary>
	// Token: 0x02000339 RID: 825
	public interface ITreeDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets a collection of child designers.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" />, containing the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> child objects of the current designer. </returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060019FF RID: 6655
		ICollection Children { get; }

		/// <summary>Gets the parent designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or null if there is no parent.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A00 RID: 6656
		IDesigner Parent { get; }
	}
}
