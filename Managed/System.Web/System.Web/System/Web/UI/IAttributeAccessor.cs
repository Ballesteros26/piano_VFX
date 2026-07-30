using System;

namespace System.Web.UI
{
	/// <summary>Defines methods used by ASP.NET server controls to provide programmatic access to any attribute declared in the opening tag of a server control.</summary>
	// Token: 0x02000166 RID: 358
	public interface IAttributeAccessor
	{
		/// <summary>When implemented by a class, retrieves the specified attribute property from the server control.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="key">A <see cref="T:System.String" /> that represents the name of the server control attribute. </param>
		// Token: 0x06000F50 RID: 3920
		string GetAttribute(string key);

		/// <summary>When implemented by a class, designates an attribute and its value to assign to the ASP.NET server control.</summary>
		/// <param name="key">The name of the attribute to be set. </param>
		/// <param name="value">The value assigned to the attribute. </param>
		// Token: 0x06000F51 RID: 3921
		void SetAttribute(string key, string value);
	}
}
