using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DESCKIND" /> instead.</summary>
	// Token: 0x02000911 RID: 2321
	[Obsolete]
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x04002D90 RID: 11664
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> was returned.</summary>
		// Token: 0x04002D91 RID: 11665
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a VARDESC was returned.</summary>
		// Token: 0x04002D92 RID: 11666
		DESCKIND_VARDESC,
		/// <summary>Indicates that a TYPECOMP was returned.</summary>
		// Token: 0x04002D93 RID: 11667
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an IMPLICITAPPOBJ was returned.</summary>
		// Token: 0x04002D94 RID: 11668
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end of enumeration marker.</summary>
		// Token: 0x04002D95 RID: 11669
		DESCKIND_MAX
	}
}
