using System;
using System.IO;
using System.Net;
using System.Text;

namespace System.Web.Services.Protocols
{
	/// <summary>Provides a common base implementation for writers of out-going request parameters for Web service clients implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000043 RID: 67
	public abstract class MimeParameterWriter : MimeFormatter
	{
		/// <summary>Gets a value that indicates whether Web method parameter values are serialized to the out-going HTTP request body.</summary>
		/// <returns>true if the Web method parameter values are serialized to the out-going HTTP request body; otherwise false.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00002B51 File Offset: 0x00000D51
		public virtual bool UsesWriteRequest
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the encoding used to write parameters to the HTTP request.</summary>
		/// <returns>The encoding used to write parameters to the HTTP request.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006C2F File Offset: 0x00004E2F
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000210D File Offset: 0x0000030D
		public virtual Encoding RequestEncoding
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>When overridden in a derived class, modifies the outgoing HTTP request's Uniform Request Locator (URL).</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the modified, outgoing HTTP request's Uniform Request Locator (URL).</returns>
		/// <param name="url">The HTTP request's original Uniform Resource Locator (URL).</param>
		/// <param name="parameters">The Web method parameter values to be added to the URL, if necessary.</param>
		// Token: 0x06000178 RID: 376 RVA: 0x00006C32 File Offset: 0x00004E32
		public virtual string GetRequestUrl(string url, object[] parameters)
		{
			return url;
		}

		/// <summary>When overridden in a derived class, initializes the out-going HTTP request.</summary>
		/// <param name="request">The out-going request, where the <see cref="T:System.Net.WebRequest" /> class allows transport protocols besides HTTP.</param>
		/// <param name="values">The Web method parameter values.</param>
		// Token: 0x06000179 RID: 377 RVA: 0x0000210D File Offset: 0x0000030D
		public virtual void InitializeRequest(WebRequest request, object[] values)
		{
		}

		/// <summary>When overridden in a derived class, serializes Web method parameter values into a stream representing the outgoing HTTP request body.</summary>
		/// <param name="requestStream">An input stream for the outgoing HTTP request's body.</param>
		/// <param name="values">The Web method parameter values.</param>
		// Token: 0x0600017A RID: 378 RVA: 0x0000210D File Offset: 0x0000030D
		public virtual void WriteRequest(Stream requestStream, object[] values)
		{
		}
	}
}
