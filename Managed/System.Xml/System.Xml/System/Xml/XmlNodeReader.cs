using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached forward only access to XML data in an <see cref="T:System.Xml.XmlNode" />.</summary>
	// Token: 0x02000238 RID: 568
	public class XmlNodeReader : XmlReader, IXmlNamespaceResolver
	{
		/// <summary>Creates an instance of the XmlNodeReader class using the specified <see cref="T:System.Xml.XmlNode" />.</summary>
		/// <param name="node">The XmlNode you want to read. </param>
		// Token: 0x060015E6 RID: 5606 RVA: 0x0007B038 File Offset: 0x00079238
		public XmlNodeReader(XmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.readerNav = new XmlNodeReaderNavigator(node);
			this.curDepth = 0;
			this.readState = ReadState.Initial;
			this.fEOF = false;
			this.nodeType = XmlNodeType.None;
			this.bResolveEntity = false;
			this.bStartFromDocument = false;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0007B08F File Offset: 0x0007928F
		internal bool IsInReadingStates()
		{
			return this.readState == ReadState.Interactive;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0007B09A File Offset: 0x0007929A
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlNodeType.None;
				}
				return this.nodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0007B0AC File Offset: 0x000792AC
		public override string Name
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0007B0C7 File Offset: 0x000792C7
		public override string LocalName
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.LocalName;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0007B0E2 File Offset: 0x000792E2
		public override string NamespaceURI
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0007B0FD File Offset: 0x000792FD
		public override string Prefix
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlNodeReader.Value" />.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0007B118 File Offset: 0x00079318
		public override bool HasValue
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNodeReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0007B12F File Offset: 0x0007932F
		public override string Value
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0007B14A File Offset: 0x0007934A
		public override int Depth
		{
			get
			{
				return this.curDepth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0007B152 File Offset: 0x00079352
		public override string BaseURI
		{
			get
			{
				return this.readerNav.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. XmlNodeReader always returns true.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00003242 File Offset: 0x00001442
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlNodeReader.NodeType" /> equals XmlNodeType.Element) and it ends with /&gt;; otherwise, false.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0007B15F File Offset: 0x0007935F
		public override bool IsEmptyElement
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the document type definition (DTD) or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0007B176 File Offset: 0x00079376
		public override bool IsDefault
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsDefault;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0007B18D File Offset: 0x0007938D
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlSpace.None;
				}
				return this.readerNav.XmlSpace;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0007B1A4 File Offset: 0x000793A4
		public override string XmlLang
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.XmlLang;
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the schema information for the current node.</returns>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0007B1BF File Offset: 0x000793BF
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return null;
				}
				return this.readerNav.SchemaInfo;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0007B1D6 File Offset: 0x000793D6
		public override int AttributeCount
		{
			get
			{
				if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
				{
					return 0;
				}
				return this.readerNav.AttributeCount;
			}
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x060015F8 RID: 5624 RVA: 0x0007B1F7 File Offset: 0x000793F7
		public override string GetAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			return this.readerNav.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x060015F9 RID: 5625 RVA: 0x0007B210 File Offset: 0x00079410
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string text = ((namespaceURI == null) ? string.Empty : namespaceURI);
			return this.readerNav.GetAttribute(name, text);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="attributeIndex">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlNodeReader.AttributeCount" />. </exception>
		// Token: 0x060015FA RID: 5626 RVA: 0x0007B240 File Offset: 0x00079440
		public override string GetAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			return this.readerNav.GetAttribute(attributeIndex);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x060015FB RID: 5627 RVA: 0x0007B264 File Offset: 0x00079464
		public override bool MoveToAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.readerNav.MoveToAttribute(name))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x060015FC RID: 5628 RVA: 0x0007B2E4 File Offset: 0x000794E4
		public override bool MoveToAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			string text = ((namespaceURI == null) ? string.Empty : namespaceURI);
			if (this.readerNav.MoveToAttribute(name, text))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="attributeIndex">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x060015FD RID: 5629 RVA: 0x0007B370 File Offset: 0x00079570
		public override void MoveToAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			try
			{
				if (this.AttributeCount <= 0)
				{
					throw new ArgumentOutOfRangeException("attributeIndex");
				}
				this.readerNav.MoveToAttribute(attributeIndex);
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
			}
			catch
			{
				this.readerNav.RollBackMove(ref this.curDepth);
				throw;
			}
			this.curDepth++;
			this.nodeType = this.readerNav.NodeType;
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x060015FE RID: 5630 RVA: 0x0007B41C File Offset: 0x0007961C
		public override bool MoveToFirstAttribute()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.AttributeCount > 0)
			{
				this.readerNav.MoveToAttribute(0);
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x060015FF RID: 5631 RVA: 0x0007B4A0 File Offset: 0x000796A0
		public override bool MoveToNextAttribute()
		{
			if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToNextAttribute(ref this.curDepth))
			{
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06001600 RID: 5632 RVA: 0x0007B52C File Offset: 0x0007972C
		public override bool MoveToElement()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToElement())
			{
				this.curDepth--;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x06001601 RID: 5633 RVA: 0x0007B5B3 File Offset: 0x000797B3
		public override bool Read()
		{
			return this.Read(false);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0007B5BC File Offset: 0x000797BC
		private bool Read(bool fSkipChildren)
		{
			if (this.fEOF)
			{
				return false;
			}
			if (this.readState == ReadState.Initial)
			{
				if (this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment)
				{
					this.bStartFromDocument = true;
					if (!this.ReadNextNode(fSkipChildren))
					{
						this.readState = ReadState.Error;
						return false;
					}
				}
				this.ReSetReadingMarks();
				this.readState = ReadState.Interactive;
				this.nodeType = this.readerNav.NodeType;
				this.curDepth = 0;
				return true;
			}
			if (this.bInReadBinary)
			{
				this.FinishReadBinary();
			}
			if (this.readerNav.CreatedOnAttribute)
			{
				return false;
			}
			this.ReSetReadingMarks();
			if (this.ReadNextNode(fSkipChildren))
			{
				return true;
			}
			if (this.readState == ReadState.Initial || this.readState == ReadState.Interactive)
			{
				this.readState = ReadState.Error;
			}
			if (this.readState == ReadState.EndOfFile)
			{
				this.nodeType = XmlNodeType.None;
			}
			return false;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0007B690 File Offset: 0x00079890
		private bool ReadNextNode(bool fSkipChildren)
		{
			if (this.readState != ReadState.Interactive && this.readState != ReadState.Initial)
			{
				this.nodeType = XmlNodeType.None;
				return false;
			}
			bool flag = !fSkipChildren;
			XmlNodeType xmlNodeType = this.readerNav.NodeType;
			if (flag && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.EndEntity && (xmlNodeType == XmlNodeType.Element || (xmlNodeType == XmlNodeType.EntityReference && this.bResolveEntity) || ((this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment) && this.readState == ReadState.Initial)))
			{
				if (this.readerNav.MoveToFirstChild())
				{
					this.nodeType = this.readerNav.NodeType;
					this.curDepth++;
					if (this.bResolveEntity)
					{
						this.bResolveEntity = false;
					}
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
				{
					this.nodeType = XmlNodeType.EndElement;
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					this.bResolveEntity = false;
					this.nodeType = XmlNodeType.EndEntity;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
			else
			{
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					if (this.readerNav.MoveToFirstChild())
					{
						this.nodeType = this.readerNav.NodeType;
						this.curDepth++;
					}
					else
					{
						this.nodeType = XmlNodeType.EndEntity;
					}
					this.bResolveEntity = false;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0007B80F File Offset: 0x00079A0F
		private void SetEndOfFile()
		{
			this.fEOF = true;
			this.readState = ReadState.EndOfFile;
			this.nodeType = XmlNodeType.None;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0007B826 File Offset: 0x00079A26
		private bool ReadAtZeroLevel(bool fSkipChildren)
		{
			if (!fSkipChildren && this.nodeType != XmlNodeType.EndElement && this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			this.SetEndOfFile();
			return false;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0007B864 File Offset: 0x00079A64
		private bool ReadForward(bool fSkipChildren)
		{
			if (this.readState == ReadState.Error)
			{
				return false;
			}
			if (!this.bStartFromDocument && this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (this.readerNav.MoveToNext())
			{
				this.nodeType = this.readerNav.NodeType;
				return true;
			}
			if (this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (!this.readerNav.MoveToParent())
			{
				return false;
			}
			if (this.readerNav.NodeType == XmlNodeType.Element)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			if (this.readerNav.NodeType == XmlNodeType.EntityReference)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndEntity;
				return true;
			}
			return true;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0007B920 File Offset: 0x00079B20
		private void ReSetReadingMarks()
		{
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0007B939 File Offset: 0x00079B39
		public override bool EOF
		{
			get
			{
				return this.readState != ReadState.Closed && this.fEOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlNodeReader.ReadState" /> to Closed.</summary>
		// Token: 0x06001609 RID: 5641 RVA: 0x0007B94C File Offset: 0x00079B4C
		public override void Close()
		{
			this.readState = ReadState.Closed;
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0007B955 File Offset: 0x00079B55
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x0600160B RID: 5643 RVA: 0x0007B95D File Offset: 0x00079B5D
		public override void Skip()
		{
			this.Read(true);
		}

		/// <summary>Reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or text-like node (This can include CDATA, Text nodes, and so on). This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		// Token: 0x0600160C RID: 5644 RVA: 0x0007B967 File Offset: 0x00079B67
		public override string ReadString()
		{
			if (this.NodeType == XmlNodeType.EntityReference && this.bResolveEntity && !this.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0002A957 File Offset: 0x00028B57
		public override bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0007B998 File Offset: 0x00079B98
		public override XmlNameTable NameTable
		{
			get
			{
				return this.readerNav.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		// Token: 0x0600160F RID: 5647 RVA: 0x0007B9A8 File Offset: 0x00079BA8
		public override string LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				return null;
			}
			return text;
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06001610 RID: 5648 RVA: 0x0007B9DA File Offset: 0x00079BDA
		public override void ResolveEntity()
		{
			if (!this.IsInReadingStates() || this.nodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("The node is not an expandable 'EntityReference' node."));
			}
			this.bResolveEntity = true;
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x06001611 RID: 5649 RVA: 0x0007BA04 File Offset: 0x00079C04
		public override bool ReadAttributeValue()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			if (this.readerNav.ReadAttributeValue(ref this.curDepth, ref this.bResolveEntity, ref this.nodeType))
			{
				this.bInReadBinary = false;
				return true;
			}
			return false;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlNodeReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlNodeReader" /> class always returns true.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00003242 File Offset: 0x00001442
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
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06001613 RID: 5651 RVA: 0x0007BA3C File Offset: 0x00079C3C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlNodeReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06001614 RID: 5652 RVA: 0x0007BA8C File Offset: 0x00079C8C
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06001615 RID: 5653 RVA: 0x0007BADC File Offset: 0x00079CDC
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06001616 RID: 5654 RVA: 0x0007BB2C File Offset: 0x00079D2C
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return num;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0007BB7A File Offset: 0x00079D7A
		private void FinishReadBinary()
		{
			this.bInReadBinary = false;
			this.readBinaryHelper.Finish();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.IDictionary" /> object that contains the namespaces that are in scope.</returns>
		/// <param name="scope">
		///   <see cref="T:System.Xml.XmlNamespaceScope" /> object.</param>
		// Token: 0x06001618 RID: 5656 RVA: 0x0007BB8E File Offset: 0x00079D8E
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerNav.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>
		///   <see cref="T:System.String" /> object that contains the namespace prefix.</returns>
		/// <param name="namespaceName">
		///   <see cref="T:System.String" /> object that identifies the namespace.</param>
		// Token: 0x06001619 RID: 5657 RVA: 0x0007BB9C File Offset: 0x00079D9C
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerNav.LookupPrefix(namespaceName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <returns>
		///   <see cref="T:System.String" /> that contains the namespace name.</returns>
		/// <param name="prefix">
		///   <see cref="T:System.String" /> that contains the namespace prefix.</param>
		// Token: 0x0600161A RID: 5658 RVA: 0x0007BBAC File Offset: 0x00079DAC
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return this.readerNav.DefaultLookupNamespace(prefix);
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.readerNav.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0007BBF1 File Offset: 0x00079DF1
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.readerNav.Document.DtdSchemaInfo;
			}
		}

		// Token: 0x04000E1C RID: 3612
		private XmlNodeReaderNavigator readerNav;

		// Token: 0x04000E1D RID: 3613
		private XmlNodeType nodeType;

		// Token: 0x04000E1E RID: 3614
		private int curDepth;

		// Token: 0x04000E1F RID: 3615
		private ReadState readState;

		// Token: 0x04000E20 RID: 3616
		private bool fEOF;

		// Token: 0x04000E21 RID: 3617
		private bool bResolveEntity;

		// Token: 0x04000E22 RID: 3618
		private bool bStartFromDocument;

		// Token: 0x04000E23 RID: 3619
		private bool bInReadBinary;

		// Token: 0x04000E24 RID: 3620
		private ReadContentAsBinaryHelper readBinaryHelper;
	}
}
