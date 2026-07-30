using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information needed for transferring a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x0200098F RID: 2447
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		/// <summary>Reserved; set to null.</summary>
		// Token: 0x04002E8A RID: 11914
		public IntPtr dwReserved;

		/// <summary>Indicates an <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> value describing the type.</summary>
		// Token: 0x04002E8B RID: 11915
		public IDLFLAG wIDLFlags;
	}
}
