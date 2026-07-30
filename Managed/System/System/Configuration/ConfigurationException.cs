using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The exception that is thrown when a configuration system error has occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016E RID: 366
	[Serializable]
	public class ConfigurationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		// Token: 0x06000B05 RID: 2821 RVA: 0x00039BC1 File Offset: 0x00037DC1
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException()
			: this(null)
		{
			this.filename = null;
			this.line = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		// Token: 0x06000B06 RID: 2822 RVA: 0x00039BD8 File Offset: 0x00037DD8
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x06000B07 RID: 2823 RVA: 0x00039BE1 File Offset: 0x00037DE1
		protected ConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("filename");
			this.line = info.GetInt32("line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		// Token: 0x06000B08 RID: 2824 RVA: 0x00039C0D File Offset: 0x00037E0D
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06000B09 RID: 2825 RVA: 0x00039C17 File Offset: 0x00037E17
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, XmlNode node)
			: base(message)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x06000B0A RID: 2826 RVA: 0x00039C38 File Offset: 0x00037E38
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, XmlNode node)
			: base(message, inner)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06000B0B RID: 2827 RVA: 0x00039C5A File Offset: 0x00037E5A
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, string filename, int line)
			: base(message)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> was thrown.</param>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00039C71 File Offset: 0x00037E71
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00039C8A File Offset: 0x00037E8A
		public virtual string BareMessage
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00039C92 File Offset: 0x00037E92
		public virtual string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00039C9A File Offset: 0x00037E9A
		public virtual int Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00039CA4 File Offset: 0x00037EA4
		public override string Message
		{
			get
			{
				string text;
				if (this.filename != null && this.filename.Length != 0)
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

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>A string representing the node file name.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B11 RID: 2833 RVA: 0x00039D77 File Offset: 0x00037F77
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static string GetXmlNodeFilename(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return string.Empty;
			}
			return ((IConfigXmlNode)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>An int representing the node line number.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B12 RID: 2834 RVA: 0x00039D92 File Offset: 0x00037F92
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static int GetXmlNodeLineNumber(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return 0;
			}
			return ((IConfigXmlNode)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B13 RID: 2835 RVA: 0x00039DA9 File Offset: 0x00037FA9
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("line", this.line);
		}

		// Token: 0x04000F89 RID: 3977
		private readonly string filename;

		// Token: 0x04000F8A RID: 3978
		private readonly int line;
	}
}
