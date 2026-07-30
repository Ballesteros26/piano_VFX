using System;
using System.IO;
using System.Net;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides a minimal reader of incoming response return values for Web service clients implemented using HTTP but without SOAP. </summary>
	// Token: 0x02000019 RID: 25
	public class AnyReturnReader : MimeReturnReader
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Protocols.AnyReturnReader" /> class.</summary>
		/// <param name="o">Another instance of the <see cref="T:System.Web.Services.Protocols.AnyReturnReader" /> class, on which the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo)" /> method was previously called.</param>
		// Token: 0x06000058 RID: 88 RVA: 0x0000210D File Offset: 0x0000030D
		public override void Initialize(object o)
		{
		}

		/// <summary>Returns the parameter passed to the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.Initialize(System.Object)" /> method.</summary>
		/// <returns>The parameter passed to the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.Initialize(System.Object)" /> method.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />  that indicates the Web method for which the initializer is obtained.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x00002B17 File Offset: 0x00000D17
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			if (methodInfo.IsVoid)
			{
				return null;
			}
			return this;
		}

		/// <summary>Returns the input HTTP response stream.</summary>
		/// <returns>The input HTTP response stream.</returns>
		/// <param name="response">A representation of the HTTP response sent by a Web service, containing the output message for an operation.</param>
		/// <param name="responseStream">An output stream whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
		// Token: 0x0600005A RID: 90 RVA: 0x00002B24 File Offset: 0x00000D24
		public override object Read(WebResponse response, Stream responseStream)
		{
			return responseStream;
		}
	}
}
