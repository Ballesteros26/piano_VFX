using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the data between managed and unmanaged code.</summary>
	// Token: 0x02000920 RID: 2336
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MarshalAsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as. </param>
		// Token: 0x060056D5 RID: 22229 RVA: 0x0012A8A1 File Offset: 0x00128AA1
		public MarshalAsAttribute(short unmanagedType)
		{
			this.utype = (UnmanagedType)unmanagedType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> enumeration member.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as. </param>
		// Token: 0x060056D6 RID: 22230 RVA: 0x0012A8A1 File Offset: 0x00128AA1
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</returns>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x0012A8B0 File Offset: 0x00128AB0
		public UnmanagedType Value
		{
			get
			{
				return this.utype;
			}
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x0012A8B8 File Offset: 0x00128AB8
		internal MarshalAsAttribute Copy()
		{
			return (MarshalAsAttribute)base.MemberwiseClone();
		}

		/// <summary>Provides additional information to a custom marshaler.</summary>
		// Token: 0x04002DB1 RID: 11697
		public string MarshalCookie;

		/// <summary>Specifies the fully qualified name of a custom marshaler.</summary>
		// Token: 0x04002DB2 RID: 11698
		[ComVisible(true)]
		public string MarshalType;

		/// <summary>Implements <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> as a type.</summary>
		// Token: 0x04002DB3 RID: 11699
		[ComVisible(true)]
		public Type MarshalTypeRef;

		/// <summary>Indicates the user-defined element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002DB4 RID: 11700
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04002DB5 RID: 11701
		private UnmanagedType utype;

		/// <summary>Specifies the element type of the unmanaged <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> or <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.</summary>
		// Token: 0x04002DB6 RID: 11702
		public UnmanagedType ArraySubType;

		/// <summary>Indicates the element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002DB7 RID: 11703
		public VarEnum SafeArraySubType;

		/// <summary>Indicates the number of elements in the fixed-length array or the number of characters (not bytes) in a string to import.</summary>
		// Token: 0x04002DB8 RID: 11704
		public int SizeConst;

		/// <summary>Specifies the parameter index of the unmanaged iid_is attribute used by COM.</summary>
		// Token: 0x04002DB9 RID: 11705
		public int IidParameterIndex;

		/// <summary>Indicates the zero-based parameter that contains the count of array elements, similar to size_is in COM.</summary>
		// Token: 0x04002DBA RID: 11706
		public short SizeParamIndex;
	}
}
