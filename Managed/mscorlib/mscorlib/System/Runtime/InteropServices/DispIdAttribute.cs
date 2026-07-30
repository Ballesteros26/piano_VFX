using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the COM dispatch identifier (DISPID) of a method, field, or property.</summary>
	// Token: 0x020008A6 RID: 2214
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	public sealed class DispIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the DispIdAttribute class with the specified DISPID.</summary>
		/// <param name="dispId">The DISPID for the member. </param>
		// Token: 0x060054DB RID: 21723 RVA: 0x00128353 File Offset: 0x00126553
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		/// <summary>Gets the DISPID for the member.</summary>
		/// <returns>The DISPID for the member.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x00128362 File Offset: 0x00126562
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002BF5 RID: 11253
		internal int _val;
	}
}
