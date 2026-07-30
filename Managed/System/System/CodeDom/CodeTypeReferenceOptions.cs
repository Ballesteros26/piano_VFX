using System;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x0200074D RID: 1869
	[Flags]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x04002D46 RID: 11590
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x04002D47 RID: 11591
		GenericTypeParameter = 2
	}
}
