using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node.</summary>
	// Token: 0x020002A2 RID: 674
	public enum XmlNodeType
	{
		/// <summary>This is returned by the <see cref="T:System.Xml.XmlReader" /> if a Read method has not been called.</summary>
		// Token: 0x0400103B RID: 4155
		None,
		/// <summary>An element (for example, &lt;item&gt; ).</summary>
		// Token: 0x0400103C RID: 4156
		Element,
		/// <summary>An attribute (for example, id='123' ).</summary>
		// Token: 0x0400103D RID: 4157
		Attribute,
		/// <summary>The text content of a node.</summary>
		// Token: 0x0400103E RID: 4158
		Text,
		/// <summary>A CDATA section (for example, &lt;![CDATA[my escaped text]]&gt; ).</summary>
		// Token: 0x0400103F RID: 4159
		CDATA,
		/// <summary>A reference to an entity (for example, &amp;num; ).</summary>
		// Token: 0x04001040 RID: 4160
		EntityReference,
		/// <summary>An entity declaration (for example, &lt;!ENTITY...&gt; ).</summary>
		// Token: 0x04001041 RID: 4161
		Entity,
		/// <summary>A processing instruction (for example, &lt;?pi test?&gt; ).</summary>
		// Token: 0x04001042 RID: 4162
		ProcessingInstruction,
		/// <summary>A comment (for example, &lt;!-- my comment --&gt; ).</summary>
		// Token: 0x04001043 RID: 4163
		Comment,
		/// <summary>A document object that, as the root of the document tree, provides access to the entire XML document.</summary>
		// Token: 0x04001044 RID: 4164
		Document,
		/// <summary>The document type declaration, indicated by the following tag (for example, &lt;!DOCTYPE...&gt; ).</summary>
		// Token: 0x04001045 RID: 4165
		DocumentType,
		/// <summary>A document fragment.</summary>
		// Token: 0x04001046 RID: 4166
		DocumentFragment,
		/// <summary>A notation in the document type declaration (for example, &lt;!NOTATION...&gt; ).</summary>
		// Token: 0x04001047 RID: 4167
		Notation,
		/// <summary>White space between markup.</summary>
		// Token: 0x04001048 RID: 4168
		Whitespace,
		/// <summary>White space between markup in a mixed content model or white space within the xml:space="preserve" scope.</summary>
		// Token: 0x04001049 RID: 4169
		SignificantWhitespace,
		/// <summary>An end element tag (for example, &lt;/item&gt; ).</summary>
		// Token: 0x0400104A RID: 4170
		EndElement,
		/// <summary>Returned when XmlReader gets to the end of the entity replacement as a result of a call to <see cref="M:System.Xml.XmlReader.ResolveEntity" />.</summary>
		// Token: 0x0400104B RID: 4171
		EndEntity,
		/// <summary>The XML declaration (for example, &lt;?xml version='1.0'?&gt; ).</summary>
		// Token: 0x0400104C RID: 4172
		XmlDeclaration
	}
}
