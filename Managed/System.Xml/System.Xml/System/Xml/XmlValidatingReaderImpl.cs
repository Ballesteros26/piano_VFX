using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001A1 RID: 417
	internal sealed class XmlValidatingReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x000585B0 File Offset: 0x000567B0
		internal XmlValidatingReaderImpl(XmlReader reader)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderNSResolver = reader as IXmlNamespaceResolver;
			this.coreReaderImpl = reader as XmlTextReaderImpl;
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader."), "reader");
			}
			this.coreReaderImpl.EntityHandling = EntityHandling.ExpandEntities;
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.processIdentityConstraints = true;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.Auto;
			this.SetupValidation(ValidationType.Auto);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000586C8 File Offset: 0x000568C8
		internal XmlValidatingReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
			: this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0005872C File Offset: 0x0005692C
		internal XmlValidatingReaderImpl(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
			: this(new XmlTextReader(xmlFragment, fragType, context))
		{
			if (this.coreReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = this.GetResolver().ResolveUri(null, this.coreReader.BaseURI);
			}
			if (context != null)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext;
				this.parserContext = context;
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00058790 File Offset: 0x00056990
		internal XmlValidatingReaderImpl(XmlReader reader, ValidationEventHandler settingsEventHandler, bool processIdentityConstraints)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderImpl = reader as XmlTextReaderImpl;
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader."), "reader");
			}
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.coreReaderNSResolver = reader as IXmlNamespaceResolver;
			this.processIdentityConstraints = processIdentityConstraints;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			if (settingsEventHandler != null)
			{
				this.eventHandling.AddHandler(settingsEventHandler);
			}
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.DTD;
			this.SetupValidation(ValidationType.DTD);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000588A8 File Offset: 0x00056AA8
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings;
				if (this.coreReaderImpl.V1Compat)
				{
					xmlReaderSettings = null;
				}
				else
				{
					xmlReaderSettings = this.coreReader.Settings;
				}
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.ValidationType = ValidationType.DTD;
				if (!this.processIdentityConstraints)
				{
					xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0005890A File Offset: 0x00056B0A
		public override XmlNodeType NodeType
		{
			get
			{
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00058917 File Offset: 0x00056B17
		public override string Name
		{
			get
			{
				return this.coreReader.Name;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00058924 File Offset: 0x00056B24
		public override string LocalName
		{
			get
			{
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00058931 File Offset: 0x00056B31
		public override string NamespaceURI
		{
			get
			{
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0005893E File Offset: 0x00056B3E
		public override string Prefix
		{
			get
			{
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0005894B File Offset: 0x00056B4B
		public override bool HasValue
		{
			get
			{
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00058958 File Offset: 0x00056B58
		public override string Value
		{
			get
			{
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00058965 File Offset: 0x00056B65
		public override int Depth
		{
			get
			{
				return this.coreReader.Depth;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00058972 File Offset: 0x00056B72
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0005897F File Offset: 0x00056B7F
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0005898C File Offset: 0x00056B8C
		public override bool IsDefault
		{
			get
			{
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00058999 File Offset: 0x00056B99
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000589A6 File Offset: 0x00056BA6
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x000589B3 File Offset: 0x00056BB3
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000589C0 File Offset: 0x00056BC0
		public override ReadState ReadState
		{
			get
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000589D8 File Offset: 0x00056BD8
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000589E5 File Offset: 0x00056BE5
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000589F2 File Offset: 0x00056BF2
		internal Encoding Encoding
		{
			get
			{
				return this.coreReaderImpl.Encoding;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000589FF File Offset: 0x00056BFF
		public override int AttributeCount
		{
			get
			{
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00058A0C File Offset: 0x00056C0C
		public override string GetAttribute(string name)
		{
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00058A1A File Offset: 0x00056C1A
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.coreReader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00058A29 File Offset: 0x00056C29
		public override string GetAttribute(int i)
		{
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00058A37 File Offset: 0x00056C37
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00058A51 File Offset: 0x00056C51
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (!this.coreReader.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00058A6C File Offset: 0x00056C6C
		public override void MoveToAttribute(int i)
		{
			this.coreReader.MoveToAttribute(i);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00058A81 File Offset: 0x00056C81
		public override bool MoveToFirstAttribute()
		{
			if (!this.coreReader.MoveToFirstAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00058A9A File Offset: 0x00056C9A
		public override bool MoveToNextAttribute()
		{
			if (!this.coreReader.MoveToNextAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00058AB3 File Offset: 0x00056CB3
		public override bool MoveToElement()
		{
			if (!this.coreReader.MoveToElement())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00058ACC File Offset: 0x00056CCC
		public override bool Read()
		{
			switch (this.parsingFunction)
			{
			case XmlValidatingReaderImpl.ParsingFunction.Read:
				break;
			case XmlValidatingReaderImpl.ParsingFunction.Init:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessCoreReaderEvent();
					return true;
				}
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ParseDtdFromParserContext();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ResolveEntityInternally();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
			case XmlValidatingReaderImpl.ParsingFunction.Error:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessCoreReaderEvent();
				return true;
			}
			this.validator.CompleteValidation();
			return false;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00058B78 File Offset: 0x00056D78
		public override void Close()
		{
			this.coreReader.Close();
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00058B8C File Offset: 0x00056D8C
		public override string LookupNamespace(string prefix)
		{
			return this.coreReaderImpl.LookupNamespace(prefix);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00058B9A File Offset: 0x00056D9A
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
			}
			if (!this.coreReader.ReadAttributeValue())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00058BD0 File Offset: 0x00056DD0
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00058C24 File Offset: 0x00056E24
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00058C78 File Offset: 0x00056E78
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00058CCC File Offset: 0x00056ECC
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00058D20 File Offset: 0x00056F20
		public override void ResolveEntity()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			}
			this.coreReader.ResolveEntity();
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00058D3D File Offset: 0x00056F3D
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00058D45 File Offset: 0x00056F45
		internal XmlReader OuterReader
		{
			get
			{
				return this.outerReader;
			}
			set
			{
				this.outerReader = value;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00058D4E File Offset: 0x00056F4E
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00058D84 File Offset: 0x00056F84
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00003242 File Offset: 0x00001442
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00058D92 File Offset: 0x00056F92
		public int LineNumber
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LineNumber;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00058DA4 File Offset: 0x00056FA4
		public int LinePosition
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LinePosition;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00058DB6 File Offset: 0x00056FB6
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00033FBF File Offset: 0x000321BF
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00058DBF File Offset: 0x00056FBF
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00058DC8 File Offset: 0x00056FC8
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.coreReaderNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00058DD6 File Offset: 0x00056FD6
		internal string LookupPrefix(string namespaceName)
		{
			return this.coreReaderNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000F2B RID: 3883 RVA: 0x00058DE4 File Offset: 0x00056FE4
		// (remove) Token: 0x06000F2C RID: 3884 RVA: 0x00058DF2 File Offset: 0x00056FF2
		internal event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandling.AddHandler(value);
			}
			remove
			{
				this.eventHandling.RemoveHandler(value);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00058E00 File Offset: 0x00057000
		internal object SchemaType
		{
			get
			{
				if (this.validationType == ValidationType.None)
				{
					return null;
				}
				XmlSchemaType xmlSchemaType = this.coreReaderImpl.InternalSchemaType as XmlSchemaType;
				if (xmlSchemaType != null && xmlSchemaType.QualifiedName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					return xmlSchemaType.Datatype;
				}
				return this.coreReaderImpl.InternalSchemaType;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00058E54 File Offset: 0x00057054
		internal XmlReader Reader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00058E5C File Offset: 0x0005705C
		internal XmlTextReaderImpl ReaderImpl
		{
			get
			{
				return this.coreReaderImpl;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00058E64 File Offset: 0x00057064
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00058E6C File Offset: 0x0005706C
		internal ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				if (this.ReadState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.validationType = value;
				this.SetupValidation(value);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00058E94 File Offset: 0x00057094
		internal XmlSchemaCollection Schemas
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00058E9C File Offset: 0x0005709C
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00058EA9 File Offset: 0x000570A9
		internal EntityHandling EntityHandling
		{
			get
			{
				return this.coreReaderImpl.EntityHandling;
			}
			set
			{
				this.coreReaderImpl.EntityHandling = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00058EB7 File Offset: 0x000570B7
		internal XmlResolver XmlResolver
		{
			set
			{
				this.coreReaderImpl.XmlResolver = value;
				this.validator.XmlResolver = value;
				this.schemaCollection.XmlResolver = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00058EDD File Offset: 0x000570DD
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00058EEA File Offset: 0x000570EA
		internal bool Namespaces
		{
			get
			{
				return this.coreReaderImpl.Namespaces;
			}
			set
			{
				this.coreReaderImpl.Namespaces = value;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00058EF8 File Offset: 0x000570F8
		public object ReadTypedValue()
		{
			if (this.validationType == ValidationType.None)
			{
				return null;
			}
			XmlNodeType nodeType = this.outerReader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.coreReaderImpl.InternalTypedValue;
				}
				if (nodeType == XmlNodeType.EndElement)
				{
					return null;
				}
				if (this.coreReaderImpl.V1Compat)
				{
					return null;
				}
				return this.Value;
			}
			else
			{
				if (this.SchemaType == null)
				{
					return null;
				}
				if (((this.SchemaType is XmlSchemaDatatype) ? ((XmlSchemaDatatype)this.SchemaType) : ((XmlSchemaType)this.SchemaType).Datatype) != null)
				{
					if (!this.outerReader.IsEmptyElement)
					{
						while (this.outerReader.Read())
						{
							XmlNodeType nodeType2 = this.outerReader.NodeType;
							if (nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace && nodeType2 != XmlNodeType.Comment && nodeType2 != XmlNodeType.ProcessingInstruction)
							{
								if (this.outerReader.NodeType != XmlNodeType.EndElement)
								{
									throw new XmlException("'{0}' is an invalid XmlNodeType.", this.outerReader.NodeType.ToString());
								}
								goto IL_00F3;
							}
						}
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
					}
					IL_00F3:
					return this.coreReaderImpl.InternalTypedValue;
				}
				return null;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00059020 File Offset: 0x00057220
		private void ParseDtdFromParserContext()
		{
			if (this.parserContext.DocTypeName == null || this.parserContext.DocTypeName.Length == 0)
			{
				return;
			}
			IDtdParser dtdParser = DtdParser.Create();
			XmlTextReaderImpl.DtdParserProxy dtdParserProxy = new XmlTextReaderImpl.DtdParserProxy(this.coreReaderImpl);
			IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset, dtdParserProxy);
			this.coreReaderImpl.SetDtdInfo(dtdInfo);
			this.ValidateDtd();
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000590B0 File Offset: 0x000572B0
		private void ValidateDtd()
		{
			IDtdInfo dtdInfo = this.coreReaderImpl.DtdInfo;
			if (dtdInfo != null)
			{
				switch (this.validationType)
				{
				case ValidationType.None:
				case ValidationType.DTD:
					break;
				case ValidationType.Auto:
					this.SetupValidation(ValidationType.DTD);
					break;
				default:
					return;
				}
				this.validator.DtdInfo = dtdInfo;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000590FC File Offset: 0x000572FC
		private void ResolveEntityInternally()
		{
			int depth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			while (this.outerReader.Read() && this.coreReader.Depth > depth)
			{
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0005913C File Offset: 0x0005733C
		private void SetupValidation(ValidationType valType)
		{
			this.validator = BaseValidator.CreateInstance(valType, this, this.schemaCollection, this.eventHandling, this.processIdentityConstraints);
			XmlResolver resolver = this.GetResolver();
			this.validator.XmlResolver = resolver;
			if (this.outerReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = ((resolver == null) ? new Uri(this.outerReader.BaseURI, UriKind.RelativeOrAbsolute) : resolver.ResolveUri(null, this.outerReader.BaseURI));
			}
			this.coreReaderImpl.ValidationEventHandling = ((this.validationType == ValidationType.None) ? null : this.eventHandling);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000591E0 File Offset: 0x000573E0
		private XmlResolver GetResolver()
		{
			XmlResolver resolver = this.coreReaderImpl.GetResolver();
			if (resolver == null && !this.coreReaderImpl.IsResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
			{
				if (XmlValidatingReaderImpl.s_tempResolver == null)
				{
					XmlValidatingReaderImpl.s_tempResolver = new XmlUrlResolver();
				}
				return XmlValidatingReaderImpl.s_tempResolver;
			}
			return resolver;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00059228 File Offset: 0x00057428
		private void ProcessCoreReaderEvent()
		{
			XmlNodeType nodeType = this.coreReader.NodeType;
			if (nodeType != XmlNodeType.EntityReference)
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.ValidateDtd();
					return;
				}
				if (nodeType == XmlNodeType.Whitespace && (this.coreReader.Depth > 0 || this.coreReaderImpl.FragmentType != XmlNodeType.Document) && this.validator.PreserveWhitespace)
				{
					this.coreReaderImpl.ChangeCurrentNodeType(XmlNodeType.SignificantWhitespace);
				}
			}
			else
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally;
			}
			this.coreReaderImpl.InternalSchemaType = null;
			this.coreReaderImpl.InternalTypedValue = null;
			this.validator.Validate();
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000592B9 File Offset: 0x000574B9
		internal void Close(bool closeStream)
		{
			this.coreReaderImpl.Close(closeStream);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x000592CE File Offset: 0x000574CE
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x000592D6 File Offset: 0x000574D6
		internal BaseValidator Validator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.validator = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000592DF File Offset: 0x000574DF
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.coreReaderImpl.NamespaceManager;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000592EC File Offset: 0x000574EC
		internal bool StandAlone
		{
			get
			{
				return this.coreReaderImpl.StandAlone;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x000592F9 File Offset: 0x000574F9
		internal object SchemaTypeObject
		{
			set
			{
				this.coreReaderImpl.InternalSchemaType = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00059307 File Offset: 0x00057507
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00059314 File Offset: 0x00057514
		internal object TypedValueObject
		{
			get
			{
				return this.coreReaderImpl.InternalTypedValue;
			}
			set
			{
				this.coreReaderImpl.InternalTypedValue = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00059322 File Offset: 0x00057522
		internal bool Normalization
		{
			get
			{
				return this.coreReaderImpl.Normalization;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0005932F File Offset: 0x0005752F
		internal bool AddDefaultAttribute(SchemaAttDef attdef)
		{
			return this.coreReaderImpl.AddDefaultAttributeNonDtd(attdef);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0005933D File Offset: 0x0005753D
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.coreReaderImpl.DtdInfo;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0005934C File Offset: 0x0005754C
		internal void ValidateDefaultAttributeOnUse(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader)
		{
			SchemaAttDef schemaAttDef = defaultAttribute as SchemaAttDef;
			if (schemaAttDef == null)
			{
				return;
			}
			if (!schemaAttDef.DefaultValueChecked)
			{
				SchemaInfo schemaInfo = coreReader.DtdInfo as SchemaInfo;
				if (schemaInfo == null)
				{
					return;
				}
				DtdValidator.CheckDefaultValue(schemaAttDef, schemaInfo, this.eventHandling, coreReader.BaseURI);
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0005938F File Offset: 0x0005758F
		public override Task<string> GetValueAsync()
		{
			return this.coreReader.GetValueAsync();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0005939C File Offset: 0x0005759C
		public override async Task<bool> ReadAsync()
		{
			switch (this.parsingFunction)
			{
			case XmlValidatingReaderImpl.ParsingFunction.Read:
				break;
			case XmlValidatingReaderImpl.ParsingFunction.Init:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessCoreReaderEvent();
					return true;
				}
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				await this.ParseDtdFromParserContextAsync().ConfigureAwait(false);
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				await this.ResolveEntityInternallyAsync().ConfigureAwait(false);
				break;
			case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				await this.readBinaryHelper.FinishAsync().ConfigureAwait(false);
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
			case XmlValidatingReaderImpl.ParsingFunction.Error:
				return false;
			default:
				return false;
			}
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.coreReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (configuredTaskAwaiter.GetResult())
			{
				this.ProcessCoreReaderEvent();
				flag = true;
			}
			else
			{
				this.validator.CompleteValidation();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000593E4 File Offset: 0x000575E4
		public override async Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
				}
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				int num2 = await this.readBinaryHelper.ReadContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00059444 File Offset: 0x00057644
		public override async Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
				}
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				int num2 = await this.readBinaryHelper.ReadContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000594A4 File Offset: 0x000576A4
		public override async Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
				}
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBase64Async(buffer, index, count).ConfigureAwait(false);
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00059504 File Offset: 0x00057704
		public override async Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			int num;
			if (this.ReadState != ReadState.Interactive)
			{
				num = 0;
			}
			else
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
				{
					this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
				}
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				int num2 = await this.readBinaryHelper.ReadElementContentAsBinHexAsync(buffer, index, count).ConfigureAwait(false);
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
				num = num2;
			}
			return num;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00059564 File Offset: 0x00057764
		internal async Task MoveOffEntityReferenceAsync()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000595AC File Offset: 0x000577AC
		public async Task<object> ReadTypedValueAsync()
		{
			object obj;
			if (this.validationType == ValidationType.None)
			{
				obj = null;
			}
			else
			{
				XmlNodeType nodeType = this.outerReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							if (this.coreReaderImpl.V1Compat)
							{
								obj = null;
							}
							else
							{
								obj = await this.GetValueAsync().ConfigureAwait(false);
							}
						}
						else
						{
							obj = null;
						}
					}
					else
					{
						obj = this.coreReaderImpl.InternalTypedValue;
					}
				}
				else if (this.SchemaType == null)
				{
					obj = null;
				}
				else if (((this.SchemaType is XmlSchemaDatatype) ? ((XmlSchemaDatatype)this.SchemaType) : ((XmlSchemaType)this.SchemaType).Datatype) != null)
				{
					if (!this.outerReader.IsEmptyElement)
					{
						for (;;)
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (!configuredTaskAwaiter.GetResult())
							{
								break;
							}
							XmlNodeType nodeType2 = this.outerReader.NodeType;
							if (nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace && nodeType2 != XmlNodeType.Comment && nodeType2 != XmlNodeType.ProcessingInstruction)
							{
								goto Block_15;
							}
						}
						throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
						Block_15:
						if (this.outerReader.NodeType != XmlNodeType.EndElement)
						{
							throw new XmlException("'{0}' is an invalid XmlNodeType.", this.outerReader.NodeType.ToString());
						}
					}
					obj = this.coreReaderImpl.InternalTypedValue;
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000595F4 File Offset: 0x000577F4
		private async Task ParseDtdFromParserContextAsync()
		{
			if (this.parserContext.DocTypeName != null && this.parserContext.DocTypeName.Length != 0)
			{
				IDtdParser dtdParser = DtdParser.Create();
				XmlTextReaderImpl.DtdParserProxy dtdParserProxy = new XmlTextReaderImpl.DtdParserProxy(this.coreReaderImpl);
				IDtdInfo dtdInfo = await dtdParser.ParseFreeFloatingDtdAsync(this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset, dtdParserProxy).ConfigureAwait(false);
				this.coreReaderImpl.SetDtdInfo(dtdInfo);
				this.ValidateDtd();
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0005963C File Offset: 0x0005783C
		private async Task ResolveEntityInternallyAsync()
		{
			int initialDepth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			do
			{
				configuredTaskAwaiter = this.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
			}
			while (configuredTaskAwaiter.GetResult() && this.coreReader.Depth > initialDepth);
		}

		// Token: 0x04000A64 RID: 2660
		private XmlReader coreReader;

		// Token: 0x04000A65 RID: 2661
		private XmlTextReaderImpl coreReaderImpl;

		// Token: 0x04000A66 RID: 2662
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x04000A67 RID: 2663
		private ValidationType validationType;

		// Token: 0x04000A68 RID: 2664
		private BaseValidator validator;

		// Token: 0x04000A69 RID: 2665
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000A6A RID: 2666
		private bool processIdentityConstraints;

		// Token: 0x04000A6B RID: 2667
		private XmlValidatingReaderImpl.ParsingFunction parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Init;

		// Token: 0x04000A6C RID: 2668
		private XmlValidatingReaderImpl.ValidationEventHandling eventHandling;

		// Token: 0x04000A6D RID: 2669
		private XmlParserContext parserContext;

		// Token: 0x04000A6E RID: 2670
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000A6F RID: 2671
		private XmlReader outerReader;

		// Token: 0x04000A70 RID: 2672
		private static XmlResolver s_tempResolver;

		// Token: 0x020001A2 RID: 418
		private enum ParsingFunction
		{
			// Token: 0x04000A72 RID: 2674
			Read,
			// Token: 0x04000A73 RID: 2675
			Init,
			// Token: 0x04000A74 RID: 2676
			ParseDtdFromContext,
			// Token: 0x04000A75 RID: 2677
			ResolveEntityInternally,
			// Token: 0x04000A76 RID: 2678
			InReadBinaryContent,
			// Token: 0x04000A77 RID: 2679
			ReaderClosed,
			// Token: 0x04000A78 RID: 2680
			Error,
			// Token: 0x04000A79 RID: 2681
			None
		}

		// Token: 0x020001A3 RID: 419
		internal class ValidationEventHandling : IValidationEventHandling
		{
			// Token: 0x06000F55 RID: 3925 RVA: 0x00059681 File Offset: 0x00057881
			internal ValidationEventHandling(XmlValidatingReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00059690 File Offset: 0x00057890
			object IValidationEventHandling.EventHandler
			{
				get
				{
					return this.eventHandler;
				}
			}

			// Token: 0x06000F57 RID: 3927 RVA: 0x00059698 File Offset: 0x00057898
			void IValidationEventHandling.SendEvent(Exception exception, XmlSeverityType severity)
			{
				if (this.eventHandler != null)
				{
					this.eventHandler(this.reader, new ValidationEventArgs((XmlSchemaException)exception, severity));
					return;
				}
				if (this.reader.ValidationType != ValidationType.None && severity == XmlSeverityType.Error)
				{
					throw exception;
				}
			}

			// Token: 0x06000F58 RID: 3928 RVA: 0x000596D2 File Offset: 0x000578D2
			internal void AddHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, handler);
			}

			// Token: 0x06000F59 RID: 3929 RVA: 0x000596EB File Offset: 0x000578EB
			internal void RemoveHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, handler);
			}

			// Token: 0x04000A7A RID: 2682
			private XmlValidatingReaderImpl reader;

			// Token: 0x04000A7B RID: 2683
			private ValidationEventHandler eventHandler;
		}
	}
}
