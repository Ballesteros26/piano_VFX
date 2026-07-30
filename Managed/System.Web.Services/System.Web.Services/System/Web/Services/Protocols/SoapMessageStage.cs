using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Specifies the processing stage of a SOAP message.</summary>
	// Token: 0x0200006F RID: 111
	public enum SoapMessageStage
	{
		/// <summary>The stage just prior to a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> being serialized.</summary>
		// Token: 0x0400029E RID: 670
		BeforeSerialize = 1,
		/// <summary>The stage just after a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is serialized, but before the SOAP message is sent over the wire.</summary>
		// Token: 0x0400029F RID: 671
		AfterSerialize,
		/// <summary>The stage just before a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is deserialized from the SOAP message sent across the network into an object.</summary>
		// Token: 0x040002A0 RID: 672
		BeforeDeserialize = 4,
		/// <summary>The stage just after a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is deserialized from a SOAP message into an object.</summary>
		// Token: 0x040002A1 RID: 673
		AfterDeserialize = 8
	}
}
