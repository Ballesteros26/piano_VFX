using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for root-level designer view technologies.</summary>
	// Token: 0x02000336 RID: 822
	[ComVisible(true)]
	public interface IRootDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets the set of technologies that this designer can support for its display.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060019ED RID: 6637
		ViewTechnology[] SupportedTechnologies { get; }

		/// <summary>Gets a view object for the specified view technology.</summary>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		/// <exception cref="T:System.ArgumentException">The specified view technology is not supported or does not exist. </exception>
		// Token: 0x060019EE RID: 6638
		object GetView(ViewTechnology technology);
	}
}
