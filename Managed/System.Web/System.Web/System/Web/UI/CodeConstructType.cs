using System;

namespace System.Web.UI
{
	/// <summary>Specifies the code constructs that can be parsed in the <see cref="M:System.Web.UI.PageParserFilter.ProcessCodeConstruct(System.Web.UI.CodeConstructType,System.String)" /> method of the <see cref="T:System.Web.UI.PageParserFilter" /> class.</summary>
	// Token: 0x020001AF RID: 431
	public enum CodeConstructType
	{
		/// <summary>An expression in &lt;% ... %&gt; tags.</summary>
		// Token: 0x04001392 RID: 5010
		CodeSnippet,
		/// <summary>An expression in &lt;%# ... %&gt; tags.</summary>
		// Token: 0x04001393 RID: 5011
		ExpressionSnippet,
		/// <summary>An expression in &lt;%= ... %&gt; tags.</summary>
		// Token: 0x04001394 RID: 5012
		DataBindingSnippet,
		/// <summary>An expression in a script element that contains the runat="server" attribute.</summary>
		// Token: 0x04001395 RID: 5013
		ScriptTag,
		/// <summary>An expression in &lt;%: ... %&gt; tags.</summary>
		// Token: 0x04001396 RID: 5014
		EncodedExpressionSnippet
	}
}
