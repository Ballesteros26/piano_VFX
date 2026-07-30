using System;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Specifies whether the import is made to the server or to the client computer.</summary>
	// Token: 0x0200010E RID: 270
	public enum ServiceDescriptionImportStyle
	{
		/// <summary>Specifies that the import should be made to the client computer.</summary>
		// Token: 0x0400042C RID: 1068
		[XmlEnum("client")]
		Client,
		/// <summary>Specifies that the import should be made to the server.</summary>
		// Token: 0x0400042D RID: 1069
		[XmlEnum("server")]
		Server,
		/// <summary>Specifies that the import should be made to a server interface.</summary>
		// Token: 0x0400042E RID: 1070
		[XmlEnum("serverInterface")]
		ServerInterface
	}
}
