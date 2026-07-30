using System;
using System.Configuration.Internal;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Wraps the corresponding <see cref="T:System.Xml.XmlDocument" /> type and also carries the necessary information for reporting file-name and line numbers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000168 RID: 360
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigXmlNode, IConfigErrorInfo
	{
		/// <summary>Creates a configuration element attribute.</summary>
		/// <returns>The <see cref="P:System.Xml.Serialization.XmlAttributes.XmlAttribute" /> attribute.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name that is used locally.</param>
		/// <param name="namespaceUri">The URL that is assigned to the namespace.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x000398AF File Offset: 0x00037AAF
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlAttribute(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates an XML CData section.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlCDataSection" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x000398BA File Offset: 0x00037ABA
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlDocument.ConfigXmlCDataSection(this, data);
		}

		/// <summary>Create an XML comment.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlComment" /> value.</returns>
		/// <param name="data">The comment data.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x000398C3 File Offset: 0x00037AC3
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlDocument.ConfigXmlComment(this, data);
		}

		/// <summary>Creates a configuration element.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> value.</returns>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name used locally.</param>
		/// <param name="namespaceUri">The namespace for the URL.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x000398CC File Offset: 0x00037ACC
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlElement(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates white spaces.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlSignificantWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEA RID: 2794 RVA: 0x000398D7 File Offset: 0x00037AD7
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return base.CreateSignificantWhitespace(data);
		}

		/// <summary>Create a text node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlText" /> value.</returns>
		/// <param name="text">The text to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEB RID: 2795 RVA: 0x000398E0 File Offset: 0x00037AE0
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlDocument.ConfigXmlText(this, text);
		}

		/// <summary>Creates white space.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlWhitespace" /> value.</returns>
		/// <param name="data">The data to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEC RID: 2796 RVA: 0x000398E9 File Offset: 0x00037AE9
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return base.CreateWhitespace(data);
		}

		/// <summary>Loads the configuration file.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AED RID: 2797 RVA: 0x000398F4 File Offset: 0x00037AF4
		public override void Load(string filename)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(filename);
			try
			{
				xmlTextReader.MoveToContent();
				this.LoadSingleElement(filename, xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads a single configuration element.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <param name="sourceReader">The source for the reader.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEE RID: 2798 RVA: 0x00039930 File Offset: 0x00037B30
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this.fileName = filename;
			this.lineNumber = sourceReader.LineNumber;
			string text = sourceReader.ReadOuterXml();
			this.reader = new XmlTextReader(new StringReader(text), sourceReader.NameTable);
			this.Load(this.reader);
			this.reader.Close();
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The configuration file name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00039985 File Offset: 0x00037B85
		public string Filename
		{
			get
			{
				if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				}
				return this.fileName;
			}
		}

		/// <summary>Gets the current node line number.</summary>
		/// <returns>The line number for the current node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000399BB File Offset: 0x00037BBB
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The file name.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000399C3 File Offset: 0x00037BC3
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		/// <summary>Gets the configuration line number.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000399CB File Offset: 0x00037BCB
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000399C3 File Offset: 0x00037BC3
		string IConfigXmlNode.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000399CB File Offset: 0x00037BCB
		int IConfigXmlNode.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x04000F7C RID: 3964
		private XmlTextReader reader;

		// Token: 0x04000F7D RID: 3965
		private string fileName;

		// Token: 0x04000F7E RID: 3966
		private int lineNumber;

		// Token: 0x02000169 RID: 361
		private class ConfigXmlAttribute : XmlAttribute, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF6 RID: 2806 RVA: 0x000399DB File Offset: 0x00037BDB
			public ConfigXmlAttribute(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00039A00 File Offset: 0x00037C00
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00039A36 File Offset: 0x00037C36
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000F7F RID: 3967
			private string fileName;

			// Token: 0x04000F80 RID: 3968
			private int lineNumber;
		}

		// Token: 0x0200016A RID: 362
		private class ConfigXmlCDataSection : XmlCDataSection, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF9 RID: 2809 RVA: 0x00039A3E File Offset: 0x00037C3E
			public ConfigXmlCDataSection(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00039A60 File Offset: 0x00037C60
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00039A96 File Offset: 0x00037C96
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000F81 RID: 3969
			private string fileName;

			// Token: 0x04000F82 RID: 3970
			private int lineNumber;
		}

		// Token: 0x0200016B RID: 363
		private class ConfigXmlComment : XmlComment, IConfigXmlNode
		{
			// Token: 0x06000AFC RID: 2812 RVA: 0x00039A9E File Offset: 0x00037C9E
			public ConfigXmlComment(ConfigXmlDocument document, string comment)
				: base(comment, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00039AC0 File Offset: 0x00037CC0
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00039AF6 File Offset: 0x00037CF6
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000F83 RID: 3971
			private string fileName;

			// Token: 0x04000F84 RID: 3972
			private int lineNumber;
		}

		// Token: 0x0200016C RID: 364
		private class ConfigXmlElement : XmlElement, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AFF RID: 2815 RVA: 0x00039AFE File Offset: 0x00037CFE
			public ConfigXmlElement(ConfigXmlDocument document, string prefix, string localName, string namespaceUri)
				: base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00039B23 File Offset: 0x00037D23
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00039B59 File Offset: 0x00037D59
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000F85 RID: 3973
			private string fileName;

			// Token: 0x04000F86 RID: 3974
			private int lineNumber;
		}

		// Token: 0x0200016D RID: 365
		private class ConfigXmlText : XmlText, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000B02 RID: 2818 RVA: 0x00039B61 File Offset: 0x00037D61
			public ConfigXmlText(ConfigXmlDocument document, string data)
				: base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00039B83 File Offset: 0x00037D83
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00039BB9 File Offset: 0x00037DB9
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000F87 RID: 3975
			private string fileName;

			// Token: 0x04000F88 RID: 3976
			private int lineNumber;
		}
	}
}
