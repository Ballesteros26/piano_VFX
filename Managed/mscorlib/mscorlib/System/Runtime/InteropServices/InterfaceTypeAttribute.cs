using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether a managed interface is dual, dispatch-only, or IUnknown -only when exposed to COM.</summary>
	// Token: 0x020008A8 RID: 2216
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">One of the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> values that describes how the interface should be exposed to COM clients. </param>
		// Token: 0x060054DD RID: 21725 RVA: 0x0012836A File Offset: 0x0012656A
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">Describes how the interface should be exposed to COM clients. </param>
		// Token: 0x060054DE RID: 21726 RVA: 0x0012836A File Offset: 0x0012656A
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</returns>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x00128379 File Offset: 0x00126579
		public ComInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002BFB RID: 11259
		internal ComInterfaceType _val;
	}
}
