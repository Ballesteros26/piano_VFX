using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of the code block.</summary>
	// Token: 0x02000788 RID: 1928
	public enum CodeBlockType
	{
		/// <summary>Indicates that the block type is for code. A <see cref="F:System.Web.UI.CodeBlockType.Code" /> block type is contained inside the &lt;%   %&gt; characters.</summary>
		// Token: 0x040025ED RID: 9709
		Code,
		/// <summary>Indicates that the block type is for data binding. A <see cref="F:System.Web.UI.CodeBlockType.DataBinding" /> block type is contained inside the &lt;%#   %&gt; characters.</summary>
		// Token: 0x040025EE RID: 9710
		DataBinding = 2,
		/// <summary>Indicates that the block type is for encoded expressions. A <see cref="F:System.Web.UI.CodeBlockType.EncodedExpression" /> block type is contained inside the &lt;%:   %&gt; characters.</summary>
		// Token: 0x040025EF RID: 9711
		EncodedExpression,
		/// <summary>Indicates that the block type is for data expressions. A <see cref="F:System.Web.UI.CodeBlockType.Expression" /> block type is contained inside the &lt;%=   %&gt; characters.</summary>
		// Token: 0x040025F0 RID: 9712
		Expression = 1
	}
}
