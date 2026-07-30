using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information about the schema exception.</summary>
	// Token: 0x0200044F RID: 1103
	[Serializable]
	public class XmlSchemaException : SystemException
	{
		/// <summary>Constructs a new XmlSchemaException object with the given SerializationInfo and StreamingContext information that contains all the properties of the XmlSchemaException.</summary>
		/// <param name="info">SerializationInfo.</param>
		/// <param name="context">StreamingContext.</param>
		// Token: 0x06002BFA RID: 11258 RVA: 0x00106A5C File Offset: 0x00104C5C
		protected XmlSchemaException(SerializationInfo info, StreamingContext context)
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
				this.message = XmlSchemaException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the XmlSchemaException properties into the SerializationInfo class for the given StreamingContext.</summary>
		/// <param name="info">The SerializationInfo. </param>
		/// <param name="context">The StreamingContext information. </param>
		// Token: 0x06002BFB RID: 11259 RVA: 0x00106B70 File Offset: 0x00104D70
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class.</summary>
		// Token: 0x06002BFC RID: 11260 RVA: 0x000EC653 File Offset: 0x000EA853
		public XmlSchemaException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		// Token: 0x06002BFD RID: 11261 RVA: 0x000EC65C File Offset: 0x000EA85C
		public XmlSchemaException(string message)
			: this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		// Token: 0x06002BFE RID: 11262 RVA: 0x000EC668 File Offset: 0x000EA868
		public XmlSchemaException(string message, Exception innerException)
			: this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06002BFF RID: 11263 RVA: 0x00106BEA File Offset: 0x00104DEA
		public XmlSchemaException(string message, Exception innerException, int lineNumber, int linePosition)
			: this((message == null) ? "A schema error occurred." : "{0}", new string[] { message }, innerException, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000EC681 File Offset: 0x000EA881
		internal XmlSchemaException(string res, string[] args)
			: this(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000EC690 File Offset: 0x000EA890
		internal XmlSchemaException(string res, string arg)
			: this(res, new string[] { arg }, null, null, 0, 0, null)
		{
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000EC6A8 File Offset: 0x000EA8A8
		internal XmlSchemaException(string res, string arg, string sourceUri, int lineNumber, int linePosition)
			: this(res, new string[] { arg }, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000EC6C2 File Offset: 0x000EA8C2
		internal XmlSchemaException(string res, string sourceUri, int lineNumber, int linePosition)
			: this(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000EC6D2 File Offset: 0x000EA8D2
		internal XmlSchemaException(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
			: this(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00106C11 File Offset: 0x00104E11
		internal XmlSchemaException(string res, XmlSchemaObject source)
			: this(res, null, source)
		{
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x00106C1C File Offset: 0x00104E1C
		internal XmlSchemaException(string res, string arg, XmlSchemaObject source)
			: this(res, new string[] { arg }, source)
		{
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00106C30 File Offset: 0x00104E30
		internal XmlSchemaException(string res, string[] args, XmlSchemaObject source)
			: this(res, args, null, source.SourceUri, source.LineNumber, source.LinePosition, source)
		{
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00106C50 File Offset: 0x00104E50
		internal XmlSchemaException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source)
			: base(XmlSchemaException.CreateMessage(res, args), innerException)
		{
			base.HResult = -2146231999;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceSchemaObject = source;
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00106CA4 File Offset: 0x00104EA4
		internal static string CreateMessage(string res, string[] args)
		{
			string text;
			try
			{
				text = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
				text = "UNKNOWN(" + res + ")";
			}
			return text;
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00106CE0 File Offset: 0x00104EE0
		internal string GetRes
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x00106CE8 File Offset: 0x00104EE8
		internal string[] Args
		{
			get
			{
				return this.args;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) location of the schema that caused the exception.</summary>
		/// <returns>The URI location of the schema that caused the exception.</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00106CF0 File Offset: 0x00104EF0
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x00106CF8 File Offset: 0x00104EF8
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x00106D00 File Offset: 0x00104F00
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>The XmlSchemaObject that produced the XmlSchemaException.</summary>
		/// <returns>A valid object instance represents a structural validation error in the XML Schema Object Model (SOM).</returns>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x00106D08 File Offset: 0x00104F08
		public XmlSchemaObject SourceSchemaObject
		{
			get
			{
				return this.sourceSchemaObject;
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00106D10 File Offset: 0x00104F10
		internal void SetSource(string sourceUri, int lineNumber, int linePosition)
		{
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00106D27 File Offset: 0x00104F27
		internal void SetSchemaObject(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00106D30 File Offset: 0x00104F30
		internal void SetSource(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
			this.sourceUri = source.SourceUri;
			this.lineNumber = source.LineNumber;
			this.linePosition = source.LinePosition;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00106D5D File Offset: 0x00104F5D
		internal void SetResourceId(string resourceId)
		{
			this.res = resourceId;
		}

		/// <summary>Gets the description of the error condition of this exception.</summary>
		/// <returns>The description of the error condition of this exception.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x00106D66 File Offset: 0x00104F66
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

		// Token: 0x04001D97 RID: 7575
		private string res;

		// Token: 0x04001D98 RID: 7576
		private string[] args;

		// Token: 0x04001D99 RID: 7577
		private string sourceUri;

		// Token: 0x04001D9A RID: 7578
		private int lineNumber;

		// Token: 0x04001D9B RID: 7579
		private int linePosition;

		// Token: 0x04001D9C RID: 7580
		[NonSerialized]
		private XmlSchemaObject sourceSchemaObject;

		// Token: 0x04001D9D RID: 7581
		private string message;
	}
}
