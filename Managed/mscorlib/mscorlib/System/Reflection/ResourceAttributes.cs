using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the attributes for a manifest resource.</summary>
	// Token: 0x020002FF RID: 767
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ResourceAttributes
	{
		/// <summary>A mask used to retrieve public manifest resources.</summary>
		// Token: 0x040012AC RID: 4780
		Public = 1,
		/// <summary>A mask used to retrieve private manifest resources.</summary>
		// Token: 0x040012AD RID: 4781
		Private = 2
	}
}
