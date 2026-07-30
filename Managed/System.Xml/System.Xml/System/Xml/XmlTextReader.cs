using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached, forward-only access to XML data.</summary>
	// Token: 0x0200011C RID: 284
	[EditorBrowsable(EditorBrowsableState.Never)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlTextReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the XmlTextReader.</summary>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00031ADD File Offset: 0x0002FCDD
		protected XmlTextReader()
		{
			this.impl = new XmlTextReaderImpl();
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nt">The XmlNameTable to use. </param>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00031AFC File Offset: 0x0002FCFC
		protected XmlTextReader(XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		// Token: 0x06000AAA RID: 2730 RVA: 0x00031B1C File Offset: 0x0002FD1C
		public XmlTextReader(Stream input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and stream.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		// Token: 0x06000AAB RID: 2731 RVA: 0x00031B3C File Offset: 0x0002FD3C
		public XmlTextReader(string url, Stream input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000AAC RID: 2732 RVA: 0x00031B5D File Offset: 0x0002FD5D
		public XmlTextReader(Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is null, BaseURI is set to String.Empty. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000AAD RID: 2733 RVA: 0x00031B7E File Offset: 0x0002FD7E
		public XmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		// Token: 0x06000AAE RID: 2734 RVA: 0x00031BA0 File Offset: 0x0002FDA0
		public XmlTextReader(TextReader input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x00031BC0 File Offset: 0x0002FDC0
		public XmlTextReader(string url, TextReader input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00031BE1 File Offset: 0x0002FDE1
		public XmlTextReader(TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is null, BaseURI is set to String.Empty. </param>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00031C02 File Offset: 0x0002FE02
		public XmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The stream containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not an Element, Attribute, or Document XmlNodeType. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xmlFragment" /> is null. </exception>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00031C24 File Offset: 0x0002FE24
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified string, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The string containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment string can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not an Element, Attribute, or DocumentXmlNodeType. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xmlFragment" /> is null. </exception>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00031C46 File Offset: 0x0002FE46
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file.</summary>
		/// <param name="url">The URL for the file containing the XML data. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00031C68 File Offset: 0x0002FE68
		public XmlTextReader(string url)
		{
			this.impl = new XmlTextReaderImpl(url, new NameTable());
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL for the file containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00031C8D File Offset: 0x0002FE8D
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00031CAE File Offset: 0x0002FEAE
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00031CBB File Offset: 0x0002FEBB
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00031CC8 File Offset: 0x0002FEC8
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00031CD5 File Offset: 0x0002FED5
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00031CE2 File Offset: 0x0002FEE2
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlTextReader.Value" /> other than String.Empty.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00031CEF File Offset: 0x0002FEEF
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space within an xml:space= 'preserve' scope. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00031CFC File Offset: 0x0002FEFC
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00031D09 File Offset: 0x0002FF09
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00031D16 File Offset: 0x0002FF16
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlTextReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00031D23 File Offset: 0x0002FF23
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>This property always returns false. (<see cref="T:System.Xml.XmlTextReader" /> does not expand default attributes.) </returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00031D30 File Offset: 0x0002FF30
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00031D3D File Offset: 0x0002FF3D
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00031D4A File Offset: 0x0002FF4A
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00031D57 File Offset: 0x0002FF57
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00031D64 File Offset: 0x0002FF64
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00031D71 File Offset: 0x0002FF71
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x00031D7F File Offset: 0x0002FF7F
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlTextReader.AttributeCount" />. </exception>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00031D8E File Offset: 0x0002FF8E
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00031D9C File Offset: 0x0002FF9C
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00031DAA File Offset: 0x0002FFAA
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000ACA RID: 2762 RVA: 0x00031DB9 File Offset: 0x0002FFB9
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x06000ACB RID: 2763 RVA: 0x00031DC7 File Offset: 0x0002FFC7
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000ACC RID: 2764 RVA: 0x00031DD4 File Offset: 0x0002FFD4
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000ACD RID: 2765 RVA: 0x00031DE1 File Offset: 0x0002FFE1
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000ACE RID: 2766 RVA: 0x00031DEE File Offset: 0x0002FFEE
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		// Token: 0x06000ACF RID: 2767 RVA: 0x00031DFB File Offset: 0x0002FFFB
		public override bool Read()
		{
			return this.impl.Read();
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00031E08 File Offset: 0x00030008
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x00031E15 File Offset: 0x00030015
		public override void Close()
		{
			this.impl.Close();
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00031E22 File Offset: 0x00030022
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x00031E2F File Offset: 0x0003002F
		public override void Skip()
		{
			this.impl.Skip();
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00031E3C File Offset: 0x0003003C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Xml.XmlTextReader.Namespaces" /> property is set to true and the <paramref name="prefix" /> value is null. </exception>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x00031E4C File Offset: 0x0003004C
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. The XmlTextReader class always returns true.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00031E74 File Offset: 0x00030074
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns true.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported in the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00031E81 File Offset: 0x00030081
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00031E91 File Offset: 0x00030091
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		// Token: 0x06000ADB RID: 2779 RVA: 0x00031EA1 File Offset: 0x000300A1
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000ADC RID: 2780 RVA: 0x00031EB1 File Offset: 0x000300B1
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns false.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads the contents of an element or a text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was attempted. </exception>
		// Token: 0x06000ADE RID: 2782 RVA: 0x00031EC1 File Offset: 0x000300C1
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if the class can return line information; otherwise, false.</returns>
		// Token: 0x06000ADF RID: 2783 RVA: 0x00003242 File Offset: 0x00001442
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00031ED4 File Offset: 0x000300D4
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00031EE1 File Offset: 0x000300E1
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains the current in-scope namespaces.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x00031EEE File Offset: 0x000300EE
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <returns>The namespace URI that is mapped to the prefix; null if the prefix is not mapped to a namespace URI.</returns>
		/// <param name="prefix">The prefix whose namespace URI you wish to find.</param>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00031EFC File Offset: 0x000300FC
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>The prefix that is mapped to the namespace URI; null if the namespace URI is not mapped to a prefix.</returns>
		/// <param name="namespaceName">The namespace URI whose prefix you wish to find.</param>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x00031F0A File Offset: 0x0003010A
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		/// <summary>Gets a collection that contains all namespaces currently in-scope.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> object that contains all the current in-scope namespaces. If the reader is not positioned on an element, an empty dictionary (no namespaces) is returned.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x00031EEE File Offset: 0x000300EE
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property after a read operation has occurred (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is not ReadState.Initial). </exception>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00031F18 File Offset: 0x00030118
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00031F25 File Offset: 0x00030125
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
			set
			{
				this.impl.Namespaces = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize white space and attribute values.</summary>
		/// <returns>true to normalize; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00031F33 File Offset: 0x00030133
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00031F40 File Offset: 0x00030140
		public bool Normalization
		{
			get
			{
				return this.impl.Normalization;
			}
			set
			{
				this.impl.Normalization = value;
			}
		}

		/// <summary>Gets the encoding of the document.</summary>
		/// <returns>The encoding value. If no encoding attribute exists, and there is no byte-order mark, this defaults to UTF-8.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00031F4E File Offset: 0x0003014E
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		/// <summary>Gets or sets a value that specifies how white space is handled.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WhitespaceHandling" /> values. The default is WhitespaceHandling.All (returns Whitespace and SignificantWhitespace nodes).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00031F5B File Offset: 0x0003015B
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00031F68 File Offset: 0x00030168
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.impl.WhitespaceHandling;
			}
			set
			{
				this.impl.WhitespaceHandling = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to allow DTD processing. This property is obsolete. Use <see cref="P:System.Xml.XmlTextReader.DtdProcessing" /> instead.</summary>
		/// <returns>true to disallow DTD processing; otherwise false. The default is false.</returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00031F76 File Offset: 0x00030176
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00031F86 File Offset: 0x00030186
		[Obsolete("Use DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.impl.DtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.impl.DtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.DtdProcessing" /> enumeration.</summary>
		/// <returns>The <see cref="T:System.Xml.DtdProcessing" /> enumeration.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00031F9A File Offset: 0x0003019A
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00031FA7 File Offset: 0x000301A7
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.impl.DtdProcessing;
			}
			set
			{
				this.impl.DtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no EntityHandling is specified, it defaults to EntityHandling.ExpandCharEntities.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00031FB5 File Offset: 0x000301B5
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x00031FC2 File Offset: 0x000301C2
		public EntityHandling EntityHandling
		{
			get
			{
				return this.impl.EntityHandling;
			}
			set
			{
				this.impl.EntityHandling = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving DTD references.</summary>
		/// <returns>The XmlResolver to use. If set to null, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted in order to specify an XmlResolver.</returns>
		// Token: 0x170001F5 RID: 501
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00031FD0 File Offset: 0x000301D0
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		/// <summary>Resets the state of the reader to ReadState.Initial.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling ResetState if the reader was constructed using an <see cref="T:System.Xml.XmlParserContext" />. </exception>
		/// <exception cref="T:System.Xml.XmlException">Documents in a single stream do not share the same encoding.</exception>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00031FDE File Offset: 0x000301DE
		public void ResetState()
		{
			this.impl.ResetState();
		}

		/// <summary>Gets the remainder of the buffered XML.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> containing the remainder of the buffered XML.</returns>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x00031FEB File Offset: 0x000301EB
		public TextReader GetRemainder()
		{
			return this.impl.GetRemainder();
		}

		/// <summary>Reads the text contents of an element into a character buffer. This method is designed to read large streams of embedded text by calling it successively.</summary>
		/// <returns>The number of characters read. This can be 0 if the reader is not positioned on an element or if there is no more text content to return in the current context.</returns>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="index">The position within <paramref name="buffer" /> where the method can begin writing text contents. </param>
		/// <param name="count">The number of characters to write into <paramref name="buffer" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the space specified in the <paramref name="buffer" /> (buffer size - <paramref name="index" />). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />&lt; 0 or <paramref name="count" />&lt; 0. </exception>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x00031FF8 File Offset: 0x000301F8
		public int ReadChars(char[] buffer, int index, int count)
		{
			return this.impl.ReadChars(buffer, index, count);
		}

		/// <summary>Decodes Base64 and returns the decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="array">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <exception cref="T:System.Xml.XmlException">The Base64 sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00032008 File Offset: 0x00030208
		public int ReadBase64(byte[] array, int offset, int len)
		{
			return this.impl.ReadBase64(array, offset, len);
		}

		/// <summary>Decodes BinHex and returns the decoded binary bytes.</summary>
		/// <returns>The number of bytes written to your buffer.</returns>
		/// <param name="array">The byte array that serves as the buffer to which the decoded binary bytes are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <exception cref="T:System.Xml.XmlException">The BinHex sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x00032018 File Offset: 0x00030218
		public int ReadBinHex(byte[] array, int offset, int len)
		{
			return this.impl.ReadBinHex(array, offset, len);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00032028 File Offset: 0x00030228
		internal XmlTextReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00032030 File Offset: 0x00030230
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.impl.NamespaceManager;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0003203D File Offset: 0x0003023D
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.impl.XmlValidatingReaderCompatibilityMode = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0003204B File Offset: 0x0003024B
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x04000646 RID: 1606
		private XmlTextReaderImpl impl;
	}
}
