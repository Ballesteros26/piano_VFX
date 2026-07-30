using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when there is a syntax error in XML parsing. This class cannot be inherited.</summary>
	// Token: 0x02000553 RID: 1363
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with default properties.</summary>
		// Token: 0x06003D56 RID: 15702 RVA: 0x000D9764 File Offset: 0x000D7964
		public XmlSyntaxException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		// Token: 0x06003D57 RID: 15703 RVA: 0x000DCB0C File Offset: 0x000DAD0C
		public XmlSyntaxException(int lineNumber)
			: base(string.Format(Locale.GetText("Invalid syntax on line {0}."), lineNumber))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003D58 RID: 15704 RVA: 0x000DCB29 File Offset: 0x000DAD29
		public XmlSyntaxException(int lineNumber, string message)
			: base(string.Format(Locale.GetText("Invalid syntax on line {0} - {1}."), lineNumber, message))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003D59 RID: 15705 RVA: 0x000C7E43 File Offset: 0x000C6043
		public XmlSyntaxException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003D5A RID: 15706 RVA: 0x000C7E4C File Offset: 0x000C604C
		public XmlSyntaxException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
