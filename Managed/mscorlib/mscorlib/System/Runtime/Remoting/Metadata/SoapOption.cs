using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Specifies the SOAP configuration options for use with the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> class.</summary>
	// Token: 0x020007CA RID: 1994
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum SoapOption
	{
		/// <summary>The default option indicating that no extra options are selected.</summary>
		// Token: 0x04002A82 RID: 10882
		None = 0,
		/// <summary>Indicates that type will always be included on SOAP elements. This option is useful when performing SOAP interop with SOAP implementations that require types on all elements.</summary>
		// Token: 0x04002A83 RID: 10883
		AlwaysIncludeTypes = 1,
		/// <summary>Indicates that the output SOAP string type in a SOAP Envelope is using the XSD prefix, and that the resulting XML does not have an ID attribute for the string.</summary>
		// Token: 0x04002A84 RID: 10884
		XsdString = 2,
		/// <summary>Indicates that SOAP will be generated without references. This option is currently not implemented.</summary>
		// Token: 0x04002A85 RID: 10885
		EmbedAll = 4,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x04002A86 RID: 10886
		Option1 = 8,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x04002A87 RID: 10887
		Option2 = 16
	}
}
