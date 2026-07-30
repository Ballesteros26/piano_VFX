using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines how a method is implemented.</summary>
	// Token: 0x0200088B RID: 2187
	[ComVisible(true)]
	[Serializable]
	public enum MethodCodeType
	{
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04002BCD RID: 11213
		IL,
		/// <summary>Specifies that the method is implemented in native code.</summary>
		// Token: 0x04002BCE RID: 11214
		Native,
		/// <summary>Specifies that the method implementation is in optimized intermediate language (OPTIL).</summary>
		// Token: 0x04002BCF RID: 11215
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04002BD0 RID: 11216
		Runtime
	}
}
