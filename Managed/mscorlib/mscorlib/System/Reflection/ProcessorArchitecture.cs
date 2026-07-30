using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
	// Token: 0x020002D6 RID: 726
	[ComVisible(true)]
	[Serializable]
	public enum ProcessorArchitecture
	{
		/// <summary>An unknown or unspecified combination of processor and bits-per-word.</summary>
		// Token: 0x04001180 RID: 4480
		None,
		/// <summary>Neutral with respect to processor and bits-per-word.</summary>
		// Token: 0x04001181 RID: 4481
		MSIL,
		/// <summary>A 32-bit Intel processor, either native or in the Windows on Windows environment on a 64-bit platform (WOW64).</summary>
		// Token: 0x04001182 RID: 4482
		X86,
		/// <summary>A 64-bit Intel processor only.</summary>
		// Token: 0x04001183 RID: 4483
		IA64,
		/// <summary>A 64-bit AMD processor only.</summary>
		// Token: 0x04001184 RID: 4484
		Amd64,
		/// <summary>An ARM processor.</summary>
		// Token: 0x04001185 RID: 4485
		Arm
	}
}
