using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the XPath node types that can be returned from the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x020002C4 RID: 708
	public enum XPathNodeType
	{
		/// <summary>The root node of the XML document or node tree.</summary>
		// Token: 0x04001572 RID: 5490
		Root,
		/// <summary>An element, such as &lt;element&gt;.</summary>
		// Token: 0x04001573 RID: 5491
		Element,
		/// <summary>An attribute, such as id='123'.</summary>
		// Token: 0x04001574 RID: 5492
		Attribute,
		/// <summary>A namespace, such as xmlns="namespace".</summary>
		// Token: 0x04001575 RID: 5493
		Namespace,
		/// <summary>The text content of a node. Equivalent to the Document Object Model (DOM) Text and CDATA node types. Contains at least one character.</summary>
		// Token: 0x04001576 RID: 5494
		Text,
		/// <summary>A node with white space characters and xml:space set to preserve.</summary>
		// Token: 0x04001577 RID: 5495
		SignificantWhitespace,
		/// <summary>A node with only white space characters and no significant white space. White space characters are #x20, #x9, #xD, or #xA.</summary>
		// Token: 0x04001578 RID: 5496
		Whitespace,
		/// <summary>A processing instruction, such as &lt;?pi test?&gt;. This does not include XML declarations, which are not visible to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class. </summary>
		// Token: 0x04001579 RID: 5497
		ProcessingInstruction,
		/// <summary>A comment, such as &lt;!-- my comment --&gt;</summary>
		// Token: 0x0400157A RID: 5498
		Comment,
		/// <summary>Any of the <see cref="T:System.Xml.XPath.XPathNodeType" /> node types.</summary>
		// Token: 0x0400157B RID: 5499
		All
	}
}
