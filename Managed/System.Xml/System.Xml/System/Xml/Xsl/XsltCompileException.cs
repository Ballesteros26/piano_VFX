using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown by the Load method when an error is found in the XSLT style sheet.</summary>
	// Token: 0x020004E1 RID: 1249
	[Serializable]
	public class XsltCompileException : XsltException
	{
		/// <summary>Initializes a new instance of the XsltCompileException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XsltCompileException. </param>
		/// <param name="context">The StreamingContext object containing the context information. </param>
		// Token: 0x060032E3 RID: 13027 RVA: 0x001230B7 File Offset: 0x001212B7
		protected XsltCompileException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Streams all the XsltCompileException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060032E4 RID: 13028 RVA: 0x00124970 File Offset: 0x00122B70
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class.</summary>
		// Token: 0x060032E5 RID: 13029 RVA: 0x0012497A File Offset: 0x00122B7A
		public XsltCompileException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060032E6 RID: 13030 RVA: 0x00124982 File Offset: 0x00122B82
		public XsltCompileException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified. </param>
		// Token: 0x060032E7 RID: 13031 RVA: 0x0012498B File Offset: 0x00122B8B
		public XsltCompileException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the XsltCompileException class.</summary>
		/// <param name="inner">The <see cref="T:System.Exception" /> that threw the XsltCompileException. </param>
		/// <param name="sourceUri">The location path of the style sheet. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred in the style sheet. </param>
		/// <param name="linePosition">The line position indicating where the error occurred in the style sheet. </param>
		// Token: 0x060032E8 RID: 13032 RVA: 0x00124998 File Offset: 0x00122B98
		public XsltCompileException(Exception inner, string sourceUri, int lineNumber, int linePosition)
			: base((lineNumber != 0) ? "XSLT compile error at {0}({1},{2}). See InnerException for details." : "XSLT compile error.", new string[]
			{
				sourceUri,
				lineNumber.ToString(CultureInfo.InvariantCulture),
				linePosition.ToString(CultureInfo.InvariantCulture)
			}, sourceUri, lineNumber, linePosition, inner)
		{
		}
	}
}
