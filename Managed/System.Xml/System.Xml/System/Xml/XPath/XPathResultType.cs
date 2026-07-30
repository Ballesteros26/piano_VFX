using System;

namespace System.Xml.XPath
{
	/// <summary>Specifies the return type of the XPath expression.</summary>
	// Token: 0x020002B5 RID: 693
	public enum XPathResultType
	{
		/// <summary>A numeric value.</summary>
		// Token: 0x0400153E RID: 5438
		Number,
		/// <summary>A <see cref="T:System.String" /> value.</summary>
		// Token: 0x0400153F RID: 5439
		String,
		/// <summary>A <see cref="T:System.Boolean" />true or false value.</summary>
		// Token: 0x04001540 RID: 5440
		Boolean,
		/// <summary>A node collection.</summary>
		// Token: 0x04001541 RID: 5441
		NodeSet,
		/// <summary>A tree fragment.</summary>
		// Token: 0x04001542 RID: 5442
		Navigator = 1,
		/// <summary>Any of the XPath node types.</summary>
		// Token: 0x04001543 RID: 5443
		Any = 5,
		/// <summary>The expression does not evaluate to the correct XPath type.</summary>
		// Token: 0x04001544 RID: 5444
		Error
	}
}
