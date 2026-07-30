using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for an item that is retrieved at design time from a design host, such as Visual Studio 2005.</summary>
	// Token: 0x02000092 RID: 146
	public interface IProjectItem
	{
		/// <summary>Gets the URL for the item relative to the design host.</summary>
		/// <returns>The relative URL.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000477 RID: 1143
		string AppRelativeUrl { get; }

		/// <summary>Gets the name of the item.</summary>
		/// <returns>The name of the item.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000478 RID: 1144
		string Name { get; }

		/// <summary>Gets a reference to the containing item, if any.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IProjectItem" />, if the current item is contained within another item; otherwise, null.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000479 RID: 1145
		IProjectItem Parent { get; }

		/// <summary>Gets the path for a project item.</summary>
		/// <returns>The path for the item.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600047A RID: 1146
		string PhysicalPath { get; }
	}
}
