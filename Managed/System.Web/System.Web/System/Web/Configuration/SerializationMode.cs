using System;

namespace System.Web.Configuration
{
	/// <summary>Determines the serialization method used for a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
	// Token: 0x02000578 RID: 1400
	public enum SerializationMode
	{
		/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized to a simple string.</summary>
		// Token: 0x04002069 RID: 8297
		String,
		/// <summary>The profile <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> is serialized as XML using XML serialization.</summary>
		// Token: 0x0400206A RID: 8298
		Xml,
		/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized using binary serialization.</summary>
		// Token: 0x0400206B RID: 8299
		Binary,
		/// <summary>The provider has implicit knowledge of the type and is responsible for deciding how to serialize the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object into the data store.</summary>
		// Token: 0x0400206C RID: 8300
		ProviderSpecific
	}
}
