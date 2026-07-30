using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Provides a base class for accessing the types, directives, and controls in the current Web project document. This class must be inherited.</summary>
	// Token: 0x020000BA RID: 186
	public abstract class WebFormsReferenceManager
	{
		/// <summary>Gets the register directives for the current project document.</summary>
		/// <returns>A collection of strings representing the register directives defined in the current document.</returns>
		// Token: 0x0600055E RID: 1374
		public abstract ICollection GetRegisterDirectives();

		/// <summary>Gets the tag prefix for the specified object type.</summary>
		/// <returns>The tag prefix for the specified object type, if found; otherwise, null.</returns>
		/// <param name="objectType">The type of the object.</param>
		// Token: 0x0600055F RID: 1375
		public abstract string GetTagPrefix(Type objectType);

		/// <summary>Gets the object type with the specified tag prefix and tag name.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object with the specified tag prefix and name, if found; otherwise, null.</returns>
		/// <param name="tagPrefix">The tag prefix of the type to retrieve.</param>
		/// <param name="tagName">The tag name of the type to retrieve.</param>
		// Token: 0x06000560 RID: 1376
		public abstract Type GetType(string tagPrefix, string tagName);

		/// <summary>Gets the relative URL path for the user control with the specified tag prefix and tag name.</summary>
		/// <returns>A string representing the relative URL path for the specified user control, if found; otherwise, null.</returns>
		/// <param name="tagPrefix">The tag prefix of the user control to retrieve.</param>
		/// <param name="tagName">The tag name of the user control to retrieve.</param>
		// Token: 0x06000561 RID: 1377
		public abstract string GetUserControlPath(string tagPrefix, string tagName);

		/// <summary>Adds a tag prefix for the specified type.</summary>
		/// <returns>The tag prefix string.</returns>
		/// <param name="objectType">The type to add a tag prefix for in the current document.</param>
		// Token: 0x06000562 RID: 1378
		public abstract string RegisterTagPrefix(Type objectType);
	}
}
