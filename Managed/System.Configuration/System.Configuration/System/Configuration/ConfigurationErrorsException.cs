using System;
using System.Collections;
using System.Configuration.Internal;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The current value is not one of the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> values.</summary>
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class ConfigurationErrorsException : ConfigurationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		// Token: 0x06000110 RID: 272 RVA: 0x0000553B File Offset: 0x0000373B
		public ConfigurationErrorsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x06000111 RID: 273 RVA: 0x00005543 File Offset: 0x00003743
		public ConfigurationErrorsException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a <see cref="T:System.Configuration.ConfigurationException" /> or a <see cref="T:System.Configuration.ConfigurationErrorsException" />.</exception>
		// Token: 0x06000112 RID: 274 RVA: 0x0000554C File Offset: 0x0000374C
		protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("ConfigurationErrors_Filename");
			this.line = info.GetInt32("ConfigurationErrors_Line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000113 RID: 275 RVA: 0x00005578 File Offset: 0x00003778
		public ConfigurationErrorsException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000114 RID: 276 RVA: 0x00005582 File Offset: 0x00003782
		public ConfigurationErrorsException(string message, XmlNode node)
			: this(message, null, ConfigurationErrorsException.GetFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000115 RID: 277 RVA: 0x00005598 File Offset: 0x00003798
		public ConfigurationErrorsException(string message, Exception inner, XmlNode node)
			: this(message, inner, ConfigurationErrorsException.GetFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000116 RID: 278 RVA: 0x000055AE File Offset: 0x000037AE
		public ConfigurationErrorsException(string message, XmlReader reader)
			: this(message, null, ConfigurationErrorsException.GetFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000117 RID: 279 RVA: 0x000055C4 File Offset: 0x000037C4
		public ConfigurationErrorsException(string message, Exception inner, XmlReader reader)
			: this(message, inner, ConfigurationErrorsException.GetFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x06000118 RID: 280 RVA: 0x000055DA File Offset: 0x000037DA
		public ConfigurationErrorsException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x06000119 RID: 281 RVA: 0x000055E6 File Offset: 0x000037E6
		public ConfigurationErrorsException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> was thrown.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000055FF File Offset: 0x000037FF
		public override string BareMessage
		{
			get
			{
				return base.BareMessage;
			}
		}

		/// <summary>Gets a collection of errors that detail the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains errors that identify the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003727 File Offset: 0x00001927
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> to be thrown.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005607 File Offset: 0x00003807
		public override string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000560F File Offset: 0x0000380F
		public override int Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005618 File Offset: 0x00003818
		public override string Message
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(this.filename))
				{
					if (this.line != 0)
					{
						text = string.Concat(new object[] { this.BareMessage, " (", this.filename, " line ", this.line, ")" });
					}
					else
					{
						text = this.BareMessage + " (" + this.filename + ")";
					}
				}
				else if (this.line != 0)
				{
					text = string.Concat(new object[] { this.BareMessage, " (line ", this.line, ")" });
				}
				else
				{
					text = this.BareMessage;
				}
				return text;
			}
		}

		/// <summary>Gets the path to the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> was reading when this configuration exception was thrown.</summary>
		/// <returns>The path of the configuration file the internal <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x0600011F RID: 287 RVA: 0x000056E3 File Offset: 0x000038E3
		public static string GetFilename(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).Filename;
			}
			if (reader == null)
			{
				return null;
			}
			return reader.BaseURI;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> object was processing when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that the <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000120 RID: 288 RVA: 0x00005704 File Offset: 0x00003904
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).LineNumber;
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo == null)
			{
				return 0;
			}
			return xmlLineInfo.LineNumber;
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>The path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown. </returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000121 RID: 289 RVA: 0x00005737 File Offset: 0x00003937
		public static string GetFilename(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return null;
			}
			return ((IConfigErrorInfo)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that contains the <see cref="T:System.Xml.XmlNode" /> object being parsed when this configuration exception was thrown.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x06000122 RID: 290 RVA: 0x0000574E File Offset: 0x0000394E
		public static int GetLineNumber(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return 0;
			}
			return ((IConfigErrorInfo)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000123 RID: 291 RVA: 0x00005765 File Offset: 0x00003965
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ConfigurationErrors_Filename", this.filename);
			info.AddValue("ConfigurationErrors_Line", this.line);
		}

		// Token: 0x04000086 RID: 134
		private readonly string filename;

		// Token: 0x04000087 RID: 135
		private readonly int line;
	}
}
