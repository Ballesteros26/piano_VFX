using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers that indicate special features of a language.</summary>
	// Token: 0x020007B5 RID: 1973
	[Flags]
	public enum LanguageOptions
	{
		/// <summary>The language has default characteristics.</summary>
		// Token: 0x04002E75 RID: 11893
		None = 0,
		/// <summary>The language is case-insensitive.</summary>
		// Token: 0x04002E76 RID: 11894
		CaseInsensitive = 1
	}
}
