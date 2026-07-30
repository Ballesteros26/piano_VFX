using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.VARFLAGS" /> that were originally imported for this field from the COM type library.</summary>
	// Token: 0x020008BC RID: 2236
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for the attributed field as found in the type library it was imported from. </param>
		// Token: 0x06005501 RID: 21761 RVA: 0x0012856F File Offset: 0x0012676F
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVarAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for the attributed field as found in the type library it was imported from. </param>
		// Token: 0x06005502 RID: 21762 RVA: 0x0012856F File Offset: 0x0012676F
		public TypeLibVarAttribute(short flags)
		{
			this._val = (TypeLibVarFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for this field.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibVarFlags" /> value for this field.</returns>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x0012857E File Offset: 0x0012677E
		public TypeLibVarFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C3A RID: 11322
		internal TypeLibVarFlags _val;
	}
}
