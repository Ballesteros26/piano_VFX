using System;

namespace System.Drawing.Design
{
	/// <summary>Exposes a collection of toolbox items.</summary>
	// Token: 0x0200011D RID: 285
	public interface IToolboxItemProvider
	{
		/// <summary>Gets a collection of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects.</summary>
		/// <returns>A collection of <see cref="T:System.Drawing.Design.ToolboxItem" /> objects.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D46 RID: 3398
		ToolboxItemCollection Items { get; }
	}
}
