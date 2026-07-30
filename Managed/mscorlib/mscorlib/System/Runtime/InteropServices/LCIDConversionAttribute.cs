using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a method's unmanaged signature expects a locale identifier (LCID) parameter.</summary>
	// Token: 0x020008AE RID: 2222
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the LCIDConversionAttribute class with the position of the LCID in the unmanaged signature.</summary>
		/// <param name="lcid">Indicates the position of the LCID argument in the unmanaged signature, where 0 is the first argument. </param>
		// Token: 0x060054E9 RID: 21737 RVA: 0x001283E2 File Offset: 0x001265E2
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		/// <summary>Gets the position of the LCID argument in the unmanaged signature.</summary>
		/// <returns>The position of the LCID argument in the unmanaged signature, where 0 is the first argument.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x001283F1 File Offset: 0x001265F1
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C04 RID: 11268
		internal int _val;
	}
}
