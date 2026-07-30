using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for access to a master page from a content page at design time, if provided by a design host, such as Visual Studio 2005. </summary>
	// Token: 0x02000082 RID: 130
	public interface IContentResolutionService
	{
		/// <summary>Retrieves the current state of the identified content place holder.</summary>
		/// <returns>The current state of the identified content placeholder.</returns>
		/// <param name="identifier">The identifier for a content place holder.</param>
		// Token: 0x06000424 RID: 1060
		ContentDesignerState GetContentDesignerState(string identifier);

		/// <summary>Sets the current state of the identified content place holder.</summary>
		/// <param name="identifier">The identifier for a content place holder.</param>
		/// <param name="state">A <see cref="T:System.Web.UI.Design.ContentDesignerState" />.</param>
		// Token: 0x06000425 RID: 1061
		void SetContentDesignerState(string identifier, ContentDesignerState state);

		/// <summary>Gets the <see cref="T:System.Web.UI.Design.ContentDefinition" /> objects for the content placeholders that are identified in the master page.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing identifiers and <see cref="T:System.Web.UI.Design.ContentDefinition" /> objects.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000426 RID: 1062
		IDictionary ContentDefinitions { get; }
	}
}
