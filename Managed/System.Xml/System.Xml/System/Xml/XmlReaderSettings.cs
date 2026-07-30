using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;
using Microsoft.Win32;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlReader" /> object created by the <see cref="Overload:System.Xml.XmlReader.Create" /> method. </summary>
	// Token: 0x0200010A RID: 266
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlReaderSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		// Token: 0x060009EC RID: 2540 RVA: 0x0002CD4A File Offset: 0x0002AF4A
		public XmlReaderSettings()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		/// <param name="resolver">The XML resolver.</param>
		// Token: 0x060009ED RID: 2541 RVA: 0x0002CD58 File Offset: 0x0002AF58
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public XmlReaderSettings(XmlResolver resolver)
		{
			this.Initialize(resolver);
		}

		/// <summary>Gets or sets whether asynchronous <see cref="T:System.Xml.XmlReader" /> methods can be used on a particular <see cref="T:System.Xml.XmlReader" /> instance.</summary>
		/// <returns>true if asynchronous methods can be used; otherwise, false.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002CD67 File Offset: 0x0002AF67
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0002CD6F File Offset: 0x0002AF6F
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

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNameTable" /> used for atomized string comparisons.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> that stores all the atomized strings used by all <see cref="T:System.Xml.XmlReader" /> instances created using this <see cref="T:System.Xml.XmlReaderSettings" /> object.The default is null. The created <see cref="T:System.Xml.XmlReader" /> instance will use a new empty <see cref="T:System.Xml.NameTable" /> if this value is null.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002CD83 File Offset: 0x0002AF83
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0002CD8B File Offset: 0x0002AF8B
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
			set
			{
				this.CheckReadOnly("NameTable");
				this.nameTable = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002CD9F File Offset: 0x0002AF9F
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0002CDA7 File Offset: 0x0002AFA7
		internal bool IsXmlResolverSet { get; set; }

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to access external documents.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> used to access external documents. If set to null, an <see cref="T:System.Xml.XmlException" /> is thrown when the <see cref="T:System.Xml.XmlReader" /> tries to access an external resource. The default is a new <see cref="T:System.Xml.XmlUrlResolver" /> with no credentials.</returns>
		// Token: 0x170001B1 RID: 433
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		public XmlResolver XmlResolver
		{
			set
			{
				this.CheckReadOnly("XmlResolver");
				this.xmlResolver = value;
				this.IsXmlResolverSet = true;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002CDCB File Offset: 0x0002AFCB
		internal XmlResolver GetXmlResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002CDD3 File Offset: 0x0002AFD3
		internal XmlResolver GetXmlResolver_CheckConfig()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver && !this.IsXmlResolverSet)
			{
				return null;
			}
			return this.xmlResolver;
		}

		/// <summary>Gets or sets line number offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line number offset. The default is 0.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002CDEC File Offset: 0x0002AFEC
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public int LineNumberOffset
		{
			get
			{
				return this.lineNumberOffset;
			}
			set
			{
				this.CheckReadOnly("LineNumberOffset");
				this.lineNumberOffset = value;
			}
		}

		/// <summary>Gets or sets line position offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line position offset. The default is 0.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002CE08 File Offset: 0x0002B008
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0002CE10 File Offset: 0x0002B010
		public int LinePositionOffset
		{
			get
			{
				return this.linePositionOffset;
			}
			set
			{
				this.CheckReadOnly("LinePositionOffset");
				this.linePositionOffset = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values that specifies the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply. The default is ConformanceLevel.Document.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002CE24 File Offset: 0x0002B024
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0002CE2C File Offset: 0x0002B02C
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
		/// <returns>true to do character checking; otherwise false. The default is true.NoteIf the <see cref="T:System.Xml.XmlReader" /> is processing text data, it always checks that the XML names and text content are valid, regardless of the property setting. Setting <see cref="P:System.Xml.XmlReaderSettings.CheckCharacters" /> to false turns off character checking for character entity references.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002CE4F File Offset: 0x0002B04F
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0002CE57 File Offset: 0x0002B057
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

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters XML document. A zero (0) value means no limits on the size of the XML document. A non-zero value specifies the maximum size, in characters.</summary>
		/// <returns>The maximum allowable number of characters in an XML document. The default is 0.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002CE6B File Offset: 0x0002B06B
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002CE73 File Offset: 0x0002B073
		public long MaxCharactersInDocument
		{
			get
			{
				return this.maxCharactersInDocument;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersInDocument");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersInDocument = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters in a document that result from expanding entities.</summary>
		/// <returns>The maximum allowable number of characters from expanded entities. The default is 0.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002CE97 File Offset: 0x0002B097
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0002CE9F File Offset: 0x0002B09F
		public long MaxCharactersFromEntities
		{
			get
			{
				return this.maxCharactersFromEntities;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersFromEntities");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersFromEntities = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore insignificant white space.</summary>
		/// <returns>true to ignore white space; otherwise false. The default is false.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002CEC3 File Offset: 0x0002B0C3
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0002CECB File Offset: 0x0002B0CB
		public bool IgnoreWhitespace
		{
			get
			{
				return this.ignoreWhitespace;
			}
			set
			{
				this.CheckReadOnly("IgnoreWhitespace");
				this.ignoreWhitespace = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore processing instructions.</summary>
		/// <returns>true to ignore processing instructions; otherwise false. The default is false.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002CEDF File Offset: 0x0002B0DF
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002CEE7 File Offset: 0x0002B0E7
		public bool IgnoreProcessingInstructions
		{
			get
			{
				return this.ignorePIs;
			}
			set
			{
				this.CheckReadOnly("IgnoreProcessingInstructions");
				this.ignorePIs = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore comments.</summary>
		/// <returns>true to ignore comments; otherwise false. The default is false.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002CEFB File Offset: 0x0002B0FB
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002CF03 File Offset: 0x0002B103
		public bool IgnoreComments
		{
			get
			{
				return this.ignoreComments;
			}
			set
			{
				this.CheckReadOnly("IgnoreComments");
				this.ignoreComments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to prohibit document type definition (DTD) processing. This property is obsolete. Use <see cref="P:System.Xml.XmlTextReader.DtdProcessing" /> instead.</summary>
		/// <returns>true to prohibit DTD processing; otherwise false. The default is true.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002CF17 File Offset: 0x0002B117
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002CF22 File Offset: 0x0002B122
		[Obsolete("Use XmlReaderSettings.DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.dtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.CheckReadOnly("ProhibitDtd");
				this.dtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		/// <summary>Gets or sets a value that determines the processing of DTDs.</summary>
		/// <returns>One of the values of the <see cref="T:System.Xml.DtdProcessing" /> enumeration that determines the processing of DTDs.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002CF3C File Offset: 0x0002B13C
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002CF44 File Offset: 0x0002B144
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.dtdProcessing;
			}
			set
			{
				this.CheckReadOnly("DtdProcessing");
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the underlying stream or <see cref="T:System.IO.TextReader" /> should be closed when the reader is closed.</summary>
		/// <returns>true to close the underlying stream or <see cref="T:System.IO.TextReader" /> when the reader is closed; otherwise false. The default is false.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002CF67 File Offset: 0x0002B167
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002CF6F File Offset: 0x0002B16F
		public bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.CheckReadOnly("CloseInput");
				this.closeInput = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> will perform validation or type assignment when reading.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values that indicates whether XmlReader will perform validation or type assignment when reading. The default is ValidationType.None.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002CF83 File Offset: 0x0002B183
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0002CF8B File Offset: 0x0002B18B
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				this.CheckReadOnly("ValidationType");
				if (value > ValidationType.Schema)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationType = value;
			}
		}

		/// <summary>Gets or sets a value indicating the schema validation settings. This setting applies to schema validating <see cref="T:System.Xml.XmlReader" /> objects (<see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to ValidationType.Schema).</summary>
		/// <returns>A set of <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> values. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessIdentityConstraints" /> and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes" /> are enabled by default. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessInlineSchema" />, <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation" />, and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings" /> are disabled by default.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002CFAE File Offset: 0x0002B1AE
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0002CFB6 File Offset: 0x0002B1B6
		public XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
			set
			{
				this.CheckReadOnly("ValidationFlags");
				if (value > (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationFlags = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation. The default is an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002CFDA File Offset: 0x0002B1DA
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0002CFF5 File Offset: 0x0002B1F5
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet();
				}
				return this.schemas;
			}
			set
			{
				this.CheckReadOnly("Schemas");
				this.schemas = value;
			}
		}

		/// <summary>Occurs when the reader encounters validation errors.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000A15 RID: 2581 RVA: 0x0002D009 File Offset: 0x0002B209
		// (remove) Token: 0x06000A16 RID: 2582 RVA: 0x0002D02D File Offset: 0x0002B22D
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Combine(this.valEventHandler, value);
			}
			remove
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Remove(this.valEventHandler, value);
			}
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002D051 File Offset: 0x0002B251
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlReaderSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlReaderSettings" /> object.</returns>
		// Token: 0x06000A18 RID: 2584 RVA: 0x0002D064 File Offset: 0x0002B264
		public XmlReaderSettings Clone()
		{
			XmlReaderSettings xmlReaderSettings = base.MemberwiseClone() as XmlReaderSettings;
			xmlReaderSettings.ReadOnly = false;
			return xmlReaderSettings;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002D078 File Offset: 0x0002B278
		internal ValidationEventHandler GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002D080 File Offset: 0x0002B280
		internal XmlReader CreateReader(string inputUri, XmlParserContext inputContext)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (inputUri.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The string was not recognized as a valid Uri."), "inputUri");
			}
			XmlResolver xmlResolver = this.GetXmlResolver();
			if (xmlResolver == null)
			{
				xmlResolver = XmlReaderSettings.CreateDefaultResolver();
			}
			XmlReader xmlReader = new XmlTextReaderImpl(inputUri, this, inputContext, xmlResolver);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		internal XmlReader CreateReader(Stream input, Uri baseUri, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				if (baseUri == null)
				{
					baseUriString = string.Empty;
				}
				else
				{
					baseUriString = baseUri.ToString();
				}
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, null, 0, this, baseUri, baseUriString, inputContext, this.closeInput);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002D160 File Offset: 0x0002B360
		internal XmlReader CreateReader(TextReader input, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				baseUriString = string.Empty;
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, this, baseUriString, inputContext);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002D1AF File Offset: 0x0002B3AF
		internal XmlReader CreateReader(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return this.AddValidationAndConformanceWrapper(reader);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002D1C6 File Offset: 0x0002B3C6
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002D1CE File Offset: 0x0002B3CE
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

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002D1D7 File Offset: 0x0002B3D7
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002D202 File Offset: 0x0002B402
		private void Initialize()
		{
			this.Initialize(null);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002D20C File Offset: 0x0002B40C
		private void Initialize(XmlResolver resolver)
		{
			this.nameTable = null;
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = resolver;
				this.maxCharactersFromEntities = 10000000L;
			}
			else
			{
				this.xmlResolver = ((resolver == null) ? XmlReaderSettings.CreateDefaultResolver() : resolver);
				this.maxCharactersFromEntities = 0L;
			}
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.checkCharacters = true;
			this.conformanceLevel = ConformanceLevel.Document;
			this.ignoreWhitespace = false;
			this.ignorePIs = false;
			this.ignoreComments = false;
			this.dtdProcessing = DtdProcessing.Prohibit;
			this.closeInput = false;
			this.maxCharactersInDocument = 0L;
			this.schemas = null;
			this.validationType = ValidationType.None;
			this.validationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			this.validationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
			this.useAsync = false;
			this.isReadOnly = false;
			this.IsXmlResolverSet = false;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002D2D5 File Offset: 0x0002B4D5
		private static XmlResolver CreateDefaultResolver()
		{
			return new XmlUrlResolver();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		internal XmlReader AddValidation(XmlReader reader)
		{
			if (this.validationType == ValidationType.Schema)
			{
				XmlResolver xmlResolver = this.GetXmlResolver_CheckConfig();
				if (xmlResolver == null && !this.IsXmlResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlResolver = new XmlUrlResolver();
				}
				reader = new XsdValidatingReader(reader, xmlResolver, this);
			}
			else if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			return reader;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002D330 File Offset: 0x0002B530
		private XmlReader AddValidationAndConformanceWrapper(XmlReader reader)
		{
			if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			reader = this.AddConformanceWrapper(reader);
			if (this.validationType == ValidationType.Schema)
			{
				reader = new XsdValidatingReader(reader, this.GetXmlResolver_CheckConfig(), this);
			}
			return reader;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002D366 File Offset: 0x0002B566
		private XmlValidatingReaderImpl CreateDtdValidatingReader(XmlReader baseReader)
		{
			return new XmlValidatingReaderImpl(baseReader, this.GetEventHandler(), (this.ValidationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002D380 File Offset: 0x0002B580
		internal XmlReader AddConformanceWrapper(XmlReader baseReader)
		{
			XmlReaderSettings settings = baseReader.Settings;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			DtdProcessing dtdProcessing = (DtdProcessing)(-1);
			bool flag5 = false;
			if (settings == null)
			{
				if (this.conformanceLevel != ConformanceLevel.Auto && this.conformanceLevel != XmlReader.GetV1ConformanceLevel(baseReader))
				{
					throw new InvalidOperationException(Res.GetString("Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.", new object[] { this.conformanceLevel.ToString() }));
				}
				XmlTextReader xmlTextReader = baseReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					XmlValidatingReader xmlValidatingReader = baseReader as XmlValidatingReader;
					if (xmlValidatingReader != null)
					{
						xmlTextReader = (XmlTextReader)xmlValidatingReader.Reader;
					}
				}
				if (this.ignoreWhitespace)
				{
					WhitespaceHandling whitespaceHandling = WhitespaceHandling.All;
					if (xmlTextReader != null)
					{
						whitespaceHandling = xmlTextReader.WhitespaceHandling;
					}
					if (whitespaceHandling == WhitespaceHandling.All)
					{
						flag2 = true;
						flag5 = true;
					}
				}
				if (this.ignoreComments)
				{
					flag3 = true;
					flag5 = true;
				}
				if (this.ignorePIs)
				{
					flag4 = true;
					flag5 = true;
				}
				DtdProcessing dtdProcessing2 = DtdProcessing.Parse;
				if (xmlTextReader != null)
				{
					dtdProcessing2 = xmlTextReader.DtdProcessing;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && dtdProcessing2 != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && dtdProcessing2 == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag5 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel && this.conformanceLevel != ConformanceLevel.Auto)
				{
					throw new InvalidOperationException(Res.GetString("Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.", new object[] { this.conformanceLevel.ToString() }));
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					flag5 = true;
				}
				if (this.ignoreWhitespace && !settings.IgnoreWhitespace)
				{
					flag2 = true;
					flag5 = true;
				}
				if (this.ignoreComments && !settings.IgnoreComments)
				{
					flag3 = true;
					flag5 = true;
				}
				if (this.ignorePIs && !settings.IgnoreProcessingInstructions)
				{
					flag4 = true;
					flag5 = true;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && settings.DtdProcessing != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && settings.DtdProcessing == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag5 = true;
				}
			}
			if (!flag5)
			{
				return baseReader;
			}
			IXmlNamespaceResolver xmlNamespaceResolver = baseReader as IXmlNamespaceResolver;
			if (xmlNamespaceResolver != null)
			{
				return new XmlCharCheckingReaderWithNS(baseReader, xmlNamespaceResolver, flag, flag2, flag3, flag4, dtdProcessing);
			}
			return new XmlCharCheckingReader(baseReader, flag, flag2, flag3, flag4, dtdProcessing);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002D57C File Offset: 0x0002B77C
		internal static bool EnableLegacyXmlSettings()
		{
			if (XmlReaderSettings.s_enableLegacyXmlSettings != null)
			{
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			if (!BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_2)
			{
				XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(true);
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			bool flag = false;
			if (!XmlReaderSettings.ReadSettingsFromRegistry(Registry.LocalMachine, ref flag))
			{
				XmlReaderSettings.ReadSettingsFromRegistry(Registry.CurrentUser, ref flag);
			}
			XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(flag);
			return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool ReadSettingsFromRegistry(RegistryKey hive, ref bool value)
		{
			try
			{
				using (RegistryKey registryKey = hive.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\XML", false))
				{
					if (registryKey != null && registryKey.GetValueKind("EnableLegacyXmlSettings") == RegistryValueKind.DWord)
					{
						value = (int)registryKey.GetValue("EnableLegacyXmlSettings") == 1;
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x040005BF RID: 1471
		private bool useAsync;

		// Token: 0x040005C0 RID: 1472
		private XmlNameTable nameTable;

		// Token: 0x040005C1 RID: 1473
		private XmlResolver xmlResolver;

		// Token: 0x040005C2 RID: 1474
		private int lineNumberOffset;

		// Token: 0x040005C3 RID: 1475
		private int linePositionOffset;

		// Token: 0x040005C4 RID: 1476
		private ConformanceLevel conformanceLevel;

		// Token: 0x040005C5 RID: 1477
		private bool checkCharacters;

		// Token: 0x040005C6 RID: 1478
		private long maxCharactersInDocument;

		// Token: 0x040005C7 RID: 1479
		private long maxCharactersFromEntities;

		// Token: 0x040005C8 RID: 1480
		private bool ignoreWhitespace;

		// Token: 0x040005C9 RID: 1481
		private bool ignorePIs;

		// Token: 0x040005CA RID: 1482
		private bool ignoreComments;

		// Token: 0x040005CB RID: 1483
		private DtdProcessing dtdProcessing;

		// Token: 0x040005CC RID: 1484
		private ValidationType validationType;

		// Token: 0x040005CD RID: 1485
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x040005CE RID: 1486
		private XmlSchemaSet schemas;

		// Token: 0x040005CF RID: 1487
		private ValidationEventHandler valEventHandler;

		// Token: 0x040005D0 RID: 1488
		private bool closeInput;

		// Token: 0x040005D1 RID: 1489
		private bool isReadOnly;

		// Token: 0x040005D3 RID: 1491
		private static bool? s_enableLegacyXmlSettings;
	}
}
