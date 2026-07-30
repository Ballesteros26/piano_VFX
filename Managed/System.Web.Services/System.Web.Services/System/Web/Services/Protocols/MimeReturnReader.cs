using System;
using System.IO;
using System.Net;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides a common base implementation for readers of incoming response return values for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000044 RID: 68
	public abstract class MimeReturnReader : MimeFormatter
	{
		/// <summary>When overridden in a derived class, deserializes an HTTP response into a Web method return value.</summary>
		/// <returns>An HTTP response deserialized into a Web method return value.</returns>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object containing the output message for an operation.</param>
		/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <see cref="T:System.Net.WebResponse" /> parameter.</param>
		// Token: 0x0600017C RID: 380
		public abstract object Read(WebResponse response, Stream responseStream);
	}
}
