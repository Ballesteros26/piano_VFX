using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web
{
	/// <summary>Describes an exception that occurred during the processing of HTTP requests.</summary>
	// Token: 0x02000093 RID: 147
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public class HttpException : ExternalException
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000104F0 File Offset: 0x0000E6F0
		private ExceptionPageTemplate PageTemplate
		{
			get
			{
				if (this.pageTemplate == null)
				{
					this.pageTemplate = this.GetPageTemplate();
				}
				return this.pageTemplate;
			}
		}

		/// <summary>Gets the event codes that are associated with the HTTP exception.</summary>
		/// <returns>An integer representing a Web event code.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001050C File Offset: 0x0000E70C
		public int WebEventCode
		{
			get
			{
				return this.webEventCode;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class and creates an empty <see cref="T:System.Web.HttpException" /> object.</summary>
		// Token: 0x06000716 RID: 1814 RVA: 0x00010514 File Offset: 0x0000E714
		public HttpException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using a supplied error message.</summary>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		// Token: 0x06000717 RID: 1815 RVA: 0x00010527 File Offset: 0x0000E727
		public HttpException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an error message and the <see cref="P:System.Exception.InnerException" /> property.</summary>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
		// Token: 0x06000718 RID: 1816 RVA: 0x0001053B File Offset: 0x0000E73B
		public HttpException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code and an error message.</summary>
		/// <param name="httpCode">The HTTP response status code sent to the client corresponding to this error. </param>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		// Token: 0x06000719 RID: 1817 RVA: 0x00010550 File Offset: 0x0000E750
		public HttpException(int httpCode, string message)
			: base(message)
		{
			this.http_code = httpCode;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001056B File Offset: 0x0000E76B
		internal HttpException(int httpCode, string message, string resourceName)
			: this(httpCode, message)
		{
			this.resource_name = resourceName;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001057C File Offset: 0x0000E77C
		internal HttpException(int httpCode, string message, string resourceName, string description)
			: this(httpCode, message, resourceName)
		{
			this.description = description;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the contextual information about the source or destination.</param>
		// Token: 0x0600071C RID: 1820 RVA: 0x0001058F File Offset: 0x0000E78F
		protected HttpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.http_code = info.GetInt32("_httpCode");
			this.webEventCode = info.GetInt32("_webEventCode");
		}

		/// <summary>Gets information about the exception and adds it to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the contextual information about the source or destination.</param>
		// Token: 0x0600071D RID: 1821 RVA: 0x000105C6 File Offset: 0x0000E7C6
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_httpCode", this.http_code);
			info.AddValue("_webEventCode", this.webEventCode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code, an error message, and an exception code.</summary>
		/// <param name="httpCode">The HTTP response status code displayed on the client. </param>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		/// <param name="hr">The exception code that defines the error. </param>
		// Token: 0x0600071E RID: 1822 RVA: 0x000105F2 File Offset: 0x0000E7F2
		public HttpException(int httpCode, string message, int hr)
			: base(message, hr)
		{
			this.http_code = httpCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an error message and an exception code.</summary>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		/// <param name="hr">The exception code that defines the error. </param>
		// Token: 0x0600071F RID: 1823 RVA: 0x0001060E File Offset: 0x0000E80E
		public HttpException(string message, int hr)
			: base(message, hr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code, an error message, and the <see cref="P:System.Exception.InnerException" /> property.</summary>
		/// <param name="httpCode">The HTTP response status code displayed on the client. </param>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
		// Token: 0x06000720 RID: 1824 RVA: 0x00010623 File Offset: 0x0000E823
		public HttpException(int httpCode, string message, Exception innerException)
			: base(message, innerException)
		{
			this.http_code = httpCode;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001063F File Offset: 0x0000E83F
		internal HttpException(int httpCode, string message, Exception innerException, string resourceName)
			: this(httpCode, message, innerException)
		{
			this.resource_name = resourceName;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00010652 File Offset: 0x0000E852
		[global::System.MonoTODO("For now just the default template is created. Means of user-provided templates are to be implemented yet.")]
		private ExceptionPageTemplate GetPageTemplate()
		{
			DefaultExceptionPageTemplate defaultExceptionPageTemplate = new DefaultExceptionPageTemplate();
			defaultExceptionPageTemplate.Init();
			return defaultExceptionPageTemplate;
		}

		/// <summary>Gets the HTML error message to return to the client.</summary>
		/// <returns>The HTML error message.</returns>
		// Token: 0x06000723 RID: 1827 RVA: 0x00010660 File Offset: 0x0000E860
		public string GetHtmlErrorMessage()
		{
			ExceptionPageTemplateValues exceptionPageTemplateValues = new ExceptionPageTemplateValues();
			ExceptionPageTemplate exceptionPageTemplate = this.PageTemplate;
			string text;
			try
			{
				exceptionPageTemplateValues.Add("RuntimeVersionInformation", RuntimeHelpers.MonoVersion);
				exceptionPageTemplateValues.Add("AspNetVersionInformation", Environment.Version.ToString());
				HttpContext httpContext = HttpContext.Current;
				ExceptionPageTemplateType exceptionPageTemplateType = ExceptionPageTemplateType.Standard;
				if (httpContext != null && httpContext.IsCustomErrorEnabled)
				{
					if (this.http_code != 404 && this.http_code != 403)
					{
						this.FillDefaultCustomErrorValues(exceptionPageTemplateValues);
						exceptionPageTemplateType = ExceptionPageTemplateType.CustomErrorDefault;
					}
					else
					{
						this.FillDefaultErrorValues(false, false, null, exceptionPageTemplateValues);
					}
				}
				else
				{
					Exception ex = this.GetBaseException();
					if (ex == null)
					{
						ex = this;
					}
					exceptionPageTemplateValues.Add("FullStackTrace", this.FormatFullStackTrace());
					HtmlizedException ex2 = ex as HtmlizedException;
					if (ex2 == null)
					{
						this.FillDefaultErrorValues(true, true, ex, exceptionPageTemplateValues);
					}
					else
					{
						exceptionPageTemplateType = ExceptionPageTemplateType.Htmlized;
						this.FillHtmlizedErrorValues(exceptionPageTemplateValues, ex2, ref exceptionPageTemplateType);
					}
				}
				text = exceptionPageTemplate.Render(exceptionPageTemplateValues, exceptionPageTemplateType);
			}
			catch (Exception ex3)
			{
				Console.Error.WriteLine("An exception has occurred while generating HttpException page:");
				Console.Error.WriteLine(ex3);
				Console.Error.WriteLine();
				Console.Error.WriteLine("The actual exception which was being reported was:");
				Console.Error.WriteLine(this);
				try
				{
					this.FillDefaultCustomErrorValues(exceptionPageTemplateValues);
					text = exceptionPageTemplate.Render(exceptionPageTemplateValues, ExceptionPageTemplateType.CustomErrorDefault);
				}
				catch
				{
					text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<style type=\"text/css\">\nbody { background-color: #FFFFFF; font-size: .75em; font-family: Verdana, Helvetica, Sans-Serif; margin: 0; padding: 0; color: #696969; }\na:link { color: #000000; text-decoration: underline; }\na:visited { color: #000000; }\na:hover { color: #000000; text-decoration: none; }\na:active { color: #12eb87; }\np, ul { margin-bottom: 20px; line-height: 1.6em; }\npre { font-size: 1.2em; margin-left: 20px; margin-top: 0px; }\nh1, h2, h3, h4, h5, h6 { font-size: 1.6em; color: #000; font-family: Arial, Helvetica, sans-serif; }\nh1 { font-weight: bold; margin-bottom: 0; margin-top: 0; padding-bottom: 0; }\nh2 { font-size: 1em; padding: 0 0 0px 0; color: #696969; font-weight: normal; margin-top: 0; margin-bottom: 20px; }\nh3 { font-size: 1.2em; }\nh4 { font-size: 1.1em; }\nh5, h6 { font-size: 1em; }\n#header { position: relative; margin-bottom: 0px; color: #000; padding: 0; background-color: #5c87b2; height: 38px; padding-left: 10px; }\n#header h1 { font-weight: bold; padding: 5px 0; margin: 0; color: #fff; border: none; line-height: 2em; font-family: Arial, Helvetica, sans-serif; font-size: 32px !important; }\n#header-image { float: left; padding: 3px; margin-left: 1px; margin-right: 1px; }\n#header-text { color: #fff; font-size: 1.4em; line-height: 38px; font-weight: bold; }\n#main { padding: 20px 20px 15px 20px; background-color: #fff; _height: 1px; }\n#footer { color: #999; padding: 5px 0; text-align: left; line-height: normal; margin: 20px 0px 0px 0px; font-size: .9em; border-top: solid 1px #5C87B2; }\n#footer-powered-by { float: right; }\n.details { font-family: monospace; border: solid 1px #e8eef4; white-space: pre; font-size: 1.2em; overflow: auto; padding: 6px; margin-top: 6px }\n.details-wrapped { white-space: normal }\n.details-header { margin-top: 1.5em }\n.details-header a { font-weight: bold; text-decoration: none }\np { margin-bottom: 0.3em; margin-top: 0.1em }\n.sourceErrorLine { color: #770000; font-weight: bold; }\n</style>\n\n<title>Double fault in exception reporting code</title>\n</head>\n<body>\n<h1>Double fault in exception reporting code</h1>\n<p>While generating HTML with exception report, a double fault has occurred. Please consult your server's console and/or log file to see the actual exception.</p>\n</body>\n</html>\n";
				}
			}
			return text;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000107B4 File Offset: 0x0000E9B4
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x000107CA File Offset: 0x0000E9CA
		internal virtual string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return "Error processing request.";
			}
			set
			{
				if (value != null && value.Length > 0)
				{
					this.description = value;
					return;
				}
				this.description = "Error processing request.";
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000107EB File Offset: 0x0000E9EB
		internal static HttpException NewWithCode(string message, int webEventCode)
		{
			HttpException ex = new HttpException(message);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000107FA File Offset: 0x0000E9FA
		internal static HttpException NewWithCode(string message, Exception innerException, int webEventCode)
		{
			HttpException ex = new HttpException(message, innerException);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001080A File Offset: 0x0000EA0A
		internal static HttpException NewWithCode(int httpCode, string message, int webEventCode)
		{
			HttpException ex = new HttpException(httpCode, message);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001081A File Offset: 0x0000EA1A
		internal static HttpException NewWithCode(int httpCode, string message, Exception innerException, string resourceName, int webEventCode)
		{
			HttpException ex = new HttpException(httpCode, message, innerException, resourceName);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001082D File Offset: 0x0000EA2D
		internal static HttpException NewWithCode(int httpCode, string message, string resourceName, int webEventCode)
		{
			HttpException ex = new HttpException(httpCode, message, resourceName);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001083E File Offset: 0x0000EA3E
		internal static HttpException NewWithCode(int httpCode, string message, Exception innerException, int webEventCode)
		{
			HttpException ex = new HttpException(httpCode, message, innerException);
			ex.SetWebEventCode(webEventCode);
			return ex;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001084F File Offset: 0x0000EA4F
		internal void SetWebEventCode(int webEventCode)
		{
			this.webEventCode = webEventCode;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00010858 File Offset: 0x0000EA58
		private string FormatFullStackTrace()
		{
			Exception ex = this;
			StringBuilder stringBuilder = new StringBuilder("\r\n<!--");
			bool flag = true;
			while (ex != null)
			{
				string stackTrace = ex.StackTrace;
				string message = ex.Message;
				bool flag2 = !string.IsNullOrEmpty(stackTrace);
				if (!flag2 && string.IsNullOrEmpty(message))
				{
					ex = ex.InnerException;
				}
				else
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append("\r\n");
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						"\r\n[",
						ex.GetType(),
						"]: ",
						HttpException.HtmlEncode(message),
						"\r\n"
					}));
					if (flag2)
					{
						stringBuilder.Append(ex.StackTrace);
					}
					ex = ex.InnerException;
				}
			}
			stringBuilder.Append("\r\n-->\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00010924 File Offset: 0x0000EB24
		private void FillHtmlizedErrorValues(ExceptionPageTemplateValues values, HtmlizedException exc, ref ExceptionPageTemplateType pageType)
		{
			bool flag = exc is ParseException;
			bool flag2 = !flag && exc is CompilationException;
			values.Add("Title", HttpException.HtmlEncode(exc.Title));
			values.Add("Description", HttpException.HtmlEncode(exc.Description));
			values.Add("StackTrace", HttpException.HtmlEncode(exc.StackTrace));
			values.Add("ExceptionType", exc.GetType().ToString());
			values.Add("ExceptionMessage", HttpException.HtmlEncode(exc.Message));
			values.Add("Details", HttpException.HtmlEncode(exc.ErrorMessage));
			string text;
			if (flag)
			{
				text = "Parser";
			}
			else if (flag2)
			{
				text = "Compiler";
			}
			else
			{
				text = "Other";
			}
			values.Add("HtmlizedExceptionOrigin", text);
			if (exc.FileText != null)
			{
				pageType |= ExceptionPageTemplateType.SourceError;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2;
				if (flag2)
				{
					stringBuilder2 = new StringBuilder();
				}
				else
				{
					stringBuilder2 = null;
				}
				HttpException.FormatSource(stringBuilder, stringBuilder2, exc);
				values.Add("HtmlizedExceptionShortSource", stringBuilder.ToString());
				values.Add("HtmlizedExceptionLongSource", (stringBuilder2 != null) ? stringBuilder2.ToString() : null);
				if (exc.SourceFile != exc.FileName)
				{
					values.Add("HtmlizedExceptionSourceFile", this.FormatSourceFile(exc.SourceFile));
				}
				else
				{
					values.Add("HtmlizedExceptionSourceFile", this.FormatSourceFile(exc.FileName));
				}
				if (flag || flag2)
				{
					int[] errorLines = exc.ErrorLines;
					int num = ((errorLines != null) ? errorLines.Length : 0);
					StringBuilder stringBuilder3 = new StringBuilder();
					for (int i = 0; i < num; i++)
					{
						if (i > 0)
						{
							stringBuilder3.Append(", ");
						}
						stringBuilder3.Append(errorLines[i]);
					}
					values.Add("HtmlizedExceptionErrorLines", stringBuilder3.ToString());
				}
			}
			else
			{
				values.Add("HtmlizedExceptionSourceFile", this.FormatSourceFile(exc.FileName));
			}
			if (flag2)
			{
				StringCollection compilerOutput = (exc as CompilationException).CompilerOutput;
				if (compilerOutput != null && compilerOutput.Count > 0)
				{
					pageType |= ExceptionPageTemplateType.CompilerOutput;
					StringBuilder stringBuilder4 = new StringBuilder();
					bool flag3 = true;
					foreach (string text2 in compilerOutput)
					{
						stringBuilder4.Append(HttpException.HtmlEncode(text2));
						if (flag3)
						{
							stringBuilder4.Append("<br/>");
							flag3 = false;
						}
						stringBuilder4.Append("<br/>");
					}
					values.Add("HtmlizedExceptionCompilerOutput", stringBuilder4.ToString());
				}
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		private void FillDefaultCustomErrorValues(ExceptionPageTemplateValues values)
		{
			values.Add("Title", "Runtime Error");
			values.Add("ExceptionType", "Runtime Error");
			values.Add("ExceptionMessage", "A runtime error has occurred");
			values.Add("Description", "An application error occurred on the server. The current custom error settings for this application prevent the details of the application error from being viewed (for security reasons).");
			values.Add("Details", "To enable the details of this specific error message to be viewable, please create a &lt;customErrors&gt; tag within a &quot;web.config&quot; configuration file located in the root directory of the current web application. This &lt;customErrors&gt; tag should then have its &quot;mode&quot; attribute set to &quot;Off&quot;.");
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00010C30 File Offset: 0x0000EE30
		private void FillDefaultErrorValues(bool showTrace, bool showExceptionType, Exception baseEx, ExceptionPageTemplateValues values)
		{
			if (baseEx == null)
			{
				baseEx = this;
			}
			values.Add("Title", string.Format("Error{0}", (this.http_code != 0) ? (" " + this.http_code) : string.Empty));
			values.Add("ExceptionType", showExceptionType ? baseEx.GetType().ToString() : "Runtime error");
			values.Add("ExceptionMessage", (this.http_code == 404) ? "The resource cannot be found." : HttpException.HtmlEncode(baseEx.Message));
			string text = ((this.http_code != 0) ? ("HTTP " + this.http_code + ".") : string.Empty);
			values.Add("Description", text + ((this.http_code == 404) ? "The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable.  Please review the following URL and make sure that it is spelled correctly." : HttpException.HtmlEncode(this.Description)));
			if (!string.IsNullOrEmpty(this.resource_name))
			{
				values.Add("Details", "Requested URL: " + HttpException.HtmlEncode(this.resource_name));
			}
			else if (this.http_code == 404)
			{
				values.Add("Details", "No virtual path information available.");
			}
			else if (baseEx is HttpException)
			{
				text = ((HttpException)baseEx).Description;
				values.Add("Details", (!string.IsNullOrEmpty(text)) ? HttpException.HtmlEncode(text) : "Web exception occurred but no additional error description given.");
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder("Non-web exception.");
				text = baseEx.Source;
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" Exception origin (name of application or object): {0}.", HttpException.HtmlEncode(text));
				}
				text = baseEx.HelpLink;
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" Additional information is available at {0}", HttpException.HtmlEncode(text));
				}
				values.Add("Details", stringBuilder.ToString());
			}
			if (showTrace)
			{
				string stackTrace = baseEx.StackTrace;
				if (!string.IsNullOrEmpty(stackTrace))
				{
					values.Add("StackTrace", HttpException.HtmlEncode(stackTrace));
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00010E34 File Offset: 0x0000F034
		private static string HtmlEncode(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			return HttpUtility.HtmlEncode(s).Replace("\r\n", "<br />");
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00010E55 File Offset: 0x0000F055
		private string FormatSourceFile(string filename)
		{
			if (filename == null || filename.Length == 0)
			{
				return string.Empty;
			}
			if (filename.StartsWith("@@"))
			{
				return "[internal] <!-- " + HttpUtility.HtmlEncode(filename) + " -->";
			}
			return HttpUtility.HtmlEncode(filename);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00010E91 File Offset: 0x0000F091
		private static void FormatSource(StringBuilder builder, StringBuilder longVersion, HtmlizedException e)
		{
			if (e is CompilationException)
			{
				HttpException.WriteCompilationSource(builder, longVersion, e);
				return;
			}
			HttpException.WritePageSource(builder, e);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00010EAC File Offset: 0x0000F0AC
		private static void WriteCompilationSource(StringBuilder builder, StringBuilder longVersion, HtmlizedException e)
		{
			int[] errorLines = e.ErrorLines;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (errorLines != null && errorLines.Length != 0)
			{
				num3 = errorLines[0];
			}
			int num4 = num3 - 2;
			int num5 = num3 + 2;
			if (num4 < 0)
			{
				num4 = 0;
			}
			using (TextReader textReader = new StringReader(e.FileText))
			{
				string text;
				while ((text = textReader.ReadLine()) != null)
				{
					num++;
					if (num < num4 || num > num5)
					{
						if (longVersion != null)
						{
							longVersion.AppendFormat("{0}: {1}\r\n", num, HttpException.HtmlEncode(text));
						}
					}
					else
					{
						if (num3 == num)
						{
							if (longVersion != null)
							{
								longVersion.Append("<span class=\"sourceErrorLine\">");
							}
							builder.Append("<span class=\"sourceErrorLine\">");
						}
						string text2 = string.Format("{0}: {1}\r\n", num, HttpException.HtmlEncode(text));
						builder.Append(text2);
						if (longVersion != null)
						{
							longVersion.Append(text2);
						}
						if (num == num3)
						{
							builder.Append("</span>");
							if (longVersion != null)
							{
								longVersion.Append("</span>");
							}
							num3 = ((++num2 < errorLines.Length) ? errorLines[num2] : 0);
						}
					}
				}
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		private static void WritePageSource(StringBuilder builder, HtmlizedException e)
		{
			int num = 0;
			int num2 = e.ErrorLines[0];
			int num3 = e.ErrorLines[1];
			int num4 = num2 - 2;
			int num5 = num3 + 2;
			if (num4 <= 0)
			{
				num4 = 1;
			}
			TextReader textReader = new StringReader(e.FileText);
			string text;
			while ((text = textReader.ReadLine()) != null)
			{
				num++;
				if (num >= num4)
				{
					if (num > num5)
					{
						break;
					}
					if (num2 == num)
					{
						builder.Append("<span class=\"sourceErrorLine\">");
					}
					builder.AppendFormat("{0}: {1}\r\n", num, HttpException.HtmlEncode(text));
					if (num3 <= num)
					{
						builder.Append("</span>");
						num3 = num5 + 1;
					}
				}
			}
		}

		/// <summary>Gets the HTTP response status code to return to the client. </summary>
		/// <returns>A non-zero HTTP code representing the exception or the <see cref="P:System.Exception.InnerException" /> code; otherwise, HTTP response status code 500.</returns>
		// Token: 0x06000736 RID: 1846 RVA: 0x0001106C File Offset: 0x0000F26C
		public int GetHttpCode()
		{
			return this.http_code;
		}

		/// <summary>Creates a new <see cref="T:System.Web.HttpException" /> exception based on the error code that is returned from the Win32 API GetLastError() method.</summary>
		/// <returns>An <see cref="T:System.Web.HttpException" /> based on the error code that is returned from a call to the Win32 API GetLastError() method.</returns>
		/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
		// Token: 0x06000737 RID: 1847 RVA: 0x00011074 File Offset: 0x0000F274
		public static HttpException CreateFromLastError(string message)
		{
			return new HttpException(message, 0);
		}

		// Token: 0x04000F5E RID: 3934
		private const string DEFAULT_DESCRIPTION_TEXT = "Error processing request.";

		// Token: 0x04000F5F RID: 3935
		private const string ERROR_404_DESCRIPTION = "The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable.  Please review the following URL and make sure that it is spelled correctly.";

		// Token: 0x04000F60 RID: 3936
		private int webEventCode;

		// Token: 0x04000F61 RID: 3937
		private int http_code = 500;

		// Token: 0x04000F62 RID: 3938
		private string resource_name;

		// Token: 0x04000F63 RID: 3939
		private string description;

		// Token: 0x04000F64 RID: 3940
		private ExceptionPageTemplate pageTemplate;

		// Token: 0x04000F65 RID: 3941
		private const string DoubleFaultExceptionMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<style type=\"text/css\">\nbody { background-color: #FFFFFF; font-size: .75em; font-family: Verdana, Helvetica, Sans-Serif; margin: 0; padding: 0; color: #696969; }\na:link { color: #000000; text-decoration: underline; }\na:visited { color: #000000; }\na:hover { color: #000000; text-decoration: none; }\na:active { color: #12eb87; }\np, ul { margin-bottom: 20px; line-height: 1.6em; }\npre { font-size: 1.2em; margin-left: 20px; margin-top: 0px; }\nh1, h2, h3, h4, h5, h6 { font-size: 1.6em; color: #000; font-family: Arial, Helvetica, sans-serif; }\nh1 { font-weight: bold; margin-bottom: 0; margin-top: 0; padding-bottom: 0; }\nh2 { font-size: 1em; padding: 0 0 0px 0; color: #696969; font-weight: normal; margin-top: 0; margin-bottom: 20px; }\nh3 { font-size: 1.2em; }\nh4 { font-size: 1.1em; }\nh5, h6 { font-size: 1em; }\n#header { position: relative; margin-bottom: 0px; color: #000; padding: 0; background-color: #5c87b2; height: 38px; padding-left: 10px; }\n#header h1 { font-weight: bold; padding: 5px 0; margin: 0; color: #fff; border: none; line-height: 2em; font-family: Arial, Helvetica, sans-serif; font-size: 32px !important; }\n#header-image { float: left; padding: 3px; margin-left: 1px; margin-right: 1px; }\n#header-text { color: #fff; font-size: 1.4em; line-height: 38px; font-weight: bold; }\n#main { padding: 20px 20px 15px 20px; background-color: #fff; _height: 1px; }\n#footer { color: #999; padding: 5px 0; text-align: left; line-height: normal; margin: 20px 0px 0px 0px; font-size: .9em; border-top: solid 1px #5C87B2; }\n#footer-powered-by { float: right; }\n.details { font-family: monospace; border: solid 1px #e8eef4; white-space: pre; font-size: 1.2em; overflow: auto; padding: 6px; margin-top: 6px }\n.details-wrapped { white-space: normal }\n.details-header { margin-top: 1.5em }\n.details-header a { font-weight: bold; text-decoration: none }\np { margin-bottom: 0.3em; margin-top: 0.1em }\n.sourceErrorLine { color: #770000; font-weight: bold; }\n</style>\n\n<title>Double fault in exception reporting code</title>\n</head>\n<body>\n<h1>Double fault in exception reporting code</h1>\n<p>While generating HTML with exception report, a double fault has occurred. Please consult your server's console and/or log file to see the actual exception.</p>\n</body>\n</html>\n";
	}
}
