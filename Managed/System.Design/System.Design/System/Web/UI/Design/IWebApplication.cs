using System;
using System.Configuration;
using System.Runtime.InteropServices;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for accessing a Web application in a design host, such as Microsoft Visual Studio 2005, at design time.</summary>
	// Token: 0x02000096 RID: 150
	[Guid("cff39fa8-5607-4b6d-86f3-cc80b3cfe2dd")]
	public interface IWebApplication : IServiceProvider
	{
		/// <summary>Returns a project item from a design host based on its URL.</summary>
		/// <returns>A project item from a design host based on its URL.</returns>
		/// <param name="appRelativeUrl">The relative path to the project item to retrieve.</param>
		// Token: 0x0600048F RID: 1167
		IProjectItem GetProjectItemFromUrl(string appRelativeUrl);

		/// <summary>Returns a <see cref="T:System.Configuration.Configuration" /> object representing the current Web application in the design host.</summary>
		/// <returns>An object representing the current Web application in the design host.</returns>
		/// <param name="isReadOnly">true to indicate the returned <see cref="T:System.Configuration.Configuration" /> is editable; otherwise, false.</param>
		// Token: 0x06000490 RID: 1168
		Configuration OpenWebConfiguration(bool isReadOnly);

		/// <summary>Gets the root project item from the design host.</summary>
		/// <returns>The root project item from the design host.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000491 RID: 1169
		IProjectItem RootProjectItem { get; }
	}
}
