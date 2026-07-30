using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IDLDESC" /> instead.</summary>
	// Token: 0x020008FA RID: 2298
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		/// <summary>Reserved; set to null.</summary>
		// Token: 0x04002D2B RID: 11563
		public int dwReserved;

		/// <summary>Indicates an <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> value describing the type.</summary>
		// Token: 0x04002D2C RID: 11564
		public IDLFLAG wIDLFlags;
	}
}
