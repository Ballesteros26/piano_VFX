using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown when an error occurs while processing an XSLT transformation.</summary>
	// Token: 0x020004E0 RID: 1248
	[Serializable]
	public class XsltException : SystemException
	{
		/// <summary>Initializes a new instance of the XsltException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XsltException. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060032D5 RID: 13013 RVA: 0x00124660 File Offset: 0x00122860
		protected XsltException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.sourceUri = (string)info.GetValue("sourceUri", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XsltException.CreateMessage(this.res, this.args, this.sourceUri, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the XsltException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x060032D6 RID: 13014 RVA: 0x00124784 File Offset: 0x00122984
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class.</summary>
		// Token: 0x060032D7 RID: 13015 RVA: 0x001247FE File Offset: 0x001229FE
		public XsltException()
			: this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class with a specified error message. </summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060032D8 RID: 13016 RVA: 0x0012480C File Offset: 0x00122A0C
		public XsltException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the XsltException class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> which threw the XsltException, if any. This value can be null. </param>
		// Token: 0x060032D9 RID: 13017 RVA: 0x00124816 File Offset: 0x00122A16
		public XsltException(string message, Exception innerException)
			: this("{0}", new string[] { message }, null, 0, 0, innerException)
		{
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x00124831 File Offset: 0x00122A31
		internal static XsltException Create(string res, params string[] args)
		{
			return new XsltException(res, args, null, 0, 0, null);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0012483E File Offset: 0x00122A3E
		internal static XsltException Create(string res, string[] args, Exception inner)
		{
			return new XsltException(res, args, null, 0, 0, inner);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0012484B File Offset: 0x00122A4B
		internal XsltException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, Exception inner)
			: base(XsltException.CreateMessage(res, args, sourceUri, lineNumber, linePosition), inner)
		{
			base.HResult = -2146231998;
			this.res = res;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the location path of the style sheet.</summary>
		/// <returns>The location path of the style sheet.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x0012488A File Offset: 0x00122A8A
		public virtual string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets the line number indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line number indicating where the error occurred in the style sheet.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x00124892 File Offset: 0x00122A92
		public virtual int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line position indicating where the error occurred in the style sheet.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x0012489A File Offset: 0x00122A9A
		public virtual int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the formatted error message describing the current exception.</summary>
		/// <returns>The formatted error message describing the current exception.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x001248A2 File Offset: 0x00122AA2
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x001248BC File Offset: 0x00122ABC
		private static string CreateMessage(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
		{
			string text2;
			try
			{
				string text = XsltException.FormatMessage(res, args);
				if (res != "XSLT compile error at {0}({1},{2}). See InnerException for details." && lineNumber != 0)
				{
					text = text + " " + XsltException.FormatMessage("An error occurred at {0}({1},{2}).", new string[]
					{
						sourceUri,
						lineNumber.ToString(CultureInfo.InvariantCulture),
						linePosition.ToString(CultureInfo.InvariantCulture)
					});
				}
				text2 = text;
			}
			catch (MissingManifestResourceException)
			{
				text2 = "UNKNOWN(" + res + ")";
			}
			return text2;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x00124948 File Offset: 0x00122B48
		private static string FormatMessage(string key, params string[] args)
		{
			string text = Res.GetString(key);
			if (text != null && args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, text, args);
			}
			return text;
		}

		// Token: 0x040020F7 RID: 8439
		private string res;

		// Token: 0x040020F8 RID: 8440
		private string[] args;

		// Token: 0x040020F9 RID: 8441
		private string sourceUri;

		// Token: 0x040020FA RID: 8442
		private int lineNumber;

		// Token: 0x040020FB RID: 8443
		private int linePosition;

		// Token: 0x040020FC RID: 8444
		private string message;
	}
}
