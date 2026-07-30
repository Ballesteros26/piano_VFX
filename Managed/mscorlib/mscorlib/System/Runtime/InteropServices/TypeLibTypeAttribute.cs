using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> that were originally imported for this type from the COM type library.</summary>
	// Token: 0x020008BA RID: 2234
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the TypeLibTypeAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from. </param>
		// Token: 0x060054FB RID: 21755 RVA: 0x00128541 File Offset: 0x00126741
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the TypeLibTypeAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from. </param>
		// Token: 0x060054FC RID: 21756 RVA: 0x00128541 File Offset: 0x00126741
		public TypeLibTypeAttribute(short flags)
		{
			this._val = (TypeLibTypeFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</returns>
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x00128550 File Offset: 0x00126750
		public TypeLibTypeFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C38 RID: 11320
		internal TypeLibTypeFlags _val;
	}
}
