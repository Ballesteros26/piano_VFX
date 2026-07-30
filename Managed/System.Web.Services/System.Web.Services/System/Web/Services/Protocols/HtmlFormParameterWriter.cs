using System;
using System.IO;
using System.Net;
using System.Text;

namespace System.Web.Services.Protocols
{
	/// <summary>Writes outgoing request parameters for Web services implemented using HTTP with name-value pairs encoded like an HTML form rather than as a SOAP message.</summary>
	// Token: 0x0200002E RID: 46
	public class HtmlFormParameterWriter : UrlEncodedParameterWriter
	{
		/// <summary>Gets a value that indicates whether Web method parameter values are serialized to the outgoing HTTP request body.</summary>
		/// <returns>true to indicate that the Web method parameters are serialized. This property always returns true.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002B54 File Offset: 0x00000D54
		public override bool UsesWriteRequest
		{
			get
			{
				return true;
			}
		}

		/// <summary>Initializes the outgoing HTTP request. </summary>
		/// <param name="request">The outgoing request.</param>
		/// <param name="values">The Web method parameter values.</param>
		// Token: 0x06000104 RID: 260 RVA: 0x00004D49 File Offset: 0x00002F49
		public override void InitializeRequest(WebRequest request, object[] values)
		{
			request.ContentType = ContentType.Compose("application/x-www-form-urlencoded", this.RequestEncoding);
		}

		/// <summary>Serializes Web method parameter values into a stream representing the outgoing HTTP request body.</summary>
		/// <param name="requestStream">An input stream for the outgoing HTTP request's body.</param>
		/// <param name="values">The Web method parameter values.</param>
		// Token: 0x06000105 RID: 261 RVA: 0x00004D64 File Offset: 0x00002F64
		public override void WriteRequest(Stream requestStream, object[] values)
		{
			if (values.Length == 0)
			{
				return;
			}
			TextWriter textWriter = new StreamWriter(requestStream, new ASCIIEncoding());
			base.Encode(textWriter, values);
			textWriter.Flush();
		}
	}
}
