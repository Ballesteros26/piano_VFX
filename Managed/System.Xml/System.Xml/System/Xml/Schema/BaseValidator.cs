using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x0200038B RID: 907
	internal class BaseValidator
	{
		// Token: 0x060024AC RID: 9388 RVA: 0x000DEB50 File Offset: 0x000DCD50
		public BaseValidator(BaseValidator other)
		{
			this.reader = other.reader;
			this.schemaCollection = other.schemaCollection;
			this.eventHandling = other.eventHandling;
			this.nameTable = other.nameTable;
			this.schemaNames = other.schemaNames;
			this.positionInfo = other.positionInfo;
			this.xmlResolver = other.xmlResolver;
			this.baseUri = other.baseUri;
			this.elementName = other.elementName;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000DEBCF File Offset: 0x000DCDCF
		public BaseValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
		{
			this.reader = reader;
			this.schemaCollection = schemaCollection;
			this.eventHandling = eventHandling;
			this.nameTable = reader.NameTable;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.elementName = new XmlQualifiedName();
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000DEC0F File Offset: 0x000DCE0F
		public XmlValidatingReaderImpl Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000DEC17 File Offset: 0x000DCE17
		public XmlSchemaCollection SchemaCollection
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000DEC1F File Offset: 0x000DCE1F
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000DEC28 File Offset: 0x000DCE28
		public SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames != null)
				{
					return this.schemaNames;
				}
				if (this.schemaCollection != null)
				{
					this.schemaNames = this.schemaCollection.GetSchemaNames(this.nameTable);
				}
				else
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000DEC7C File Offset: 0x000DCE7C
		public PositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x000DEC84 File Offset: 0x000DCE84
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x000DEC8C File Offset: 0x000DCE8C
		public XmlResolver XmlResolver
		{
			get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000DEC95 File Offset: 0x000DCE95
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x000DEC9D File Offset: 0x000DCE9D
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000DECA6 File Offset: 0x000DCEA6
		public ValidationEventHandler EventHandler
		{
			get
			{
				return (ValidationEventHandler)this.eventHandling.EventHandler;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000DECB8 File Offset: 0x000DCEB8
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x000DECC0 File Offset: 0x000DCEC0
		public SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000DECB8 File Offset: 0x000DCEB8
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000DECCC File Offset: 0x000DCECC
		public IDtdInfo DtdInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				SchemaInfo schemaInfo = value as SchemaInfo;
				if (schemaInfo == null)
				{
					throw new XmlException("An internal error has occurred.", string.Empty);
				}
				this.schemaInfo = schemaInfo;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void Validate()
		{
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void CompleteValidation()
		{
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual object FindId(string name)
		{
			return null;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000DECFC File Offset: 0x000DCEFC
		public void ValidateText()
		{
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				ContentValidator contentValidator = this.context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly)
				{
					ArrayList arrayList = contentValidator.ExpectedElements(this.context, false);
					if (arrayList == null)
					{
						this.SendValidationEvent("The element {0} cannot contain text.", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						this.SendValidationEvent("The element {0} cannot contain text. List of possible elements expected: {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, false)
						});
					}
				}
				else if (contentType == XmlSchemaContentType.Empty)
				{
					this.SendValidationEvent("The element cannot contain text. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000DEE0C File Offset: 0x000DD00C
		public void ValidateWhitespace()
		{
			if (this.context.NeedValidateChildren)
			{
				int contentType = (int)this.context.ElementDecl.ContentValidator.ContentType;
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (contentType == 1)
				{
					this.SendValidationEvent("The element cannot contain white space. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000DEE9C File Offset: 0x000DD09C
		private void SaveTextValue(string value)
		{
			if (this.textString.Length == 0)
			{
				this.textString = value;
				return;
			}
			if (!this.hasSibling)
			{
				this.textValue.Append(this.textString);
				this.hasSibling = true;
			}
			this.textValue.Append(value);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000DEEEC File Offset: 0x000DD0EC
		protected void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000DEEFA File Offset: 0x000DD0FA
		protected void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000DEF2A File Offset: 0x000DD12A
		protected void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, arg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000DEF5A File Offset: 0x000DD15A
		protected void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { arg1, arg2 }, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000DEF97 File Offset: 0x000DD197
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000DEFA1 File Offset: 0x000DD1A1
		protected void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000DEFD2 File Offset: 0x000DD1D2
		protected void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000DF003 File Offset: 0x000DD203
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandling != null)
			{
				this.eventHandling.SendEvent(e, severity);
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000DF020 File Offset: 0x000DD220
		protected static void ProcessEntity(SchemaInfo sinfo, string name, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNumber, int linePosition)
		{
			XmlSchemaException ex = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				ex = new XmlSchemaException("Reference to an undeclared entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				ex = new XmlSchemaException("Reference to an unparsed entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			if (ex == null)
			{
				return;
			}
			if (eventhandler != null)
			{
				eventhandler(sender, new ValidationEventArgs(ex));
				return;
			}
			throw ex;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000DF090 File Offset: 0x000DD290
		protected static void ProcessEntity(SchemaInfo sinfo, string name, IValidationEventHandling eventHandling, string baseUriStr, int lineNumber, int linePosition)
		{
			string text = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				text = "Reference to an undeclared entity, '{0}'.";
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				text = "Reference to an unparsed entity, '{0}'.";
			}
			if (text == null)
			{
				return;
			}
			XmlSchemaException ex = new XmlSchemaException(text, name, baseUriStr, lineNumber, linePosition);
			if (eventHandling != null)
			{
				eventHandling.SendEvent(ex, XmlSeverityType.Error);
				return;
			}
			throw ex;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000DF0F0 File Offset: 0x000DD2F0
		public static BaseValidator CreateInstance(ValidationType valType, XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling, bool processIdentityConstraints)
		{
			switch (valType)
			{
			case ValidationType.None:
				return new BaseValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Auto:
				return new AutoValidator(reader, schemaCollection, eventHandling);
			case ValidationType.DTD:
				return new DtdValidator(reader, eventHandling, processIdentityConstraints);
			case ValidationType.XDR:
				return new XdrValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Schema:
				return new XsdValidator(reader, schemaCollection, eventHandling);
			default:
				return null;
			}
		}

		// Token: 0x040018E3 RID: 6371
		private XmlSchemaCollection schemaCollection;

		// Token: 0x040018E4 RID: 6372
		private IValidationEventHandling eventHandling;

		// Token: 0x040018E5 RID: 6373
		private XmlNameTable nameTable;

		// Token: 0x040018E6 RID: 6374
		private SchemaNames schemaNames;

		// Token: 0x040018E7 RID: 6375
		private PositionInfo positionInfo;

		// Token: 0x040018E8 RID: 6376
		private XmlResolver xmlResolver;

		// Token: 0x040018E9 RID: 6377
		private Uri baseUri;

		// Token: 0x040018EA RID: 6378
		protected SchemaInfo schemaInfo;

		// Token: 0x040018EB RID: 6379
		protected XmlValidatingReaderImpl reader;

		// Token: 0x040018EC RID: 6380
		protected XmlQualifiedName elementName;

		// Token: 0x040018ED RID: 6381
		protected ValidationState context;

		// Token: 0x040018EE RID: 6382
		protected StringBuilder textValue;

		// Token: 0x040018EF RID: 6383
		protected string textString;

		// Token: 0x040018F0 RID: 6384
		protected bool hasSibling;

		// Token: 0x040018F1 RID: 6385
		protected bool checkDatatype;
	}
}
