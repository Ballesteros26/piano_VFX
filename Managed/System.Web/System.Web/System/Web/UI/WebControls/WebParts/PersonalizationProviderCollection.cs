using System;
using System.Configuration.Provider;
using System.Reflection;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Stores references to <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> objects indexed by name. This class cannot be inherited. </summary>
	// Token: 0x020007B5 RID: 1973
	[DefaultMember("Item")]
	public sealed class PersonalizationProviderCollection : ProviderCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection" /> class. </summary>
		// Token: 0x06004FAF RID: 20399 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PersonalizationProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Copies the personalization providers in the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The array to which the personalization providers are copied.</param>
		/// <param name="index">The location in the array at which to begin copying.</param>
		// Token: 0x06004FB0 RID: 20400 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(PersonalizationProvider[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
