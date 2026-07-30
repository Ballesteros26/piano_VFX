using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> that were originally imported for this method from the COM type library.</summary>
	// Token: 0x020008BB RID: 2235
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the TypeLibFuncAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from. </param>
		// Token: 0x060054FE RID: 21758 RVA: 0x00128558 File Offset: 0x00126758
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the TypeLibFuncAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from. </param>
		// Token: 0x060054FF RID: 21759 RVA: 0x00128558 File Offset: 0x00126758
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</returns>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x00128567 File Offset: 0x00126767
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C39 RID: 11321
		internal TypeLibFuncFlags _val;
	}
}
