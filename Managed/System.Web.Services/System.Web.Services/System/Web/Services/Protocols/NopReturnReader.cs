using System;
using System.IO;
using System.Net;

namespace System.Web.Services.Protocols
{
	/// <summary>Serves as a non-acting reader of incoming response return values for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000046 RID: 70
	public class NopReturnReader : MimeReturnReader
	{
		/// <summary>Returns an initializer for the specified method.</summary>
		/// <returns>An initializer for the specified method.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
		// Token: 0x06000180 RID: 384 RVA: 0x00002B14 File Offset: 0x00000D14
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return this;
		}

		/// <summary>Initializes an instance.</summary>
		/// <param name="initializer">Another instance of the <see cref="T:System.Web.Services.Protocols.NopReturnReader" /> class, on which the <see cref="M:System.Web.Services.Protocols.NopReturnReader.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo)" /> method was previously called.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x0000210D File Offset: 0x0000030D
		public override void Initialize(object initializer)
		{
		}

		/// <summary>Returns null instead of deserializing the HTTP response stream into a Web method return value.</summary>
		/// <returns>null.</returns>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object  containing the output message for an operation.</param>
		/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
		// Token: 0x06000182 RID: 386 RVA: 0x00006C35 File Offset: 0x00004E35
		public override object Read(WebResponse response, Stream responseStream)
		{
			response.Close();
			return null;
		}
	}
}
