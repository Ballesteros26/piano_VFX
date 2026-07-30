using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>The exception that is thrown when a parse error occurs.</summary>
	// Token: 0x0200009D RID: 157
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class HttpParseException : HttpException
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0001149A File Offset: 0x0000F69A
		internal HttpParseException(string message, string virtualPath, int line)
			: base(message)
		{
			this.virtualPath = virtualPath;
			this.line = line;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000114BC File Offset: 0x0000F6BC
		private HttpParseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.line = info.GetInt32("_line");
			this.virtualPath = info.GetString("_virtualPath");
			this.errors = info.GetValue("_parserErrors", typeof(ParserErrorCollection)) as ParserErrorCollection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class.</summary>
		// Token: 0x06000777 RID: 1911 RVA: 0x0001151E File Offset: 0x0000F71E
		public HttpParseException()
			: this("External component has thrown an exception")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with a specified error message. </summary>
		/// <param name="message">The exception message to specify when the error occurs.</param>
		// Token: 0x06000778 RID: 1912 RVA: 0x0001152B File Offset: 0x0000F72B
		public HttpParseException(string message)
			: base(message)
		{
			this.errors.Add(new ParserError(message, null, 0));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with a specified error message and a reference to the inner. </summary>
		/// <param name="message">The exception message to specify when the error occurs.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06000779 RID: 1913 RVA: 0x00011553 File Offset: 0x0000F753
		public HttpParseException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.errors.Add(new ParserError(message, null, 0));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with specific information about the source code being compiled and the line number on which the exception occurred. </summary>
		/// <param name="message">The exception message to specify when the error occurs.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		/// <param name="virtualPath">The virtual path for the exception.</param>
		/// <param name="sourceCode">The source code being compiled when the exception occurs.</param>
		/// <param name="line">The line number on which the exception occurred.</param>
		// Token: 0x0600077A RID: 1914 RVA: 0x0001157C File Offset: 0x0000F77C
		public HttpParseException(string message, Exception innerException, string virtualPath, string sourceCode, int line)
			: base(message, innerException)
		{
			this.virtualPath = virtualPath;
			this.line = line;
			this.errors.Add(new ParserError(message, virtualPath, line));
		}

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x0600077B RID: 1915 RVA: 0x000115B5 File Offset: 0x0000F7B5
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_virtualPath", this.virtualPath);
			info.AddValue("_parserErrors", this.errors);
			info.AddValue("_line", this.line);
		}

		/// <summary>Gets the name of the file being parsed when the error occurs.</summary>
		/// <returns>The physical path to the source file that is being parsed when the error occurs; otherwise, null, if the physical path is null.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public string FileName
		{
			get
			{
				return this.virtualPath;
			}
		}

		/// <summary>Gets the number of the line being parsed when the error occurs.</summary>
		/// <returns>The number of the line being parsed when the error occurs. This value is 1-based, not 0-based.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x000115FA File Offset: 0x0000F7FA
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>Gets the virtual path to source file that generated the error.</summary>
		/// <returns>The virtual path to the source file that generated the error.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public string VirtualPath
		{
			get
			{
				return this.virtualPath;
			}
		}

		/// <summary>Gets the parser errors for the current exception.</summary>
		/// <returns>A collection of the parser errors for the current exception.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00011602 File Offset: 0x0000F802
		public ParserErrorCollection ParserErrors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x04000F6D RID: 3949
		private int line;

		// Token: 0x04000F6E RID: 3950
		private string virtualPath;

		// Token: 0x04000F6F RID: 3951
		private ParserErrorCollection errors = new ParserErrorCollection();
	}
}
