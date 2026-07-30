using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls or other server controls to contain collections of verbs.</summary>
	// Token: 0x02000464 RID: 1124
	public interface IWebActionable
	{
		/// <summary>Gets a reference to a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</returns>
		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060033EA RID: 13290
		WebPartVerbCollection Verbs { get; }
	}
}
