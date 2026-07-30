using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface that can look up information about the types used in the current Web Forms project.</summary>
	// Token: 0x02000097 RID: 151
	[Obsolete("Use new WebFormsReferenceManager feature")]
	public interface IWebFormReferenceManager
	{
		/// <summary>Gets the type of the specified object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object, if it could be resolved.</returns>
		/// <param name="tagPrefix">The tag prefix for the type. </param>
		/// <param name="typeName">The name of the type. </param>
		// Token: 0x06000492 RID: 1170
		Type GetObjectType(string tagPrefix, string typeName);

		/// <summary>Gets the register directives for the current project.</summary>
		/// <returns>The register directives for the current project.</returns>
		// Token: 0x06000493 RID: 1171
		string GetRegisterDirectives();

		/// <summary>Gets the tag prefix for the specified type of object.</summary>
		/// <returns>The tag prefix for the specified object type, if it could be located.</returns>
		/// <param name="objectType">The type of the object. </param>
		// Token: 0x06000494 RID: 1172
		string GetTagPrefix(Type objectType);
	}
}
