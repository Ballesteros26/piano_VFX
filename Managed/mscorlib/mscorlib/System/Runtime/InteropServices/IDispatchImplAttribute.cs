using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which IDispatch implementation the common language runtime uses when exposing dual interfaces and dispinterfaces to COM.</summary>
	// Token: 0x020008B4 RID: 2228
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the IDispatchImplAttribute class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used. </param>
		// Token: 0x060054F1 RID: 21745 RVA: 0x00128427 File Offset: 0x00126627
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		/// <summary>Initializes a new instance of the IDispatchImplAttribute class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used. </param>
		// Token: 0x060054F2 RID: 21746 RVA: 0x00128427 File Offset: 0x00126627
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</returns>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x00128436 File Offset: 0x00126636
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C0B RID: 11275
		internal IDispatchImplType _val;
	}
}
