using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlWriter" /> object created by the <see cref="Overload:System.Xml.XmlWriter.Create" /> method.</summary>
	// Token: 0x020001E6 RID: 486
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlWriterSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWriterSettings" /> class.</summary>
		// Token: 0x06001123 RID: 4387 RVA: 0x000661F6 File Offset: 0x000643F6
		public XmlWriterSettings()
		{
			this.Initialize();
		}

		/// <summary>Gets or sets a value that indicates whether asynchronous <see cref="T:System.Xml.XmlWriter" /> methods can be used on a particular <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>true if asynchronous methods can be used; otherwise, false.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0006620F File Offset: 0x0006440F
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00066217 File Offset: 0x00064417
		public bool Async
		{
			get
			{
				return this.useAsync;
			}
			set
			{
				this.CheckReadOnly("Async");
				this.useAsync = value;
			}
		}

		/// <summary>Gets or sets the type of text encoding to use.</summary>
		/// <returns>The text encoding to use. The default is Encoding.UTF8.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0006622B File Offset: 0x0006442B
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00066233 File Offset: 0x00064433
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.CheckReadOnly("Encoding");
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to omit an XML declaration.</summary>
		/// <returns>true to omit the XML declaration; otherwise, false. The default is false, an XML declaration is written.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00066247 File Offset: 0x00064447
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0006624F File Offset: 0x0006444F
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
			set
			{
				this.CheckReadOnly("OmitXmlDeclaration");
				this.omitXmlDecl = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.NewLineHandling" /> values. The default is <see cref="F:System.Xml.NewLineHandling.Replace" />.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00066263 File Offset: 0x00064463
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0006626B File Offset: 0x0006446B
		public NewLineHandling NewLineHandling
		{
			get
			{
				return this.newLineHandling;
			}
			set
			{
				this.CheckReadOnly("NewLineHandling");
				if (value > NewLineHandling.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.newLineHandling = value;
			}
		}

		/// <summary>Gets or sets the character string to use for line breaks.</summary>
		/// <returns>The character string to use for line breaks. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is \r\n (carriage return, new line).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" /> is null.</exception>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0006628E File Offset: 0x0006448E
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00066296 File Offset: 0x00064496
		public string NewLineChars
		{
			get
			{
				return this.newLineChars;
			}
			set
			{
				this.CheckReadOnly("NewLineChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.newLineChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent elements.</summary>
		/// <returns>true to write individual elements on new lines and indent; otherwise, false. The default is false.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000662B8 File Offset: 0x000644B8
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x000662C3 File Offset: 0x000644C3
		public bool Indent
		{
			get
			{
				return this.indent == TriState.True;
			}
			set
			{
				this.CheckReadOnly("Indent");
				this.indent = (value ? TriState.True : TriState.False);
			}
		}

		/// <summary>Gets or sets the character string to use when indenting. This setting is used when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property is set to true.</summary>
		/// <returns>The character string to use when indenting. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is two spaces.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.IndentChars" /> is null.</exception>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000662DD File Offset: 0x000644DD
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x000662E5 File Offset: 0x000644E5
		public string IndentChars
		{
			get
			{
				return this.indentChars;
			}
			set
			{
				this.CheckReadOnly("IndentChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.indentChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
		/// <returns>true to write attributes on individual lines; otherwise, false. The default is false.NoteThis setting has no effect when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property value is false.When <see cref="P:System.Xml.XmlWriterSettings.NewLineOnAttributes" /> is set to true, each attribute is pre-pended with a new line and one extra level of indentation.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00066307 File Offset: 0x00064507
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x0006630F File Offset: 0x0006450F
		public bool NewLineOnAttributes
		{
			get
			{
				return this.newLineOnAttributes;
			}
			set
			{
				this.CheckReadOnly("NewLineOnAttributes");
				this.newLineOnAttributes = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlWriter" /> should also close the underlying stream or <see cref="T:System.IO.TextWriter" /> when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>true to also close the underlying stream or <see cref="T:System.IO.TextWriter" />; otherwise, false. The default is false.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00066323 File Offset: 0x00064523
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x0006632B File Offset: 0x0006452B
		public bool CloseOutput
		{
			get
			{
				return this.closeOutput;
			}
			set
			{
				this.CheckReadOnly("CloseOutput");
				this.closeOutput = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlWriter" /> complies with.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values. The default is ConformanceLevel.Document.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0006633F File Offset: 0x0006453F
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00066347 File Offset: 0x00064547
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			set
			{
				this.CheckReadOnly("ConformanceLevel");
				if (value > ConformanceLevel.Document)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.conformanceLevel = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do character checking.</summary>
		/// <returns>true to do character checking; otherwise, false. The default is true.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0006636A File Offset: 0x0006456A
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00066372 File Offset: 0x00064572
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> should remove duplicate namespace declarations when writing XML content. The default behavior is for the writer to output all namespace declarations that are present in the writer's namespace resolver.</summary>
		/// <returns>The <see cref="T:System.Xml.NamespaceHandling" /> enumeration used to specify whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00066386 File Offset: 0x00064586
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x0006638E File Offset: 0x0006458E
		public NamespaceHandling NamespaceHandling
		{
			get
			{
				return this.namespaceHandling;
			}
			set
			{
				this.CheckReadOnly("NamespaceHandling");
				if (value > NamespaceHandling.OmitDuplicates)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.namespaceHandling = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> will add closing tags to all unclosed element tags when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>true if all unclosed element tags will be closed out; otherwise, false. The default value is true. </returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x000663B1 File Offset: 0x000645B1
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x000663B9 File Offset: 0x000645B9
		public bool WriteEndDocumentOnClose
		{
			get
			{
				return this.writeEndDocumentOnClose;
			}
			set
			{
				this.CheckReadOnly("WriteEndDocumentOnClose");
				this.writeEndDocumentOnClose = value;
			}
		}

		/// <summary>Gets the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlOutputMethod" /> values. The default is <see cref="F:System.Xml.XmlOutputMethod.Xml" />.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000663CD File Offset: 0x000645CD
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x000663D5 File Offset: 0x000645D5
		public XmlOutputMethod OutputMethod
		{
			get
			{
				return this.outputMethod;
			}
			internal set
			{
				this.outputMethod = value;
			}
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x06001140 RID: 4416 RVA: 0x000663DE File Offset: 0x000645DE
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlWriterSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlWriterSettings" /> object.</returns>
		// Token: 0x06001141 RID: 4417 RVA: 0x000663F1 File Offset: 0x000645F1
		public XmlWriterSettings Clone()
		{
			XmlWriterSettings xmlWriterSettings = base.MemberwiseClone() as XmlWriterSettings;
			xmlWriterSettings.cdataSections = new List<XmlQualifiedName>(this.cdataSections);
			xmlWriterSettings.isReadOnly = false;
			return xmlWriterSettings;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00066416 File Offset: 0x00064616
		internal List<XmlQualifiedName> CDataSectionElements
		{
			get
			{
				return this.cdataSections;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> do not escape URI attributes.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlWriter" /> do not escape URI attributes; otherwise, false.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0006641E File Offset: 0x0006461E
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00066426 File Offset: 0x00064626
		public bool DoNotEscapeUriAttributes
		{
			get
			{
				return this.doNotEscapeUriAttributes;
			}
			set
			{
				this.CheckReadOnly("DoNotEscapeUriAttributes");
				this.doNotEscapeUriAttributes = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0006643A File Offset: 0x0006463A
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00066442 File Offset: 0x00064642
		internal bool MergeCDataSections
		{
			get
			{
				return this.mergeCDataSections;
			}
			set
			{
				this.CheckReadOnly("MergeCDataSections");
				this.mergeCDataSections = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00066456 File Offset: 0x00064656
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0006645E File Offset: 0x0006465E
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.CheckReadOnly("MediaType");
				this.mediaType = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00066472 File Offset: 0x00064672
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x0006647A File Offset: 0x0006467A
		internal string DocTypeSystem
		{
			get
			{
				return this.docTypeSystem;
			}
			set
			{
				this.CheckReadOnly("DocTypeSystem");
				this.docTypeSystem = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0006648E File Offset: 0x0006468E
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x00066496 File Offset: 0x00064696
		internal string DocTypePublic
		{
			get
			{
				return this.docTypePublic;
			}
			set
			{
				this.CheckReadOnly("DocTypePublic");
				this.docTypePublic = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000664AA File Offset: 0x000646AA
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x000664B2 File Offset: 0x000646B2
		internal XmlStandalone Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				this.CheckReadOnly("Standalone");
				this.standalone = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x000664C6 File Offset: 0x000646C6
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x000664CE File Offset: 0x000646CE
		internal bool AutoXmlDeclaration
		{
			get
			{
				return this.autoXmlDecl;
			}
			set
			{
				this.CheckReadOnly("AutoXmlDeclaration");
				this.autoXmlDecl = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000664E2 File Offset: 0x000646E2
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x000664EA File Offset: 0x000646EA
		internal TriState IndentInternal
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000664F3 File Offset: 0x000646F3
		internal bool IsQuerySpecific
		{
			get
			{
				return this.cdataSections.Count != 0 || this.docTypePublic != null || this.docTypeSystem != null || this.standalone == XmlStandalone.Yes;
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00066520 File Offset: 0x00064720
		internal XmlWriter CreateWriter(string outputFileName)
		{
			if (outputFileName == null)
			{
				throw new ArgumentNullException("outputFileName");
			}
			XmlWriterSettings xmlWriterSettings = this;
			if (!xmlWriterSettings.CloseOutput)
			{
				xmlWriterSettings = xmlWriterSettings.Clone();
				xmlWriterSettings.CloseOutput = true;
			}
			FileStream fileStream = null;
			XmlWriter xmlWriter;
			try
			{
				fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, this.useAsync);
				xmlWriter = xmlWriterSettings.CreateWriter(fileStream);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				throw;
			}
			return xmlWriter;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00066594 File Offset: 0x00064794
		internal XmlWriter CreateWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			if (this.Encoding.WebName == "utf-8")
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextUtf8RawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			else
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextEncodedRawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000666E4 File Offset: 0x000648E4
		internal XmlWriter CreateWriter(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			switch (this.OutputMethod)
			{
			case XmlOutputMethod.Xml:
				if (this.Indent)
				{
					xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new XmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Html:
				if (this.Indent)
				{
					xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new HtmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Text:
				xmlWriter = new TextEncodedRawTextWriter(output, this);
				break;
			case XmlOutputMethod.AutoDetect:
				xmlWriter = new XmlAutoDetectWriter(output, this);
				break;
			default:
				return null;
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000667A2 File Offset: 0x000649A2
		internal XmlWriter CreateWriter(XmlWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return this.AddConformanceWrapper(output);
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000667B9 File Offset: 0x000649B9
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x000667C1 File Offset: 0x000649C1
		internal bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000667CA File Offset: 0x000649CA
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000667F8 File Offset: 0x000649F8
		private void Initialize()
		{
			this.encoding = Encoding.UTF8;
			this.omitXmlDecl = false;
			this.newLineHandling = NewLineHandling.Replace;
			this.newLineChars = Environment.NewLine;
			this.indent = TriState.Unknown;
			this.indentChars = "  ";
			this.newLineOnAttributes = false;
			this.closeOutput = false;
			this.namespaceHandling = NamespaceHandling.Default;
			this.conformanceLevel = ConformanceLevel.Document;
			this.checkCharacters = true;
			this.writeEndDocumentOnClose = true;
			this.outputMethod = XmlOutputMethod.Xml;
			this.cdataSections.Clear();
			this.mergeCDataSections = false;
			this.mediaType = null;
			this.docTypeSystem = null;
			this.docTypePublic = null;
			this.standalone = XmlStandalone.Omit;
			this.doNotEscapeUriAttributes = false;
			this.useAsync = false;
			this.isReadOnly = false;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000668B0 File Offset: 0x00064AB0
		private XmlWriter AddConformanceWrapper(XmlWriter baseWriter)
		{
			ConformanceLevel conformanceLevel = ConformanceLevel.Auto;
			XmlWriterSettings settings = baseWriter.Settings;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (settings == null)
			{
				if (this.newLineHandling == NewLineHandling.Replace)
				{
					flag3 = true;
					flag4 = true;
				}
				if (this.checkCharacters)
				{
					flag = true;
					flag4 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel)
				{
					conformanceLevel = this.ConformanceLevel;
					flag4 = true;
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					flag2 = conformanceLevel == ConformanceLevel.Auto;
					flag4 = true;
				}
				if (this.newLineHandling == NewLineHandling.Replace && settings.NewLineHandling == NewLineHandling.None)
				{
					flag3 = true;
					flag4 = true;
				}
			}
			XmlWriter xmlWriter = baseWriter;
			if (flag4)
			{
				if (conformanceLevel != ConformanceLevel.Auto)
				{
					xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
				}
				if (flag || flag3)
				{
					xmlWriter = new XmlCharCheckingWriter(xmlWriter, flag, flag2, flag3, this.NewLineChars);
				}
			}
			if (this.IsQuerySpecific && (settings == null || !settings.IsQuerySpecific))
			{
				xmlWriter = new QueryOutputWriterV1(xmlWriter, this);
			}
			return xmlWriter;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00066988 File Offset: 0x00064B88
		internal void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.Write(this.Encoding.CodePage);
			writer.Write(this.OmitXmlDeclaration);
			writer.Write((sbyte)this.NewLineHandling);
			writer.WriteStringQ(this.NewLineChars);
			writer.Write((sbyte)this.IndentInternal);
			writer.WriteStringQ(this.IndentChars);
			writer.Write(this.NewLineOnAttributes);
			writer.Write(this.CloseOutput);
			writer.Write((sbyte)this.ConformanceLevel);
			writer.Write(this.CheckCharacters);
			writer.Write((sbyte)this.outputMethod);
			writer.Write(this.cdataSections.Count);
			foreach (XmlQualifiedName xmlQualifiedName in this.cdataSections)
			{
				writer.Write(xmlQualifiedName.Name);
				writer.Write(xmlQualifiedName.Namespace);
			}
			writer.Write(this.mergeCDataSections);
			writer.WriteStringQ(this.mediaType);
			writer.WriteStringQ(this.docTypeSystem);
			writer.WriteStringQ(this.docTypePublic);
			writer.Write((sbyte)this.standalone);
			writer.Write(this.autoXmlDecl);
			writer.Write(this.ReadOnly);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00066AE0 File Offset: 0x00064CE0
		internal XmlWriterSettings(XmlQueryDataReader reader)
		{
			this.Encoding = Encoding.GetEncoding(reader.ReadInt32());
			this.OmitXmlDeclaration = reader.ReadBoolean();
			this.NewLineHandling = (NewLineHandling)reader.ReadSByte(0, 2);
			this.NewLineChars = reader.ReadStringQ();
			this.IndentInternal = (TriState)reader.ReadSByte(-1, 1);
			this.IndentChars = reader.ReadStringQ();
			this.NewLineOnAttributes = reader.ReadBoolean();
			this.CloseOutput = reader.ReadBoolean();
			this.ConformanceLevel = (ConformanceLevel)reader.ReadSByte(0, 2);
			this.CheckCharacters = reader.ReadBoolean();
			this.outputMethod = (XmlOutputMethod)reader.ReadSByte(0, 3);
			int num = reader.ReadInt32();
			this.cdataSections = new List<XmlQualifiedName>(num);
			for (int i = 0; i < num; i++)
			{
				this.cdataSections.Add(new XmlQualifiedName(reader.ReadString(), reader.ReadString()));
			}
			this.mergeCDataSections = reader.ReadBoolean();
			this.mediaType = reader.ReadStringQ();
			this.docTypeSystem = reader.ReadStringQ();
			this.docTypePublic = reader.ReadStringQ();
			this.Standalone = (XmlStandalone)reader.ReadSByte(0, 2);
			this.autoXmlDecl = reader.ReadBoolean();
			this.ReadOnly = reader.ReadBoolean();
		}

		// Token: 0x04000C3C RID: 3132
		private bool useAsync;

		// Token: 0x04000C3D RID: 3133
		private Encoding encoding;

		// Token: 0x04000C3E RID: 3134
		private bool omitXmlDecl;

		// Token: 0x04000C3F RID: 3135
		private NewLineHandling newLineHandling;

		// Token: 0x04000C40 RID: 3136
		private string newLineChars;

		// Token: 0x04000C41 RID: 3137
		private TriState indent;

		// Token: 0x04000C42 RID: 3138
		private string indentChars;

		// Token: 0x04000C43 RID: 3139
		private bool newLineOnAttributes;

		// Token: 0x04000C44 RID: 3140
		private bool closeOutput;

		// Token: 0x04000C45 RID: 3141
		private NamespaceHandling namespaceHandling;

		// Token: 0x04000C46 RID: 3142
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000C47 RID: 3143
		private bool checkCharacters;

		// Token: 0x04000C48 RID: 3144
		private bool writeEndDocumentOnClose;

		// Token: 0x04000C49 RID: 3145
		private XmlOutputMethod outputMethod;

		// Token: 0x04000C4A RID: 3146
		private List<XmlQualifiedName> cdataSections = new List<XmlQualifiedName>();

		// Token: 0x04000C4B RID: 3147
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000C4C RID: 3148
		private bool mergeCDataSections;

		// Token: 0x04000C4D RID: 3149
		private string mediaType;

		// Token: 0x04000C4E RID: 3150
		private string docTypeSystem;

		// Token: 0x04000C4F RID: 3151
		private string docTypePublic;

		// Token: 0x04000C50 RID: 3152
		private XmlStandalone standalone;

		// Token: 0x04000C51 RID: 3153
		private bool autoXmlDecl;

		// Token: 0x04000C52 RID: 3154
		private bool isReadOnly;
	}
}
