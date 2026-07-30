using System;

namespace System.Web.UI
{
	/// <summary>Provides access to the <see cref="T:System.Web.UI.CodeBlockType" /> of a code block builder.</summary>
	// Token: 0x0200078A RID: 1930
	public interface ICodeBlockTypeAccessor
	{
		/// <summary>Gets the type of code block.</summary>
		/// <returns>The type of code block.</returns>
		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06004E46 RID: 20038
		CodeBlockType BlockType { get; }
	}
}
