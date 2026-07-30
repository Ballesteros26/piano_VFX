using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the platform targeted by an executable.</summary>
	// Token: 0x0200031B RID: 795
	[ComVisible(true)]
	[Serializable]
	public enum ImageFileMachine
	{
		/// <summary>Targets a 32-bit Intel processor.</summary>
		// Token: 0x0400131D RID: 4893
		I386 = 332,
		/// <summary>Targets a 64-bit Intel processor.</summary>
		// Token: 0x0400131E RID: 4894
		IA64 = 512,
		/// <summary>Targets a 64-bit AMD processor.</summary>
		// Token: 0x0400131F RID: 4895
		AMD64 = 34404,
		/// <summary>Targets an ARM processor.</summary>
		// Token: 0x04001320 RID: 4896
		ARM = 452
	}
}
