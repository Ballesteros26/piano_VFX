using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether white space should be ignored in the base 64 transformation.</summary>
	// Token: 0x02000648 RID: 1608
	[ComVisible(true)]
	[Serializable]
	public enum FromBase64TransformMode
	{
		/// <summary>White space should be ignored.</summary>
		// Token: 0x040023D1 RID: 9169
		IgnoreWhiteSpaces,
		/// <summary>White space should not be ignored.</summary>
		// Token: 0x040023D2 RID: 9170
		DoNotIgnoreWhiteSpaces
	}
}
