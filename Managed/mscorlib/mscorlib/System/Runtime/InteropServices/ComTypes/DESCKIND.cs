using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the type description being bound to.</summary>
	// Token: 0x02000986 RID: 2438
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x04002E3D RID: 11837
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure was returned.</summary>
		// Token: 0x04002E3E RID: 11838
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a VARDESC was returned.</summary>
		// Token: 0x04002E3F RID: 11839
		DESCKIND_VARDESC,
		/// <summary>Indicates that a TYPECOMP was returned.</summary>
		// Token: 0x04002E40 RID: 11840
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an IMPLICITAPPOBJ was returned.</summary>
		// Token: 0x04002E41 RID: 11841
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end-of-enumeration marker.</summary>
		// Token: 0x04002E42 RID: 11842
		DESCKIND_MAX
	}
}
