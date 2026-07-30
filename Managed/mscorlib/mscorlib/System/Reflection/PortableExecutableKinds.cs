using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the nature of the code in an executable file.</summary>
	// Token: 0x0200033C RID: 828
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PortableExecutableKinds
	{
		/// <summary>The file is not in portable executable (PE) file format.</summary>
		// Token: 0x0400136C RID: 4972
		NotAPortableExecutableImage = 0,
		/// <summary>The executable contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.</summary>
		// Token: 0x0400136D RID: 4973
		ILOnly = 1,
		/// <summary>The executable can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.</summary>
		// Token: 0x0400136E RID: 4974
		Required32Bit = 2,
		/// <summary>The executable requires a 64-bit platform.</summary>
		// Token: 0x0400136F RID: 4975
		PE32Plus = 4,
		/// <summary>The executable contains pure unmanaged code.</summary>
		// Token: 0x04001370 RID: 4976
		Unmanaged32Bit = 8,
		/// <summary>The executable is platform-agnostic but should be run on a 32-bit platform whenever possible.</summary>
		// Token: 0x04001371 RID: 4977
		Preferred32Bit = 16
	}
}
