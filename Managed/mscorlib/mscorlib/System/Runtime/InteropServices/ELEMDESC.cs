using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC" /> instead.</summary>
	// Token: 0x020008FE RID: 2302
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x04002D3A RID: 11578
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x04002D3B RID: 11579
		public ELEMDESC.DESCUNION desc;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC.DESCUNION" /> instead.</summary>
		// Token: 0x020008FF RID: 2303
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x04002D3C RID: 11580
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x04002D3D RID: 11581
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
