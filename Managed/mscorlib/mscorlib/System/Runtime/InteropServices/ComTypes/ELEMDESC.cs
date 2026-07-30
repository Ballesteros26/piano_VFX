using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the type description and process transfer information for a variable, function, or a function parameter.</summary>
	// Token: 0x02000993 RID: 2451
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x04002E99 RID: 11929
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x04002E9A RID: 11930
		public ELEMDESC.DESCUNION desc;

		/// <summary>Contains information about an element. </summary>
		// Token: 0x02000994 RID: 2452
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x04002E9B RID: 11931
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x04002E9C RID: 11932
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
