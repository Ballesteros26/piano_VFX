using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides document type definition (DTD), XML-Data Reduced (XDR) schema, and XML Schema definition language (XSD) validation.</summary>
	// Token: 0x020001A0 RID: 416
	[Obsolete("Use XmlReader created by XmlReader.Create() method using appropriate XmlReaderSettings instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlValidatingReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the XmlValidatingReader class that validates the content returned from the given <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader to read from while validating. The current implementation supports only <see cref="T:System.Xml.XmlTextReader" />. </param>
		/// <exception cref="T:System.ArgumentException">The reader specified is not an XmlTextReader. </exception>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00058202 File Offset: 0x00056402
		public XmlValidatingReader(XmlReader reader)
		{
			this.impl = new XmlValidatingReaderImpl(reader);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the XmlValidatingReader class with the specified values.</summary>
		/// <param name="xmlFragment">The string containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment string can contain (see table below). </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the XML fragment is to be parsed. This includes the <see cref="T:System.Xml.NameTable" /> to use, encoding, namespace scope, current xml:lang, and xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not one of the node types listed in the table below. </exception>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x00058222 File Offset: 0x00056422
		public XmlValidatingReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			if (xmlFragment == null)
			{
				throw new ArgumentNullException("xmlFragment");
			}
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the XmlValidatingReader class with the specified values.</summary>
		/// <param name="xmlFragment">The stream containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This determines what the fragment can contain (see table below). </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the XML fragment is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, current xml:lang, and xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="fragType" /> is not one of the node types listed in the table below. </exception>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00058252 File Offset: 0x00056452
		public XmlValidatingReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			if (xmlFragment == null)
			{
				throw new ArgumentNullException("xmlFragment");
			}
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00058282 File Offset: 0x00056482
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlValidatingReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0005828F File Offset: 0x0005648F
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0005829C File Offset: 0x0005649C
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		/// <summary>Gets the namespace Uniform Resource Identifier (URI) (as defined in the World Wide Web Consortium (W3C) Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000582A9 File Offset: 0x000564A9
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000582B6 File Offset: 0x000564B6
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlValidatingReader.Value" /> other than String.Empty.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000582C3 File Offset: 0x000564C3
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlValidatingReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000582D0 File Offset: 0x000564D0
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000582DD File Offset: 0x000564DD
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000582EA File Offset: 0x000564EA
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlValidatingReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x000582F7 File Offset: 0x000564F7
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the document type definition (DTD) or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00058304 File Offset: 0x00056504
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00058311 File Offset: 0x00056511
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0005831E File Offset: 0x0005651E
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0005832B File Offset: 0x0005652B
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00058338 File Offset: 0x00056538
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
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00058345 File Offset: 0x00056545
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00058353 File Offset: 0x00056553
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlValidatingReader.AttributeCount" />. </exception>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00058362 File Offset: 0x00056562
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00058370 File Offset: 0x00056570
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0005837E File Offset: 0x0005657E
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000ECA RID: 3786 RVA: 0x0005838D File Offset: 0x0005658D
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x06000ECB RID: 3787 RVA: 0x0005839B File Offset: 0x0005659B
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x06000ECC RID: 3788 RVA: 0x000583A8 File Offset: 0x000565A8
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000ECD RID: 3789 RVA: 0x000583B5 File Offset: 0x000565B5
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06000ECE RID: 3790 RVA: 0x000583C2 File Offset: 0x000565C2
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x06000ECF RID: 3791 RVA: 0x000583CF File Offset: 0x000565CF
		public override bool Read()
		{
			return this.impl.Read();
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000583DC File Offset: 0x000565DC
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x000583E9 File Offset: 0x000565E9
		public override void Close()
		{
			this.impl.Close();
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000583F6 File Offset: 0x000565F6
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>XmlNameTable that enables you to get the atomized version of a string within the node.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00058403 File Offset: 0x00056603
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace Uniform Resource Identifier (URI) you want to resolve. To match the default namespace, pass an empty string. </param>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00058410 File Offset: 0x00056610
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
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. XmlValidatingReader always returns true.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00058438 File Offset: 0x00056638
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlValidatingReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlValidatingReader" /> class returns true.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00003242 File Offset: 0x00001442
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
		///   <see cref="M:System.Xml.XmlValidatingReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00058445 File Offset: 0x00056645
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
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x00058455 File Offset: 0x00056655
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
		///   <see cref="M:System.Xml.XmlValidatingReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		// Token: 0x06000EDA RID: 3802 RVA: 0x00058465 File Offset: 0x00056665
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
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlValidatingReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000EDB RID: 3803 RVA: 0x00058475 File Offset: 0x00056675
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		/// <summary>Reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.NoteThe text node can be either an element or an attribute text node.</returns>
		// Token: 0x06000EDC RID: 3804 RVA: 0x00058485 File Offset: 0x00056685
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if the class can return line information; otherwise, false.</returns>
		// Token: 0x06000EDD RID: 3805 RVA: 0x00003242 File Offset: 0x00001442
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number. The starting value for this property is 1.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00058498 File Offset: 0x00056698
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position. The starting value for this property is 1.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000584A5 File Offset: 0x000566A5
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>An T:System.Collections.IDictionary object that identifies the namespaces in scope.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> object that identifies the scope of the reader.</param>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x000584B2 File Offset: 0x000566B2
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <returns>A string value that contains the namespace Uri that is associated with the prefix.</returns>
		/// <param name="prefix">The namespace prefix.</param>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x000584C0 File Offset: 0x000566C0
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>A string value that contains the namespace prefix that is associated with the <paramref name="namespaceName" />.</returns>
		/// <param name="namespaceName">The namespace that is associated with the prefix.</param>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x000584CE File Offset: 0x000566CE
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		/// <summary>Sets an event handler for receiving information about document type definition (DTD), XML-Data Reduced (XDR) schema, and XML Schema definition language (XSD) schema validation errors.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000EE3 RID: 3811 RVA: 0x000584DC File Offset: 0x000566DC
		// (remove) Token: 0x06000EE4 RID: 3812 RVA: 0x000584EA File Offset: 0x000566EA
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.impl.ValidationEventHandler += value;
			}
			remove
			{
				this.impl.ValidationEventHandler -= value;
			}
		}

		/// <summary>Gets a schema type object.</summary>
		/// <returns>
		///   <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />, or <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> depending whether the node value is a built in XML Schema definition language (XSD) type or a user defined simpleType or complexType; null if the current node has no schema type.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000584F8 File Offset: 0x000566F8
		public object SchemaType
		{
			get
			{
				return this.impl.SchemaType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReader" /> used to construct this XmlValidatingReader.</summary>
		/// <returns>The XmlReader specified in the constructor.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00058505 File Offset: 0x00056705
		public XmlReader Reader
		{
			get
			{
				return this.impl.Reader;
			}
		}

		/// <summary>Gets or sets a value indicating the type of validation to perform.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values. If this property is not set, it defaults to ValidationType.Auto.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting the property after a Read has been called. </exception>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00058512 File Offset: 0x00056712
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0005851F File Offset: 0x0005671F
		public ValidationType ValidationType
		{
			get
			{
				return this.impl.ValidationType;
			}
			set
			{
				this.impl.ValidationType = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Xml.Schema.XmlSchemaCollection" /> to use for validation.</summary>
		/// <returns>The XmlSchemaCollection to use for validation.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0005852D File Offset: 0x0005672D
		public XmlSchemaCollection Schemas
		{
			get
			{
				return this.impl.Schemas;
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no EntityHandling is specified, it defaults to EntityHandling.ExpandEntities.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value was specified. </exception>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0005853A File Offset: 0x0005673A
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00058547 File Offset: 0x00056747
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

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving external document type definition (DTD) and schema location references. The XmlResolver is also used to handle any import or include elements found in XML Schema definition language (XSD) schemas.</summary>
		/// <returns>The XmlResolver to use. If set to null, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted to specify an XmlResolver.</returns>
		// Token: 0x1700027C RID: 636
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00058555 File Offset: 0x00056755
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00058563 File Offset: 0x00056763
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x00058570 File Offset: 0x00056770
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

		/// <summary>Gets the common language runtime type for the specified XML Schema definition language (XSD) type.</summary>
		/// <returns>The common language runtime type for the specified XML Schema type.</returns>
		// Token: 0x06000EEF RID: 3823 RVA: 0x0005857E File Offset: 0x0005677E
		public object ReadTypedValue()
		{
			return this.impl.ReadTypedValue();
		}

		/// <summary>Gets the encoding attribute for the document.</summary>
		/// <returns>The encoding value. If no encoding attribute exists, and there is not byte-order mark, this defaults to UTF-8.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0005858B File Offset: 0x0005678B
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00058598 File Offset: 0x00056798
		internal XmlValidatingReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x000585A0 File Offset: 0x000567A0
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x04000A63 RID: 2659
		private XmlValidatingReaderImpl impl;
	}
}
